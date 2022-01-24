using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Negocio.BO;
using Negocio.Model;

namespace Apresentacao
{ // todo AVANIR OU ANCELMO	pesquisar sobre fixar a linha de titulo do greedview a a coluna dos botões de ação
    public partial class WebMoto : System.Web.UI.Page
    {    /// <summary>
        /// Dentro do Page Load fica o metodo de validação da página definindo quais os usuários tem acesso e suas ações.
        /// </summary>
        /// <param name="sender">Objeto do evento</param>
        /// <param name="e">Evento</param>
        protected void Page_Load(object sender, EventArgs e)
        {       
                       
            if ((Funcionario)Session["Logado"] != null)
            {
                lblMotoID.Visible = false;
                Session["folha"] = "cadastro";
                if (!IsPostBack)
                {
                    btnCancelar_Click(sender, e);
                }
            }
            else
            {
                Session.Remove("Logado");
                Response.Redirect("WebLogin.aspx");
            }             
        }

        #region ASINATURA DE METODOS E DECLARAÇÕES
        /// <summary>
        /// Cria os objetos
        /// </summary>
        static Moto objMoto;
        MotoBO objMotoBO;
        /// <summary>
        /// Cria as variaveis
        /// </summary>
        int numInt;

        #endregion

        #region CRUD
        /// <summary>
        /// Método que libera a tela para preenchimento das informações da moto.
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
        /// Método que adiciona ou altera as informações da moto.
        /// </summary>
        /// <param name="sender">Objeto do evento</param>
        /// <param name="e">Evento</param>
        protected void btnGravar_Click(object sender, EventArgs e)
        {
            try
            {
                objMoto = new Moto();
                objMoto._AnoFabricacao = txtAnoFabricacao.Text;
                objMoto._Chassi = txtChassi.Text.ToUpper();
                objMoto._CorPredominante = txtCorPredominante.Text.ToUpper();
                objMoto._MarcaModelo = txtModeloMarca.Text.ToUpper();
                objMoto._Observacao = txtObservacao.Text.ToUpper();
                objMoto._Placa = txtPlaca.Text.ToUpper();

                objMotoBO = new MotoBO();
                if (int.TryParse(lblMotoID.Text, out numInt) && lblMotoID.Text != "0")
                {
                    //altera o existente

                    objMoto._MotoID = int.Parse(lblMotoID.Text);
                    objMotoBO.Gravar(objMoto);

                    lblMensagem.Text = "Alterado com Sucesso!!!";               
                }
                else
                {
                    //Gravar novo
                    objMotoBO.Gravar(objMoto);

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
        /// Método que libera os campos para alteração das informações da moto ja registrado.
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
        ///Método que chama o popUp de confirmação de exlusão das informações da moto.
        /// </summary>
        /// <param name="sender">Objeto do evento</param>
        /// <param name="e">Evento</param>
        protected void btnExcluir_Click(object sender, EventArgs e)
        {
            ClientScript.RegisterStartupScript(GetType(), "Show", "<script> $('#popUpMensagemConfirmacao').modal('toggle');</script>");
        }
        /// <summary>
        ///Método que confirma a exlusão das informações da moto.
        /// </summary>
        /// <param name="sender">Objeto do evento</param>
        /// <param name="e">Evento</param>
        protected void OK_Click(object sender, EventArgs e)
        {
            try
            {
                objMotoBO = new MotoBO();
                objMotoBO.Excluir(objMoto);

                lblMensagem.Text = "Excluida com Sucesso!!!";
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
        ///Método que que busca uma lista de moto.
        /// </summary>
        /// <param name="sender">Objeto do evento</param>
        /// <param name="e">Evento</param>
        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                objMotoBO = new MotoBO();
                objMoto = new Moto();
                objMoto._Placa = txtBuscar.Text;
                gvListaMoto.DataSource = objMotoBO.BuscarListaMoto(objMoto);
                gvListaMoto.DataBind();
                ClientScript.RegisterStartupScript(GetType(), "Show", "<script> $('#popUpLista').modal('toggle');</script>");
            }
            catch (Exception erro)
            {
                lblMensagem.Text = erro.Message;
                lblMensagem.CssClass = "text-danger";
                ClientScript.RegisterStartupScript(GetType(), "Show", "<script> $('#popUpMensagem').modal('toggle');</script>");
            }
        }
        /// <summary>
        ///Método que que busca as informações de uma moto e preenche os campos da tela.
        /// </summary>
        /// <param name="sender">Objeto do evento</param>
        /// <param name="e">Evento</param>
        protected void gvListaMoto_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            { 
            
                LimparCampos();

                objMoto = new Moto();
                objMoto._MotoID = Convert.ToInt32(gvListaMoto.SelectedDataKey.Value);
                
                objMotoBO = new MotoBO();
                objMoto = objMotoBO.BuscarMoto(objMoto);
                txtAnoFabricacao.Text = objMoto._AnoFabricacao.ToString();
                txtChassi.Text = objMoto._Chassi;
                txtCorPredominante.Text = objMoto._CorPredominante;
                txtModeloMarca.Text = objMoto._MarcaModelo;
                txtObservacao.Text = objMoto._Observacao;
                lblMotoID.Text = objMoto._MotoID.ToString();
                txtPlaca.Text = objMoto._Placa;

                visibleBotao(true);
                btnGravar.Visible = false;
                
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
            txtAnoFabricacao.Text = "";
            txtBuscar.Text = "";
            txtChassi.Text = "";
            txtCorPredominante.Text = "";
            txtModeloMarca.Text = "";
            txtObservacao.Text = "";
            txtPlaca.Text = "";
            lblMotoID.Text = "";
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