using System;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.WebControls;
using M17_TP01_N02.Modal;

namespace M17_TP01_N02.painel {
    public partial class Edit : Page {
        protected void Page_Load(object sender, EventArgs e) {

            if (Session["role"] == null || !Session["role"].Equals("0"))
                Response.Redirect("../index.aspx");
            if (Request["table"] != null) {
                if (Request["table"] == "users") {
                    products.Visible = false;
                    brands.Visible = false;
                    categories.Visible = false;
                    user.Visible = true;
                    var data = Database.Instance.UserInfo(int.Parse(Request["id"]));
                    txtUsername.Text = data.Rows[0][1].ToString();
                    txtName.Text = data.Rows[0][4].ToString();
                    txtEmail.Text = data.Rows[0][3].ToString();
                    txtBirthday.Text = DateTime.Parse(data.Rows[0][5].ToString()).ToString("dd/MM/yyyy");
                    txtAddress.Text = data.Rows[0][6].ToString();
                    txtPhone.Text = data.Rows[0][8].ToString();
                    UpdateOptGender();
                    optGender.SelectedIndex = int.Parse(data.Rows[0][7].ToString());
                    UpdateOptProfile();
                    optRole.SelectedIndex = int.Parse(data.Rows[0][13].ToString());
                } else if (Request["table"] == "products") {
                    products.Visible = true;
                    brands.Visible = false;
                    categories.Visible = false;
                    user.Visible = false;
                } else if (Request["table"] == "brands") {
                    products.Visible = false;
                    brands.Visible = true;
                    categories.Visible = false;
                    user.Visible = false;
                } else if (Request["table"] == "categories") {
                    products.Visible = false;
                    brands.Visible = false;
                    categories.Visible = true;
                    user.Visible = false;
                }
            } else {
                throw new Exception();
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

        protected void btnEditUser_OnClick(object sender, EventArgs e) {
            try {
                if (txtUsername.Text == string.Empty)
                    throw new Exception("Tem de fornecer um nome de utilizador");
                if (Database.Instance.UsernameExist(txtUsername.Text))
                    throw new Exception("O username já existe");
                if (txtPassword.Text != txtConfirmPassword.Text)
                    throw new Exception("As passwords têm de ser iguais");
                if (txtPassword.Text.Length < 8)
                    throw new Exception("A password é demasiado insegura");
                var result = Database.Instance.EditUser(int.Parse(Request["id"]), txtUsername.Text, txtPassword.Text, txtEmail.Text, txtName.Text,
                    txtBirthday.Text, txtAddress.Text, optGender.SelectedValue, txtPhone.Text, "", int.Parse(optRole.SelectedValue));
                if (fuImageUser.PostedFile.ContentLength > 0 && fuImageUser.PostedFile.ContentType == "image/jpeg") {
                    var filePath = Server.MapPath("~/images/") + Request["id"] + ".jpg";
                    fuImageUser.SaveAs(filePath);
                }
                if (result) {
                    lblResult.CssClass = "alert alert-success col-md-12";
                    lblResult.Text = "Utilizador editado com sucesso.";
                } else
                    throw new Exception("Erro");
            } catch (Exception exception) {
                lblResult.CssClass = "alert alert-danger col-md-12";
                lblResult.Text = exception.Message;
            }
        }
    }
}