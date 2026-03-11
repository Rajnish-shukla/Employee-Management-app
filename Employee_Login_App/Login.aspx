<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Employee_Login_App.Login" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Employee Login</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            Username<br />
            <asp:TextBox ID="txtUsername" runat="server"></asp:TextBox>
            <br /><br />

            Password<br />
            <asp:TextBox ID="txtPassword" runat="server" TextMode="Password"></asp:TextBox>
            <asp:RegularExpressionValidator 
                runat="server"
                ControlToValidate="txtPassword"
                ErrorMessage="Password must contain upper, lower, number, special char"
                ValidationExpression="^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&]).{6,}$"
                ForeColor="Red">
            </asp:RegularExpressionValidator>
            <br /><br />

            Retype Password<br />
            <asp:TextBox ID="txtRetypePassword" runat="server" TextMode="Password"></asp:TextBox>
            <asp:CompareValidator
                runat="server"
                ControlToValidate="txtRetypePassword"
                ControlToCompare="txtPassword"
                ErrorMessage="Passwords do not match"
                ForeColor="Red">
            </asp:CompareValidator>
            <br /><br />

            <asp:Label ID="lblMessage" runat="server" ForeColor="Red"></asp:Label>
            <br /><br />

            <asp:Button ID="btnLogin" runat="server" Text="Login" OnClick="btnLogin_Click" />
        </div>
    </form>
</body>
</html>