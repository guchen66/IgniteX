using IgniteX.UI.ViewModels;
using IgniteX.UI.ViewModels.Dialogs;
using IgniteX.UI.Views;
using IgniteX.UI.Views.Dialogs;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using System.Runtime.CompilerServices;

namespace IgniteX.UI
{
    public class UIModule : IModule
    {
       
        public void OnInitialized(IContainerProvider containerProvider)
        {
            
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            //注册界面
            containerRegistry.RegisterForNavigation<HomeView, HomeViewModel>();
            containerRegistry.RegisterForNavigation<IgniteTubeView, IgniteTubeViewModel>();
            containerRegistry.RegisterForNavigation<DeviceStateView, DeviceStateViewModel>();
            containerRegistry.RegisterForNavigation<ResistanceView, ResistanceViewModel>();
            containerRegistry.RegisterForNavigation<ThermalChangeView, ThermalChangeViewModel>();
            containerRegistry.RegisterForNavigation<PlcConfigView, PlcConfigViewModel>();
            containerRegistry.RegisterForNavigation<RepiceArgs, RepiceArgsViewModel>();
            containerRegistry.RegisterForNavigation<RepiceList, RepiceListViewModel>();
            containerRegistry.RegisterForNavigation<AlarmLogging, AlarmLoggingViewModel>();
            containerRegistry.RegisterForNavigation<AlarmHistory, AlarmHistoryViewModel>();
            //注册窗体
            containerRegistry.RegisterDialog<ExcelDownDialog, ExcelDownDialogViewModel>();
            containerRegistry.RegisterDialog<RefreshDialog, RefreshDialogViewModel>();
        }
    }
}