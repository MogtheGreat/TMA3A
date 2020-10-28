<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Order.aspx.cs" Inherits="TMA3A.part4.Order" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <title>Part 4: Order</title>
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
                <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                <h1>Order Management</h1>
                <p class="instructOrder">Press <img class="instructImage" src="../shared/icons/bin.png" /> to remove the order.</p>
                <p class="instructOrder">Press <img class="instructImage" src="../shared/icons/List.png" /> to view/edit the order.</p>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <div>
                            <asp:Label ID="OrderNum" CssClass="OrderNumberDisplay" runat="server"></asp:Label>
                        </div>
                        <asp:Table ID="orderManageTable" runat="server">
                            <asp:TableHeaderRow>
                                <asp:TableHeaderCell>Date</asp:TableHeaderCell>
                                <asp:TableHeaderCell>Order Number</asp:TableHeaderCell>
                                <asp:TableHeaderCell>Order</asp:TableHeaderCell>
                                <asp:TableHeaderCell>Total</asp:TableHeaderCell>
                                <asp:TableHeaderCell>Options</asp:TableHeaderCell>
                            </asp:TableHeaderRow>
                        </asp:Table>
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
