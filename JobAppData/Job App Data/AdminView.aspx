<%@ Page Title="Application Submissions" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="AdminView.aspx.cs" Inherits="Job_App_Data.AdminView" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="TopContentPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="FormContentPlaceHolder" runat="server">
    <form runat="server">
        <div id="logout">
            <asp:LinkButton runat="server" ID="LogoutButton" OnClick="LogoutButton_Click" Text="Logout" />
        </div>
    </form>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="BottomContentPlaceHolder" runat="server">
    <table id="adminTable">
        <tr>
            <th>SSN</th>
            <th>First Name</th>
            <th>Last Name</th>
            <th>Phone Number</th>
            <th>Date Submitted</th>
            <th></th>
        </tr>
        <asp:Repeater ID="adminTableRow" runat="server">
            <ItemTemplate>
                <tr>
                    <td><%# Eval("SSN") %></td>
                    <td><%# Eval("FirstName") %></td>
                    <td><%# Eval("LastName") %></td>
                    <td><%# Eval("Phone") %></td>
                    <td><%# Eval("DateSubmitted") %></td>
                    <td>
                        <a href='/View.aspx?id=<%# Eval("ID") %>'>View</a>
                    </td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </table>
</asp:Content>
