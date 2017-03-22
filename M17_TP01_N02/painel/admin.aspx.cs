using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using M17_TP01_N02.Modal;

namespace M17_TP01_N02.painel {
    public partial class Admin : Page {
        protected void Page_Load(object sender, EventArgs e) {
            if(Session["role"] == null || !Session["role"].Equals("0"))
                Response.Redirect("../index.aspx");
            gvAllUsers.RowCommand += gvAllUsers_RowCommand;
            gvProducts.RowCommand += gvProducts_RowCommand;
            gvAllCategories.RowCommand += gvAllCategories_RowCommand;
            gvAllBrand.RowCommand += gvAllBrand_RowCommand;
            if (IsPostBack) return;
            try {
                if (Request["tab"] == null || Request["sub"] == null)
                    throw new Exception();
                if (Request["tab"].Contains("products")) {
                    divCategories.Visible = false;
                    divBrands.Visible = false;
                    divUsers.Visible = false;
                    UpdateOptBrand();
                    UpdateOptCategories();
                    UpdateGvAllProducts();
                    if (Request["sub"] == "addProduct") {
                        divAddProducts.Visible = true;
                        divAllProducts.Visible = false;
                        divActiveProducts.Visible = false;
                        divInactiveProducts.Visible = false;
                        divProductsWithoutStock.Visible = false;
                    } else if (Request["sub"] == "allProducts") {
                        divAddProducts.Visible = false;
                        divAllProducts.Visible = true;
                        divActiveProducts.Visible = false;
                        divInactiveProducts.Visible = false;
                        divProductsWithoutStock.Visible = false;
                    } else if (Request["sub"] == "activeProducts") {
                        divAddProducts.Visible = false;
                        divAllProducts.Visible = false;
                        divActiveProducts.Visible = true;
                        divInactiveProducts.Visible = false;
                        divProductsWithoutStock.Visible = false;
                        gvActiveProducts.DataSource = Database.Instance.AllActiveProducts();
                        gvActiveProducts.DataBind();
                    } else if (Request["sub"] == "inactiveProducts") {
                        divAddProducts.Visible = false;
                        divAllProducts.Visible = false;
                        divActiveProducts.Visible = false;
                        divInactiveProducts.Visible = true;
                        divProductsWithoutStock.Visible = false;
                        gvInactiveProducts.DataSource = Database.Instance.AllInactiveProducts();
                        gvActiveProducts.DataBind();
                    } else if (Request["sub"] == "withoutStock")
                    {
                        divAddProducts.Visible = false;
                        divAllProducts.Visible = false;
                        divActiveProducts.Visible = false;
                        divInactiveProducts.Visible = false;
                        divProductsWithoutStock.Visible = true;
                        gvProductsWithoutStock.DataSource = Database.Instance.AllWhithoutStockProducts();
                        gvProductsWithoutStock.DataBind();
                    }
                } else if (Request["tab"].Contains("categories")) {
                    divProducts.Visible = false;
                    divBrands.Visible = false;
                    divUsers.Visible = false;
                    UpdateGvAllCategories();
                } else if (Request["tab"].Contains("brands")) {
                    divProducts.Visible = false;
                    divCategories.Visible = false;
                    divUsers.Visible = false;
                    UpdateGvAllBrands();
                } else if (Request["tab"].Contains("users")) {
                    divProducts.Visible = false;
                    divBrands.Visible = false;
                    divCategories.Visible = false;
                    UpdateGvAllUsers();
                    UpdateOptProfile();
                    UpdateOptGender();
                } else
                    throw new Exception();
            } catch (Exception ee) {
                Response.Redirect("admin.aspx?tab=products&sub=allProducts");
            }
        }

        private void UpdateOptGender() {
            optGender.Items.Clear();
            optGender.Items.Add(new ListItem("Masculino", "0"));
            optGender.Items.Add(new ListItem("Feminino", "1"));
            optGender.Items.Add(new ListItem("Outro", "2"));
        }

        private void UpdateOptProfile() {
            optRole.Items.Clear();
            optRole.Items.Add(new ListItem("Administrador", "0"));
            optRole.Items.Add(new ListItem("Cliente", "1"));
        }

        private void gvAllBrand_RowCommand(object sender, GridViewCommandEventArgs e) {
            var linha = int.Parse(e.CommandArgument as string);
            var id = int.Parse(gvAllBrand.Rows[linha].Cells[2].Text);
            if (e.CommandName == "eliminar") {
                Response.Redirect($"delete.aspx?table=brands&id={id}");
            } else if (e.CommandName == "editar") {
                Response.Redirect($"edit.aspx?table=brands&id={id}");
            }
        }

        private void gvAllCategories_RowCommand(object sender, GridViewCommandEventArgs e) {
            var linha = int.Parse(e.CommandArgument as string);
            var id = int.Parse(gvAllCategories.Rows[linha].Cells[2].Text);
            if (e.CommandName == "eliminar") {
                Response.Redirect($"delete.aspx?table=categories&id={id}");
            } else if (e.CommandName == "editar") {
                Response.Redirect($"edit.aspx?table=categories&id={id}");
            }
        }

