using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TMA3A.part4
{
    
    public partial class partList : System.Web.UI.Page
    {
        /*
         * Loads the complete part table from database.
         */
        private DataTable getTable()
        {
            string sql = "SELECT * FROM Parts";
            DataTable dt = new DataTable();
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["tma3aConnectionString"].ConnectionString);
            SqlCommand cmd = new SqlCommand(sql, conn);

            try
            {
                conn.Open();
                SqlDataAdapter ad = new SqlDataAdapter(cmd);
                ad.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    conn.Close();
                    return dt;
                }
            }
            catch (SqlException ex)
            {
                System.Diagnostics.Debug.WriteLine("ERROR: " + ex.Message);
                Console.WriteLine(ex.Message);
            }
            finally
            {
                conn.Close();
            }

            return null;
        }

        private int getInt (string str)
        {
            int integer = 0;

            try
            {
                integer = Int32.Parse(str);
            }
            catch (FormatException)
            {
                Console.WriteLine($"Unable to parse '{str}'");
                integer = 0;
            }

            return integer;
        }

        /*
         * Gets current order or make a new order and save it in database.
         */
        private int getCurOder (int userId)
        {
            int curOrder = 0;

            if (Session != null)
            {
                if (Session["curOrder"] != null)
                {
                    curOrder = getInt(Session["curOrder"] as string);
                }
                else
                {
                    string sql = "INSERT INTO OrderTable(userID, Date) OUTPUT INSERTED.OrderNum VALUES (@userId, @date)";
                    SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["tma3aConnectionString"].ConnectionString);
                    SqlCommand cmd = new SqlCommand(sql, conn);

                    cmd.Parameters.AddWithValue("@userId", userId);
                    cmd.Parameters.AddWithValue("@date", DateTime.Now);

                    try
                    {
                        conn.Open();
                        curOrder = (int)cmd.ExecuteScalar();
                        
                        if (Session != null)
                        {
                            Session["curOrder"] = curOrder.ToString();
                            System.Diagnostics.Debug.WriteLine("Session['curOrder']: " + Session["curOrder"] as string);
                        }
                    }
                    catch (SqlException ex)
                    {
                        System.Diagnostics.Debug.WriteLine("ERROR: " + ex.Message);
                        Console.WriteLine(ex.Message);
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
            }

            return curOrder;
        }

        /*
         * Adds the part to the order item table. Only records the part unique id to reference
         * which item in the part table.
         */
        private void addToOrder (int curOrder, int partId)
        {
            string sql = "INSERT INTO OrderItems(OrderNum, PartID, CompID) OUTPUT INSERTED.OrderNum VALUES (@curOrder, @partID, NULL)";
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["tma3aConnectionString"].ConnectionString);
            SqlCommand cmd = new SqlCommand(sql, conn);

            cmd.Parameters.AddWithValue("@curOrder", curOrder);
            cmd.Parameters.AddWithValue("@partID", partId);

            try
            {
                conn.Open();
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                System.Diagnostics.Debug.WriteLine("ERROR: " + ex.Message);
                Console.WriteLine(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        /*
         * Displays all parts available to sell in a table (ID: partsTable)
         * 
         */
        private void displayPartList(DataTable partTable)
        {
            for (var i = 0; i < partTable.Rows.Count; i++)
            {
                //System.Diagnostics.Debug.WriteLine(i+": " + partTable.Rows[i].ToString());
                //System.Diagnostics.Debug.WriteLine(i + ": " + partTable.Rows[i]["Part ID"].ToString());

                TableRow row = new TableRow();
                TableCell imageCell = new TableCell();
                TableCell describeCell = new TableCell();
                TableCell priceCell = new TableCell();
                TableCell optionCell = new TableCell();
                Image componentImage = new Image();
                Button optButton = new Button();

                componentImage.ImageUrl = partTable.Rows[i]["Image"].ToString();
                componentImage.AlternateText = "Computer part";
                optButton.Text = "Add to Order";
                optButton.ID = "buyProd" + partTable.Rows[i]["Part ID"].ToString();
                optButton.Click += ProdBuy_Click;
                describeCell.Text = partTable.Rows[i]["Description"].ToString();
                priceCell.Text = "$"+partTable.Rows[i]["Price"].ToString();

                componentImage.ControlStyle.CssClass = "productImage";
                optButton.ControlStyle.CssClass = "buyProd";
                imageCell.ControlStyle.CssClass = "prodImage";
                describeCell.ControlStyle.CssClass = "prodDescript";
                priceCell.ControlStyle.CssClass = "prodPrice";
                optionCell.ControlStyle.CssClass = "prodOption";

                imageCell.Controls.Add(componentImage);
                optionCell.Controls.Add(optButton);
                row.Cells.Add(imageCell);
                row.Cells.Add(describeCell);
                row.Cells.Add(priceCell);
                row.Cells.Add(optionCell);
                partsTable.Rows.Add(row);
            }
        }

/************************************Event Functions***************************************/

        protected void Page_Load(object sender, EventArgs e)
        {
            DataTable partTable = getTable();
            if (partTable != null)
                displayPartList(partTable); //Displays all parts available from database
            partTable = null;

            if (Session != null)
            {
                //If user logged in, dispaly user's Name and other options
                if ((Session["userName"] != null))
                {
                    accountName.Text = Session["userName"] as string;
                    OrderListButton.Style["display"] = "block";
                    LogOutButton.Style["display"] = "block";
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

        protected void ProdBuy_Click(object sender, EventArgs e)
        {
            Button toBuy = sender as Button;
            string id = toBuy.ID;

            //The numerical portion of button id contains the id for the part in database
            if (String.Compare(id.Substring(0,7), "buyProd") == 0)
            {
                id = id.Replace(id.Substring(0, 7), "");
                System.Diagnostics.Debug.WriteLine("id: " + id);

                if ((Session != null) && (Session["userName"] != null))
                {
                    int partID = getInt(id);
                    int userId = getInt(Session["userID"] as string);
                    int curOrder = getCurOder(userId); //Get current order. If no current, make new order.

                    System.Diagnostics.Debug.WriteLine("userName: "+ Session["userName"] as string);
                    System.Diagnostics.Debug.WriteLine("partID: " + partID);
                    System.Diagnostics.Debug.WriteLine("userID: " + userId);
                    System.Diagnostics.Debug.WriteLine("curOrder: " + curOrder);

                    addToOrder(curOrder, partID);   //Add item to order table.
                    Response.Redirect("Cart.aspx");
                }
                else
                {
                    Response.Redirect("Login.aspx");
                }
            }
        }
    }
}