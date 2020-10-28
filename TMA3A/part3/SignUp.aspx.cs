using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TMA3A.part3
{
    public partial class SignUp : System.Web.UI.Page
    {
        private bool PswdMatch (string pswdOne, string pswdTwo)
        {
            if (String.Compare(pswdOne, pswdTwo) == 0)
            {
                return true;
            }
            return false;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            ValidationSettings.UnobtrusiveValidationMode = UnobtrusiveValidationMode.None; // To avoid needing jquery.
        }

        protected void Logo_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("part3.aspx");
        }

        protected void CheckOut_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("Checkout.aspx");
        }

        protected void Account_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("Login.aspx");
        }

        protected void RegisterButton_Click(object sender, EventArgs e)
        {
            string pswdOne = signupConfirmPswd.Text;
            string pswdTwo = signupEnterPswd.Text;
            
            //Check to make sure the Enter password and confirm password textbox match
            if (PswdMatch(pswdOne, pswdTwo))
            {
                Response.Redirect("part3.aspx");
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Confirmed Password does not match Entered Password.')", true);
            }
        }
    }
}