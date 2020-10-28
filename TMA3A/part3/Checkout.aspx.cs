using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TMA3A.part3.computerParts;

namespace TMA3A.part3
{
    public partial class Checkout : System.Web.UI.Page
    {
        List<Components> partsCart = null;
        List<Computer> compCart = null;
        double cartTotal = 0.0;
        double cartTax = 0.0;

        /*
         * Goes through the parts listed in the Component cart display them in
         *  the table (id: CartTable). Each row contains the option to remove
         *  the entry.
         */
        private void addParts(List<Components> partsCart)
        {
            System.Diagnostics.Debug.WriteLine("addParts section");
            for (var i = 0; i < partsCart.Count; i++)
            {
                TableRow row = new TableRow();
                TableCell cell1 = new TableCell();
                TableCell cell2 = new TableCell();
                System.Web.UI.HtmlControls.HtmlGenericControl textDev = new System.Web.UI.HtmlControls.HtmlGenericControl("DIV");
                System.Web.UI.HtmlControls.HtmlGenericControl buttonDev = new System.Web.UI.HtmlControls.HtmlGenericControl("DIV");
                ImageButton removeButton = new ImageButton();

                textDev.Attributes.Add("class", "productDescribeContainer");
                buttonDev.Attributes.Add("class", "removeItemButtonContainer");
                textDev.InnerText = partsCart[i].ToString();
                cell2.Text = (partsCart[i].Price).ToString("0.##");
                cartTotal += partsCart[i].Price;

                removeButton.ID = "part_" + i.ToString();
                removeButton.ControlStyle.CssClass = "removeItemButton";
                removeButton.ImageUrl = "~/shared/icons/bin.png";
                removeButton.AlternateText = "remove item";
                removeButton.Click += RemoveItem_Click;

                buttonDev.Controls.Add(removeButton);
                cell1.Controls.Add(textDev);
                cell1.Controls.Add(buttonDev);
                row.Cells.Add(cell1);
                row.Cells.Add(cell2);
                CartTable.Rows.Add(row);
            }
        }

        /*
         * Goes through the parts listed in the computer cart display them in
         *  the table (id: CartTable). Each row contains the option to remove
         *  the entry.
         */
        private void addCompter(List<Computer> compCart)
        {
            System.Diagnostics.Debug.WriteLine("addComputer section");
            for (var i = 0; i < compCart.Count; i++)
            {
                TableRow row = new TableRow();
                TableCell cell1 = new TableCell();
                TableCell cell2 = new TableCell();
                System.Web.UI.HtmlControls.HtmlGenericControl textDev = new System.Web.UI.HtmlControls.HtmlGenericControl("DIV");
                System.Web.UI.HtmlControls.HtmlGenericControl buttonDev = new System.Web.UI.HtmlControls.HtmlGenericControl("DIV");
                ImageButton removeButton = new ImageButton();

                textDev.Attributes.Add("class", "productDescribeContainer");
                buttonDev.Attributes.Add("class", "removeItemButtonContainer");
                textDev.InnerText = compCart[i].ToString();
                cell2.Text = (compCart[i].TotalPrice()).ToString("0.##");
                cartTotal += compCart[i].TotalPrice();

                removeButton.ID = "comp_" + i.ToString();
                removeButton.ControlStyle.CssClass = "removeItemButton";
                removeButton.ImageUrl = "~/shared/icons/bin.png";
                removeButton.AlternateText = "remove item";
                removeButton.Click += RemoveItem_Click;

                buttonDev.Controls.Add(removeButton);
                cell1.Controls.Add(textDev);
                cell1.Controls.Add(buttonDev);
                row.Cells.Add(cell1);
                row.Cells.Add(cell2);
                CartTable.Rows.Add(row);
            }
        }

        /*
         * Display total cost before tax to screen
         */
        private void getCartSubTotal()
        {
            CartSubTotal.Text = "SubTotal: $" + (cartTotal).ToString("0.##");
        }

        /*
         * Display the tax
         */
        private void getTax()
        {
            cartTax = (cartTotal * 0.13);
            Tax.Text = "Tax: $" + (cartTax).ToString("0.##");
        }

        /*
         * SubTotal plus tax
         */
        private void getCartTotal()
        {
            Total.Text = "Total: $" + (cartTotal + cartTax).ToString("0.##");
        }

        /******************************Event Functions************************/
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session != null)
            {
                if (Session["cartList"] != null)
                {
                    partsCart = Session["cartList"] as List<Components>;
                    addParts(partsCart);
                }

                if (Session["compCartList"] != null)
                {
                    compCart = Session["compCartList"] as List<Computer>;
                    addCompter(compCart);
                }

                getCartSubTotal();
                getTax();
                getCartTotal();
            }
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

        protected void PayButton_Click(object sender, EventArgs e)
        {
            partsCart = null;
            Session["cartList"] = null;
            compCart = null;
            Session["compCartList"] = null;
            Response.Redirect("part3.aspx");
        }

        protected void Cancel_Click(object sender, EventArgs e)
        {
            partsCart = null;
            Session["cartList"] = null;
            compCart = null;
            Session["compCartList"] = null;
            Response.Redirect("part3.aspx");
        }

        protected void RemoveItem_Click(object sender, EventArgs e)
        {
            ImageButton removeButton = sender as ImageButton;
            string id = removeButton.ID;
            //System.Diagnostics.Debug.WriteLine(id.Substring(0, 5));

            //The numerical portion of the button's ID contains the component/Computer id which is used to remove said
            // component/computer from cart
            if (String.Compare(id.Substring(0, 5), "part_") == 0)
            {
                System.Diagnostics.Debug.WriteLine("part section");
                string hold = id.Replace(id.Substring(0, 5), "");
                //System.Diagnostics.Debug.WriteLine(hold);
                try
                {
                    int selection = Int32.Parse(hold);
                    partsCart.RemoveAt(selection);

                    if ((Session != null) && (Session["cartList"] != null))
                    {
                        Session["cartList"] = partsCart;
                        Response.Redirect("Checkout.aspx");
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine($"Unable to parse '{hold}'");
                }
                // System.Diagnostics.Debug.WriteLine(selection);
            }
            else if (String.Compare(id.Substring(0, 5), "comp_") == 0)
            {
                System.Diagnostics.Debug.WriteLine("comp section");
                string hold = id.Replace(id.Substring(0, 5), "");

                try
                {
                    int selection = Int32.Parse(hold);
                    compCart.RemoveAt(selection);

                    if ((Session != null) && (Session["compCartList"] != null))
                    {
                        Session["compCartList"] = compCart;
                        Response.Redirect("Checkout.aspx");
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine($"Unable to parse '{hold}'");
                }
            }
        }
    }
}