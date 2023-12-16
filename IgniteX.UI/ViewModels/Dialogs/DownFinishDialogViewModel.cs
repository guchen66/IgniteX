using IgniteX.UI.Views.Dialogs;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace IgniteX.UI.ViewModels.Dialogs
{
    internal class DownFinishDialogViewModel : BindableBase, IDialogAware
    {
        public string Title => "完成弹窗";

        public event Action<IDialogResult> RequestClose;

        public bool CanCloseDialog()
        {
            return true;
        }

        public void OnDialogClosed()
        {
            
        }

        public void OnDialogOpened(IDialogParameters parameters)
        {
            
        }

       /* private void ShowPopupWindow()
        {
            PopupWindow popup = new PopupWindow();

            // 设置窗口的样式
            popup.WindowStyle = WindowStyle.None;

            // 设置窗口的大小和内容自适应
            popup.SizeToContent = SizeToContent.WidthAndHeight;

            // 设置不显示在任务栏
            popup.ShowInTaskbar = false;

            // 将窗口定位到右下角
            popup.WindowStartupLocation = WindowStartupLocation.Manual;
            popup.Left = SystemParameters.WorkArea.Width - popup.Width;
            popup.Top = SystemParameters.WorkArea.Height - popup.Height;

            // 显示弹窗
            popup.ShowDialog();
        }*/
    }
}
