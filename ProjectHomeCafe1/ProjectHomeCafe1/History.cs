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
    public partial class History : Form
    {

        public History()
        {
            InitializeComponent();
        }

        private MySqlConnection databaseConnection()
        {
            string connectionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=projectcafe;";
            MySqlConnection conn = new MySqlConnection(connectionString);
            return conn;
        }
        private void showhistory()
        {
            MySqlConnection conn = databaseConnection();
            DataSet ds = new DataSet();
            conn.Open();
            MySqlCommand cmd;
            cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT Drinklist,Price,Type,Status FROM saledata WHERE Username = '"+label1.Text+"' ";
            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
            adapter.Fill(ds);
            conn.Close();
            dataHistory.DataSource = ds.Tables[0].DefaultView;
        }

        private void History_Load(object sender, EventArgs e)
        {
            label1.Text = Program.Username;
            showhistory();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Main a = new Main();
            Program.Username = label1.Text;
            this.Hide();
            a.Show();
        }
    }
}
