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
using System.Xml.Linq;
using TMA3A.part3.computerParts;

namespace TMA3A.part4
{
    public partial class Customize : System.Web.UI.Page
    {
        static int cpu = 0;
        static int ram = 0;
        static int hdd = 0;
        static int sound = 0;
        static int os = 0;
        static int monitor = 0;
        static double totalPrice = 0.0;

        //Tables of available components for each part of computer
        static DataTable cpuTable = null;
        static DataTable RamTable = null;
        static DataTable hddTable = null;
        static DataTable soundTable = null;
        static DataTable osTable = null;
        static DataTable monitorTable = null;

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

            return partPrice;
        }

        /*
         * Displays the ram component to screen
         */
        private void displayRamChoice(int ram)
        {
            string ramDescribe = getPartDescription(ram);
            double ramPrice = getPartPrice(ram);

            RamSelected.Text = ramDescribe;
            RamSelectedPrice.Text = " $" + ramPrice.ToString("0.##");
        }

        /*
         * Displays the ram component to screen
         */
        private void displayRamChoice(string ramDescribe, double ramPrice)
        {
            RamSelected.Text = ramDescribe;
            RamSelectedPrice.Text = " $" + ramPrice.ToString("0.##");
        }

        /*
         * Goes through each component in the computer and display it to screen.
         * Complete computer description displayed at the top.
         * Each separate part description displayed below.
         */
        private void displayCustomParts (int cpu, int ram, int hdd, int sound, int os, int monitor)
        {
            string cpuDescribe = getPartDescription(cpu);
            string ramDescribe = getPartDescription(ram);
            string hddDescribe = getPartDescription(hdd);
            string soundDescribe = getPartDescription(sound);
            string osDescribe = getPartDescription(os);
            string monitorDescribe = getPartDescription(monitor);
            const string sep = ", ";

            double cpuPrice = getPartPrice(cpu);
            double ramPrice = getPartPrice(ram);
            double hddPrice = getPartPrice(hdd);
            double soundPrice = getPartPrice(sound);
            double osPrice = getPartPrice(os);
            double monitorPrice = getPartPrice(monitor);
            double total = cpuPrice + ramPrice + hddPrice + soundPrice + osPrice + monitorPrice;

            CustomComputer.Text = cpuDescribe + sep + ramDescribe + sep + hddDescribe + sep + soundDescribe + sep + osDescribe + sep + monitorDescribe;
            CustomerComputerPrice.Text = "$" + total.ToString("0.##");
            displayRamChoice(ramDescribe, ramPrice); //Displays the ram component to screen
            CpuSelected.Text = cpuDescribe;
            CpuSelectedPrice.Text = "$" + cpuPrice.ToString("0.##");

            HDDSelected.Text = hddDescribe;
            HDDSelectedPrice.Text = "$" + hddPrice.ToString("0.##");
            SoundSelected.Text = soundDescribe;
            SoundSelectedPrice.Text = "$" + soundPrice.ToString("0.##");

            OsSelected.Text = osDescribe;
            OsSelectedPrice.Text = "$" + osPrice.ToString("0.##");
            MonitorSelected.Text = monitorDescribe;
            MonitorSelectedPrice.Text = "$" + monitorPrice.ToString("0.##");

            totalPrice = total;
        }

        /*
         * Gets the each part id for looking up on the database.
         */
        private void getPartList(DataTable dt)
        {
            cpu = getInt(dt.Rows[0]["CPU"].ToString());
            ram = getInt(dt.Rows[0]["Ram"].ToString());
            hdd = getInt(dt.Rows[0]["HardDrive"].ToString());
            sound = getInt(dt.Rows[0]["Sound"].ToString());
            os = getInt(dt.Rows[0]["OS"].ToString());
            monitor = getInt(dt.Rows[0]["Display"].ToString());
        }