        private void gvProducts_RowCommand(object sender, GridViewCommandEventArgs e) {
            var linha = int.Parse(e.CommandArgument as string);
            var id = int.Parse(gvProducts.Rows[linha].Cells[2].Text);
            if (e.CommandName == "eliminar") {
                Response.Redirect($"delete.aspx?table=products&id={id}");
            } else if (e.CommandName == "editar") {
                Response.Redirect($"edit.aspx?table=products&id={id}");
            }
        }

        private void gvAllUsers_RowCommand(object sender, GridViewCommandEventArgs e) {
            var linha = int.Parse(e.CommandArgument as string);
            var id = int.Parse(gvAllUsers.Rows[linha].Cells[2].Text);
            if (e.CommandName == "eliminar") {
                Response.Redirect($"delete.aspx?table=users&id={id}");
            } else if (e.CommandName == "editar") {
                Response.Redirect($"edit.aspx?table=users&id={id}");
            }
        }


        private void UpdateOptBrand() {
            optBrand.Items.Clear();
            var brands = Database.Instance.AllBrands();
            foreach (DataRow item in brands.Rows)
                optBrand.Items.Add(new ListItem(item[1].ToString(), item[0].ToString()));
        }

        private void UpdateOptCategories() {
            optCategory.Items.Clear();
            var categories = Database.Instance.AllCategories();
            foreach (DataRow item in categories.Rows)
                optCategory.Items.Add(new ListItem(item[1].ToString(), item[0].ToString()));
        }

        private void UpdateGvAllProducts() {
            gvProducts.Columns.Clear();
            gvProducts.DataSource = null;
            gvProducts.DataBind();

            gvProducts.DataSource = Database.Instance.AllProducts();
            var bfReceberLivro = new ButtonField {
                HeaderText = "Eliminar",
                Text = "Eliminar",
                ButtonType = ButtonType.Button,
                ControlStyle = { CssClass = "btn btn-default" },
                CommandName = "eliminar"
            };
            gvProducts.Columns.Add(bfReceberLivro);

            var bfEnviarEmail = new ButtonField {
                HeaderText = "Editar",
                Text = "Editar",
                ButtonType = ButtonType.Button,
                ControlStyle = { CssClass = "btn btn-default" },
                CommandName = "editar"
            };
            gvProducts.Columns.Add(bfEnviarEmail);

            gvProducts.DataBind();
        }

        private void UpdateGvAllCategories() {
            gvAllCategories.Columns.Clear();
            gvAllCategories.DataSource = null;
            gvAllCategories.DataBind();

            gvAllCategories.DataSource = Database.Instance.AllCategories();
            var bfReceberLivro = new ButtonField {
                HeaderText = "Eliminar",
                Text = "Eliminar",
                ButtonType = ButtonType.Button,
                ControlStyle = { CssClass = "btn btn-default" },
                CommandName = "eliminar"
            };
            gvAllCategories.Columns.Add(bfReceberLivro);

            var bfEnviarEmail = new ButtonField {
                HeaderText = "Editar",
                Text = "Editar",
                ButtonType = ButtonType.Button,
                ControlStyle = { CssClass = "btn btn-default" },
                CommandName = "editar"
            };
            gvAllCategories.Columns.Add(bfEnviarEmail);

            gvAllCategories.DataBind();
        }

        private void UpdateGvAllBrands() {
            gvAllBrand.Columns.Clear();
            gvAllBrand.DataSource = null;
            gvAllBrand.DataBind();

            gvAllBrand.DataSource = Database.Instance.AllBrands();
            var bfReceberLivro = new ButtonField {
                HeaderText = "Eliminar",
                Text = "Eliminar",
                ButtonType = ButtonType.Button,
                ControlStyle = { CssClass = "btn btn-default" },
                CommandName = "eliminar"
            };
            gvAllBrand.Columns.Add(bfReceberLivro);

            var bfEnviarEmail = new ButtonField {
                HeaderText = "Editar",
                Text = "Editar",
                ButtonType = ButtonType.Button,
                ControlStyle = { CssClass = "btn btn-default" },
                CommandName = "editar"
            };
            gvAllBrand.Columns.Add(bfEnviarEmail);

            gvAllBrand.DataBind();
        }

        private void UpdateGvAllUsers() {
            gvAllUsers.Columns.Clear();
            gvAllUsers.DataSource = null;
            gvAllUsers.DataBind();

            gvAllUsers.DataSource = Database.Instance.AllUsers();
            var bfEliminar = new ButtonField {
                HeaderText = "Eliminar",
                Text = "Eliminar",
                ButtonType = ButtonType.Button,
                ControlStyle = { CssClass = "btn btn-default" },
                CommandName = "eliminar"
            };
            gvAllUsers.Columns.Add(bfEliminar);

            var bfEditar = new ButtonField {
                HeaderText = "Editar",
                Text = "Editar",
                ButtonType = ButtonType.Button,
                ControlStyle = { CssClass = "btn btn-default" },
                CommandName = "editar"
            };
            gvAllUsers.Columns.Add(bfEditar);

            gvAllUsers.DataBind();
        }

