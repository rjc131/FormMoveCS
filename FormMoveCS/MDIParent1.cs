using System;
using System.Windows.Forms;

namespace FormMoveCS
{
    public partial class MDIParent1 : Form
    {
        public MDIParent1(string roles)
        {
            InitializeComponent();
            label_Status.Text = roles;
        }

        private void MDIParent1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void dataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Work ss = new Work();
            if (ss.MdiParent == null)
            {
                ss.MdiParent = this;
                ss.Dock = DockStyle.Fill;
                ss.Show();
            }
            else
            {
                MessageBox.Show("Please close current window");
            }
        }

        private void adminStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (label_Status.Text != "Admin")
            {
                MessageBox.Show("You do not have permission to access this program");
            }
            else
            {
                AdminManager am = new AdminManager();
                if (am.MdiParent == null)
                {
                    am.MdiParent = this;
                    am.Dock = DockStyle.Fill;
                    am.Show();
                }

                else
                {
                    MessageBox.Show("Please close current window");
                }
            }
        }
    }
}

