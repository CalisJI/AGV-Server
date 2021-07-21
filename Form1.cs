using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using MySql.Data.MySqlClient;
using MySql.Data;

namespace Call_AGV
{
    public partial class Form1 : Form
    {
        DataTable DataTable = new DataTable { TableName="Map_Data"};
        DataTable Route = new DataTable { TableName = "Point" };
        Mapping Mapping = new Mapping();
        Call_AGV_Configue Call_AGV_Configue = new Call_AGV_Configue();
        FileSystemWatcher FileSystemWatcher = new FileSystemWatcher();
        FileSystemWatcher FileSystemWatcher1 = new FileSystemWatcher();
        Timer Timer = new Timer();
        MySQL_dosomething mySQL_Dosomething = new MySQL_dosomething();
        BackgroundWorker Download_DATA_BGR = new BackgroundWorker();
        Community Community = new Community();
        public Form1()
        {
            InitializeComponent();
            Timer.Interval = 500;
            Timer.Tick += Timer_Tick;
            Timer.Enabled = false;
            DataTable.Columns.Add("ID", typeof(string));
            DataTable.Columns.Add("X", typeof(string));
            DataTable.Columns.Add("Y", typeof(string));
            DataTable.Columns.Add("RUN", typeof(string));
            DataTable.Columns.Add("LR", typeof(string));
            DataTable.Columns.Add("SPEED", typeof(string));
            DataTable.Columns.Add("DIR", typeof(string));
            DataTable.Columns.Add("LIFT", typeof(string));
            Download_DATA_BGR.DoWork += Download_DATA_BGR_DoWork;
            Download_DATA_BGR.RunWorkerCompleted += Download_DATA_BGR_RunWorkerCompleted;
            Download_DATA_BGR.WorkerSupportsCancellation = true;
            Route.Columns.Add("Start", typeof(Point));
            Route.Columns.Add("T1", typeof(Point));
            Route.Columns.Add("T2", typeof(Point));
            Route.Columns.Add("T3", typeof(Point));
            Route.Columns.Add("T4", typeof(Point));


        }

        


        #region Form
        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                Call_AGV_Configue = ConfigXML.GetSystem_Config();
                Mapping = ConfigXML.GetMapping(Call_AGV_Configue.Selected_Station);
                Community = ConfigXML.Get_community(Call_AGV_Configue.Selected_Station);
                File_data = Call_AGV_Configue.File_data;             
                Fill_circle = Mapping.Rectangles_2_Fill;
                DataTable = Community.DataTable;
                Route = Mapping.Route;             
                MethodInvoker inv = delegate
                {
                    textBox1.Text = Call_AGV_Configue.Path_Communicate;
                }; this.Invoke(inv);
                TB_Save.Enabled = false;
                TB_Save.Visible = false;
                foreach (var item in File_data)
                {
                    comboBox1.Items.Add(item);
                }
                if (Call_AGV_Configue.Selected_Station != "")
                {
                    foreach (var item in comboBox1.Items)
                    {
                        if (item.ToString() == Call_AGV_Configue.Selected_Station)
                        {
                            comboBox1.SelectedItem = item;
                            break;
                        }
                    }
                }

