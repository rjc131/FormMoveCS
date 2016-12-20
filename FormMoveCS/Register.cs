using System;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;

namespace FormMoveCS
{
    public partial class Register : Form
    {
        int TogMove; int MValX; int MValY;
        public Register()
        {
            InitializeComponent();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        #region Form  Move:
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
          
        private void Register_MouseDown(object sender, MouseEventArgs e)
        {
            TogMove = 1; MValX = e.X; MValY = e.Y;
        }

        private void Register_MouseMove(object sender, MouseEventArgs e)
        {
            if (TogMove == 1)
            {
                this.SetDesktopLocation(MousePosition.X - MValX, MousePosition.Y - MValY);
            }
        }

        private void Register_MouseUp(object sender, MouseEventArgs e)
        {
            TogMove = 0;
        }
        #endregion

        private void CancelButton_Click(object sender, EventArgs e)
        {
            Login log = new Login();
            log.Show();
            Close();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox about = new AboutBox();
            about.Show();
        }

        #region Check Available Name:
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(@"Data Source=(localdb)\ProjectsV13; Initial Catalog=Database1;Integrated Security=True");
            SqlDataAdapter sda = new SqlDataAdapter
                ("Select count(*) from Login Where UserName ='" + textBox1.Text + "'", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if (int.Parse(dt.Rows[0][0].ToString()) == 0)
            {
                label1.Text = textBox1.Text + " Is Available";
                label1.ForeColor = Color.Green;
            }
            else
            {
                label1.Text = textBox1.Text + " Is not Available";
                label1.ForeColor = Color.Red;
            }
        }
        #endregion

        #region Submit to DataBase:
        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(@"Data Source=(localdb)\ProjectsV13; Initial Catalog=Database1;Integrated Security=True");
            con.Open();
            if (label1.ForeColor == Color.Green)
            {
                if (textBox2.Text == textBox3.Text)
                {
                    SqlCommand cmd = new SqlCommand("Insert into Login(UserName,Password) Values('" + textBox1.Text + "','" + textBox3.Text + "')", con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Successfully submitted new account");
                    con.Close();
                    Close();
                    Login ss = new Login();
                    ss.Show();
                }
                else
                {
                    MessageBox.Show("Passwords do not match");
                }
            }
            else
            {
                MessageBox.Show("Name not available");
            }
        }
        #endregion

        private void passwordCharCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (passwordCharCheckBox.Checked == true)
            {
                textBox2.PasswordChar = '\0';
                textBox3.PasswordChar = '\0';
            }
            else
            {
                textBox2.PasswordChar = '*';
                textBox3.PasswordChar = '*';
            }
        }
    }
}
