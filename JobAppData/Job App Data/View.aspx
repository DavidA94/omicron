<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="View.aspx.cs" Inherits="Job_App_Data.View" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="TopContentPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="FormContentPlaceHolder" runat="server">
    <form id="viewForm" runat="server">
        <table>
            <tr>
                <td>SSN:</td>
                <td><asp:TextBox ID="SSN" runat="server" /></td>
            </tr>
            <tr>
                <td>First Name:</td>
                <td><asp:TextBox ID="FirstName" runat="server" /></td>
            </tr>
            <tr>
                <td>Last Name:</td>
                <td><asp:TextBox ID="LastName" runat="server" /></td>
            </tr>
            <tr>
                <td>Phone Number:</td>
                <td><asp:TextBox ID="Phone" runat="server" /></td>
            </tr>
            <tr>
                <td>Date Submitted:</td>
                <td><asp:TextBox ID="DateSubmitted" runat="server" /></td>
            </tr>
            <tr>
                <td colspan="2">
                    <%-- Only one visible at a time --%>
                    <asp:Button ID="saveUserDetailsButton" Text="Update" runat="server" OnClick="saveUserDetailsButton_Click" />
                    <asp:LinkButton ID="returnAdminLink" Text="Back to applications" runat="server" Visible="false"
                        OnClick="returnAdminLink_Click" />
                </td>
            </tr>
        </table>
    </form>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="BottomContentPlaceHolder" runat="server">
</asp:Content>
