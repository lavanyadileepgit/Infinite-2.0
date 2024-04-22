<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NumCheck.aspx.cs" Inherits="CodeChallange1.NumCheck" %>
<!DOCTYPE html>
<html>
<head>
    <title>Number Checker</title>
</head>
<body>
    <form id="form1" runat="server">
        <div id="output"></div>
        <script>
            let outputDiv = document.getElementById('output');
            for (let i = 0; i <= 15; i++) {
                let message = (i % 2 === 0) ? `"${i} is even"` : `"${i} is odd"`;
                outputDiv.innerHTML += message + '<br>';
            }
        </script>
    </form>
</body>
</html>
