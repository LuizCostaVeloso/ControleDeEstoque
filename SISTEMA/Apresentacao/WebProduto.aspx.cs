using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Negocio.BO;
using Negocio.Model;
using System.Diagnostics;

namespace Apresentacao
{
   
   
    public partial class WebProduto : System.Web.UI.Page
    {
        /// <summary>
        /// Dentro do Page Load fica o metodo de validação da página definindo quais os usuários tem acesso e suas ações.
        /// </summary>
        /// <param name="sender">Objeto do evento</param>
        /// <param name="e">Evento</param>
        protected void Page_Load(object sender, EventArgs e)
        {

            if ((Funcionario)Session["Logado"] != null)
            {
                lblProdutoID.Visible = false;
                Session["folha"] = "cadastro";
                txtEstoque.ReadOnly = true;               
                Cargo gerente = Cargo.GERENTE;
                if (!IsPostBack)
                { 
                    btnCancelar_Click(sender, e);
                }

                Funcionario objFuncionario = new Funcionario();
                objFuncionario = ((Funcionario)Session["Logado"]);
                if (objFuncionario._Cargo == gerente)
                {
                    spanBtnNovo.Visible = true;
                }
                else
                {
                    divEntEst.Visible = false;
                    divPrecoCompra.Visible = false;
                    spanBtnNovo.Visible = false;
                    visibleBotao(false);
                }

            }
            else
            {
                Response.Redirect("WebLogin.aspx");
            }
           
        }
        #region ASINATURA DE METODOS E DECLARAÇÕES
        /// <summary>
        /// Cria os objetos
        /// </summary>
        static Produto objProduto;
        ProdutoBO objProdutoBO;
        /// <summary>
        /// Cria as variaveis
        /// </summary>
        int numInt;
        decimal numDecimal;

        #endregion

        #region CRUD
        /// <summary>
        /// Método que libera a tela para preenchimento das informações de um novo Produto
        /// </summary>
        /// <param name="sender">Objeto do evento</param>
        /// <param name="e">Evento</param>
        protected void btnNovo_Click(object sender, EventArgs e)
        {
            LimparCampos();
            btnGravar.Visible = true;
            btnCancelar.Visible = true;
            panelConteudo.Enabled = true;
            panelBusca.Enabled = false;

        }
        /// <summary>
        /// Método que define o estado inicial da tela.
        /// </summary>
        /// <param name="sender">Objeto do evento</param>
        /// <param name="e">Evento</param>
        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            visibleBotao(false);
            LimparCampos();
            panelConteudo.Enabled = false;
            panelBusca.Enabled = true;
        }


