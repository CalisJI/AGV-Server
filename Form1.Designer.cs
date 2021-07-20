
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
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.button7 = new System.Windows.Forms.Button();
            this.button8 = new System.Windows.Forms.Button();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(2, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1190, 720);
            this.panel1.TabIndex = 0;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            this.panel1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseDown);
            this.panel1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseMove);
            this.panel1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseUp);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(1203, 11);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(120, 23);
            this.textBox1.TabIndex = 1;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(1329, 11);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(29, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "...";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(1203, 72);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(152, 48);
            this.button2.TabIndex = 2;
            this.button2.Text = "Create New";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(1203, 180);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(152, 48);
            this.button4.TabIndex = 2;
            this.button4.Text = "Edit Map";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(1203, 288);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(152, 48);
            this.button5.TabIndex = 2;
            this.button5.Text = "Download Map";
            this.button5.UseVisualStyleBackColor = true;
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
            this.button3.Location = new System.Drawing.Point(1203, 234);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(152, 48);
            this.button3.TabIndex = 3;
            this.button3.Text = "Make Route";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(1203, 43);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(152, 23);
            this.comboBox1.TabIndex = 4;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // button6
            // 
            this.button6.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.button6.Location = new System.Drawing.Point(1203, 342);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(152, 48);
            this.button6.TabIndex = 2;
            this.button6.Text = "Call";
            this.button6.UseVisualStyleBackColor = false;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // textBox2
            // 
            this.textBox2.BackColor = System.Drawing.SystemColors.InfoText;
            this.textBox2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.textBox2.ForeColor = System.Drawing.Color.Lime;
            this.textBox2.Location = new System.Drawing.Point(1203, 405);
            this.textBox2.Multiline = true;
            this.textBox2.Name = "textBox2";
            this.textBox2.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox2.Size = new System.Drawing.Size(150, 65);
            this.textBox2.TabIndex = 5;
            // 
            // button7
            // 
            this.button7.Location = new System.Drawing.Point(1203, 126);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(152, 48);
            this.button7.TabIndex = 2;
            this.button7.Text = "Delete Map";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.drop_btn);
            // 
            // button8
            // 
            this.button8.Location = new System.Drawing.Point(1237, 522);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(75, 23);
            this.button8.TabIndex = 6;
            this.button8.Text = "button8";
            this.button8.UseVisualStyleBackColor = true;
            this.button8.Click += new System.EventHandler(this.button8_Click);
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(1237, 493);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(100, 23);
            this.textBox3.TabIndex = 7;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1370, 734);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.button8);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button7);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.panel1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.contextMenuStrip1.ResumeLayout(false);
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
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.TextBox textBox3;
    }
}

