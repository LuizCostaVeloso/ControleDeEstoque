using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Negocio.Model;
using Negocio.BO;
using System.Data.SqlClient;

namespace Apresentacao.Relatorios
{
    public partial class WebOrdemServicoImpressao : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
          
             OrdemServico obj = new OrdemServico();
             //obj._OrdemServicoID = int.Parse(Request.QueryString["cod"]);    
             int id = (int)Session["imprimirRelatorio"];
             obj._OrdemServicoID = id;
             CarregarImpressao(obj);
           
             
        }
        #region ASINATURA DE METODOS DECLARAÇÕES
        /// <summary>
        /// Cria os objetos
        /// </summary>
        static OrdemServico objOrdemServico;
        OrdemServicoBO objOrdemServicoBO;

        ItemServicoBO objItemServicoBO;
        ItemProdutoBO objItemProdutoBO;


        #endregion
        //METODOS Ordem de Serviço
        #region METODOS DA ORDEM DE SERVIÇO
        /// <summary>
        ///Método que que busca uma ordem de serviço e preenche os campos da tela.
        /// </summary>
        /// <param name="sender">Objeto do evento</param>
        /// <param name="e">Evento</param>
        private void CarregarImpressao(OrdemServico obj)
        {
            divDesconto.Visible = true;
            pnlprodutoInserido.Visible = false;
            pnlServicoInserido.Visible = false;
            LimparCampos();
            objOrdemServico = new OrdemServico();
            objOrdemServicoBO = new OrdemServicoBO();
            objOrdemServico._OrdemServicoID = obj._OrdemServicoID;
            objOrdemServico = objOrdemServicoBO.BuscarOrdemServico(objOrdemServico);
            lblNomeCliente.Text = objOrdemServico._Cliente._Nome;
            lblDataAbertura.Text = objOrdemServico._DataAberturaOS.ToString();
            lblDataFinalizacao.Text = objOrdemServico._DataFechamentoOS.ToString();
            lblCnpjCpf.Text = objOrdemServico._Cliente._CpfCnpj;
            lblTelefone.Text = objOrdemServico._Cliente._Telefone1;
            lblEndereco.Text = objOrdemServico._Cliente._Endereco;
            lblNomeMecanico.Text = objOrdemServico._Funcionario._Nome;
            lblPlaca.Text = objOrdemServico._Moto._Placa;
            lblMarcaModelo.Text = objOrdemServico._Moto._MarcaModelo;
            lblAno.Text = objOrdemServico._Moto._AnoFabricacao;
            lblBairro.Text = objOrdemServico._Cliente._Bairro;
            lblKm.Text = objOrdemServico._Km;
            if (objOrdemServico._TipoDesconto == 0)
            {
                lblDesconto.Text = "R$ " + objOrdemServico._Desconto.ToString();
            }
            if (objOrdemServico._TipoDesconto == 1)
            {
                lblDesconto.Text = objOrdemServico._Desconto.ToString()+ "%";
            }
            if (string.IsNullOrEmpty(lblDesconto.Text))
            {
                divDesconto.Visible = false;
            }
            lblTotalProdutoColuna.Text = "R$ " + objOrdemServico._ValorTotalProduto.ToString();
            lblTotalServicoColuna.Text = "R$ " + objOrdemServico._ValorTotalServicos.ToString();


            lblTotalOS.Text = objOrdemServico._ValorTotal.ToString();

            lblTotalComDesconto.Text = objOrdemServico._TotalOSdesconto.ToString("C2");
            lblTotalOS.Text = "R$ " + objOrdemServico._ValorTotal.ToString();

           
            if (objOrdemServico._ListaItemProduto != null)
            {
                objItemProdutoBO = new ItemProdutoBO();
                gvProdutosInseridos.DataSource = objItemProdutoBO.listaIDataTableItemProduto(objOrdemServico);
                gvProdutosInseridos.DataBind();
                pnlprodutoInserido.Visible = true;
            }
            if (objOrdemServico._ListaItemServico != null)
            {
                objItemServicoBO = new ItemServicoBO();
                gvServicoInseridos.DataSource = objItemServicoBO.buscaListaDataTableItemServico(objOrdemServico);
                gvServicoInseridos.DataBind();
                pnlServicoInserido.Visible = true;
            }
           
        }

        #endregion

        /// <summary>
        /// Método de apoio que limpa os campos quando solicitado
        /// </summary>
        public void LimparCampos()
        {
            lblDataAbertura.Text = "";
            lblDataFinalizacao.Text = "";
            lblCnpjCpf.Text = "";
            lblEndereco.Text = "";
            lblNomeCliente.Text = "";
            lblTelefone.Text = "";
            lblMarcaModelo.Text = "";
            lblPlaca.Text = "";
            lblAno.Text = "";
            lblKm.Text = "";
            lblTotalProdutoColuna.Text = "";
            lblTotalServicoColuna.Text = "";
            lblBairro.Text = "";
            lblDesconto.Text = "";
            lblTotalComDesconto.Text = "";

        }

       
       
        //todo Colocar uma mensagem de erro na classe dao no metodo exluir de todas as classe ESTE NÃO PODE SER EXLUIDO POIS ESTÁ VINCUNLADO A OUTRO FORMULÁRIO.

    }
}