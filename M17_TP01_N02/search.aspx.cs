using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using M17_TP01_N02.Modal;

namespace M17_TP01_N02 {
    public partial class search : System.Web.UI.Page {
        protected void Page_Load(object sender, EventArgs e) {
            UdpateList(Request["s"]);
        }

        private void UdpateList(string s) {
            try
            {
                var data = Database.Instance.SearchProductByName(s);
                if (data == null || data.Rows.Count == 0)
                    throw new Exception("Não há produtos.");
                var inner = data.Rows.Cast<DataRow>().Aggregate("", (current, item) => current + $@"
                <div class='item col-md-3'>
                    <div class='thumbnail'>
                    <img class='group list-group-image' src='images/products/{item[10]}' alt='' />
                    <div class='caption'>
                        <h4 class='group inner list-group-item-heading'>
                        {item[1]}</h4>
                        <p class='group inner list-group-item-text'>
                        {item[2]}</p>
                        <div class='row'>
                            <div class='col-xs-12 col-md-6'>
                                <p class='lead'>
                                {decimal.Parse(item[4].ToString()):C}</p>
                            </div>
                            <div class='col-xs-12 col-md-6'>
                                <a class='btn btn-success' href='details.aspx?id={item[0]}'>Detalhes</a>
                            </div>
                            </div>
                        </div>
                    </div>
                </div>
            ");
                divProducts.InnerHtml = inner;
            } catch (Exception e) {
                divProducts.InnerHtml = e.Message;
                divProducts.Attributes["class"] = "alert alert-danger";
            }
        }
    }
}