        /// <summary>
        /// Método que adiciona ou altera um produto
        /// </summary>
        /// <param name="sender">Objeto do evento</param>
        /// <param name="e">Evento</param>
        protected void btnGravar_Click(object sender, EventArgs e)
        {
            try
            {
                if (String.IsNullOrEmpty(txtEstoqueMinimo.Text))
                    throw new Exception("O campo Estoque Minimo é obrigatório!!!");
                if (!int.TryParse(txtEstoqueMinimo.Text, out numInt))
                    throw new Exception("O campo Estoque Minimo só aceita Números!!!");               
                if (String.IsNullOrEmpty(txtPrecoCompra.Text))
                    throw new Exception("O campo Preço de Custo é obrigatório!!!");
                if (!Decimal.TryParse(txtPrecoCompra.Text, out numDecimal))
                    throw new Exception("Digite um Preço de Custo Válido ex: 10,34");
                if (String.IsNullOrEmpty(txtPrecoVenda.Text))
                    throw new Exception("O campo Preço de Venda é obrigatório!!!");
                if (!Decimal.TryParse(txtPrecoVenda.Text, out numDecimal))
                    throw new Exception("Digite um Preço de Venda Válido ex: 10,34");

                objProduto = new Produto();
                objProduto._NomeProduto = txtNomeProduto.Text.ToUpper();
                objProduto._NomeFornecedor = txtNomeFornecedor.Text.ToUpper();
                objProduto._TelefoneFornecedor = txtTelFornecedor.Text;
                objProduto._MarcaModelo = txtMarca.Text.ToUpper();
                objProduto._EstoqueMinimo = int.Parse(txtEstoqueMinimo.Text);
                objProduto._PrecoCompra = decimal.Parse(txtPrecoCompra.Text);
                objProduto._PrecoVenda = decimal.Parse(txtPrecoVenda.Text);
                objProduto._Observacao = txtObservacao.Text.ToUpper();

                objProdutoBO = new ProdutoBO();
                if (int.TryParse(lblProdutoID.Text, out numInt) && lblProdutoID.Text != "0")
                {
                    if (String.IsNullOrEmpty(txtEntradaEstoque.Text))
                        txtEntradaEstoque.Text = "0";
                    if (!int.TryParse(txtEntradaEstoque.Text, out numInt))
                        throw new Exception("O campo Entrada de Estoque só aceita Números!!!");
                    objProduto._EntradaEstoque = int.Parse(txtEntradaEstoque.Text);             
                    objProduto._Estoque = int.Parse(txtEstoque.Text);
                    objProduto._ProdutoID = int.Parse(lblProdutoID.Text);
                    objProdutoBO.Gravar(objProduto);

                    lblMensagem.Text = "Alterado com Sucesso!!!";
                }
                else
                {
                    if (String.IsNullOrEmpty(txtEntradaEstoque.Text))
                        throw new Exception("Campo Entrada de Estoque é obrigatório!!!");
                    else if (!int.TryParse(txtEntradaEstoque.Text, out numInt))
                        throw new Exception("O campo Entrada de Estoque só aceita Números!!!");
                    objProduto._EntradaEstoque = int.Parse(txtEntradaEstoque.Text);
                    objProdutoBO.Gravar(objProduto);

                    lblMensagem.Text = "Salvo com Sucesso!!!";
                }

                mensagemSucesso.Visible = true;
                mensagemErro.Visible = false;
                ClientScript.RegisterStartupScript(GetType(), "Show", "<script> $('#popUpMensagem').modal('toggle');</script>");
                btnCancelar_Click(sender, e);
            }
            catch (Exception erro)
            {
                lblMensagem.Text = erro.Message;
                mensagemSucesso.Visible = false;
                mensagemErro.Visible = true;
                ClientScript.RegisterStartupScript(GetType(), "Show", "<script> $('#popUpMensagem').modal('toggle');</script>");
            }
        }
        /// <summary>
        /// Método que libera os campos para alteração de um produto ja registrado.
        /// </summary>
        /// <param name="sender">Objeto do evento</param>
        /// <param name="e">Evento</param>
        protected void btnEditar_Click(object sender, EventArgs e)
        {
            btnExcluir.Visible = false;
            btnEditar.Visible = false;
            btnGravar.Visible = true;
            panelConteudo.Enabled = true;
            panelBusca.Enabled = false;

        }
        /// <summary>
        ///Método que chama o popUp de confirmação de exlusão de um produto
        /// </summary>
        /// <param name="sender">Objeto do evento</param>
        /// <param name="e">Evento</param>
        protected void btnExcluir_Click(object sender, EventArgs e)
        {
            ClientScript.RegisterStartupScript(GetType(), "Show", "<script> $('#popUpMensagemConfirmacao').modal('toggle');</script>");
        }
        /// <summary>
        ///Método que confirma a exlusão do produto
        /// </summary>
        /// <param name="sender">Objeto do evento</param>
        /// <param name="e">Evento</param>
        protected void OK_Click(object sender, EventArgs e)
        {
            try
            {
                objProdutoBO = new ProdutoBO();
                objProdutoBO.Excluir(objProduto);
                lblMensagem.Text = "Excluido com Sucesso!!!";
                mensagemSucesso.Visible = true;
                mensagemErro.Visible = false;
                ClientScript.RegisterStartupScript(GetType(), "Show", "<script> $('#popUpMensagem').modal('toggle');</script>");
                btnCancelar_Click(sender, e);
            }
            catch (Exception erro)
            {
                lblMensagem.Text = erro.Message;
                mensagemSucesso.Visible = false;
                mensagemErro.Visible = true;
                ClientScript.RegisterStartupScript(GetType(), "Show", "<script> $('#popUpMensagem').modal('toggle');</script>");
            }
        }

        #endregion

        #region BUSCAS
        /// <summary>
        ///Método que que busca uma lista de produtos.
        /// </summary>
        /// <param name="sender">Objeto do evento</param>
        /// <param name="e">Evento</param>
        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                objProduto = new Produto();
                objProduto._NomeProduto = txtBuscar.Text;
                objProdutoBO = new ProdutoBO();
                gvListaProduto.DataSource = objProdutoBO.BuscarListaProduto(objProduto);
                gvListaProduto.DataBind();

