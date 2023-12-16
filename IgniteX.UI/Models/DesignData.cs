using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IgniteX.UI.Models
{
    /// <summary>
    /// 设置一个全局数据集合点
    /// </summary>
    public class DesignData : BindableBase
    {
        public static readonly DesignData Instance = new Lazy<DesignData>(() => new DesignData()).Value;

        public DesignData()
        {
            TaskBars = new ObservableCollection<TaskBar>();
           /* CreateMenuBar();
            CreateTestData();*/
            CreateTaskBars();
        }

      
      /*  public ObservableCollection<MenuBar> menuBars;

        public ObservableCollection<MenuBar> MenuBars
        {
            get { return menuBars; }
            set { menuBars = value; RaisePropertyChanged(); }
        }
        public void CreateMenuBar()
        {
            MenuBars = new ObservableCollection<MenuBar>();
            MenuBars.Add(new MenuBar() { Icon = "Home", Title = "首页", NameSpace = "IndexView" });
            MenuBars.Add(new MenuBar() { Icon = "NotebookOutline", Title = "待办事项", NameSpace = "ToDoView" });
            MenuBars.Add(new MenuBar() { Icon = "NotebookPlus", Title = "备忘录", NameSpace = "MemoView" });
            MenuBars.Add(new MenuBar() { Icon = "Cog", Title = "设置", NameSpace = "SettingsView" });
        }*/
        public ObservableCollection<TaskBar> taskBars;
        public ObservableCollection<TaskBar> TaskBars
        {
            get { return taskBars; }
            set
            {
                taskBars = value; RaisePropertyChanged();
            }
        }


        /*  public ObservableCollection<ToDoDto> toDoDtos;
          public ObservableCollection<ToDoDto> ToDoDtos
          {
              get { return toDoDtos; }
              set
              {
                  toDoDtos = value; RaisePropertyChanged();
              }
          }


          public ObservableCollection<MemoDto> memoDtos;
          public ObservableCollection<MemoDto> MemoDtos
          {
              get { return memoDtos; }
              set
              {
                  memoDtos = value; RaisePropertyChanged();
              }
          }*/
      //  public ObservableCollection<ButtonList> ButtonItems { get; set; } = new ObservableCollection<ButtonList>();
        private void CreateTaskBars()
        {
            TaskBars = new ObservableCollection<TaskBar>();
            TaskBars.Add(new TaskBar() { Icon = "ClockFast", Title = "产品计数 :", Content = "9", Color = "#FF0CA0FF", Target = "ToDoView" });
            TaskBars.Add(new TaskBar() { Icon = "ClockCheckOutline", Title = "合格数 :", Content = "9", Color = "#FF1ECA3A", Target = "ToDoView" });
            TaskBars.Add(new TaskBar() { Icon = "AlertBox", Title = "NG数 :", Content = "9", Color = "#FF02C6DC", Target = "" });
            TaskBars.Add(new TaskBar() { Icon = "ChartLine", Title = "不合格率 :", Content = "9", Color = "#FFFFA000", Target = "MemoView" });
            TaskBars.Add(new TaskBar() { Icon = "ArrangeSendBackward", Title = "绝缘不良品 :", Content = "9", Color = "#FFCFCFCF", Target = "MemoView" });
            TaskBars.Add(new TaskBar() { Icon = "FlipToFront", Title = "桥路不良 :", Content = "9", Color = "#FFF5E6CE", Target = "MemoView" });
            TaskBars.Add(new TaskBar() { Icon = "FlipToBack", Title = "热瞬态不良 :", Content = "9", Color = "#87CEEB", Target = "MemoView" });
            TaskBars.Add(new TaskBar() { Icon = "FlipVertical", Title = "CCD不良 :", Content = "9", Color = "#FFB1EDE9", Target = "MemoView" });
        }

      /*  private void CreateTestData()
        {
            ToDoDtos = new ObservableCollection<ToDoDto>();
            MemoDtos = new ObservableCollection<MemoDto>();

            for (int i = 0; i < 10; i++)
            {
                ToDoDtos.Add(new ToDoDto() { Title = "待办" + i, Content = "正在处理中。。。。" });
                MemoDtos.Add(new MemoDto() { Title = "备忘" + i, Content = "我的密码。。。。" });
            }
        }
*/

    }
}
