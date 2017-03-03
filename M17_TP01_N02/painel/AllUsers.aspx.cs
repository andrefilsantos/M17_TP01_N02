using System;
using System.Web.UI.WebControls;
using M17_TP01_N02.Modal;

namespace M17_TP01_N02
{
    public partial class AllUsers : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            UpdateList();
        }

        private void UpdateList()
        {
            gvUsers.Columns.Clear();
            gvUsers.DataSource = null;
            gvUsers.DataBind();

            gvUsers.DataSource = Database.Instance.AllUsers();

            //botão emprestar
            /*var btEmprestar = new ButtonField
            {
                HeaderText = "Fazer empréstimo",
                Text = "Emprestar",
                ButtonType = ButtonType.Button,
                CommandName = "emprestar"
            };
            gvUsers.Columns.Add(btEmprestar);*/

            gvUsers.DataBind();
        }
    }
}