<%@ Page Title="" Language="C#" MasterPageFile="~/painel/MasterPagePainel.Master" AutoEventWireup="true" CodeBehind="admin.aspx.cs" Inherits="M17_TP01_N02.painel.Admin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div runat="server" id="divProducts">
        <div class="col-md-3" id="sidebarProduct">
            <ul class="sidebar">
                <a href="admin.aspx?tab=products&sub=addProduct">
                    <li <% if (Request["sub"] == "addProduct") Response.Write("class='active'"); %>>Adicionar Produto</li>
                </a>
                <a href="admin.aspx?tab=products&sub=allProducts">
                    <li <% if (Request["sub"] == "allProducts") Response.Write("class='active'"); %>>Todos os Produtos</li>
                </a>
                <a href="admin.aspx?tab=products&sub=activeProducts">
                    <li <% if (Request["sub"] == "activeProducts") Response.Write("class='active'"); %>>Produtos Ativos</li>
                </a>
                <a href="admin.aspx?tab=products&sub=inactiveProducts">
                    <li <% if (Request["sub"] == "inactiveProducts") Response.Write("class='active'"); %>>Produtos Inativos</li>
                </a>
                <a href="admin.aspx?tab=products&sub=withoutStockProducts">
                    <li <% if (Request["sub"] == "withoutStockProducts") Response.Write("class='active'"); %>>Produtos sem Stock</li>
                </a>
            </ul>
        </div>
        <div class="col-md-9" id="divContentProduct">
            <div id="divAddProducts" class="form-horizontal" runat="server">
                <h1>Adicionar Produtos</h1>
                <br />
                <asp:label runat="server" id="lblResultProducts"></asp:label>
                <br />
                <div class="form-group">
                    <label for="ContentPlaceHolder1_txtProductName" class="col-sm-2 control-label">Nome do Produto</label>
                    <div class="col-sm-10">
                        <asp:textbox runat="server" cssclass="form-control" id="txtProductName" placeholder="Nome do Produto"></asp:textbox>
                    </div>
                </div>
                <div class="form-group">
                    <label for="ContentPlaceHolder1_txtShortDescription" class="col-sm-2 control-label">Descrição Curta</label>
                    <div class="col-sm-10">
                        <asp:textbox runat="server" cssclass="form-control" maxlength="70" id="txtShortDescription" placeholder="Descrição Curta"></asp:textbox>
                    </div>
                </div>
                <div class="form-group">
                    <label for="ContentPlaceHolder1_txtLongDescription" class="col-sm-2 control-label">Descrição Longa</label>
                    <div class="col-sm-10">
                        <asp:textbox runat="server" cssclass="form-control" textmode="MultiLine" columns="5" style="resize: none;" id="txtLongDescription" placeholder="Descrição Longa"></asp:textbox>
                    </div>
                </div>
                <div class="form-group">
                    <label for="ContentPlaceHolder1_txtWarnings" class="col-sm-2 control-label">Alertas</label>
                    <div class="col-sm-10">
                        <asp:textbox runat="server" cssclass="form-control" textmode="MultiLine" columns="5" style="resize: none;" id="txtWarnings" placeholder="Alertas"></asp:textbox>
                    </div>
                </div>
                <div class="form-group">
                    <label for="ContentPlaceHolder1_txtPrice" class="col-sm-2 control-label">Preço</label>
                    <div class="col-sm-10">
                        <asp:textbox runat="server" cssclass="form-control" id="txtPrice" placeholder="Preço"></asp:textbox>
                    </div>
                </div>
                <div class="form-group">
                    <label for="ContentPlaceHolder1_txtBrand" class="col-sm-2 control-label">Marca</label>
                    <div class="col-sm-10">
                        <asp:dropdownlist runat="server" cssclass="form-control" id="optBrand" placeholder="Marca"></asp:dropdownlist>
                    </div>
                </div>
                <div class="form-group">
                    <label for="ContentPlaceHolder1_txtCategory" class="col-sm-2 control-label">Categoria</label>
                    <div class="col-sm-10">
                        <asp:dropdownlist runat="server" cssclass="form-control" id="optCategory" placeholder="Categoria"></asp:dropdownlist>
                    </div>
                </div>
                <div class="form-group">
                    <label for="ContentPlaceHolder1_txtStock" class="col-sm-2 control-label">Stock</label>
                    <div class="col-sm-10">
                        <asp:textbox runat="server" textmode="Number" cssclass="form-control" id="txtStock" placeholder="Stock"></asp:textbox>
                    </div>
                </div>
                <div class="form-group">
                    <label for="ContentPlaceHolder1_file" class="col-sm-2 control-label">Imagem</label>
                    <div class="col-sm-10">
                        <asp:fileupload runat="server" id="fuImageProduct" />
                    </div>
                </div>
                <div class="form-group">
                    <div class="col-sm-offset-2 col-sm-10">
                        <asp:button runat="server" text="Adicionar" cssclass="btn btn-success pull-right" id="btnAddProduct" onclick="btnAddProduct_OnClick" />
                    </div>
                </div>
            </div>
            <br />
            <div id="divAllProducts" runat="server">
                <h1>Todos os Produtos</h1>
                <asp:gridview runat="server" id="gvProducts" cssclass="table table-responsive table-bordered table-hover col-md-12"></asp:gridview>
            </div>
            <div id="divActiveProducts" runat="server">
                <h1>Produtos Ativos</h1>
                <asp:gridview runat="server" id="gvActiveProducts" cssclass="table table-responsive table-bordered table-hover col-md-12"></asp:gridview>
            </div>
            <div id="divInactiveProducts" runat="server">
                <h1>Produtos Inativos</h1>
                <asp:gridview runat="server" id="gvInactiveProducts" cssclass="table table-responsive table-bordered table-hover col-md-12" Visible="True"></asp:gridview>
            </div>
            <div id="divProductsWithoutStock" runat="server">
                <h1>Produtos Ativos</h1>
                <asp:gridview runat="server" id="gvProductsWithoutStock" cssclass="table table-responsive table-bordered table-hover col-md-12" Visible="True"></asp:gridview>
            </div>
        </div>
    </div>
    <div runat="server" id="divCategories">
        <div class="col-md-3" id="divSidbarCategories">
            <ul class="sidebar">
                <li>Adicionar Categoria</li>
                <li>Todas as Categorias</li>
                <li>Categorias Ativas</li>
                <li>Categorias Inativas</li>
            </ul>
        </div>
        <div class="col-md-9">
            <div id="divAddCategories" class="form-horizontal">
                <h1>Adicionar Categoria</h1>
                <br />
                <asp:label runat="server" id="lblResultCategory"></asp:label>
                <br />
                <div class="form-group">
                    <label for="ContentPlaceHolder1_txtNameCategory" class="col-sm-2 control-label">Categoria</label>
                    <div class="col-sm-10">
                        <asp:textbox runat="server" cssclass="form-control" id="txtNameCategory" placeholder="Nome da Categoria"></asp:textbox>
                    </div>
                </div>
                <div class="form-group">
                    <label for="ContentPlaceHolder1_txtNameCategory" class="col-sm-2 control-label">Descrição</label>
                    <div class="col-sm-10">
                        <asp:textbox runat="server" cssclass="form-control" textmode="MultiLine" columns="5" style="resize: none;" id="txtDescriptionCategory" placeholder="Descrição"></asp:textbox>
                    </div>
                </div>
                <div class="form-group">
                    <div class="col-sm-offset-2 col-sm-10">
                        <asp:button runat="server" text="Adicionar" cssclass="btn btn-success" id="btnAddCategory" onclick="btnAddCategory_OnClick" />
                    </div>
                </div>
            </div>
            <div style="clear: both"></div>
            <br />
            <br />
            <div id="divAllCategories" runat="server">
                <h1>Todas as Categorias</h1>
                <asp:gridview runat="server" id="gvAllCategories" cssclass="table table-responsive table-bordered table-hover col-md-12"></asp:gridview>
            </div>
        </div>
    </div>
    <div runat="server" id="divBrands">
        <div class="col-md-3" id="divSidbarBrands">
            <ul class="sidebar">
                <li>Adicionar Marca</li>
                <li>Todas as Marcas</li>
                <li>Marcas Ativas</li>
                <li>Marcas Inativas</li>
            </ul>
        </div>
        <div class="col-md-9">
            <div id="divAddBrands" class="form-horizontal">
                <h1>Adicionar Marcas</h1>
                <br />
                <asp:label runat="server" id="lblResultBrands"></asp:label>
                <br />
                <div class="form-group">
                    <label for="ContentPlaceHolder1_txtNameBrand" class="col-sm-2 control-label">Nome da Marca</label>
                    <div class="col-sm-10">
                        <asp:textbox runat="server" cssclass="form-control" id="txtNameBrand" placeholder="Nome da marca"></asp:textbox>
                    </div>
                </div>
                <div class="form-group">
                    <label for="ContentPlaceHolder1_txtNameCategory" class="col-sm-2 control-label">Descrição</label>
                    <div class="col-sm-10">
                        <asp:textbox runat="server" cssclass="form-control" textmode="MultiLine" columns="5" style="resize: none;" id="txtDescriptionBrand" placeholder="Descrição"></asp:textbox>
                    </div>
                </div>
                <div class="form-group">
                    <div class="col-sm-offset-2 col-sm-10">
                        <asp:button runat="server" text="Adicionar" cssclass="btn btn-success" id="btnAddBrand" onclick="btnAddBrand_OnClick" />
                    </div>
                </div>
                <br />
                <div id="divAllBrands">
                    <h1>Todas as Marcas</h1>
                    <asp:gridview runat="server" id="gvAllBrand" cssclass="table table-responsive table-bordered table-hover col-md-12"></asp:gridview>
                </div>
            </div>
        </div>
    </div>
    <div runat="server" id="divUsers">
        <div class="col-md-3" id="divSidbarUsers">
            <ul class="sidebar">
                <li>Adicionar Utilizador</li>
                <li>Todas os Utilizadores</li>
                <li>Utilizadores Ativos</li>
                <li>Utilizadores Inativos</li>
            </ul>
        </div>
        <div class="col-md-9">
            <div id="divAddUsers" class="form-horizontal">
                <h1>Adicionar Produtos</h1>
                <br />
                <asp:label runat="server" id="lblResultUsers"></asp:label>
                <br />
                <div class="form-group">
                    <label for="ContentPlaceHolder1_txtUsername" class="col-sm-2 control-label">Nome de Utilizador</label>
                    <div class="col-sm-10">
                        <asp:textbox runat="server" cssclass="form-control" id="txtUsername" placeholder="Nome de Utilizador"></asp:textbox>
                    </div>
                </div>
                <div class="form-group">
                    <label for="ContentPlaceHolder1_txtPassword" class="col-sm-2 control-label">Password</label>
                    <div class="col-sm-10">
                        <asp:textbox runat="server" cssclass="form-control" id="txtPassword" textmode="Password" placeholder="Password"></asp:textbox>
                    </div>
                </div>
                <div class="form-group">
                    <label for="ContentPlaceHolder1_txtConfirmPassword" class="col-sm-2 control-label">Confirmar a Password</label>
                    <div class="col-sm-10">
                        <asp:textbox runat="server" cssclass="form-control" id="txtConfirmPassword" textmode="Password" placeholder="Password"></asp:textbox>
                    </div>
                </div>
                <div class="form-group">
                    <label for="ContentPlaceHolder1_txtName" class="col-sm-2 control-label">Nome</label>
                    <div class="col-sm-10">
                        <asp:textbox runat="server" cssclass="form-control" id="txtName" placeholder="Nome"></asp:textbox>
                    </div>
                </div>
                <div class="form-group">
                    <label for="ContentPlaceHolder1_txtEmail" class="col-sm-2 control-label">Email</label>
                    <div class="col-sm-10">
                        <asp:textbox runat="server" cssclass="form-control" id="txtEmail" placeholder="Email"></asp:textbox>
                    </div>
                </div>
                <div class="form-group">
                    <label for="ContentPlaceHolder1_txtBirthday" class="col-sm-2 control-label">Data de Nascimento</label>
                    <div class="col-sm-10">
                        <asp:textbox runat="server" cssclass="form-control" id="txtBirthday" textmode="Date" placeholder="Data de Nascimento"></asp:textbox>
                    </div>
                </div>
                <div class="form-group">
                    <label for="ContentPlaceHolder1_txtAddress" class="col-sm-2 control-label">Morada</label>
                    <div class="col-sm-10">
                        <asp:textbox runat="server" cssclass="form-control" id="txtAddress" placeholder="Morada"></asp:textbox>
                    </div>
                </div>
                <div class="form-group">
                    <label for="ContentPlaceHolder1_optGender" class="col-sm-2 control-label">Sexo</label>
                    <div class="col-sm-10">
                        <asp:dropdownlist runat="server" cssclass="form-control" id="optGender" placeholder="Sexo"></asp:dropdownlist>
                    </div>
                </div>
                <div class="form-group">
                    <label for="ContentPlaceHolder1_optRole" class="col-sm-2 control-label">Perfil</label>
                    <div class="col-sm-10">
                        <asp:dropdownlist runat="server" cssclass="form-control" id="optRole" placeholder="Perfil"></asp:dropdownlist>
                    </div>
                </div>
                <div class="form-group">
                    <label for="ContentPlaceHolder1_txtPhone" class="col-sm-2 control-label">Telefone</label>
                    <div class="col-sm-10">
                        <asp:textbox runat="server" cssclass="form-control" id="txtPhone" placeholder="Telefone"></asp:textbox>
                    </div>
                </div>
                <div class="form-group">
                    <label for="ContentPlaceHolder1_file" class="col-sm-2 control-label">Imagem</label>
                    <div class="col-sm-10">
                        <asp:fileupload runat="server" id="fuImageUser" />
                    </div>
                </div>
                <div class="form-group">
                    <div class="col-sm-offset-2 col-sm-10">
                        <asp:button runat="server" text="Adicionar" cssclass="btn btn-success pull-right" id="btnAddUser" onclick="btnAddUser_OnClick" />
                    </div>
                </div>
            </div>
            <br />
            <br />
            <div id="divAllUsers">
                <h1>Todos os Utilizadores</h1>
                <asp:gridview runat="server" id="gvAllUsers" cssclass="table table-responsive table-bordered table-hover col-md-12"></asp:gridview>
            </div>
        </div>
    </div>
</asp:Content>
