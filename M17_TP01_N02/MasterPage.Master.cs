using System;
using System.Web;
using System.Web.UI;

namespace M17_TP01_N02 {
    public partial class Site1 : MasterPage {
        protected void Page_Load(object sender, EventArgs e) {
            var cookie = Request.Cookies["cookies"];
            if (cookie != null)
                div_aviso.Visible = false;

        }

        protected void btCookie_OnClick(object sender, EventArgs e) {
            var cookie = new HttpCookie("cookies", "1") { Expires = DateTime.Now.AddYears(1) };
            Response.Cookies.Add(cookie);
            div_aviso.Visible = false;
        }

        protected void OnClick(object sender, EventArgs e) {
            Response.Redirect($"search.aspx?s={txtSearch.Text}");
        }
    }
}