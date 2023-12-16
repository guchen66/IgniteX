using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace IgniteX.UI.ViewModels.Dialogs
{
    public class RefreshDialogViewModel : BindableBase, IDialogAware
    {
        private Timer _timer;
        private readonly IDialogService _dialogService;
        private readonly Dispatcher _dispatcher;

        public string Title => "刷新界面";

        public event Action<IDialogResult> RequestClose;

        public RefreshDialogViewModel(IDialogService dialogService)
        {
            _dialogService = dialogService;
            _dispatcher = Application.Current.Dispatcher;
        }

        public bool CanCloseDialog()
        {
            return true;
        }

        public void OnDialogClosed()
        {

        }

        public void OnDialogOpened(IDialogParameters parameters)
        {
            // 在2秒后关闭弹窗
            _timer = new Timer(CloseDialog, null, TimeSpan.FromSeconds(2), Timeout.InfiniteTimeSpan);
        }

        private void CloseDialog(object state)
        {
            // 在UI线程上关闭弹窗
            _dispatcher.Invoke(() =>
            {
                RequestClose?.Invoke(new DialogResult(ButtonResult.None));
            });
        }
    }
}
