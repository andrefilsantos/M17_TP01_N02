using System;
using System.Data;
using System.Linq;
using M17_TP01_N02.Modal;

namespace M17_TP01_N02
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            UpdateLatestProducts(Database.Instance.LatestProducts());
        }

        private void UpdateLatestProducts(DataTable data)
        {
            if (data == null || data.Rows.Count == 0)
            {
                divLatestProducts.InnerHtml = "Não há nodidades :(";
                return;
            }
            var inner = data.Rows.Cast<DataRow>().Aggregate("", (current, item) => current + $@"
                <div class='item col-md-3'>
                    <div class='thumbnail'>
                    <img class='group list-group-image' src='images/{item[9]}.jpg' alt='' />
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
                                <a class='btn btn-success' href='#'>Comprar</a>
                            </div>
                            </div>
                        </div>
                    </div>
                </div>
            ");
            divLatestProducts.InnerHtml = inner;
        }
    }
}