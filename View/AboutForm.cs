using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VG0DQ7
{
    public partial class AboutForm : Form
    {
        public AboutForm()
        {
            InitializeComponent();
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            txt_date.Text = DateTime.Now.ToShortDateString();
            txt_time.Text = DateTime.Now.ToLongTimeString();
        }

        private void btn_ok_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
