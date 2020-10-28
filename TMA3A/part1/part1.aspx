<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="part1.aspx.cs" Inherits="TMA3A.part1.part1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <title>Part 1: Cookie</title>
    <link rel="stylesheet" type="text/css" href="../shared/tma3a.css"/>
</head>
<body>
    <!--Top bar. Copy over to other sections.-->
    <div class="topNavBar">
        <!--Main Nav Menu-->
        <div class="navMenu">
            <img class="menuIcon" alt="navMenu" src="/shared/icons/Hamburger_icon.png" />
            <div class="navMenuContent">
                <a href="#">Part 1</a>
                <a href="../part2/part2.aspx">Part 2</a>
                <a href="../part3/part3.aspx">Part 3</a>
                <a href="../part4/part4.aspx">Part 4</a>
            </div>
        </div>
        <!--Return Home Link-->
        <a href="../tma3a.htm" id="homeLink">TMA 3A</a>
    </div>
    <div class="part1Container">
        <h1>Part 1: Persistent Cookie</h1>
        <p>This web application uses a persistent cookie to track how times a user has visited the page.
            The application will also display the user's ip address and time-zone. 
            If the user has disabled cookies in thier browser, then the web application may not work as intended.</p>
        <form id="part1Labels" runat="server">
            <div>
                <h2>Number of Times Visited:</h2>
                <asp:Label ID="numberTimesViewed" runat="server" CssClass="cookiePageLabels" Enabled="False" Text="N/A"></asp:Label>
            </div>
            <div>
                <h2>IP Address Of Visiting Computer:</h2>
                <asp:Label ID="ipAddress" runat="server" CssClass="cookiePageLabels" style="z-index: 1" Text="N/A"></asp:Label>
            </div>
            <div>
                <h2>Time Zone of Visiting Computer:</h2>
                <asp:Label ID="timeZone" runat="server" CssClass="cookiePageLabels" style="z-index: 1" Text="N/A"></asp:Label>
            </div>
            <script>
                //Displays the timezone using Javascript
                document.getElementById('<%=timeZone.ClientID%>').innerHTML = Intl.DateTimeFormat().resolvedOptions().timeZone;
           </script>
        </form>
    </div>
</body>
</html>
