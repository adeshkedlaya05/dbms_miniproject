using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace dbms_miniproject
{
    public partial class warehouse_dashboard : Form
    {
        public warehouse_dashboard()
        {
            InitializeComponent();
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Create an instance of the Product_form
            Product_form productForm = new Product_form();

            // Show the Product_form
            productForm.Show();

            // Optionally, you can hide the current form if needed
            this.Hide();
        }

        private void warehouse_dashboard_Load(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            // Create an instance of the warehouse_forum form
            warehouse_forum warehouseForumForm = new warehouse_forum();

            // Show the warehouse_forum form
            warehouseForumForm.Show();

            this.Hide();
        }
        private const string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\adesh\OneDrive\Documents\logindata.mdf;Integrated Security=True;Connect Timeout=30;Encrypt=False";
        private void UpdateProductCountLabel()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT COUNT(*) FROM product";
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                int productCount = (int)command.ExecuteScalar();
                label5.Text = "Product Count: " + productCount.ToString();
            }
        }
        private void UpdatewarehouseCountLabel()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT COUNT(*) FROM warehouse";
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                int warehouseCount = (int)command.ExecuteScalar();
                label8.Text = "warehouse Count: " + warehouseCount.ToString();
            }
        }
        private void UpdatetransactionCountLabel()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT COUNT(*) FROM Transac";
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                int transactioncount = (int)command.ExecuteScalar();
                label10.Text = "Transaction " + transactioncount.ToString();
            }
        }
        private void UpdateinventoyCountLabel()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT COUNT(*) FROM inventory";
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                int inventorycount = (int)command.ExecuteScalar();
                label6.Text = "Inventories " + inventorycount.ToString();
            }
        }

        private void label5_Click(object sender, EventArgs e)
        {
            UpdateProductCountLabel();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {
            UpdatewarehouseCountLabel();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            // Create an instance of the warehouse_dashboard form
            Form1 Form = new Form1();

            // Show the warehouse_dashboard form
            Form.Show();

            // Close the current form (warehouse_forum)
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // Create an instance of the warehouse_dashboard form
           traansaction_form transForm = new traansaction_form();

            // Show the warehouse_dashboard form
            transForm.Show();

            // Close the current form (warehouse_forum)
            this.Close();
        }

        private void label10_Click(object sender, EventArgs e)
        {
            UpdatetransactionCountLabel();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Inventories_form inventory = new Inventories_form();
            inventory.Show();
            this.Close();
        }

        private void label6_Click(object sender, EventArgs e)
        {
            UpdateinventoyCountLabel();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
