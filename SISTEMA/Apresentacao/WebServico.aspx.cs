using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Negocio.Model;
using Negocio.BO;

namespace Apresentacao
{
    public partial class WebServico : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            /// <summary>
            /// Dentro do Page Load fica o metodo de validação da página definindo quais os usuários tem acesso e suas ações.
            /// </summary>
            /// <param name="sender">Objeto do evento</param>
            /// <param name="e">Evento</param>
            if ((Funcionario)Session["Logado"] != null)
            {
                lblServicoID.Visible = false;
                Session["folha"] = "cadastro";
                
                Cargo gerente = Cargo.GERENTE;
                if (!IsPostBack)
                { 
                    btnCancelar_Click(sender, e);
                } 
               Funcionario objFuncionario = new Funcionario();
                objFuncionario = ((Funcionario)Session["Logado"]);
                if (objFuncionario._Cargo == gerente)
                {                    
                     spanBtnNovo.Visible=true;
                }
                else
                {                       
                    spanBtnNovo.Visible=false;
                    visibleBotao(false);                      
                }

            }
            else
            {
                Response.Redirect("WebLogin.aspx");
            }

        }
        //---------------------------------///-------------------------///---------------------------------///
        #region ASINATURA DE METODOS E DECLARAÇÕES
        /// <summary>
        /// Cria os objetos
        /// </summary>
        static Servico objServico;
        ServicoBO objServicoBO;
        /// <summary>
        /// Cria as variaveis
        /// </summary>
        int numInt;
        decimal numDecimal;

        #endregion
        ////---------------------------------///-------------------------///---------------------------------///
        #region CRUD
        /// <summary>
        /// Método que libera a tela para preenchimento das informações de um novo Serviço
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
        /// Método que adiciona ou altera as informações do serviço
        /// </summary>
        /// <param name="sender">Objeto do evento</param>
        /// <param name="e">Evento</param>
        protected void btnGravar_Click(object sender, EventArgs e)
        {
          
            try
            {
                if (String.IsNullOrEmpty(txtValorServico.Text))
                    throw new Exception("O campo Preço é obrigatório!!!");
                if (!Decimal.TryParse(txtValorServico.Text, out numDecimal))
                    throw new Exception("Digite um Preço Válido ex: 10,34");


                objServico = new Servico();
                objServico._NomeServico = txtNomeServico.Text.ToUpper();
                objServico._Observacao = txtObservacao.Text.ToUpper();
                objServico._Preco = decimal.Parse(txtValorServico.Text);
                objServico._MarcaModelo = txtModelo.Text.ToUpper();
                objServicoBO = new ServicoBO();
                if (int.TryParse(lblServicoID.Text, out numInt) && lblServicoID.Text != "0")
                {
                    //altera o existente                   
                    objServico._ServicoID = int.Parse(lblServicoID.Text);
                    objServicoBO.Gravar(objServico);

                    lblMensagem.Text = "Alterado com Sucesso!!!";
                }
                else
                {
                    objServicoBO.Gravar(objServico);
                    //Gravar novo
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
        /// Método que libera os campos para alteração das informações do serviço ja registrado.
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
        ///Método que chama o popUp de confirmação de exlusão das informções do serviço
        /// </summary>
        /// <param name="sender">Objeto do evento</param>
        /// <param name="e">Evento</param>
        protected void btnExcluir_Click(object sender, EventArgs e)
        {
            ClientScript.RegisterStartupScript(GetType(), "Show", "<script> $('#popUpMensagemConfirmacao').modal('toggle');</script>");
        }
        /// <summary>
        ///Método que confirma a exlusão das informações do serviço.
        /// </summary>
        /// <param name="sender">Objeto do evento</param>
        /// <param name="e">Evento</param>
        protected void OK_Click(object sender, EventArgs e)
        {
            try
            {

                objServicoBO = new ServicoBO();
                objServicoBO.Excluir(objServico);
                LimparCampos();
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
        ////---------------------------------///-------------------------///---------------------------------///
        #region BUSCAS
        /// <summary>
        ///Método que que busca uma lista de serviços.
        /// </summary>
        /// <param name="sender">Objeto do evento</param>
        /// <param name="e">Evento</param>
        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                objServico = new Servico();
                objServicoBO = new ServicoBO();
                objServico._NomeServico = txtBuscar.Text;
                gvListaServico.DataSource = objServicoBO.BuscarListaServico(objServico);
                gvListaServico.DataBind();
                ClientScript.RegisterStartupScript(GetType(), "Show", "<script> $('#popUpLista').modal('toggle');</script>");
            }
            catch (Exception erro)
            {
                lblMensagem.Text = erro.Message;
                mensagemSucesso.Visible = false;
                mensagemErro.Visible = true;
                ClientScript.RegisterStartupScript(GetType(), "Show", "<script> $('#popUpMensagem').modal('toggle');</script>");

            }
            txtBuscar.Text = "";
        }

        /// <summary>
        ///Método que que busca as informaçoes de um serviço e preenche os campos da tela.
        /// </summary>
        /// <param name="sender">Objeto do evento</param>
        /// <param name="e">Evento</param>
        protected void gvListaServico_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
               
                LimparCampos();

                objServico = new Servico();
                objServico._ServicoID = Convert.ToInt32(gvListaServico.SelectedDataKey.Value);

                objServicoBO = new ServicoBO();
                objServico = objServicoBO.BuscarServico(objServico);
                txtModelo.Text = objServico._MarcaModelo;
                lblServicoID.Text = objServico._ServicoID.ToString();
                txtNomeServico.Text = objServico._NomeServico;
                txtValorServico.Text = objServico._Preco.ToString();
                txtObservacao.Text = objServico._Observacao;

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
        //---------------------------------///-------------------------///---------------------------------///
        #region METODOS DE APOIO
         /// <summary>
         /// Método de apoio que limpa os campos quando solicitado
         /// </summary>
        public void LimparCampos()
        {
            lblServicoID.Text = "";
            txtNomeServico.Text = "";
            txtValorServico.Text = "";
            txtObservacao.Text = "";
            txtModelo.Text = "";
            txtBuscar.Text = "";
        }
        /// <summary>
        /// Método de apoio que deixa visivel ou oculto conforme solicitado. 
        /// </summary>
        /// <param name="ocultar">Este recebe como parametros tru ou false.</param>
        public void visibleBotao(bool ocultar)
        {
            btnEditar.Visible = ocultar;
            btnExcluir.Visible = ocultar;
            btnGravar.Visible = ocultar;
            btnCancelar.Visible = ocultar;
        }

        #endregion         

        
    }
}