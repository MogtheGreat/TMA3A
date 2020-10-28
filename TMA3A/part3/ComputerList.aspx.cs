using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TMA3A.part3.computerParts;

namespace TMA3A.part3
{
    public partial class ComputerList : System.Web.UI.Page
    {
        List<Computer> cart = null; //Records items being sold.

        /*
         * Fills the Table (ID: compTable) with computers listed in masterList.
         * Each row numbered starting at 1.
         * CellOne represents part description in the leftmost cell. Named: curProd'i' where 'i' represents current row.
         * CellTwo represents part price in the rightMost cell. Named: price'i' where 'i' represents current row.
         */
        private void addDetails ()
        {
            string describe = "CompDescript";
            string price = "CompPrice";

            for (var i = 0; i < 3; i++)
            {
                Control cellOne = FindControl(describe+(i+1));
                Control cellTwo = FindControl(price+(i+1));
                ((TableCell)cellOne).Text = MasterList.compList[i].ToString();
                ((TableCell)cellTwo).Text = "$" + (MasterList.compList[i].TotalPrice()).ToString("0.##");
            }
        }

        /*
         * Uses the Button id to get the computer reference from the master list.
         */
        private Computer getComputer(string id)
        {
            Computer newComp = null;
            switch (id)
            {
                case "buyComp1":
                case "customizeComp1":  newComp = MasterList.compList[0];
                                        break;
                case "buyComp2":
                case "customizeComp2":  newComp = MasterList.compList[1];
                                        break;
                case "buyComp3":
                case "customizeComp3":  newComp = MasterList.compList[2];
                                        break;
            }

            return newComp;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            //System.Diagnostics.Debug.WriteLine("Page load...");
            addDetails();

            if (Session != null)
            {
                if (Session["compCartList"] != null)
                {
                    cart = Session["compCartList"] as List<Computer>; //Loads current order
                    //System.Diagnostics.Debug.WriteLine("Computer cart is " + cart.Count());
                }
                else
                {
                    cart = new List<Computer>();
                    Session.Add("compCartList", cart); //Start new order
                }
            }
        }

        protected void Logo_Click(object sender, ImageClickEventArgs e)
        {
            //System.Diagnostics.Debug.WriteLine(cart.Count());
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

        protected void CompBuy_Click(object sender, EventArgs e)
        {
            Button buyButton = sender as Button;
            string id = buyButton.ID;
            Computer toBuy = null;

            toBuy = getComputer(id); //Gets computer from master list using the button's id. 
            if (toBuy != null)
            {
                if ((Session != null) && (cart != null))
                {
                    cart.Add(toBuy);
                    Session["compCartList"] = cart;
                    Response.Redirect("Checkout.aspx");
                    /*System.Diagnostics.Debug.WriteLine("Computer List not in session is " + cart.Count());
                    List<Computer> temp = null;
                    temp = Session["compCartList"] as List<Computer>;
                    System.Diagnostics.Debug.WriteLine("temp list in session is " + temp.Count()); */
                }
            }
        }

        protected void CompCustom_Click(object sender, EventArgs e)
        {
            Button customButton = sender as Button;
            string id = customButton.ID;
            Computer baseComp = null;
            Computer toCustom = null;

            baseComp = getComputer(id); //Gets computer from master list using the button's id. Computer will be used as base computer.
            toCustom = new Computer(baseComp.CPU, baseComp.Ram, baseComp.HardDrive, baseComp.SoundDrive, baseComp.OpSys, baseComp.Display);
            //Create new object since any modifications to reference will change all other reference. 

            if (toCustom != null)
            {
                if (Session != null)
                {
                    Session["customComp"] = toCustom;
                    Response.Redirect("Customize.aspx");
                }
            }
        }
    }
}