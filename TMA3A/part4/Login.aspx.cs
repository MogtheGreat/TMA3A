using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Configuration;
using System.Web.UI.WebControls;

namespace TMA3A.part4
{
    public partial class Login : System.Web.UI.Page
    {
        /*
         * Checks the database for the email. True if found. False otherwise.
         */
        private bool validEmail (string email)
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
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + ex.Message + "')", true);
            }
            finally
            {
                conn.Close();
            }

            return false;
        }

        /*
         * Gets the user password from database.
         */
        private string getPassword (string email)
        {
            string pswd = "";
            string sql = "SELECT Password FROM users WHERE Email = @email";
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["tma3aConnectionString"].ConnectionString);
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@email", email);

            try
            {
                conn.Open();
                object value = cmd.ExecuteScalar();

                if (value != null)
                    pswd = value.ToString();
            }
            catch (SqlException ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + ex.Message + "')", true);
            }
            finally
            {
                conn.Close();
            }

            return pswd;
        }

        /*
         * Gets the fisrst name of the user from database.
         */
        private string getFirst(string email)
        {
            string firstName = "";
            string sql = "SELECT FirstName FROM users WHERE Email = @email";
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["tma3aConnectionString"].ConnectionString);
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@email", email);

            try
            {
                conn.Open();
                object value = cmd.ExecuteScalar();

                if (value != null)
                    firstName = value.ToString();
            }
            catch (SqlException ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + ex.Message + "')", true);
            }
            finally
            {
                conn.Close();
            }

            return firstName;
        }

        /*
         * Gets the user id from database.
         */
        private string getID(string email)
        {
            string useId = "";
            string sql = "SELECT Id FROM users WHERE Email = @email";
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["tma3aConnectionString"].ConnectionString);
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@email", email);

            try
            {
                conn.Open();
                object value = cmd.ExecuteScalar();

                if (value != null)
                    useId = value.ToString();
            }
            catch (SqlException ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + ex.Message + "')", true);
            }
            finally
            {
                conn.Close();
            }

            return useId;
        }

        /*
         * Check to make sure the passwords match.
         */
        private bool PswdMatch(string pswdOne, string pswdTwo)
        {
            if (String.Compare(pswdOne, pswdTwo) == 0)
            {
                return true;
            }
            return false;
        }

        /************************Event Functions*************************************************************/
        protected void Page_Load(object sender, EventArgs e)
        {
            ValidationSettings.UnobtrusiveValidationMode = UnobtrusiveValidationMode.None; // To avoid needing jquery.
            if (Session != null)
            {
                //If user logged in, dispaly user's Name and other options. Redirect to main page.
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

        protected void loginButton_Click(object sender, EventArgs e)
        {
            string email = emailLoginBox.Text;
            string pswd = passwordBox.Text;

            if ((!String.IsNullOrEmpty(email)) && (!String.IsNullOrEmpty(pswd)))
            {
                string tablePswd = getPassword(email);

                if (PswdMatch(pswd, tablePswd) == true) 
                {
                    if (Session != null)
                    {
                        Session["userName"] = getFirst(email);
                        Session["userID"] = getID(email);
                        Response.Redirect("part4.aspx");
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Session terminated! Refresh Page!')", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Bad email/password combination')", true);
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please enter a email/password.')", true);
            }
        }

        protected void signupButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("SignUp.aspx");
        }

        protected void passwordRecover_Click (object sender, EventArgs e)
        {
            string email = pswdRecoverText.Text;
            
            if (!(String.IsNullOrEmpty(email)))
            {
                if (validEmail(email)) {
                    string pswd = getPassword(email);
                    //System.Diagnostics.Debug.WriteLine("pswd: " + pswd);
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + pswd + "')", true);
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please enter a valid email address.')", true);
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please enter a valid email address.')", true);
            }
        }
    }
}