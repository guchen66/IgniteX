using Bogus.DataSets;
using IgniteX.Shared;
using IgniteX.Shared.Commands;
using IgniteX.Shared.Enums;
using IgniteX.Shared.Extensions;
using NPOI.SS.Formula.Functions;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using SqlSugarDB.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
namespace IgniteX.UI.ViewModels.Dialogs
{
    public class ExcelDownDialogViewModel : BindableBase, IDialogAware
    {

        public Action action { get; set; }
        IgniteTubeService db = new IgniteTubeService();
       
        public ExcelDownDialogViewModel()
        {
            
        }
      
        public string Title => "下载弹窗";
        public string DownFileName { get; set; }
        public event Action<IDialogResult> RequestClose;

        public bool CanCloseDialog()
        {
            return true;
        }

        public void OnDialogClosed()
        {
          //  action();
        }

        public void OnDialogOpened(IDialogParameters parameters)
        {
           /* if (parameters.ContainsKey("RefreshValue"))
            {
                action = parameters.GetValue<Action>("RefreshValue");

            }*/
           
            if (parameters.ContainsKey("prefixName"))
            {
                DownFileName = parameters.GetValue<string>("prefixName");
            }
        }
        /// <summary>
        /// 取消下载
        /// </summary>CanelCommand
        private DelegateCommand _canelCommand;
        public DelegateCommand CancelCommand  =>
            _canelCommand ?? (_canelCommand = new DelegateCommand(ExecuteCanel));

        private void ExecuteCanel()
        {
           
            RaiseRequestClose(new DialogResult(ButtonResult.Cancel));
        }

        /// <summary>
        /// 确认下载Excel文件
        /// </summary>
        private AsyncCommand<string> _confirmCommand { get; set; }
        public AsyncCommand<string> ConfirmCommand =>
            _confirmCommand ?? (_confirmCommand = new AsyncCommand<string>(ExecuteConfirm));

        private async Task ExecuteConfirm(string name)
        {
            name = DownFileName;
            // 定义一个字典来存储事件与处理方法的映射关系
            Dictionary<string, Func<Task<bool>>> eventHandlers = new Dictionary<string, Func<Task<bool>>>();

            // 初始化字典，将事件名称和对应的处理方法添加到字典中
            eventHandlers.Add(MenuNames.IgniteTube, async () =>
            {
                return await ExcelExtension.WriteIgniteDataToExcel(name);
            });

            eventHandlers.Add(MenuNames.DeviceState, async() =>
            {
                return await ExcelExtension.WriteDeviceStateDataToExcel(name);
            });

            eventHandlers.Add(MenuNames.RepiceList, async () =>
            {
                return await ExcelExtension.WriteRepiceListToExcel(name);
            });
            // 添加其他事件和处理方法...

            // 根据 eventName 执行对应的处理方法
            if (eventHandlers.TryGetValue(name, out var handler))
            {
                bool result = await handler.Invoke();  // 使用await等待异步方法的完成，并获取其返回值
                if (result) 
                {
                    RaiseRequestClose(new DialogResult(ButtonResult.OK));
                }
                else
                {
                    RaiseRequestClose(new DialogResult(ButtonResult.No));
                }
            }
           
        }

        //触发窗体关闭事件
        public virtual void RaiseRequestClose(IDialogResult dialogResult)
        {
           
            RequestClose?.Invoke(dialogResult);
        }
    }
}
