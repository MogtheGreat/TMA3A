using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TMA3A.part4
{
    public partial class SignUp : System.Web.UI.Page
    {
        /*
         * Check to see if passwords match.
         */
        private bool PswdMatch(string pswdOne, string pswdTwo)
        {
            if (String.Compare(pswdOne, pswdTwo) == 0)
            {
                return true;
            }
            return false;
        }

        /*
         * Checks for duplicate email. Only one email per account allowed.
         */
        private bool checkDuplicateEmail (string email)
        {
            string sql = "SELECT * FROM users WHERE Email = @email";
            DataTable dt = new DataTable();
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["tma3aConnectionString"].ConnectionString);
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@email", email);
              
            try
            {
                conn.Open();
                SqlDataAdapter ad = new SqlDataAdapter(cmd);
                ad.Fill(dt);
                System.Diagnostics.Debug.WriteLine("dt.Rows.Count: " + dt.Rows.Count);
                if (dt.Rows.Count == 1)
                {
                    conn.Close();
                    return true;
                }
                else
                {
                    conn.Close();
                    return false;
                }
                    
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + ex.Message + "')", true);
                return false;
            }
        }

        /*
         * Records a new user to database.
         */
        private void registerUser (string firstName, string lastName, string email, string pswd)
        {
            System.Diagnostics.Debug.WriteLine("Start of registerUser...");
            string sql = "INSERT INTO users (FirstName, LastName, Email, Password) VALUES (@first, @last, @email, @pswd)";
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["tma3aConnectionString"].ConnectionString);
            SqlCommand cmd = new SqlCommand(sql, conn);
            
            cmd.Parameters.AddWithValue("@first", firstName);
            cmd.Parameters.AddWithValue("@last", lastName);
            cmd.Parameters.AddWithValue("@email", email);
            cmd.Parameters.AddWithValue("@pswd", pswd);

            try
            {
                System.Diagnostics.Debug.WriteLine("Now entering try of registerUser...");
                conn.Open();
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                System.Diagnostics.Debug.WriteLine("ERROR: " + ex.Message);
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('"+ ex.Message + "')", true);
                Console.WriteLine(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }
/************************Event Functions***************************************************/

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session != null)
            {
                //If user logged in, dispaly user's Name and other options
                if ((Session["userName"] != null))
                {
                    accountName.Text = Session["userName"] as string;
                    OrderListButton.Style["display"] = "block";
                    LogOutButton.Style["display"] = "block";
                    Response.Redirect("part4.aspx");
                }
            }
        }

        protected void Logo_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("part4.aspx");
        }

        protected void CheckOut_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("Cart.aspx");
        }

        protected void Account_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("Login.aspx");
        }

        protected void Order_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("Order.aspx");
        }

        protected void Logout_Click(object sender, ImageClickEventArgs e)
        {
            Session["userName"] = null;
            Session["userID"] = null;
            Session["curOrder"] = null;
            Response.Redirect("Login.aspx");
        }

        protected void RegisterButton_Click(object sender, EventArgs e)
        {
            string pswdOne = signupConfirmPswd.Text;
            string pswdTwo = signupEnterPswd.Text;

            //Check to make sure enter password and confirm password match
            if (PswdMatch(pswdOne, pswdTwo))
            {
                string firstName = signupFirstName.Text;
                string lastName = signupLastName.Text;
                string email = signupEmail.Text;
                string pswd = pswdOne;

                //Limits on field based on: https://stackoverflow.com/questions/20958/list-of-standard-lengths-for-database-fields
                if (firstName.Length > 60)
                    firstName = firstName.Substring(0,60); //Limits firstname to 60 chars.
                if (lastName.Length > 60)
                    lastName = lastName.Substring(0, 60); //Limits lastName to 60 chars.
                if (email.Length > 100)
                    email = email.Substring(0, 100); //Limit email to 100 chars.
               
                //Only one email address per account
                if (checkDuplicateEmail(email) == false)
                {
                    System.Diagnostics.Debug.WriteLine("Now entering data...");
                    registerUser(firstName, lastName, email, pswd); //Records new user into database.
                    Response.Redirect("Login.aspx");
                }
                else
                {
                    Console.WriteLine("Email already taken!");
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Email already taken!')", true);
                }
                
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Confirmed Password does not match Entered Password.')", true);
            }
        }
    }
}