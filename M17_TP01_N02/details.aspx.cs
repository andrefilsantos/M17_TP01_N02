using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using M17_TP01_N02.Modal;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace M17_TP01_N02 {
    public partial class Details : Page {
        private int _id;
        private DataTable _product;
        protected void Page_Load(object sender, EventArgs e) {
            try {
                _id = int.Parse(Request["product"]);
                _product = Database.Instance.ProductInfo(_id);
                imgProduct.ImageUrl = $"~/images/products/{_product.Rows[0][10]}";
                productName.InnerText = _product.Rows[0][1].ToString();
                shortDescription.InnerText = _product.Rows[0][2].ToString();
                UpdatePrice();
                brandCategory.InnerHtml = "<b>" + Database.Instance.BrandNameById(int.Parse(_product.Rows[0][5].ToString())) + "</b> | " + Database.Instance.CategoryNameById(int.Parse(_product.Rows[0][6].ToString()));
                longDescription.InnerText = _product.Rows[0][3].ToString();
                if (_product.Rows[0][8].ToString().Length > 0)
                    warnings.InnerText = "<h6>Atenção!</h6>" + _product.Rows[0][8];
                UpdateDivComments();
                var insertCookie = new HttpCookie("lastProduct", _id.ToString()) {
                    Expires = DateTime.Now.AddDays(1)
                };
                Response.Cookies.Add(insertCookie);
                if (Session["id"] == null)
                    divComments.Visible = false;
            } catch (Exception) {
                Response.Redirect("index.aspx");
            }
        }

        private void UpdatePrice() {
            var qtd = int.Parse(txtQtd.Text);
            var productPrice = decimal.Parse(_product.Rows[0][4].ToString());
            price.InnerText = $" {decimal.Parse((qtd * productPrice).ToString(CultureInfo.InvariantCulture)):C}";
        }

        protected void OnTextChanged(object sender, EventArgs e) => UpdatePrice();

        protected void OnClick(object sender, EventArgs e) {
            var listCart = new List<Cart>();
            var cookie = Request.Cookies["cart"];
            if (cookie != null) {
                var cart = cookie.Value;
                var element = JArray.Parse(cart);
                listCart.AddRange(element.Select(t => new Cart {
                    Qtd = int.Parse(t["Qtd"].ToString()),
                    IdProduct = int.Parse(t["IdProduct"].ToString())
                }));
            }
            listCart.Add(new Cart {
                Qtd = int.Parse(txtQtd.Text),
                IdProduct = _id
            });
            var insertCookie = new HttpCookie("cart", JsonConvert.SerializeObject(listCart)) {
                Expires = DateTime.Now.AddDays(1)
            };
            Response.Cookies.Add(insertCookie);
        }

        protected void btnAddComment_OnClick(object sender, EventArgs e) {
            try {
                if (Database.Instance.AddComment(int.Parse(Session["id"].ToString()), _id, txtTitle.Text,
                    txtComment.Text)) {
                    lblResultComment.CssClass = "alert alert-success col-md-12";
                    lblResultComment.Text = "Comentário adicionado com sucesso.";
                    UpdateDivComments();
                } else
                    throw new Exception("Ocorreu um erro ao adicionar o comentário :(");
            } catch (Exception exception) {
                lblResultComment.CssClass = "alert alert-danger col-md-12";
                lblResultComment.Text = exception.Message;
            }
        }

        private void UpdateDivComments() {
            var data = Database.Instance.AllProductComments(_id);
            var inner = data.Rows.Cast<DataRow>().Aggregate("", (current, item) => current + $@"
                    <h3>{item[0]}</h3>
                    <h4>{item[1]}</h4>
                    <p>{item[2]}</p>
                    <hr>
            ");
            divAllComments.InnerHtml += inner;
        }
    }
}