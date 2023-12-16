using Bogus;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IgniteX.Shared.Models.OperatePlc
{
    public class PlcShowData:ObservableCollection<PLCInfo>
    {
        public PlcShowData()
        {
            Add(new PLCInfo(1,"张三", "19", "大一"));
            Add(new PLCInfo(2,"李四", "20", "大二"));
            Add(new PLCInfo(3,"王五", "30", "大七"));
            Add(new PLCInfo(4,"赵六", "18", "大一"));
        }
    }

    public class PLCInfo
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Sex { get; set; }
        public string Type { get; set; }

        public PLCInfo(int id, string name, string sex, string type)
        {
            Id = id;
            Name = name;
            Sex = sex;
            Type = type;
        }
    }
}
