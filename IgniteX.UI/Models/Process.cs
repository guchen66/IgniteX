using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace IgniteX.UI.Models
{
    public class Process
    {
        public int Id { get; set; }
        public string StationName { get; set; }
        public string IsComm { get; set; }
        public Color ProcessColor { get; set; }
        public Brush ProcessBrush { get; set; }
    }
}
