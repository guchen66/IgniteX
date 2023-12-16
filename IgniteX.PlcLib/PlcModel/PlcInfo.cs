using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IgniteX.PlcLib.PlcModel
{
    public class PlcInfo : BindableBase
    {
        public string OP { get; set; }
        public string IP { get; set; }
        public int Port { get; set; }
        public byte Rack { get; set; }
        public byte Slot { get; set; }
        public string Version { get; set; }
        private bool _IsConn;

        public bool IsConn
        {
            get { return _IsConn; }
            set { SetProperty(ref _IsConn, value); }
        }

        public string Remark { get; set; }
        public PublicInfo PI { get; set; }
        public List<EventInfo> EIs { get; set; }
    }
}