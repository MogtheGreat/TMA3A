﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Checkout.aspx.cs" Inherits="TMA3A.part3.Checkout" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <title>Part 3: Cart</title>
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
            </div> <!--End of StoreNaveMenu-->
            <div class="storeDisplayArea">
                <h1>Checkout</h1>
                <div class="ItemsInCart">
                    <asp:Table ID="CartTable" runat="server">
                        <asp:TableRow>
                            <asp:TableHeaderCell>Product Description</asp:TableHeaderCell>
                            <asp:TableHeaderCell>Price</asp:TableHeaderCell>
                        </asp:TableRow>
                    </asp:Table>
                </div>
                <div class="TotalCost">
                    <div id="CalculatingContainer">
                        <div class="CalculatingTotal">
                            <asp:Label ID="CartSubTotal" runat="server">SubTotal: $0</asp:Label>
                        </div>
                        <div class="CalculatingTotal">
                            <asp:Label ID="Tax" runat="server">Tax: $0</asp:Label>
                        </div>
                        <div>
                            <asp:Label ID="Total" runat="server">Total: $0</asp:Label>
                        </div>
                    </div>
                    <div>
                        <asp:Button ID="Cancel" Text="Cancel" runat="server" OnClick="Cancel_Click" />
                        <asp:Button ID="PayButton" Text="Checkout" runat="server" OnClick="PayButton_Click" />
                    </div>
                </div> 
            </div> <!--End of Store Display Area-->
            <div class="bottomNavContainer">
                <div class="bottomNav">
                    <h4>Customer Support</h4>
                    <div><a href="ContactUs.aspx">Contact Us</a></div>
                    <div><a href="FeedBack.aspx">FeedBack</a></div>
                    <div><a href="Return.aspx">Return Policy</a></div>
                </div>
            </div> <!--End of Bottom Nav Container-->
        </div>
    </form>
</body>
</html>