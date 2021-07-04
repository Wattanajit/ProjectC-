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
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }


        List<Bill> allbill = new List<Bill>();

        private MySqlConnection databaseConnection()
        {
            string connectionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=projectcafe;";
            MySqlConnection conn = new MySqlConnection(connectionString);
            return conn;
        }


        private void showCoffee()
        {
            MySqlConnection conn = databaseConnection();
            DataSet ds = new DataSet();

            conn.Open();

            MySqlCommand cmd;
            cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT * FROM information WHERE Type = \"กาแฟ\" ";

            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
            adapter.Fill(ds);

            conn.Close();

            dataDrink.DataSource = ds.Tables[0].DefaultView;
        }

        private void showTea()
        {
            MySqlConnection conn = databaseConnection();
            DataSet ds = new DataSet();

            conn.Open();

            MySqlCommand cmd;
            cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT * FROM information WHERE Type = \"ชา\" ";

            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
            adapter.Fill(ds);

            conn.Close();

            dataDrink.DataSource = ds.Tables[0].DefaultView;
        }

        private void showMilk()
        {
            MySqlConnection conn = databaseConnection();
            DataSet ds = new DataSet();

            conn.Open();

            MySqlCommand cmd;
            cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT * FROM information WHERE Type = \"นม\" ";

            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
            adapter.Fill(ds);

            conn.Close();

            dataDrink.DataSource = ds.Tables[0].DefaultView;
        }

        private void showSmoothie()
        {
            MySqlConnection conn = databaseConnection();
            DataSet ds = new DataSet();

            conn.Open();

            MySqlCommand cmd;
            cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT * FROM information WHERE Type = \"น้ำผลไม้ปั่น\" ";

            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
            adapter.Fill(ds);

            conn.Close();

            dataDrink.DataSource = ds.Tables[0].DefaultView;
        }

        private void showItaliansoda()
        {
            MySqlConnection conn = databaseConnection();
            DataSet ds = new DataSet();

            conn.Open();

            MySqlCommand cmd;
            cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT * FROM information WHERE Type = \"อิตาเลี่ยนโซดา\" ";

            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
            adapter.Fill(ds);

            conn.Close();

            dataDrink.DataSource = ds.Tables[0].DefaultView;
        }

        private void CoffeeToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            label2.Text = "เลือกเมนูกาแฟ";
            pictureBox1.Image = null;
            //textBox1.Text = String.Empty;
            //textBox2.Text = String.Empty;
            //textBox3.Text = String.Empty;
            //textBox3.Text = String.Empty;
            showCoffee();
        }

        private void TeaToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            label2.Text = "เลือกเมนูชา";
            pictureBox1.Image = null;
            //textBox1.Text = String.Empty;
            //textBox2.Text = String.Empty;
            //textBox3.Text = String.Empty;
            //textBox3.Text = String.Empty;
            showTea();
        }

        private void MilkToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            label2.Text = "เลือกเมนูนม";
            pictureBox1.Image = null;
            //textBox1.Text = String.Empty;
            //textBox2.Text = String.Empty;
            //textBox3.Text = String.Empty;
            //textBox3.Text = String.Empty;
            showMilk();
        }

        private void SmoothieToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            label2.Text = "เลือกเมนูน้ำผลไม้ปั่น";
            pictureBox1.Image = null;
            //textBox1.Text = String.Empty;
            //textBox2.Text = String.Empty;
            //textBox3.Text = String.Empty;
            //textBox3.Text = String.Empty;
            showSmoothie();
        }

        private void ItaliansodaToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            label2.Text = "เลือกเมนูอิตาเลี่ยนโซดา";
            pictureBox1.Image = null;
            //textBox1.Text = String.Empty;
            //textBox2.Text = String.Empty;
            //textBox3.Text = String.Empty;
            //textBox3.Text = String.Empty;
            showItaliansoda();
        }


        private void dataDrink_CellClick(object sender, DataGridViewCellEventArgs e)
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
        private void Main_Load(object sender, EventArgs e)
        {
            showinformation();
            showDrink2();
            showMoney();
        }

        private void showDrink2() // เรียกข้อมูลจาก DB saledata มาโชว์ใน กริดวิว2(กริดวิวเลือกสินค้า) 
        {
            MySqlConnection conn = databaseConnection();
            DataSet ds = new DataSet();
            conn.Open();
            MySqlCommand cmd;
            cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT ID,Drinklist,Price,Type FROM saledata WHERE Status = '" + "Not paid" + "'  ";
            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
            adapter.Fill(ds);
            conn.Close();
            dataDrink2.DataSource = ds.Tables[0].DefaultView;
        }

        private void showMoney() // แสดงราคาทั้งหมดที่เลือกมาจาก saledata แล้วอยู่ใน GVDrink2 แสดง ยอดรวม 
        {
            textBox1.Text = "0";
            MySqlConnection conn = databaseConnection();
            conn.Open();
            MySqlCommand cmd;
            cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT SUM(Price) FROM saledata WHERE Status = '" + "Not paid" + "'";
            Object sum = cmd.ExecuteScalar();
            conn.Close();
            if (Convert.ToString(sum) != "")
            {
                textBox1.Text = Convert.ToString(sum);
            }
        }


        private void dataDrink2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                dataDrink2.CurrentRow.Selected = true;
                NameText.Text = dataDrink2.Rows[e.RowIndex].Cells["Drinklist"].FormattedValue.ToString();
                PriceText.Text = dataDrink2.Rows[e.RowIndex].Cells["Price"].FormattedValue.ToString();
                TypeText.Text = dataDrink2.Rows[e.RowIndex].Cells["Type"].FormattedValue.ToString();
            }
            catch (Exception) { }
        }

        //public int sum = 0;
        private void button4_Click(object sender, EventArgs e) //สั่งเครื่อมดื่ม
        {
            MySqlConnection conn = databaseConnection();
            string sql = $"INSERT INTO saledata (Drinklist,Price,Type,Status,Username) VALUES(\"{NameText.Text}\",\"{PriceText.Text}\",\"{TypeText.Text}\",\"{"Not paid"}\",\"{Program.Username}\")";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            conn.Open();

            int row = cmd.ExecuteNonQuery();
            if (row>0)
            {
                //MessageBox.Show("เพิ่มรายการเรียบร้อยแล้ว");
                showDrink2();
                showMoney();
            }

        }

        private void button2_Click(object sender, EventArgs e) //คิดเงิน
        {
            int A = int.Parse(textBox1.Text); //รวมเงิน
            int B = int.Parse(textBox2.Text); //รับเงิน
            
            if (textBox1.Text == "0" || textBox1.Text == "" )
            {
                MessageBox.Show("กรุณาสั่งเครื่องดื่ม", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (textBox2.Text == "0" || textBox2.Text == "")
            {
                MessageBox.Show("กรุณาจ่ายเงินด้วยครับ", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                if (B < A)
                {
                    MessageBox.Show("กรุณาจ่ายเงินให้ครบด้วยครับ", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    int C = B - A;
                    textBox3.Text = C.ToString(); //เงินทอน

                    showDrink2();
                    allbill.Clear();
                    MySqlConnection conn2 = databaseConnection();
                    MySqlCommand cmd1 = new MySqlCommand($"SELECT * FROM saledata WHERE Username = \"{Program.Username}\"AND Status = '" + "Not Paid" + "'", conn2);
                    conn2.Open();
                    MySqlDataReader adapter = cmd1.ExecuteReader();
                    //Program.sum = 0;
                    while (adapter.Read())
                    {
                        Program.Drinklist = adapter.GetString("Drinklist").ToString();
                        Program.Price = adapter.GetString("Price").ToString();
                        Program.Type = adapter.GetString("Type").ToString();
                        Bill item = new Bill()
                        {
                            Drinklist = Program.Drinklist,
                            Price = Program.Price,
                            Type = Program.Type,
                        };
                        allbill.Add(item);
                    }
                    printPreviewDialog1.Document = printDocument1;
                    printPreviewDialog1.ShowDialog();

 
                    MySqlConnection conn = databaseConnection();
                    String sql = "UPDATE saledata SET Status = '" + "Paid" + "' ";
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    conn.Open();
                    int rows = cmd.ExecuteNonQuery();
                    conn.Close();

                    if (rows > 0)
                    {
                        MessageBox.Show("ขอบคุณที่ใช้บริการ");
                        showDrink2();
                    }

                }
            }  
        }



        private void button3_Click(object sender, EventArgs e) //ลบรายการ
        {
            if (textBox1.Text == "0")
            {
                MessageBox.Show("กรุณาสั่งเครื่องดื่ม");
            }
            else
            {
                int selectedRow = dataDrink2.CurrentCell.RowIndex;
                int deleteId = Convert.ToInt32(dataDrink2.Rows[selectedRow].Cells["ID"].Value);
                MySqlConnection conn = databaseConnection();
                String sql = "DELETE FROM saledata WHERE ID = '" + deleteId + "'";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                conn.Open();
                int rows = cmd.ExecuteNonQuery();
                conn.Close();
                if (rows > 0)
                {
                    MessageBox.Show("ลบข้อมูลสำเร็จ");
                    showDrink2();
                    showMoney();
                }
            }
        }


        private void button9_Click(object sender, EventArgs e) //ออกโปรแกรม
        {
            MySqlConnection conn = databaseConnection();
            String sql = "DELETE FROM saledata WHERE Status = '" + "Not paid" + "'";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            conn.Open();
            int rows = cmd.ExecuteNonQuery();
            conn.Close();
            if (rows > 0)
            {
                showDrink2();
                showMoney();
            }
            MessageBox.Show("ออกจากโปรแกรมสำเร็จ");
            Application.Exit();

        }

        private void HistoryBTN_Click(object sender, EventArgs e)
        {
            History a = new History();
            this.Hide();
            a.Show();
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e) //ใบเสร็จ
        {
            e.Graphics.DrawString("ใบเสร็จ", new Font("supermarket", 20, FontStyle.Bold), Brushes.Black, new Point(400, 50));
            e.Graphics.DrawString("Home Cafe", new Font("supermarket", 24, FontStyle.Bold), Brushes.Black, new Point(355, 90));
            e.Graphics.DrawString(" วัน   " + System.DateTime.Now.ToString("dd/MM/yyyy "), new Font("supermarket", 16, FontStyle.Regular), Brushes.Black, new PointF(520, 150));
            e.Graphics.DrawString("เวลา  " + System.DateTime.Now.ToString("HH : mm : ss น."), new Font("supermarket", 16, FontStyle.Regular), Brushes.Black, new PointF(520, 180));
            e.Graphics.DrawString("ข้อมูลร้าน : Home Cafe", new Font("supermarket", 18, FontStyle.Regular), Brushes.Black, new Point(80, 150));
            e.Graphics.DrawString("         บ้านเลขที่ 69 หมู่ ตำบลหนองนกเขียน  ", new Font("supermarket", 18, FontStyle.Regular), Brushes.Black, new Point(80, 195));
            e.Graphics.DrawString("         อำเภอศรีธาตุ จังหวัดอุดรธานี 41230", new Font("supermarket", 18, FontStyle.Regular), Brushes.Black, new Point(80, 230));
            e.Graphics.DrawString("-----------------------------------------------------------------------------------", new Font("supermarket", 16, FontStyle.Regular), Brushes.Black, new Point(80, 285));
            e.Graphics.DrawString("    ลำดับ       ชื่อเครื่อมดื่ม             ราคา (บาท)         ชนิดเครื่องดื่ม", new Font("supermarket", 18, FontStyle.Regular), Brushes.Black, new Point(80, 315));
            e.Graphics.DrawString("-----------------------------------------------------------------------------------", new Font("supermarket", 16, FontStyle.Regular), Brushes.Black, new Point(80, 345));
            int y = 345;
            int number = 1;
            foreach (var i in allbill)
            {
                y = y + 35;
                e.Graphics.DrawString("   " + number.ToString(), new Font("supermarket", 16, FontStyle.Regular), Brushes.Black, new PointF(100, y));
                e.Graphics.DrawString("   " + i.Drinklist, new Font("supermarket", 16, FontStyle.Regular), Brushes.Black, new PointF(190, y));// ชื่อเครื่อมดื่ม
                e.Graphics.DrawString("   " + i.Price, new Font("supermarket", 16, FontStyle.Regular), Brushes.Black, new PointF(420, y));//ราคา
                e.Graphics.DrawString("   " + i.Type, new Font("supermarket", 16, FontStyle.Regular), Brushes.Black, new PointF(575, y));//ประเภท
                number = number + 1;
            }
            e.Graphics.DrawString("-----------------------------------------------------------------------------------", new Font("supermarket", 16, FontStyle.Regular), Brushes.Black, new Point(80, y + 30));
            e.Graphics.DrawString("รวมทั้งสิ้น   " + textBox1.Text + "    บาท", new Font("supermarket", 16, FontStyle.Regular), Brushes.Black, new Point(490, (y + 30) + 45));
            e.Graphics.DrawString("ชื่อผู้ให้บริการ     " + Program.Username, new Font("supermarket", 16, FontStyle.Bold), Brushes.Black, new Point(80, (y + 30) + 45));
            e.Graphics.DrawString("รับเงิน        "+ textBox2.Text +"  บาท", new Font("supermarket", 16, FontStyle.Regular), Brushes.Black, new Point(490, ((y + 30) + 45) + 45));
            e.Graphics.DrawString("เงินทอน      " + textBox3.Text +"   บาท", new Font("supermarket", 16, FontStyle.Regular), Brushes.Black, new Point(490, (((y + 30) + 45) + 45) + 45));
        }

        private void adminToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Admin a = new Admin();
            this.Hide();
            a.Show();
        }
    }
}
