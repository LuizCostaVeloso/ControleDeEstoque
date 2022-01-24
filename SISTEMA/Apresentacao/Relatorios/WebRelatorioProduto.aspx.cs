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
    public partial class WebRelatorioProduto : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            carregar();
            
        }
        public void carregar()
        {
            Produto objProduto = new Produto();

            ProdutoBO objProdutoBO = new ProdutoBO();
            gvListaProduto.DataSource = objProdutoBO.BuscarListaProduto(objProduto);
            gvListaProduto.DataBind();


        }
        protected void gvListaProduto_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow) //verifica se a linha selecionada é uma linha de dados
            {

                if (e.Row.Cells[5].Text.Equals("Baixo")) //Quando for linha que contém dados, verifica se o valor da coluna 6 está como baixo
                {
                    e.Row.ForeColor = System.Drawing.Color.Orange;
                }
            }

        }
    }
}