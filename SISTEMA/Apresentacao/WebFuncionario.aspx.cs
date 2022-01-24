using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Negocio.BO;
using Negocio.Model;
using Negocio;

namespace Apresentacao
{
    public partial class WebFuncionario : System.Web.UI.Page
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
                Cargo gerente = Cargo.GERENTE;
             
                Funcionario objFuncionario = new Funcionario();
                objFuncionario = ((Funcionario)Session["Logado"]);
                if (objFuncionario._Cargo == gerente)
                {
                    lblFuncionarioID.Visible = false;
                    if (!IsPostBack)
                    {
                        btnCancelar_Click(sender, e);                                                                                    
                        CarregarCargo();
                    }
                    txtDataAtivado.ReadOnly = true;
                    txtDataDesativado.ReadOnly = true;
                }
                else
                {
                    Response.Redirect("WebCriarLogin.aspx");
                }
            }
            else
            {
                Session.Remove("Logado");
                Response.Redirect("WebLogin.aspx");
            }
        }

        #region ASINATURA DE METODOS
        /// <summary>
        /// Cria os objetos
        /// </summary>
        static Funcionario objFuncionario;
        FuncionarioBO objFuncionarioBO;
        /// <summary>
        /// Cria as variaveis
        /// </summary>
        DateTime data;
        int numInt;


        #endregion

        #region CRUD
        /// <summary>
        /// Método que libera a tela para preenchimento das informações de um novo funcionário.
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
            panelObservacao.Enabled = true;
            txtDataAtivado.Text = DateTime.Now.ToShortDateString();
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
            panelObservacao.Enabled = false;
            panelObservacao.Enabled = false;
            panelBusca.Enabled = true;          

        }
        /// <summary>
        /// Método que adiciona ou altera as informações do funcionário.
        /// </summary>
        /// <param name="sender">Objeto do evento</param>
        /// <param name="e">Evento</param>
        protected void btnGravar_Click(object sender, EventArgs e)
        {

            try
            {
                if (!DateTime.TryParse(txtDataAtivado.Text, out data))
                    throw new Exception("Campo ATIVO está vazio!!!");
                if (!DateTime.TryParse(txtDtNascimento.Text, out data))
                    throw new Exception("Campo Data de Nascimento está vazio ou não é uma data válida. ex: 31/12/2013!!!");

                objFuncionario = new Funcionario();
                objFuncionario._Bairro = txtBairro.Text.ToUpper();
                objFuncionario._Cep = txtCep.Text;
                objFuncionario._Complemento = txtComplemento.Text.ToUpper();
                objFuncionario._Cpf = txtCpf.Text;
                objFuncionario._DataNascimento = DateTime.Parse(txtDtNascimento.Text);
                objFuncionario._Endereco = txtEndereco.Text.ToUpper();
                objFuncionario._Nome = txtNome.Text.ToUpper();
                objFuncionario._Observacao = txtObservacao.Text.ToUpper();
                objFuncionario._OrgaoEmissor = txtOrgaoEmissor.Text.ToUpper();
                objFuncionario._Rg = txtRg.Text;
                objFuncionario._Telefone2 = txtTel2.Text;
                objFuncionario._Telefone1 = txtTel1.Text;
                objFuncionario._Cargo = (Cargo)Enum.Parse(typeof(Cargo), ddlCargo.Text);
                objFuncionario._Categoria = txtCategoria.Text.ToUpper();
                objFuncionario._DataAtivado = DateTime.Parse(txtDataAtivado.Text);
                objFuncionario._Habilitacao = txtHabilitacao.Text;
                if (DateTime.TryParse(txtDataDesativado.Text, out data))
                    objFuncionario._DataDesativado = DateTime.Parse(txtDataDesativado.Text);                
                objFuncionarioBO = new FuncionarioBO();
                if (int.TryParse(lblFuncionarioID.Text, out numInt) && lblFuncionarioID.Text != "0")
                {
                    //altera o existente 
                    
                    objFuncionario._FuncionarioID = int.Parse(lblFuncionarioID.Text);
                    objFuncionarioBO.Gravar(objFuncionario);
                    lblMensagem.Text = "Alterado com Sucesso!!!";
                    lblMensagem1.Text = "Alterado com Sucesso!!!";
                    lblMensagemLogin.Text = "Deseja Alterar o Usuário ou senha deste funcionário agora?";  
                    
                }
                else
                {
                    //Gravar novo                        
                    objFuncionarioBO.Gravar(objFuncionario);
                    lblMensagem.Text = "Salvo com Sucesso!!!";
                    lblMensagem1.Text = "Salvo com Sucesso!!!";
                    lblMensagemLogin.Text = "Deseja criar Usuário e senha para este funcionário agora?";
                } 
                             

                if (ddlCargo.Text == "ATENDENTE" || ddlCargo.Text == "CAIXA" || ddlCargo.Text == "GERENTE")
                {
                    mensagemSucesso1.Visible = true;
                    mensagemErro1.Visible = false;                     
                    ClientScript.RegisterStartupScript(GetType(), "Show", "<script> $('#popUpMensagem1').modal('toggle');</script>");
                }
                else
                {
                    mensagemSucesso.Visible = true;
                    mensagemErro.Visible = false;  
                    ClientScript.RegisterStartupScript(GetType(), "Show", "<script> $('#popUpMensagem').modal('toggle');</script>");
                }
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
        /// Método que libera os campos para alteração das informações do funcionário já registrado.
        /// </summary>
        /// <param name="sender">Objeto do evento</param>
        /// <param name="e">Evento</param>
        protected void btnEditar_Click(object sender, EventArgs e)
        {
            btnEditar.Visible = false;
            btnGravar.Visible = true;
            panelBusca.Enabled = false;
            btnDesativar.Visible = false;
            panelConteudo.Enabled = true;
            panelObservacao.Enabled = true; 
        }
        /// <summary>
        ///Método que chama o popUp de confirmação de exlusão das informções do funcionário.
        /// </summary>
        /// <param name="sender">Objeto do evento</param>
        /// <param name="e">Evento</param>
        protected void btnDesativar_Click(object sender, EventArgs e)
        {
            ClientScript.RegisterStartupScript(GetType(), "Show", "<script> $('#popUpMensagemConfirmacao').modal('toggle');</script>");
        }
        /// <summary>
        ///Método que confirma a exlusão das informações do funcionário.
        /// </summary>
        /// <param name="sender">Objeto do evento</param>
        /// <param name="e">Evento</param>
        protected void OK_Click(object sender, EventArgs e)
        {
            try
            {
                txtDataDesativado.Text = DateTime.Now.ToShortDateString();
                objFuncionario._DataDesativado = Convert.ToDateTime(txtDataDesativado.Text);
                objFuncionarioBO = new FuncionarioBO();
                objFuncionarioBO.Gravar(objFuncionario);
               
                mensagemSucesso.Visible = true;
                mensagemErro.Visible = false;
                lblMensagem.Text = "Desativado com Sucesso!!!";
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
        ///Método que que busca uma lista de funcionários conforme situação do mesmo.
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
                switch (ddlTipoBusca.Text)
                {
                    case "0":
                        {
                            gvListaFuncionario.DataSource = objFuncionarioBO.BuscarListaFuncionario("Ativos", objFuncionario);
                            gvListaFuncionario.DataBind();
                            break;
                        }
                    case "1":
                        {                 
                           
                            gvListaFuncionario.DataSource = objFuncionarioBO.BuscarListaFuncionario("Inativos", objFuncionario);
                            gvListaFuncionario.DataBind();
                            break;
                        }
                    case "2":
                        {

                            gvListaFuncionario.DataSource = objFuncionarioBO.BuscarListaFuncionario("Todos", objFuncionario);
                            gvListaFuncionario.DataBind();
                            break;
                        }

                }
               
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

                ddlCargo.Text = objFuncionario._Cargo.ToString();
                txtBairro.Text = objFuncionario._Bairro;
                txtCep.Text = objFuncionario._Cep;
                lblFuncionarioID.Text = objFuncionario._FuncionarioID.ToString();
                txtComplemento.Text = objFuncionario._Complemento;
                txtCpf.Text = objFuncionario._Cpf;
                txtDtNascimento.Text = String.Format("{0:d}", objFuncionario._DataNascimento);
                txtEndereco.Text = objFuncionario._Endereco;
                txtNome.Text = objFuncionario._Nome;
                txtObservacao.Text = objFuncionario._Observacao;
                txtRg.Text = objFuncionario._Rg;
                txtTel2.Text = objFuncionario._Telefone2;
                txtTel1.Text = objFuncionario._Telefone1;
                txtOrgaoEmissor.Text = objFuncionario._OrgaoEmissor;
               
                txtDataAtivado.Text = String.Format("{0:d}", objFuncionario._DataAtivado);
                txtDataDesativado.Text = String.Format("{0:d}", objFuncionario._DataDesativado);
                txtCategoria.Text = objFuncionario._Categoria;
                txtHabilitacao.Text = objFuncionario._Habilitacao;                
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
        /// Método que preenche o dropdawlist com as opções de cargo do funcionário.
        /// </summary>
        public void CarregarCargo()
        {
            ddlCargo.DataSource = MetodosApoio.ListaElementos(typeof(Cargo));
            ddlCargo.DataTextField = "Value";
            ddlCargo.DataValueField = "Key";
            ddlCargo.DataBind();
        }
        /// <summary>
        /// Método de apoio que limpa os campos quando solicitado
        /// </summary>
        public void LimparCampos()
        {
            lblFuncionarioID.Text = "";
            txtBairro.Text = "";
            txtBuscar.Text = "";
            txtCep.Text = "";
            txtComplemento.Text = "";
            txtCpf.Text = "";
            txtDtNascimento.Text = "";
            txtDataAtivado.Text = "";
            txtDataDesativado.Text = "";
            txtEndereco.Text = "";
            txtNome.Text = "";
            txtObservacao.Text = "";
            txtRg.Text = "";
            txtTel1.Text = "";
            txtTel2.Text = "";
            txtHabilitacao.Text = "";
            txtCategoria.Text = "";
            txtOrgaoEmissor.Text = "";            
            ddlCargo.Text = "selecione";
        }
        /// <summary>
        /// Método de apoio que deixa visivel ou oculto conforme solicitado. 
        /// </summary>
        /// <param name="ocultar">Este recebe como parametros tru ou false.</param>
        public void visibleBotao(bool ocultar)
        {
            btnEditar.Visible = ocultar;
            btnDesativar.Visible = ocultar;
            btnGravar.Visible = ocultar;
            btnCancelar.Visible = ocultar;
        }       

        #endregion        

        #region METODOS AUTOPOSTBACK


        #endregion
        /// <summary>
        ///  Método que redireciona o administrador para a página de criar usuário e senha.
        /// </summary>
        /// <param name="sender">Objeto do evento</param>
        /// <param name="e">Evento</param>
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            Response.Redirect("WebCriarLogin.aspx");
        }
         /// <summary>
         ///  Método que chama o popup de opção se deseja criar usuário e senha.
         /// </summary>
        /// <param name="sender">Objeto do evento</param>
        /// <param name="e">Evento</param>
        protected void btnFechar_Click(object sender, EventArgs e)
        {
            ClientScript.RegisterStartupScript(GetType(), "Show", "<script> $('#popUpMensagemLogin').modal('toggle');</script>");
        }
        /// <summary>
        ///  Método que pinta os funcionários na lista quando desativados.
        /// </summary>
        /// <param name="sender">Objeto do evento</param>
        /// <param name="e">Evento</param>
        protected void gvListaFuncionario_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow) //verifica se a linha selecionada é uma linha de dados
            {

                if (e.Row.Cells[6].Text.Equals("Desativado")) //Quando for linha que contém dados, verifica se o valor da coluna 0 está como aberta
                {
                     e.Row.ForeColor = System.Drawing.Color.Red;
                }
            }
        }       
    }
}




