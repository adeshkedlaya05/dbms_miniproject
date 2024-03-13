using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;

namespace dbms_miniproject
{
    public partial class Signup_page : Form
    {
        SqlConnection connect = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\adesh\OneDrive\Documents\logindata.mdf;Integrated Security=True;Connect Timeout=30");
        public Signup_page()
        {
            InitializeComponent();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void Password_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {
            Form1 lform = new Form1();
            lform.Show();
            this.Hide();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(textBox1.Text == "" || textBox2.Text== "" || textBox3.Text== "")
            {
                MessageBox.Show("Please enter the details","Error Message",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
            else
            {
                if (connect.State != ConnectionState.Open)
                {
                    try
                    {
                        connect.Open();
                        string checkusername = "SELECT * FROM ADMIN WHERE username='" + textBox1.Text.Trim() + "';";
                        using (SqlCommand checkUser = new SqlCommand(checkusername, connect))
                        {
                            SqlDataAdapter adapter = new SqlDataAdapter(checkUser);
                            DataTable dt = new DataTable();
                            adapter.Fill(dt);

                            if (dt.Rows.Count >= 1)
                            {
                                MessageBox.Show(textBox1.Text + "is already exist", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            else
                            {
                                string insertdata = "INSERT INTO ADMIN (email, username, password, date_created) " +
                         "VALUES (@email, @username, @password, @date_created)";

                                DateTime date = DateTime.Today;
                                using (SqlCommand cmd = new SqlCommand(insertdata, connect))
                                {
                                    cmd.Parameters.AddWithValue("@email", textBox3.Text.Trim());
                                    cmd.Parameters.AddWithValue("@username", textBox1.Text.Trim());
                                    cmd.Parameters.AddWithValue("@password", textBox2.Text.Trim());
                                    cmd.Parameters.AddWithValue("@date_created", date);

                                    cmd.ExecuteNonQuery();
                                    MessageBox.Show("Registered successfully", "Information Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    Form1 lform = new Form1();
                                    lform.Show();
                                    this.Hide();
                                }
                            }

                        }

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("ERROR..!" + ex, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        connect.Close();
                    }
                }
            }
        }

           

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBox1 .Checked )
            {
                textBox2.PasswordChar = '\0';
            }
            else
            {
                textBox2.PasswordChar = '*';
            }
        }
    }
}
