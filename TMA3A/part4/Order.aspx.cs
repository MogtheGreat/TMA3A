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
    public partial class Order : System.Web.UI.Page
    {
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
         * Gets the selected table from database with the parameter
         */
        private DataTable getSelectedTable (string sql, string param,  int id)
        {
            DataTable dt = new DataTable();
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["tma3aConnectionString"].ConnectionString);
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue(param, id);

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
         * Extract the part description from database using the id.
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
         * Retrieves the part id from computer database.
         * Uses the part ids to build the computer description.
         * 
         */
        private string getCompDescription(int compID)
        {
            string compDescribe = "";
            const string SEP = ", ";
            string sql = "SELECT * FROM Computer WHERE ID =@compID";
            DataTable comp = getSelectedTable(sql, "@compID", compID);

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
         * Gets a list of parts/computer that make up an order
         */
        private BulletedList getOrderList(int orderNum, BulletedList orderListBox)
        {
            string sql = "SELECT * FROM OrderItems WHERE OrderNum = @orderNum";
            DataTable orderTable = getSelectedTable(sql, "@orderNum", orderNum);

            foreach (DataRow row in orderTable.Rows)
            {
                string describe = "";
                if ((!String.IsNullOrEmpty(row["PartID"].ToString())) || (!String.IsNullOrEmpty(row["CompID"].ToString())))
                {
                    if (!String.IsNullOrEmpty(row["PartID"].ToString()))
                    {
                        int partID = getInt(row["PartID"].ToString());
                        describe = getPartDescription(partID);
                    }
                    else if (!String.IsNullOrEmpty(row["CompID"].ToString()))
                    {
                        int compID = getInt(row["CompID"].ToString());
                        describe = getCompDescription(compID);
                    }

                    ListItem listItem = new ListItem();
                    listItem.Text = describe;
                    orderListBox.Items.Add(listItem);
                }
            }

            orderTable = null;
            return orderListBox;
        }

        /*
         * Extract the computer price from database
         */
        private double getCompPrice(int compID)
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

            return price;
        }

        /*
         * Extract the part price from the database
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

            return partPrice;
        }

        /*
         * Calcualtes the total cost of the order (computer plus parts plus tax);
         */
        private double orderTotal (int orderNum)
        {
            double total = 0.0;
            string sql = "SELECT * FROM OrderItems WHERE OrderNum = @orderNum";
            DataTable order = getSelectedTable(sql, "@orderNum", orderNum);

            if (order != null)
            {
                foreach (DataRow orderRow in order.Rows)
                {
                    if ((!String.IsNullOrEmpty(orderRow["PartID"].ToString())) || (!String.IsNullOrEmpty(orderRow["CompID"].ToString())))
                    {
                        if (!String.IsNullOrEmpty(orderRow["PartID"].ToString()))
                        {
                            int partID = getInt(orderRow["PartID"].ToString());
                            total += getPartPrice(partID);
                        }
                        else if (!String.IsNullOrEmpty(orderRow["CompID"].ToString()))
                        {
                            int compID = getInt(orderRow["CompID"].ToString());
                            total += getCompPrice(compID);
                        }
                    }
                }
            }

            total = total + (total * 0.13);
            return total;
        }

        /*
         * Display all orders made by user.
         * Includes total price and list of items in order.
         */
        private void displayOrders (int userID)
        {
            string sql = "SELECT * FROM OrderTable WHERE userID = @userID";
            DataTable orderList = getSelectedTable(sql, "@userID", userID);

            if (orderList != null)
            {
                foreach (DataRow orderRow in orderList.Rows)
                {
                    TableRow orderEntry = new TableRow();
                    TableCell dateCell = new TableCell();
                    TableCell orderNumCell = new TableCell();
                    TableCell orderCell = new TableCell();
                    TableCell totalCell = new TableCell();
                    TableCell optCell = new TableCell();

                    BulletedList orderListBox = new BulletedList();
                    ImageButton removeButton = new ImageButton();
                    ImageButton editButton = new ImageButton();

                    int orderNum = getInt(orderRow["OrderNum"].ToString());
                    orderEntry.ID = "row_"+ orderNum;
                    dateCell.Text = orderRow["Date"].ToString();
                    orderNumCell.Text = orderNum.ToString();
                    totalCell.Text = "$" + orderTotal(orderNum).ToString("0.##");

                    dateCell.ControlStyle.CssClass = "DateCol";
                    orderNumCell.ControlStyle.CssClass = "OrderNumCol";
                    optCell.ControlStyle.CssClass = "OrderOptCol";
                    totalCell.ControlStyle.CssClass = "TotalCol";

                    orderListBox.BulletStyle = BulletStyle.Disc;
                    orderListBox.DisplayMode = BulletedListDisplayMode.Text;
                    orderListBox = getOrderList(orderNum, orderListBox);
                    orderCell.Controls.Add(orderListBox);

                    editButton.ID = "editOrder_" + orderNum;
                    editButton.Click += EditButton_Click;
                    editButton.ImageUrl = "~/shared/icons/edit.png";
                    editButton.AlternateText = "View/Edit order";
                    editButton.ControlStyle.CssClass = "removeItemButton";
                    optCell.Controls.Add(editButton);

                    removeButton.ID = "removeOrder_" + orderNum;
                    removeButton.Click += RemoveItem_Click;
                    removeButton.ImageUrl = "~/shared/icons/bin.png";
                    removeButton.AlternateText = "Delete Order";
                    removeButton.ControlStyle.CssClass = "removeItemButton";
                    optCell.Controls.Add(removeButton);

                    orderEntry.Cells.Add(dateCell);
                    orderEntry.Cells.Add(orderNumCell);
                    orderEntry.Cells.Add(orderCell);
                    orderEntry.Cells.Add(totalCell);
                    orderEntry.Cells.Add(optCell);
                    orderManageTable.Rows.Add(orderEntry);
                }
            }
        }

        /*
         * Removes a row from the table (not database table)
         */
        private void removeRow (string rowId)
        {
            TableRow rowToRemove = (TableRow)orderManageTable.FindControl(rowId);
            if (rowToRemove != null)
            {
                orderManageTable.Rows.Remove(rowToRemove);
            }
        }

        /*
         * Clears all items from an order.
         */
        private void clearOrder(int curOrder)
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

        /************************Event Functions********************************************/
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
                
                if ((Session["userID"] != null))
                {
                    int userID = getInt(Session["userID"].ToString());
                    displayOrders(userID); //Display all the orders made by user
                    
                }

                if (Session["curOrder"] != null)
                {
                    OrderNum.Text = "Order #: " + Session["curOrder"].ToString();
                }
                else
                {
                    OrderNum.Text = "Order #: N/A";
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

        protected void EditButton_Click(object sender, ImageClickEventArgs e)
        {
            ImageButton toViewEdit = sender as ImageButton;
            string id = toViewEdit.ID;

            //The numerical portion of button id contains the id for the order in database
            if (String.Compare(id.Substring(0, 10), "editOrder_") == 0)
            {
                id = id.Replace(id.Substring(0, 10), "");
                int orderId = getInt(id);
                Session["curOrder"] = orderId;
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Order: " + orderId + " Loaded. Can now view/edit order.')", true);
                Response.Redirect("Cart.aspx");
            }
        }

        protected void RemoveItem_Click(object sender, EventArgs e)
        {
            ImageButton toRemove = sender as ImageButton;
            string id = toRemove.ID;

            //The numerical portion of button id contains the id for the order in database
            if (String.Compare(id.Substring(0, 12), "removeOrder_") == 0)
            {
                id = id.Replace(id.Substring(0, 12), "");

                if ((Session != null) && (Session["userName"] != null))
                {
                    int orderId = getInt(id);
                    int userID = getInt(Session["userID"].ToString());

                    if ((Session["curOrder"] != null ) && (String.Compare(id, Session["curOrder"].ToString())) == 0)
                    {
                        Session["curOrder"] = null;
                    }

                    deleteOrder(orderId); //Removes all items under order and deletes the order.
                    removeRow("row_"+orderId); // Removes row from table (not database table)
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Order: " + orderId + " deleted.')", true);
                }
                else
                {
                    Response.Redirect("Login.aspx");
                }
            }
        }
    }
}