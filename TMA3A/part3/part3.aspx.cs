using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TMA3A.part3.computerParts;

namespace TMA3A.part3
{
    public partial class part3 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void Unnamed1_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("part3.aspx");
        }

        protected void Unnamed3_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("Login.aspx");
        }

        protected void Unnamed2_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("Checkout.aspx");
        }
    }
}