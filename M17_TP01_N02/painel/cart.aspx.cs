using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using M17_TP01_N02.Modal;
using Newtonsoft.Json.Linq;

namespace M17_TP01_N02.painel {
    public partial class cart : Page {
        protected void Page_Load(object sender, EventArgs e) {
            var listCart = new List<Cart>();
            var cookie = Request.Cookies["cart"];
            if (cookie != null) {
                var cart = cookie.Value;
                var element = JArray.Parse(cart);
                listCart.AddRange(element.Select(t => new Cart {
                    Qtd = int.Parse(t["Qtd"].ToString()),
                    IdProduct = int.Parse(t["IdProduct"].ToString())
                }));
                foreach (var item in listCart)
                {
                    var product = Database.Instance.ProductInfo(item.IdProduct);
                    divCart.InnerHtml += $@" <div class='item  col-xs-4 col-lg-4 list-group-item'>
                                        <div class='thumbnail'>
                                            <img class='group list-group-image' src='../images/{product.Rows[0][10]}.jpg' width='200' height='100'>
                                            <div class='caption'>
                                                <h4 class='group inner list-group-item-heading'>{product.Rows[0][1]}</h4>
                                                <div class='row'>
                                                    <div class='col-xs-12 col-md-6'>
                                                        <p class='lead'>
                                                            {item.Qtd * decimal.Parse(product.Rows[0][4].ToString()):C}
                                                            <br/>
                                                            ({item.Qtd} x {decimal.Parse(product.Rows[0][4].ToString()):C})
                                                        </p>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>";
                }
            }
            else
            {
                lblCart.Text = "Não tem quaisquer produtos no seu carrinho.";
                lblCart.CssClass = "alert alert-warning col-md-12";
            }
        }
    }
}