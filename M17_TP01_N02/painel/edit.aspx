<%@ Page Title="" Language="C#" MasterPageFile="~/painel/MasterPagePainel.Master" AutoEventWireup="true" CodeBehind="edit.aspx.cs" Inherits="M17_TP01_N02.painel.Edit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div runat="server" id="products" class="form-horizontal">
        <div class="form-group">
            <label for="ContentPlaceHolder1_txtProductName" class="col-sm-2 control-label">Nome do Produto</label>
            <div class="col-sm-10">
                <asp:TextBox runat="server" CssClass="form-control" ID="txtProductName" placeholder="Nome do Produto"></asp:TextBox>
            </div>
        </div>
        <div class="form-group">
            <label for="ContentPlaceHolder1_txtShortDescription" class="col-sm-2 control-label">Descrição Curta</label>
            <div class="col-sm-10">
                <asp:TextBox runat="server" CssClass="form-control" MaxLength="70" ID="txtShortDescription" placeholder="Descrição Curta"></asp:TextBox>
            </div>
        </div>
        <div class="form-group">
            <label for="ContentPlaceHolder1_txtLongDescription" class="col-sm-2 control-label">Descrição Longa</label>
            <div class="col-sm-10">
                <asp:TextBox runat="server" CssClass="form-control" TextMode="MultiLine" Columns="5" Style="resize: none;" ID="txtLongDescription" placeholder="Descrição Longa"></asp:TextBox>
            </div>
        </div>
        <div class="form-group">
            <label for="ContentPlaceHolder1_txtWarnings" class="col-sm-2 control-label">Alertas</label>
            <div class="col-sm-10">
                <asp:TextBox runat="server" CssClass="form-control" TextMode="MultiLine" Columns="5" Style="resize: none;" ID="txtWarnings" placeholder="Alertas"></asp:TextBox>
            </div>
        </div>
        <div class="form-group">
            <label for="ContentPlaceHolder1_txtPrice" class="col-sm-2 control-label">Preço</label>
            <div class="col-sm-10">
                <asp:TextBox runat="server" CssClass="form-control" ID="txtPrice" placeholder="Preço"></asp:TextBox>
            </div>
        </div>
        <div class="form-group">
            <label for="ContentPlaceHolder1_txtBrand" class="col-sm-2 control-label">Marca</label>
            <div class="col-sm-10">
                <asp:DropDownList runat="server" CssClass="form-control" ID="optBrand" placeholder="Marca"></asp:DropDownList>
            </div>
        </div>
        <div class="form-group">
            <label for="ContentPlaceHolder1_txtCategory" class="col-sm-2 control-label">Categoria</label>
            <div class="col-sm-10">
                <asp:DropDownList runat="server" CssClass="form-control" ID="optCategory" placeholder="Categoria"></asp:DropDownList>
            </div>
        </div>
        <div class="form-group">
            <label for="ContentPlaceHolder1_txtStock" class="col-sm-2 control-label">Stock</label>
            <div class="col-sm-10">
                <asp:TextBox runat="server" TextMode="Number" CssClass="form-control" ID="txtStock" placeholder="Stock"></asp:TextBox>
            </div>
        </div>
        <div class="form-group">
            <label for="ContentPlaceHolder1_file" class="col-sm-2 control-label">Imagem</label>
            <div class="col-sm-10">
                <asp:FileUpload runat="server" ID="file" />
            </div>
        </div>
        <div class="form-group">
            <div class="col-sm-offset-2 col-sm-10">
                <asp:Button runat="server" Text="Adicionar" CssClass="btn btn-success pull-right" ID="btnEditProduct" />
            </div>
        </div>
    </div>
    <div runat="server" id="brands">
        <div class="form-group">
            <label for="ContentPlaceHolder1_txtNameBrand" class="col-sm-2 control-label">Nome da Marca</label>
            <div class="col-sm-10">
                <asp:TextBox runat="server" CssClass="form-control" ID="txtNameBrand" placeholder="Nome da marca"></asp:TextBox>
            </div>
        </div>
        <div class="form-group">
            <label for="ContentPlaceHolder1_txtNameCategory" class="col-sm-2 control-label">Descrição</label>
            <div class="col-sm-10">
                <asp:TextBox runat="server" CssClass="form-control" TextMode="MultiLine" Columns="5" Style="resize: none;" ID="txtDescriptionBrand" placeholder="Descrição"></asp:TextBox>
            </div>
        </div>
        <div class="form-group">
            <div class="col-sm-offset-2 col-sm-10">
                <asp:Button runat="server" Text="Adicionar" CssClass="btn btn-success" ID="btnEditBrand" />
            </div>
        </div>
    </div>
    <div runat="server" id="categories">
        <div class="form-group">
            <label for="ContentPlaceHolder1_txtNameCategory" class="col-sm-2 control-label">Categoria</label>
            <div class="col-sm-10">
                <asp:TextBox runat="server" CssClass="form-control" ID="txtNameCategory" placeholder="Nome da Categoria"></asp:TextBox>
            </div>
        </div>
        <div class="form-group">
            <label for="ContentPlaceHolder1_txtNameCategory" class="col-sm-2 control-label">Descrição</label>
            <div class="col-sm-10">
                <asp:TextBox runat="server" CssClass="form-control" TextMode="MultiLine" Columns="5" Style="resize: none;" ID="txtDescriptionCategory" placeholder="Descrição"></asp:TextBox>
            </div>
        </div>
        <div class="form-group">
            <div class="col-sm-offset-2 col-sm-10">
                <asp:Button runat="server" Text="Adicionar" CssClass="btn btn-success" ID="btnEditCategory"/>
            </div>
        </div>
    </div>
    <div runat="server" id="user" class="form-horizontal">
        <h1>Editar Utilizador</h1>
        <div class="form-group">
            <label for="ContentPlaceHolder1_txtUsername" class="col-sm-2 control-label">Nome de Utilizador</label>
            <div class="col-sm-10">
                <asp:TextBox runat="server" CssClass="form-control" ID="txtUsername" placeholder="Nome de Utilizador" ReadOnly="True"></asp:TextBox>
            </div>
        </div>
        <div class="form-group">
            <label for="ContentPlaceHolder1_txtPassword" class="col-sm-2 control-label">Password</label>
            <div class="col-sm-10">
                <asp:TextBox runat="server" CssClass="form-control" ID="txtPassword" TextMode="Password" placeholder="Password"></asp:TextBox>
            </div>
        </div>
        <div class="form-group">
            <label for="ContentPlaceHolder1_txtConfirmPassword" class="col-sm-2 control-label">Confirmar a Password</label>
            <div class="col-sm-10">
                <asp:TextBox runat="server" CssClass="form-control" ID="txtConfirmPassword" TextMode="Password" placeholder="Password"></asp:TextBox>
            </div>
        </div>
        <div class="form-group">
            <label for="ContentPlaceHolder1_txtName" class="col-sm-2 control-label">Nome</label>
            <div class="col-sm-10">
                <asp:TextBox runat="server" CssClass="form-control" ID="txtName" placeholder="Nome"></asp:TextBox>
            </div>
        </div>
        <div class="form-group">
            <label for="ContentPlaceHolder1_txtEmail" class="col-sm-2 control-label">Email</label>
            <div class="col-sm-10">
                <asp:TextBox runat="server" CssClass="form-control" ID="txtEmail" placeholder="Email"></asp:TextBox>
            </div>
        </div>
        <div class="form-group">
            <label for="ContentPlaceHolder1_txtBirthday" class="col-sm-2 control-label">Data de Nascimento</label>
            <div class="col-sm-10">
                <asp:TextBox runat="server" CssClass="form-control" ID="txtBirthday" TextMode="Date" placeholder="Data de Nascimento"></asp:TextBox>
            </div>
        </div>
        <div class="form-group">
            <label for="ContentPlaceHolder1_txtAddress" class="col-sm-2 control-label">Morada</label>
            <div class="col-sm-10">
                <asp:TextBox runat="server" CssClass="form-control" ID="txtAddress" placeholder="Morada"></asp:TextBox>
            </div>
        </div>
        <div class="form-group">
            <label for="ContentPlaceHolder1_optGender" class="col-sm-2 control-label">Sexo</label>
            <div class="col-sm-10">
                <asp:DropDownList runat="server" CssClass="form-control" ID="optGender" placeholder="Sexo"></asp:DropDownList>
            </div>
        </div>
        <div class="form-group">
            <label for="ContentPlaceHolder1_optRole" class="col-sm-2 control-label">Perfil</label>
            <div class="col-sm-10">
                <asp:DropDownList runat="server" CssClass="form-control" ID="optRole" placeholder="Perfil"></asp:DropDownList>
            </div>
        </div>
        <div class="form-group">
            <label for="ContentPlaceHolder1_txtPhone" class="col-sm-2 control-label">Telefone</label>
            <div class="col-sm-10">
                <asp:TextBox runat="server" CssClass="form-control" ID="txtPhone" placeholder="Telefone"></asp:TextBox>
            </div>
        </div>
        <div class="form-group">
            <label for="ContentPlaceHolder1_file" class="col-sm-2 control-label">Imagem</label>
            <div class="col-sm-10">
                <asp:FileUpload runat="server" ID="fuImageUser" />
            </div>
        </div>
        <div class="form-group">
            <div class="col-sm-offset-2 col-sm-10">
                <asp:Button runat="server" Text="Atualizar" CssClass="btn btn-success pull-right" ID="btnEditUser" OnClick="btnEditUser_OnClick" />
            </div>
        </div>
        <asp:Label runat="server" ID="lblResult"></asp:Label>
    </div>
</asp:Content>
