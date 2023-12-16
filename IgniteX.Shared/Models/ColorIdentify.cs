using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace IgniteX.Shared.Models
{
    public class ColorIdentify
    {
        public Dictionary<string,Color> ColorIdentifyDict = new Dictionary<string,Color>();

        public string IgniteName { get; set; }    
    }
}
