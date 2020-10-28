using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TMA3A.part1
{
    public partial class part1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string name = "partOneCookie";
            string ipAdd;
            HttpCookie cookie = null;
            System.Web.HttpContext context = System.Web.HttpContext.Current;


            //https://blog.mastykarz.nl/inconvenient-asp-net-cookies/ Oct. 7
            //Checks if the cookie is in response or request.
            if (HttpContext.Current.Response.Cookies.AllKeys.Contains (name))
            {
                cookie = HttpContext.Current.Response.Cookies[name];
                cookie["visit"] = ((Int32.Parse(cookie["visit"]))+1).ToString(); //increment visit. Can only store string
            }
            else if (HttpContext.Current.Request.Cookies[name] != null)
            {
                cookie = Request.Cookies[name];
                cookie["visit"] = ((Int32.Parse(cookie["visit"])) + 1).ToString(); //increment visit. Can only store string
            }
            else if (cookie == null)
            {
                //Create new cookie that expires seven days from creation
                cookie = new HttpCookie(name);
                cookie["visit"] = "1";
                cookie["Expire"] = "7 Days";
                cookie.Expires = DateTime.Now.AddDays(7);
            }
            Response.Cookies.Add(cookie);
            numberTimesViewed.Text = cookie["visit"];

            //Gets the address of the visitor
            ipAdd = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (ipAdd == "" || ipAdd == null)
                ipAdd = Request.ServerVariables["REMOTE_ADDR"];
            ipAddress.Text = ipAdd;
        }
    }
}