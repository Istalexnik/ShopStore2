<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Registration.aspx.cs" Inherits="ShopStore2.Registration" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Label runat="server" ID="lblMessage"/>
    <table>
        <thead>
            <tr>
                <th></th>
                <th>Registration</th>
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
                <td>Email:</td>
                <td><asp:TextBox runat="server" ID="txtEmail"/></td>
                <td></td>
                <td></td>
            </tr>
            <tr>
                <td></td>
                <td><asp:Button runat="server" ID="btnCreateAccount" Text="Create Account" OnClick="btnCreateAccount_Click"/></td>
                <td></td>
                <td><a href="Login.aspx">Log In</a></td>
            </tr>
        </tbody>
    </table>
</asp:Content>