                panel1.Refresh();
                if (!File.Exists(Call_AGV_Configue.Path_Communicate + @"\" + ConfigXML.Read_txt))
                {
                    File.Create(Call_AGV_Configue.Path_Communicate + @"\" + ConfigXML.Read_txt).Close();
                }
                else { }
                if (!File.Exists(Call_AGV_Configue.Path_Communicate + @"\" + ConfigXML.Write_txt))
                {
                    File.Create(Call_AGV_Configue.Path_Communicate + @"\" + ConfigXML.Write_txt).Close();
                }
                else { }
                if (!File.Exists(Call_AGV_Configue.Path_Communicate + @"\" + ConfigXML.GPS))
                {
                    File.Create(Call_AGV_Configue.Path_Communicate + @"\" + ConfigXML.GPS).Close();
                }
                else { }
                FileSystemWatcher = new FileSystemWatcher(Path.GetDirectoryName(Call_AGV_Configue.Path_Communicate + @"\" + ConfigXML.Write_txt));
                FileSystemWatcher.NotifyFilter = NotifyFilters.LastWrite;
                FileSystemWatcher.Filter = "*.txt";
                FileSystemWatcher.Changed += FileSystemWatcher_Changed;
                FileSystemWatcher.EnableRaisingEvents = true;
                FileSystemWatcher1 = new FileSystemWatcher(Path.GetDirectoryName(Call_AGV_Configue.Path_Communicate + @"\" + ConfigXML.GPS));
                FileSystemWatcher1.NotifyFilter = NotifyFilters.LastWrite;
                FileSystemWatcher1.Filter = "*.txt";
                FileSystemWatcher1.Changed += FileSystemWatcher1_Changed;
                FileSystemWatcher1.EnableRaisingEvents = true;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }


        }

       

        int z = 0;
        int t = 0;
        int dem = 0;
        string GPS = string.Empty;
        private void FileSystemWatcher1_Changed(object sender, FileSystemEventArgs e)
        {
            if (e.FullPath == Call_AGV_Configue.Path_Communicate + @"\" + ConfigXML.GPS)
            {
                t++;
                if (t == 2)
                {
                   
                    int a = 0;
                    using (StreamReader sr = new StreamReader(Call_AGV_Configue.Path_Communicate + @"\" + ConfigXML.Write_txt))
                    {
                        while (sr.ReadLine() != null)
                        {

                            a++;
                        }
                    }
                    using (StreamReader sr = new StreamReader(Call_AGV_Configue.Path_Communicate + @"\" + ConfigXML.Write_txt))
                    {
                        for (int b = 0; b < a; b++)
                        {
                            MethodInvoker inv = delegate
                            {
                                GPS = sr.ReadLine();
                            }; this.Invoke(inv);
                        }
                        panel1.Refresh();
                    }
                    t = 0;
                }

            }
        }
        private void FileSystemWatcher_Changed(object sender, FileSystemEventArgs e)
        {
            if (e.FullPath == Call_AGV_Configue.Path_Communicate + @"\" + ConfigXML.Write_txt)
            {
                z++;

                if (z == 2)
                {
                    dem++;
                    int a = 0;
                    using (StreamReader sr = new StreamReader(Call_AGV_Configue.Path_Communicate + @"\" + ConfigXML.Write_txt))
                    {
                        while (sr.ReadLine() != null)
                        {

                            a++;
                        }
                    }
                    using (StreamReader sr = new StreamReader(Call_AGV_Configue.Path_Communicate + @"\" + ConfigXML.Write_txt))
                    {
                        for (int b = 0; b < a; b++)
                        {
                            MethodInvoker inv = delegate
                            {
                                textBox2.AppendText(sr.ReadLine() + "(" + dem.ToString() + ")" + Environment.NewLine);
                            }; this.Invoke(inv);
                        }
                    }
                    z = 0;
                }

            }
        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {

            ConfigXML.Update_Mapping(Mapping, comboBox1.Text);
            ConfigXML.Update_Mapping(Community, comboBox1.Text);
            ConfigXML.UpdateFullSystem_Config(Call_AGV_Configue);
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

            Call_AGV_Configue.File_data = File_data;
            Call_AGV_Configue.Selected_Station = comboBox1.Text;
            ConfigXML.UpdateFullSystem_Config(Call_AGV_Configue);
            Call_AGV_Configue = ConfigXML.GetSystem_Config();
            Mapping = ConfigXML.GetMapping(Call_AGV_Configue.Selected_Station);
            Community = ConfigXML.Get_community(Call_AGV_Configue.Selected_Station);
            File_data = Call_AGV_Configue.File_data;
            DataTable = Community.DataTable;
            Fill_circle = Mapping.Rectangles_2_Fill;
            Route = Mapping.Route;
            panel1.Refresh();
        }
        #endregion
        #region Draw point
        List<Rectangle> Board = new List<Rectangle>();
        int size_rec = 8;
        void Draw_Point_Board(PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            if (mode == 1) 
            {
                Board.Clear();
                for (int i = 0; i < panel1.Width; i += 30)
                {
                    for (int j = 0; j < panel1.Height; j += 30)
                    {
                        e.Graphics.DrawEllipse(new Pen(Color.Black, 1), new Rectangle(new Point(i + size_rec / 2, j + size_rec / 2), new Size(size_rec, size_rec)));
                        Board.Add(new Rectangle(new Point(i + size_rec / 2, j + size_rec / 2), new Size(size_rec, size_rec)));
                    }
                }
            }         
            if (Fill_circle != null) 
            {
                foreach (var item in Fill_circle)
                {
                    e.Graphics.FillEllipse(Brushes.Orange, item);
                }
            }
        }
        void Draw_Route(PaintEventArgs e) 
        {
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            if ( Route.Rows.Count > 0)
            {
                for (int j = 0; j < Route.Rows.Count; j++)
                {
                    Pen pen = new Pen(Brushes.Black, 1);
                    pen.EndCap = System.Drawing.Drawing2D.LineCap.ArrowAnchor;
                    for (int q = 1; q < Route.Columns.Count; q++)
                    {
                        object n = Route.Rows[j][q];
                        object a = Route.Rows[j][0];
                        if (n == DBNull.Value|| a == DBNull.Value) 
                        {
                            continue;
                        }
                        
                        e.Graphics.DrawLine(pen, (Point)Route.Rows[j][0], (Point)Route.Rows[j][q]);
                        
                       
                    }
                    
                }
                for (int w = 0; w < Route.Rows.Count; w++)
                {
                    object n1 = Route.Rows[w][0];
                    object n2 = Route.Rows[w][1];
                    if (n1!=DBNull.Value && n2==DBNull.Value) 
                    {                       
                        Font font = new Font("Times New Roman", 10);
                        Point pointF = (Point)Route.Rows[w][0];                   
                        e.Graphics.DrawString("(*)", font, Brushes.Red, new PointF(pointF.X, pointF.Y - size_rec));
                    }
                }
            }
           
        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            blink++;
            AGV_location(GPS);
        }
        int blink = 0;
        Point temp_Poi = new Point();
        private static double calutator_angle(Point point1, Point point2, Point point3)
        {
            double ang = 0;
            ang = (Math.Atan2(point3.Y - point1.Y, point3.X - point1.X) - Math.Atan2(point2.Y - point1.Y, point2.X - point1.X)) * (double)(180 / Math.PI);         
            return ang;
        }
        List<Point> temp = new List<Point>(2);
        string ID_tam = string.Empty;
        Point bien_khac = new Point();
        void AGV_location(string ID) 
        {
            try
            {
                if (!Timer.Enabled)
                {
                    Timer.Enabled = true;
                    Timer.Start();
                }
                if (ID_tam != ID)
                {
                    temp_Poi = get_locaion(ID_tam);
                    ID_tam = ID;
                }
               
                Graphics graphics = panel1.CreateGraphics();
                graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                Rectangle rectangle = new Rectangle(get_locaion(ID), new Size(size_rec * 2, size_rec * 2));
                string abc = DataTable.Rows[Get_index][5].ToString();
                int f = int.Parse(abc);
                switch (f)
                {
                    case 0:
                        if (blink % 2 == 0)
                        {
                            graphics.FillEllipse(Brushes.Lime, rectangle);
                            temp_Poi = get_locaion(ID);
                        }
                        else
                        {
                            graphics.FillEllipse(Brushes.White, rectangle);
                            graphics.DrawEllipse(new Pen(Color.Black, 1), new Rectangle(get_locaion(ID).X + size_rec/2, get_locaion(ID).Y + size_rec/2, size_rec, size_rec));
                            graphics.FillEllipse(Brushes.Orange, new Rectangle(get_locaion(ID).X + size_rec/2, get_locaion(ID).Y + size_rec/2, size_rec, size_rec));
                        }
                        break;
                    case > 0:
                       
                        if (blink % 2 == 0)
                        {
                            
                            if (int.Parse(DataTable.Rows[Get_index][6].ToString()) == 1) //kiểm tra hướng tiến hay lùi
                            {
                                Point point = new Point(get_locaion(ID).X + size_rec, get_locaion(ID).Y + size_rec);
                                for (int i = 0; i < Route.Rows.Count; i++)
                                {
                                    
                                    if ((Point)Route.Rows[i][0] == point) 
                                    {
                                        int c = 0;
                                        for (int g = 1; g < Route.Columns.Count; g++)
                                        {
                                            if (Route.Rows[i][g] != DBNull.Value) 
                                            {
                                                c++;
                                            }
                                        }
                                        if (c == 1) 
                                        {
                                            //TH1 R->L
                                            if (point.X > temp_Poi.X )
                                            {                                               
                                                if (Route.Rows[i][1] != DBNull.Value)
                                                {
                                                    Point point1 = (Point)Route.Rows[i][1];
                                                   
                                                        graphics.DrawLine(new Pen(Color.Red, 2), point, point1);
                                                       
                                                        goto done;
                                                    
                                                }
                                            }
                                            //TH2 U->D
                                            else if (point.Y < temp_Poi.Y )
                                            {
                                                if (Route.Rows[i][1] != DBNull.Value)
                                                {
                                                    Point point1 = (Point)Route.Rows[i][1];
                                                   
                                                        graphics.DrawLine(new Pen(Color.Red, 2), point, point1);
                                                       
                                                        goto done;
                                                   
                                                }
                                            }
                                            //TH3 L->R
                                            else if (point.X < temp_Poi.X )
                                            {
                                                if (Route.Rows[i][1] != DBNull.Value)
                                                {
                                                    Point point1 = (Point)Route.Rows[i][1];                                                   
                                                        graphics.DrawLine(new Pen(Color.Red, 2), point, point1);                                                    
                                                        goto done;
                                                    
                                                }
                                            }
                                            //TH4 D->U
                                            else if (point.Y > temp_Poi.Y )
                                            {
                                                if (Route.Rows[i][1] != DBNull.Value)
                                                {
                                                    Point point1 = (Point)Route.Rows[i][1];
                                                    graphics.DrawLine(new Pen(Color.Red, 2), point, point1);
                                                    goto done;

                                                }
                                            }
                                        }
                                        else if (c==2) 
                                        {
                                            //TH1 R=>L
                                            if (point.X > temp_Poi.X && Math.Abs(point.Y - temp_Poi.Y) < 60)
                                            {
                                                if (int.Parse(DataTable.Rows[Get_index][4].ToString()) == 1||int.Parse(DataTable.Rows[Get_index][4].ToString()) == 0)
                                                {
                                                    Point point1 = (Point)Route.Rows[i][1];
                                                    Point point2 = (Point)Route.Rows[i][2];
                                                    if (point1.Y > point2.Y) 
                                                    {
                                                        graphics.DrawLine(new Pen(Color.Red, 2), point, point1);
                                                    }
                                                    else 
                                                    {
                                                        graphics.DrawLine(new Pen(Color.Red, 2), point, point2);
                                                    }
                                                }
                                                else if (int.Parse(DataTable.Rows[Get_index][4].ToString()) == 2)
                                                {
                                                    Point point1 = (Point)Route.Rows[i][1];
                                                    Point point2 = (Point)Route.Rows[i][2];
                                                    if (point1.Y > point2.Y)
                                                    {
                                                        graphics.DrawLine(new Pen(Color.Red, 2), point, point2);
                                                    }
                                                    else
                                                    {
                                                        graphics.DrawLine(new Pen(Color.Red, 2), point, point1);
                                                    }
                                                }
                                               
                                            }
                                            //TH2 U->D
                                            else if (point.Y < temp_Poi.Y && Math.Abs(point.X - temp_Poi.X) < 60)
                                            {
                                                if (int.Parse(DataTable.Rows[Get_index][4].ToString()) == 1 || int.Parse(DataTable.Rows[Get_index][4].ToString()) == 0)
                                                {
                                                    Point point1 = (Point)Route.Rows[i][1];
                                                    Point point2 = (Point)Route.Rows[i][2];
                                                    if (point1.X < point2.X)
                                                    {
                                                        graphics.DrawLine(new Pen(Color.Red, 2), point, point1);
                                                    }
                                                    else
                                                    {
                                                        graphics.DrawLine(new Pen(Color.Red, 2), point, point2);
                                                    }
                                                }
                                                else if (int.Parse(DataTable.Rows[Get_index][4].ToString()) == 2)
                                                {
                                                    Point point1 = (Point)Route.Rows[i][1];
                                                    Point point2 = (Point)Route.Rows[i][2];
                                                    if (point1.X < point2.X)
                                                    {
                                                        graphics.DrawLine(new Pen(Color.Red, 2), point, point2);
                                                    }
                                                    else
                                                    {
                                                        graphics.DrawLine(new Pen(Color.Red, 2), point, point1);
                                                    }
                                                }

                                            }
                                            //TH3 L->R
                                            else if (point.X < temp_Poi.X && Math.Abs(point.Y - temp_Poi.Y) < 60)
                                            {
                                                if (int.Parse(DataTable.Rows[Get_index][4].ToString()) == 1 || int.Parse(DataTable.Rows[Get_index][4].ToString()) == 0)
                                                {
                                                    Point point1 = (Point)Route.Rows[i][1];
                                                    Point point2 = (Point)Route.Rows[i][2];
                                                    if (point1.Y < point2.Y)
                                                    {
                                                        graphics.DrawLine(new Pen(Color.Red, 2), point, point1);
                                                    }
                                                    else
                                                    {
                                                        graphics.DrawLine(new Pen(Color.Red, 2), point, point2);
                                                    }
                                                }
                                                else if (int.Parse(DataTable.Rows[Get_index][4].ToString()) == 2)
                                                {
                                                    Point point1 = (Point)Route.Rows[i][1];
                                                    Point point2 = (Point)Route.Rows[i][2];
                                                    if (point1.Y < point2.Y)
                                                    {
                                                        graphics.DrawLine(new Pen(Color.Red, 2), point, point2);
                                                    }
                                                    else
                                                    {
                                                        graphics.DrawLine(new Pen(Color.Red, 2), point, point1);
                                                    }
                                                }

                                            }
                                            //TH4 D->U
                                            else if (point.Y > temp_Poi.Y && Math.Abs(point.X - temp_Poi.X) < 60)
                                            {
                                                if (int.Parse(DataTable.Rows[Get_index][4].ToString()) == 1 || int.Parse(DataTable.Rows[Get_index][4].ToString()) == 0)
                                                {
                                                    Point point1 = (Point)Route.Rows[i][1];
                                                    Point point2 = (Point)Route.Rows[i][2];
                                                    if (point1.X > point2.X)
                                                    {
                                                        graphics.DrawLine(new Pen(Color.Red, 2), point, point1);
                                                    }
                                                    else
                                                    {
                                                        graphics.DrawLine(new Pen(Color.Red, 2), point, point2);
                                                    }
                                                }
                                                else if (int.Parse(DataTable.Rows[Get_index][4].ToString()) == 2)
                                                {
                                                    Point point1 = (Point)Route.Rows[i][1];
                                                    Point point2 = (Point)Route.Rows[i][2];
                                                    if (point1.X > point2.X)
                                                    {
                                                        graphics.DrawLine(new Pen(Color.Red, 2), point, point2);
                                                    }
                                                    else
                                                    {
                                                        graphics.DrawLine(new Pen(Color.Red, 2), point, point1);
                                                    }
                                                }

                                            }
                                        }
                                        else if (c > 2) 
                                        {
                                            //TH1 R->L
                                            if (point.X > temp_Poi.X && Math.Abs(point.Y - temp_Poi.Y) < 60)
                                            {
                                                if (int.Parse(DataTable.Rows[Get_index][4].ToString()) == 1)
                                                {
                                                    
                                                   
                                                    if (c == 3) 
                                                    {
                                                        Point point1 = (Point)Route.Rows[i][1];
                                                        Point point2 = (Point)Route.Rows[i][2];
                                                        Point point3 = (Point)Route.Rows[i][3];
                                                        if (point1.X < point.X) 
                                                        {
                                                            if (point2.Y > point3.Y) 
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point2);
                                                            }
                                                            else 
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point3);
                                                            }
                                                        }
                                                        else if (point2.X < point.X)
                                                        {
                                                            if (point1.Y > point3.Y)
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point1);
                                                            }
                                                            else
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point3);
                                                            }
                                                        }
                                                        else if (point3.X < point.X)
                                                        {
                                                            if (point1.Y > point2.Y)
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point1);
                                                            }
                                                            else
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point2);
                                                            }
                                                        }
                                                        else 
                                                        {
                                                            if (point1.Y > point2.Y && point1.Y > point3.Y) 
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point1);
                                                            }
                                                            else if (point2.Y > point1.Y && point2.Y > point3.Y)
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point2);
                                                            }
                                                            else if (point3.Y > point1.Y && point3.Y > point2.Y)
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point3);
                                                            }
                                                        }
                                                    }
                                                    else if (c == 4)
                                                    {
                                                        Point point1 = (Point)Route.Rows[i][1];
                                                        Point point2 = (Point)Route.Rows[i][2];
                                                        Point point3 = (Point)Route.Rows[i][3];
                                                        Point point4 = (Point)Route.Rows[i][4];
                                                        if (point1.X < point.X)
                                                        {
                                                            if (point4.Y > point2.Y && point4.Y > point3.Y)
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point4);
                                                            }
                                                            else if (point2.Y > point4.Y && point2.Y > point3.Y)
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point2);
                                                            }
                                                            else if (point3.Y > point4.Y && point3.Y > point2.Y)
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point3);
                                                            }
                                                        }
                                                        else if (point2.X < point.X)
                                                        {
                                                            if (point1.Y > point4.Y && point1.Y > point3.Y)
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point1);
                                                            }
                                                            else if (point4.Y > point1.Y && point4.Y > point3.Y)
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point4);
                                                            }
                                                            else if (point3.Y > point1.Y && point3.Y > point4.Y)
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point3);
                                                            }
                                                        }
                                                        else if (point3.X < point.X)
                                                        {
                                                            if (point1.Y > point2.Y && point1.Y > point4.Y)
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point1);
                                                            }
                                                            else if (point2.Y > point1.Y && point2.Y > point4.Y)
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point2);
                                                            }
                                                            else if (point4.Y > point1.Y && point4.Y > point2.Y)
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point4);
                                                            }
                                                        }
                                                        else if (point4.X < point.X)
                                                        {
                                                            if (point1.Y > point2.Y && point1.Y > point3.Y)
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point1);
                                                            }
                                                            else if (point2.Y > point1.Y && point2.Y > point3.Y)
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point2);
                                                            }
                                                            else if (point3.Y > point1.Y && point3.Y > point2.Y)
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point3);
                                                            }
                                                        }
                                                       
                                                    }
                                                   
                                                }
                                                else if (int.Parse(DataTable.Rows[Get_index][4].ToString()) == 2)
                                                {
                                                    if (c == 3)
                                                    {
                                                        Point point1 = (Point)Route.Rows[i][1];
                                                        Point point2 = (Point)Route.Rows[i][2];
                                                        Point point3 = (Point)Route.Rows[i][3];
                                                        if (point1.X < point.X)
                                                        {
                                                            if (point2.Y < point3.Y)
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point2);
                                                            }
                                                            else
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point3);
                                                            }
                                                        }
                                                        else if (point2.X < point.X)
                                                        {
                                                            if (point1.Y < point3.Y)
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point1);
                                                            }
                                                            else
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point3);
                                                            }
                                                        }
                                                        else if (point3.X < point.X)
                                                        {
                                                            if (point1.Y < point2.Y)
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point1);
                                                            }
                                                            else
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point2);
                                                            }
                                                        }
                                                        else
                                                        {
                                                            if (point1.Y < point2.Y && point1.Y< point3.Y)
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point1);
                                                            }
                                                            else if (point2.Y < point1.Y && point2.Y < point3.Y)
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point2);
                                                            }
                                                            else if (point3.Y < point1.Y && point3.Y < point2.Y)
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point3);
                                                            }
                                                        }
                                                    }
                                                    else if (c == 4)
                                                    {
                                                        Point point1 = (Point)Route.Rows[i][1];
                                                        Point point2 = (Point)Route.Rows[i][2];
                                                        Point point3 = (Point)Route.Rows[i][3];
                                                        Point point4 = (Point)Route.Rows[i][4];
                                                        if (point1.X < point.X)
                                                        {
                                                            if (point4.Y < point2.Y && point4.Y < point3.Y)
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point4);
                                                            }
                                                            else if (point2.Y < point4.Y && point2.Y < point3.Y)
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point2);
                                                            }
                                                            else if (point3.Y < point4.Y && point3.Y < point2.Y)
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point3);
                                                            }
                                                        }
                                                        else if (point2.X < point.X)
                                                        {
                                                            if (point1.Y < point4.Y && point1.Y < point3.Y)
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point1);
                                                            }
                                                            else if (point4.Y < point1.Y && point4.Y < point3.Y)
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point4);
                                                            }
                                                            else if (point3.Y < point1.Y && point3.Y < point4.Y)
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point3);
                                                            }
                                                        }
                                                        else if (point3.X < point.X)
                                                        {
                                                            if (point1.Y < point2.Y && point1.Y < point4.Y)
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point1);
                                                            }
                                                            else if (point2.Y < point1.Y && point2.Y < point4.Y)
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point2);
                                                            }
                                                            else if (point4.Y < point1.Y && point4.Y < point2.Y)
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point4);
                                                            }
                                                        }
                                                        else if (point4.X < point.X)
                                                        {
                                                            if (point1.Y < point2.Y && point1.Y < point3.Y)
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point1);
                                                            }
                                                            else if (point2.Y < point1.Y && point2.Y < point3.Y)
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point2);
                                                            }
                                                            else if (point3.Y < point1.Y && point3.Y < point2.Y)
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point3);
                                                            }
                                                        }

                                                    }
                                                }
                                                else if (int.Parse(DataTable.Rows[Get_index][4].ToString()) == 0)
                                                {
                                                    if (c == 3)
                                                    {
                                                        Point point1 = (Point)Route.Rows[i][1];
                                                        Point point2 = (Point)Route.Rows[i][2];
                                                        Point point3 = (Point)Route.Rows[i][3];
                                                        if (point1.X < point.X)
                                                        {
                                                            if (point2.Y > point3.Y)
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point2);
                                                            }
                                                            else
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point3);
                                                            }
                                                        }
                                                        else if (point2.X < point.X)
                                                        {
                                                            if (point1.Y > point3.Y)
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point1);
                                                            }
                                                            else
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point3);
                                                            }
                                                        }
                                                        else if (point3.X < point.X)
                                                        {
                                                            if (point1.Y > point2.Y)
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point1);
                                                            }
                                                            else
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point2);
                                                            }
                                                        }
                                                        else
                                                        {
                                                            if (point1.Y > point2.Y && point1.Y > point3.Y && point2.Y > point3.Y)
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point2);
                                                            }
                                                            else if (point1.Y > point2.Y && point1.Y > point3.Y && point2.Y < point3.Y)
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point3);
                                                            }
                                                            else if (point2.Y > point1.Y && point2.Y > point3.Y && point1.Y < point3.Y)
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point3);
                                                            }
                                                            else if (point2.Y > point1.Y && point2.Y > point3.Y && point1.Y > point3.Y)
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point1);
                                                            }
                                                            else if (point3.Y > point1.Y && point3.Y > point2.Y && point1.Y > point2.Y)
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point1);
                                                            }
                                                            else if (point3.Y > point1.Y && point3.Y > point2.Y && point1.Y < point2.Y)
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point2);
                                                            }
                                                        }
                                                    }
                                                    else if (c == 4)
                                                    {
                                                        Point point1 = (Point)Route.Rows[i][1];
                                                        Point point2 = (Point)Route.Rows[i][2];
                                                        Point point3 = (Point)Route.Rows[i][3];
                                                        Point point4 = (Point)Route.Rows[i][4];
                                                        if (point1.X < point.X)
                                                        {
                                                            if (point4.Y > point2.Y && point4.Y > point3.Y && point2.Y > point3.Y)
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point2);
                                                            }
                                                            else if (point4.Y > point2.Y && point4.Y > point3.Y && point2.Y < point3.Y)
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point3);
                                                            }
                                                            else if (point2.Y > point4.Y && point2.Y > point3.Y && point4.Y < point3.Y)
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point3);
                                                            }
                                                            else if (point2.Y > point4.Y && point2.Y > point3.Y && point4.Y > point3.Y)
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point4);
                                                            }
                                                            else if (point3.Y > point4.Y && point3.Y > point2.Y && point4.Y > point2.Y)
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point4);
                                                            }
                                                            else if (point3.Y > point4.Y && point3.Y > point2.Y && point4.Y < point2.Y)
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point2);
                                                            }
                                                        }
                                                        else if (point2.X < point.X)
                                                        {
                                                            if (point1.Y > point4.Y && point1.Y > point3.Y && point4.Y > point3.Y)
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point4);
                                                            }
                                                            else if (point1.Y > point4.Y && point1.Y > point3.Y && point4.Y < point3.Y)
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point3);
                                                            }
                                                            else if (point4.Y > point1.Y && point4.Y > point3.Y && point1.Y < point3.Y)
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point3);
                                                            }
                                                            else if (point4.Y > point1.Y && point4.Y > point3.Y && point1.Y > point3.Y)
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point1);
                                                            }
                                                            else if (point3.Y > point1.Y && point3.Y > point4.Y && point1.Y > point4.Y)
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point1);
                                                            }
                                                            else if (point3.Y > point1.Y && point3.Y > point4.Y && point1.Y < point4.Y)
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point4);
                                                            }
                                                        }
                                                        else if (point3.X < point.X)
                                                        {
                                                            if (point1.Y > point2.Y && point1.Y > point4.Y && point2.Y > point4.Y)
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point2);
                                                            }
                                                            else if (point1.Y > point2.Y && point1.Y > point4.Y && point2.Y < point4.Y)
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point4);
                                                            }
                                                            else if (point2.Y > point1.Y && point2.Y > point4.Y && point1.Y < point4.Y)
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point4);
                                                            }
                                                            else if (point2.Y > point1.Y && point2.Y > point4.Y && point1.Y > point4.Y)
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point1);
                                                            }
                                                            else if (point4.Y > point1.Y && point4.Y > point2.Y && point1.Y > point2.Y)
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point1);
                                                            }
                                                            else if (point4.Y > point1.Y && point4.Y > point2.Y && point1.Y < point2.Y)
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point2);
                                                            }
                                                        }
                                                        else if (point4.X < point.X)
                                                        {
                                                            if (point1.Y > point2.Y && point1.Y > point3.Y && point2.Y > point3.Y)
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point2);
                                                            }
                                                            else if (point1.Y > point2.Y && point1.Y > point3.Y && point2.Y < point3.Y)
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point3);
                                                            }
                                                            else if (point2.Y > point1.Y && point2.Y > point3.Y && point1.Y < point3.Y)
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point3);
                                                            }
                                                            else if (point2.Y > point1.Y && point2.Y > point3.Y && point1.Y > point3.Y)
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point1);
                                                            }
                                                            else if (point3.Y > point1.Y && point3.Y > point2.Y && point1.Y > point2.Y)
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point1);
                                                            }
                                                            else if (point3.Y > point1.Y && point3.Y > point2.Y && point1.Y < point2.Y)
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point2);
                                                            }
                                                        }

                                                    }
                                                }
                                            }
                                            //TH2 U->D
                                            else if (point.Y < temp_Poi.Y && Math.Abs(point.X - temp_Poi.X) < 60)
                                            {
                                                if (int.Parse(DataTable.Rows[Get_index][4].ToString()) == 1)
                                                {


                                                    if (c == 3)
                                                    {
                                                        Point point1 = (Point)Route.Rows[i][1];
                                                        Point point2 = (Point)Route.Rows[i][2];
                                                        Point point3 = (Point)Route.Rows[i][3];
                                                        if (point1.Y < point.Y)
                                                        {
                                                            if (point2.X < point3.X)
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point2);
                                                            }
                                                            else
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point3);
                                                            }
                                                        }
                                                        else if (point2.Y < point.Y)
                                                        {
                                                            if (point1.X < point3.X)
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point1);
                                                            }
                                                            else
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point3);
                                                            }
                                                        }
                                                        else if (point3.Y < point.Y)
                                                        {
                                                            if (point1.X < point2.X)
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point1);
                                                            }
                                                            else
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point2);
                                                            }
                                                        }
                                                        else
                                                        {
                                                            if (point1.X < point2.X && point1.X < point3.X)
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point1);
                                                            }
                                                            else if (point2.X < point1.X && point2.X < point3.X)
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point2);
                                                            }
                                                            else if (point3.X < point1.Y && point3.X < point2.X)
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point3);
                                                            }
                                                        }
                                                    }
                                                    else if (c == 4)
                                                    {
                                                        Point point1 = (Point)Route.Rows[i][1];
                                                        Point point2 = (Point)Route.Rows[i][2];
                                                        Point point3 = (Point)Route.Rows[i][3];
                                                        Point point4 = (Point)Route.Rows[i][4];
                                                        if (point1.Y < point.Y)
                                                        {
                                                            if (point4.X < point2.X && point4.X < point3.X)
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point4);
                                                            }
                                                            else if (point2.X < point4.X && point2.X < point3.X)
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point2);
                                                            }
                                                            else if (point3.X < point4.X && point3.X < point2.X)
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point3);
                                                            }
                                                        }
                                                        else if (point2.Y < point.Y)
                                                        {
                                                            if (point1.X < point4.X && point1.X < point3.X)
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point1);
                                                            }
                                                            else if (point4.X < point1.X && point4.X < point3.X)
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point4);
                                                            }
                                                            else if (point3.X < point1.X && point3.X < point4.X)
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point3);
                                                            }
                                                        }
                                                        else if (point3.Y < point.Y)
                                                        {
                                                            if (point1.X < point2.X && point1.X < point4.X)
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point1);
                                                            }
                                                            else if (point2.X < point1.X && point2.X < point4.X)
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point2);
                                                            }
                                                            else if (point4.X < point1.X && point4.X < point2.X)
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point4);
                                                            }
                                                        }
                                                        else if (point4.Y < point.Y)
                                                        {
                                                            if (point1.X < point2.X && point1.X < point3.X)
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point1);
                                                            }
                                                            else if (point2.X < point1.X && point2.X < point3.X)
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point2);
                                                            }
                                                            else if (point3.X < point1.X && point3.X < point2.X)
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point3);
                                                            }
                                                        }

                                                    }

                                                }
                                                else if (int.Parse(DataTable.Rows[Get_index][4].ToString()) == 2)
                                                {
                                                    if (c == 3)
                                                    {
                                                        Point point1 = (Point)Route.Rows[i][1];
                                                        Point point2 = (Point)Route.Rows[i][2];
                                                        Point point3 = (Point)Route.Rows[i][3];
                                                        if (point1.Y < point.Y)
                                                        {
                                                            if (point2.X > point3.X)
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point2);
                                                            }
                                                            else
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point3);
                                                            }
                                                        }
                                                        else if (point2.Y < point.Y)
                                                        {
                                                            if (point1.X > point3.X)
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point1);
                                                            }
                                                            else
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point3);
                                                            }
                                                        }
                                                        else if (point3.Y < point.Y)
                                                        {
                                                            if (point1.X > point2.X)
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point1);
                                                            }
                                                            else
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point2);
                                                            }
                                                        }
                                                        else
                                                        {
                                                            if (point1.X > point2.X && point1.X > point3.X)
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point1);
                                                            }
                                                            else if (point2.X > point1.X && point2.X > point3.X)
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point2);
                                                            }
                                                            else if (point3.X > point1.X && point3.X > point2.X)
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point3);
                                                            }
                                                        }
                                                    }
                                                    else if (c == 4)
                                                    {
                                                        Point point1 = (Point)Route.Rows[i][1];
                                                        Point point2 = (Point)Route.Rows[i][2];
                                                        Point point3 = (Point)Route.Rows[i][3];
                                                        Point point4 = (Point)Route.Rows[i][4];
                                                        if (point1.Y < point.Y)
                                                        {
                                                            if (point4.X > point2.X && point4.X > point3.X)
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point4);
                                                            }
                                                            else if (point2.X > point4.X && point2.X > point3.X)
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point2);
                                                            }
                                                            else if (point3.X > point4.X && point3.X > point2.X)
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point3);
                                                            }
                                                        }
                                                        else if (point2.Y < point.Y)
                                                        {
                                                            if (point1.X > point4.X && point1.X > point3.X)
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point1);
                                                            }
                                                            else if (point4.X > point1.X && point4.X > point3.X)
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point4);
                                                            }
                                                            else if (point3.X > point1.X && point3.X > point4.X)
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point3);
                                                            }
                                                        }
                                                        else if (point3.Y < point.Y)
                                                        {
                                                            if (point1.X > point2.X && point1.X > point4.X)
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point1);
                                                            }
                                                            else if (point2.X > point1.X && point2.X > point4.X)
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point2);
                                                            }
                                                            else if (point4.X > point1.X && point4.X > point2.X)
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point4);
                                                            }
                                                        }
                                                        else if (point4.Y < point.Y)
                                                        {
                                                            if (point1.X > point2.X && point1.X > point3.X)
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point1);
                                                            }
                                                            else if (point2.X > point1.X && point2.X > point3.X)
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point2);
                                                            }
                                                            else if (point3.X > point1.X && point3.X > point2.X)
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point3);
                                                            }
                                                        }

                                                    }
                                                }
                                                else if (int.Parse(DataTable.Rows[Get_index][4].ToString()) == 0)
                                                {
                                                    if (c == 3)
                                                    {
                                                        Point point1 = (Point)Route.Rows[i][1];
                                                        Point point2 = (Point)Route.Rows[i][2];
                                                        Point point3 = (Point)Route.Rows[i][3];
                                                        if (point1.Y < point.Y)
                                                        {
                                                            if (point2.X < point3.X)
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point2);
                                                            }
                                                            else
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point3);
                                                            }
                                                        }
                                                        else if (point2.Y < point.Y)
                                                        {
                                                            if (point1.X < point3.X)
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point1);
                                                            }
                                                            else
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point3);
                                                            }
                                                        }
                                                        else if (point3.Y < point.Y)
                                                        {
                                                            if (point1.X < point2.X)
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point1);
                                                            }
                                                            else
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point2);
                                                            }
                                                        }
                                                        else
                                                        {
                                                            if (point1.X > point2.X && point1.X > point3.X && point2.X > point3.X)
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point2);
                                                            }
                                                            else if (point1.X > point2.X && point1.X > point3.X && point2.X < point3.X)
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point3);
                                                            }
                                                            else if (point2.X > point1.X && point2.X > point3.X && point1.X < point3.X)
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point3);
                                                            }
                                                            else if (point2.X > point1.X && point2.X > point3.X && point1.X > point3.X)
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point1);
                                                            }
                                                            else if (point3.X > point1.X && point3.X > point2.X && point1.X > point2.X)
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point1);
                                                            }
                                                            else if (point3.X > point1.X && point3.X > point2.X && point1.X < point2.X)
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point2);
                                                            }
                                                        }
                                                    }
                                                    else if (c == 4)
                                                    {
                                                        Point point1 = (Point)Route.Rows[i][1];
                                                        Point point2 = (Point)Route.Rows[i][2];
                                                        Point point3 = (Point)Route.Rows[i][3];
                                                        Point point4 = (Point)Route.Rows[i][4];
                                                        if (point1.Y < point.Y)
                                                        {
                                                            if (point4.X > point2.X && point4.X > point3.X && point2.X > point3.X)
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point2);
                                                            }
                                                            else if (point4.X > point2.X && point4.X > point3.X && point2.X < point3.X)
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point3);
                                                            }
                                                            else if (point2.X > point4.X && point2.X > point3.X && point4.X < point3.X)
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point3);
                                                            }
                                                            else if (point2.X > point4.X && point2.X > point3.X && point4.X > point3.X)
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point4);
                                                            }
                                                            else if (point3.X > point4.X && point3.X > point2.X && point4.X > point2.X)
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point4);
                                                            }
                                                            else if (point3.Y > point4.X && point3.X > point2.X && point4.X < point2.X)
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point2);
                                                            }
                                                        }
                                                        else if (point2.Y < point.Y)
                                                        {
                                                            if (point1.X > point4.X && point1.X > point3.X && point4.X > point3.X)
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point4);
                                                            }
                                                            else if (point1.X > point4.X && point1.X > point3.X && point4.X < point3.X)
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point3);
                                                            }
                                                            else if (point4.X > point1.X && point4.X > point3.X && point1.X < point3.X)
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point3);
                                                            }
                                                            else if (point4.X > point1.X && point4.X > point3.X && point1.X > point3.X)
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point1);
                                                            }
                                                            else if (point3.X > point1.X && point3.X > point4.X && point1.X > point4.X)
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point1);
                                                            }
                                                            else if (point3.X > point1.X && point3.X > point4.X && point1.X < point4.X)
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point4);
                                                            }
                                                        }
                                                        else if (point3.Y < point.Y)
                                                        {
                                                            if (point1.X > point2.X && point1.X > point4.X && point2.X > point4.X)
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point2);
                                                            }
                                                            else if (point1.X > point2.X && point1.X > point4.X && point2.X < point4.X)
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point4);
                                                            }
                                                            else if (point2.X > point1.X && point2.X > point4.X && point1.X < point4.X)
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point4);
                                                            }
                                                            else if (point2.X > point1.X && point2.X > point4.X && point1.X > point4.X)
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point1);
                                                            }
                                                            else if (point4.X > point1.X && point4.X > point2.X && point1.X > point2.X)
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point1);
                                                            }
                                                            else if (point4.X > point1.X && point4.X > point2.X && point1.X < point2.X)
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point2);
                                                            }
                                                        }
                                                        else if (point4.Y < point.Y)
                                                        {
                                                            if (point1.X > point2.X && point1.X > point3.X && point2.X > point3.X)
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point2);
                                                            }
                                                            else if (point1.X > point2.X && point1.X > point3.X && point2.X < point3.X)
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point3);
                                                            }
                                                            else if (point2.X > point1.X && point2.X > point3.X && point1.X < point3.X)
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point3);
                                                            }
                                                            else if (point2.X > point1.X && point2.X > point3.X && point1.X > point3.X)
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point1);
                                                            }
                                                            else if (point3.X > point1.X && point3.X > point2.X && point1.X > point2.X)
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point1);
                                                            }
                                                            else if (point3.Y > point1.X && point3.X > point2.X && point1.X < point2.X)
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point2);
                                                            }
                                                        }

                                                    }
                                                }
                                            }
                                            //TH3 L->R
                                            else if (point.X < temp_Poi.X && Math.Abs(point.Y - temp_Poi.Y) < 60)
                                            {
                                                if (int.Parse(DataTable.Rows[Get_index][4].ToString()) == 1)
                                                {


                                                    if (c == 3)
                                                    {
                                                        Point point1 = (Point)Route.Rows[i][1];
                                                        Point point2 = (Point)Route.Rows[i][2];
                                                        Point point3 = (Point)Route.Rows[i][3];
                                                        if (point1.X > point.X)
                                                        {
                                                            if (point2.Y < point3.Y)
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point2);
                                                            }
                                                            else
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point3);
                                                            }
                                                        }
                                                        else if (point2.X > point.X)
                                                        {
                                                            if (point1.Y < point3.Y)
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point1);
                                                            }
                                                            else
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point3);
                                                            }
                                                        }
                                                        else if (point3.X > point.X)
                                                        {
                                                            if (point1.Y < point2.Y)
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point1);
                                                            }
                                                            else
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point2);
                                                            }
                                                        }
                                                        else
                                                        {
                                                            if (point1.Y < point2.Y && point1.Y < point3.Y)
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point1);
                                                            }
                                                            else if (point2.Y < point1.Y && point2.Y < point3.Y)
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point2);
                                                            }
                                                            else if (point3.Y < point1.Y && point3.Y < point2.Y)
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point3);
                                                            }
                                                        }
                                                    }
                                                    else if (c == 4)
                                                    {
                                                        Point point1 = (Point)Route.Rows[i][1];
                                                        Point point2 = (Point)Route.Rows[i][2];
                                                        Point point3 = (Point)Route.Rows[i][3];
                                                        Point point4 = (Point)Route.Rows[i][4];
                                                        if (point1.X > point.X)
                                                        {
                                                            if (point4.Y < point2.Y && point4.Y < point3.Y)
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point4);
                                                            }
                                                            else if (point2.Y < point4.Y && point2.Y < point3.Y)
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point2);
                                                            }
                                                            else if (point3.Y < point4.Y && point3.Y < point2.Y)
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point3);
                                                            }
                                                        }
                                                        else if (point2.X < point.X)
                                                        {
                                                            if (point1.Y < point4.Y && point1.Y < point3.Y)
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point1);
                                                            }
                                                            else if (point4.Y < point1.Y && point4.Y < point3.Y)
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point4);
                                                            }
                                                            else if (point3.Y < point1.Y && point3.Y < point4.Y)
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point3);
                                                            }
                                                        }
                                                        else if (point3.X < point.X)
                                                        {
                                                            if (point1.Y < point2.Y && point1.Y < point4.Y)
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point1);
                                                            }
                                                            else if (point2.Y < point1.Y && point2.Y < point4.Y)
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point2);
                                                            }
                                                            else if (point4.Y < point1.Y && point4.Y < point2.Y)
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point4);
                                                            }
                                                        }
                                                        else if (point4.X < point.X)
                                                        {
                                                            if (point1.Y < point2.Y && point1.Y < point3.Y)
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point1);
                                                            }
                                                            else if (point2.Y < point1.Y && point2.Y < point3.Y)
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point2);
                                                            }
                                                            else if (point3.Y < point1.Y && point3.Y < point2.Y)
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point3);
                                                            }
                                                        }

                                                    }

                                                }
                                                else if (int.Parse(DataTable.Rows[Get_index][4].ToString()) == 2)
                                                {
                                                    if (c == 3)
                                                    {
                                                        Point point1 = (Point)Route.Rows[i][1];
                                                        Point point2 = (Point)Route.Rows[i][2];
                                                        Point point3 = (Point)Route.Rows[i][3];
                                                        if (point1.X > point.X)
                                                        {
                                                            if (point2.Y > point3.Y)
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point2);
                                                            }
                                                            else
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point3);
                                                            }
                                                        }
                                                        else if (point2.X > point.X)
                                                        {
                                                            if (point1.Y > point3.Y)
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point1);
                                                            }
                                                            else
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point3);
                                                            }
                                                        }
                                                        else if (point3.X > point.X)
                                                        {
                                                            if (point1.Y > point2.Y)
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point1);
                                                            }
                                                            else
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point2);
                                                            }
                                                        }
                                                        else
                                                        {
                                                            if (point1.Y > point2.Y && point1.Y > point3.Y)
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point1);
                                                            }
                                                            else if (point2.Y > point1.Y && point2.Y > point3.Y)
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point2);
                                                            }
                                                            else if (point3.Y > point1.Y && point3.Y > point2.Y)
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point3);
                                                            }
                                                        }
                                                    }
                                                    else if (c == 4)
                                                    {
                                                        Point point1 = (Point)Route.Rows[i][1];
                                                        Point point2 = (Point)Route.Rows[i][2];
                                                        Point point3 = (Point)Route.Rows[i][3];
                                                        Point point4 = (Point)Route.Rows[i][4];
                                                        if (point1.X > point.X)
                                                        {
                                                            if (point4.Y > point2.Y && point4.Y > point3.Y)
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point4);
                                                            }
                                                            else if (point2.Y > point4.Y && point2.Y > point3.Y)
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point2);
                                                            }
                                                            else if (point3.Y > point4.Y && point3.Y > point2.Y)
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point3);
                                                            }
                                                        }
                                                        else if (point2.X > point.X)
                                                        {
                                                            if (point1.Y > point4.Y && point1.Y > point3.Y)
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point1);
                                                            }
                                                            else if (point4.Y > point1.Y && point4.Y > point3.Y)
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point4);
                                                            }
                                                            else if (point3.Y > point1.Y && point3.Y > point4.Y)
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point3);
                                                            }
                                                        }
                                                        else if (point3.X > point.X)
                                                        {
                                                            if (point1.Y > point2.Y && point1.Y > point4.Y)
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point1);
                                                            }
                                                            else if (point2.Y > point1.Y && point2.Y > point4.Y)
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point2);
                                                            }
                                                            else if (point4.Y > point1.Y && point4.Y > point2.Y)
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point4);
                                                            }
                                                        }
                                                        else if (point4.X > point.X)
                                                        {
                                                            if (point1.Y > point2.Y && point1.Y > point3.Y)
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point1);
                                                            }
                                                            else if (point2.Y > point1.Y && point2.Y > point3.Y)
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point2);
                                                            }
                                                            else if (point3.Y > point1.Y && point3.Y > point2.Y)
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point3);
                                                            }
                                                        }

                                                    }
                                                }
                                                else if (int.Parse(DataTable.Rows[Get_index][4].ToString()) == 0)
                                                {
                                                    if (c == 3)
                                                    {
                                                        Point point1 = (Point)Route.Rows[i][1];
                                                        Point point2 = (Point)Route.Rows[i][2];
                                                        Point point3 = (Point)Route.Rows[i][3];
                                                        if (point1.X > point.X)
                                                        {
                                                            if (point2.Y < point3.Y)
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point2);
                                                            }
                                                            else
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point3);
                                                            }
                                                        }
                                                        else if (point2.X > point.X)
                                                        {
                                                            if (point1.Y < point3.Y)
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point1);
                                                            }
                                                            else
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point3);
                                                            }
                                                        }
                                                        else if (point3.X > point.X)
                                                        {
                                                            if (point1.Y < point2.Y)
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point1);
                                                            }
                                                            else
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point2);
                                                            }
                                                        }
                                                        else
                                                        {
                                                            if (point1.Y > point2.Y && point1.Y > point3.Y && point2.Y > point3.Y)
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point2);
                                                            }
                                                            else if (point1.Y > point2.Y && point1.Y > point3.Y && point2.Y < point3.Y)
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point3);
                                                            }
                                                            else if (point2.Y > point1.Y && point2.Y > point3.Y && point1.Y < point3.Y)
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point3);
                                                            }
                                                            else if (point2.Y > point1.Y && point2.Y > point3.Y && point1.Y > point3.Y)
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point1);
                                                            }
                                                            else if (point3.Y > point1.Y && point3.Y > point2.Y && point1.Y > point2.Y)
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point1);
                                                            }
                                                            else if (point3.Y > point1.Y && point3.Y > point2.Y && point1.Y < point2.Y)
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point2);
                                                            }
                                                        }
                                                    }
                                                    else if (c == 4)
                                                    {
                                                        Point point1 = (Point)Route.Rows[i][1];
                                                        Point point2 = (Point)Route.Rows[i][2];
                                                        Point point3 = (Point)Route.Rows[i][3];
                                                        Point point4 = (Point)Route.Rows[i][4];
                                                        if (point1.X > point.X)
                                                        {
                                                            if (point4.Y > point2.Y && point4.Y > point3.Y && point2.Y > point3.Y)
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point2);
                                                            }
                                                            else if (point4.Y > point2.Y && point4.Y > point3.Y && point2.Y < point3.Y)
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point3);
                                                            }
                                                            else if (point2.Y > point4.Y && point2.Y > point3.Y && point4.Y < point3.Y)
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point3);
                                                            }
                                                            else if (point2.Y > point4.Y && point2.Y > point3.Y && point4.Y > point3.Y)
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point4);
                                                            }
                                                            else if (point3.Y > point4.Y && point3.Y > point2.Y && point4.Y > point2.Y)
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point4);
                                                            }
                                                            else if (point3.Y > point4.Y && point3.Y > point2.Y && point4.Y < point2.Y)
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point2);
                                                            }
                                                        }
                                                        else if (point2.X > point.X)
                                                        {
                                                            if (point1.Y > point4.Y && point1.Y > point3.Y && point4.Y > point3.Y)
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point4);
                                                            }
                                                            else if (point1.Y > point4.Y && point1.Y > point3.Y && point4.Y < point3.Y)
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point3);
                                                            }
                                                            else if (point4.Y > point1.Y && point4.Y > point3.Y && point1.Y < point3.Y)
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point3);
                                                            }
                                                            else if (point4.Y > point1.Y && point4.Y > point3.Y && point1.Y > point3.Y)
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point1);
                                                            }
                                                            else if (point3.Y > point1.Y && point3.Y > point4.Y && point1.Y > point4.Y)
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point1);
                                                            }
                                                            else if (point3.Y > point1.Y && point3.Y > point4.Y && point1.Y < point4.Y)
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point4);
                                                            }
                                                        }
                                                        else if (point3.X > point.X)
                                                        {
                                                            if (point1.Y > point2.Y && point1.Y > point4.Y && point2.Y > point4.Y)
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point2);
                                                            }
                                                            else if (point1.Y > point2.Y && point1.Y > point4.Y && point2.Y < point4.Y)
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point4);
                                                            }
                                                            else if (point2.Y > point1.Y && point2.Y > point4.Y && point1.Y < point4.Y)
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point4);
                                                            }
                                                            else if (point2.Y > point1.Y && point2.Y > point4.Y && point1.Y > point4.Y)
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point1);
                                                            }
                                                            else if (point4.Y > point1.Y && point4.Y > point2.Y && point1.Y > point2.Y)
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point1);
                                                            }
                                                            else if (point4.Y > point1.Y && point4.Y > point2.Y && point1.Y < point2.Y)
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point2);
                                                            }
                                                        }
                                                        else if (point4.X > point.X)
                                                        {
                                                            if (point1.Y > point2.Y && point1.Y > point3.Y && point2.Y > point3.Y)
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point2);
                                                            }
                                                            else if (point1.Y > point2.Y && point1.Y > point3.Y && point2.Y < point3.Y)
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point3);
                                                            }
                                                            else if (point2.Y > point1.Y && point2.Y > point3.Y && point1.Y < point3.Y)
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point3);
                                                            }
                                                            else if (point2.Y > point1.Y && point2.Y > point3.Y && point1.Y > point3.Y)
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point1);
                                                            }
                                                            else if (point3.Y > point1.Y && point3.Y > point2.Y && point1.Y > point2.Y)
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point1);
                                                            }
                                                            else if (point3.Y > point1.Y && point3.Y > point2.Y && point1.Y < point2.Y)
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point2);
                                                            }
                                                        }

                                                    }
                                                }
                                            }
                                            //TH4 D->U
                                            else if (point.Y > temp_Poi.Y && Math.Abs(point.X - temp_Poi.X) < 60)
                                            {
                                                if (int.Parse(DataTable.Rows[Get_index][4].ToString()) == 1)
                                                {


                                                    if (c == 3)
                                                    {
                                                        Point point1 = (Point)Route.Rows[i][1];
                                                        Point point2 = (Point)Route.Rows[i][2];
                                                        Point point3 = (Point)Route.Rows[i][3];
                                                        if (point1.Y > point.Y)
                                                        {
                                                            if (point2.X > point3.X)
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point2);
                                                            }
                                                            else
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point3);
                                                            }
                                                        }
                                                        else if (point2.Y > point.Y)
                                                        {
                                                            if (point1.X > point3.X)
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point1);
                                                            }
                                                            else
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point3);
                                                            }
                                                        }
                                                        else if (point3.Y > point.Y)
                                                        {
                                                            if (point1.X > point2.X)
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point1);
                                                            }
                                                            else
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point2);
                                                            }
                                                        }
                                                        else
                                                        {
                                                            if (point1.X > point2.X && point1.X > point3.X)
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point1);
                                                            }
                                                            else if (point2.X > point1.X && point2.X > point3.X)
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point2);
                                                            }
                                                            else if (point3.X > point1.Y && point3.X > point2.X)
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point3);
                                                            }
                                                        }
                                                    }
                                                    else if (c == 4)
                                                    {
                                                        Point point1 = (Point)Route.Rows[i][1];
                                                        Point point2 = (Point)Route.Rows[i][2];
                                                        Point point3 = (Point)Route.Rows[i][3];
                                                        Point point4 = (Point)Route.Rows[i][4];
                                                        if (point1.Y > point.Y)
                                                        {
                                                            if (point4.X > point2.X && point4.X > point3.X)
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point4);
                                                            }
                                                            else if (point2.X > point4.X && point2.X > point3.X)
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point2);
                                                            }
                                                            else if (point3.X > point4.X && point3.X > point2.X)
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point3);
                                                            }
                                                        }
                                                        else if (point2.Y > point.Y)
                                                        {
                                                            if (point1.X > point4.X && point1.X > point3.X)
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point1);
                                                            }
                                                            else if (point4.X > point1.X && point4.X > point3.X)
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point4);
                                                            }
                                                            else if (point3.X > point1.X && point3.X > point4.X)
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point3);
                                                            }
                                                        }
                                                        else if (point3.Y > point.Y)
                                                        {
                                                            if (point1.X > point2.X && point1.X > point4.X)
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point1);
                                                            }
                                                            else if (point2.X > point1.X && point2.X > point4.X)
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point2);
                                                            }
                                                            else if (point4.X > point1.X && point4.X > point2.X)
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point4);
                                                            }
                                                        }
                                                        else if (point4.Y > point.Y)
                                                        {
                                                            if (point1.X > point2.X && point1.X > point3.X)
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point1);
                                                            }
                                                            else if (point2.X > point1.X && point2.X > point3.X)
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point2);
                                                            }
                                                            else if (point3.X > point1.X && point3.X > point2.X)
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point3);
                                                            }
                                                        }

                                                    }

                                                }
                                                else if (int.Parse(DataTable.Rows[Get_index][4].ToString()) == 2)
                                                {
                                                    if (c == 3)
                                                    {
                                                        Point point1 = (Point)Route.Rows[i][1];
                                                        Point point2 = (Point)Route.Rows[i][2];
                                                        Point point3 = (Point)Route.Rows[i][3];
                                                        if (point1.Y > point.Y)
                                                        {
                                                            if (point2.X < point3.X)
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point2);
                                                            }
                                                            else
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point3);
                                                            }
                                                        }
                                                        else if (point2.Y > point.Y)
                                                        {
                                                            if (point1.X < point3.X)
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point1);
                                                            }
                                                            else
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point3);
                                                            }
                                                        }
                                                        else if (point3.Y > point.Y)
                                                        {
                                                            if (point1.X < point2.X)
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point1);
                                                            }
                                                            else
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point2);
                                                            }
                                                        }
                                                        else
                                                        {
                                                            if (point1.X < point2.X && point1.X < point3.X)
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point1);
                                                            }
                                                            else if (point2.X < point1.X && point2.X < point3.X)
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point2);
                                                            }
                                                            else if (point3.X < point1.X && point3.X < point2.X)
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point3);
                                                            }
                                                        }
                                                    }
                                                    else if (c == 4)
                                                    {
                                                        Point point1 = (Point)Route.Rows[i][1];
                                                        Point point2 = (Point)Route.Rows[i][2];
                                                        Point point3 = (Point)Route.Rows[i][3];
                                                        Point point4 = (Point)Route.Rows[i][4];
                                                        if (point1.Y > point.Y)
                                                        {
                                                            if (point4.X < point2.X && point4.X < point3.X)
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point4);
                                                            }
                                                            else if (point2.X < point4.X && point2.X < point3.X)
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point2);
                                                            }
                                                            else if (point3.X < point4.X && point3.X < point2.X)
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point3);
                                                            }
                                                        }
                                                        else if (point2.Y > point.Y)
                                                        {
                                                            if (point1.X < point4.X && point1.X < point3.X)
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point1);
                                                            }
                                                            else if (point4.X < point1.X && point4.X < point3.X)
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point4);
                                                            }
                                                            else if (point3.X < point1.X && point3.X < point4.X)
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point3);
                                                            }
                                                        }
                                                        else if (point3.Y > point.Y)
                                                        {
                                                            if (point1.X < point2.X && point1.X < point4.X)
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point1);
                                                            }
                                                            else if (point2.X < point1.X && point2.X < point4.X)
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point2);
                                                            }
                                                            else if (point4.X < point1.X && point4.X < point2.X)
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point4);
                                                            }
                                                        }
                                                        else if (point4.Y > point.Y)
                                                        {
                                                            if (point1.X < point2.X && point1.X < point3.X)
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point1);
                                                            }
                                                            else if (point2.X < point1.X && point2.X < point3.X)
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point2);
                                                            }
                                                            else if (point3.X < point1.X && point3.X < point2.X)
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point3);
                                                            }
                                                        }

                                                    }
                                                }
                                                else if (int.Parse(DataTable.Rows[Get_index][4].ToString()) == 0)
                                                {
                                                    if (c == 3)
                                                    {
                                                        Point point1 = (Point)Route.Rows[i][1];
                                                        Point point2 = (Point)Route.Rows[i][2];
                                                        Point point3 = (Point)Route.Rows[i][3];
                                                        if (point1.Y > point.Y)
                                                        {
                                                            if (point2.X > point3.X)
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point2);
                                                            }
                                                            else
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point3);
                                                            }
                                                        }
                                                        else if (point2.Y > point.Y)
                                                        {
                                                            if (point1.X > point3.X)
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point1);
                                                            }
                                                            else
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point3);
                                                            }
                                                        }
                                                        else if (point3.Y > point.Y)
                                                        {
                                                            if (point1.X > point2.X)
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point1);
                                                            }
                                                            else
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point2);
                                                            }
                                                        }
                                                        else
                                                        {
                                                            if (point1.X > point2.X && point1.X > point3.X && point2.X > point3.X)
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point2);
                                                            }
                                                            else if (point1.X > point2.X && point1.X > point3.X && point2.X < point3.X)
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point3);
                                                            }
                                                            else if (point2.X > point1.X && point2.X > point3.X && point1.X < point3.X)
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point3);
                                                            }
                                                            else if (point2.X > point1.X && point2.X > point3.X && point1.X > point3.X)
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point1);
                                                            }
                                                            else if (point3.X > point1.X && point3.X > point2.X && point1.X > point2.X)
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point1);
                                                            }
                                                            else if (point3.X > point1.X && point3.X > point2.X && point1.X < point2.X)
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point2);
                                                            }
                                                        }
                                                    }
                                                    else if (c == 4)
                                                    {
                                                        Point point1 = (Point)Route.Rows[i][1];
                                                        Point point2 = (Point)Route.Rows[i][2];
                                                        Point point3 = (Point)Route.Rows[i][3];
                                                        Point point4 = (Point)Route.Rows[i][4];
                                                        if (point1.Y > point.Y)
                                                        {
                                                            if (point4.X > point2.X && point4.X > point3.X && point2.X > point3.X)
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point2);
                                                            }
                                                            else if (point4.X > point2.X && point4.X > point3.X && point2.X < point3.X)
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point3);
                                                            }
                                                            else if (point2.X > point4.X && point2.X > point3.X && point4.X < point3.X)
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point3);
                                                            }
                                                            else if (point2.X > point4.X && point2.X > point3.X && point4.X > point3.X)
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point4);
                                                            }
                                                            else if (point3.X > point4.X && point3.X > point2.X && point4.X > point2.X)
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point4);
                                                            }
                                                            else if (point3.Y > point4.X && point3.X > point2.X && point4.X < point2.X)
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point2);
                                                            }
                                                        }
                                                        else if (point2.Y > point.Y)
                                                        {
                                                            if (point1.X > point4.X && point1.X > point3.X && point4.X > point3.X)
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point4);
                                                            }
                                                            else if (point1.X > point4.X && point1.X > point3.X && point4.X < point3.X)
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point3);
                                                            }
                                                            else if (point4.X > point1.X && point4.X > point3.X && point1.X < point3.X)
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point3);
                                                            }
                                                            else if (point4.X > point1.X && point4.X > point3.X && point1.X > point3.X)
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point1);
                                                            }
                                                            else if (point3.X > point1.X && point3.X > point4.X && point1.X > point4.X)
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point1);
                                                            }
                                                            else if (point3.X > point1.X && point3.X > point4.X && point1.X < point4.X)
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point4);
                                                            }
                                                        }
                                                        else if (point3.Y > point.Y)
                                                        {
                                                            if (point1.X > point2.X && point1.X > point4.X && point2.X > point4.X)
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point2);
                                                            }
                                                            else if (point1.X > point2.X && point1.X > point4.X && point2.X < point4.X)
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point4);
                                                            }
                                                            else if (point2.X > point1.X && point2.X > point4.X && point1.X < point4.X)
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point4);
                                                            }
                                                            else if (point2.X > point1.X && point2.X > point4.X && point1.X > point4.X)
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point1);
                                                            }
                                                            else if (point4.X > point1.X && point4.X > point2.X && point1.X > point2.X)
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point1);
                                                            }
                                                            else if (point4.X > point1.X && point4.X > point2.X && point1.X < point2.X)
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point2);
                                                            }
                                                        }
                                                        else if (point4.Y > point.Y)
                                                        {
                                                            if (point1.X > point2.X && point1.X > point3.X && point2.X > point3.X)
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point2);
                                                            }
                                                            else if (point1.X > point2.X && point1.X > point3.X && point2.X < point3.X)
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point3);
                                                            }
                                                            else if (point2.X > point1.X && point2.X > point3.X && point1.X < point3.X)
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point3);
                                                            }
                                                            else if (point2.X > point1.X && point2.X > point3.X && point1.X > point3.X)
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point1);
                                                            }
                                                            else if (point3.X > point1.X && point3.X > point2.X && point1.X > point2.X)
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point1);
                                                            }
                                                            else if (point3.Y > point1.X && point3.X > point2.X && point1.X < point2.X)
                                                            {
                                                                graphics.DrawLine(new Pen(Color.Red, 2), point, point2);
                                                            }
                                                        }

                                                    }
                                                }
                                            }
                                        }
                                        
                                    }
                                }
                               
                               
                                //else if () 
                                //{
                                    
                                //}

                            }
                            else if (int.Parse(DataTable.Rows[Get_index][6].ToString()) == 0 && int.Parse(DataTable.Rows[Get_index][5].ToString()) != 0)
                            {

                            }
                        done:;
                        }
                        else
                        {
                            panel1.Refresh();
                            //graphics.FillEllipse(Brushes.Transparent, rectangle);
                        }
                        break;
                    default:
                        break;
                }

            }
            catch (Exception ex)
            {

                //MessageBox.Show(ex.Message);
            }
        }
        int Get_index = 0;
        Point get_locaion(string  ID) 
        {
            Point rectangle = new Point();
            for (int i = 0; i < DataTable.Rows.Count; i++)
            {
                if (DataTable.Rows[i][0].ToString()==ID)
                {
                    rectangle = new Point(Convert.ToInt32(DataTable.Rows[i][1]) - size_rec, Convert.ToInt32(DataTable.Rows[i][2]) - size_rec);
                    Get_index = i;
                }
            }
            return rectangle;
        }
        #endregion
        #region Paint
        int mode = 0;
        List<Rectangle> Fill_circle = new List<Rectangle>();
        List<Point> Toa_do = new List<Point>();
       
        bool pressed = false;
        int FIFO = 0;
        Point point1 = new Point();
        int get_row = 0;
        int check = 0;
        bool draw = false;
       
        Point currentPos = new Point();
        Point startPos = new Point();
        public Rectangle GetRectangle()
        {
            return new Rectangle(
                Math.Min(startPos.X, currentPos.X),
                Math.Min(startPos.Y, currentPos.Y),
                Math.Abs(startPos.X - currentPos.X),
                Math.Abs(startPos.Y - currentPos.Y));
        }
        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            pressed = true;
            switch (mode)
            {
                case 1:
                    if (e.Button == MouseButtons.Left)
                    {
                            
                        foreach (var item in Board)
                        {
                            if ((e.X > item.X && (e.X < item.X + item.Width)) && (e.Y > item.Y && (e.Y < item.Y + item.Height)))
                            {
                                if (!Fill_circle.Contains(item)) 
                                {
                                    Fill_circle.Add(item);
                                    
                                    int d = 0;

                                    DataTable.Rows.Add();
                                    DataTable.Rows[DataTable.Rows.Count - 1][1] = (item.X + size_rec / 2).ToString();
                                    DataTable.Rows[DataTable.Rows.Count - 1][2] = (item.Y + size_rec / 2).ToString();
                                    string x = DataTable.Rows[DataTable.Rows.Count - 1][1].ToString();
                                    string y = DataTable.Rows[DataTable.Rows.Count - 1][2].ToString();
                                    panel1.Refresh();
                                    break;
                                }
                               
                            }
                            
                        }

                    }
                    else if (e.Button == MouseButtons.Right)
                    {
                        int i = 0;
                        foreach (var item in Fill_circle)
                        {
                            if ((e.X > item.X && (e.X < item.X + item.Width)) && (e.Y > item.Y && (e.Y < item.Y + item.Height)))
                            {
                                Point point = new Point(item.X, item.Y);
                                for (int fi = 0; fi < DataTable.Rows.Count; fi++)
                                {
                                    string x = DataTable.Rows[fi][1].ToString();
                                    string y = DataTable.Rows[fi][2].ToString();
                                    if (item.X + size_rec / 2 == Convert.ToInt32(x) && item.Y + size_rec / 2 == Convert.ToInt32(y)) 
                                    {
                                        MethodInvoker inv = delegate 
                                        {
                                            TB_Station.Text = "CARD: " + fi.ToString();
                                            TB_ID.Text ="ID:"+ DataTable.Rows[fi][0].ToString();
                                            TB_RUN.Text = "RUN:" + DataTable.Rows[fi][3].ToString();
                                            TB_LR.Text = "LR:" + DataTable.Rows[fi][4].ToString();
                                            TB_SPEED.Text = "SPEED:" + DataTable.Rows[fi][5].ToString();
                                            TB_DIR.Text = "DIR:" + DataTable.Rows[fi][6].ToString();
                                            TB_LIFT.Text = "LIFT:" + DataTable.Rows[fi][7].ToString();
                                        };this.Invoke(inv);
                                        break;
                                    }
                                }
                                TB_X.Text = "X: " + item.X.ToString();
                                TB_Y.Text = "Y: " + item.Y.ToString();
                                remove_point = point;
                                contextMenuStrip1.Items[1].Text = "Remove Point";
                                contextMenuStrip1.Show(point);

                                break;
                            }
                            i++;
                        }

                    }
                    break;
                case 0:
                    if (e.Button == MouseButtons.Right)
                    {
                        int i = 0;
                        foreach (var item in Fill_circle)
                        {
                            if ((e.X > item.X && (e.X < item.X + item.Width)) && (e.Y > item.Y && (e.Y < item.Y + item.Height)))
                            {
                                Point point = new Point(item.X, item.Y);
                                for (int fi = 0; fi < DataTable.Rows.Count; fi++)
                                {
                                    string x = DataTable.Rows[fi][1].ToString();
                                    string y = DataTable.Rows[fi][2].ToString();
                                    if (item.X + size_rec / 2 == Convert.ToInt32(x) && item.Y + size_rec / 2 == Convert.ToInt32(y))
                                    {
                                        MethodInvoker inv = delegate
                                        {
                                            TB_Station.Text = "CARD: " + fi.ToString();
                                            TB_ID.Text = "ID:" + DataTable.Rows[fi][0].ToString();
                                            TB_RUN.Text = "RUN:" + DataTable.Rows[fi][3].ToString();
                                            TB_LR.Text = "LR:" + DataTable.Rows[fi][4].ToString();
                                            TB_SPEED.Text = "SPEED:" + DataTable.Rows[fi][5].ToString();
                                            TB_DIR.Text = "DIR:" + DataTable.Rows[fi][6].ToString();
                                            TB_LIFT.Text = "LIFT:" + DataTable.Rows[fi][7].ToString();
                                        }; this.Invoke(inv);
                                        break;
                                    }
                                }
                                TB_X.Text = "X: " + item.X.ToString();
                                TB_Y.Text = "Y: " + item.Y.ToString();
                                //remove_point = point
                                contextMenuStrip1.Items[1].Text = "Remove Point";
                                contextMenuStrip1.Show(point);

                                break;
                            }
                            i++;
                        }
                        
                    }
                    else if (e.Button == MouseButtons.Left)
                    {

                        foreach (var item in Fill_circle)
                        {
                            if ((e.X > item.X && (e.X < item.X + item.Width)) && (e.Y > item.Y && (e.Y < item.Y + item.Height)))
                            {
                                if (Toa_do.Count < 3) 
                                {
                                    Point point = new Point(item.X + item.Width / 2, item.Y + item.Height / 2);
                                    Graphics graphics = panel1.CreateGraphics();
                                    graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                                    graphics.FillEllipse(Brushes.Red, item);
                                    Toa_do.Add(point);
                                }
                                else 
                                {
                                    MessageBox.Show("Only 2 Point To Select");
                                }
                            }
                            //else 
                            //{
                            
                            //}
                        }
                        //if (Toa_do.Count > 0) 
                        //{
                        //    panel1.Refresh();
                        //    Toa_do.Clear();
                        //}
                    }
                    break;
                case 2:
                    if (e.Button == MouseButtons.Left)
                    {
                        int i = 0;
                        foreach (var item in Fill_circle)
                        {
                            if ((e.X > item.X && (e.X < item.X + item.Width)) && (e.Y > item.Y && (e.Y < item.Y + item.Height)))
                            {
                                FIFO++;
                               
                               
                                Point point = new Point(item.X + item.Width / 2, item.Y + item.Height / 2);
                                if (FIFO % 2 == 1) 
                                {
                                    temp_Poi = point;
                                }
                                if (FIFO % 2 == 1) 
                                {
                                    bool them = true;
                                    for (int y = 0; y < Route.Rows.Count; y++)
                                    {
                                        //if (Route.Rows[y][0] == DBNull.Value) 
                                        //{
                                        //    them = false;
                                        //    Route.Rows[y][0] = point;
                                        //}
                                        if ((Point)Route.Rows[y][0] == point) 
                                        {
                                            them = false;
                                            break;
                                        }
                                        else 
                                        {
                                            them = true;
                                        }
                                    }
                                    if (them) 
                                    {
                                        Route.Rows.Add();
                                        Route.Rows[Route.Rows.Count - 1][0] = point;
                                        if (Route.Rows.Count == 1)
                                        {
                                            get_row = 0;
                                        }
                                    }
                                }
                                for (int t = 0; t < Route.Rows.Count; t++)
                                {
                                    if ((Point)Route.Rows[t][0] == point && FIFO % 2 == 1) //kiểm tra cột đầu tiên có chứa điểm hay không
                                    {
                                        check = 1;                                         //có chứa
                                        get_row = t;
                                        break;
                                    }
                                    else if((Point)Route.Rows[t][0] != point && FIFO % 2 == 1)
                                    {
                                        check = 0;
                                    }
                                    
                                }

                                
                                if (check == 0 && FIFO % 2 == 0) 
                                {
                                    for (int ii = 1; ii < Route.Columns.Count; ii++)
                                    {
                                        object n = Route.Rows[get_row][ii];
                                        if (n==DBNull.Value)
                                        {
                                            Route.Rows[get_row][ii] = point;
                                            break;
                                        }
                                    }
                                    //break;
                                }
                                else if (check == 1 && FIFO % 2 == 0) 
                                {
                                    for (int ii = 1; ii < Route.Columns.Count; ii++)
                                    {
                                        object n = Route.Rows[get_row][ii];
                                        if (n == DBNull.Value)
                                        {
                                            Route.Rows[get_row][ii] = point;
                                            break;
                                        }
                                    }
                                    //break;
                                }
                                
                                panel1.Refresh();
                            }
                            i++;
                        }
                        Call_AGV_Configue.Dem = FIFO;
                      
                    }
                    else if (e.Button == MouseButtons.Right)
                    {
                        int i = 0;
                        foreach (var item in Fill_circle)
                        {
                            if ((e.X > item.X && (e.X < item.X + item.Width)) && (e.Y > item.Y && (e.Y < item.Y + item.Height)))
                            {
                                Point point = new Point(item.X, item.Y);
                                Point point2 = new Point(item.X + item.Width / 2, item.Y + item.Height / 2);
                                for (int fi = 0; fi < DataTable.Rows.Count; fi++)
                                {
                                    string x = DataTable.Rows[fi][1].ToString();
                                    string y = DataTable.Rows[fi][2].ToString();
                                    if (item.X + size_rec / 2 == Convert.ToInt32(x) && item.Y + size_rec / 2 == Convert.ToInt32(y))
                                    {
                                        MethodInvoker inv = delegate
                                        {
                                            TB_ID.Text = "ID:" + DataTable.Rows[fi][0].ToString();
                                            TB_RUN.Text = "RUN:" + DataTable.Rows[fi][3].ToString();
                                            TB_LR.Text = "LR:" + DataTable.Rows[fi][4].ToString();
                                            TB_SPEED.Text = "SPEED:" + DataTable.Rows[fi][5].ToString();
                                            TB_DIR.Text = "DIR:" + DataTable.Rows[fi][6].ToString();
                                            TB_LIFT.Text = "LIFT:" + DataTable.Rows[fi][7].ToString();
                                        }; this.Invoke(inv);
                                        break;
                                    }
                                }
                                TB_X.Text = "X: " + item.X.ToString();
                                TB_Y.Text = "Y: " + item.Y.ToString();
                                remove_route = point2;
                                contextMenuStrip1.Items[1].Text = "Remove Route";
                                contextMenuStrip1.Show(point);

                                break;
                            }
                            i++;
                        }

                    }
                    break;
                default:
                    break;
            }
        }
        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            Draw_Point_Board(e);
            Draw_Route(e);
        }
        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            pressed = false;
        }
        #endregion
        #region Button Event
        private void drop_btn(object sender, EventArgs e)
        {
            int i = 0;
            string mesage = "Do you want to remove this station";
            string cap = "Remove Item";
            var result = MessageBox.Show(mesage, cap, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                foreach (var item in File_data)
                {
                    if (item == comboBox1.Text)
                    {
                        File_data.Remove(item);
                        comboBox1.Items.Clear();
                        if (File.Exists(ConfigXML.Community_MapFile(item)))
                        {
                            File.Delete(ConfigXML.Community_MapFile(item));
                        }
                        if (File.Exists(ConfigXML.Create_MapFile(item)))
                        {
                            File.Delete(ConfigXML.Create_MapFile(item));
                        }
                        break;
                    }
                    i++;
                }
                foreach (var item in File_data)
                {
                    comboBox1.Items.Add(item);
                }
                comboBox1.SelectedIndex = i - 1;
                Call_AGV_Configue.File_data = File_data;
                Call_AGV_Configue.Selected_Station = comboBox1.Text;
                ConfigXML.UpdateFullSystem_Config(Call_AGV_Configue);
                Call_AGV_Configue = ConfigXML.GetSystem_Config();
                Mapping = ConfigXML.GetMapping(Call_AGV_Configue.Selected_Station);
                Community = ConfigXML.Get_community(Call_AGV_Configue.Selected_Station);
                File_data = Call_AGV_Configue.File_data;
                DataTable = Community.DataTable;
                Fill_circle = Mapping.Rectangles_2_Fill;
                Route = Mapping.Route;
                panel1.Refresh();
            }

        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text == "") return;
            if (!comboBox1.Items.Contains(comboBox1.Text))
            {
                comboBox1.Items.Add(comboBox1.Text);
                File_data.Add(comboBox1.Text);
                Call_AGV_Configue.File_data = File_data;
                Call_AGV_Configue.Selected_Station = comboBox1.Text;
                ConfigXML.UpdateFullSystem_Config(Call_AGV_Configue);
                Call_AGV_Configue = ConfigXML.GetSystem_Config();
                Mapping = ConfigXML.GetMapping(Call_AGV_Configue.Selected_Station);
                Community = ConfigXML.Get_community(Call_AGV_Configue.Selected_Station);
                File_data = Call_AGV_Configue.File_data;
                DataTable = Community.DataTable;
                Fill_circle = Mapping.Rectangles_2_Fill;
                Route = Mapping.Route;
                panel1.Refresh();
            }
            else
            {
                MessageBox.Show("Map already exitsted");
            }

        }
        private void button1_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog FolderBrowserDialog = new FolderBrowserDialog();
            if (FolderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = FolderBrowserDialog.SelectedPath.ToString();
                Call_AGV_Configue.Path_Communicate = textBox1.Text;
                ConfigXML.UpdateFullSystem_Config(Call_AGV_Configue);
                Call_AGV_Configue = ConfigXML.GetSystem_Config();
                if (!File.Exists(Call_AGV_Configue.Path_Communicate + @"\" + ConfigXML.Read_txt))
                {
                    File.Create(Call_AGV_Configue.Path_Communicate + @"\" + ConfigXML.Read_txt).Close();
                }
                FileSystemWatcher = new FileSystemWatcher(Path.GetDirectoryName(Call_AGV_Configue.Path_Communicate + @"\" + ConfigXML.Write_txt));
                FileSystemWatcher.NotifyFilter = NotifyFilters.LastWrite;
                FileSystemWatcher.Filter = "*.txt";
                FileSystemWatcher.Changed += FileSystemWatcher_Changed;
                FileSystemWatcher.EnableRaisingEvents = true;
            }
        }
        private void Download_DATA_BGR_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

        }

        private void Download_DATA_BGR_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                
            }
            catch (Exception ex)
            {

               
            }
        }
        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                StreamWriter streamWriter = new StreamWriter(Call_AGV_Configue.Path_Communicate + @"\" + ConfigXML.Read_txt);
                streamWriter.WriteLine("Go To:");
                streamWriter.WriteLine(comboBox1.Text);
                streamWriter.Close();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }


        }
        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                StreamWriter streamWriter = new StreamWriter(Call_AGV_Configue.Path_Communicate + @"\" + ConfigXML.Read_txt);
                streamWriter.WriteLine("Run:"+comboBox1.Text);
                streamWriter.Close();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            if (button3.Text == "Make Route")
            {
                mode = 2;
                button3.Text = "Apply Route";
            }
            else if (button3.Text == "Apply Route")
            {
                try
                {
                    mode = 0;
                    button3.Text = "Make Route";
                    Mapping.Route = Route;
                    ConfigXML.Update_Mapping(Mapping, comboBox1.Text);
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message);
                }

            }
        }
        private void button4_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text == "") return;
           
            if (button4.Text == "Edit Map")
            {
                if (Timer.Enabled) 
                {
                    Timer.Stop();
                    Timer.Enabled = false;
                }
                button4.Text = "Done";
                mode = 1;
                panel1.Refresh();

            }
            else if (button4.Text == "Done")
            {
                try
                {
                    button4.Text = "Edit Map";
                    mode = 0;
                    panel1.Refresh();
                    Mapping.Rectangles_2_Fill = Fill_circle;
                    Community.DataTable = DataTable;
                    Call_AGV_Configue.Selected_Station = comboBox1.Text;
                    ConfigXML.UpdateFullSystem_Config(Call_AGV_Configue);
                    ConfigXML.Update_Mapping(Mapping, comboBox1.Text);
                    ConfigXML.Update_Mapping(Community, comboBox1.Text);
                   
                    MessageBox.Show("Edit Successfully");
                }
                catch (Exception ex)
                {

                    MessageBox.Show("Error Edit. Because " + ex.Message);
                }
            }

        }
        #endregion
        #region menutool trip
        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {

        }
        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {

        }
        private void TB_ID_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (pressed) return;
                if (!TB_ID.Text.Contains("ID:"))
                {
                    throw new Exception();
                }
                MethodInvoker inv = delegate
                {
                    string[] Chia = new string[2];
                    Chia = TB_ID.Text.Split(':');
                    if (Chia[1] != "") 
                    {
                        for (int i = 0; i < DataTable.Rows.Count; i++)
                        {
                            string x = DataTable.Rows[i][1].ToString();
                            string y = DataTable.Rows[i][2].ToString();
                            x = (int.Parse(x) - size_rec / 2).ToString();
                            y = (int.Parse(y) - size_rec / 2).ToString();
                            x.Trim(new char[] { ' ' });
                            y.Trim(new char[] { ' ' });
                            string[] Chiax = TB_X.Text.Split(':');
                            string[] Chiay = TB_Y.Text.Split(':');
                            Chiax[1].Trim(new char[] { ' ' });
                            Chiay[1].Trim(new char[] { ' ' });
                            if (int.Parse(x) == int.Parse(Chiax[1])&& int.Parse(y) == int.Parse(Chiay[1])) 
                            {
                                DataTable.Rows[i][0] = Chia[1];
                            }
                           
                        }
                    }
                }; this.Invoke(inv);
                TB_Save.Enabled = true;
                TB_Save.Visible = true;
            }
            catch (Exception)
            {

                MethodInvoker inv = delegate
                {
                    TB_ID.Text = "ID:";
                }; this.Invoke(inv);
            }
        }
        private void button7_Click(object sender, EventArgs e)
        {

        }
        private void TB_RUN_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (pressed) return;
                if (!TB_RUN.Text.Contains("RUN:"))
                {
                    throw new Exception();
                }
                MethodInvoker inv = delegate
                {

                    string[] Chia = new string[2];
                    Chia = TB_RUN.Text.Split(':');
                    if (Chia[1] != "")
                    {
                        for (int i = 0; i < DataTable.Rows.Count; i++)
                        {
                            string x = DataTable.Rows[i][1].ToString();
                            string y = DataTable.Rows[i][2].ToString();
                            x = (int.Parse(x) - size_rec / 2).ToString();
                            y = (int.Parse(y) - size_rec / 2).ToString();
                            x.Trim(new char[] { ' ' });
                            y.Trim(new char[] { ' ' });
                            string[] Chiax = TB_X.Text.Split(':');
                            string[] Chiay = TB_Y.Text.Split(':');
                            Chiax[1].Trim(new char[] { ' ' });
                            Chiay[1].Trim(new char[] { ' ' });
                            if (int.Parse(x) == int.Parse(Chiax[1]) && int.Parse(y) == int.Parse(Chiay[1]))
                            {
                                DataTable.Rows[i][3] = Chia[1];
                            }

                        }
                    }
                }; this.Invoke(inv);
                TB_Save.Enabled = true;
                TB_Save.Visible = true;
            }
            catch (Exception)
            {

                MethodInvoker inv = delegate
                {
                    TB_RUN.Text = "RUN:";
                }; this.Invoke(inv);
            }
        }
        private void TB_LR_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (pressed) return;
                if (!TB_LR.Text.Contains("LR:"))
                {
                    throw new Exception();
                }
                MethodInvoker inv = delegate
                {

                    string[] Chia = new string[2];
                    Chia = TB_LR.Text.Split(':');
                    if (Chia[1] != "")
                    {
                        for (int i = 0; i < DataTable.Rows.Count; i++)
                        {
                            string x = DataTable.Rows[i][1].ToString();
                            string y = DataTable.Rows[i][2].ToString();
                            x = (int.Parse(x) - size_rec / 2).ToString();
                            y = (int.Parse(y) - size_rec / 2).ToString();
                            x.Trim(new char[] { ' ' });
                            y.Trim(new char[] { ' ' });
                            string[] Chiax = TB_X.Text.Split(':');
                            string[] Chiay = TB_Y.Text.Split(':');
                            Chiax[1].Trim(new char[] { ' ' });
                            Chiay[1].Trim(new char[] { ' ' });
                            if (int.Parse(x) == int.Parse(Chiax[1]) && int.Parse(y) == int.Parse(Chiay[1]))
                            {
                                DataTable.Rows[i][4] = Chia[1];
                            }

                        }
                    }
                }; this.Invoke(inv);
                TB_Save.Enabled = true;
                TB_Save.Visible = true;
            }
            catch (Exception)
            {

                MethodInvoker inv = delegate
                {
                    TB_LR.Text = "LR:";
                }; this.Invoke(inv);
            }
        }
        private void TB_SPEED_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (pressed) return;
                if (!TB_SPEED.Text.Contains("SPEED:"))
                {
                    throw new Exception();
                }
                MethodInvoker inv = delegate
                {

                    string[] Chia = new string[2];
                    Chia = TB_SPEED.Text.Split(':');
                    if (Chia[1] != "")
                    {
                        for (int i = 0; i < DataTable.Rows.Count; i++)
                        {
                            string x = DataTable.Rows[i][1].ToString();
                            string y = DataTable.Rows[i][2].ToString();
                            x = (int.Parse(x) - size_rec / 2).ToString();
                            y = (int.Parse(y) - size_rec / 2).ToString();
                            x.Trim(new char[] { ' ' });
                            y.Trim(new char[] { ' ' });
                            string[] Chiax = TB_X.Text.Split(':');
                            string[] Chiay = TB_Y.Text.Split(':');
                            Chiax[1].Trim(new char[] { ' ' });
                            Chiay[1].Trim(new char[] { ' ' });
                            if (int.Parse(x) == int.Parse(Chiax[1]) && int.Parse(y) == int.Parse(Chiay[1]))
                            {
                                DataTable.Rows[i][5] = Chia[1];
                            }

                        }
                    }
                }; this.Invoke(inv);
                TB_Save.Enabled = true;
                TB_Save.Visible = true;
            }
            catch (Exception)
            {

                MethodInvoker inv = delegate
                {
                    TB_SPEED.Text = "SPEED:";
                }; this.Invoke(inv);
            }
        }
        private void TB_DIR_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (pressed) return;
                if (!TB_DIR.Text.Contains("DIR:"))
                {
                    throw new Exception();
                }
                MethodInvoker inv = delegate
                {

                    string[] Chia = new string[2];
                    Chia = TB_DIR.Text.Split(':');
                    if (Chia[1] != "")
                    {
                        for (int i = 0; i < DataTable.Rows.Count; i++)
                        {
                            string x = DataTable.Rows[i][1].ToString();
                            string y = DataTable.Rows[i][2].ToString();
                            x = (int.Parse(x) - size_rec / 2).ToString();
                            y = (int.Parse(y) - size_rec / 2).ToString();
                            x.Trim(new char[] { ' ' });
                            y.Trim(new char[] { ' ' });
                            string[] Chiax = TB_X.Text.Split(':');
                            string[] Chiay = TB_Y.Text.Split(':');
                            Chiax[1].Trim(new char[] { ' ' });
                            Chiay[1].Trim(new char[] { ' ' });
                            if (int.Parse(x) == int.Parse(Chiax[1]) && int.Parse(y) == int.Parse(Chiay[1]))
                            {
                                DataTable.Rows[i][6] = Chia[1];
                            }

                        }
                    }
                }; this.Invoke(inv);
                TB_Save.Enabled = true;
                TB_Save.Visible = true;
            }
            catch (Exception)
            {

                MethodInvoker inv = delegate
                {
                    TB_DIR.Text = "DIR:";
                }; this.Invoke(inv);
            }
        }
        private void TB_LIFT_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (pressed) return;
                if (!TB_LIFT.Text.Contains("LIFT:"))
                {
                    throw new Exception();
                }
                MethodInvoker inv = delegate
                {

                    string[] Chia = new string[2];
                    Chia = TB_LIFT.Text.Split(':');
                    if (Chia[1] != "")
                    {
                        for (int i = 0; i < DataTable.Rows.Count; i++)
                        {
                            string x = DataTable.Rows[i][1].ToString();
                            string y = DataTable.Rows[i][2].ToString();
                            x = (int.Parse(x) - size_rec / 2).ToString();
                            y = (int.Parse(y) - size_rec / 2).ToString();
                            x.Trim(new char[] { ' ' });
                            y.Trim(new char[] { ' ' });
                            string[] Chiax = TB_X.Text.Split(':');
                            string[] Chiay = TB_Y.Text.Split(':');
                            Chiax[1].Trim(new char[] { ' ' });
                            Chiay[1].Trim(new char[] { ' ' });
                            if (int.Parse(x) == int.Parse(Chiax[1]) && int.Parse(y) == int.Parse(Chiay[1]))
                            {
                                DataTable.Rows[i][7] = Chia[1];
                            }

                        }
                    }
                }; this.Invoke(inv);
                TB_Save.Enabled = true;
                TB_Save.Visible = true;
            }
            catch (Exception)
            {

                MethodInvoker inv = delegate
                {
                    TB_LIFT.Text = "LIFT:";
                }; this.Invoke(inv);
            }
        }
        Point remove_point;
        Point remove_route;
        private void removeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (contextMenuStrip1.Items[1].Text == "Remove Point" && mode == 1)
            {
                var item = Fill_circle.Single(x => x.Contains(remove_point));
                int s = Fill_circle.IndexOf(item);
                Fill_circle.Remove(item);
                DataTable.Rows[s].Delete();
                DataTable.AcceptChanges();
                panel1.Refresh();
            }
            else if (contextMenuStrip1.Items[1].Text == "Remove Route") 
            {
                for (int i = 0; i < Route.Rows.Count; i++)
                {
                    bool done = false;
                    foreach (var item in Toa_do)
                    {
                        if ((Point)Route.Rows[i][0] == item) 
                        {
                            foreach (var item1 in Toa_do)
                            {
                                int k = 0;
                                for (int j = 1; j < Route.Columns.Count; j++)
                                {
                                    if (Route.Rows[i][j] != DBNull.Value) 
                                    {
                                        k++;
                                    }
                                   
                                }
                                if (k == 1) 
                                {
                                    for (int j = 0; j < Route.Columns.Count; j++)
                                    {
                                        if (Route.Rows[i][j] != DBNull.Value)
                                        {
                                            if ((Point)Route.Rows[i][j] == item1)
                                            {
                                                Route.Rows[i].Delete();
                                                Route.AcceptChanges();
                                                goto here;
                                            }
                                        }
                                    }
                                }
                                else if (k > 1) 
                                {
                                    for (int j = 1; j < Route.Columns.Count; j++)
                                    {
                                        if (Route.Rows[i][j] != DBNull.Value)
                                        {
                                            if ((Point)Route.Rows[i][j] == item1)
                                            {
                                                Route.Rows[i][j] = DBNull.Value;
                                                if (j == 1) 
                                                {
                                                    Route.Rows[i][j] = Route.Rows[i][j + 1];
                                                    Route.Rows[i][j + 1] = Route.Rows[i][j + 2];
                                                    Route.Rows[i][j + 2] = Route.Rows[i][j + 3];
                                                }
                                                else if (j == 2) 
                                                {
                                                    Route.Rows[i][j] = Route.Rows[i][j + 1];
                                                    Route.Rows[i][j + 1] = Route.Rows[i][j + 2];
                                                }
                                                else if (j == 3)
                                                {
                                                    Route.Rows[i][j] = Route.Rows[i][j + 1];
                                                }
                                                goto here;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            here:;
            Toa_do.Clear();              
            panel1.Refresh();
            }

        }
        private void TB_Save_Click(object sender, EventArgs e)
        {
            MethodInvoker inv = delegate
            {
                Community.DataTable = DataTable;
            }; this.Invoke(inv);
            TB_Save.Enabled = false;
            TB_Save.Visible = false;
        }
        List<string> File_data = new List<string>();




        #endregion

        private void button8_Click(object sender, EventArgs e)
        {
           
            GPS = textBox3.Text;
            blink = 0;
            AGV_location(GPS);
            panel1.Refresh();
        }
    }
}
