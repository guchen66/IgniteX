using IgniteX.Shared.Events;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace IgniteX.UI.ViewModels
{
    public class HeaderViewModel : BindableBase
    {
        private readonly IEventAggregator _eventAggregator;
        public HeaderViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;

            CloseCommand = new DelegateCommand(ExecuteClose);
            MinCommand = new DelegateCommand(ExecuteMin);
            MaxCommand = new DelegateCommand(ExecuteMax);//, ThreadOption.UIThread
        }

        public DelegateCommand CloseCommand { get; }
        public DelegateCommand MinCommand { get; }
        public DelegateCommand MaxCommand { get; }

        private void ExecuteClose()
        {
            Application.Current.Dispatcher.Invoke(() => Application.Current.Shutdown());
        }

        private void ExecuteMin()
        {
            /* Application.Current.Dispatcher.Invoke(() =>
             {
                 if (_mainView.WindowState == WindowState.Maximized)
                 {
                     _mainView.WindowState = WindowState.Minimized;
                 }
             });*/

            _eventAggregator.GetEvent<MinimizeEvent>().Publish();
        }

        private void ExecuteMax()
        {
            _eventAggregator.GetEvent<MaximizeEvent>().Publish();
            /* Application.Current.Dispatcher.Invoke(() =>
             {
                 _mainView.WindowState = _mainView.WindowState == WindowState.Maximized ? WindowState.Normal : WindowState.Maximized;
             });*/
        }

        private DelegateCommand<string> _dragMoveCommand;
        public DelegateCommand<string> DragMoveCommand =>
            _dragMoveCommand ?? (_dragMoveCommand = new DelegateCommand<string>(ExecuteDragMoveCommand, CanExecuteDragMoveCommand));

        private void ExecuteDragMoveCommand(string parameter)
        {
            Application.Current.MainWindow.DragMove();

        }

        private bool CanExecuteDragMoveCommand(string parameter)
        {
            return true;
        }
    }
}
