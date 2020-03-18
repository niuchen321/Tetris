using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tetris
{
    public partial class FormMessageBox : Form
    {
        public FormMessageBox(string text)
        {
            InitializeComponent();
            lblMessage.Text = text;
            //lblMessage.ForeColor = Color.Red;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
