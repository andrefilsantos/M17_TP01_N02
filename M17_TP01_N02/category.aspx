<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="category.aspx.cs" Inherits="M17_TP01_N02.Category" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <h1 runat="server" ID="title"><span class="name">OPCS</span>, estes são os produtos da categoria</h1>
        <br/>
        <div id="divProducts" runat="server">
        </div>
    </div>
</asp:Content>
