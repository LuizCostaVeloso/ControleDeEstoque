using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Negocio.Model;

namespace Apresentacao
{
    public partial class PaginaPrincipal : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Cargo gerente = Cargo.GERENTE;
            Funcionario objFuncionario = new Funcionario();
            objFuncionario = ((Funcionario)Session["Logado"]);


            if ((Funcionario)Session["Logado"] != null)
            {
                if (objFuncionario._Cargo == gerente)
                {
                    if (!IsPostBack)
                    {
                        Gerente();
                        gerentes.Visible = true;
                        atendCaixa.Visible = false;
                        literalUsuario.Text = objFuncionario._Login;

                    }

                }
                else
                {
                    AtendCaixa();
                    literalUsuario1.Text = objFuncionario._Login;
                    gerentes.Visible = false;
                    atendCaixa.Visible = true;
                }

            }
            else
            {
                Session.Remove("Logado");
                Response.Redirect("WebLogin.aspx");
            }             
           
        }

         #region Metodo para marcar o menu na pagina logada

        public void Gerente()
        {
            if (((string)Session["folha"]) != null)
            {
                string acao = ((string)Session["folha"]);
                switch (acao)
                {
                    case "cadastro":
                        litInicio.Text = ("<li><a href=\"WebIndex.aspx\">INÍCIO</a></li>");
                        litCadastro.Text = ("<li class=\"active\">");
                        literalOS.Text = ("<li>");
                        break;
                    case "inicio":
                        litInicio.Text = ("<li class=\"active\"><a href=\"WebIndex.aspx\">INÍCIO</a></li>");
                        litCadastro.Text = ("<li>");
                        literalOS.Text = ("<li>");

                        break;
                    case "ordemServico":
                        litInicio.Text = ("<li><a href=\"WebIndex.aspx\">INÍCIO</a></li>");
                        litCadastro.Text = ("<li>");
                        literalOS.Text = ("<li class=\"active\">");
                        break;
                }
            }
            else
            {
                litInicio.Text = ("<li><a href=\"WebInex.aspx\">Home</a></li>");
                litCadastro.Text = ("<li>");
                literalOS.Text = ("<li>");
            }
        }

        public void AtendCaixa()
        {
            if (((string)Session["folha"]) != null)
            {
                string acao = ((string)Session["folha"]);
                switch (acao)
                {
                    case "cadastro":
                        litInicio1.Text = ("<li><a href=\"WebIndex.aspx\">INÍCIO</a></li>");
                        litCadastro1.Text = ("<li class=\"active\">");
                        literalOS1.Text = (" <li><a href=\"WebOrdemServicoA.aspx\">Ordem de Serviço</a></li>");
                        break;
                    case "inicio":
                        litInicio1.Text = ("<li class=\"active\"><a href=\"WebIndex.aspx\">INÍCIO</a></li>");
                        litCadastro1.Text = ("<li>");
                        literalOS1.Text = (" <li><a href=\"WebOrdemServicoA.aspx\">Ordem de Serviço</a></li>");

                        break;
                    case "ordemServico":
                        litInicio1.Text = ("<li><a href=\"WebIndex.aspx\">INÍCIO</a></li>");
                        litCadastro1.Text = ("<li>");
                        literalOS1.Text = (" <li class=\"active\"><a href=\"WebOrdemServicoA.aspx\">Ordem de Serviço</a></li>");
                        
                        break;
                }
            }
            else
            {
                litInicio1.Text = ("<li><a href=\"WebInex.aspx\">Home</a></li>");
                litCadastro1.Text = ("<li>");
                literalOS1.Text = ("<li>");
            }
        }
            #endregion            

        protected void sair_ServerClick(object sender, EventArgs e)
        {
            Session.Remove("Logado");
            Response.Redirect("WebLogin.aspx");
        }
    }  

}