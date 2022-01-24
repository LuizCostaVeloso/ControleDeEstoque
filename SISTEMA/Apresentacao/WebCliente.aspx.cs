using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Negocio.BO;
using Negocio.Model;
using System.Data;


namespace Apresentacao
{
    public partial class WebCliente : System.Web.UI.Page
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
                Session["folha"] = "cadastro";
                txtDtCriacao.ReadOnly = true;              
                lblClienteID.Visible = false;
                if (!IsPostBack)
                {                    
                    CarregarUf();                     
                    btnCancelar_Click(sender, e);
                    txtCnpj.Visible = false;
                }
            }
            else
            {
                Session.Remove("Logado");
                Response.Redirect("WebLogin.aspx");
            }

        }
        //---------------------------------///------------------------///--------------------------------///
        #region ASINATURA DE METODOS DECLARAÇÕES
        /// <summary>
        /// Cria os objetos
        /// </summary>
        static Cliente objCliente;
        ClienteBO objClienteBO;
        /// <summary>
        /// Cria as variaveis
        /// </summary>
        int numInt;
        DateTime data;

        #endregion
        //---------------------------------///-------------------------///---------------------------------///
        #region CRUD
        /// <summary>
        /// Método que libera a tela para preenchimento das informações de um novo Cliente
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
            rbTipoPessoa0.Checked = true;
            rbTipoPessoa0_CheckedChanged(sender, e);
            txtDtCriacao.Text = DateTime.Now.ToShortDateString();
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
        /// Método que adiciona ou altera as informações do cliente.
        /// </summary>
        /// <param name="sender">Objeto do evento</param>
        /// <param name="e">Evento</param>
        protected void btnGravar_Click(object sender, EventArgs e)
        {

            try
            {
                if (!DateTime.TryParse(txtDtCriacao.Text, out data))
                    throw new Exception("Digite uma Data Válida ex: 31/12/2013!!!");


                objCliente = new Cliente();
                objCliente._DataCadastro = DateTime.Parse(txtDtCriacao.Text);
                objCliente._Nome = txtNome.Text.ToUpper();
                if (rbSexo0.Checked == true)
                    objCliente._Sexo = (Sexo)Enum.Parse(typeof(Sexo), "0");
                if (rbSexo1.Checked == true)
                    objCliente._Sexo = (Sexo)Enum.Parse(typeof(Sexo), "1");                  
                if (rbTipoPessoa0.Checked == true)
                {
                    objCliente._CpfCnpj = txtCpf.Text;
                    objCliente._TipoPessoa = 0;
                }
                if (rbTipoPessoa1.Checked == true)
                {
                    objCliente._CpfCnpj = txtCnpj.Text;
                    objCliente._TipoPessoa = 1;
                    objCliente._Sexo = (Sexo)Enum.Parse(typeof(Sexo), "3");  
                }
                objCliente._Rg = txtRg.Text;
                objCliente._OrgaoEmissor = txtObservacao.Text.ToUpper();
                objCliente._Cep = txtCep.Text;
                objCliente._Endereco = txtEndereco.Text.ToUpper();
                objCliente._Bairro = txtBairro.Text.ToUpper();
                objCliente._Cidade = txtCidade.Text.ToUpper();
                objCliente._Uf = (Uf)Enum.Parse(typeof(Uf), ddlUf.Text);
                objCliente._Complemento = txtComplemento.Text.ToUpper();
                objCliente._Email = txtEmail.Text;
                objCliente._Telefone1 = txtTel1.Text;
                objCliente._Telefone2 = txtTel2.Text;
                objCliente._Observacao = txtObservacao.Text.ToUpper();
                objClienteBO = new ClienteBO();

                if ((int.TryParse(lblClienteID.Text, out numInt)) && lblClienteID.Text != "0")
                {
                    //altera o existente                    
                    objCliente._ClienteID = int.Parse(lblClienteID.Text);
                    objClienteBO.Gravar(objCliente);

                    lblMensagem.Text = "Alterado com Sucesso!!!";

                }
                else
                {
                    //Gravar novo
                    objClienteBO.Gravar(objCliente);

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
        /// Método que libera os campos para alteração das informações do cliente ja registrado.
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
        ///Método que chama o popUp de confirmação de exlusão das informções do cliente
        /// </summary>
        /// <param name="sender">Objeto do evento</param>
        /// <param name="e">Evento</param>
        protected void btnExcluir_Click(object sender, EventArgs e)
        {
            ClientScript.RegisterStartupScript(GetType(), "Show", "<script> $('#popUpMensagemConfirmacao').modal('toggle');</script>");
        }
        /// <summary>
        ///Método que confirma a exlusão das informações do cliente.
        /// </summary>
        /// <param name="sender">Objeto do evento</param>
        /// <param name="e">Evento</param>
       
        protected void OK_Click(object sender, EventArgs e)
        {
            try
            {
                objClienteBO = new ClienteBO();
                objClienteBO.Excluir(objCliente);

                lblMensagem.Text = "Cliente Excluido com Sucesso!!!!!!";
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
        //---------------------------------///-------------------------///---------------------------------///
        #region BUSCAS
        /// <summary>
        ///Método que que busca uma lista de Cliente.
        /// </summary>
        /// <param name="sender">Objeto do evento</param>
        /// <param name="e">Evento</param>
        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {

                objClienteBO = new ClienteBO();
                objCliente = new Cliente();
                objCliente._Nome = txtBuscar.Text;
                gvListaCliente.DataSource = objClienteBO.BuscarListaCliente(objCliente);
                gvListaCliente.DataBind();
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
        ///Método que que busca as informaçoes de um cliente e preenche os campos da tela.
        /// </summary>
        /// <param name="sender">Objeto do evento</param>
        /// <param name="e">Evento</param>
        protected void gvListaCliente_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                LimparCampos();

                objClienteBO = new ClienteBO();
                objCliente = new Cliente();
                objCliente._ClienteID = Convert.ToInt32(gvListaCliente.SelectedDataKey.Value);
                objCliente = objClienteBO.BuscarCliente(objCliente);
                lblClienteID.Text = objCliente._ClienteID.ToString();
                if (objCliente._TipoPessoa == 0)
                {
                    rbTipoPessoa0_CheckedChanged(sender, e);
                    rbTipoPessoa0.Checked = true;
                    txtCpf.Text = objCliente._CpfCnpj;
                    

                }
                if (objCliente._TipoPessoa == 1)
                {
                    rbTipoPessoa1_CheckedChanged(sender, e);
                    txtCnpj.Text = objCliente._CpfCnpj;
                    rbTipoPessoa1.Checked = true;
                   
                }
                if (objCliente._Sexo.ToString() == "M")
                    rbSexo0.Checked = true;
                if (objCliente._Sexo.ToString() == "F")
                    rbSexo1.Checked = true;
                txtBairro.Text = objCliente._Bairro;
                txtCep.Text = objCliente._Cep;
                txtCidade.Text = objCliente._Cidade;
                txtComplemento.Text = objCliente._Complemento;
                txtDtCriacao.Text = objCliente._DataCadastro.ToString("dd/MM/yyyy");
                txtEmail.Text = objCliente._Email;
                txtEndereco.Text = objCliente._Endereco;
                txtNome.Text = objCliente._Nome;
                txtObservacao.Text = objCliente._Observacao;
                txtRg.Text = objCliente._Rg;
                txtTel1.Text = objCliente._Telefone1;
                txtTel2.Text = objCliente._Telefone2;
                ddlUf.Text = objCliente._Uf.ToString();
                txtOrgaoEmissor.Text = objCliente._OrgaoEmissor;
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
        //---------------------------------///-------------------------///---------------------------------///
        #region METODOS DA PÁGINA DE APRESENTAÇÃO

        //todo REFERENCIA (SOBRE RADIOBUTTON E AUTOPOSTVACK http://www.devmedia.com.br/componentes-intermediarios-controles-asp-net-menu-standard-parte-2/17805)
        /// <summary>
        ///  metodo que determina que o txtCpf vai aparecer na tela
        /// </summary>
        /// <param name="sender">Objeto do evento</param>
        /// <param name="e">Evento</param>
        protected void rbTipoPessoa0_CheckedChanged(object sender, EventArgs e)
        {
            txtOrgaoEmissor.Text = "";
            txtRg.Text = "";
            txtCpf.Text = "";
            txtCnpj.Text = "";
            txtCpf.Visible = true;
            txtCnpj.Visible = false;
            rbSexo0.Enabled = true;
            rbSexo1.Enabled = true;
            txtRg.Enabled = true;
            txtOrgaoEmissor.Enabled = true;
            rbSexo0.Checked = false;
            rbSexo1.Checked = false;
            
        }
        /// <summary>
        ///  metodo que determina que o txtCnpj vai aparecer na tela
        /// </summary>
        /// <param name="sender">Objeto do evento</param>
        /// <param name="e">Evento</param>
        protected void rbTipoPessoa1_CheckedChanged(object sender, EventArgs e)
        {
            txtCpf.Text = "";
            txtCnpj.Text = "";
            txtOrgaoEmissor.Text = "";
            txtRg.Text = "";
            txtCpf.Visible = false;
            txtCnpj.Visible = true;
            rbSexo0.Enabled = false;
            rbSexo1.Enabled = false;
            txtRg.Enabled = false;
            txtOrgaoEmissor.Enabled = false;
            rbSexo0.Checked = false;
            rbSexo1.Checked = false;

        }
        /// <summary>
        /// Método que carrega as opções de estado.
        /// </summary>
        public void CarregarUf()
        {
            ddlUf.DataSource = Enum.GetNames(typeof(Uf));
            ddlUf.DataBind();
        }

        #endregion
        //---------------------------------///-------------------------///---------------------------------///
        #region METODOS DE APOIO
        /// <summary>
        /// Método de apoio que limpa os campos quando solicitado
        /// </summary>
        public void LimparCampos()
        {
            lblClienteID.Text = "";
            txtBairro.Text = "";
            txtBuscar.Text = "";
            txtCep.Text = "";
            txtCidade.Text = "";
            txtCnpj.Text = "";
            txtComplemento.Text = "";
            txtCpf.Text = "";
            txtDtCriacao.Text = "";
            txtEmail.Text = "";
            txtEndereco.Text = "";
            txtOrgaoEmissor.Text = "";
            txtNome.Text = "";
            txtObservacao.Text = "";
            txtRg.Text = "";
            txtTel1.Text = "";
            txtTel2.Text = "";
            ddlUf.Text = "UF";
            rbTipoPessoa0.Checked = false;
            rbTipoPessoa1.Checked = false;
            rbTipoPessoa0.Checked = false;
            rbTipoPessoa1.Checked = false;
            rbSexo0.Checked = false;
            rbSexo1.Checked = false;
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