        protected void btnAddProduct_OnClick(object sender, EventArgs e) {
            try {
                if (txtProductName.Text == string.Empty)
                    throw new Exception("Tem de inserir o nome do produto");
                if (txtShortDescription.Text == string.Empty)
                    throw new Exception("Pelo menos, tem de indicar uma descrição curta");
                if (double.Parse(txtPrice.Text) < 0)
                    throw new Exception("O preço inserido não é válido");
                var g = Guid.NewGuid();
                if (fuImageProduct.PostedFile.ContentLength > 0 && fuImageProduct.PostedFile.ContentType == "image/jpeg") {
                    var ficheiro = Server.MapPath(@"~\images\products\");
                    ficheiro += g + ".jpg";
                    fuImageProduct.SaveAs(ficheiro);
                }
                if (Database.Instance.AddProduct(txtProductName.Text, txtShortDescription.Text, txtLongDescription.Text,
                   double.Parse(txtPrice.Text),
                   optBrand.SelectedValue, optCategory.SelectedValue, int.Parse(txtStock.Text), txtWarnings.Text, g + ".jpg")) {
                    UpdateGvAllProducts();
                    lblResultProducts.CssClass = "alert alert-success col-md-12";
                    lblResultProducts.Text = "Produto adicionado com sucesso.";
                    var data = Database.Instance.AllNewsletter();
                    foreach (DataRow item in data.Rows) {
                        Helper.SendMail(item[0].ToString(), $"[OPCS] Novo produto!", "Adicionamos recentemente um novo producto (" + txtProductName.Text + "). Visite o nosso site para o conhecer!");
                    }
                } else
                    throw new Exception("Erro");
            } catch (Exception exception) {
                lblResultProducts.CssClass = "alert alert-danger col-md-12";
                lblResultProducts.Text = exception.Message;
            }
        }

        protected void btnAddCategory_OnClick(object sender, EventArgs e) {
            try {
                if (txtNameCategory.Text == string.Empty)
                    throw new Exception("Tem de preencher, pelo menos, o campo nome");
                if (Database.Instance.AddCategory(txtNameCategory.Text, txtDescriptionCategory.Text)) {
                    UpdateGvAllCategories();
                    lblResultCategory.CssClass = "alert alert-success col-md-12";
                    lblResultCategory.Text = "Categoria adicionada com sucesso.";
                } else
                    throw new Exception("Erro");
            } catch (Exception exception) {
                lblResultCategory.CssClass = "alert alert-danger col-md-12";
                lblResultCategory.Text = exception.Message;
            }
        }

        protected void btnAddBrand_OnClick(object sender, EventArgs e) {
            try {
                if (txtNameBrand.Text == string.Empty)
                    throw new Exception("Tem de adicionar um nome");
                if (Database.Instance.AddBrand(txtNameBrand.Text, txtDescriptionBrand.Text)) {
                    lblResultBrands.CssClass = "alert alert-success col-md-12";
                    lblResultBrands.Text = "Marca adicionada com sucesso!";
                    UpdateGvAllBrands();
                } else
                    throw new Exception("Impossivel adicionar a marca.\nPor favor, tente mais tarde.");
            } catch (Exception exception) {
                lblResultBrands.CssClass = "alert alert-danger col-md-12";
                lblResultBrands.Text = exception.Message;
            }
        }

        protected void btnAddUser_OnClick(object sender, EventArgs e) {
            try {
                if (txtUsername.Text == string.Empty)
                    throw new Exception("Tem de fornecer um nome de utilizador");
                if (Database.Instance.UsernameExist(txtUsername.Text))
                    throw new Exception("O username já existe");
                if (txtPassword.Text != txtConfirmPassword.Text)
                    throw new Exception("As passwords têm de ser iguais");
                if (txtPassword.Text.Length < 8)
                    throw new Exception("A password é demasiado insegura");
                var result = Database.Instance.AddUser(txtUsername.Text, txtPassword.Text, txtEmail.Text, txtName.Text,
                    txtBirthday.Text, txtAddress.Text, optGender.SelectedValue, txtPhone.Text, false,
                    int.Parse(optRole.SelectedValue));
                if (fuImageUser.PostedFile.ContentLength > 0 && fuImageUser.PostedFile.ContentType == "image/jpeg") {
                    var filePath = Server.MapPath("~/images/") + result + ".jpg";
                    fuImageUser.SaveAs(filePath);

                }
                if (result > 0) {
                    UpdateGvAllUsers();
                    lblResultUsers.CssClass = "alert alert-success col-md-12";
                    lblResultUsers.Text = "Utilizador adicionado com sucesso.";
                } else
                    throw new Exception("Erro");
            } catch (Exception exception) {
                lblResultUsers.CssClass = "alert alert-danger col-md-12";
                lblResultUsers.Text = exception.Message;
            }
        }
    }
}