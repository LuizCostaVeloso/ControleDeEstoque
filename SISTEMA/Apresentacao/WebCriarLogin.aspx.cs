using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Negocio.BO;
using Negocio.Model;

namespace Apresentacao
{
    public partial class WebCriarLogin : System.Web.UI.Page
    {
        Cargo gerente = Cargo.GERENTE;
        Cargo atendente = Cargo.ATENDENTE;
        Cargo caixa = Cargo.CAIXA;
        static Funcionario objFuncionarioLogado;
        static Funcionario objFuncionario;

        /// <summary>
        /// Dentro do Page Load fica o metodo de validação da página definindo quais os usuários tem acesso e suas ações.
        /// </summary>
        /// <param name="sender">Objeto do evento</param>
        /// <param name="e">Evento</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if ((Funcionario)Session["Logado"] != null)
            {
                objFuncionarioLogado = new Funcionario();
                objFuncionarioLogado = ((Funcionario)Session["Logado"]);
                Session["folha"] = "cadastro";
                visibleBotao(true);

                if (objFuncionarioLogado._Cargo == gerente)
                {
                    if (!IsPostBack)
                        btnCancelar_Click(sender, e);

                }
                else
                {
                    if (!IsPostBack)
                        btnCancelarSenha_Click(sender, e);

                }
            }
            else
            {
                Response.Redirect("WebLogin.aspx");
            }
        }

        #region ASINATURA DE METODOS
        /// <summary>
        /// Cria os objetos
        /// </summary>
        FuncionarioBO objFuncionarioBO;
        /// <summary>
        /// Cria as variaveis
        /// </summary>
        int numInt;
        string variavel, cargo;
        #endregion

        #region BUSCAS
        /// <summary>
        ///Método que que busca uma lista de funcionários.
        /// </summary>
        /// <param name="sender">Objeto do evento</param>
        /// <param name="e">Evento</param>
        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                objFuncionario = new Funcionario();
                objFuncionario._Nome = txtBuscar.Text;
                objFuncionarioBO = new FuncionarioBO();
                gvListaFuncionario.DataSource = objFuncionarioBO.BuscarListaFuncionario("Ativos", objFuncionario);
                gvListaFuncionario.DataBind();
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
        ///Método que que busca as informaçoes de um funcionário e preenche os campos da tela.
        /// </summary>
        /// <param name="sender">Objeto do evento</param>
        /// <param name="e">Evento</param>
        protected void gvListaFuncionario_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                LimparCampos();
                objFuncionario = new Funcionario();
                objFuncionario._FuncionarioID = Convert.ToInt32(gvListaFuncionario.SelectedDataKey.Value);
                objFuncionarioBO = new FuncionarioBO();
                objFuncionario = objFuncionarioBO.BuscarFuncionario(objFuncionario);
                lblCPF.Text = objFuncionario._Cpf;
                lblNome.Text = objFuncionario._Nome;

                if (!String.IsNullOrEmpty(objFuncionario._Login))
                {
                    txtLogin.Text = objFuncionario._Login;
                    txtLogin2.Text = objFuncionario._Login;
                }


