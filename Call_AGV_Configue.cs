using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Call_AGV
{
    public class Call_AGV_Configue
    {
        public string Sel_travel { get; set; }
        public List<string> File_data { get; set; }
        public string Selected_Station { get; set; }
        public string Path_Communicate { get; set; }

    }
}
