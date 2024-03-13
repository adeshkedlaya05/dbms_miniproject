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
    public partial class Product_form : Form
    {
        public Product_form()
        {
            InitializeComponent();
        }

        private void Product_form_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Create an instance of the warehouse_dashboard form
            warehouse_dashboard warehouseForm = new warehouse_dashboard();

            // Show the warehouse_dashboard form
            warehouseForm.Show();

            // Close the current form (Product_form)
            this.Close();
        }
        private const string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\adesh\OneDrive\Documents\logindata.mdf;Integrated Security=True;Connect Timeout=30;Encrypt=False";
        private void button2_Click(object sender, EventArgs e)
        {

            // Create a form to input product details
            using (var form = new Form())

            {
                form.Text = "Enter Product Details";
                form.Width = 400;
                form.Height = 250;

                // Labels
                var productIdLabel = new Label();
                productIdLabel.Text = "Product ID:";
                productIdLabel.Location = new System.Drawing.Point(10, 10);
                form.Controls.Add(productIdLabel);

                var productNameLabel = new Label();
                productNameLabel.Text = "Product Name:";
                productNameLabel.Location = new System.Drawing.Point(10, 40);
                form.Controls.Add(productNameLabel);

                var categoryLabel = new Label();
                categoryLabel.Text = "Category:";
                categoryLabel.Location = new System.Drawing.Point(10, 70);
                form.Controls.Add(categoryLabel);

                var descriptionLabel = new Label();
                descriptionLabel.Text = "Description:";
                descriptionLabel.Location = new System.Drawing.Point(10, 100);
                form.Controls.Add(descriptionLabel);

                // Textboxes
                var productIdTextBox = new TextBox();
                productIdTextBox.Location = new System.Drawing.Point(120, 10);
                form.Controls.Add(productIdTextBox);

                var productNameTextBox = new TextBox();
                productNameTextBox.Location = new System.Drawing.Point(120, 40);
                form.Controls.Add(productNameTextBox);

                var categoryTextBox = new TextBox();
                categoryTextBox.Location = new System.Drawing.Point(120, 70);
                form.Controls.Add(categoryTextBox);

                var descriptionTextBox = new TextBox();
                descriptionTextBox.Location = new System.Drawing.Point(120, 100);
                descriptionTextBox.Multiline = true;
                descriptionTextBox.Height = 50;
                descriptionTextBox.Width = 100;
                form.Controls.Add(descriptionTextBox);

                // Submit button
                var submitButton = new Button();
                submitButton.Text = "Submit";
                submitButton.DialogResult = DialogResult.OK;
                submitButton.Location = new System.Drawing.Point(120, 150);
                form.Controls.Add(submitButton);

                // Submit button click event handler
                submitButton.Click += (s, ev) =>
                {
                    // Extract data from textboxes
                    string productId = productIdTextBox.Text;
                    string productName = productNameTextBox.Text;
                    string category = categoryTextBox.Text;
                    string description = descriptionTextBox.Text;

                    // SQL command to insert data into the product table
                    string insertQuery = $"INSERT INTO product (product_id, product_name, category, description) VALUES ('{productId}', '{productName}', '{category}', '{description}')";

                    // Establish connection and execute SQL command
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        try
                        {
                            connection.Open(); // Open the connection
                            SqlCommand command = new SqlCommand(insertQuery, connection);
                            int rowsAffected = command.ExecuteNonQuery(); // Execute the SQL command
                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Data inserted successfully!");
                            }
                            else
                            {
                                MessageBox.Show("Failed to insert data!");
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Error: {ex.Message}");
                        }
                    }
                };

                // Discard button
                var discardButton = new Button();
                discardButton.Text = "Discard";
                discardButton.DialogResult = DialogResult.Cancel;
                discardButton.Location = new System.Drawing.Point(230, 150);
                form.Controls.Add(discardButton);

                // Show the form as a dialog box
                form.ShowDialog();
            }
        }


        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        // Declare DataGridView and DataTable globally
        DataGridView dataGridView;
        DataTable dataTable;

        private void panel4_Paint(object sender, PaintEventArgs e)
        {
            // Create a DataGridView
            dataGridView = new DataGridView();
            dataGridView.Dock = DockStyle.Fill;

            // Create a label to display row count
            Label rowCountLabel = new Label();
            rowCountLabel.Dock = DockStyle.Bottom;
            rowCountLabel.TextAlign = ContentAlignment.MiddleRight;

            try
            {
                // Retrieve data from the product table
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT * FROM product";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    dataTable = new DataTable();
                    adapter.Fill(dataTable);
                }

                // Populate DataGridView with data
                dataGridView.DataSource = dataTable;

                // Add delete button column
                DataGridViewButtonColumn deleteButtonColumn = new DataGridViewButtonColumn();
                deleteButtonColumn.HeaderText = "Delete";
                deleteButtonColumn.Text = "Delete";
                deleteButtonColumn.UseColumnTextForButtonValue = true;
                dataGridView.Columns.Add(deleteButtonColumn);

                // Handle delete button click event
                dataGridView.CellContentClick += (s, ev) =>
                {
                    if (dataGridView.Columns[ev.ColumnIndex] is DataGridViewButtonColumn && ev.RowIndex >= 0)
                    {
                        // Get the ID of the product to delete
                        int productIdToDelete = (int)dataGridView.Rows[ev.RowIndex].Cells["product_id"].Value;

                        // Remove the product from the database
                        try
                        {
                            using (SqlConnection connection = new SqlConnection(connectionString))
                            {
                                connection.Open();
                                string deleteQuery = $"DELETE FROM product WHERE product_id = {productIdToDelete}";
                                SqlCommand command = new SqlCommand(deleteQuery, connection);
                                int rowsAffected = command.ExecuteNonQuery();
                                if (rowsAffected > 0)
                                {
                                    MessageBox.Show("Product deleted successfully!");
                                    // Remove the row from the DataGridView
                                    dataGridView.Rows.RemoveAt(ev.RowIndex);
                                    // Update row count label
                                    rowCountLabel.Text = $"Total Rows: {dataGridView.Rows.Count}";
                                }
                                else
                                {
                                    MessageBox.Show("Failed to delete product!");
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Error: {ex.Message}");
                        }
                    }
                };

                // Add DataGridView to the panel
                panel4.Controls.Add(dataGridView);

                // Set initial row count label text
                rowCountLabel.Text = $"Total Rows: {dataGridView.Rows.Count}";
                // Add row count label to the panel
                panel4.Controls.Add(rowCountLabel);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }

        }
        private void UpdateProductCountLabel()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT COUNT(*) FROM product";
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                int productCount = (int)command.ExecuteScalar();
                label3.Text = "Product Count: " + productCount.ToString();
            }
        }



        private void label3_Click(object sender, EventArgs e)
        {
            // Call the method to update the product count label
            UpdateProductCountLabel();
        }

    }
}
