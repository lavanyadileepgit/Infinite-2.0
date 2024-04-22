<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Message.aspx.cs" Inherits="CodeChallange1.Message" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Message Page</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Button ID="Button1" runat="server" Text="Click Me" OnClick="Button1_Click" />
             <span id="clickCount" runat="server"></span>
            <asp:HiddenField ID="HiddenField1" runat="server" Value="0" />
        </div>
    </form>
</body>
</html>
