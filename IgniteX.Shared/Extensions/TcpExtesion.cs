using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Channels;
using System.Threading;
using System.Threading.Tasks;

namespace IgniteX.Shared.Extensions
{
    public static class TcpExtension
    {
        public static ChannelReader<byte[]> ReadDataFromTcp(TcpClient client, int bufferSize = 1024, CancellationToken token = default)
        {
            var output = Channel.CreateBounded<byte[]>(capacity: -1); // 创建无界通道

            Task.Run(async () =>
            {
                using (var networkStream = client.GetStream())
                {
                    while (!token.IsCancellationRequested)
                    {
                        var buffer = new byte[bufferSize];
                        var bytesRead = await networkStream.ReadAsync(buffer, 0, buffer.Length, token).ConfigureAwait(false);
                        if (bytesRead > 0)
                        {
                            var data = new byte[bytesRead];
                            Array.Copy(buffer, data, bytesRead);
                            await output.Writer.WriteAsync(data, token).ConfigureAwait(false); // 将数据写入通道
                        }
                        else
                        {
                            break; // 如果没有读取到字节表示连接已断开
                        }
                    }
                }

                output.Writer.Complete(); // 停止写入
            }, token);

            return output.Reader; // 返回读取器
        }

        public static async Task WriteDataToTcp(TcpClient client, ChannelReader<byte[]> input, CancellationToken token = default)
        {
            try
            {
                using (var networkStream = client.GetStream())
                {
                    await foreach (var data in input.ReadAllAsync(token).ConfigureAwait(false))
                    {
                        await networkStream.WriteAsync(data, 0, data.Length, token).ConfigureAwait(false); // 将数据写入网络流
                    }
                }
            }
            finally
            {
                client.Close(); // 关闭 TCP 连接
            }
        }
    }
}
