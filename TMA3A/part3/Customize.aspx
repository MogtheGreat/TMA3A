<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Customize.aspx.cs" Inherits="TMA3A.part3.Customize" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <title>Part 3: Customize</title>
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
                <asp:ImageButton ImageUrl="~/shared/icons/shoppingCart.png" AlternateText="Checkout" CssClass="cartIcon" runat="server" OnClick="CheckOut_Click" />
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
                            <asp:DropDownList ID="ChooseRam" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ChooseRam_SelectedIndexChanged"> 
                                <asp:ListItem Text="Timetec Hynix IC 8GB Memory $81.99" Value="1"></asp:ListItem>
                                <asp:ListItem Text="Corsair 16GB Memory $84.99" Value="0"></asp:ListItem>
                                <asp:ListItem Text="G.SKILL 32GB Memory $292.82" Value="2"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="CustomSection">
                            <asp:Label ID="CpuSelected" runat="server"></asp:Label>
                            <asp:Label ID="CpuSelectedPrice"  runat="server"></asp:Label>
                            <asp:DropDownList ID="ChooseCpu" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ChooseCpu_SelectedIndexChanged"> 
                                <asp:ListItem Text="AMD 6 Cores Processor 	$429.99" Value="3"></asp:ListItem>
                                <asp:ListItem Text="intel 8 Cores Processor $208.37" Value="4"></asp:ListItem>
                                <asp:ListItem Text="AMD 12 Cores Processor 	$621.48" Value="5"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="CustomSection">
                            <asp:Label ID="HDDSelected" runat="server"></asp:Label>
                            <asp:Label ID="HDDSelectedPrice"  runat="server"></asp:Label>
                            <asp:DropDownList ID="ChooseHDD" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ChooseHDD_SelectedIndexChanged"> 
                                <asp:ListItem Text="Seagate 2TB Hard Drive 	$74.99" Value="6"></asp:ListItem>
                                <asp:ListItem Text="Western Digital 4TB Hard Drive 	$114.99" Value="7"></asp:ListItem>
                                <asp:ListItem Text="Toshiba 8TB Hard Drive $272.66" Value="8"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="CustomSection">
                            <asp:Label ID="SoundSelected" runat="server"></asp:Label>
                            <asp:Label ID="SoundSelectedPrice" runat="server"></asp:Label>
                            <asp:DropDownList ID="ChooseSound" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ChooseSound_SelectedIndexChanged"> 
                                <asp:ListItem Text="Creative Sound Blaster Audigy FX 5.1 Sound Card 	$49.99" Value="9"></asp:ListItem>
                                <asp:ListItem Text="Sound Blaster Z PCIe Sound Card $112.33" Value="10"></asp:ListItem>
                                <asp:ListItem Text="Creative Sound Blaster X AE-5 Sound Card 	$193.91" Value="11"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="CustomSection">
                            <asp:Label ID="OsSelected" runat="server"></asp:Label>
                            <asp:Label ID="OsSelectedPrice" runat="server"></asp:Label>
                            <asp:DropDownList ID="ChooseOS" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ChooseOS_SelectedIndexChanged"> 
                                <asp:ListItem Text="Mac OS X Lion 10.7 OS $39.99" Value="13"></asp:ListItem>
                                <asp:ListItem Text="Microsoft Windows 10 Home OS 	$105" Value="12"></asp:ListItem>
                                <asp:ListItem Text="Microsoft Windows 10 Pro OS $129.99" Value="14"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="CustomSection">
                            <asp:Label ID="MonitorSelected" runat="server"></asp:Label>
                            <asp:Label ID="MonistorSelectedPrice" runat="server"></asp:Label>
                            <asp:DropDownList ID="ChooseMonitor" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ChooseMonitor_SelectedIndexChanged"> 
                                <asp:ListItem Text="Samsung 22inch monitor 	$144.99" Value="15"></asp:ListItem>
                                <asp:ListItem Text="LG Electronics 24inch monitor 	$169.99" Value="16"></asp:ListItem>
                                <asp:ListItem Text="Sceptre 27inch monitor 	$287.61" Value="17"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
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
