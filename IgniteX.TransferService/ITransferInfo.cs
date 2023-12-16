using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IgniteX.TransferService
{
    /// <summary>
    /// 传输数据的信息
    /// 它应该有Guid、有数据上下文、状态、实时路径
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ITransferInfo<out T> : IObservable<TransferNotification>, IDisposable
    {
        Guid Id { get; }

        T Context { get; }

        TransferStatus Status { get; }

      //  IReadOnlyDictionary<long, BlockTransferContext> BlockContexts { get; }
    }

    /// <summary>
    /// 传输的操作，运行和停止
    /// </summary>
    public interface ITransferOperations
    {
        void Run();

        void Stop();
    }

   /* public interface IDownloader : ITransferInfo<DownloadContext>, ITransferOperations
    {
    }*/
}
