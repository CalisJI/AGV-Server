﻿using System;
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
        DataTable DataTable = new DataTable();
        Mapping Mapping = new Mapping();
        Call_AGV_Configue Call_AGV_Configue = new Call_AGV_Configue();
        FileSystemWatcher FileSystemWatcher = new FileSystemWatcher();
        Timer Timer = new Timer();
        public Form1()
        {
            InitializeComponent();
            Timer.Interval = 1000;
            Timer.Tick += Timer_Tick;
            Timer.Enabled = false;
        }

        
        #region Form
        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                Call_AGV_Configue = ConfigXML.GetSystem_Config();
                Mapping = ConfigXML.GetMapping(Call_AGV_Configue.Selected_Station);
                File_data = Call_AGV_Configue.File_data;
                //Board = Mapping.Rectangles;
                Fill_circle = Mapping.Rectangles_2_Fill;
                Route = Mapping.Route;
                //panel1.Paint += new PaintEventHandler(panel1_Paint);
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
                FileSystemWatcher = new FileSystemWatcher(Path.GetDirectoryName(Call_AGV_Configue.Path_Communicate + @"\" + ConfigXML.Write_txt));
                FileSystemWatcher.NotifyFilter = NotifyFilters.LastWrite;
                FileSystemWatcher.Filter = "*.txt";
                FileSystemWatcher.Changed += FileSystemWatcher_Changed;
                FileSystemWatcher.EnableRaisingEvents = true;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }


        }
        int z = 0;
        int dem = 0;
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
            ConfigXML.UpdateFullSystem_Config(Call_AGV_Configue);
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

            Call_AGV_Configue.File_data = File_data;
            Call_AGV_Configue.Selected_Station = comboBox1.Text;
            ConfigXML.UpdateFullSystem_Config(Call_AGV_Configue);
            Call_AGV_Configue = ConfigXML.GetSystem_Config();
            Mapping = ConfigXML.GetMapping(Call_AGV_Configue.Selected_Station);
            File_data = Call_AGV_Configue.File_data;
            //Board = Mapping.Rectangles;
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
            Board.Clear();
            for (int i = 0; i < panel1.Width; i += 30)
            {
                for (int j = 0; j < panel1.Height; j += 30)
                {
                    e.Graphics.DrawEllipse(new Pen(Color.Black, 1), new Rectangle(new Point(i + size_rec/2, j + size_rec / 2), new Size(size_rec, size_rec)));
                    Board.Add(new Rectangle(new Point(i + size_rec / 2, j + size_rec / 2), new Size(size_rec, size_rec)));
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
            if (Route.Count % 2 == 0 && Route.Count > 1)
            {
                for (int j = 0; j < Route.Count; j = j + 2)
                {
                    Pen pen = new Pen(Brushes.Black, 1);
                    pen.EndCap = System.Drawing.Drawing2D.LineCap.ArrowAnchor;
                    e.Graphics.DrawLine(pen, Route[j], Route[j + 1]);
                }
            }
            else if (Route.Count % 2 == 1 && Route.Count > 1)
            {
                for (int j = 0; j < Route.Count - 1; j = j + 2)
                {
                    Pen pen = new Pen(Brushes.Yellow, 1);
                   
                    pen.EndCap = System.Drawing.Drawing2D.LineCap.ArrowAnchor;
                    e.Graphics.DrawLine(pen, Route[j], Route[j + 1]);
                  

                }
                Font font = new Font("Times New Roman", 10);
                e.Graphics.DrawString("(*)", font, Brushes.Red, new PointF(Route[Route.Count-1].X, Route[Route.Count-1].Y - size_rec));
            }
        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            blink++;
        }
        int blink = 0;
        void AGV_location() 
        {
            
        }      
        Point get_locaion(string  ID) 
        {
            Point rectangle = new Point();
            for (int i = 0; i < DataTable.Rows.Count; i++)
            {
                if (DataTable.Rows[i][0].ToString()==ID)
                {
                    rectangle = new Point(Convert.ToInt32(DataTable.Rows[i][1]),Convert.ToInt32(DataTable.Rows[i][2]));
                }
            }
            return rectangle;
        }
        #endregion
        #region Paint
        int mode = 0;
        List<Rectangle> Fill_circle = new List<Rectangle>();
        List<string> Toa_do = new List<string>();
        List<Point> Route = new List<Point>();
        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            switch (mode)
            {
                case 1:
                    if (e.Button == MouseButtons.Left)
                    {

                        foreach (var item in Board)
                        {
                            if ((e.X > item.X && (e.X < item.X + item.Width)) && (e.Y > item.Y && (e.Y < item.Y + item.Height)))
                            {
                                Fill_circle.Add(item);
                                Toa_do.Add((item.X + size_rec / 2).ToString() + "-" + (item.Y + size_rec / 2).ToString());
                                panel1.Refresh();
                                break;
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

                    break;
                case 2:
                    if (e.Button == MouseButtons.Left)
                    {
                        int i = 0;
                        foreach (var item in Fill_circle)
                        {
                            if ((e.X > item.X && (e.X < item.X + item.Width)) && (e.Y > item.Y && (e.Y < item.Y + item.Height)))
                            {
                                Point point = new Point(item.X + item.Width / 2, item.Y + item.Height / 2);
                                Route.Add(point);
                                panel1.Refresh();
                                break;
                            }
                            i++;
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
                                Point point2 = new Point(item.X + item.Width / 2, item.Y + item.Height / 2);

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
        #endregion
        #region Button Event
        private void button2_Click(object sender, EventArgs e)
        {

            if (!comboBox1.Items.Contains(comboBox1.Text))
            {
                comboBox1.Items.Add(comboBox1.Text);
                File_data.Add(comboBox1.Text);
                Call_AGV_Configue.File_data = File_data;
                Call_AGV_Configue.Selected_Station = comboBox1.Text;
                ConfigXML.UpdateFullSystem_Config(Call_AGV_Configue);
                Call_AGV_Configue = ConfigXML.GetSystem_Config();
                Mapping = ConfigXML.GetMapping(Call_AGV_Configue.Selected_Station);
                File_data = Call_AGV_Configue.File_data;
                //Board = Mapping.Rectangles;
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
        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                StreamWriter streamWriter = new StreamWriter(Call_AGV_Configue.Path_Communicate + @"\" + ConfigXML.Read_txt);
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
                streamWriter.WriteLine(comboBox1.Text);
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
            if (button4.Text == "Edit Map")
            {
                button4.Text = "Done";
                mode = 1;

            }
            else if (button4.Text == "Done")
            {
                try
                {
                    button4.Text = "Edit Map";
                    mode = 0;

                    Mapping.Rectangles_2_Fill = Fill_circle;
                    ConfigXML.Update_Mapping(Mapping, comboBox1.Text);
                    Call_AGV_Configue.Selected_Station = comboBox1.Text;
                    ConfigXML.UpdateFullSystem_Config(Call_AGV_Configue);
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
                if (!TB_ID.Text.Contains("ID:"))
                {
                    throw new Exception();
                }
                MethodInvoker inv = delegate
                {
                    string[] Chia = new string[2];
                    Chia = TB_ID.Text.Split(':');
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
                if (!TB_RUN.Text.Contains("RUN:"))
                {
                    throw new Exception();
                }
                MethodInvoker inv = delegate
                {

                    string[] Chia = new string[2];
                    Chia = TB_RUN.Text.Split(':');
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
                if (!TB_LR.Text.Contains("LR:"))
                {
                    throw new Exception();
                }
                MethodInvoker inv = delegate
                {

                    string[] Chia = new string[2];
                    Chia = TB_LR.Text.Split(':');
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
                if (!TB_SPEED.Text.Contains("SPEED:"))
                {
                    throw new Exception();
                }
                MethodInvoker inv = delegate
                {

                    string[] Chia = new string[2];
                    Chia = TB_SPEED.Text.Split(':');
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
                if (!TB_DIR.Text.Contains("DIR:"))
                {
                    throw new Exception();
                }
                MethodInvoker inv = delegate
                {

                    string[] Chia = new string[2];
                    Chia = TB_DIR.Text.Split(':');
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
                if (!TB_LIFT.Text.Contains("LIFT:"))
                {
                    throw new Exception();
                }
                MethodInvoker inv = delegate
                {

                    string[] Chia = new string[2];
                    Chia = TB_LIFT.Text.Split(':');
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
            if (contextMenuStrip1.Items[1].Text == "Remove Point")
            {
                var item = Fill_circle.Single(x => x.Contains(remove_point));
                Fill_circle.Remove(item);
                panel1.Refresh();
            }
            else if (contextMenuStrip1.Items[1].Text == "Remove Route")
            {
                int a = 0;
                foreach (var aitem in Route)
                {
                    if (aitem == remove_route)
                    {
                        if (a % 2 == 0)
                        {
                            Route.RemoveRange(a, 2);
                            panel1.Refresh();
                            break;

                        }
                        else
                        {
                            Route.RemoveRange(a - 1, 2);
                            panel1.Refresh();
                            break;
                        }
                    }

                    a++;
                }
            }

        }
        private void TB_Save_Click(object sender, EventArgs e)
        {
            MethodInvoker inv = delegate
            {

            }; this.Invoke(inv);
            TB_Save.Enabled = false;
            TB_Save.Visible = false;
        }
        List<string> File_data = new List<string>();
        #endregion
    }
}