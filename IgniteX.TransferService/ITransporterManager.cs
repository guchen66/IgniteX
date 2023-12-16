using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IgniteX.TransferService
{
    /// <summary>
    /// 传输数据的管理
    /// </summary>
    /// <typeparam name="TTransferInfo"></typeparam>
    /// <typeparam name="TContext"></typeparam>
    public interface ITransporterManager<TTransferInfo, TContext>
        where TTransferInfo : ITransferInfo<TContext>
    {
        IEnumerable<TTransferInfo> Transporters { get; }

        int MaxConcurrent { get; set; }

        bool Add(TTransferInfo transporter);

        void AsNext(Guid id);

        void Ready(Guid id);

        void Suspend(Guid id);

        void StartAll();

        void SuspendAll();
    }

    /// <summary>
    /// 下载的管理，实现数据的管理
    /// </summary>
   /* public interface IDownloaderManager : ITransporterManager<IDownloader, DownloadContext>
    {
    }*/
}
