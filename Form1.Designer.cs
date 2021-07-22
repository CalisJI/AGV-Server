
namespace Call_AGV
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.panel1 = new System.Windows.Forms.Panel();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.informationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.TB_Station = new System.Windows.Forms.ToolStripTextBox();
            this.TB_ID = new System.Windows.Forms.ToolStripTextBox();
            this.TB_X = new System.Windows.Forms.ToolStripTextBox();
            this.TB_Y = new System.Windows.Forms.ToolStripTextBox();
            this.TB_RUN = new System.Windows.Forms.ToolStripTextBox();
            this.TB_LR = new System.Windows.Forms.ToolStripTextBox();
            this.TB_SPEED = new System.Windows.Forms.ToolStripTextBox();
            this.TB_DIR = new System.Windows.Forms.ToolStripTextBox();
            this.TB_LIFT = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.TB_Save = new System.Windows.Forms.ToolStripMenuItem();
            this.removeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.button3 = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.button6 = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.button9 = new System.Windows.Forms.Button();
            this.contextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.Ten_Tram = new System.Windows.Forms.ToolStripTextBox();
            this.Trang_Thai = new System.Windows.Forms.ToolStripTextBox();
            this.Station_remove = new System.Windows.Forms.ToolStripMenuItem();
            this.label6 = new System.Windows.Forms.Label();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.button8 = new System.Windows.Forms.Button();
            this.contextMenuStrip1.SuspendLayout();
            this.contextMenuStrip2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(2, 55);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1366, 678);
            this.panel1.TabIndex = 0;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            this.panel1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseDown);
            this.panel1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseMove);
            this.panel1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseUp);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(12, 26);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(120, 23);
            this.textBox1.TabIndex = 1;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 1);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(120, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "Select Share Folder";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.button2.BackgroundImage = global::Call_AGV.Properties.Resources.create;
            this.button2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button2.Location = new System.Drawing.Point(245, 5);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(45, 44);
            this.button2.TabIndex = 2;
            this.toolTip1.SetToolTip(this.button2, "Create New Navigation Map");
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button4
            // 
            this.button4.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.button4.BackgroundImage = global::Call_AGV.Properties.Resources._3651302_200;
            this.button4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button4.Location = new System.Drawing.Point(398, 6);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(45, 44);
            this.button4.TabIndex = 2;
            this.button4.UseVisualStyleBackColor = false;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button5
            // 
            this.button5.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.button5.BackgroundImage = global::Call_AGV.Properties.Resources.Download_79;
            this.button5.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button5.Location = new System.Drawing.Point(500, 6);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(45, 44);
            this.button5.TabIndex = 2;
            this.button5.UseVisualStyleBackColor = false;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.informationToolStripMenuItem,
            this.removeToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(138, 48);
            this.contextMenuStrip1.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip1_Opening);
            // 
            // informationToolStripMenuItem
            // 
            this.informationToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TB_Station,
            this.TB_ID,
            this.TB_X,
            this.TB_Y,
            this.TB_RUN,
            this.TB_LR,
            this.TB_SPEED,
            this.TB_DIR,
            this.TB_LIFT,
            this.toolStripSeparator1,
            this.TB_Save});
            this.informationToolStripMenuItem.Name = "informationToolStripMenuItem";
            this.informationToolStripMenuItem.Size = new System.Drawing.Size(137, 22);
            this.informationToolStripMenuItem.Text = "Information";
            // 
            // TB_Station
            // 
            this.TB_Station.Name = "TB_Station";
            this.TB_Station.Size = new System.Drawing.Size(100, 23);
            this.TB_Station.Text = "Trạm";
            // 
            // TB_ID
            // 
            this.TB_ID.Name = "TB_ID";
            this.TB_ID.Size = new System.Drawing.Size(100, 23);
            this.TB_ID.Text = "ID:";
            this.TB_ID.TextChanged += new System.EventHandler(this.TB_ID_TextChanged);
            // 
            // TB_X
            // 
            this.TB_X.Name = "TB_X";
            this.TB_X.ReadOnly = true;
            this.TB_X.Size = new System.Drawing.Size(100, 23);
            this.TB_X.Text = "X:";
            // 
            // TB_Y
            // 
            this.TB_Y.Name = "TB_Y";
            this.TB_Y.ReadOnly = true;
            this.TB_Y.Size = new System.Drawing.Size(100, 23);
            this.TB_Y.Text = "Y:";
            // 
            // TB_RUN
            // 
            this.TB_RUN.Name = "TB_RUN";
            this.TB_RUN.Size = new System.Drawing.Size(100, 23);
            this.TB_RUN.Text = "RUN:";
            this.TB_RUN.TextChanged += new System.EventHandler(this.TB_RUN_TextChanged);
            // 
            // TB_LR
            // 
            this.TB_LR.Name = "TB_LR";
            this.TB_LR.Size = new System.Drawing.Size(100, 23);
            this.TB_LR.Text = "LR:";
            this.TB_LR.TextChanged += new System.EventHandler(this.TB_LR_TextChanged);
            // 
            // TB_SPEED
            // 
            this.TB_SPEED.Name = "TB_SPEED";
            this.TB_SPEED.Size = new System.Drawing.Size(100, 23);
            this.TB_SPEED.Text = "SPEED:";
            this.TB_SPEED.TextChanged += new System.EventHandler(this.TB_SPEED_TextChanged);
            // 
            // TB_DIR
            // 
            this.TB_DIR.Name = "TB_DIR";
            this.TB_DIR.Size = new System.Drawing.Size(100, 23);
            this.TB_DIR.Text = "DIR:";
            this.TB_DIR.TextChanged += new System.EventHandler(this.TB_DIR_TextChanged);
            // 
            // TB_LIFT
            // 
            this.TB_LIFT.Name = "TB_LIFT";
            this.TB_LIFT.Size = new System.Drawing.Size(100, 23);
            this.TB_LIFT.Text = "LIFT:";
            this.TB_LIFT.TextChanged += new System.EventHandler(this.TB_LIFT_TextChanged);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(157, 6);
            // 
            // TB_Save
            // 
            this.TB_Save.Name = "TB_Save";
            this.TB_Save.Size = new System.Drawing.Size(160, 22);
            this.TB_Save.Text = "Save Setting";
            this.TB_Save.Click += new System.EventHandler(this.TB_Save_Click);
            // 
            // removeToolStripMenuItem
            // 
            this.removeToolStripMenuItem.Name = "removeToolStripMenuItem";
            this.removeToolStripMenuItem.Size = new System.Drawing.Size(137, 22);
            this.removeToolStripMenuItem.Text = "Remove";
            this.removeToolStripMenuItem.Click += new System.EventHandler(this.removeToolStripMenuItem_Click);
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.button3.BackgroundImage = global::Call_AGV.Properties.Resources.images__1_;
            this.button3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button3.Location = new System.Drawing.Point(449, 6);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(45, 44);
            this.button3.TabIndex = 3;
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(138, 26);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(101, 23);
            this.comboBox1.TabIndex = 4;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // button6
            // 
            this.button6.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.button6.BackgroundImage = global::Call_AGV.Properties.Resources.images__2_;
            this.button6.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button6.Location = new System.Drawing.Point(551, 6);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(45, 44);
            this.button6.TabIndex = 2;
            this.button6.UseVisualStyleBackColor = false;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // button7
            // 
            this.button7.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.button7.BackgroundImage = global::Call_AGV.Properties.Resources.remove_434_1082788;
            this.button7.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button7.Location = new System.Drawing.Point(296, 5);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(45, 44);
            this.button7.TabIndex = 2;
            this.button7.UseVisualStyleBackColor = false;
            this.button7.Click += new System.EventHandler(this.drop_btn);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(155, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Destination";
            // 
            // textBox2
            // 
            this.textBox2.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.textBox2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.textBox2.ForeColor = System.Drawing.Color.Lime;
            this.textBox2.Location = new System.Drawing.Point(602, 5);
            this.textBox2.Multiline = true;
            this.textBox2.Name = "textBox2";
            this.textBox2.ReadOnly = true;
            this.textBox2.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox2.Size = new System.Drawing.Size(192, 45);
            this.textBox2.TabIndex = 9;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(1263, 20);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(75, 15);
            this.label3.TabIndex = 11;
            this.label3.Text = "Editing Point";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(1263, 35);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(78, 15);
            this.label4.TabIndex = 11;
            this.label4.Text = "Editing Route";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(1263, 53);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(83, 15);
            this.label5.TabIndex = 11;
            this.label5.Text = "Editing Sation";
            // 
            // button9
            // 
            this.button9.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.button9.BackgroundImage = global::Call_AGV.Properties.Resources._1675914_200;
            this.button9.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button9.Location = new System.Drawing.Point(347, 6);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(45, 44);
            this.button9.TabIndex = 2;
            this.button9.UseVisualStyleBackColor = false;
            this.button9.Click += new System.EventHandler(this.button9_Click);
            // 
            // contextMenuStrip2
            // 
            this.contextMenuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Ten_Tram,
            this.Trang_Thai,
            this.Station_remove});
            this.contextMenuStrip2.Name = "contextMenuStrip2";
            this.contextMenuStrip2.Size = new System.Drawing.Size(161, 76);
            // 
            // Ten_Tram
            // 
            this.Ten_Tram.Name = "Ten_Tram";
            this.Ten_Tram.Size = new System.Drawing.Size(100, 23);
            this.Ten_Tram.TextChanged += new System.EventHandler(this.Ten_Tram_TextChanged);
            // 
            // Trang_Thai
            // 
            this.Trang_Thai.Name = "Trang_Thai";
            this.Trang_Thai.ReadOnly = true;
            this.Trang_Thai.Size = new System.Drawing.Size(100, 23);
            this.Trang_Thai.TextChanged += new System.EventHandler(this.Trang_Thai_TextChanged);
            // 
            // Station_remove
            // 
            this.Station_remove.Name = "Station_remove";
            this.Station_remove.Size = new System.Drawing.Size(160, 22);
            this.Station_remove.Text = "Remove Station";
            this.Station_remove.Click += new System.EventHandler(this.Station_remove_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(1263, 5);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(84, 15);
            this.label6.TabIndex = 11;
            this.label6.Text = "Editing Station";
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(812, 6);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(100, 23);
            this.textBox3.TabIndex = 12;
            // 
            // button8
            // 
            this.button8.Location = new System.Drawing.Point(826, 31);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(75, 23);
            this.button8.TabIndex = 13;
            this.button8.Text = "button8";
            this.button8.UseVisualStyleBackColor = true;
            this.button8.Click += new System.EventHandler(this.button8_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1370, 734);
            this.Controls.Add(this.button8);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button9);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button7);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.panel1);
            this.Name = "Form1";
            this.Text = "AGV Server";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.contextMenuStrip1.ResumeLayout(false);
            this.contextMenuStrip2.ResumeLayout(false);
            this.contextMenuStrip2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem informationToolStripMenuItem;
        private System.Windows.Forms.ToolStripTextBox TB_ID;
        private System.Windows.Forms.ToolStripTextBox TB_X;
        private System.Windows.Forms.ToolStripTextBox TB_Y;
        private System.Windows.Forms.ToolStripTextBox TB_RUN;
        private System.Windows.Forms.ToolStripTextBox TB_LR;
        private System.Windows.Forms.ToolStripTextBox TB_SPEED;
        private System.Windows.Forms.ToolStripTextBox TB_DIR;
        private System.Windows.Forms.ToolStripTextBox TB_LIFT;
        private System.Windows.Forms.ToolStripMenuItem removeToolStripMenuItem;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.ToolStripMenuItem TB_Save;
        private System.Windows.Forms.ToolStripTextBox TB_Station;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button button9;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip2;
        private System.Windows.Forms.ToolStripMenuItem Station_remove;
        private System.Windows.Forms.ToolStripTextBox Ten_Tram;
        private System.Windows.Forms.ToolStripTextBox Trang_Thai;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.Button button8;
    }
}

