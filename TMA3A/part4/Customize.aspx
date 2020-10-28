<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Customize.aspx.cs" Inherits="TMA3A.part4.Customize" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <title>Part 4: Customize</title>
    <link rel="stylesheet" type="text/css" href="../shared/tma3a.css" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="topNavBar">
            <div class="navMenu">
                <img class="menuIcon" alt="navMenu" src="/shared/icons/Hamburger_icon.png" />
                <div class="navMenuContent">
                    <a href="../part1/part1.aspx">Part 1</a>
                    <a href="../part2/part2.aspx">Part 2</a>
                    <a href="../part3/part3.aspx">Part 3</a>
                    <a href="../part4/part4.aspx">Part 4</a>
                </div>
            </div>
            <a href="../tma3a.htm" id="homeLink">TMA 3A</a>
        </div> <!--End of topNavBar-->
        <div class="storeContainer">
            <div class="storeMenuArea">
                <asp:ImageButton ImageUrl="~/shared/icons/PCustom.png" AlternateText="PCustom" CssClass="storeLogo" runat="server" OnClick="Logo_Click"/>
                <asp:ImageButton ImageUrl="~/shared/icons/shoppingCart.png" AlternateText="Checkout" CssClass="cartIcon" runat="server" OnClick="CheckOut_Click"/>
                <asp:ImageButton ImageUrl="~/shared/image/logout.png" AlternateText="logout" ID="LogOutButton" CssClass="logoutIcon" runat="server" OnClick="Logout_Click" causesvalidation="false"/>
                <asp:ImageButton ImageUrl="~/shared/icons/List.png" AlternateText="Order List" ID="OrderListButton" CssClass="logoutIcon" runat="server" OnClick="Order_Click" causesvalidation="false"/>
                <div class="storeAccount">
                    <asp:ImageButton ImageUrl="~/shared/icons/Account.png" AlternateText="Account" CssClass="accountLogo" runat="server" OnClick="Account_Click"/>
                    <asp:Label ID="accountName" runat="server">Sign in</asp:Label>
                </div>
            </div> <!--End of StoreMenuArea-->
            <div class="storeNavMenu">
                <a href="ComputerList.aspx">Computer</a>
                <a href="partList.aspx">Parts</a>
                <a href="Return.aspx">Returns</a>
                <a href="FeedBack.aspx">FeedBack</a>
                <a href="ContactUs.aspx">Contact us</a>
            </div>
            <div class="storeDisplayArea">
                                <h1>Customization</h1>
                <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <div class="CustomCompDescription">
                            <asp:Label ID="CustomComputer" runat="server"></asp:Label>
                            <asp:Label ID="CustomerComputerPrice" runat="server"></asp:Label>
                            <div>
                                <asp:Button ID="CancelCustom" Text="Cancel" runat="server" OnClick="CancelCustom_Click" />
                                <asp:Button ID="AddCustom" Text="Add To Cart" runat="server" OnClick="AddCustom_Click" />
                            </div>
                        </div>
                        <div class="CustomSection">
                            <asp:Label ID="RamSelected" runat="server">Ram:</asp:Label>
                            <asp:Label ID="RamSelectedPrice" runat="server">Price:</asp:Label>
                            <asp:DropDownList ID="ChooseRam" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ChooseOpt_SelectedIndexChanged"> 
                            </asp:DropDownList>
                        </div>
                        <div class="CustomSection">
                            <asp:Label ID="CpuSelected" runat="server"></asp:Label>
                            <asp:Label ID="CpuSelectedPrice"  runat="server"></asp:Label>
                            <asp:DropDownList ID="ChooseCpu" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ChooseOpt_SelectedIndexChanged"> 
                            </asp:DropDownList>
                        </div>
                        <div class="CustomSection">
                            <asp:Label ID="HDDSelected" runat="server"></asp:Label>
                            <asp:Label ID="HDDSelectedPrice"  runat="server"></asp:Label>
                            <asp:DropDownList ID="ChooseHDD" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ChooseOpt_SelectedIndexChanged"> 
                            </asp:DropDownList>
                        </div>
                        <div class="CustomSection">
                            <asp:Label ID="SoundSelected" runat="server"></asp:Label>
                            <asp:Label ID="SoundSelectedPrice" runat="server"></asp:Label>
                            <asp:DropDownList ID="ChooseSound" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ChooseOpt_SelectedIndexChanged"> 
                            </asp:DropDownList>
                        </div>
                        <div class="CustomSection">
                            <asp:Label ID="OsSelected" runat="server"></asp:Label>
                            <asp:Label ID="OsSelectedPrice" runat="server"></asp:Label>
                            <asp:DropDownList ID="ChooseOS" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ChooseOpt_SelectedIndexChanged"> 
                            </asp:DropDownList>
                        </div>
                        <div class="CustomSection">
                            <asp:Label ID="MonitorSelected" runat="server"></asp:Label>
                            <asp:Label ID="MonitorSelectedPrice" runat="server"></asp:Label>
                            <asp:DropDownList ID="ChooseMonitor" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ChooseOpt_SelectedIndexChanged"> 
                            </asp:DropDownList>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div> <!--End of storeDisplayArea -->
            <div class="bottomNavContainer">
                <div class="bottomNav">
                    <h4>Customer Support</h4>
                    <div><a href="ContactUs.aspx">Contact Us</a></div>
                    <div><a href="FeedBack.aspx">FeedBack</a></div>
                    <div><a href="Return.aspx">Return Policy</a></div>
                </div>
            </div> <!--End of bottomNavContainer-->
        </div>
    </form>
</body>
</html>
