using System;
using System.Web.UI;
using M17_TP01_N02.Modal;

namespace M17_TP01_N02.painel {
    public partial class Delete : Page {
        protected void Page_Load(object sender, EventArgs e) {

            if (Session["role"] == null || !Session["role"].Equals("0"))
                Response.Redirect("../index.aspx");
            var table = Request["table"];
            var id = int.Parse(Request["id"]);
            var text = "";
            try {
                if (Database.Instance.Delete(table, id)) {
                    if (table == "users")
                        text += "Utilizador eliminado";
                    else if (table == "brands")
                        text += "Marca eliminada";
                    else if (table == "products")
                        text += "Produto eliminado";
                    else if (table == "categories")
                        text += "Categoria eliminada";
                    text += " com sucesso";
                    lblResult.Text = text;
                    lblResult.CssClass = "alert alert-success col-md-12";
                } else {
                    throw new Exception();
                }
            } catch (Exception err) {
                text += "Ocorreu um erro ao eliminar ";
                if (table == "users")
                    text += " o utilizador";
                else if (table == "brands")
                    text += "a marca";
                else if (table == "products")
                    text += "o produto";
                else if (table == "categories")
                    text += "a categoria";
                text += ". <br /> Por favor, tente mais tarde.";
                text += "<br /><br /><strong>Detalhes do erro: </strong><br/>" + err.Message;
                lblResult.Text = text;
                lblResult.CssClass = "alert alert-danger col-md-12";
            }

        }
    }
}