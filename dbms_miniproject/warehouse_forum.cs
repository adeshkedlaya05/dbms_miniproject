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
    public partial class warehouse_forum : Form
    {
        public warehouse_forum()
        {
            InitializeComponent();
        }

        private void warehouse_forum_Load(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

            // Call the method to update the product count label
            UpdatewarehouseCountLabel();
        }
        private const string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\chand\OneDrive\Documents\loginData.mdf;Integrated Security=True;Connect Timeout=30";
        private void button2_Click(object sender, EventArgs e)
        {
            using (var form = new Form())

            {
                form.Text = "Enter Product Details";
                form.Width = 400;
                form.Height = 250;

                // Labels
                var warehouseIdLabel = new Label();
                warehouseIdLabel.Text = "WareHouse ID:";
                warehouseIdLabel.Location = new System.Drawing.Point(10, 10);
                form.Controls.Add(warehouseIdLabel);


                var LocationLabel = new Label();
                LocationLabel.Text = "Location:";
                LocationLabel.Location = new System.Drawing.Point(10, 40);
                form.Controls.Add(LocationLabel);

                // Textboxes
                var warehouseIdTextBox = new TextBox();
                warehouseIdTextBox.Location = new System.Drawing.Point(120, 10);
                form.Controls.Add(warehouseIdTextBox);



                var LocationTextBox = new TextBox();
                LocationTextBox.Location = new System.Drawing.Point(120, 40);
                LocationTextBox.Multiline = true;
                LocationTextBox.Height = 50;
                LocationTextBox.Width = 100;
                form.Controls.Add(LocationTextBox);

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
                    string warehouseId = warehouseIdTextBox.Text;
                    string Location = LocationTextBox.Text;

                    // SQL command to insert data into the product table
                    string insertQuery = $"INSERT INTO warehouse (warehouse_id, location) VALUES ('{warehouseId}', '{Location}')";

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
       

        private void button1_Click(object sender, EventArgs e)
        {
            // Create an instance of the warehouse_dashboard form
            warehouse_dashboard warehouseForm = new warehouse_dashboard();

            // Show the warehouse_dashboard form
            warehouseForm.Show();

            // Close the current form (warehouse_forum)
            this.Close();
        }
        // Declare DataGridView and DataTable globally
        DataGridView dataGridView;
        DataTable dataTable;

        private void panel2_Paint(object sender, PaintEventArgs e)
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
                    string query = "SELECT * FROM warehouse";
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
                        int warehouseIdToDelete = (int)dataGridView.Rows[ev.RowIndex].Cells["warehouse_id"].Value;

                        // Remove the product from the database
                        try
                        {
                            using (SqlConnection connection = new SqlConnection(connectionString))
                            {
                                connection.Open();
                                string deleteQuery = $"DELETE FROM warehouse WHERE warehouse_id = {warehouseIdToDelete}";
                                SqlCommand command = new SqlCommand(deleteQuery, connection);
                                int rowsAffected = command.ExecuteNonQuery();
                                if (rowsAffected > 0)
                                {
                                    MessageBox.Show("warehouse deleted successfully!");
                                    // Remove the row from the DataGridView
                                    dataGridView.Rows.RemoveAt(ev.RowIndex);
                                    // Update row count label
                                    rowCountLabel.Text = $"Total Rows: {dataGridView.Rows.Count}";
                                }
                                else
                                {
                                    MessageBox.Show("Failed to delete warehouse!");
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
                panel2.Controls.Add(dataGridView);

                // Set initial row count label text
                rowCountLabel.Text = $"Total Rows: {dataGridView.Rows.Count}";
                // Add row count label to the panel
                panel2.Controls.Add(rowCountLabel);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
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
                label3.Text = "warehouse Count: " + warehouseCount.ToString();
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}

