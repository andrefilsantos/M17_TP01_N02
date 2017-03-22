<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="details.aspx.cs" Inherits="M17_TP01_N02.Details" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div runat="server" id="divProduct" class="container">
        <asp:Image runat="server" CssClass="img-responsive col-md-4" ID="imgProduct" Style="max-height: 500px;" />
        <div class="col-md-8">
            <h1 runat="server" id="productName"></h1>
            <h4 runat="server" id="shortDescription"></h4>
            <strong>
                <h4 runat="server" id="price" class="col-md-6"></h4>
            </strong>
            <div class="input-group col-md-6">
                <asp:TextBox runat="server" TextMode="Number" min="1" Text="1" CssClass=" form-control" ID="txtQtd" OnTextChanged="OnTextChanged" AutoPostBack="True"></asp:TextBox>
                <div class="input-group-btn">
                    <asp:Button runat="server" CssClass="btn btn-info" Text="Adicionar ao Carrinho" OnClick="OnClick" />
                </div>
            </div>
            <h5 runat="server" id="brandCategory"></h5>
            <p runat="server" id="longDescription"></p>
            <div runat="server" id="warnings"></div>
        </div>
    </div>
    <h1 class="container">Comentários</h1>
    <div class="container" id="divComments" runat="server">
        <div class="form-horizontal">
            <h3>Adicione o seu comentário!</h3>
            <asp:TextBox runat="server" CssClass="form-control" placeholder="Título" ID="txtTitle"></asp:TextBox>
            <br />
            <asp:TextBox runat="server" CssClass="form-control" placeholder="Comentário" ID="txtComment"></asp:TextBox>
            <br />
            <asp:Button runat="server" CssClass="btn btn-success" ID="btnAddComment" Text="Adicionar" OnClick="btnAddComment_OnClick" />
        </div>
        <br />
        <asp:Label runat="server" ID="lblResultComment"></asp:Label>
    </div>
    <div runat="server" id="divAllComments" class="container">
        <h3>Veja outros comentários</h3>
    </div>
</asp:Content>
