<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="ShopStore2.Login" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Label runat="server" ID="lblMessageActivationLinkSent" ForeColor="Green" Text="Activation link was sent to your email. Please use that link to complete your registration"/>
    <asp:Label runat="server" ID="lblMessage"/>
<table>
    <thead>
        <tr>
            <th></th>
            <th>Login</th>
        </tr>
    </thead>
    <tbody>
        <tr>
            <td>Username:</td>
            <td><asp:TextBox runat="server" ID="txtUsername"/></td>
            <td></td>
            <td></td>
        </tr>
          <tr>
            <td>Password:</td>
            <td><asp:TextBox runat="server" ID="txtPassword"/></td>
            <td></td>
            <td></td>
        </tr>
          <tr>
            <td><asp:CheckBox runat="server" ID="chkRememberMe" Text="Remember Me"/></td>
            <td><asp:Button runat="server" ID="btnLogin" Text="Log In" OnClick="btnLogin_Click"/></td>
            <td></td>
            <td><a href="Registration.aspx">Create Account</a></td>
        </tr>

    </tbody>
</table>
</asp:Content>
