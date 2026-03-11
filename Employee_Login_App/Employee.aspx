<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Employee.aspx.cs" Inherits="Employee_Login_App.Employee" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Employee Management</title>
</head>
<body>
    <form id="form1" runat="server">
        <h2>Employee Management</h2>
        <asp:Label ID="lblMessage" runat="server" ForeColor="Red"></asp:Label>
        <br /><br />

        <asp:GridView ID="gvEmployees" runat="server" AutoGenerateColumns="False" 
            OnRowEditing="gvEmployees_RowEditing" OnRowUpdating="gvEmployees_RowUpdating" 
            OnRowCancelingEdit="gvEmployees_RowCancelingEdit" OnRowDeleting="gvEmployees_RowDeleting"
            DataKeyNames="Id">
            <Columns>
                <asp:BoundField DataField="Id" HeaderText="ID" ReadOnly="True" />
                <asp:BoundField DataField="Username" HeaderText="Username" />
                <asp:BoundField DataField="Name" HeaderText="Name" />
                <asp:BoundField DataField="Address" HeaderText="Address" />
                <asp:BoundField DataField="Contact" HeaderText="Contact" />
                <asp:CommandField ShowEditButton="True" ShowDeleteButton="True" />
            </Columns>
        </asp:GridView>
        <br /><br />

        <h3>Add New Employee</h3>
        Username:<br />
        <asp:TextBox ID="txtUsername" runat="server"></asp:TextBox><br /><br />
        Password:<br />
        <asp:TextBox ID="txtPassword" runat="server" TextMode="Password"></asp:TextBox><br /><br />
        Name:<br />
        <asp:TextBox ID="txtName" runat="server"></asp:TextBox><br /><br />
        Address:<br />
        <asp:TextBox ID="txtAddress" runat="server"></asp:TextBox><br /><br />
        Contact:<br />
        <asp:TextBox ID="txtContact" runat="server"></asp:TextBox><br /><br />
        <asp:Button ID="btnAdd" runat="server" Text="Add Employee" OnClick="btnAdd_Click" />
    </form>
</body>
</html>
