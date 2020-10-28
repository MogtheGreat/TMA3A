using System;
using System.Collections.Generic;
using System.EnterpriseServices;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TMA3A.part3.computerParts;

namespace TMA3A.part3
{
    public partial class Customize : System.Web.UI.Page
    {
        static Computer custom = null; //The custom Computer

        /*
         * Uses the dropDown item value to get the component from the master part list.
         * The value is a number that represents where the part is in the list.
         */
        private Components getComponents (string dropDownValue)
        {
            int selection = 0;
            Components component = null;
            
            try
            {
                selection = Int32.Parse(dropDownValue);
                System.Diagnostics.Debug.WriteLine("Selection: " + selection);
                component = MasterList.prodList[selection];
            }
            catch (FormatException)
            {
                Console.WriteLine($"Unable to parse '{dropDownValue}'");
            }

            return component;
        }

        /*
         * Updates/display the complete description of the custom computer to screen.
         */
        private void updateComputerInfo()
        {
            CustomComputer.Text = custom.ToString();
            CustomerComputerPrice.Text = "$" + (custom.TotalPrice()).ToString("0.##");
        }

        /*
         * Updates/display the selected ram for custom computer
         */
        private void updateRamInfo()
        {
            RamSelected.Text = (custom.Ram).ToString();
            RamSelectedPrice.Text = "$" + ((custom.Ram).Price).ToString("0.##");
        }

        /*
         * Updates/display the selected cpu for custom computer
         */
        private void updateCpuInfo()
        {
            CpuSelected.Text = (custom.CPU).ToString();
            CpuSelectedPrice.Text = "$" + ((custom.CPU).Price).ToString("0.##");
        }

        /*
         * Updates/display the selected HardDrive for custom computer
         */
        private void updateHDDInfo()
        {
            HDDSelected.Text = (custom.HardDrive).ToString();
            HDDSelectedPrice.Text = "$" + ((custom.HardDrive).Price).ToString("0.##");
        }

        /*
         * Updates/display the selected sound drive for custom computer
         */
        private void updateSoundInfo()
        {
            SoundSelected.Text = (custom.SoundDrive).ToString();
            SoundSelectedPrice.Text = "$" + ((custom.SoundDrive).Price).ToString("0.##");
        }

        /*
         * Updates/display the selected Operating system for custom computer
         */
        private void updateOsInfo()
        {
            OsSelected.Text = (custom.OpSys).ToString();
            OsSelectedPrice.Text = "$" + ((custom.OpSys).Price).ToString("0.##");
        }

