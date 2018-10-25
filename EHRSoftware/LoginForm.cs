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
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            this.ActiveControl = txtUsername;
        }

        private void LoginForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                //Connection String asscoaited with my Database
                string conString = "Data Source=DESKTOP-QOR8COA\\SQLEXPRESS;Initial Catalog=EHR_DB;Integrated Security=True";

                //SqlConnection object created, so we can open a connection, and call a command with the connection
                SqlConnection con = new SqlConnection(conString);

                //Open the connection
                con.Open();

                //check if the connection is currently open, if so then proceed
                if (con.State == System.Data.ConnectionState.Open)
                {

                    //Create a sql command string
                    string q = "SELECT Password FROM AdminTable WHERE Username LIKE '" + txtUsername.Text.ToString() + "'";

                    //create command linked to the connected database
                    SqlCommand cmd = new SqlCommand(q, con);

                    //execute command and store selected item into variable
                    SqlDataReader dr = cmd.ExecuteReader();

                    try
                    {
                        //Read the first row read in from the database (the entered username's row)
                        dr.Read();

                        //storing the password that matched the username, into variable "thePassword"
                        string thePassword = dr["Password"].ToString();
                        
                        //test entered password with one stored in database
                        if (thePassword == txtPassword.Text.ToString())
                        {
                            MessageBox.Show("You've entered the correct password!");
                            
                            //Hide the login window, and open the adminMenu window
                            this.Hide();
                            adminMenu f3 = new adminMenu();
                            f3.ShowDialog();
                        }
                        else
                        {
                            MessageBox.Show("Incorrect Password!");
                        }

                        //close Data Reader
                        dr.Close();

                        //close connection
                        con.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("No Such Username in Database.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
    }
}
