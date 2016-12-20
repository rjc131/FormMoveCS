using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FormMoveCS
{
    public partial class Work : Form
    {
        public Work()
        {
            InitializeComponent();
        }
        string imageLocation = "";

        private void Work_Load(object sender, EventArgs e)
        {
            label_LoginAS.Text = MdiParent.Controls["label_Status"].Text;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog()==DialogResult.OK)
            {
                imageLocation = ofd.FileName.ToString();
                pictureBox1.ImageLocation = imageLocation;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            AboutBox ab = new AboutBox();
            ab.Show();
        }
    }
}
