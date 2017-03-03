<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="AllUsers.aspx.cs" Inherits="M17_TP01_N02.AllUsers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <h1>Utilizadores</h1>
        <asp:gridview runat="server" id="gvUsers" cssclass="table table-responsive table-bordered table-hover"></asp:gridview>
    </div>
</asp:Content>
