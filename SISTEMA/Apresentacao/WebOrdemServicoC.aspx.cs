using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Negocio.Model;
using Negocio.BO;
using System.Data.SqlClient;

namespace Apresentacao
{
    public partial class WebOrdemServicoC : System.Web.UI.Page
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
                Cargo atendente = Cargo.ATENDENTE;
                Funcionario objFuncionario = new Funcionario();
                objFuncionario = ((Funcionario)Session["Logado"]);              
                lblIdLi.Text = Hidden2.Value;
                carregarLiTab();
                Session["folha"] = "ordemServico";
                BloquearCampoPermanentes(true);
                txtBuscar.ReadOnly = true;                 
                OcultarLabesIDs();             
                if (objFuncionario._Cargo != atendente)
                {
                    if (!IsPostBack)
                    {
                        CarregarNomeMecanico();
                        btnCancelar_Click(sender, e);

                    }
                }
                else
                {
                    Response.Redirect("WebLogin.aspx");
                }
               
            }
            else
            {
                Response.Redirect("WebLogin.aspx");
            } 
        }
        #region ASINATURA DE METODOS DECLARAÇÕES
        /// <summary>
        /// Cria os objetos
        /// </summary>
        static OrdemServico objOrdemServico;              
        OrdemServicoBO objOrdemServicoBO;
        FuncionarioBO objFuncionarioBO;
        ItemServicoBO objItemServicoBO;
        ItemProdutoBO objItemProdutoBO;
        /// <summary>
        /// Cria a lista do tipo da classe
        /// </summary>
        static IList<Funcionario> listaFuncionario;
        /// <summary>
        /// Cria as variaveis
        /// </summary>
        int numInt;
        DateTime data;

        #endregion
        //METODOS Ordem de Serviço
        #region METODOS DA ORDEM DE SERVIÇO
        #region Crud

        /// <summary>
        /// Método que define o estado inicial da tela.
        /// </summary>
        /// <param name="sender">Objeto do evento</param>
        /// <param name="e">Evento</param>
        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            LimparCampos();
            panelConteudo.Visible = false;
            panelBusca.Visible = true;
            btnBuscar_Click(sender, e);
            btnImprimir.Visible = false;

        }

        // BOTÃO GRAVAR O.S.
        /// <summary>
        ///  Método que chama o popup de confirmação de finalização da ordem de serviço.
        /// </summary>
        /// <param name="sender">Objeto do evento</param>
        /// <param name="e">Evento</param>
        protected void btnGravar_Click(object sender, EventArgs e)
        {
            lblMensagemConfirmacao.Text = "Tem certeza que deseja Finalizar esta Ordem de Serviço?";
            btnImprimir.Visible = false;           
            ClientScript.RegisterStartupScript(GetType(), "Show", "<script> $('#popUpMensagemConfirmacao').modal('toggle');</script>");
           
        }

       
        #endregion

        #region Busca
        /// <summary>
        ///Método que que busca uma lista de ordem de serviço conforme o tipo definido no drop draw list.
        /// </summary>
        /// <param name="sender">Objeto do evento</param>
        /// <param name="e">Evento</param>
        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            panelConteudo.Visible = false;
            panelBusca.Visible = true;
            objOrdemServicoBO = new OrdemServicoBO();
            switch (ddlTipoBusca.Text)
            {
                case "0":
                    {

                        gvListaOS.DataSource = objOrdemServicoBO.BuscarListaOrdemServico("abertas");
                        gvListaOS.DataBind();
                        break;
                    }
                case "1":
                    {
                        gvListaOS.DataSource = objOrdemServicoBO.BuscarListaOrdemServico("fechada");
                        gvListaOS.DataBind();
                        break;
                    }
                case "2":
                    {
                        gvListaOS.DataSource = objOrdemServicoBO.BuscarListaOrdemServico("todos");
                        gvListaOS.DataBind();
                        break;
                    }
                case "3":
                    {
                        if (DateTime.TryParse(txtBuscar.Text, out data))
                        {
                            DateTime Data = Convert.ToDateTime(txtBuscar.Text).Date;
                            gvListaOS.DataSource = objOrdemServicoBO.BuscarListaOrdemServicoData("todos", Data);
                            gvListaOS.DataBind();
                            txtBuscar.ReadOnly = false;
                            txtBuscar.Text = "";
                        }
                        else
                        {
                            lblMensagem.Text = "Insira uma data válida!!!";
                            mensagemSucesso.Visible = false;
                            mensagemErro.Visible = true;                          
                            txtBuscar.ReadOnly = false;
                            txtBuscar.Text = "";
                            ClientScript.RegisterStartupScript(GetType(), "Show", "<script> $('#popUpMensagem').modal('toggle');</script>");
                        }

                        break;

                    }

            }

        }
        /// <summary>
        ///Método que que busca uma ordem de serviço e preenche os campos da tela.
        /// </summary>
        /// <param name="sender">Objeto do evento</param>
        /// <param name="e">Evento</param>
        protected void gvListaOS_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblIdLi.Text = "a";
            carregarLiTab();
            //divProdutosInseridos.Visible = false;
            //divServicoInserido.Visible = false;

            objOrdemServico = new OrdemServico();
            objOrdemServicoBO = new OrdemServicoBO();
            objOrdemServico._OrdemServicoID = Convert.ToInt32(gvListaOS.SelectedDataKey.Value);
            objOrdemServico = objOrdemServicoBO.BuscarOrdemServico(objOrdemServico);

            lblClienteID.Text = objOrdemServico._Cliente._ClienteID.ToString();
            lblMotoID.Text = objOrdemServico._Moto._MotoID.ToString();
            lblOrdemServicoID.Text = objOrdemServico._OrdemServicoID.ToString();
            txtNomeCliente.Text = objOrdemServico._Cliente._Nome;
            txtDataAbertura.Text = objOrdemServico._DataAberturaOS.ToString();
            txtCnpjCpf.Text = objOrdemServico._Cliente._CpfCnpj;
            txtTelefone.Text = objOrdemServico._Cliente._Telefone1;
            txtEndereco.Text = objOrdemServico._Cliente._Endereco;
            ddlNomeMecanico.Text = objOrdemServico._Funcionario._FuncionarioID.ToString();
            txtPlaca.Text = objOrdemServico._Moto._Placa;
            txtMarcaModelo.Text = objOrdemServico._Moto._MarcaModelo;
            txtAno.Text = objOrdemServico._Moto._AnoFabricacao;
            txtKm.Text = objOrdemServico._Km;
            txtObservacao.Text = objOrdemServico._Observacao;
            if (objOrdemServico._TipoDesconto == 0)
            {                
                txtDescontoReal.Text =objOrdemServico._Desconto.ToString();
                divReal.Visible = true;
                divPorcento.Visible = false;
            }
            if (objOrdemServico._TipoDesconto == 1)
            {
                txtDescontoPorcento.Text = objOrdemServico._Desconto.ToString();
                divReal.Visible = false;
                divPorcento.Visible = true;
            }
            if (objOrdemServico._TipoDesconto == null)
            {               
                divPorcento.Visible = false;
                divReal.Visible = true;
            }
            if (objOrdemServico._DataFechamentoOS != null)
                txtDataFinalizacao.Text = objOrdemServico._DataFechamentoOS.ToString();


            txtTotalProdutoColuna.Text = objOrdemServico._ValorTotalProduto.ToString();
            txtTotalServicoColuna.Text = objOrdemServico._ValorTotalServicos.ToString();  
           
            txtTotalComDesconto.Text = objOrdemServico._TotalOSdesconto.ToString("C2");
            txtTotalOS.Text =objOrdemServico._ValorTotal.ToString();
            if (objOrdemServico._ListaItemProduto != null)
            {
                //divProdutosInseridos.Visible = true;
                objItemProdutoBO = new ItemProdutoBO();
                gvProdutosInseridos.DataSource = objItemProdutoBO.listaIDataTableItemProduto(objOrdemServico);
                gvProdutosInseridos.DataBind();

            }
            if (objOrdemServico._ListaItemServico !=null)
            {
                //divServicoInserido.Visible = true;
                objItemServicoBO = new ItemServicoBO();
                gvServicoInseridos.DataSource = objItemServicoBO.buscaListaDataTableItemServico(objOrdemServico);
                gvServicoInseridos.DataBind();
            }             
            
            panelConteudo.Visible = true;
            panelConteudo.Enabled = false;
            panelBusca.Visible = false;
            btnCancelar.Visible = true;
            btnGravar.Visible = true;
            if (objOrdemServico._DataFechamentoOS != null)
                btnGravar.Visible = false;
            btnImprimir.Visible = true;
                                                       
        }

        #endregion

        #endregion

    

       

       

        //METODOS de apoio
        #region METODOS DE APOIO

        /// <summary>
        /// Método que carrega os funcionários mecânico no dropdawlist
        /// </summary>
        public void CarregarNomeMecanico()
        {
            listaFuncionario = new List<Funcionario>();
            objFuncionarioBO = new FuncionarioBO();
            listaFuncionario = objFuncionarioBO.BuscarListaMecanico();
            ddlNomeMecanico.DataTextField = "_Nome";
            ddlNomeMecanico.DataValueField = "_FuncionarioID";
            ddlNomeMecanico.DataSource = listaFuncionario;
            ddlNomeMecanico.DataBind();
        }
        /// <summary>
        /// Método que oculta todas as label que recebem o id
        /// </summary>
        public void OcultarLabesIDs()
        {
            lblClienteID.Visible = false;
            lblMotoID.Visible = false;
            lblOrdemServicoID.Visible = false;
            lblItemProdutoID.Visible = false;
            lblQtdeAterior.Visible = false;
            lblIdLi.Visible = false;
        }
        /// <summary>
        /// Método que bloquea ou desbloqueia conforme solicitado.
        /// </summary>
        /// <param name="bloquear">parâmetro que recebe tru ou false.</param>
        public void BloquearCampoPermanentes(bool bloquear)
        {
            txtDataAbertura.ReadOnly = bloquear;
            txtDataFinalizacao.ReadOnly = bloquear;
            txtEndereco.ReadOnly = bloquear;
            txtTelefone.ReadOnly = bloquear;
            txtMarcaModelo.ReadOnly = bloquear;
            txtAno.ReadOnly = bloquear;         
            txtTotalProdutoColuna.ReadOnly = bloquear;
            txtTotalServicoColuna.ReadOnly = bloquear;     
            txtBairro.ReadOnly = bloquear;
            txtTotalOS.ReadOnly = bloquear;
            txtTotalComDesconto.ReadOnly = bloquear;
            txtCnpjCpf.ReadOnly = bloquear;
            btnGravar.Visible = !bloquear;
            btnCancelar.Visible = !bloquear;
           
        }
        /// <summary>
        /// Método de apoio que limpa os campos quando solicitado
        /// </summary>
        public void LimparCampos()
        {
            lblClienteID.Text = "";
            lblItemProdutoID.Text = "";
            lblMotoID.Text = "";
            lblOrdemServicoID.Text = "";
            lblProdutoID.Text = "";
            lblQtdeAterior.Text = "";
            txtDataAbertura.Text = "";
            txtDataFinalizacao.Text = "";
            txtCnpjCpf.Text = "";
            txtEndereco.Text = "";
            txtNomeCliente.Text = "";
            txtObservacao.Text = "";
            txtTelefone.Text = "";
            txtMarcaModelo.Text = "";
            txtPlaca.Text = "";
            txtAno.Text = "";
            txtKm.Text = "";
            txtBuscar.Text = "";             
            txtTotalProdutoColuna.Text = "";
            txtTotalServicoColuna.Text = "";
            txtBairro.Text = "";
            txtDescontoPorcento.Text = "";
            txtDescontoReal.Text = "";             
            txtTotalComDesconto.Text = "";       

        }       
        #endregion

        #region Metodo HTML
        /// <summary>
        /// Método html que defina a tab que deve está aberta.
        /// </summary>
        public void carregarLiTab()
        {
            if (lblIdLi.Text != "")
            {
                Literal1.Text = "<li><a href=\"#Informacoes\" data-toggle=\"tab\" id=\"a\" ><font><font class=\"goog-text-highlight\">Informações da OS</font></font></a></li>";
                Literal2.Text = "<li><a href=\"#produto\" data-toggle=\"tab\" id=\"b\"><font><font>Produtos e Serviços</font></font></a></li>";              
                Literal1b.Text = " <div class=\"tab-pane fade\" id=\"Informacoes\">";
                Literal1c.Text = "</div>";
                Literal2b.Text = " <div class=\"tab-pane fade\" id=\"produto\">";
                Literal2c.Text = "</div>";
             
                switch (lblIdLi.Text)
                {
                    case "a":
                        Literal1.Text = "<li class=\"active\"><a href=\"#Informacoes\" data-toggle=\"tab\" id=\"a\" ><font><font class=\"goog-text-highlight\">Informações da OS</font></font></a></li>";
                        Literal1b.Text = " <div class=\"tab-pane fade in active\" id=\"Informacoes\">";
                        Literal1c.Text = "</div>";
                        break;
                    case "b":
                        Literal2.Text = "<li class=\"active\"><a href=\"#produto\" data-toggle=\"tab\" id=\"b\"><font><font>Produtos e Serviços</font></font></a></li>";
                        Literal2b.Text = " <div class=\"tab-pane fade in active\" id=\"produto\">";
                        Literal2c.Text = "</div>";

                        break;                  
                }
            }
            else
            {
                Literal1.Text = "<li class=\"active\"><a href=\"#Informacoes\" data-toggle=\"tab\" id=\"a\" ><font><font class=\"goog-text-highlight\">Informações da OS</font></font></a></li>";
                Literal1b.Text = " <div class=\"tab-pane fade in active\" id=\"Informacoes\">";
                Literal1c.Text = "</div>";
                Literal2.Text = "<li><a href=\"#produto\" data-toggle=\"tab\" id=\"b\"><font><font>Produtos e Serviços</font></font></a></li>";
                Literal2b.Text = " <div class=\"tab-pane fade\" id=\"produto\">";
                Literal2c.Text = "</div>";         
               
            }
        }
        #endregion
        /// <summary>
        ///    Método de confirmação de impressão.
        /// </summary>
        /// <param name="sender">Objeto do evento</param>
        /// <param name="e">Evento</param>
        protected void OK_Click(object sender, EventArgs e)
        {
            try
            {
                objOrdemServicoBO = new OrdemServicoBO();
                if (int.TryParse(lblOrdemServicoID.Text, out numInt) && lblOrdemServicoID.Text != "0")
                {
                    if (!DateTime.TryParse(txtDataFinalizacao.Text, out data))
                    {
                        txtDataFinalizacao.Text = DateTime.Now.ToString(); 
                        objOrdemServico._DataFechamentoOS = Convert.ToDateTime(txtDataFinalizacao.Text);
                    }
                    btnImprimir.Visible = true;
                    btnCancelar.Visible = true;                  
                    objOrdemServicoBO.Gravar(objOrdemServico);
                    mensagemSucesso.Visible = true;
                    mensagemErro.Visible = false;
                    lblMensagem.Text = "Finalizado com Sucesso!!!";
                    ClientScript.RegisterStartupScript(GetType(), "Show", "<script> $('#popUpMensagem').modal('toggle');</script>");
                }                              
                    
            }
            catch (Exception erro)
            {
                if (erro.Message == "Selecione um Cliente na Lista de Busca para esta Ordem de Serviço!!!")
                    txtNomeCliente.Text = "";
                if (erro.Message == "Selecione uma Moto na Lista de Busca para esta Ordem de Serviço!!!")
                    txtPlaca.Text = "";
                lblMensagem.Text = erro.Message;
                mensagemSucesso.Visible = false;
                mensagemErro.Visible = true;
                ClientScript.RegisterStartupScript(GetType(), "Show", "<script> $('#popUpMensagem').modal('toggle');</script>");
            }
        }

        protected void btnImprimir_Click(object sender, EventArgs e)
        {
            //Response.Redirect("Relatorios/WebOrdemServicoImpressao.aspx?cod="+objOrdemServico._OrdemServicoID);
            int id = objOrdemServico._OrdemServicoID;
            Session["imprimirRelatorio"] = id;
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "_new", "window.open('Relatorios/WebOrdemServicoImpressao.aspx');", true);
            btnImprimir.Visible = true;
            btnGravar.Visible = true;
            btnCancelar.Visible = true;
           
        }
        /// <summary>
        /// Método que pinta a linha das ordem de serviços finalizadas.
        /// </summary>
        /// <param name="sender">Objeto do evento</param>
        /// <param name="e">Evento</param>
        protected void gvListaOS_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                if (e.Row.Cells[6].Text.Equals("FINALIZADA"))
                {
                    //e.Row.BackColor = System.Drawing.Color.;
                    e.Row.ForeColor = System.Drawing.Color.Red;
                }
            }
        }
        protected void ddlTipoBusca_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlTipoBusca.Text != "3")
            {
                txtBuscar.ReadOnly = true;
              
            }
            else
            {
               
                txtBuscar.ReadOnly = false;
            }
            txtBuscar.Text = "";
        }

     
        //todo Colocar uma mensagem de erro na classe dao no metodo exluir de todas as classe ESTE NÃO PODE SER EXLUIDO POIS ESTÁ VINCUNLADO A OUTRO FORMULÁRIO.
       
    }
}