<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="part2.aspx.cs" Inherits="TMA3A.part2.part2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <title>Part 2: SlideShow</title>
    <link rel="stylesheet" type="text/css" href="../shared/tma3a.css" />
</head>
<body>
    <div class="topNavBar">
        <div class="navMenu">
            <img class="menuIcon" alt="navMenu" src="/shared/icons/Hamburger_icon.png" />
            <div class="navMenuContent">
                <a href="../part1/part1.aspx">Part 1</a>
                <a href="#">Part 2</a>
                <a href="../part3/part3.aspx">Part 3</a>
                <a href="../part4/part4.aspx">Part 4</a>
            </div>
        </div>
        <a href="../tma3a.htm" id="homeLink">TMA 3A</a>
    </div>
    <div class="slideShowContainer">
        <h1>SlideShow</h1>
        <form id="slideShowForm" runat="server">
            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
            <asp:UpdatePanel ID="slideShowUpdate" runat="server">
                <ContentTemplate>
                    <asp:Timer ID="Timer1" runat="server" Interval="3000" OnTick="timerPlay" Enabled="False"></asp:Timer>
                    <div class="slideShow">
                        <asp:Image ID="currentImage" CssClass="slideShowImage" ImageUrl="~/part2/slideShow/1.jpg" runat="server"/>
                    </div>
                    <div class="slideShowCaptions">
                        <asp:TextBox ID="imageCaption" CssClass="captionTextBox" Wrap="True" TextMode="MultiLine" runat="server"/>
                    </div>
                    </ContentTemplate>
            </asp:UpdatePanel> 
            <asp:UpdatePanel ID="slideShowControls" runat="server">
                <ContentTemplate>
                    <div class="slideShowControls">
                        <h2>Controls</h2>
                        <asp:ImageButton CssClass="controlImage" ID="backWardImage" ImageUrl="~/part2/icons/back.png" AlternateText="back" runat="server" OnClick="backWardImage_Click" />
                        <asp:ImageButton CssClass="controlImage" ID="playPause" ImageUrl="~/part2/icons/play.png" AlternateText="playPause" runat="server" OnClick="playPause_Click" />
                        <asp:ImageButton CssClass="controlImage" ID="forWardImage" ImageUrl="~/part2/icons/forward.png" AlternateText="forward" runat="server" OnClick="forWardImage_Click" />
                        <asp:ImageButton CssClass="controlImage" ID="sequentialRandom" ImageUrl="~/part2/icons/sequent.png" AlternateText="sequentialRandom" runat="server" OnClick="sequentialRandom_Click" />
                        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:tma3aConnectionString %>" SelectCommand="SELECT * FROM [part2Pictures]"></asp:SqlDataSource>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </form>
    </div>
</body>
</html>
