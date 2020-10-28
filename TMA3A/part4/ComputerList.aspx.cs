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
    public partial class ComputerList : System.Web.UI.Page
    {
        /*
         * Gets all the computers from database
         */
        private DataTable getTable()
        {
            DataTable dt = new DataTable();
            string sql = "SELECT * FROM Computer";
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["tma3aConnectionString"].ConnectionString);
            SqlCommand cmd = new SqlCommand(sql, conn);

            try
            {
                conn.Open();
                SqlDataAdapter ad = new SqlDataAdapter(cmd);
                ad.Fill(dt);
                
                if(dt.Rows.Count > 0)
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
         * Use the part id for each component in the computer to create the computer description
         */
        private string getCompDescription (int cpu, int ram, int hdd, int sound, int os, int monitor)
        {
            string describe = "";
            const string SEP = ", ";

            describe += getPartDescription(cpu) + SEP;
            describe += getPartDescription(ram) + SEP;
            describe += getPartDescription(hdd) + SEP;
            describe += getPartDescription(sound) + SEP;
            describe += getPartDescription(os) + SEP;
            describe += getPartDescription(monitor);

            return describe;
        }

        /*
         * Gets and display the first three computers from database to display. These are the prebuilt base computers. All others computers
         * in database are customer's custom computers.
         */
        private void displayCompList(DataTable computerTable)
        {
            for (var i = 0; ((i < 3) && (i < computerTable.Rows.Count)); i++)
            {
                TableRow row = new TableRow();
                TableCell imageCell = new TableCell();
                TableCell describeCell = new TableCell();
                TableCell priceCell = new TableCell();
                TableCell optionCell = new TableCell();

                Image computerImage = new Image();
                Button optOneButton = new Button();
                Button optTwoButton = new Button();
                System.Web.UI.HtmlControls.HtmlGenericControl buttonDev = new System.Web.UI.HtmlControls.HtmlGenericControl("DIV");

                //Get the part id for each component of the computer
                int cpu = getInt(computerTable.Rows[i]["CPU"].ToString()); 
                int ram = getInt(computerTable.Rows[i]["Ram"].ToString());
                int hdd = getInt(computerTable.Rows[i]["HardDrive"].ToString());
                int sound = getInt(computerTable.Rows[i]["Sound"].ToString());
                int os = getInt(computerTable.Rows[i]["OS"].ToString());
                int monitor = getInt(computerTable.Rows[i]["Display"].ToString());

                computerImage.ImageUrl = computerTable.Rows[i]["Image"].ToString();
                computerImage.AlternateText = "Computer";
                optOneButton.Text = "Quick Order";
                optOneButton.ID = "buyComp" + computerTable.Rows[i]["ID"].ToString();
                optOneButton.Click += CompBuy_Click;
              
                optTwoButton.Text = "Customize";
                optTwoButton.ID = "customizeComp" + computerTable.Rows[i]["ID"].ToString();
                optTwoButton.Click += CompCustom_Click;
                describeCell.Text = getCompDescription(cpu, ram, hdd, sound, os, monitor);
                priceCell.Text = "$" + computerTable.Rows[i]["Price"].ToString();

                computerImage.ControlStyle.CssClass = "productImage";
                optOneButton.ControlStyle.CssClass = "compListOpt";
                optTwoButton.ControlStyle.CssClass = "compListOpt";

                imageCell.ControlStyle.CssClass = "prodImage";
                describeCell.ControlStyle.CssClass = "prodDescript";
                priceCell.ControlStyle.CssClass = "prodPrice";
                optionCell.ControlStyle.CssClass = "prodOption";

                buttonDev.Controls.Add(optOneButton);
                imageCell.Controls.Add(computerImage);
                optionCell.Controls.Add(buttonDev);
                optionCell.Controls.Add(optTwoButton);
                row.Cells.Add(imageCell);
                row.Cells.Add(describeCell);
                row.Cells.Add(priceCell);
                row.Cells.Add(optionCell);
                compTable.Rows.Add(row);
            }
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
         * Adds the computer to the order item table using the computer's id.
         */
        private void addToOrder(int curOrder, int compID)
        {
            string sql = "INSERT INTO OrderItems(OrderNum, PartID, CompID) OUTPUT INSERTED.OrderNum VALUES (@curOrder, NULL, @compID)";
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["tma3aConnectionString"].ConnectionString);
            SqlCommand cmd = new SqlCommand(sql, conn);

            cmd.Parameters.AddWithValue("@curOrder", curOrder);
            cmd.Parameters.AddWithValue("@compID", compID);

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

        /************************************Event Functions***************************************/
        protected void Page_Load(object sender, EventArgs e)
        {
            DataTable compTable = getTable();
            if (compTable != null)
                displayCompList(compTable);
            compTable = null;

            if (Session != null)
            {
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

        protected void CompBuy_Click(object sender, EventArgs e)
        {
            Button toBuy = sender as Button;
            string id = toBuy.ID;

            //The numerical portion of button id contains the id for the computer in database
            if (String.Compare(id.Substring(0,7), "buyComp") == 0)
            {
                id = id.Replace(id.Substring(0, 7), "");

                if ((Session != null) && (Session["userName"] != null))
                {
                    int compID = getInt(id);
                    int userID = getInt(Session["userID"].ToString());
                    int curOrder = getCurOder(userID); //Get current order. If no current, make new order.
                     
                    addToOrder(curOrder, compID); //Add item to order table.
                    Response.Redirect("Cart.aspx");
                }
                else
                {
                    Response.Redirect("Login.aspx");
                }
            }
        }

        protected void CompCustom_Click(object sender, EventArgs e)
        {
            Button toCustom = sender as Button;
            string id = toCustom.ID;

            //The numerical portion of button id contains the id for the computer in database
            if (String.Compare(id.Substring(0, 13), "customizeComp") == 0)
            {
                id = id.Replace(id.Substring(0, 13), "");

                if ((Session != null) && (Session["userName"] != null))
                {
                    int compID = getInt(id); 
                    Session["customComp"] = compID; //Store the id of the computer for quick lookup
                    Response.Redirect("Customize.aspx");
                }
                else
                {
                    Response.Redirect("Login.aspx");
                }
            }
        }
    }
}