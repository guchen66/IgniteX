using IgniteX.Shared.Calculations;
using IgniteX.Shared.Common;
using IgniteX.Shared.Models;
using IgniteX.UI.Models;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using SqlSugarDB.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Windows.Input;
using System.Windows.Media;

namespace IgniteX.UI.ViewModels
{
    public class HomeViewModel:BindableBase
    {
        public DesignData DesignData { get; set; } = DesignData.Instance;
        private readonly SubscriptionToken _subscriptionToken;
        private readonly ILog _logger;
        DataResultService db=new DataResultService();
        
        public HomeViewModel(ILog logger)
        {
            _logger = logger;
            ResistanceValue resistanceValue = new ResistanceValue();
            resistanceValue.CalculationCompleted += ResistanceValue_CalculationCompleted;
            resistanceValue.CalculateData(null,()=>new ElectronicData());
        }
        private ObservableCollection<ElectronicData> _gridModelList = new ObservableCollection<ElectronicData>();
        public ObservableCollection<ElectronicData> GridModelList//和前台要对应
        {
            get { return _gridModelList; }
            set { _gridModelList = value; RaisePropertyChanged(); }
        }
        private void ResistanceValue_CalculationCompleted(object sender, CalculationEventArgs e)
        {
            if (e.CalculationResult != null) // 添加防呆判断，确保结果不为null
            {
                GridModelList.Add(e.CalculationResult);
            }
        }
    }
}