        /*
         * Updates/display the selected Monitor for custom computer
         */
        private void updateMonitorInfo()
        {
            MonitorSelected.Text = (custom.Display).ToString();
            MonistorSelectedPrice.Text = "$" + ((custom.Display).Price).ToString("0.##");
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if ((Session != null) && (!IsPostBack))
            {
                //System.Diagnostics.Debug.WriteLine("Loading Customize.apsx");
                if (Session["customComp"] != null)
                {
                    custom = Session["customComp"] as Computer;

                    //Display the current custom build and component selection.
                    updateComputerInfo();
                    updateRamInfo();
                    updateCpuInfo();
                    updateHDDInfo();
                    updateSoundInfo();
                    updateOsInfo();
                    updateMonitorInfo();
                    //System.Diagnostics.Debug.WriteLine(custom.ToString());
                }
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

        protected void CancelCustom_Click(object sender, EventArgs e)
        {
            if (Session != null)
            {
                Session["customComp"] = null;
            }
            Response.Redirect("ComputerList.aspx");
        }

        protected void AddCustom_Click(object sender, EventArgs e)
        {
            //System.Diagnostics.Debug.WriteLine("Inside AddCustom_Click...");
            List<Computer> cart = null;
            if (Session != null)
            {
                if (Session["compCartList"] != null)
                {
                    Session["customComp"] = null;
                    cart = Session["compCartList"] as List<Computer>;
                    cart.Add(custom);
                    Session["compCartList"] = cart;
                    Response.Redirect("Checkout.aspx");
                }
            }
        }

        protected void ChooseRam_SelectedIndexChanged(object sender, EventArgs e)
        {
            //System.Diagnostics.Debug.WriteLine("Inside ChooseRam_SelectedIndexChanged");
            //System.Diagnostics.Debug.WriteLine(ChooseRam.SelectedItem.Value);
            Components newRam = getComponents(ChooseRam.SelectedItem.Value); 

            if ((newRam != null) && (custom != null))
            {
                custom.Ram = newRam;
                Session["customComp"] = custom;
                updateComputerInfo(); //Update current Custom computer build
                updateRamInfo();  //Update selected Comonent displayed info
            }
        }

        protected void ChooseCpu_SelectedIndexChanged(object sender, EventArgs e)
        {
            //System.Diagnostics.Debug.WriteLine("Inside ChooseCpu_SelectedIndexChanged");
            //System.Diagnostics.Debug.WriteLine(ChooseCpu.SelectedItem.Value);
            Components newCpu = getComponents(ChooseCpu.SelectedItem.Value);

            if ((newCpu != null) && (custom != null))
            {
                custom.CPU = newCpu;
                Session["customComp"] = custom;
                updateComputerInfo(); //Update current Custom computer build
                updateCpuInfo(); //Update selected Comonent displayed info
            }
        }

        protected void ChooseHDD_SelectedIndexChanged(object sender, EventArgs e)
        {
            //System.Diagnostics.Debug.WriteLine("Inside ChooseHDD_SelectedIndexChanged");
            //System.Diagnostics.Debug.WriteLine(ChooseHDD.SelectedItem.Value);
            Components newHDD = getComponents(ChooseHDD.SelectedItem.Value);

            if((newHDD != null) && (custom != null))
            {
                custom.HardDrive = newHDD;
                Session["customComp"] = custom;
                updateComputerInfo(); //Update current Custom computer build
                updateHDDInfo(); //Update selected Comonent displayed info
            }
        }

        protected void ChooseSound_SelectedIndexChanged(object sender, EventArgs e)
        {
            //System.Diagnostics.Debug.WriteLine("Inside ChooseSound_SelectedIndexChanged");
            //System.Diagnostics.Debug.WriteLine(ChooseSound.SelectedItem.Value);
            Components newSound = getComponents(ChooseSound.SelectedItem.Value);
            
            if ((newSound != null) && (custom != null))
            {
                custom.SoundDrive = newSound;
                Session["customComp"] = custom;
                updateComputerInfo(); //Update current Custom computer build
                updateSoundInfo(); //Update selected Comonent displayed info
            }
        }

        protected void ChooseOS_SelectedIndexChanged(object sender, EventArgs e)
        {
            //System.Diagnostics.Debug.WriteLine("Inside ChooseOS_SelectedIndexChanged");
            //System.Diagnostics.Debug.WriteLine(ChooseOS.SelectedItem.Value);
            Components newOS = getComponents(ChooseOS.SelectedItem.Value);

            if ((newOS != null) && (custom != null))
            {
                custom.OpSys = newOS;
                Session["customComp"] = custom;
                updateComputerInfo(); //Update current Custom computer build
                updateOsInfo(); //Update selected Comonent displayed info
            }

        }

        protected void ChooseMonitor_SelectedIndexChanged(object sender, EventArgs e)
        {
            //System.Diagnostics.Debug.WriteLine("Inside ChooseMonitor_SelectedIndexChanged");
            //System.Diagnostics.Debug.WriteLine(ChooseMonitor.SelectedItem.Value);
            Components newMonitor = getComponents(ChooseMonitor.SelectedItem.Value);

            if ((newMonitor != null) && (custom != null))
            {
                custom.Display = newMonitor;
                Session["customComp"] = custom;
                updateComputerInfo(); //Update current Custom computer build
                updateMonitorInfo(); //Update selected Comonent displayed info
            }
        }
    }
}