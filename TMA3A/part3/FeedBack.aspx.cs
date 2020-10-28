using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TMA3A.part3
{
    public partial class FeedBack : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

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

        protected void SubmitFeedback_click(object sender, EventArgs e)
        {
            FeedBackBox.Text = "";
            Response.Write("<script>alert('Thank you for your feedback!')</script>");
        }
    }
}