using IgniteX.UI.Models;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace IgniteX.UI.ViewModels
{
    public class PlcConfigViewModel:BindableBase
    {

        public PlcConfigViewModel()
        {
            ProcessList = new ObservableCollection<Process>()
            {
                new Process() {StationName="塑料套颜色识别",IsComm="通讯正常",ProcessColor=Colors.Green},
                new Process() {StationName="极性识别",IsComm="通讯正常",ProcessColor=Colors.Green},
                new Process() {StationName="桥路电阻检测",IsComm="通讯正常",ProcessColor=Colors.Green},
                new Process() {StationName="绝缘电阻检测",IsComm="通讯正常",ProcessColor=Colors.Green},
                new Process() {StationName="热瞬态检测",IsComm="通讯正常",ProcessColor=Colors.Green},
                new Process() {StationName="尺寸检测",IsComm="通讯正常",ProcessColor=Colors.Green},
                new Process() {StationName="喷码",IsComm="通讯正常",ProcessColor=Colors.Green},
                new Process() {StationName="外观检测",IsComm="通讯失败",ProcessColor=Colors.OrangeRed},
            };
        }
        private bool _isVisibled=true;

		public bool IsVisibled
        {
			get { return _isVisibled; }
			set { SetProperty<bool>(ref _isVisibled, value); }
		}
        private ObservableCollection<Process> _processList;

        public ObservableCollection<Process> ProcessList
        {
            get { return _processList; }
            set { SetProperty(ref _processList, value); }
        }

        private DelegateCommand _showProcessCommand;
        public DelegateCommand ShowProcessCommand =>
            _showProcessCommand ?? (_showProcessCommand = new DelegateCommand(ExecuteCommand));

        private void ExecuteCommand()
        {
            IsVisibled=!IsVisibled;
        }
	}
}
