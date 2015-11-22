<%@ Page Title="Login" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="Job_App_Data.index" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="TopContentPlaceHolder" runat="server">
    <h2>Login to System</h2>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="FormContentPlaceHolder" runat="server">
    <form id="LoginForm" runat="server">
        <asp:Login ID="LoginData" runat="server" OnAuthenticate="LoginData_Authenticate" 
            DisplayRememberMe="False" 
            FailureText="Bad Username or Password" 
            UserNameLabelText="Username:" 
            UserNameRequiredErrorMessage="Username is required." 
            TitleText=""
            >
            <FailureTextStyle CssClass="red" />
        </asp:Login>
    </form>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="BottomContentPlaceHolder" runat="server">
    <asp:Literal ID="serverReturn" runat="server" />
</asp:Content>