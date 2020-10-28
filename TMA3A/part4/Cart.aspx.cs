using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TMA3A.part4
{
    public partial class Cart : System.Web.UI.Page
    {
        double totalPrice = 0.0;
        private int getInt(string str)
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
         * Gets the order contents from database.
         */
        private DataTable getOrder (int curOrder)
        {
            DataTable cart = new DataTable();
            string sql = "SELECT * FROM OrderItems WHERE OrderNum = @orderNum";
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["tma3aConnectionString"].ConnectionString);
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@orderNum", curOrder);

            try
            {
                conn.Open();
                SqlDataAdapter ad = new SqlDataAdapter(cmd);
                ad.Fill(cart);

                if (cart.Rows.Count > 0)
                {
                    conn.Close();
                    return cart;
                }
            }
            catch (SqlException ex)
            {
                System.Diagnostics.Debug.WriteLine("getOrder() ERROR: " + ex.Message);
                Console.WriteLine(ex.Message);
            }
            finally
            {
                conn.Close();
            }

            return null;
        }

        /*
         * Extracts the description of a part from database.
         */
        private string getPartDescription(int partID)
        {
            string partDescribe = "";
            string sql = "SELECT Description From Parts WHERE [Part ID] = @ID";
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["tma3aConnectionString"].ConnectionString);
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@ID", partID);

            try
            {
                conn.Open();
                object value = cmd.ExecuteScalar();

                if (value != null)
                    partDescribe = value.ToString();
            }
            catch (SqlException ex)
            {
                System.Diagnostics.Debug.WriteLine("getPartDescription() ERROR: " + ex.Message);
                Console.WriteLine(ex.Message);
            }
            finally
            {
                conn.Close();
            }

            return partDescribe;
        }

        /*
         * Extract the price of a part from database.
         */
        private double getPartPrice(int partID)
        {
            double partPrice = 0.00;
            string sql = "SELECT Price From Parts WHERE [Part ID] = @ID";
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["tma3aConnectionString"].ConnectionString);
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@ID", partID);

            try
            {
                conn.Open();
                object value = cmd.ExecuteScalar();

                if (value != null)
                    partPrice = float.Parse(value.ToString(), CultureInfo.InvariantCulture.NumberFormat);
            }
            catch (SqlException ex)
            {
                System.Diagnostics.Debug.WriteLine("getPartPrice() ERROR: " + ex.Message);
                Console.WriteLine(ex.Message);
            }
            finally
            {
                conn.Close();
            }

            totalPrice += partPrice;
            return partPrice;
        }

        /*
         * Gets a computer from database.
         */
        private DataTable getComp(int compID)
        {
            DataTable dt = new DataTable();
            string sql = "SELECT * FROM Computer WHERE ID =@compID";
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["tma3aConnectionString"].ConnectionString);
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@compID", compID);

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

        /*
         * Goes through all part reference in a computer and gets the description of each part.
         * Combines into one string to form the computer description.
         */
        private string getCompDescription(int compID)
        {
            string compDescribe = "";
            const string SEP = ", ";
            DataTable comp = getComp(compID);

            if (comp != null)
            {
                int cpu = getInt(comp.Rows[0]["CPU"].ToString());
                int ram = getInt(comp.Rows[0]["Ram"].ToString());
                int hdd = getInt(comp.Rows[0]["HardDrive"].ToString());
                int sound = getInt(comp.Rows[0]["Sound"].ToString());
                int os = getInt(comp.Rows[0]["OS"].ToString());
                int monitor = getInt(comp.Rows[0]["Display"].ToString());

                compDescribe += getPartDescription(cpu) + SEP;
                compDescribe += getPartDescription(ram) + SEP;
                compDescribe += getPartDescription(hdd) + SEP;
                compDescribe += getPartDescription(sound) + SEP;
                compDescribe += getPartDescription(os) + SEP;
                compDescribe += getPartDescription(monitor);

                comp = null;
            }

            return compDescribe;
        }

        /*
         * Gets price of computer from database
         */
        private double getCompPrice (int compID)
        {
            double price = 0.0;
            string sql = "SELECT Price From Computer WHERE [ID] = @ID";
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["tma3aConnectionString"].ConnectionString);
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@ID", compID);

            try
            {
                conn.Open();
                object value = cmd.ExecuteScalar();

                if (value != null)
                    price = float.Parse(value.ToString(), CultureInfo.InvariantCulture.NumberFormat);
            }
            catch (SqlException ex)
            {
                System.Diagnostics.Debug.WriteLine("getPartPrice() ERROR: " + ex.Message);
                Console.WriteLine(ex.Message);
            }
            finally
            {
                conn.Close();
            }

            totalPrice += price;
            return price;
        }

        /*
         * Display contents of current order to screen. All items displayed in table (id: CartTable)
         */
        private void displayCart (DataTable cart)
        {

            foreach (DataRow dataRow in cart.Rows)
            {
                if ((!String.IsNullOrEmpty(dataRow["PartID"].ToString())) || (!String.IsNullOrEmpty(dataRow["CompID"].ToString())))
                {
                    TableRow tableRow = new TableRow();
                    TableCell describeCell = new TableCell();
                    TableCell priceCell = new TableCell();
                    System.Web.UI.HtmlControls.HtmlGenericControl textDev = new System.Web.UI.HtmlControls.HtmlGenericControl("DIV");
                    System.Web.UI.HtmlControls.HtmlGenericControl buttonDev = new System.Web.UI.HtmlControls.HtmlGenericControl("DIV");
                    ImageButton removeButton = new ImageButton();

                    textDev.Attributes.Add("class", "productDescribeContainer");
                    buttonDev.Attributes.Add("class", "removeItemButtonContainer");
                    tableRow.ID = "row_" + getInt(dataRow["ID"].ToString());

                    if (!String.IsNullOrEmpty(dataRow["PartID"].ToString())) //dataRow["var"] !=null result in empty string and eval to true.
                    {
                        int partID = getInt(dataRow["PartID"].ToString());
                        textDev.InnerText = getPartDescription(partID); // Get description of part in database
                        priceCell.Text = "$"+ getPartPrice(partID).ToString("0.##");
                    }
                    else if (!String.IsNullOrEmpty(dataRow["CompID"].ToString())) //dataRow["var"] !=null result in empty string and eval to true.
                    {
                        int compID = getInt(dataRow["CompID"].ToString());
                        textDev.InnerText = getCompDescription(compID); // Get description of comp in database
                        priceCell.Text = "$" + getCompPrice(compID).ToString("0.##");
                    }

                    removeButton.ID = "orderItem_" + getInt(dataRow["ID"].ToString());
                    removeButton.ControlStyle.CssClass = "removeItemButton";
                    removeButton.ImageUrl = "~/shared/icons/bin.png";
                    removeButton.AlternateText = "remove item";
                    removeButton.Click += RemoveItem_Click;

                    buttonDev.Controls.Add(removeButton);
                    describeCell.Controls.Add(textDev);
                    describeCell.Controls.Add(buttonDev);
                    tableRow.Cells.Add(describeCell);
                    tableRow.Cells.Add(priceCell);
                    CartTable.Rows.Add(tableRow);
                }
            }

            CartSubTotal.Text = "$" + totalPrice.ToString("0.##");
            Tax.Text = "$" + (totalPrice * 0.13).ToString("0.##");
            Total.Text = "$" + (totalPrice + (totalPrice * 0.13)).ToString("0.##");
        }

        /*
         * Removes one item from order in database.
         */
        private void removeOrderItem (int id)
        {
            string sql = "DELETE FROM OrderItems WHERE ID= @orderNum";
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["tma3aConnectionString"].ConnectionString);
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@orderNum", id);

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
         * Removes any items attach to an order number. Does not delete order itself.
         */
        private void clearOrder (int curOrder)
        {
            string sql = "DELETE FROM OrderItems WHERE OrderNum= @curOrder";
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["tma3aConnectionString"].ConnectionString);
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@curOrder", curOrder);

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
         * Deletes all items in the order then deletes the order from database.
         */
        private void deleteOrder(int orderId)
        {
            clearOrder(orderId);
            string sql = "DELETE FROM OrderTable WHERE OrderNum= @orderID";
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["tma3aConnectionString"].ConnectionString);
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@orderID", orderId);

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

        /***********************************************Event Functions*****************************************************************/
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
                }
                
                if (Session["curOrder"] != null)
                {
                    OrderNumber.Text = "ORDER #:" + Session["curOrder"].ToString(); 
                    int curOrder = getInt(Session["curOrder"].ToString());
                    DataTable cart = getOrder(curOrder);  //Loads current order from database
                    if (cart != null)
                    {
                        displayCart(cart);//Display content of current order to screen
                        cart = null;
                    }
                }
                else
                {
                    OrderNumber.Text = "ORDER #: N/A";
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

        protected void Logout_Click(object sender, ImageClickEventArgs e)
        {
            Session["userName"] = null;
            Session["userID"] = null;
            Session["curOrder"] = null;
            Response.Redirect("Login.aspx");
        }

        protected void Order_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("Order.aspx");
        }

        protected void PayButton_Click(object sender, EventArgs e)
        {
            Session["curOrder"] = null;
            Response.Redirect("Cart.aspx");
        }

        protected void Cancel_Click(object sender, EventArgs e)
        {
            int curOrder = getInt(Session["curOrder"].ToString());
            deleteOrder(curOrder);//Remove all content from order and delete the actual order. 
            Session["curOrder"] = null;
            Response.Redirect("Cart.aspx");
        }

        protected void RemoveItem_Click(object sender, EventArgs e)
        {
            ImageButton toRemove = sender as ImageButton;
            string id = toRemove.ID;

            //Gets the numerical part from button id. USsed to retrieve part/comp id from order.
            if (String.Compare(id.Substring(0, 10), "orderItem_") == 0)
            {
                id = id.Replace(id.Substring(0, 10), "");
               
                if ((Session != null) && (Session["userName"] != null))
                {
                    int orderItemId = getInt(id);
                    removeOrderItem(orderItemId); // Removes from order in database
                    TableRow rowToRemove = (TableRow)CartTable.FindControl("row_" + orderItemId); //Updates table on screen
                    if (rowToRemove != null)
                        CartTable.Rows.Remove(rowToRemove);
                }
                else
                {
                    Response.Redirect("Login.aspx");
                }
            }
        }
    }
}