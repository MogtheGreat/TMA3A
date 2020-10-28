<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="TMA3A.part3.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <title>Part 3: Login</title>
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
                <asp:ImageButton ImageUrl="~/shared/icons/PCustom.png" AlternateText="PCustom" CssClass="storeLogo" runat="server" OnClick="Logo_Click" causesvalidation="false" />
                <asp:ImageButton ImageUrl="~/shared/icons/shoppingCart.png" AlternateText="Checkout" CssClass="cartIcon" runat="server" OnClick="CheckOut_Click" causesvalidation="false"/>
                <div class="storeAccount">
                    <asp:ImageButton ImageUrl="~/shared/icons/Account.png" AlternateText="Account" CssClass="accountLogo" runat="server" OnClick="Account_Click" causesvalidation="false"/>
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
                <h2>Account</h2>
                <div class="loginArea">
                    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                    <h2>Login</h2>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <div>
                                <asp:Label ID="emailLabel" runat="server" Text="Email:"></asp:Label>
                                <asp:TextBox ID="emailLoginBox" TextMode="Email" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="ReqField1" CssClass="signUpValidator" runat="server" ControlToValidate="emailLoginBox" ErrorMessage="*Please enter your email.">*Please enter your email.</asp:RequiredFieldValidator>
                            </div>
                            <div>
                                <asp:Label ID="passwordLabel" runat="server" Text="Password:"></asp:Label>
                                <asp:TextBox ID="passwordBox" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="ReqField2" CssClass="signUpValidator" runat="server" ControlToValidate="passwordBox" ErrorMessage="*Please enter the password.">*Please enter the password.</asp:RequiredFieldValidator>
                            </div>
                            <asp:Button ID="loginButton" runat="server" Text="Login" OnClick="loginButton_Click" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <div class="signUpArea">
                    <h2>No Account?</h2>
                    <p>Then register for an account:</p>
                    <asp:Button ID="signupButton" runat="server" Text="Register" OnClick="signupButton_Click" causesvalidation="false"/>
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
        </div> <!--End of Store Container-->
    </form>
</body>
</html>
