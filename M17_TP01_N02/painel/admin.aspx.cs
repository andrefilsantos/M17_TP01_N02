using System;
using System.Data;
using System.Web.UI.WebControls;
using M17_TP01_N02.Modal;

namespace M17_TP01_N02.painel
{
    public partial class admin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            UpdateOptBrand();
            UpdateOptCategories();
        }

        private void UpdateOptBrand()
        {
            optBrand.Items.Clear();
            var brands = Database.Instance.AllBrands();
            foreach (DataRow item in brands.Rows)
                optBrand.Items.Add(new ListItem(item[1].ToString(), item[0].ToString()));
        }

        private void UpdateOptCategories()
        {
            optCategory.Items.Clear();
            var categories = Database.Instance.AllCategories();
            foreach (DataRow item in categories.Rows)
                optBrand.Items.Add(new ListItem(item[1].ToString(), item[0].ToString()));
        }

        protected void btnAddProduct_OnClick(object sender, EventArgs e)
        {
            try
            {
                if(Database.Instance.AddProduct(txtProductName.Text, txtShortDescription.Text, txtLongDescription.Text,
                    optBrand.SelectedValue, optCategory.SelectedValue, int.Parse(txtStock.Text), txtWarnings.Text, null))
                    Response.Write("<script>alert('Inserido com sucesso')</script>");
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                throw;
            }
        }
    }
}