using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace ProjectHomeCafe1
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private MySqlConnection databaseConnection()
        {
            string connectionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=projectcafe;";
            MySqlConnection conn = new MySqlConnection(connectionString);
            return conn;
        }


        private void LoginBTN_Click(object sender, EventArgs e)
        {
            MySqlConnection conn = databaseConnection();
            conn.Open();
            MySqlCommand cmd;
            cmd = conn.CreateCommand();
            cmd.CommandText = $"SELECT * FROM userpass WHERE Username = \"{Username.Text}\" AND Password = \"{Password.Text}\"";

            MySqlDataReader row = cmd.ExecuteReader();
            if (row.HasRows)
            {
                //selectname();
                
                MessageBox.Show("เข้าสู่ระบบสำเร็จ");
                Program.Username = Username.Text;
                Main a = new Main();
                //a.Username.Text = label3.Text;
                this.Hide();
                a.Show();
            }
            else
            {
                MessageBox.Show("ชื่อผู้ใช้ หรือ รหัสผ่านไม่ถูกต้อง", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            conn.Close();
        }

        private void ExitBTN_Click(object sender, EventArgs e)
        {
            MessageBox.Show("ออกจากโปรแกรมสำเร็จ");
            Application.Exit();
        }

        private void RegisterBTN_Click(object sender, EventArgs e)
        {
            SignUp a = new SignUp();
            this.Hide();
            a.Show();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e) //show password
        {
            if (checkBox1.Checked)
            {
                string P = Password.Text;
                Password.PasswordChar = '\0';
            }
            else
            {
                Password.PasswordChar = '•';
            }
        }

    }
}
