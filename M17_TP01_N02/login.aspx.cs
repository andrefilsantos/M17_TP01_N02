using System;
using System.Web;
using M17_TP01_N02.Modal;

namespace M17_TP01_N02
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {


        }

        protected void btnLogin_OnClick(object sender, EventArgs e)
        {
            try
            {
                if (Database.Instance.Login(txtUsername.Text, txtPassword.Text))
                {
                    var responseCookie = Session["user"];
                    if (responseCookie != null && Database.Instance.UserInfo(int.Parse(responseCookie.ToString())).Rows[0]["role"].ToString() == "0")
                        Response.Redirect("painel/admin.aspx");
                    else if (responseCookie != null && Database.Instance.UserInfo(int.Parse(responseCookie.ToString())).Rows[0]["role"].ToString() == "1")
                        Response.Redirect("painel/index.aspx");
                    else if (responseCookie != null && Database.Instance.UserInfo(int.Parse(responseCookie.ToString())).Rows[0]["role"].ToString() == "3")
                        Response.Redirect("painel/user.aspx");
                    else
                        Response.Write("<script>alert('Erro');</script>");
                }
                else
                {
                    Response.Write("<script>alert('Nome de Utilizador ou Password incorretos');</script>");
                }
                Response.Redirect(Database.Instance.Login(txtUsername.Text, txtPassword.Text)
                    ? "painel/index.aspx"
                    : "index.aspx");
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                throw;
            }
        }
    }
}