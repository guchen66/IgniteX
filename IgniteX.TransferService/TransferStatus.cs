using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IgniteX.TransferService
{
    /// <summary>
    /// 上传和下载中文件的状态
    /// </summary>
    public enum TransferStatus
    {
        /// <summary>
        /// 任务已就绪，待处理状态
        /// </summary>
        Ready,

        /// <summary>
        /// 转移
        /// 正在下载或上载的活动状态
        /// </summary>
        Transferring,

        /// <summary>
        /// 暂停
        /// </summary>
        Suspended,

        /// <summary>
        /// 任务中断，任务失败
        /// </summary>
        Faulted,

        /// <summary>
        /// 任务完成
        /// </summary>
        Completed,

        /// <summary>
        /// 任务结束状态
        /// </summary>
        Disposed
    }
}
