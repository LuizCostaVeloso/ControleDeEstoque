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
    public partial class WebLogin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        /// <summary>
        ///  Método que verifica e autentica o usuário.
        /// </summary>
        /// <param name="sender">Objeto do evento</param>
        /// <param name="e">Evento</param>
        protected void btnLogar_Click(object sender, EventArgs e)
        {
            try
            {
                Funcionario objFuncionario = new Funcionario();
                objFuncionario._Login = txtLogin.Text;
                objFuncionario._Senha = txtSenha.Text;
                FuncionarioBO objFuncionarioBO = new FuncionarioBO();                 
                objFuncionario = objFuncionarioBO.BuscarLoginSenha(objFuncionario);
                Session["Logado"] = objFuncionario;
                Response.Redirect("WebIndex.aspx");
                lblMensagem.Visible = false;
            }
            catch (Exception)
            {                
                txtSenha.Text = "";
                lblMensagem.Text = "Seu Usuário ou Senha é inválido!!!";
                lblMensagem.Visible = true;                
            }
        }   
    }
}