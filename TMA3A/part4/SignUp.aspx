<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SignUp.aspx.cs" Inherits="TMA3A.part4.SignUp" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <title>Part 4: Sign Up</title>
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
                <asp:ImageButton ImageUrl="~/shared/icons/PCustom.png" AlternateText="PCustom" CssClass="storeLogo" runat="server" OnClick="Logo_Click" causesvalidation="false"/>
                <asp:ImageButton ImageUrl="~/shared/icons/shoppingCart.png" AlternateText="Checkout" CssClass="cartIcon" runat="server" OnClick="CheckOut_Click" causesvalidation="false"/>
                <asp:ImageButton ImageUrl="~/shared/image/logout.png" AlternateText="logout" ID="LogOutButton" CssClass="logoutIcon" runat="server" OnClick="Logout_Click" causesvalidation="false"/>
                <asp:ImageButton ImageUrl="~/shared/icons/List.png" AlternateText="Order List" ID="OrderListButton" CssClass="logoutIcon" runat="server" OnClick="Order_Click" causesvalidation="false"/>
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
            </div> <!--End of storeNavMenu-->
            <div class="storeDisplayArea">
                <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                <h2>Sign Up</h2>
                <div class="registerationArea">
                    <p>Please fill in all fields and press submit.</p>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <div>
                                <asp:Label ID="signupFirstNameLabel" CssClass="signUpLabel" runat="server" Text="First Name:"></asp:Label>
                                <asp:TextBox ID="signupFirstName" CssClass="signUpInput" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="ReqField1" CssClass="signUpValidator" runat="server" ControlToValidate="signupFirstName" ErrorMessage="* Please enter your first name.">*Please enter your first name.</asp:RequiredFieldValidator>
                            </div>
                            <div>
                                <asp:Label ID="signupLastNameLabel" CssClass="signUpLabel" runat="server" Text="Last Name:"></asp:Label>
                                <asp:TextBox ID="signupLastName" CssClass="signUpInput" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="ReqField2" CssClass="signUpValidator" runat="server" ControlToValidate="signupLastName" ErrorMessage="* Please enter your last name.">*Please enter your last name.</asp:RequiredFieldValidator>
                            </div>
                            <div>
                                <asp:Label ID="signupEmailLabel" CssClass="signUpLabel" runat="server" Text="Email:"></asp:Label>
                                <asp:TextBox ID="signupEmail" CssClass="signUpInput" TextMode="Email" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="ReqField3" CssClass="signUpValidator" runat="server" ControlToValidate="signupEmail" ErrorMessage="* Please enter a email.">*Please enter a email.</asp:RequiredFieldValidator>
                            </div>
                            <div>
                                <asp:Label ID="signupEnterPswdLabel" CssClass="signUpLabel" runat="server" Text="Enter Password:"></asp:Label>
                                <asp:TextBox ID="signupEnterPswd" CssClass="signUpInput" TextMode="Password" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="ReqField4" CssClass="signUpValidator" runat="server" ControlToValidate="signupEnterPswd" ErrorMessage="* Please enter a password.">*Please enter a password.</asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="valMinEntered" runat="server" ControlToValidate="signupEnterPswd" ErrorMessage="Minimum length: 8" ValidationExpression=".{8}.*" Display="Dynamic" ></asp:RegularExpressionValidator>
                            </div>
                            <div>
                                <asp:Label ID="signupConfirmPswdLabel" CssClass="signUpLabel" runat="server" Text="Confirm Password:"></asp:Label>
                                <asp:TextBox ID="signupConfirmPswd" CssClass="signUpInput" TextMode="Password" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="ReqField5" CssClass="signUpValidator" runat="server" ControlToValidate="signupConfirmPswd" ErrorMessage="* Please confirm password.">*Please confirm password.</asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="valMinConfirm" runat="server" ControlToValidate="signupConfirmPswd" ErrorMessage="Minimum length: 8" ValidationExpression=".{8}.*" Display="Dynamic" ></asp:RegularExpressionValidator>
                            </div>
                            <asp:Button ID="RegisterButton" runat="server" Text="Register Account" OnClick="RegisterButton_Click" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
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
