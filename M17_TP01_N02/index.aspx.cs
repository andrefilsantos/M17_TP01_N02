using System;
using System.Data;
using System.Linq;
using System.Web.UI;
using M17_TP01_N02.Modal;

namespace M17_TP01_N02 {
    public partial class WebForm1 : Page {
        protected void Page_Load(object sender, EventArgs e) {
            if (IsPostBack) return;
            myCarousel.InnerHtml += @"
                
";
            UpdateLatestProducts(Database.Instance.LatestProducts());
            UpdateSimilarProduts();
        }

        private void UpdateSimilarProduts() {
            var requestCookie = Request.Cookies["lastProduct"];
            if (requestCookie != null) {
                var price = double.Parse(Database.Instance.ProductInfo(int.Parse(requestCookie.Value)).Rows[0][4].ToString());
                var data = Database.Instance.SimilarProducts(price);
                if (data == null || data.Rows.Count == 0) {
                    divSimilarProducts.InnerText +=
                        "<p>Estamos a conhecer-te melhor. <br/> Em breve poderemos apresentar-te alguns produtos de que gostarás.</p>";
                    return;
                }
                var inner = data.Rows.Cast<DataRow>().Aggregate("", (current, item) => current + $@"
                    <div class='item col-md-3'>
                        <div class='thumbnail'>
                        <img class='group list-group-image' src='images/products/{item[10]}' alt='' />
                        <div class='caption'>
                            <h4 class='group inner list-group-item-heading' style='color: #333'>
                            {item[1]}</h4>
                            <p class='group inner list-group-item-text' style='color: #333'>
                            {item[2]}</p>
                            <div class='row'>
                                <div class='col-xs-12 col-md-6'>
                                    <p class='lead' style='color: #333'>
                                    {decimal.Parse(item[4].ToString()):C}</p>
                                </div>
                                <div class='col-xs-12 col-md-6'>
                                    <a class='btn btn-info' href='details.aspx?product={item[0]}'>Detalhes</a>
                                </div>
                                </div>
                            </div>
                        </div>
                    </div>
            ");
                divSimilarProducts.InnerHtml = inner;
            } else {
                divSimilarProducts.InnerText +=
                        "<p>Estamos a conhecer-te melhor. <br/> Em breve poderemos apresentar-te alguns produtos de que gostarás.</p>";
            }
        }

        private void UpdateLatestProducts(DataTable data) {
            if (data == null || data.Rows.Count == 0) {
                divLatestProducts.InnerHtml = "Não há nodidades :(";
                return;
            }
            var inner = data.Rows.Cast<DataRow>().Aggregate("", (current, item) => current + $@"
                    <div class='item col-md-3'>
                        <div class='thumbnail'>
                        <img class='group list-group-image' src='images/products/{item[10]}' alt='' />
                        <div class='caption'>
                            <h4 class='group inner list-group-item-heading' style='color: #333'>
                            {item[1]}</h4>
                            <p class='group inner list-group-item-text' style='color: #333'>
                            {item[2]}</p>
                            <div class='row'>
                                <div class='col-xs-12 col-md-6'>
                                    <p class='lead' style='color: #333'>
                                    {decimal.Parse(item[4].ToString()):C}</p>
                                </div>
                                <div class='col-xs-12 col-md-6'>
                                    <a class='btn btn-info' href='details.aspx?product={item[0]}'>Detalhes</a>
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