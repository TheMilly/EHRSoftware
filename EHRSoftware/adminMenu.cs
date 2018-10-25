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

namespace EHRSoftware
{
    public partial class adminMenu : Form
    {

        public adminMenu()
        {
            InitializeComponent();
        }

        //This method is called when the dataGridView needs to be repopulated
        //Typically called when a filter is applied (checkbox), or form loads
        public void populate()
        {
            //Refreshing the DataGrid, so it doesn't keep appending the newly populated values
            dataGridView1.Rows.Clear();
            dataGridView1.Refresh();
            
            //This whereClause will be used to apply the checkbox filters to the SQL query string
            string whereClause = "";

            int count = 0;

            //Appending the whereClause appropriately (basically just adds a comma if there is more than 1 item)
            foreach (object itemChecked in checkedListBox1.CheckedItems)
            {
                if (count != 0)
                {
                    whereClause += ",'" + itemChecked.ToString() + "'";
                }
                else
                {
                    whereClause += "'" + itemChecked.ToString() + "'";
                }
                count += 1;
            }

            //Connection String asscoaited with my Database
            string conString = "Data Source=DESKTOP-QOR8COA\\SQLEXPRESS;Initial Catalog=EHR_DB;Integrated Security=True";
            
            //SqlConnection object created, so we can open a connection, and call a command with the connection
            SqlConnection con = new SqlConnection(conString);

            //Open the connection
            con.Open();

            //Initialize a SQL command string
            string q;

            //This if junction will add in the "whereClause" elements to apply "filters" to the SQL query statement
            if (count != 0 && chkAdmitted.Checked)
            {
                q = "SELECT * FROM PatientTable WHERE Section IN (" + whereClause + ") AND (Admitted = 'True')";
            }
            else if(count != 0 && !chkAdmitted.Checked)
            {
                q = "SELECT * FROM PatientTable WHERE Section IN (" + whereClause + ")";
            }
            else
            {
                q = "SELECT * From PatientTable";
            }

            // Create command linked to the connected database
            SqlCommand cmd = new SqlCommand(q, con);

            try
            {
                // Execute command and store selected item into variable
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    // Create a new row and store its id into "rowId"
                    int rowId = dataGridView1.Rows.Add();

                    // Grab the new row using its ID
                    DataGridViewRow row = dataGridView1.Rows[rowId];


                    // Add the data to the dataGridView column by column, using the datareader's current row.
                    //DataReader "dr" is indexed by the name of the columns in the SQL database
                    row.Cells["IDCol"].Value = dr["ID"].ToString();
                    row.Cells["firstNameCol"].Value = dr["FirstName"].ToString();
                    row.Cells["lastNameCol"].Value = dr["LastName"].ToString();
                    row.Cells["ageCol"].Value = dr["Age"].ToString();
                    row.Cells["admittedCol"].Value = dr["Admitted"].ToString();
                    row.Cells["secCol"].Value = dr["Section"].ToString();
                }
                //Close data reader "dr"
                dr.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Sorry Error");
            }

            //Close Connection
            con.Close();

        }

        //This method will create and instance of the PatientEditForm, populate it, and open it
        private void populateEditForm()
        {
            patientEditForm f2 = new patientEditForm();
            f2.lblDisplayID.Text = this.dataGridView1.CurrentRow.Cells["IDCol"].Value.ToString();
            f2.txtFirst.Text = this.dataGridView1.CurrentRow.Cells["firstNameCol"].Value.ToString();
            f2.txtLast.Text = this.dataGridView1.CurrentRow.Cells["lastNameCol"].Value.ToString();
            f2.txtAge.Text = this.dataGridView1.CurrentRow.Cells["ageCol"].Value.ToString();
            f2.cboAdmitted.SelectedItem = this.dataGridView1.CurrentRow.Cells["admittedCol"].Value.ToString();
            f2.cboSection.SelectedItem = this.dataGridView1.CurrentRow.Cells["secCol"].Value.ToString();
            f2.ShowDialog();
        }

        //When row is double clicked it will open the form and populate it
        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            populateEditForm();
        }

        //When the edit button it will open the form and populate it
        private void btnEdit_Click(object sender, EventArgs e)
        {
            populateEditForm();
        }

        //When checkboxes are checked, the table will repopulate automatically
        private void chkAdmitted_CheckedChanged(object sender, EventArgs e)
        {
            populate();
        }

        //When checkboxes are checked, the table will repopulate automatically
        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            populate();
        }

        private void adminMenu_Load(object sender, EventArgs e)
        {
            //When form is loaded, check all the checkboxes for sections, just to ensure table is loaded
            //with all present data in the PatientTable
            for (int i = 0; i < checkedListBox1.Items.Count; i++)
            {
                checkedListBox1.SetItemChecked(i, true);
            }

            //Hard coded the column headers to be silver, just for a cleaner visual appeal
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.Silver;
            dataGridView1.EnableHeadersVisualStyles = false;

            //When form is loaded, populate the table with the entire patient table in database
            //this is because all the checkboxes are checked by default when form loads.
            populate();
        }

        //When btnAdd is clicked, open the addPatientForm
        private void btnAdd_Click(object sender, EventArgs e)
        {
            addPatientForm f3 = new addPatientForm();
            f3.ShowDialog();
        }

        //Stop Application if the window is closed.
        private void adminMenu_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
