using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace magnumOpus
{
    public partial class Form1 : Form
    {
        public PerformanceCounter myCounter;
        private NotifyIcon notifyIcon1;
        
        private bool mouseDown;
        private Point lastLocation;

        public Form1()
        {
            InitializeComponent();
            TopMost = true;
            notifyIcon1 = new NotifyIcon();
            notifyIcon1.Icon = new Icon("Cute-Ball-Windows-icon.ico");
            myCounter = new PerformanceCounter("PhysicalDisk", "% Idle Time", "1 D:");
            notifyIcon1.Visible = true;
            timer1.Start(); 
            MouseDown += Form1_MouseDown; 
            MouseMove += Form1_MouseMove;
            MouseUp += Form1_MouseUp;
        }

       
        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            mouseDown = true;
            lastLocation = e.Location;
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseDown)
            {
                this.Location = new Point(
                    (this.Location.X - lastLocation.X) + e.X, (this.Location.Y - lastLocation.Y) + e.Y);
                this.Update();
            }
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            mouseDown = false;
        }

        private void label1_Click(object sender, EventArgs e)
        {
            notifyIcon1.Icon = new Icon("Windows_Media_Center.ico"); 
        }

        private void mainHddEvent(object sender, EventArgs e)
        {
            int procent = (100 - (int)myCounter.NextValue());
            label1.Text =  procent.ToString() + '%';

            switch (procent)
            {
                case int p when p > 90:
                    notifyIcon1.Icon = new Icon("redHdd.ico");
                    pictureBox1.Image = Image.FromFile("redHdd.ico");
                    break;
                case int p when p > 50:
                    notifyIcon1.Icon = new Icon("Windows_Media_Center.ico");
                    break;
                default: notifyIcon1.Icon = new Icon("Cute-Ball-Windows-icon.ico");
                    break;
            }
        }

        private void Form1_DoubleClick(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            notifyIcon1.Visible = false;
            Close();
        }
    }
    
}