        /*
         * Gets a table from database.
         * If id is 0, returns entire table.
         * If id > 0, returns table with WHERE clause.
         */
        private DataTable getTable(string table, int id = 0)
        {
            DataTable dt = new DataTable();
            string sql = "";
            if (id != 0)
            {
                sql = "SELECT * FROM " + table + " WHERE ID = @id";
            }
            else
            {
                sql = "SELECT * FROM " + table;
            }
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["tma3aConnectionString"].ConnectionString);
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@id", id);

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
                dt = null;
            }

            return dt;
        }

        /*
         * Loads the avaiable components onto a drop down menu
         */
        private void displayDropListOpt(DataTable dt, DropDownList dropList, bool debug=false)
        {
            if (debug)
            {
                Page.Response.Write("<script>console.log('Row count: " + dt.Rows.Count + "');</script>");
            }
            for (var i =0; i < dt.Rows.Count; i++)
            {
                int id = getInt(dt.Rows[i]["Id"].ToString());
                string describe = getPartDescription(id);
                double price = getPartPrice(id);

                ListItem opt = new ListItem();
                opt.Text = describe + " " + price.ToString("0.##");
                opt.Value = id.ToString();
                dropList.Items.Add(opt);

                if (debug)
                {
                    Page.Response.Write("<script>console.log('i count: " + i + "');</script>");
                    Page.Response.Write("<script>console.log('id: " + id + "');</script>");
                    Page.Response.Write("<script>console.log('describe: " + describe + "');</script>");
                }
            }
        }

        /*
         * Loads all avaiable part option to drow down menu
         */
        private void LoadOptions()
        {
            if (cpuTable == null)
            {
                cpuTable = getTable("CPU");
                RamTable = getTable("Ram");
                hddTable = getTable("HardDrive");
                soundTable = getTable("Sound");
                osTable = getTable("OpSys");
                monitorTable = getTable("Monitor");
            }

            if ((cpuTable != null))
            {
                displayDropListOpt(cpuTable, ChooseCpu);
                displayDropListOpt(RamTable,  ChooseRam);
                displayDropListOpt(hddTable, ChooseHDD);
                displayDropListOpt(soundTable, ChooseSound);
                displayDropListOpt(osTable, ChooseOS);
                displayDropListOpt(monitorTable, ChooseMonitor);
            }
        }

        /*
         * Stores the selected part id into the desired component of the computer
         */
        private void setCustomParts(string id, int value)
        {
            System.Diagnostics.Debug.WriteLine("Inside setCustomParts...");
            System.Diagnostics.Debug.WriteLine("Value: " + value);
            System.Diagnostics.Debug.WriteLine("Before insert ram: " + ram);
            switch (id)
            {
                case "ChooseRam"        :   ram = value;
                                            System.Diagnostics.Debug.WriteLine("Inserting into ram...");
                                            break;
                case "ChooseCpu"        :   cpu = value;
                                            break;
                case "ChooseHDD"        :   hdd = value;
                                            break;
                case "ChooseSound"      :   sound = value;
                                            break;
                case "ChooseOS"         :   os = value;
                                            break;
                case "ChooseMonitor"    :   monitor = value;
                                            break;
            }

            System.Diagnostics.Debug.WriteLine("After insert ram: " + ram);
            displayCustomParts(cpu, ram, hdd, sound, os, monitor); //Update computer descrition
        }

        /*
         * Stores the new custom computer to database.
         */ 
        private int addCustomComputer()
        {
            int id = -1;
            string sql = "INSERT INTO Computer (CPU, Ram, Sound, OS, HardDrive, Display, Price, Image) OUTPUT INSERTED.ID VALUES(@cpu, @ram, @sound, @os, @hdd, @display, @price, NULL)";
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["tma3aConnectionString"].ConnectionString);
            SqlCommand cmd = new SqlCommand(sql, conn);

            cmd.Parameters.AddWithValue("@cpu", cpu);
            cmd.Parameters.AddWithValue("@ram", ram);
            cmd.Parameters.AddWithValue("@sound", sound);
            cmd.Parameters.AddWithValue("@os", os);
            cmd.Parameters.AddWithValue("@hdd", hdd);
            cmd.Parameters.AddWithValue("@display", monitor);
            cmd.Parameters.AddWithValue("@price", totalPrice);

            try
            {
                conn.Open();
                id = (int)cmd.ExecuteScalar();
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

            return id;
        }

        /*
         * Gets current order or make a new order and save it in database.
         */
        private int getCurOder(int userId)
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

        /**************************Event Functiuons*************************/
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
                else
                {
                    Response.Redirect("Login.aspx");
                }

                if (Session["customComp"] != null)
                {
                    int id = getInt(Session["customComp"].ToString());
                    System.Diagnostics.Debug.WriteLine("Base computert id: " + id);
                    DataTable compSpec = getTable("Computer", id);

                    if (compSpec != null)
                    {
                        System.Diagnostics.Debug.WriteLine("compSpec.Rows.Count: " + compSpec.Rows.Count);

                        if (!IsPostBack)
                        {
                            getPartList(compSpec);
                            LoadOptions();
                            compSpec = null;
                        }
                        displayCustomParts(cpu, ram, hdd, sound, os, monitor);
                    }
                }
                else
                {
                    Response.Redirect("ComputerList.aspx");
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

        //Delete all work for current custom computer
        protected void CancelCustom_Click(object sender, EventArgs e)
        {
            Session["customComp"] = null;
            Response.Redirect("ComputerList.aspx");
        }

        /*
         * Adds the customer computer to database
         */
        protected void AddCustom_Click(object sender, EventArgs e)
        {
            Session["customComp"] = null;

            if ((Session != null) && (Session["userName"] != null))
            {
                int customID = addCustomComputer();
                int userID = getInt(Session["userID"].ToString());
                int curOrder = getCurOder(userID);//Get current order. If no current, make new order.
                addToOrder(curOrder, customID);
                Response.Redirect("Cart.aspx");
            }
            else
            {
                Response.Redirect("Login.aspx");
            }
        }

        /*
         * Gets the id of the selected component and update the custom computer
         */
        protected void ChooseOpt_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList drpDwn = sender as DropDownList;
            string id = drpDwn.ID;
            int value = getInt(drpDwn.SelectedValue);

            System.Diagnostics.Debug.WriteLine("id: " + id);
            System.Diagnostics.Debug.WriteLine("value: " + value);
            System.Diagnostics.Debug.WriteLine("RAM before setCustomParts: " + ram);
            setCustomParts(id, value);
            System.Diagnostics.Debug.WriteLine("RAM outside setCustomParts: " + ram);
            System.Diagnostics.Debug.WriteLine("");
        }
    }
}