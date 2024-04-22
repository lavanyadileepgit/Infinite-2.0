<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Message.aspx.cs" Inherits="CodeChallange1.Message" %>

<!DOCTYPE html>
<html>
<head>
    <title>Button Example</title>
</head>
<body>
    <button id="myButton">Click Me :) </button>
    <script>
       
        const button = document.getElementById('myButton');

      
        button.addEventListener('click', () => {
            console.log('Button clicked!');
        });
    </script>
</body>
</html>
