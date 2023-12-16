using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IgniteX.TransferService
{
    /// <summary>
    /// 转移通知
    /// </summary>
    public struct TransferNotification
    {
        public TransferNotification(long offset, TransferStatus status, long bytes)
        {
            Offset = offset;
            Status = status;
            Bytes = bytes;
        }

        public long Offset { get; }

        public TransferStatus Status { get; }

        public long Bytes { get; }
    }
}
