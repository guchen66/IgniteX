using IgniteX.Shared.Commands;
using IgniteX.UI.Models;
using IgniteX.UI.Views.Dialogs;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace IgniteX.UI.ViewModels
{
   public class RepiceArgsViewModel:BindableBase
    {
        public ObservableCollection<User> Users { get; set; }


        public ICommand ShowWindowCommand { get; set; }
        public RepiceArgsViewModel()
        {
            Users = UserManager.GetUsers();

            ShowWindowCommand = new DelegateCommand<object>(ShowWindow, CanShowWindow);
        
        }

        private bool CanShowWindow(object obj)
        {
            return true;
        }

        private void ShowWindow(object obj)
        {
          /*  var mainWindow = obj as Window;

            AddUser addUserWin = new AddUser();
            addUserWin.Owner = mainWindow;
            addUserWin.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            addUserWin.Show();*/


        }
    }
}
