<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="M17_TP01_N02.WebForm1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!-- Carousel
    ================================================== -->
    <div runat="server" id="myCarousel" class="carousel slide" data-ride="carousel" style="margin-top: -20px; height: 468px">
        <!-- Indicators -->
        
    </div>
    <!-- /.carousel -->

    <div class="container" style="margin-top: 50px;">
        <div class="destaques">
            <h1><span class="name">OPCS</span>, vais gostar disto</h1>
            <div runat="server" id="divSimilarProducts" class="row list-group"></div>
        </div>
        <div class="novidades">
            <h1><span class="name">OPCS</span>, isto é novo por cá</h1>
            <div runat="server" id="divLatestProducts" class="row list-group"></div>
        </div>
    </div>
</asp:Content>
