using IgniteX.Common;
using IgniteX.Models;
using Microsoft.Extensions.Configuration;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace IgniteX.ViewModels
{
    public class LoginViewModel : BindableBase
    {

        #region  字段
        public AppData appData { get; set; } = AppData.Instance;
      /*  public SimpleClient<User> sdb = new SimpleClient<User>();
        UserService db = new UserService();
        private SignUpArgs _signUpArgs;*/
        private bool _isRememberPassword;
        private bool _isAutoSignIn;

        /// <summary>
        /// 输入用户名
        /// </summary>
        private string _inputName;
        public string InputName
        {
            get { return _inputName; }
            set { SetProperty<string>(ref _inputName, value); }
        }

        /// <summary>
        /// 输入密码
        /// </summary>
        private string _inputPwd;

        public string InputPwd
        {
            get { return _inputPwd; }
            set { SetProperty<string>(ref _inputPwd, value); }
        }



        #endregion

        #region  属性

        /// <summary>
        /// 记住密码
        /// </summary>
        public bool IsRememberPassword
        {
            get => _isRememberPassword;
            set { if (SetProperty(ref _isRememberPassword, value) && !value) IsAutoSignIn = false; }
        }

        /// <summary>
        /// 自动登录
        /// </summary>
        public bool IsAutoSignIn
        {
            get => _isAutoSignIn;
            set { if (SetProperty(ref _isAutoSignIn, value) && value) IsRememberPassword = true; }
        }
        #endregion


        #region 命令

        #endregion

        /// <summary>
        /// 带参登录
        /// </summary>
        private DelegateCommand<Window> _loginCommand;
        public DelegateCommand<Window> LoginCommand =>
            _loginCommand ?? (_loginCommand = new DelegateCommand<Window>(DoLogin));

        private void DoLogin(Window win)
        {
            /*MD5Encrypt Encrypt = new MD5Encrypt();
            string encryPwd = Encrypt.GetMD5Provider(InputPwd, InputName);*/

            Task.Delay(6500).ContinueWith(t => { });
            win.DialogResult = true;
           // win.Close();
            // var user = db.GetAllUsers().FirstOrDefault(item => item.Name == appData.CurrentUser.Name
            // && item.Password == encryPwd);

            /* if (user == null)
             {
                 MessageBox.Show("用户名和密码错误");
             }
             else
             {
                 win.DialogResult = true;
                 win.Close();
             }*/
        }

        /// <summary>
        /// 取消操作
        /// </summary>
        private DelegateCommand _cancelCommand;
        public DelegateCommand CancelCommand =>
            _cancelCommand ?? (_cancelCommand = new DelegateCommand(DoCancel));

        private void DoCancel()
        {
            //App.Current.Shutdown();
            Application.Current.Shutdown();
        }

        private static bool CanSignIn(string username, string password) => !string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password);


    }
}
