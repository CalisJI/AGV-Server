using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing.Drawing2D;
using System.Drawing;
using System.Data;
namespace Call_AGV
{
    public class Mapping
    {
        public List<Point> Position { get; set; }
        public List<Rectangle> Rectangles { get; set; }
        public List<Rectangle> Rectangles_2_Fill { get; set; }
        public List<string> Toa_Do { get; set; }
        public List<Point> Route { get; set; }
       

    }
}