                visibleBotao(true);
                panelBusca.Enabled = false;
                BloquearCampoGerente(false);

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
        /// <summary>
        /// Método que grava login e senha do usuário.
        /// </summary>
        /// <param name="sender">Objeto do evento</param>
        /// <param name="e">Evento</param>
        protected void btnGravar_Click(object sender, EventArgs e)
        {

            try
            {
                variavel = objFuncionario._FuncionarioID.ToString();

                objFuncionario._Senha = txtSenha.Text;
                objFuncionario._Senha2 = txtSenha2.Text;
                objFuncionario._Login = txtLogin.Text;
                objFuncionario._Login2 = txtLogin2.Text;
                objFuncionarioBO = new FuncionarioBO();

                if (int.TryParse(variavel, out numInt) && objFuncionario._FuncionarioID != 0)
                {
                    if (objFuncionarioLogado._Cargo == gerente)
                    {

                        objFuncionarioLogado._Senha = txtSenhaGerente.Text;
                        Funcionario objFunc = new Funcionario();
                        //metodo que testa que se quem esta logado e o mesmo que quer alterar ou criar login e senha
                        objFunc = objFuncionarioBO.BuscarLoginSenha(objFuncionarioLogado);
                        cargo = objFunc._Cargo.ToString();
                        if (objFunc != null)
                            objFuncionarioBO.GravarSenha(objFuncionario, cargo);


                    }
                    else
                    {
                        throw new Exception("Você não tem permissão!!!");

                    }

                    lblMensagem.Text = "Salvo com Sucesso!!!";
                    lblMensagem.Text = "Salvo com Sucesso!!!";
                    mensagemSucesso.Visible = true;
                    mensagemErro.Visible = false;
                    ClientScript.RegisterStartupScript(GetType(), "Show", "<script> $('#popUpMensagem').modal('toggle');</script>");
                }

                btnCancelar_Click(sender, e);
            }
            catch (Exception erro)
            {

                lblMensagem.Text = erro.Message;

                if (lblMensagem.Text == "Digite sua Senha!!!")
                    txtSenhaAtual.Text = "";

                if (lblMensagem.Text == "Senha atual inválida!!!")
                    txtSenhaAtual.Text = "";
                if (lblMensagem.Text == "O campos Senha é obrigatório!!!")
                {
                    txtSenha.Text = "";
                    txtSenha2.Text = "";
                }
                if (lblMensagem.Text == "Os Campos da Senha não são iguais, digite novamente!!!")
                {
                    txtSenha.Text = "";
                    txtSenha2.Text = "";
                }
                if (lblMensagem.Text == "O campo do usuário é obrigatório!!!")
                {
                    txtLogin.Text = "";
                    txtLogin2.Text = "";
                }
                if (lblMensagem.Text == "Os Campos de usuário não são iguais, digite novamente!!!")
                {
                    txtLogin.Text = "";
                    txtLogin2.Text = "";
                }
                if (lblMensagem.Text == "Usuário já existe no sistema!!!")
                {
                    txtLogin.Text = "";
                    txtLogin2.Text = "";
                }
                
                mensagemSucesso.Visible = false;
                mensagemErro.Visible = true;
                txtSenhaGerente.Focus();
                ClientScript.RegisterStartupScript(GetType(), "Show", "<script> $('#popUpMensagem').modal('toggle');</script>");

            }
        }
        /// <summary>
        /// Método que altera a senha do usuário.
        /// </summary>
        /// <param name="sender">Objeto do evento</param>
        /// <param name="e">Evento</param>
        protected void btnGravarSenha_Click(object sender, EventArgs e)
        {
            try
            {
                variavel = objFuncionarioLogado._FuncionarioID.ToString();

                objFuncionarioLogado._Senha = txtSenha.Text;
                objFuncionarioLogado._Senha2 = txtSenha2.Text;

                objFuncionarioBO = new FuncionarioBO();

                if (int.TryParse(variavel, out numInt) && objFuncionarioLogado._FuncionarioID != 0)
                {
                    if (objFuncionarioLogado._Cargo == atendente || objFuncionarioLogado._Cargo == caixa)
                    {

                        objFuncionarioLogado._Senha = txtSenhaAtual.Text;
                        Funcionario objFunc = new Funcionario();
                        objFunc = objFuncionarioBO.BuscarLoginSenha(objFuncionarioLogado);
                        cargo = objFunc._Cargo.ToString();
                        if (objFunc != null)
                        {
                            objFuncionarioLogado._Senha = txtSenha.Text;
                            objFuncionarioLogado._Senha2 = txtSenha2.Text;
                            objFuncionarioBO.GravarSenha(objFuncionarioLogado, cargo);
                        }



                    }
                    else
                    {
                        throw new Exception("Você não tem permissão!!!");

                    }

                    lblMensagem.Text = "Alterado com Sucesso!!!";
                    mensagemSucesso.Visible = true;
                    mensagemErro.Visible = false;
                    ClientScript.RegisterStartupScript(GetType(), "Show", "<script> $('#popUpMensagem').modal('toggle');</script>");
                }

                btnCancelarSenha_Click(sender, e);
            }
            catch (Exception erro)
            {

                lblMensagem.Text = erro.Message;
                if (lblMensagem.Text == "Digite sua Senha Atual!!!")
                {
                    txtSenhaAtual.Text = "";
                    txtLogin.Text = "";
                    txtLogin2.Text = "";                    
                }
                if (lblMensagem.Text == "O campos Senha é obrigatório!!!")
                {
                    txtSenha.Text = "";
                    txtSenha2.Text = "";
                }
                if (lblMensagem.Text == "Os Campos da Senha não são iguais, digite novamente!!!")
                {
                    txtSenha.Text = "";
                    txtSenha2.Text = "";
                }
                if (lblMensagem.Text == "Senha atual inválida!!!")
                {
                    txtSenhaAtual.Text = "";
                }

               
                mensagemSucesso.Visible = false;
                mensagemErro.Visible = true;
                txtSenhaGerente.Focus();
                btnGravar.Visible = false;
                btnCancelar.Visible = false;
                ClientScript.RegisterStartupScript(GetType(), "Show", "<script> $('#popUpMensagem').modal('toggle');</script>");
            }

        }
        /// <summary>
        /// Método que preenche o funcionário
        /// </summary>
        public void PrencherFuncionario()
        {
            lblCPF.Text = objFuncionarioLogado._Cpf;
            lblNome.Text = objFuncionarioLogado._Nome;
            lblUsuario.Text = objFuncionarioLogado._Login;
        }
        /// <summary>
        /// Método de apoio que limpa os campos quando solicitado
        /// </summary>
        public void LimparCampos()
        {
            txtBuscar.Text = "";
            lblCPF.Text = "";
            lblNome.Text = "";
            txtLogin.Text = "";
            txtLogin2.Text = "";
            txtSenha.Text = "";
            txtSenha2.Text = "";
            txtSenhaAtual.Text = "";
            txtSenhaGerente.Text = "";
        }
        /// <summary>
        /// Método de apoio que deixa visivel ou oculto conforme solicitado. 
        /// </summary>
        /// <param name="ocultar">Este recebe como parametros tru ou false.</param>
        public void visibleBotao(bool ocultar)
        {
            btnGravar.Visible = ocultar;
            btnCancelar.Visible = ocultar;

        }
        /// <summary>
        /// Método que define o estado inicial da tela se administrador.
        /// </summary>
        /// <param name="sender">Objeto do evento</param>
        /// <param name="e">Evento</param>
        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            panelBusca.Enabled = true;
            pnlUsuario.Visible = false;
            btnGravar.Visible = false;
            btnGravarSenha.Visible = false;
            btnEditar.Visible = false;
            btnCancelar.Visible = false;
            btnCancelarSenha.Visible = false;
            BloquearCampoGerente(true);
            LimparCampos();
        }
        /// <summary>
        /// Método que define o estado inicial da tela se usuário.
        /// </summary>
        /// <param name="sender">Objeto do evento</param>
        /// <param name="e">Evento</param>
        protected void btnCancelarSenha_Click(object sender, EventArgs e)
        {
            LimparCampos();
            lblUsuario.Text = "";
            pnlGerente.Visible = false;
            panelBusca.Visible = false;
            btnGravar.Visible = false;
            btnCancelar.Visible = false;
            btnCancelarSenha.Visible = false;
            btnGravarSenha.Visible = false;
            btnEditar.Visible = true;
            BloquearCampoUsuario(true);

        }
        /// <summary>
        /// Método que bloqueia o campo do usuário
        /// </summary>
        /// <param name="habilitar">Parâmetro que recebe true ou false.</param>
        public void BloquearCampoUsuario(bool habilitar)
        {
            txtSenha.ReadOnly = habilitar;
            txtSenha2.ReadOnly = habilitar;
            txtSenhaAtual.ReadOnly = habilitar;
        }
        /// <summary>
        /// Método que bloqueia o campo do gerente
        /// </summary>
        /// <param name="habilitar">Parâmetro que recebe true ou false.</param>
        public void BloquearCampoGerente(bool habilitar)
        {
            txtLogin.ReadOnly = habilitar;
            txtLogin2.ReadOnly = habilitar;
            txtSenha.ReadOnly = habilitar;
            txtSenha2.ReadOnly = habilitar;
            txtSenhaGerente.ReadOnly = habilitar;
        }
        /// <summary>
        /// Método que libera os campos para alteração das informações do funcionáiro.
        /// </summary>
        /// <param name="sender">Objeto do evento</param>
        /// <param name="e">Evento</param>
        protected void btnEditar_Click(object sender, EventArgs e)
        {
            pnlGerente.Visible = false;
            panelBusca.Visible = false;
            btnGravar.Visible = false;
            btnCancelar.Visible = false;
            btnCancelarSenha.Visible = true;
            btnGravarSenha.Visible = true;
            btnEditar.Visible = false;
            BloquearCampoUsuario(false);
            PrencherFuncionario();
        }
    }
}