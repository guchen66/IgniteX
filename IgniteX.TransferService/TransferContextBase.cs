using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IgniteX.TransferService
{
    /// <summary>
    /// 传输上下文库，上位机中可以表示传输过去的数据
    /// </summary>
    public class TransferContextBase
    {
        [JsonProperty]
        public Guid Id { get; protected set; }

        [JsonProperty]
        public long TotalSize { get; internal set; }

        [JsonProperty]
        public virtual string LocalPath { get; internal set; }
    }
}
