<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="validation.aspx.cs" Inherits="Validationprog.validation" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
         <div>
            <p>Insert your details:</p>
            <asp:Label ID="lblName" runat="server" AssociatedControlID="txtName">Name:</asp:Label>
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:TextBox ID="txtName" runat="server" CssClass="red-asterisk"></asp:TextBox>
            <span style="color: red;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; *</span>
             <br />
            <br />
        </div>
        <div>
            <asp:Label ID="lblFamilyName" runat="server" AssociatedControlID="txtFamilyName">Family Name:</asp:Label>
            &nbsp;&nbsp;&nbsp;
            <asp:TextBox ID="txtFamilyName" runat="server" CssClass="red-asterisk"></asp:TextBox>
            <span style="color: red;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; *</span>
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; differs from name
            <br />
            <br />
        </div>
        <div>
            <asp:Label ID="lblAddress" runat="server" AssociatedControlID="txtAddress">Address:</asp:Label>
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;
            <asp:TextBox ID="txtAddress" runat="server" CssClass="red-asterisk"></asp:TextBox>
            <span style="color: red;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; *</span>
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; at least 2 chars
            <br />
            <br />
        </div>
        <div>
            <asp:Label ID="lblCity" runat="server" AssociatedControlID="txtCity">City:</asp:Label>
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;
            <asp:TextBox ID="txtCity" runat="server" CssClass="red-asterisk"></asp:TextBox>
            <span style="color: red;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; *</span>
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; at least 2 chars
            <br />
            <br />
        </div>
        <div>
            <asp:Label ID="lblZipCode" runat="server" AssociatedControlID="txtZipCode">Zip Code:</asp:Label>
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;
            <asp:TextBox ID="txtZipCode" runat="server" CssClass="red-asterisk"></asp:TextBox>
            <span style="color: red;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; *</span>
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; (xxxxx)
            <br />
            <br />
        </div>
        <div>
            <asp:Label ID="lblPhone" runat="server" AssociatedControlID="txtPhone">Phone:</asp:Label>
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:TextBox ID="txtPhone" runat="server" CssClass="red-asterisk"></asp:TextBox>
            <span style="color: red;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; *</span>
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; (xx-xxxxxxx or xxx-xxxxxxx)
            <br />
            <br />
        </div>
        <div>
            <asp:Label ID="lblEmail" runat="server" AssociatedControlID="txtEmail">Email:</asp:Label>
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:TextBox ID="txtEmail" runat="server" CssClass="red-asterisk"></asp:TextBox>
            <span style="color: red;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; *</span>
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <a href="mailto:examaple@example.com">example@example.com</a>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Button ID="btnCheck" runat="server" Text="Check" OnClick="btnCheck_Click" />
            <br />
        </div>
        <div>
            <div id="details" runat="server"></div>
        </div>
    </form>
</body>
</html>
