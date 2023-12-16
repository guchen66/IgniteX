using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IgniteX.PlcLib.PlcModel
{
    public class PublicInfo
    {
        public string ReadAddr { get; set; }
        public ushort ReadLen { get; set; }
        public string ReadClassName { get; set; }
        public string WriteAddr { get; set; }
        public ushort WriteLen;
        public string WriteClassName { get; set; }
        public byte[] WriteBuffer { get; set; }
        public dynamic ObjR { get; set; }
        public dynamic ObjW { get; set; }
    }
}