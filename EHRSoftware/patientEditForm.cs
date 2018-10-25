using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;
using System.Drawing.Imaging;

namespace EHRSoftware
{
    public partial class patientEditForm : Form
    {
        public patientEditForm()
        {
            InitializeComponent();
        }

        // This method is called when the dataGridView needs to be repopulated
        // Typically called when a image is saved to the database, or the form loads
        public void populateImages()
        {
            // Refreshing the DataGrid, so it doesn't keep appending the newly populated values
            dataGridViewImages.Rows.Clear();
            dataGridViewImages.Refresh();

            // Connection String asscoaited with my Database
            string conString = "Data Source=DESKTOP-QOR8COA\\SQLEXPRESS;Initial Catalog=EHR_DB;Integrated Security=True";

            // SqlConnection object created, so we can open a connection, and call a command with the connection
            SqlConnection con = new SqlConnection(conString);

            // Open Connection
            con.Open();
            
            // Initialize a SQL command string
            string q;
            
            // Load the current patient's ID that you're viewing into "patID"
            int patID = Convert.ToInt32(lblDisplayID.Text);

            // Selecting everything from the specified patient's table from the database
            // The images, info, and dates are all loaded
            q = "SELECT * From patientNum" + patID;
            
            // Create command linked to the connected database
            SqlCommand cmd = new SqlCommand(q, con);

            try
            {
                // Execute command and store selected item into variable
                SqlDataReader dr = cmd.ExecuteReader();

                //Read through all the rows that were read in with the SQL command "q"
                while (dr.Read())
                {
                    // Create a new row and store its id into "rowId"
                    int rowId = dataGridViewImages.Rows.Add();

                    // Grab the new row using its ID
                    DataGridViewRow row = dataGridViewImages.Rows[rowId];

                    // Add the data to the dataGridView column by column, using the datareader's current row.
                    //DataReader "dr" is indexed by the name of the columns in the SQL database
                    row.Cells["ID"].Value = dr["ID"].ToString();
                    row.Cells["Info"].Value = dr["Info"].ToString();
                    row.Cells["Date"].Value = dr["Date"].ToString();
                }
            }
            catch (Exception ex)
            {
                //This just prevents error messages if there is nothing stored in the patient's table in the database
            }

        }

        //Update button will update the edited information
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            //Checking that fields are are filled in before trying to add patient
            if (!string.IsNullOrEmpty(txtAge.Text) && !string.IsNullOrEmpty(txtFirst.Text) && !string.IsNullOrEmpty(txtLast.Text) && !string.IsNullOrEmpty(txtFirst.Text) && !string.IsNullOrEmpty(cboAdmitted.SelectedItem.ToString()) && !string.IsNullOrEmpty(cboSection.SelectedItem.ToString()))
            {
                try
                {
                    // Connection String asscoaited with my Database
                    string conString = "Data Source=DESKTOP-QOR8COA\\SQLEXPRESS;Initial Catalog=EHR_DB;Integrated Security=True";

                    // SqlConnection object created, so we can open a connection, and call a command with the connection
                    SqlConnection con = new SqlConnection(conString);

                    // Open Connection
                    con.Open();

                    // Check if the connection is currently open, if so then proceed
                    if (con.State == System.Data.ConnectionState.Open)
                    {
                        // Setting q to have the SQL command to update the table with the entered info to edit
                        string q = "UPDATE PatientTable set FirstName= '" + txtFirst.Text + "' , LastName= '" + txtLast.Text + "' , Age= '" + Convert.ToInt32(txtAge.Text) + "' , Admitted= '" + cboAdmitted.SelectedItem.ToString() + "' , Section= '" + cboSection.SelectedItem.ToString() + "' WHERE ID= '" + lblDisplayID.Text + "'";

                        // Create command linked to the connected database
                        SqlCommand cmd = new SqlCommand(q, con);

                        // Execute command
                        cmd.ExecuteNonQuery();
                        con.Close();

                        // Also update the datagridview, so the user doesn't need to "repopulate" the table!
                        adminMenu master = (adminMenu)Application.OpenForms["adminMenu"];
                        master.populate();

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                // Error thrown if there is a blank field
                MessageBox.Show("Please Fill in all the values");
            }
        }
        
        public Image ConvertBinaryToImage(byte[] imgData)
        {
            // MessageBox.Show("yup");
            System.Drawing.ImageConverter converter = new System.Drawing.ImageConverter();
            Image img = (Image)converter.ConvertFrom(imgData);
            return img;
            
        }

        // Initialize strings to store the image location on machine, and store the file's name
        string imgLocation = "";
        string fileName;

        // When Open button is clicked, open a windows explorer to search machine for image to open
        public void btnOpen_Click(object sender, EventArgs e)
        {
            // Verifying that the opened file will be jpg
            using (OpenFileDialog ofd = new OpenFileDialog() { Filter = "JPEG|*.jpg", ValidateNames = true, Multiselect = false })
            {
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    fileName = ofd.FileName;
                    imgLocation = ofd.FileName.ToString();
                    pictureBox1.ImageLocation = imgLocation;
                }

            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image != null)
            {
                // Load the current patient's ID that you're viewing into "patID"
                int patID = Convert.ToInt32(lblDisplayID.Text);

                // Connection String asscoaited with my Database
                string conString = "Data Source=DESKTOP-QOR8COA\\SQLEXPRESS;Initial Catalog=EHR_DB;Integrated Security=True";

                // SqlConnection object created, so we can open a connection, and call a command with the connection
                SqlConnection con = new SqlConnection(conString);

                // Open Connection
                con.Open();

                // Initialize string q to have query to insert the open image
                string q;

                // Initialize a byte array to store the converted image's byte array, to be eligble to store into database
                byte[] data = null;

                // Initializing a file stream to open the image with the variables from the specified opened image
                FileStream FStream = new FileStream(imgLocation, FileMode.Open, FileAccess.Read);

                // Converting the FileStream "FStream" object into a byte array using a BinaryReader object
                BinaryReader brs = new BinaryReader(FStream);

                // Reading bytes from the BinaryReader, which is the FileStream's image
                data = brs.ReadBytes((int)FStream.Length);

                // Check if the connection is currently open, if so then proceed
                if (con.State == System.Data.ConnectionState.Open)
                {
                    // Setting q to be the SQL command to insert the images byte array, the info entered in the textbox, and the current date/timestamp
                    q = "INSERT INTO patientNum" + patID + " (Image, Info, Date) VALUES (@images, '" + txtInfo.Text.ToString() + "', CURRENT_TIMESTAMP)";

                    // Create command linked to the connected database
                    SqlCommand cmd1 = new SqlCommand(q, con);

                    // Adding the byte array into the paramater list properly
                    cmd1.Parameters.Add(new SqlParameter("@images", data));

                    // Execute Command
                    cmd1.ExecuteNonQuery();

                    // Close Connection
                    con.Close();
                    
                    // Repopulate the image's dataGridView
                    populateImages();

                    // Resetting the info textbox
                    txtInfo.Text = null;

                    // Reset picturebox to be empty
                    pictureBox1.Image = null;

                    MessageBox.Show("Successfully Saved Image!");

                }
            }
            else
            {
                MessageBox.Show("Please open a image to save.");
            }
        }

