<%@ Page Title="" Language="C#" MasterPageFile="~/painel/MasterPagePainel.Master" AutoEventWireup="true" CodeBehind="admin.aspx.cs" Inherits="M17_TP01_N02.painel.admin" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div runat="server" id="divAddProducts">
        <h1>Adicionar Produtos</h1>
        <div class="form-group col-md-12">
            <label for="ContentPlaceHolder1_txtProductName">Nome do Produto:</label>
            <asp:TextBox runat="server" CssClass="form-control" ID="txtProductName" placeholder="Nome do Produto"></asp:TextBox>
        </div>
        <div class="form-group col-md-12">
            <label for="ContentPlaceHolder1_txtShortDescription">Descrição Curta (70 carateres)</label>
            <asp:TextBox runat="server" CssClass="form-control" MaxLength="70" ID="txtShortDescription" placeholder="Descrição Curta"></asp:TextBox>
        </div>
        <div class="form-group col-md-12">
            <label for="ContentPlaceHolder1_txtLongDescription">Descrição Longa</label>
            <asp:TextBox runat="server" CssClass="form-control" TextMode="MultiLine" Columns="5" style="resize:none;" ID="txtLongDescription" placeholder="Descrição Longa"></asp:TextBox>
        </div>
        <div class="form-group col-md-12">
            <label for="ContentPlaceHolder1_txtWarnings">Alertas</label>
            <asp:TextBox runat="server" CssClass="form-control" TextMode="MultiLine" Columns="5" style="resize:none;" ID="txtWarnings" placeholder="Alertas"></asp:TextBox>
        </div>
        <div class="form-group col-md-3">
            <label for="ContentPlaceHolder1_txtPrice">Preço</label>
            <asp:TextBox runat="server" CssClass="form-control" ID="txtPrice" placeholder="Preço"></asp:TextBox>
        </div>
        <div class="form-group col-md-3">
            <label for="ContentPlaceHolder1_txtBrand">Marca</label>
            <asp:DropDownList runat="server" CssClass="form-control" ID="optBrand" placeholder="Marca"></asp:DropDownList>
        </div>
        <div class="form-group col-md-3">
            <label for="ContentPlaceHolder1_txtCategory">Categoria</label>
            <asp:DropDownList runat="server" CssClass="form-control" ID="optCategory" placeholder="Categoria"></asp:DropDownList>
        </div>
        <div class="form-group col-md-3">
            <label for="ContentPlaceHolder1_txtStock">Stock</label>
            <asp:TextBox runat="server" TextMode="Number" CssClass="form-control" ID="txtStock" placeholder="Stock"></asp:TextBox>
        </div>
        <asp:Button runat="server" Text="Adicionar" CssClass="btn btn-success" ID="btnAddProduct" OnClick="btnAddProduct_OnClick"/>
    </div>
</asp:Content>
