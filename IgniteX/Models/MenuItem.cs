using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IgniteX.Models
{
    public class MenuItem
    {
        public string IconKind { get; set; }
        public string Title { get; set; }
        public ObservableCollection<NavigationItem> SubName { get; set; }
    }
    public class NavigationItem
    {
        public string SubTitle { get; set; }
    }
      //  public string CommandName { get; set; }
    
}
