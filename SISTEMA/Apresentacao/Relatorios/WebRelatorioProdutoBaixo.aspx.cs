using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Negocio.BO;
using Negocio.Model;
using System.Diagnostics;


namespace Apresentacao.Relatorios
{
    public partial class WebRelatorioProdutoBaixo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            carregar();
        }
        public void carregar()
        {
            Produto objProduto = new Produto();               
            ProdutoBO  objProdutoBO = new ProdutoBO();
            gvListaProduto.DataSource = objProdutoBO.BuscarListaProdutoBaixo(objProduto);
            gvListaProduto.DataBind();
        }
    }
}