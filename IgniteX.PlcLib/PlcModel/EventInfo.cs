using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IgniteX.PlcLib.PlcModel
{
    public class EventInfo
    {
        public int Idx { get; set; }

        //public string Name;
        public string ReadAddr { get; set; }

        public ushort ReadLen { get; set; }
        public string ReadClassName { get; set; }
        public string WriteAddr { get; set; }
        public ushort WriteLen { get; set; }
        public string WriteClassName { get; set; }
        public byte[] WriteBuffer { get; set; }
        public bool TriggerCompleted { get; set; }
        public int SequenceIDR { get; set; } = -1;
        public int SequenceIDW { get; set; } = -1;
        public dynamic ObjR { get; set; }
        public dynamic ObjW { get; set; }
    }
}