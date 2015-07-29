using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Globalization;

namespace Timer
{
    public partial class Timer : Form
    {
        static int timeRemaing;
        static int labelTime;
        public Timer()
        {
            InitializeComponent();    
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            if ((nudMinutes.Value > 0) || (nudHour.Value > 0))
            {
                Decimal timeInterval = GetTimeBySeconds();
                timeRemaing = (int)timeInterval;                               
                timer1.Interval = 1000;
                timer1.Tick += new EventHandler(Timer_Tick);
                progressBar1.Maximum = timeRemaing;
                timer1.Start();
                labelTime = timeRemaing;
                SetComponentsVisible(false);
            }
        }

        Decimal GetTimeBySeconds()
        {
            Decimal hour = nudHour.Value * 60 * 60;
            Decimal minutes = nudMinutes.Value * 60;            
            return hour + minutes;
        }

        void SetComponentsVisible(bool value)
        {
            nudHour.Enabled = value;
            nudMinutes.Enabled = value;
            btnStart.Enabled = value;

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            SetComponentsVisible(true);
            progressBar1.Value = 0;
            timer1.Stop();
        }

        String SetTimeToLabel(int time)
        {
            TimeSpan timespan = TimeSpan.FromSeconds(time);

            //here backslash is must to tell that colon is
            //not the part of format, it just a character that we want in output
            return timespan.ToString(@"hh\:mm\:ss\:fff");
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (timeRemaing > progressBar1.Value)
            {
                progressBar1.Value++;                
                lbTime.Text = SetTimeToLabel(labelTime--);                
            }
            else
            {
                timer1.Stop();
                Process.Start("shutdown", "/s /t 0");
            }
        }
    }
}
