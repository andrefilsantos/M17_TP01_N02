using System;
using System.Web.UI;
using M17_TP01_N02.Modal;

namespace M17_TP01_N02
{
    public partial class Login : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {


        }

        protected void btnLogin_OnClick(object sender, EventArgs e)
        {
            try
            {
                var dados = Database.Instance.Login(txtUsername.Text, txtPassword.Text);
                if (dados == null || dados.Rows.Count == 0)
                    throw new Exception("Username ou Password inválidos.");
                Session["username"] = dados.Rows[0]["username"].ToString();
                Session["role"] = dados.Rows[0]["role"].ToString();
                Session["id"] = dados.Rows[0]["idUser"].ToString();
                Response.Redirect(Session["role"].Equals("0") ? "painel/admin.aspx?tab=products" : "painel/user.aspx");
            }
            catch (Exception erro)
            {
                lblError.Text = erro.Message;
                lblError.CssClass = "alert alert-danger";
            }
        }
    }
}