        public void loadImage()
        {
            //open connection to MovieKioskDB
            string conString = "Data Source=DESKTOP-QOR8COA\\SQLEXPRESS;Initial Catalog=EHR_DB;Integrated Security=True";

            // SqlConnection object created, so we can open a connection, and call a command with the connection
            SqlConnection con = new SqlConnection(conString);

            // Load the current patient's ID that you're viewing into "patID"
            int patID = Convert.ToInt32(lblDisplayID.Text);

            // Open Connection
            con.Open();

            // Initialize string "q1" to be SQL command to pull the specified image from the database and load it in a windows form
            string q1;

            // Initialize a byte array to store the image's byte array from the database
            byte[] var;

            // Convert the selected image's ID into an int32 value
            int imgID = Convert.ToInt32(this.dataGridViewImages.CurrentRow.Cells["ID"].Value);

            // Check if the connection is currently open, if so then proceed
            if (con.State == System.Data.ConnectionState.Open)
            {
                // Set "q1" to be the SQL SELECT command to access the current patient's table and load the specified image
                q1 = "SELECT Image FROM patientNum" + patID + " WHERE ID=" + imgID;

                // Create command linked to the connected database
                SqlCommand cmd2 = new SqlCommand(q1, con);

                // Create a data reader object "dr" to read in the selected data from the database
                SqlDataReader dr = cmd2.ExecuteReader();

                // Read in the selected row's column's information
                dr.Read();
                
                // Setting the byte array "var" to hold the byte array of the specified image
                var = (byte[])dr["Image"];

                // Creating a "newImg" Image object and converting from a byte array to a visible image
                Image newImg = ConvertBinaryToImage(var);

                // Create an instance of PhotoForm, which is just a new window to see the picture enlarged
                PhotoForm f5 = new PhotoForm();
                f5.imageBox.Image = newImg;
                f5.ShowDialog();

                // Close the data reader
                dr.Close();

                // Close the connection to the database
                con.Close();
            }
        }

        // When Load button is clicked, load the image
        private void btnLoad_Click(object sender, EventArgs e)
        {
            loadImage();
        }

        // When a row in the dataGridView is clicked, load that specified/clicked row's image
        private void dataGridViewImages_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            loadImage();
        }

        //When the patientEditForm loads, set the cursor to the Firstn Name textbox, and populate the image dataGridView
        private void patientEditForm_Load(object sender, EventArgs e)
        {
            this.ActiveControl = txtFirst;
            populateImages();
        }

        // If cancel button is clicked, close window
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
