using Bogus;
using IgniteX.Shared;
using IgniteX.Shared.Common;
using IgniteX.Shared.Extensions;
using IgniteX.UI;
using IgniteX.ViewModels;
using IgniteX.Views;
using Microsoft.Extensions.Configuration;
using MySqlConnector.Logging;
using Prism.Events;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using Prism.Services.Dialogs;
using SqlSugarDB.Config;
using System;
using System.Windows;

namespace IgniteX
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
       

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
           
            // 尝试建立数据库连接
            bool isDatabaseAvailable = CheckDatabaseConnection();

            // 判断是否有数据库连接
            if (isDatabaseAvailable)
            {
                // 正常运行程序
                RunApplication();
            }
            else
            {
                // 切换到离线模式
                SwitchToOfflineMode();
            }
        }

        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }
        protected override void InitializeShell(Window shell)
        {
            base.InitializeShell(shell);
           /* if (Container.Resolve<LoginView>().ShowDialog() == true)
            {
                base.InitializeShell(shell);
            }
            else
            {
                Application.Current?.Shutdown();
            }*/
        }
        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
          
            // 注册 ILog 日志接口和 NLogLogger 实现类
            containerRegistry.Register<ILog, NLogLogger>();
            containerRegistry.Register<IProProcessService, ProProcessService>();

        }
        //新建类库，通过模块化传入用户控件
        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            //注册模块就行
            moduleCatalog.AddModule<UIModule>();
            base.ConfigureModuleCatalog(moduleCatalog);
        }

        protected override void Initialize()
        {
            base.Initialize();
            SugarGlobal.Initialized();
        }
        protected override void OnInitialized()
        {
           
            //注册WatchDog
            //Container.Resolve<WatchLog>();
            //他妈的一个星期了，艹
            // var regionManager = containerProvider.Resolve<IRegionManager>();
            base.OnInitialized();
            var regionManager = Container.Resolve<IRegionManager>();

            regionManager.RequestNavigate("ContentRegion", "HomeView");
           
       //     regionManager.RequestNavigate("HeaderRegion", "HeaderView");
           // regionManager.RequestNavigate("FooterRegion", "FooterView");
        }
        private bool CheckDatabaseConnection()
        {
            try
            {
                // 在这里进行数据库连接测试的逻辑
                // 如果连接成功，返回true；否则返回false
            }
            catch (Exception ex)
            {
                // 处理数据库连接异常
                // 返回false表明连接失败
            }

            return false;
        }

        private void RunApplication()
        {
            // 在这里启动正常的应用程序逻辑
        }

        private void SwitchToOfflineMode()
        {
            // 在这里切换到离线模式的逻辑
            // 可以通过修改界面状态或导航到相关的404页面实现
        }
    }
}
