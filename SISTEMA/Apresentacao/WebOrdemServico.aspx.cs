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
    public partial class WebOrdemServico : System.Web.UI.Page
    {     /// <summary>
        /// Dentro do Page Load fica o metodo de validação da página definindo quais os usuários tem acesso e suas ações.
        /// </summary>
        /// <param name="sender">Objeto do evento</param>
        /// <param name="e">Evento</param>
        protected void Page_Load(object sender, EventArgs e)
        {

            if ((Funcionario)Session["Logado"] != null)
            {
                Cargo gerente = Cargo.GERENTE;

                Funcionario objFuncionario = new Funcionario();
                objFuncionario = ((Funcionario)Session["Logado"]);
                if (objFuncionario._Cargo == gerente)
                {
                    lblIdLi.Text = Hidden2.Value;
                    carregarLiTab();
                    Session["folha"] = "ordemServico";
                    //spanDataFinalizacao.Visible = false;
                    BloquearCampoPermanentes(true);
                    OcultarLabesIDs();
                    if (!IsPostBack)
                    {
                        CarregarNomeMecanico();
                        btnCancelar_Click(sender, e);
                    }
                }
                else
                    Response.Redirect("WebLogin.aspx");
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
        static Cliente objCliente;
        static Moto objMoto;
        static Servico objServico;
        static ItemServico objItemServico;
        static Produto objProduto;
        static ItemProduto objItemProduto;
        OrdemServicoBO objOrdemServicoBO;
        ClienteBO objClienteBO;
        FuncionarioBO objFuncionarioBO;
        MotoBO objMotoBO;
        ServicoBO objServicoBO;
        ItemServicoBO objItemServicoBO;
        ProdutoBO objProdutoBO;
        ItemProdutoBO objItemProdutoBO;
        /// <summary>
        /// Cria a lista do tipo da classe
        /// </summary>
        static IList<Funcionario> listaFuncionario;
        /// <summary>
        /// Cria as variaveis
        /// </summary>
        int numInt;
        decimal numDecimal;
        DateTime data;

        #endregion
        //METODOS Ordem de Serviço
        #region METODOS DA ORDEM DE SERVIÇO
        #region Crud
        /// <summary>
        /// Método que libera a tela para preenchimento das informações de um nova ordem de Serviço
        /// </summary>
        /// <param name="sender">Objeto do evento</param>
        /// <param name="e">Evento</param>
        protected void btnNovo_Click(object sender, EventArgs e)
        {
            lblIdLi.Text = "a";
            carregarLiTab();
            LimparCampos();

            btnGravar.Visible = true;
            btnCancelar.Visible = true;
            panelConteudo.Enabled = true;
            panelConteudo.Visible = true;
            panelBusca.Visible = false;

            divReal.Visible = true;
            divPorcento.Visible = false;
            bloquearCamposNPermanentes(false);
            txtPlaca.ReadOnly = false;
            txtNomeProduto.ReadOnly = false;
            txtQuantidade.ReadOnly = true;
            txtKm.ReadOnly = false;
            txtNomeCliente.ReadOnly = false;
            txtDataAbertura.Text = DateTime.Now.ToString(); 
           
            spanLimparFinalizacao.Visible = false;
            spanLimparInfPlaca.Visible = false;
            spanLimparInfCliente.Visible = false;
            spanLimparInsercaoProduto.Visible = false;
            spanInserirProduto.Visible = false;
            spanBuscarCliente.Visible = true;
            spanBuscarPlaca.Visible = true;
            spanBuscarProduto.Visible = true;
            spanLimparQuantidade.Visible = false;
            gvProdutosInseridos.DataSource = null;
            gvProdutosInseridos.DataBind();
            gvServicoInseridos.DataSource = null;
            gvServicoInseridos.DataBind();



        }
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
            visibleBotao(false);
            btnBuscar_Click(sender, e);

        }

        // BOTÃO GRAVAR O.S.
        /// <summary>
        /// Método que chama o método gravar para gravar ou alterar a ordem de serviço.
        /// </summary>
        /// <param name="sender">Objeto do evento</param>
        /// <param name="e">Evento</param>
        protected void btnGravar_Click(object sender, EventArgs e)
        {
            try
            {
                Gravar();
                btnCancelar_Click(sender, e);


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
        /// <summary>
        /// Método que grava ou altera a ordem de serviço quando solicitado
        /// </summary>
        public void Gravar()
        {
            try
            {
                if (!decimal.TryParse(lblClienteID.Text, out numDecimal))
                    throw new Exception("Selecione um Cliente na Lista de Busca para esta Ordem de Serviço!!!");
                if (!decimal.TryParse(lblMotoID.Text, out numDecimal))
                    throw new Exception("Selecione uma Moto na Lista de Busca para esta Ordem de Serviço!!!");
                objOrdemServico = new OrdemServico();
                objOrdemServico._DataAberturaOS = DateTime.Parse(txtDataAbertura.Text);
                objOrdemServico._Km = txtKm.Text;
                objOrdemServico._Observacao = txtObservacao.Text.ToUpper();
                objOrdemServico._Cliente._ClienteID = int.Parse(lblClienteID.Text);
                objOrdemServico._Moto._MotoID = int.Parse(lblMotoID.Text);
                objOrdemServico._Funcionario._FuncionarioID = int.Parse(ddlNomeMecanico.SelectedValue);
                objOrdemServicoBO = new OrdemServicoBO();
                if (int.TryParse(lblOrdemServicoID.Text, out numInt) && lblOrdemServicoID.Text != "0")
                {
                    if (rbTipoDesconto0.Checked == true)
                    {
                        objOrdemServico._TipoDesconto = 0;
                        if (decimal.TryParse(txtDescontoReal.Text, out numDecimal) && txtDescontoReal.Text != "0")
                            objOrdemServico._Desconto = decimal.Parse(txtDescontoReal.Text);
                    }

                    if (rbTipoDesconto1.Checked == true)
                    {
                        objOrdemServico._TipoDesconto = 1;

                        if (decimal.TryParse(txtDescontoPorcento.Text, out numDecimal) && txtDescontoPorcento.Text != "0")
                            objOrdemServico._Desconto = decimal.Parse(txtDescontoPorcento.Text);
                    }


                    if (DateTime.TryParse(txtDataFinalizacao.Text, out data))
                        objOrdemServico._DataFechamentoOS = DateTime.Parse(txtDataFinalizacao.Text);

                    objOrdemServico._OrdemServicoID = int.Parse(lblOrdemServicoID.Text);
                    objOrdemServicoBO.Gravar(objOrdemServico);
                }
                else
                {
                    //Gravar novo  
                    objOrdemServico._OrdemServicoID = objOrdemServicoBO.Gravar(objOrdemServico);
                    lblOrdemServicoID.Text = objOrdemServico._OrdemServicoID.ToString();


                }

            }
            catch (Exception erro)
            {
                throw new Exception(erro.Message);
            }
        }


        /// <summary>
        /// Método que libera os campos para alteração de uma ordem de serviço ja registrado.
        /// </summary>
        /// <param name="sender">Objeto do evento</param>
        /// <param name="e">Evento</param>
        protected void btnEditar_Click(object sender, EventArgs e)
        {
            if (objOrdemServico._DataFechamentoOS != null)
            {
                lblIdLi.Text = "a";
                carregarLiTab();
                panelConteudo.Enabled = true;
                BloquearCampoPermanentes(true);
                spanLimparFinalizacao.Visible = true;                         
              
                btnLimparDataFinalizacao.Enabled = true;                
                bloquearCamposNPermanentes(true);              
                divDataFinalizacao.Attributes.Add("class", "input-group");
            }
            else
            {
               
                panelConteudo.Enabled = true;
                bloquearCamposNPermanentes(false);
                spanInserirProduto.Visible = false;
                txtNomeProduto.ReadOnly = false;
                spanBuscarProduto.Visible = true;
                spanBuscarCliente.Visible = false;
                spanBuscarPlaca.Visible = false;
                spanLimparInfCliente.Visible = true;
                spanLimparInfPlaca.Visible = true;
                txtNomeCliente.ReadOnly = true;
                txtPlaca.ReadOnly = true;
                txtKm.ReadOnly = false;
                txtQuantidade.ReadOnly = true;
                divDataFinalizacao.Attributes.Add("class", "");
            }
            btnExcluir.Visible = false;
            btnEditar.Visible = false;
            btnGravar.Visible = true;
            btnImprimir.Visible = false; 
        }
        /// <summary>
        ///Método que chama o popUp de confirmação de exlusão de uma ordem de serviço
        /// </summary>
        /// <param name="sender">Objeto do evento</param>
        /// <param name="e">Evento</param>
        protected void btnExcluir_Click(object sender, EventArgs e)
        {
            ClientScript.RegisterStartupScript(GetType(), "Show", "<script> $('#popUpMensagemConfirmacao').modal('toggle');</script>");

        }
        /// <summary>
        ///Método que confirma a exlusão de ordem de serviço
        /// </summary>
        /// <param name="sender">Objeto do evento</param>
        /// <param name="e">Evento</param>
        protected void OK_Click(object sender, EventArgs e)
        {
            try
            {
                objOrdemServicoBO = new OrdemServicoBO();
                objOrdemServicoBO.Excluir(objOrdemServico);
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
                            btnNovo.Enabled = false;
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
            txtBairro.Text = objOrdemServico._Cliente._Bairro;
            if (objOrdemServico._DataFechamentoOS != null)
                txtDataFinalizacao.Text = objOrdemServico._DataFechamentoOS.ToString();
            if (objOrdemServico._TipoDesconto == 0)
            {
                rbTipoDesconto0.Checked = true;
                txtDescontoReal.Text = objOrdemServico._Desconto.ToString();
                divReal.Visible = true;
                divPorcento.Visible = false;
            }
            if (objOrdemServico._TipoDesconto == 1)
            {
                rbTipoDesconto1.Checked = true;
                txtDescontoPorcento.Text = objOrdemServico._Desconto.ToString();
                divReal.Visible = false;
                divPorcento.Visible = true;
            }
            if (objOrdemServico._TipoDesconto == null)
            {
                rbTipoDesconto0.Checked = true;
                divPorcento.Visible = false;
                divReal.Visible = true;
            }


            txtTotalProdutoColuna.Text = objOrdemServico._ValorTotalProduto.ToString();
            txtTotalServicoColuna.Text = objOrdemServico._ValorTotalServicos.ToString();


            txtTotalOS.Text = objOrdemServico._ValorTotal.ToString();
            txtTotalComDesconto.Text = objOrdemServico._TotalOSdesconto.ToString("C2");
            txtTotalOS.Text = objOrdemServico._ValorTotal.ToString();

            objItemProdutoBO = new ItemProdutoBO();
            gvProdutosInseridos.DataSource = objItemProdutoBO.listaIDataTableItemProduto(objOrdemServico);
            gvProdutosInseridos.DataBind();
            objItemServicoBO = new ItemServicoBO();
            gvServicoInseridos.DataSource = objItemServicoBO.buscaListaDataTableItemServico(objOrdemServico);
            gvServicoInseridos.DataBind();
            panelConteudo.Visible = true;
            panelConteudo.Enabled = false;
            panelBusca.Visible = false;
            visibleBotao(true);
            spanLimparInsercaoProduto.Visible = false;
            spanLimparQuantidade.Visible = false;
            spanLimparInfCliente.Visible = false;
            spanLimparInfPlaca.Visible = false;
            spanInserirProduto.Visible = false;
            btnGravar.Visible = false;
            if (objOrdemServico._DataFechamentoOS != null)
                btnEditar.Visible = false;           
            btnExcluir.Visible = false;
            spanLimparFinalizacao.Visible = false;
            divDataFinalizacao.Attributes.Add("class", "");

            if (objOrdemServico._DataFechamentoOS != null)
                btnExcluir.Visible = false;
            else
            {
                btnExcluir.Visible = true;
                ddlNomeMecanico.Enabled = true;
            }


            btnEditar.Visible = true;
            btnImprimir.Visible = true;


        }

        #endregion

        #endregion

        #region METODOS DE CONSULTA DE CLIENTE E MOTO
        #region Busca
        /// <summary>
        ///  Método que busca o cliente e preenche os campos inerentes do cliente na ordem de serviço.
        /// </summary>
        /// <param name="sender">Objeto do evento</param>
        /// <param name="e">Evento</param>
        protected void gvListaCliente_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblIdLi.Text = "a";
            carregarLiTab();
            objClienteBO = new ClienteBO();
            objCliente = new Cliente();
            objCliente._ClienteID = Convert.ToInt32(gvListaCliente.SelectedDataKey.Value);
            objCliente = objClienteBO.BuscarCliente(objCliente);
            txtTelefone.Text = objCliente._Telefone1;
            txtNomeCliente.Text = objCliente._Nome;
            txtEndereco.Text = objCliente._Endereco;
            lblClienteID.Text = objCliente._ClienteID.ToString();
            txtCnpjCpf.Text = objCliente._CpfCnpj;
            txtBairro.Text = objCliente._Bairro;
            spanLimparInfCliente.Visible = true;
            spanBuscarCliente.Visible = false;
            txtNomeCliente.ReadOnly = true;
            txtPlaca.Focus();
            if (txtPlaca.Text != "")
                txtKm.Focus();
        }
        /// <summary>
        ///  Método que busca uma lista de cliente pelo nome para ser selecionado.
        /// </summary>
        /// <param name="sender">Objeto do evento</param>
        /// <param name="e">Evento</param>
        protected void btnBuscarCliente_Click(object sender, EventArgs e)
        {
            objClienteBO = new ClienteBO();
            objCliente = new Cliente();
            objCliente._Nome = txtNomeCliente.Text;
            gvListaCliente.DataSource = objClienteBO.BuscarListaCliente(objCliente);
            gvListaCliente.DataBind();
            ClientScript.RegisterStartupScript(GetType(), "Show", "<script> $('#popUpListaCliente').modal('toggle');</script>");
        }

        /// <summary>
        ///  Método que busca uma lista de moto pela placa para ser selecionado.
        /// </summary>
        /// <param name="sender">Objeto do evento</param>
        /// <param name="e">Evento</param>
        protected void btnBuscarPlaca_Click(object sender, EventArgs e)
        {
            objMoto = new Moto();
            objMotoBO = new MotoBO();
            objMoto._Placa = txtPlaca.Text;
            gvListaMoto.DataSource = objMotoBO.BuscarListaMoto(objMoto);
            gvListaMoto.DataBind();
            ClientScript.RegisterStartupScript(GetType(), "Show", "<script> $('#popUpListaMoto').modal('toggle');</script>");

        }
        /// <summary>
        ///  Método que busca a moto selecionada e preenche os campos inerentes das informações da moto na ordem de serviço.
        /// </summary>
        /// <param name="sender">Objeto do evento</param>
        /// <param name="e">Evento</param>
        protected void gvListaMoto_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblIdLi.Text = "a";
            carregarLiTab();
            objMoto = new Moto();
            objMoto._MotoID = Convert.ToInt32(gvListaMoto.SelectedDataKey.Value);
            objMotoBO = new MotoBO();
            objMoto = objMotoBO.BuscarMoto(objMoto);
            lblMotoID.Text = objMoto._MotoID.ToString();
            txtPlaca.Text = objMoto._Placa;
            txtMarcaModelo.Text = objMoto._MarcaModelo;
            txtAno.Text = objMoto._AnoFabricacao;
            txtPlaca.ReadOnly = true;
            spanLimparInfPlaca.Visible = true;
            spanBuscarPlaca.Visible = false;
            txtKm.Focus();
        }


        #endregion

        #endregion

        //METODOS Serviço
        #region METODOS DE SERVIÇO
        #region Crud
        /// <summary>
        ///  Método que busca o serviço selecionado e inseri na ordem de serviço.
        /// </summary>
        /// <param name="sender">Objeto do evento</param>
        /// <param name="e">Evento</param>
        protected void gvInserirServico_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (String.IsNullOrEmpty(lblOrdemServicoID.Text) || lblOrdemServicoID.Text == "0")
                    Gravar();

                objServico = new Servico();
                objServico._ServicoID = Convert.ToInt32(gvListaServico.SelectedDataKey.Value);
                objItemServico = new ItemServico(objOrdemServico);
                objItemServicoBO = new ItemServicoBO();

                objItemServicoBO.Gravar(objItemServico, objServico);

                objOrdemServico._ListaItemServico = objItemServicoBO.buscarListaItemServico(objItemServico);

                objOrdemServicoBO = new OrdemServicoBO();
                objOrdemServico = objOrdemServicoBO.CalcularServico(objOrdemServico);
                txtTotalServicoColuna.Text = objOrdemServico._ValorTotalServicos.ToString();
                txtTotalOS.Text = objOrdemServico._ValorTotal.ToString();
                txtTotalComDesconto.Text = objOrdemServico._TotalOSdesconto.ToString();

                gvServicoInseridos.DataSource = objItemServicoBO.buscaListaDataTableItemServico(objOrdemServico);
                gvServicoInseridos.DataBind();
                txtNomeServico.Text = "";
                LimparDesconto();
                btnCancelar.Visible = false;

            }
            catch (Exception erro)
            {
                if (erro.Message == "Selecione um Cliente na Lista de Busca para esta Ordem de Serviço!!!")
                {
                    lblIdLi.Text = "a";
                    carregarLiTab();
                    txtNomeCliente.Text = "";
                }
                if (erro.Message == "Selecione uma Moto na Lista de Busca para esta Ordem de Serviço!!!")
                {
                    lblIdLi.Text = "a";
                    carregarLiTab();
                    txtPlaca.Text = "";
                }
                lblMensagem.Text = erro.Message;
                mensagemSucesso.Visible = false;
                mensagemErro.Visible = true;
                ClientScript.RegisterStartupScript(GetType(), "Show", "<script> $('#popUpMensagem').modal('toggle');</script>");

            }

        }
        /// <summary>
        /// Este método executa a exclusão do serviço quando a ordem de serviço ainda não foi finalizada.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvServicoInseridos_SelectedIndexChanged(object sender, EventArgs e)
        {
            objItemServico = new ItemServico(objOrdemServico);
            objItemServico._ItemServicoID = Convert.ToInt32(gvServicoInseridos.SelectedDataKey.Value);
            objItemServicoBO = new ItemServicoBO();
            objItemServicoBO.Excluir(objItemServico);
            objOrdemServico._ListaItemServico = objItemServicoBO.buscarListaItemServico(objItemServico);

            objOrdemServicoBO = new OrdemServicoBO();
            objOrdemServico = objOrdemServicoBO.CalcularServico(objOrdemServico);
            txtTotalServicoColuna.Text = objOrdemServico._ValorTotalServicos.ToString();
            txtTotalOS.Text = objOrdemServico._ValorTotal.ToString();
            txtTotalComDesconto.Text = objOrdemServico._TotalOSdesconto.ToString();

            gvServicoInseridos.DataSource = objItemServicoBO.buscaListaDataTableItemServico(objOrdemServico);
            gvServicoInseridos.DataBind();
            LimparDesconto();
        }
        #endregion

        #region Busca
        /// <summary>
        ///  Método que busca o serviço para ser selecionado e inserido na ordem de serviço.
        /// </summary>
        /// <param name="sender">Objeto do evento</param>
        /// <param name="e">Evento</param>
        protected void btnBuscarServico_Click(object sender, EventArgs e)
        {
            objServicoBO = new ServicoBO();
            objServico = new Servico();
            objServico._NomeServico = txtNomeServico.Text;
            gvListaServico.DataSource = objServicoBO.BuscarListaServico(objServico);
            gvListaServico.DataBind();
            ClientScript.RegisterStartupScript(GetType(), "Show", "<script> $('#popUpListaServico').modal('toggle');</script>");
        }

        #endregion
        #endregion

        //METODOS Produto
        #region METODOS DE PRODUTO
        #region Crud
        /// <summary>
        /// Método que verificar se uma ordem de serviço existe, se não grava essa ordem e depois insere o produto.
        /// </summary>
        /// <param name="sender">Objeto do evento</param>
        /// <param name="e">Evento</param>
        protected void btnInserirProduto_Click(object sender, EventArgs e)
        {
            try
            {
                if (String.IsNullOrEmpty(lblOrdemServicoID.Text) || lblOrdemServicoID.Text == "0")
                    Gravar();

                if (string.IsNullOrEmpty(lblProdutoID.Text))
                    throw new Exception("Campo Produto vazio ou Produto não cadastrado!!!");
                if (string.IsNullOrEmpty(txtQuantidade.Text) || txtQuantidade.Text == "0")
                    throw new Exception("Preencha a quantidade com um numero acima de ZERO!!!");

                objItemProduto = new ItemProduto(objOrdemServico);
                objItemProduto._QuantidadeItem = int.Parse(txtQuantidade.Text);


                objItemProdutoBO = new ItemProdutoBO();
                int qtde = 0;

                if (int.TryParse(lblItemProdutoID.Text, out numInt) && lblItemProdutoID.Text != "0")
                {
                    qtde = int.Parse(lblQtdeAterior.Text);
                    objItemProduto._ItemProdutoID = int.Parse(lblItemProdutoID.Text);
                    objItemProduto._OrdemServico._OrdemServicoID = int.Parse(lblOrdemServicoID.Text);
                    objItemProduto._Preco = decimal.Parse(txtPrecoProduto.Text);
                    objItemProdutoBO.Gravar(objItemProduto, objProduto, qtde);
                }
                else
                {
                    objItemProduto._OrdemServico._OrdemServicoID = objOrdemServico._OrdemServicoID;
                    objItemProdutoBO.Gravar(objItemProduto, objProduto, qtde);

                }

                objOrdemServicoBO = new OrdemServicoBO();

                objOrdemServico._ListaItemProduto = objItemProdutoBO.buscarListaItemProduto(objItemProduto);

                objOrdemServico = objOrdemServicoBO.CaucularProduto(objOrdemServico);

                gvProdutosInseridos.DataSource = objItemProdutoBO.listaIDataTableItemProduto(objOrdemServico);
                gvProdutosInseridos.DataBind();
                txtQuantidade.Attributes.Add("class", "form-control input-md text-center");
                txtTotalProdutoLinha.Text = objOrdemServico._ValorTotalProduto.ToString();
                txtTotalProdutoColuna.Text = objOrdemServico._ValorTotalProduto.ToString();
                txtTotalOS.Text = objOrdemServico._ValorTotal.ToString();
                txtTotalComDesconto.Text = objOrdemServico._TotalOSdesconto.ToString();
                LimparDesconto();
                btnLimparInsercaoProduto_Click(sender, e);
                btnCancelar.Visible = false;

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
        /// <summary>
        /// Método que busca e preenche os campos do produto para ser editado como também exclui conforme solicitação do usuário.
        /// </summary>
        /// <param name="sender">Objeto do evento</param>
        /// <param name="e">Evento</param>
        protected void gvProdutosInseridos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int posicaoLinha, itemID, prodID, qtde;
            string nomeProduto;
            posicaoLinha = int.Parse((string)e.CommandArgument);
            itemID = Convert.ToInt32(gvProdutosInseridos.DataKeys[posicaoLinha]["ItemProdutoID"]);
            prodID = Convert.ToInt32(gvProdutosInseridos.DataKeys[posicaoLinha]["ProdutoID"]);
            qtde = Convert.ToInt32(gvProdutosInseridos.DataKeys[posicaoLinha]["Qtde"]);
            nomeProduto = gvProdutosInseridos.DataKeys[posicaoLinha]["Nome do Produto"].ToString();

            if (e.CommandName == "Editar")
            {
                LimparInsercaoProdutos();

                foreach (var itemPro in objOrdemServico._ListaItemProduto)
                {
                    if (itemID == itemPro._ItemProdutoID)
                    {
                        txtNomeProduto.Text = nomeProduto;
                        txtNomeProduto.ReadOnly = true;
                        lblQtdeAterior.Text = qtde.ToString();
                        txtQuantidade.Text = qtde.ToString();
                        txtQuantidade.ReadOnly = true;
                        txtPrecoProduto.Text = itemPro._Preco.ToString();
                        txtTotalProdutoLinha.Text = itemPro._Total.ToString();
                        lblItemProdutoID.Text = itemPro._ItemProdutoID.ToString();
                        spanBuscarProduto.Visible = false;
                        spanLimparInsercaoProduto.Visible = true;
                        spanLimparQuantidade.Visible = true;
                        lblProdutoID.Text = itemPro._ObjProduto._ProdutoID.ToString();
                        objProduto = new Produto();
                        objProduto._ProdutoID = itemPro._ObjProduto._ProdutoID;
                    }
                }
            }
            if (e.CommandName == "Excluir")
            {
                objItemProduto = new ItemProduto(objOrdemServico);
                objItemProduto._ItemProdutoID = itemID;
                objItemProduto._ObjProduto._ProdutoID = prodID;
                objItemProduto._QuantidadeItem = qtde;
                objItemProdutoBO = new ItemProdutoBO();
                objItemProdutoBO.Excluir(objItemProduto);
                objOrdemServico._ListaItemProduto = objItemProdutoBO.buscarListaItemProduto(objItemProduto);

                objOrdemServicoBO = new OrdemServicoBO();
                objOrdemServico = objOrdemServicoBO.CaucularProduto(objOrdemServico);
                txtTotalProdutoColuna.Text = objOrdemServico._ValorTotalProduto.ToString();
                txtTotalServicoColuna.Text = objOrdemServico._ValorTotalServicos.ToString();
                txtTotalOS.Text = objOrdemServico._ValorTotal.ToString();
                txtTotalComDesconto.Text = objOrdemServico._TotalOSdesconto.ToString();

                // BUSCAR OS ITENS DE SERVIÇOS E MOSTRA NA TELA  

                gvProdutosInseridos.DataSource = objItemProdutoBO.listaIDataTableItemProduto(objOrdemServico);
                gvProdutosInseridos.DataBind();
                LimparDesconto();
                btnLimparInsercaoProduto_Click(sender, e);

            }
        }

        #endregion

        #region Busca
        /// <summary>
        /// Método que busca uma lista de produtos para ser selecionado.
        /// </summary>
        /// <param name="sender">Objeto do evento</param>
        /// <param name="e">Evento</param>
        protected void btnBuscarProduto_Click(object sender, EventArgs e)
        {
            try
            {
                objProduto = new Produto();
                objProduto._NomeProduto = txtNomeProduto.Text;
                objProdutoBO = new ProdutoBO();
                gvListaProduto.DataSource = objProdutoBO.BuscarListaProduto(objProduto);
                gvListaProduto.DataBind();
                ClientScript.RegisterStartupScript(GetType(), "Show", "<script> $('#popUpListaProduto').modal('toggle');</script>");
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
        /// Método que busca um produtos selecionado na lista e preenche os campos inerentes deste.
        /// </summary>
        /// <param name="sender">Objeto do evento</param>
        /// <param name="e">Evento</param>
        protected void gvListaProduto_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                LimparInsercaoProdutos();
                objProduto = new Produto();
                objProduto._ProdutoID = Convert.ToInt32(gvListaProduto.SelectedDataKey.Value);
                objProdutoBO = new ProdutoBO();
                objProduto = objProdutoBO.BuscarProduto(objProduto);
                txtNomeProduto.Text = objProduto._NomeProduto;
                txtPrecoProduto.Text = objProduto._PrecoVenda.ToString();
                txtNomeProduto.ReadOnly = true;
                lblProdutoID.Text = objProduto._ProdutoID.ToString();
                spanBuscarProduto.Visible = false;
                spanLimparInsercaoProduto.Visible = true;
                txtQuantidade.ReadOnly = false;

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
        #endregion

        //METODOS de apoio
        #region METODOS DE APOIO

        #region Outros
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
            btnImprimir.Visible = ocultar;
        }
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
        ///  Método que limpa os campos de inserção de produtos.
        /// </summary>
        /// <param name="sender">Objeto do evento</param>
        /// <param name="e">Evento</param>
        protected void btnLimparInsercaoProduto_Click(object sender, EventArgs e)
        {
            txtQuantidade.Attributes.Add("class", "form-control input-md text-center");
            LimparInsercaoProdutos();
            txtNomeProduto.ReadOnly = false;
            spanBuscarProduto.Visible = true;
            spanLimparInsercaoProduto.Visible = false;
            spanInserirProduto.Visible = false;
            spanLimparQuantidade.Visible = false;

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
        public void bloquearCamposNPermanentes(bool bloquear)
        {
            txtPlaca.ReadOnly = bloquear;
            txtObservacao.ReadOnly = bloquear;
            txtKm.ReadOnly = bloquear;
            ddlNomeMecanico.Enabled = !bloquear;
            txtNomeCliente.ReadOnly = bloquear;
            btnBuscarCliente.Enabled = !bloquear;
            btnBuscarPlaca.Enabled = !bloquear;
            txtDescontoPorcento.ReadOnly = bloquear;
            txtDescontoReal.ReadOnly = bloquear;
            txtNomeProduto.ReadOnly = bloquear;
            btnBuscarProduto.Enabled = !bloquear;
            gvProdutosInseridos.Enabled = !bloquear;
            gvServicoInseridos.Enabled = !bloquear;
            txtNomeServico.ReadOnly = bloquear;
            btnBuscarServico.Enabled = !bloquear;
            txtQuantidade.ReadOnly = bloquear;
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
            txtPrecoProduto.Enabled = bloquear;
            txtTotalProdutoLinha.ReadOnly = bloquear;
            txtTotalProdutoColuna.ReadOnly = bloquear;
            txtTotalServicoColuna.ReadOnly = bloquear;
            txtPrecoProduto.ReadOnly = bloquear;
            txtBairro.ReadOnly = bloquear;
            txtTotalOS.ReadOnly = bloquear;
            txtTotalComDesconto.ReadOnly = bloquear;
            txtCnpjCpf.ReadOnly = bloquear;
            txtBuscar.ReadOnly = bloquear;
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
            txtNomeServico.Text = "";
            txtNomeProduto.Text = "";
            txtTotalProdutoColuna.Text = "";
            txtTotalServicoColuna.Text = "";
            txtBairro.Text = "";
            txtDescontoPorcento.Text = "";
            txtDescontoReal.Text = "";
            txtPrecoProduto.Text = "";
            txtQuantidade.Text = "";
            txtTotalComDesconto.Text = "";
            txtTotalProdutoLinha.Text = "";
            txtTotalOS.Text = "";
            LimparInsercaoProdutos();


        }
        /// <summary>
        /// Método de apoio que limpa Insercao de Produtos quando solicitado
        /// </summary>
        public void LimparInsercaoProdutos()
        {
            txtNomeProduto.Text = "";
            txtQuantidade.Text = "";
            txtPrecoProduto.Text = "";
            txtTotalProdutoLinha.Text = "";
            lblQtdeAterior.Text = "";
            lblProdutoID.Text = "";
            lblItemProdutoID.Text = "";
        }
        /// <summary>
        /// Método de apoio que limpa o campo desconto quando solicitado
        /// </summary>
        public void LimparDesconto()
        {
            txtTotalComDesconto.Text = txtTotalOS.Text;
            txtDescontoReal.Text = "";
            txtDescontoPorcento.Text = "";

        }
        /// <summary>
        /// Método de apoio que checa o tipo de desconto e mostra o text box real.
        /// </summary>
        protected void rbTipoDesconto0_CheckedChanged(object sender, EventArgs e)
        {
            txtDescontoReal.Text = "";
            txtDescontoPorcento.Text = "";
            divReal.Visible = true;
            divPorcento.Visible = false;
            txtTotalComDesconto.Text = txtTotalOS.Text;

        }
        /// <summary>
        /// Método de apoio que checa o tipo de desconto e mostra o text box porcentagem.
        /// </summary>
        protected void rbTipoDesconto1_CheckedChanged(object sender, EventArgs e)
        {
            txtDescontoReal.Text = "";
            txtDescontoPorcento.Text = "";
            divReal.Visible = false;
            divPorcento.Visible = true;
            txtTotalComDesconto.Text = txtTotalOS.Text;


        }
        /// <summary>
        ///  Método que pinta a linha dos produtos que estão abaixo do estoque minimo definido pelo usuário.
        /// </summary>
        /// <param name="sender">Objeto do evento</param>
        /// <param name="e">Evento</param>
        protected void gvListaProduto_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.Cells[6].Text.Equals("Baixo"))
                {
                    e.Row.ForeColor = System.Drawing.Color.Orange;
                }
            }

        }
        /// <summary>
        /// Método que limpa as informações inerentes da moto.
        /// </summary>
        /// <param name="sender">Objeto do evento</param>
        /// <param name="e">Evento</param>
        protected void btnLimparInfPlaca_Click(object sender, EventArgs e)
        {
            txtPlaca.ReadOnly = false;
            lblMotoID.Text = "";
            spanBuscarPlaca.Visible = true;
            spanLimparInfPlaca.Visible = false;
            txtMarcaModelo.Text = "";
            txtAno.Text = "";
            txtPlaca.Text = "";
        }
        /// <summary>
        /// Método que limpa as informações inerentes do cliente.
        /// </summary>
        /// <param name="sender">Objeto do evento</param>
        /// <param name="e">Evento</param>
        protected void btnLimparInfCliente_Click(object sender, EventArgs e)
        {
            txtNomeCliente.ReadOnly = false;
            lblClienteID.Text = "";
            spanLimparInfCliente.Visible = false;
            spanBuscarCliente.Visible = true;
            txtNomeCliente.Text = "";
            txtTelefone.Text = "";
            txtCnpjCpf.Text = "";
            txtEndereco.Text = "";
            txtBairro.Text = "";
        }
        /// <summary>
        /// Método que limpa a quantidade.
        /// </summary>
        /// <param name="sender">Objeto do evento</param>
        /// <param name="e">Evento</param>
        protected void btnLimparQuantidade_Click(object sender, EventArgs e)
        {
            spanInserirProduto.Visible = false;
            txtQuantidade.Text = "";
            txtQuantidade.ReadOnly = false;
            spanLimparQuantidade.Visible = false;
            txtTotalProdutoLinha.Text = "";
            txtQuantidade.Attributes.Add("class", "form-control input-md text-center");
        }
        /// <summary>
        /// Método que calcula o produto.
        /// </summary>
        /// <param name="sender">Objeto do evento</param>
        /// <param name="e">Evento</param>
        protected void txtQuantidade_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(lblProdutoID.Text))
                {
                    txtQuantidade.Text = "";
                    throw new Exception("Campo Produto vazio ou Produto não cadastrado!!!");
                }
                if (string.IsNullOrEmpty(txtQuantidade.Text) || (!int.TryParse(txtQuantidade.Text, out numInt)))
                {
                    txtQuantidade.Text = "";
                    throw new Exception("Preencha a quantidade com um numero acima de ZERO!!!");
                }
                if (string.IsNullOrEmpty(lblQtdeAterior.Text))
                    lblQtdeAterior.Text = "0";
                decimal preco, total;
                int quantidade = int.Parse(txtQuantidade.Text);
                preco = decimal.Parse(txtPrecoProduto.Text);
                if (quantidade <= 0)
                    throw new Exception("Preencha a quantidade com um numero acima de ZERO!!!");

                if (objProduto._Estoque == 0)
                {
                    objProdutoBO = new ProdutoBO();
                    objProduto = objProdutoBO.BuscarProduto(objProduto);
                    objProduto._Estoque = objProduto._Estoque + Convert.ToInt32(lblQtdeAterior.Text);
                }
                if (objProduto._Estoque < quantidade)
                {
                    total = preco * objProduto._Estoque;

                    lblMensagem.Text = "Estoque memor que o inserido!!!";
                    txtQuantidade.Attributes.Add("class", "form-control input-md text-center alert-warning");
                    txtQuantidade.Text = objProduto._Estoque.ToString();

                    mensagemSucesso.Visible = false;
                    mensagemErro.Visible = true;
                    ClientScript.RegisterStartupScript(GetType(), "Show", "<script> $('#popUpMensagem').modal('toggle');</script>");
                }
                else
                {
                    total = quantidade * preco;
                }

                txtTotalProdutoLinha.Text = total.ToString();
                txtQuantidade.ReadOnly = true;
                spanInserirProduto.Visible = true;
                spanLimparQuantidade.Visible = true;


            }
            catch (Exception erro)
            {

                txtQuantidade.Text = "";
                lblMensagem.Text = erro.Message;
                mensagemSucesso.Visible = false;
                mensagemErro.Visible = true;
                ClientScript.RegisterStartupScript(GetType(), "Show", "<script> $('#popUpMensagem').modal('toggle');</script>");
                txtQuantidade.Focus();

            }

        }
        /// <summary>
        /// Método que calcula o desconto em espécie.
        /// </summary>
        /// <param name="sender">Objeto do evento</param>
        /// <param name="e">Evento</param>
        protected void txtDescontoReal_TextChanged(object sender, EventArgs e)
        {
            decimal desconto, totalOS;
            desconto = 0;
            totalOS = 0;
            try
            {
                if (!decimal.TryParse(txtTotalOS.Text, out numDecimal) || txtTotalOS.Text == "0")
                    throw new Exception("Não existe nenhum Produto ou Serviço Inserido!!!");
                if (!decimal.TryParse(txtDescontoReal.Text, out numDecimal))
                    desconto = 0;
                else
                    desconto = decimal.Parse(txtDescontoReal.Text);
                totalOS = decimal.Parse(txtTotalOS.Text);
                totalOS = totalOS - desconto;
                txtTotalComDesconto.Text = totalOS.ToString();
                divPorcento.Visible = false;
            }
            catch (Exception erro)
            {
                divPorcento.Visible = false;
                txtDescontoReal.Text = "";
                txtTotalComDesconto.Text = txtTotalOS.Text;
                lblMensagem.Text = erro.Message;
                mensagemSucesso.Visible = false;
                mensagemErro.Visible = true;
                ClientScript.RegisterStartupScript(GetType(), "Show", "<script> $('#popUpMensagem').modal('toggle');</script>");
            }

        }
        /// <summary>
        /// Método que calcula o desconto em porcentagem.
        /// </summary>
        /// <param name="sender">Objeto do evento</param>
        /// <param name="e">Evento</param>
        protected void txtDescontoPorcento_TextChanged(object sender, EventArgs e)
        {
            decimal desconto, totalOS;
            desconto = 0;
            totalOS = 0;
            try
            {
                if (!decimal.TryParse(txtTotalOS.Text, out numDecimal) || txtTotalOS.Text == "0")
                    throw new Exception("Não existe nenhum Produto ou Serviço Inserido!!!");
                if (!decimal.TryParse(txtDescontoPorcento.Text, out numDecimal))
                    desconto = 0;
                else
                    desconto = decimal.Parse(txtDescontoPorcento.Text);

                totalOS = decimal.Parse(txtTotalOS.Text);
                totalOS = ((100 - desconto) * totalOS) / 100;
                txtTotalComDesconto.Text = totalOS.ToString();
            }
            catch (Exception erro)
            {

                txtTotalComDesconto.Text = txtTotalOS.Text;
                txtDescontoPorcento.Text = "";
                lblMensagem.Text = erro.Message;
                mensagemSucesso.Visible = false;
                mensagemErro.Visible = true;
                ClientScript.RegisterStartupScript(GetType(), "Show", "<script> $('#popUpMensagem').modal('toggle');</script>");
            }
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
        /// <summary>
        /// Método que limpa a data de finalizaçãoda OS.
        /// </summary>
        /// <param name="sender">Objeto do evento</param>
        /// <param name="e">Evento</param>
        protected void btnLimparDataFinalizacao_Click(object sender, EventArgs e)
        {
            txtDataFinalizacao.Text = "";
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
                Literal2.Text = "<li><a href=\"#produto\" data-toggle=\"tab\" id=\"b\"><font><font>Produto</font></font></a></li>";
                Literal3.Text = "<li><a href=\"#servico\" data-toggle=\"tab\" id=\"c\"><font><font>Serviço</font></font></a></li>";
                Literal1b.Text = " <div class=\"tab-pane fade\" id=\"Informacoes\">";
                Literal1c.Text = "</div>";
                Literal2b.Text = " <div class=\"tab-pane fade\" id=\"produto\">";
                Literal2c.Text = "</div>";
                Literal3b.Text = "<div class=\"tab-pane fade\" id=\"servico\">";
                Literal3c.Text = "</div>";
                switch (lblIdLi.Text)
                {
                    case "a":
                        Literal1.Text = "<li class=\"active\"><a href=\"#Informacoes\" data-toggle=\"tab\" id=\"a\" ><font><font class=\"goog-text-highlight\">Informações da OS</font></font></a></li>";
                        Literal1b.Text = " <div class=\"tab-pane fade in active\" id=\"Informacoes\">";
                        Literal1c.Text = "</div>";
                        break;
                    case "b":
                        Literal2.Text = "<li class=\"active\"><a href=\"#produto\" data-toggle=\"tab\" id=\"b\"><font><font>Produto</font></font></a></li>";
                        Literal2b.Text = " <div class=\"tab-pane fade in active\" id=\"produto\">";
                        Literal2c.Text = "</div>";

                        break;
                    case "c":
                        Literal3.Text = "<li class=\"active\"><a href=\"#servico\" data-toggle=\"tab\" id=\"c\"><font><font>Serviço</font></font></a></li>";
                        Literal3b.Text = "<div class=\"tab-pane fade in active\" id=\"servico\">";
                        Literal3c.Text = "</div>";
                        break;
                }
            }
            else
            {
                Literal1.Text = "<li class=\"active\"><a href=\"#Informacoes\" data-toggle=\"tab\" id=\"a\" ><font><font class=\"goog-text-highlight\">Informações da OS</font></font></a></li>";
                Literal1b.Text = " <div class=\"tab-pane fade in active\" id=\"Informacoes\">";
                Literal1c.Text = "</div>";
                Literal2.Text = "<li><a href=\"#produto\" data-toggle=\"tab\" id=\"b\"><font><font>Produto</font></font></a></li>";
                Literal2b.Text = " <div class=\"tab-pane fade\" id=\"produto\">";
                Literal2c.Text = "</div>";
                Literal3.Text = "<li><a href=\"#servico\" data-toggle=\"tab\" id=\"c\"><font><font>Serviço</font></font></a></li>";
                Literal3b.Text = "<div class=\"tab-pane fade\" id=\"servico\">";
                Literal3c.Text = "</div>";
            }
        }
        #endregion

        protected void btnImprimir_Click(object sender, EventArgs e)
        {
            //Response.Redirect("Relatorios/WebOrdemServicoImpressao.aspx?cod=" + objOrdemServico._OrdemServicoID);
            int id = objOrdemServico._OrdemServicoID;
            Session["imprimirRelatorio"] = id;
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "_new", "window.open('Relatorios/WebOrdemServicoImpressao.aspx');", true);
        }

        #endregion

        protected void ddlTipoBusca_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlTipoBusca.Text != "3")
            {
                txtBuscar.ReadOnly = true;
                btnNovo.Enabled = true;
            }
            else
            {
                btnNovo.Enabled = false;
                txtBuscar.ReadOnly = false;
            }
            txtBuscar.Text = "";
        }






        //todo TAREFAS criar um metodo para validar se o ano de fabricação da moto e maior que o ano virgente
    }
}