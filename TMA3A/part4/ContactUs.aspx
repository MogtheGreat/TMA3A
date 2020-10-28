<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ContactUs.aspx.cs" Inherits="TMA3A.part4.ContactUs" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <title>Part 4: Contact Us</title>
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
        </div>
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
                <h2 class="customerSupportTitle">Contact Us</h2>
                <div class="contactInfo">
                    <p><strong>Cusomter Care</strong></p>
                    <p>1-800-###-####</p>
                </div>
                <div class="contactInfo">
                    <p><strong>Monday to Friday</strong></p>
                    <p>9:00AM-9:00PM EST</p>
                </div>
                <div class="contactInfo">
                    <p><strong>Saturday & Sunday,</strong></p>
                    <p>10:00AM-9:00PM EST</p>
                </div>
                <h2 class="customerSupportTitle">Email</h2>
                <div class="contactInfo">
                    <p>customerSupport@pcustom.ca</p>
                </div>
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