                ClientScript.RegisterStartupScript(GetType(), "Show", "<script> $('#popUpLista').modal('toggle');</script>");
            }
            catch (Exception erro)
            {
                lblMensagem.Text = erro.Message;
                mensagemSucesso.Visible = false;
                mensagemErro.Visible = true;
                ClientScript.RegisterStartupScript(GetType(), "Show", "<script> $('#popUpMensagem').modal('toggle');</script>");
            }
        }
        /// <summary>
        ///Método que que busca um produto e preenche os campos da tela.
        /// </summary>
        /// <param name="sender">Objeto do evento</param>
        /// <param name="e">Evento</param>
        protected void gvListaProduto_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                LimparCampos();

                objProduto = new Produto();
                objProduto._ProdutoID = Convert.ToInt32(gvListaProduto.SelectedDataKey.Value);
                objProdutoBO = new ProdutoBO();
                objProduto = objProdutoBO.BuscarProduto(objProduto);
                lblProdutoID.Text = objProduto._ProdutoID.ToString();
                txtEstoqueMinimo.Text = objProduto._EstoqueMinimo.ToString();
                txtMarca.Text = objProduto._MarcaModelo;
                txtNomeFornecedor.Text = objProduto._NomeFornecedor;
                txtNomeProduto.Text = objProduto._NomeProduto;
                txtObservacao.Text = objProduto._Observacao;
                txtTelFornecedor.Text = objProduto._TelefoneFornecedor;
                txtPrecoCompra.Text = objProduto._PrecoCompra.ToString();
                txtPrecoVenda.Text = objProduto._PrecoVenda.ToString();
                txtEstoque.Text = objProduto._Estoque.ToString();

                Funcionario objFuncionario = new Funcionario();
                objFuncionario = ((Funcionario)Session["Logado"]);
                Cargo gerente = Cargo.GERENTE;
                if (objFuncionario._Cargo == gerente)
                {
                    visibleBotao(true);
                    btnGravar.Visible = false;

                }
                else
                {
                   
                    visibleBotao(false);
                    btnCancelar.Visible = true;
                }

            }
            catch (Exception erro)
            {
                lblMensagem.Text = erro.Message;
                mensagemSucesso.Visible = false;
                mensagemErro.Visible = true;
                ClientScript.RegisterStartupScript(GetType(), "Show", "<script> $('#popUpMensagem').modal('toggle');</script>");
            }
        }

        #endregion

        #region METODOS DE APOIO
        /// <summary>
        /// Método de apoio que limpa os campos quando solicitado
        /// </summary>
        public void LimparCampos()
        {
            txtBuscar.Text = "";
            txtEstoqueMinimo.Text = "";
            txtNomeFornecedor.Text = "";
            txtNomeProduto.Text = "";
            txtEntradaEstoque.Text = "";
            txtObservacao.Text = "";
            txtTelFornecedor.Text = "";
            txtPrecoCompra.Text = "";
            txtPrecoVenda.Text = "";
            txtMarca.Text = "";
            lblProdutoID.Text = "";
            txtEstoque.Text = "";

        }
        /// <summary>
        /// Método de apoio que deixa visivel ou oculto conforme solicitado. 
        /// </summary>
        /// <param name="ocultar">Este recebe como parametros tru ou false</param>
        public void visibleBotao(bool ocultar)
        {
            btnEditar.Visible = ocultar;
            btnExcluir.Visible = ocultar;
            btnGravar.Visible = ocultar;
            btnCancelar.Visible = ocultar;
        }

        #endregion


        #region Metodos da página
        /// <summary>
        /// Método que chama a caluladora do windows para calcular o preço de venda do produto.
        /// </summary>
        /// <param name="sender">Objeto do evento</param>
        /// <param name="e">Evento</param>
        protected void btnCalculadora_Click(object sender, EventArgs e)
        {
            Process.Start("Calc.exe");
        }

        #endregion   
        /// <summary>
        /// Método que pinta a linha dos produtos que estiverem abaixo do estoque minimo definido pelo administrador.
        /// </summary>
        /// <param name="sender">Objeto do evento</param>
        /// <param name="e">Evento</param>
        protected void gvListaProduto_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow) //verifica se a linha selecionada é uma linha de dados
            {

                if (e.Row.Cells[6].Text.Equals("Baixo")) //Quando for linha que contém dados, verifica se o valor da coluna 6 está como baixo
                {
                    e.Row.ForeColor = System.Drawing.Color.Orange;
                }                 
            }

        }
    }
}