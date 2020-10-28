using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using TMA3A.part3.computerParts;

namespace TMA3A.part3
{
    public partial class partList : System.Web.UI.Page
    {
        List<Components> cart = null;       //Records items being sold.

        /*
         * Fills the Table (ID: partsTable) with parts listed in masterList.
         * Each row numbered starting at 1.
         * CellOne represents part description in the leftmost cell. Named: curProd'i' where 'i' represents current row.
         * CellTwo represents part price in the rightMost cell. Named: price'i' where 'i' represents current row.
         */
        private void addDetails ()
        {
            string curProd = "prod";
            string curPrice = "price";

            for (var i = 0; i < 18; i++)
            {
                Control cellOne = FindControl((curProd + (i+1))); 
                Control cellTwo = FindControl((curPrice + (i+1)));
                ((TableCell)cellOne).Text = MasterList.prodList[i].ToString();
                ((TableCell)cellTwo).Text = "$" + (MasterList.prodList[i].Price).ToString("0.##");
            }
        }

        /*MasterList.prodList legend:
             0-2 ram
            3-5 cpu
            6-8 hardDrive
            9-11 sound card
            12-14 Operating System
            15-17 Display
        Using the id, gets the desired component from the master list and returns it.*/
        private Components getComponents (string id)
        {
            Components hold = null;
            switch (id)
            {
                case "buyProd1":    hold = MasterList.prodList[0];
                                    break;
                case "buyProd2":    hold = MasterList.prodList[1];
                                    break;
                case "buyProd3":    hold = MasterList.prodList[2];
                                    break;
                case "buyProd4":    hold = MasterList.prodList[3];
                                    break;

                case "buyProd5":    hold = MasterList.prodList[4];
                                    break;
                case "buyProd6":    hold = MasterList.prodList[5];
                                    break;
                case "buyProd7":    hold = MasterList.prodList[6];
                                    break;
                case "buyProd8":    hold = MasterList.prodList[7];
                                    break;

                case "buyProd9":    hold = MasterList.prodList[8];
                                    break;
                case "buyProd10":   hold = MasterList.prodList[9];
                                    break;
                case "buyProd11":   hold = MasterList.prodList[10];
                                    break;
                case "buyProd12":   hold = MasterList.prodList[11];
                                    break;

                case "buyProd13":   hold = MasterList.prodList[12];
                                    break;
                case "buyProd14":   hold = MasterList.prodList[13];
                                    break;
                case "buyProd15":   hold = MasterList.prodList[14];
                                    break;
                case "buyProd16":   hold = MasterList.prodList[15];
                                    break;

                case "buyProd17":   hold = MasterList.prodList[16];
                                    break;
                case "buyProd18":   hold = MasterList.prodList[17];
                                    break;
            }
            
            return hold;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            addDetails();
            
            if (Session != null)
            {
                if (Session["cartList"] != null)
                {
                    cart = Session["cartList"] as List<Components>; //Gets the current order.
                    System.Diagnostics.Debug.WriteLine("Part cart is " + cart.Count());
                }
                else
                {
                    cart = new List<Components>(); //Start new order and save it.
                    Session.Add("cartList", cart);
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

        protected void ProdBuy_Click(object sender, EventArgs e)
        {
            Button buyButton = sender as Button;
            string id = buyButton.ID;
            Components toBuy = null;
            //System.Diagnostics.Debug.WriteLine(id);
            toBuy = getComponents(id); //Gets the id of the part using the number in the button's id.

            if (toBuy != null)
            {
                if ((Session != null) && (cart != null))
                {
                    cart.Add(toBuy);
                    Session["cartList"] = cart;
                    Response.Redirect("Checkout.aspx");
                    //System.Diagnostics.Debug.WriteLine("Added to cart.");
                }
            }
        }
    }
}