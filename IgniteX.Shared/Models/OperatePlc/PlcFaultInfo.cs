using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IgniteX.Shared.Models.OperatePlc
{
    public class PlcFaultInfo:BindableBase
    {
        public int Id { get; set; }
        public string AlarmStation { get; set; }
        public string AlarmStatus { get; set; }
        public DateTime AlarmTime { get; set; }
        public string AlarmCause { get; set; }         //原因
        public string AlarmLevel { get; set; }          //级别
        public string AlarmDetail { get; set; }
        public string Measure { get; set; }            //报警措施
    }
}
