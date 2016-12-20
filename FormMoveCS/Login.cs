using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace FormMoveCS
{
    public partial class Login : Form
    {
        int TogMove; int MValX; int MValY;
        public Login()
        {
            InitializeComponent();
        }


        #region Form Move:
        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            TogMove = 1; MValX = e.X; MValY = e.Y;
        }
        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            TogMove = 0;
        }
        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (TogMove == 1)
            {
                this.SetDesktopLocation(MousePosition.X - MValX, MousePosition.Y - MValY);
            }
        }
        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            TogMove = 1; MValX = e.X; MValY = e.Y;
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            TogMove = 0;
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (TogMove == 1)
            {
                this.SetDesktopLocation(MousePosition.X - MValX, MousePosition.Y - MValY);
            }
        }
        #endregion

        #region Close Application:
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void Login_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
        #endregion

        #region Submit for Login:
        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(@"Data Source=(localdb)\ProjectsV13; Initial Catalog=Database1;Integrated Security=True");
            SqlDataAdapter sda = new SqlDataAdapter
                ("Select Roles from Login Where UserName='" + textBox1.Text + "'and Password=  '"
                + textBox2.Text + "' ",con );
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if(dt.Rows.Count == 1)
            {
                MDIParent1 ss = new MDIParent1(dt.Rows[0][0].ToString());
                ss.Show();
                Hide();

                //((Form)this.MdiParent).Controls["label_Status"].Text = dt.Rows[0][0].ToString();
            }
            else
            {
                MessageBox.Show("Wrong user name or password!");
            }
        }
        #endregion

        #region Exit App:
        private void button2_Click(object sender, EventArgs e)
        {
            // To confirm yo want to close the window or program
            DialogResult confirm = MessageBox.Show("Are you sure you want to exit?", "Confirm", MessageBoxButtons.YesNo);

            if (confirm == DialogResult.Yes)

                Application.Exit();

            else if (confirm == DialogResult.No)
            {

            }         
        }
        #endregion

        #region Open Register Form:
        private void registerStripMenuItem_Click(object sender, EventArgs e)
        {
            Register ss = new Register();
            ss.Show();
            Hide();
        }
        #endregion

        #region About:
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox about = new AboutBox();
            about.Show();
        }
        #endregion

        #region OPen NewsLetter Form:
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewsLetter news = new NewsLetter();
            news.Show();
            Hide();
        }
        #endregion

        #region Password Character Show Password:
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (passwordCharCheckBox.Checked == true)
            {
                textBox2.PasswordChar = '\0';
            }
            else
            {
                textBox2.PasswordChar = '*';
            }
        }
        #endregion
    }
}

