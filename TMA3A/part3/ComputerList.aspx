<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ComputerList.aspx.cs" Inherits="TMA3A.part3.ComputerList" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <title>Part 3: Computer List</title>
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
                <asp:ImageButton ImageUrl="~/shared/icons/PCustom.png" AlternateText="PCustom" CssClass="storeLogo" runat="server" OnClick="Logo_Click" />
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
                <asp:Table ID="compTable" runat="server">
                    <asp:TableRow>
                        <asp:TableHeaderCell CssClass="prodImage">Computer</asp:TableHeaderCell>
                        <asp:TableHeaderCell CssClass="prodDescript">Description</asp:TableHeaderCell>
                        <asp:TableHeaderCell CssClass="prodPrice">Price</asp:TableHeaderCell>
                        <asp:TableHeaderCell CssClass="prodOption">Option</asp:TableHeaderCell>
                    </asp:TableRow>

                    <asp:TableRow>
                        <asp:TableCell CssClass="prodImage"><asp:Image ImageUrl="~/shared/image/comp1.png" ID="CompImage1" CssClass="productImage" AlternateText="Computer Bundle One" runat="server" /></asp:TableCell>
                        <asp:TableCell ID="CompDescript1" CssClass="prodDescript"></asp:TableCell>
                        <asp:TableCell ID="CompPrice1" CssClass="prodPrice"></asp:TableCell>
                        <asp:TableCell CssClass="prodOption">
                            <div><asp:Button ID="buyComp1" CssClass="compListOpt" Text="Quick Buy" runat="server" OnClick="CompBuy_Click"/></div>
                            <asp:Button ID="customizeComp1" CssClass="compListOpt" Text="Customize" runat="server" OnClick="CompCustom_Click"/>
                        </asp:TableCell>
                    </asp:TableRow>

                    <asp:TableRow>
                        <asp:TableCell CssClass="prodImage"><asp:Image ImageUrl="~/shared/image/comp2.jpg" ID="CompImage2" CssClass="productImage" AlternateText="Computer Bundle Two" runat="server" /></asp:TableCell>
                        <asp:TableCell ID="CompDescript2" CssClass="prodDescript"></asp:TableCell>
                        <asp:TableCell ID="CompPrice2" CssClass="prodPrice"></asp:TableCell>
                        <asp:TableCell CssClass="prodOption">
                            <div><asp:Button ID="buyComp2" CssClass="compListOpt" Text="Quick Buy" runat="server" OnClick="CompBuy_Click"/></div>
                            <asp:Button ID="customizeComp2" CssClass="compListOpt" Text="Customize" runat="server" OnClick="CompCustom_Click"/>
                        </asp:TableCell>
                    </asp:TableRow>

                    <asp:TableRow>
                        <asp:TableCell CssClass="prodImage"><asp:Image ImageUrl="~/shared/image/comp3.jpg" ID="CompImage3" CssClass="productImage" AlternateText="Computer Bundle Three" runat="server" /></asp:TableCell>
                        <asp:TableCell ID="CompDescript3" CssClass="prodDescript"></asp:TableCell>
                        <asp:TableCell ID="CompPrice3" CssClass="prodPrice"></asp:TableCell>
                        <asp:TableCell CssClass="prodOption">
                            <div><asp:Button ID="buyComp3" CssClass="compListOpt" Text="Quick Buy" runat="server" OnClick="CompBuy_Click"/></div>
                            <asp:Button ID="customizeComp3"  CssClass="compListOpt" Text="Customize" runat="server" OnClick="CompCustom_Click"/>
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </div> <!--End of Store Display Area-->
            <div class="bottomNavContainer">
                <div class="bottomNav">
                    <h4>Customer Support</h4>
                    <div><a href="ContactUs.aspx">Contact Us</a></div>
                    <div><a href="FeedBack.aspx">FeedBack</a></div>
                    <div><a href="Return.aspx">Return Policy</a></div>
                </div>
            </div> <!--End of Bottom Nav Container-->
        </div> <!--End of Store Container-->
    </form>
</body>
</html>
