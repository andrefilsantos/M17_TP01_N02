using System;
using System.Web.UI;

namespace M17_TP01_N02.painel
{
    public partial class Index : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(Session["role"].Equals("0"))
                Response.Redirect("admin.aspx");
            else if (Session["role"].Equals("1"))
                Response.Redirect("user.aspx");
        }
    }
}