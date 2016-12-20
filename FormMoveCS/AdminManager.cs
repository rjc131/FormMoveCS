using System;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Data;
using System.IO;

namespace FormMoveCS
{
    public partial class AdminManager : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=(localdb)\ProjectsV13;Initial Catalog=Database1;Integrated Security=True");
        public AdminManager()
        {
            InitializeComponent();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        #region SQL Submit or Insert button
        private void submitButton_Click(object sender, EventArgs e)
        {
            byte[] imageBt = null;
            FileStream fStream = new FileStream(label_ImageLocation.Text, FileMode.Open, FileAccess.Read);
            BinaryReader br = new BinaryReader(fStream);
            imageBt = br.ReadBytes((int)fStream.Length);

            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = ("insert into Login (UserName, Password, Roles, Image) Values ('" + userNameTextBox.Text + "','" + passTextBox.Text + "','" + userTypeTextBox.Text + "',@IMG )");
            cmd.Parameters.Add(new SqlParameter("@IMG", imageBt));
            cmd.ExecuteNonQuery();
            con.Close();
            disp_data();
            MessageBox.Show("Successfully submitted account");
        }
        #endregion

        #region To display data to gridveiw
        public void disp_data()
        {
            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from Login ";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            userDataGridView.DataSource = dt;
            con.Close();
        }
        #endregion


        private void AdminManager_Load(object sender, EventArgs e)
        {
            userStatus_label.Text = MdiParent.Controls["label_Status"].Text;
            disp_data();
        }

        #region Update Account --- will add confirmation later
        private void updateButton_Click(object sender, EventArgs e)
        {
            if(NewUserTextBox.Text != null)
            {
                con.Open();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "update Login set UserName= '" + NewUserTextBox.Text + "'where UserName='" + userNameTextBox.Text + "'";
                cmd.ExecuteNonQuery();
                con.Close();
                disp_data();
            }
            if (userTypeTextBox.Text != null)
            {
                con.Open();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "update Login set Roles= '" + userTypeTextBox.Text + "'where UserName='" + userNameTextBox.Text + "'";
                //cmd.CommandText = "update Login set UserName= '" + userNameTextBox.Text + "'where UserName='" + userNameTextBox.Text + "'";
                //cmd.CommandText = "update Login set Password= '" + passTextBox.Text + "'where UserName='" + userNameTextBox.Text + "'";
                cmd.ExecuteNonQuery();
                con.Close();
                disp_data();               
            }
            //if (passTextBox.Text == "")
            //{
            //    MessageBox.Show("At this point you must also update the password");
            //}
            else if (passTextBox.Text != null)
            {
                con.Open();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                //cmd.CommandText = "update Login set Roles= '" + userTypeTextBox.Text + "'where UserName='" + userNameTextBox.Text + "'";
                //cmd.CommandText = "update Login set UserName= '" + userNameTextBox.Text + "'where UserName='" + userNameTextBox.Text + "'";
                cmd.CommandText = "update Login set Password= '" + passTextBox.Text + "'where UserName='" + userNameTextBox.Text + "'";
                cmd.ExecuteNonQuery();                               
                MessageBox.Show("Successfully submitted account");
            }            
            con.Close();
            disp_data();
            userNameTextBox.Text = "";
            userTypeTextBox.Text = "";
            passTextBox.Text = "";
            rePassTextBox.Text = "";
            NewUserTextBox.Text = "";
        }
        #endregion

        #region Delete Account with confirmation
        private void deleteButton_Click(object sender, EventArgs e)
        {
            DialogResult confirm = MessageBox.Show("Are you sure you want to delete account??", "Confirm", MessageBoxButtons.YesNo);

            if (confirm == DialogResult.Yes)
            {
                con.Open();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = ("delete from Login where UserName='" + userNameTextBox.Text + "'");
                cmd.ExecuteNonQuery();
                con.Close();
                disp_data();
                MessageBox.Show("Successfully deleted account");
            }

            else if (confirm == DialogResult.No)
            {

            }            
        }
        #endregion

        private void userDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //userNameTextBox.Text = userDataGridView
        }

        #region SQL Search Button
        private void searchButton_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from Login where UserName= '" + userNameTextBox.Text + "'";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            userDataGridView.DataSource = dt;
            con.Close();
        }
        #endregion

        private void userDataGridView_Click(object sender, EventArgs e)
        {
            if (userDataGridView.CurrentRow.Index != -1)
            {
                userNameTextBox.Text = userDataGridView.CurrentRow.Cells[0].Value.ToString();
                userTypeTextBox.Text = userDataGridView.CurrentRow.Cells[2].Value.ToString();
            }
        }

        #region Show Password:
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                passTextBox.PasswordChar = '\0';
                rePassTextBox.PasswordChar = '\0';
            }
            else
            {
                passTextBox.PasswordChar = '*';
                rePassTextBox.PasswordChar = '*';
            }
        }
        #endregion

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "JPG Files(*.jpg)|*.jpg|PNG Files(*.png)|*.png|All FIles(*.*)|*.*";
            ofd.Title = "Employee Picture";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                string picPath = ofd.FileName.ToString();
                label_ImageLocation.Text = picPath;
                pictureBox1.ImageLocation = picPath;
            }
        }
    }
}