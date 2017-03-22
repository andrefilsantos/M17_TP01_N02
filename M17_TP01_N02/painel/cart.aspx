<%@ Page Title="" Language="C#" MasterPageFile="~/painel/MasterPagePainel.Master" AutoEventWireup="true" CodeBehind="cart.aspx.cs" Inherits="M17_TP01_N02.painel.cart" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Label runat="server" ID="lblCart"></asp:Label>
    <div runat="server" id="divCart"></div>
    <img src="https://www.paypalobjects.com/webstatic/en_US/i/buttons/checkout-logo-large.png" alt="Check out with PayPal" />
</asp:Content>
