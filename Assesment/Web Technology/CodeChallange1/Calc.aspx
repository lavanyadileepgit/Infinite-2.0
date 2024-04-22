<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Calc.aspx.cs" Inherits="CodeChallange1.Calc" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Multiplication and Division Calculator</title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:Label ID="Label1" runat="server" Text="1st Number :"></asp:Label>&nbsp&nbsp;
        <asp:TextBox ID="n1" runat="server"></asp:TextBox><br /><br />
        <asp:Label ID="Label2" runat="server" Text="2nd Number :"></asp:Label>&nbsp
        <asp:TextBox ID="n2" runat="server"></asp:TextBox><br /><br />
        <asp:Button ID="btnmul" runat="server" Text="Multiply" OnClick="btnmul_Click" />&nbsp&nbsp
        <asp:Button ID="btndiv" runat="server" Text="Divide" OnClick="btndiv_Click" /><br /><br />
        <asp:Label ID="result" runat="server"></asp:Label><br />
    </form>
</body>
</html>


