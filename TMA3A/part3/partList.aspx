<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="partList.aspx.cs" Inherits="TMA3A.part3.partList" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <title>Part 3: Part List</title>
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
                <asp:Table ID="partsTable" runat="server">
                    <asp:TableRow>
                        <asp:TableHeaderCell CssClass="prodImage" >Product</asp:TableHeaderCell>
                        <asp:TableHeaderCell CssClass="prodDescript">Description</asp:TableHeaderCell>
                        <asp:TableHeaderCell CssClass="prodPrice">Price</asp:TableHeaderCell>
                        <asp:TableHeaderCell CssClass="prodOption">Option</asp:TableHeaderCell>
                    </asp:TableRow>

                    <asp:TableRow>
                        <asp:TableCell CssClass="prodImage"><asp:Image ImageUrl="~/shared/image/Ram.jpg" AlternateText="Ram" runat="server" /></asp:TableCell>
                        <asp:TableCell ID="prod1" CssClass="prodDescript"></asp:TableCell>
                        <asp:TableCell ID="price1" CssClass="prodPrice"></asp:TableCell>
                        <asp:TableCell CssClass="prodOption"><asp:Button ID="buyProd1" CssClass="buyProd" Text="Add to Cart" runat="server" OnClick="ProdBuy_Click"/></asp:TableCell> <%--1--%>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell CssClass="prodImage"><asp:Image ImageUrl="~/shared/image/Ram.jpg" AlternateText="Ram" runat="server" /></asp:TableCell>
                        <asp:TableCell ID="prod2" CssClass="prodDescript"></asp:TableCell>
                        <asp:TableCell ID="price2" CssClass="prodPrice"></asp:TableCell>
                        <asp:TableCell CssClass="prodOption"><asp:Button ID="buyProd2" CssClass="buyProd" Text="Add to Cart" runat="server" OnClick="ProdBuy_Click"/></asp:TableCell> 
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell CssClass="prodImage"><asp:Image ImageUrl="~/shared/image/Ram.jpg" AlternateText="Ram" runat="server" /></asp:TableCell>
                        <asp:TableCell ID="prod3" CssClass="prodDescript"></asp:TableCell>
                        <asp:TableCell ID="price3" CssClass="prodPrice"></asp:TableCell>
                        <asp:TableCell CssClass="prodOption"><asp:Button ID="buyProd3" CssClass="buyProd" Text="Add to Cart" runat="server" OnClick="ProdBuy_Click"/></asp:TableCell> 
                    </asp:TableRow>

                    <asp:TableRow>
                        <asp:TableCell CssClass="prodImage"><asp:Image ImageUrl="~/shared/image/cpu.jpg" AlternateText="CPU" runat="server"/></asp:TableCell>
                        <asp:TableCell ID="prod4" CssClass="prodDescript"></asp:TableCell>
                        <asp:TableCell ID="price4" CssClass="prodPrice"></asp:TableCell>
                        <asp:TableCell CssClass="prodOption"><asp:Button ID="buyProd4" CssClass="buyProd" Text="Add to Cart" runat="server" OnClick="ProdBuy_Click"/></asp:TableCell> 
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell CssClass="prodImage"><asp:Image ImageUrl="~/shared/image/cpu.jpg" AlternateText="CPU" runat="server" /></asp:TableCell>
                        <asp:TableCell ID="prod5" CssClass="prodDescript"></asp:TableCell>
                        <asp:TableCell ID="price5" CssClass="prodPrice"></asp:TableCell>
                        <asp:TableCell CssClass="prodOption"><asp:Button ID="buyProd5" CssClass="buyProd" Text="Add to Cart" runat="server" OnClick="ProdBuy_Click"/></asp:TableCell> 
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell CssClass="prodImage"><asp:Image ImageUrl="~/shared/image/cpu.jpg" AlternateText="CPU" runat="server" /></asp:TableCell>
                        <asp:TableCell ID="prod6" CssClass="prodDescript"></asp:TableCell>
                        <asp:TableCell ID="price6" CssClass="prodPrice"></asp:TableCell>
                        <asp:TableCell CssClass="prodOption"><asp:Button ID="buyProd6" CssClass="buyProd" Text="Add to Cart" runat="server" OnClick="ProdBuy_Click"/></asp:TableCell> 
                    </asp:TableRow>

                    <asp:TableRow>
                        <asp:TableCell CssClass="prodImage"><asp:Image ImageUrl="~/shared/image/hardDrive.jpg" CssClass="productImage" AlternateText="Harddrive" runat="server" /></asp:TableCell>
                        <asp:TableCell ID="prod7" CssClass="prodDescript"></asp:TableCell>
                        <asp:TableCell ID="price7" CssClass="prodPrice"></asp:TableCell>
                        <asp:TableCell CssClass="prodOption"><asp:Button ID="buyProd7" CssClass="buyProd" Text="Add to Cart" runat="server" OnClick="ProdBuy_Click"/></asp:TableCell> 
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell CssClass="prodImage"><asp:Image ImageUrl="~/shared/image/hardDrive.jpg" CssClass="productImage" AlternateText="Harddrive" runat="server" /></asp:TableCell>
                        <asp:TableCell ID="prod8" CssClass="prodDescript"></asp:TableCell>
                        <asp:TableCell ID="price8" CssClass="prodPrice"></asp:TableCell>
                        <asp:TableCell CssClass="prodOption"><asp:Button ID="buyProd8" CssClass="buyProd" Text="Add to Cart" runat="server" OnClick="ProdBuy_Click"/></asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell CssClass="prodImage"><asp:Image ImageUrl="~/shared/image/hardDrive.jpg" CssClass="productImage" AlternateText="Harddrive" runat="server" /></asp:TableCell>
                        <asp:TableCell ID="prod9" CssClass="prodDescript"></asp:TableCell>
                        <asp:TableCell ID="price9" CssClass="prodPrice"></asp:TableCell>
                        <asp:TableCell CssClass="prodOption"><asp:Button ID="buyProd9" CssClass="buyProd" Text="Add to Cart" runat="server" OnClick="ProdBuy_Click"/></asp:TableCell> 
                    </asp:TableRow>

                    <asp:TableRow>
                        <asp:TableCell CssClass="prodImage"><asp:Image ImageUrl="~/shared/image/soundCard.png" AlternateText="Sound Card" runat="server" /></asp:TableCell>
                        <asp:TableCell ID="prod10" CssClass="prodDescript"></asp:TableCell>
                        <asp:TableCell ID="price10" CssClass="prodPrice"></asp:TableCell>
                        <asp:TableCell CssClass="prodOption"><asp:Button ID="buyProd10" CssClass="buyProd" Text="Add to Cart" runat="server" OnClick="ProdBuy_Click"/></asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell CssClass="prodImage"><asp:Image ImageUrl="~/shared/image/soundCard.png" AlternateText="Sound Card" runat="server" /></asp:TableCell>
                        <asp:TableCell ID="prod11" CssClass="prodDescript"></asp:TableCell>
                        <asp:TableCell ID="price11" CssClass="prodPrice"></asp:TableCell>
                        <asp:TableCell CssClass="prodOption"><asp:Button ID="buyProd11" CssClass="buyProd" Text="Add to Cart" runat="server" OnClick="ProdBuy_Click"/></asp:TableCell> 
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell CssClass="prodImage"><asp:Image ImageUrl="~/shared/image/soundCard.png" AlternateText="Sound Card" runat="server" /></asp:TableCell>
                        <asp:TableCell ID="prod12" CssClass="prodDescript"></asp:TableCell>
                        <asp:TableCell ID="price12" CssClass="prodPrice"></asp:TableCell>
                        <asp:TableCell CssClass="prodOption"><asp:Button ID="buyProd12" CssClass="buyProd" Text="Add to Cart" runat="server" OnClick="ProdBuy_Click"/></asp:TableCell> 
                    </asp:TableRow>

                    <asp:TableRow>
                        <asp:TableCell CssClass="prodImage"><asp:Image ImageUrl="~/shared/image/os.jpg" AlternateText="Operating System" runat="server" /></asp:TableCell>
                        <asp:TableCell ID="prod13" CssClass="prodDescript"></asp:TableCell>
                        <asp:TableCell ID="price13" CssClass="prodPrice"></asp:TableCell>
                        <asp:TableCell CssClass="prodOption"><asp:Button ID="buyProd13" CssClass="buyProd" Text="Add to Cart" runat="server" OnClick="ProdBuy_Click"/></asp:TableCell> 
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell CssClass="prodImage"><asp:Image ImageUrl="~/shared/image/os.jpg" AlternateText="Operating System" runat="server" /></asp:TableCell>
                        <asp:TableCell ID="prod14" CssClass="prodDescript"></asp:TableCell>
                        <asp:TableCell ID="price14" CssClass="prodPrice"></asp:TableCell>
                        <asp:TableCell CssClass="prodOption"><asp:Button ID="buyProd14" CssClass="buyProd" Text="Add to Cart" runat="server" OnClick="ProdBuy_Click"/></asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell CssClass="prodImage"><asp:Image ImageUrl="~/shared/image/os.jpg" AlternateText="Operating System" runat="server" /></asp:TableCell>
                        <asp:TableCell ID="prod15" CssClass="prodDescript"></asp:TableCell>
                        <asp:TableCell ID="price15" CssClass="prodPrice"></asp:TableCell>
                        <asp:TableCell CssClass="prodOption"><asp:Button ID="buyProd15" CssClass="buyProd" Text="Add to Cart" runat="server" OnClick="ProdBuy_Click"/></asp:TableCell>
                    </asp:TableRow>

                    <asp:TableRow>
                        <asp:TableCell CssClass="prodImage"><asp:Image ImageUrl="~/shared/image/compMonitor.png" CssClass="productImage" AlternateText="Dispaly" runat="server" /></asp:TableCell>
                        <asp:TableCell ID="prod16" CssClass="prodDescript"></asp:TableCell>
                        <asp:TableCell ID="price16" CssClass="prodPrice"></asp:TableCell>
                        <asp:TableCell CssClass="prodOption"><asp:Button ID="buyProd16" CssClass="buyProd" Text="Add to Cart" runat="server" OnClick="ProdBuy_Click"/></asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell CssClass="prodImage"><asp:Image ImageUrl="~/shared/image/compMonitor.png" CssClass="productImage" AlternateText="Dispaly" runat="server" /></asp:TableCell>
                        <asp:TableCell ID="prod17" CssClass="prodDescript"></asp:TableCell>
                        <asp:TableCell ID="price17" CssClass="prodPrice"></asp:TableCell>
                        <asp:TableCell CssClass="prodOption"><asp:Button ID="buyProd17" CssClass="buyProd" Text="Add to Cart" runat="server" OnClick="ProdBuy_Click"/></asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell CssClass="prodImage"><asp:Image ImageUrl="~/shared/image/compMonitor.png" CssClass="productImage" AlternateText="Dispaly" runat="server" /></asp:TableCell>
                        <asp:TableCell ID="prod18" CssClass="prodDescript"></asp:TableCell>
                        <asp:TableCell ID="price18" CssClass="prodPrice"></asp:TableCell>
                        <asp:TableCell CssClass="prodOption"><asp:Button ID="buyProd18" CssClass="buyProd" Text="Add to Cart" runat="server" OnClick="ProdBuy_Click"/></asp:TableCell>
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
