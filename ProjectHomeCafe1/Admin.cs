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
using System.IO;

namespace ProjectHomeCafe1
{
    public partial class Admin : Form
    {
        public Admin()
        {
            InitializeComponent();
        }

        private MySqlConnection databaseConnection()
        {
            string connectionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=projectcafe;";
            MySqlConnection conn = new MySqlConnection(connectionString);
            return conn;
        }

        private void backtomain_Click(object sender, EventArgs e)
        {
            Main a = new Main();
            this.Hide();
            a.Show();
        }


        private void showinformation()
        {
            MySqlConnection conn = databaseConnection();
            DataSet ds = new DataSet();
            conn.Open();
            MySqlCommand cmd;
            cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT * FROM information";
            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
            adapter.Fill(ds);
            conn.Close();
            dataDrink.DataSource = ds.Tables[0].DefaultView;
        }
        private void Admin_Load(object sender, EventArgs e)
        {
            showinformation();
        }

        private void dataDrink_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                dataDrink.CurrentRow.Selected = true;
                int selectedRows = dataDrink.CurrentCell.RowIndex;
                int Picture = Convert.ToInt32(dataDrink.Rows[selectedRows].Cells["ID"].Value);

                NameText.Text = dataDrink.Rows[e.RowIndex].Cells["Drinklist"].FormattedValue.ToString();
                PriceText.Text = dataDrink.Rows[e.RowIndex].Cells["Price"].FormattedValue.ToString();
                TypeText.Text = dataDrink.Rows[e.RowIndex].Cells["Type"].FormattedValue.ToString();

                MySqlConnection conn = databaseConnection();
                MySqlCommand cmd = conn.CreateCommand();
                conn.Open();
                cmd.CommandText = ($"SELECT Picture FROM information WHERE ID =\"{ Picture}\"");
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    MemoryStream ms = new MemoryStream((byte[])ds.Tables[0].Rows[0]["Picture"]);
                    pictureBox1.Image = new Bitmap(ms);
                }
            }
            catch (Exception) { }
        }

        private void button6_Click(object sender, EventArgs e) //เพิ่มข้อมูล
        {
            string connection = "datasource=127.0.0.1;port=3306;username=root;password=;database=projectcafe;";
            MySqlConnection conn = new MySqlConnection(connection);
            byte[] image = null;
            //pictureBox3.ImageLocation = textLocation.Text;
            string filepath = textBoxPic.Text;
            FileStream fs = new FileStream(filepath, FileMode.Open, FileAccess.Read);
            BinaryReader br = new BinaryReader(fs);
            image = br.ReadBytes((int)fs.Length);
            string sql = $" INSERT INTO information (Drinklist,Price,Type,Picture) VALUES(\"{ NameText.Text}\",\"{PriceText.Text}\",\"{ TypeText.Text}\",@Imgg)";
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.Add(new MySqlParameter("@Imgg", image));
                int x = cmd.ExecuteNonQuery();
                conn.Close();
                showinformation();
            }
            MessageBox.Show("เพิ่มข้อมูลสำเร็จ", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void button7_Click(object sender, EventArgs e) //แก้ไข
        {
            int selectedRows = dataDrink.CurrentCell.RowIndex;
            int editid = Convert.ToInt32(dataDrink.Rows[selectedRows].Cells["ID"].Value);
            MySqlConnection conn = databaseConnection();
            byte[] image = null;
            string filepath = textBoxPic.Text;
            FileStream fs = new FileStream(filepath, FileMode.Open, FileAccess.Read);
            BinaryReader br = new BinaryReader(fs);
            image = br.ReadBytes((int)fs.Length);
            String sql = "UPDATE  information SET Drinklist = '" + NameText.Text + "',Price = '" + PriceText.Text + "',Type ='" + TypeText.Text + "',Picture= @imgg WHERE ID = '" + editid + "'";
            conn.Open();
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.Add(new MySqlParameter("@Imgg", image));
            int rows = cmd.ExecuteNonQuery();
            conn.Close();
            if (rows > 0)
            {

                MessageBox.Show("ข้อมูลแก้ไขสำเร็จ", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                showinformation();
            }
        }

        private void button8_Click(object sender, EventArgs e) //เพิ่มรูปภาพ
        {
            OpenFileDialog open = new OpenFileDialog();
            // image filters  
            open.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp)|*.jpg; *.jpeg; *.gif; *.bmp";
            if (open.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Image = new Bitmap(open.FileName);
                textBoxPic.Text = open.FileName;
            }
        }

        private void button5_Click(object sender, EventArgs e) //ลบข้อมูล
        {
            int selectedRow = dataDrink.CurrentCell.RowIndex;
            int deleteId = Convert.ToInt32(dataDrink.Rows[selectedRow].Cells["id"].Value);
            string connection = "datasource=127.0.0.1;port=3306;username=root;password=;database=projectcafe;";
            MySqlConnection conn = new MySqlConnection(connection);
            String sql = "DELETE FROM information WHERE id = '" + deleteId + "'";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            conn.Open();
            int rows = cmd.ExecuteNonQuery();
            conn.Close();
            if (rows > 0)
            {
                MessageBox.Show("ข้อมูลถูกลบสำเร็จ", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                showinformation();
            }
        }
    }
}
