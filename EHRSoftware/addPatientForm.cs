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
using System.Text.RegularExpressions;

namespace EHRSoftware
{
    public partial class addPatientForm : Form
    {
        public addPatientForm()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            //Connection String asscoaited with my Database
            string conString = "Data Source=DESKTOP-QOR8COA\\SQLEXPRESS;Initial Catalog=EHR_DB;Integrated Security=True";
            
            //SqlConnection object created, so we can open a connection, and call a command with the connection
            SqlConnection con = new SqlConnection(conString);

            //Open the connection
            con.Open();

            // Initialize a sql command string
            string q;
            string q2;
            int i;

            // Checking if name textboxes only contain letters, and checking if age only has numbers
            if (!int.TryParse(txtAge.Text, out i) || !Regex.IsMatch(txtFirst.Text, @"^[a-zA-Z]+$") || !Regex.IsMatch(txtLast.Text, @"^[a-zA-Z]+$"))
            {
                MessageBox.Show("Make sure all fields are filled in, and make sure Age has only integer values, and Name fields only have text.");
                return;
            }
            else
            {
                // Checking that fields are are filled in before trying to add patient
                if (!string.IsNullOrEmpty(txtAge.Text) && !string.IsNullOrEmpty(txtFirst.Text) && !string.IsNullOrEmpty(txtLast.Text) && !string.IsNullOrEmpty(txtFirst.Text) && (cboAdmitted.SelectedItem != null) && (cboSection.SelectedItem != null))
                {
                    // Setting first query statement "q" to have the inputs from the fields within the form
                    q = "INSERT INTO PatientTable (FirstName, LastName, Age, Admitted, Section) OUTPUT INSERTED.ID VALUES ('" + txtFirst.Text + "', '" + txtLast.Text + "', " + Convert.ToInt32(txtAge.Text) + ", '" + cboAdmitted.SelectedItem.ToString() + "', '" + cboSection.SelectedItem.ToString() + "'); SELECT SCOPE_IDENTITY()";

                    // Create command linked to the connected database
                    SqlCommand cmd = new SqlCommand(q, con);

                    // Executing the command, and storing the newly incremented ID that was inserted, into "newId"
                    int newId = Convert.ToInt32(cmd.ExecuteScalar());
                    MessageBox.Show("Patient ID: " + newId.ToString());

                    // Update the datagridview, so the user doesn't need to "repopulate" the table!
                    adminMenu master = (adminMenu)Application.OpenForms["adminMenu"];
                    master.populate();

                    // SQL command to create a new table in the database for the added user, this will be used to store images, such as X-Rays, for that patient
                    q2 = "CREATE TABLE patientNum" + newId + " (ID int IDENTITY(1,1) PRIMARY KEY, Image image, Info nvarchar(50), Date datetime)";

                    // Create command linked to the connected database
                    SqlCommand cmd2 = new SqlCommand(q2, con);

                    // Execute command and store selected item into variable
                    cmd2.ExecuteNonQuery();

                    // Close Connection
                    con.Close();

                    //Close Form
                    this.Close();
                }
                else
                {
                    //Error thrown if there is a blank field
                    MessageBox.Show("Please Fill in all the values");
                }
            }
        }

        //If Cancel button is clicked, close window
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //Set cursor focus to the First Name Textbox when form loads
        private void addPatientForm_Load(object sender, EventArgs e)
        {
            this.ActiveControl = txtFirst;
        }
    }
}
