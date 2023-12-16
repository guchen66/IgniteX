using IgniteX.Shared.Models.OperatePlc;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IgniteX.UI.ViewModels
{
    public class AlarmHistoryViewModel:BindableBase
    {
        public AlarmHistoryViewModel()
        {
            GridModelList = new ObservableCollection<PlcFaultInfo>()
            {

                new PlcFaultInfo()
                {
                    AlarmCause="11",AlarmDetail="11",AlarmLevel="11",AlarmStation="11",AlarmStatus="11",AlarmTime=new DateTime(),Measure="11",
                },
                new PlcFaultInfo()
                {
                    AlarmCause="11",AlarmDetail="11",AlarmLevel="11",AlarmStation="11",AlarmStatus="11",AlarmTime=new DateTime(),Measure="11",
                },
                new PlcFaultInfo()
                {
                    AlarmCause="11",AlarmDetail="11",AlarmLevel="11",AlarmStation="11",AlarmStatus="11",AlarmTime=new DateTime(),Measure="11",
                },
            };
        }

        private ObservableCollection<PlcFaultInfo> gridModelList;
        public ObservableCollection<PlcFaultInfo> GridModelList//和前台要对应
        {
            get { return gridModelList; }
            set { gridModelList = value; RaisePropertyChanged(); }
        }
    }

   

}
