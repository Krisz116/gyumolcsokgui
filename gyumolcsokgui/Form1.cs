
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

namespace gyumolcsokgui
{
    public partial class Form1 : Form
    {

        MySqlConnection conn = null;
        MySqlCommand cmd = null;
        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            MySqlConnectionStringBuilder builder = new MySqlConnectionStringBuilder();
            builder.Server = "localhost";
            builder.UserID = "root";
            builder.Password = "";
            builder.Database = "gyumolcsok";
            conn = new MySqlConnection(builder.ConnectionString);
            try
            {
                // terv szerint
                conn.Open();

                cmd = conn.CreateCommand();
            }
            catch (MySqlException ex)
            { // váratlan hiba

                MessageBox.Show(ex.Message + Environment.NewLine + "A program állni!");
                Environment.Exit(0);
            }
            finally
            {
                // hiba és tervs zerinti esetén is lefut
                conn.Close();
            }

            gyumolcsok_lista_updata();

        }

        private void gyumolcsok_lista_updata()
        {
            listBox_gyumolcsadat.Items.Clear();
            cmd.CommandText = "SELECT * FROM `gyumolcsok`;";
            conn.Open();
            using (MySqlDataReader dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                   gyumolcsok uj = new gyumolcsok(dr.GetInt32("id"), dr.GetString("nev"), dr.GetDouble("egysegar"), dr.GetInt32("menyiseg"));
                    listBox_gyumolcsadat.Items.Add(uj);
                }

            }
            conn.Close();
        }

        private void button_insert_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox_nev.Text))
            {
                MessageBox.Show("Adja meg nevet");
                textBox_nev.Focus();
                return;
            }
            if (string.IsNullOrEmpty(textBox_egysegar.Text))
            {
                MessageBox.Show("Nem adat meg egységárat!");
                textBox_egysegar.Focus();
                return;
            }
            if (numericUpDown_mennyiseg.Value <= 0)
            {
                MessageBox.Show("Adjon meg menyiséget!");
                numericUpDown_mennyiseg.Focus();
                return;
            }

            cmd.CommandText = "INSERT INTO `gyumolcsok` (`nev`, `egysegar`, `menyiseg`) VALUES (@nev, @egysegar, @menyiseg);";
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@nev", textBox_nev.Text);
            cmd.Parameters.AddWithValue("@egysegar", textBox_egysegar.Text);
            cmd.Parameters.AddWithValue("@menyiseg", numericUpDown_mennyiseg.Value.ToString());
            conn.Open();
            try
            {
                if (cmd.ExecuteNonQuery() == 1)
                {
                    MessageBox.Show("Sikeres rögzítéve!");
                    textBox_id.Text = "";
                    textBox_nev.Text = "";
                    textBox_egysegar.Text = "";
                    numericUpDown_mennyiseg.Value = numericUpDown_mennyiseg.Minimum;


                }
                else
                {
                    MessageBox.Show("Sikertelen rözítés!");
                }
            }
            catch (MySqlException ex)
            {

                MessageBox.Show(ex.Message);
            }
            conn.Close();
        }
        
    }
}
