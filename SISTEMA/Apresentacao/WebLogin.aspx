<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebLogin.aspx.cs" Inherits="Apresentacao.WebLogin" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
     <title>Mix Motos</title>
    <link href="css/bootstrap.css" rel="stylesheet" />
    <link href="css/bootstrap-theme.css" rel="stylesheet" />      
    <link href="css/Login.css" rel="stylesheet" />   
      
</head>
<body style=" background-color: #f7fafd;" >
     <script src="js/jquery-2.1.0.js"></script> 
    <script src="js/bootstrap.js"></script>    
  <div id="fullscreen_bg" class="fullscreen_bg" />

    <div class="container" >     
        <form id="Form1" class="form-signin" runat="server" style="margin-top:15%; background-image: url('/img/fundoLogin.png');" >

            <h1 class="form-signin-heading text-muted">SICSEP</h1>
            <div class="form-group">
                <div class="input-group">
                    <span class="input-group-addon"><span class="glyphicon glyphicon-user"></span></span>
                    <asp:TextBox ID="txtLogin" runat="server" type="text" class="form-control" placeholder="Digite seu Usuário..." required="" autofocus=""></asp:TextBox>
                    
                </div>
                <div class="input-group">
                    <span class="input-group-addon"><span class="glyphicon glyphicon-lock"></span></span>
                     <asp:TextBox ID="txtSenha" runat="server" type="password" class="form-control" placeholder="Digite sua Senha..." required=""></asp:TextBox>
                    
                </div>
                 <asp:Label ID="lblMensagem" runat="server" style="color:red;"></asp:Label>
            </div>          
            <asp:Button ID="btnLogar" runat="server" Text="Entrar" class="btn btn-lg btn-primary btn-block" OnClick="btnLogar_Click" />
          
        </form>

    </div>

        <%--MODAL MENSAGEM DE ERRRO--%>
    <div class="modal fade" id="popUpMensagem" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true" style="">
        <div class="modal-dialog">
            <div class="modal-content " style="width: 280px; margin: 0 auto;">
                <div id="mensagemSucesso" runat="server" class="modal-header alert alert-success" style="padding-bottom: 2px; text-align: center;">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true"><font><font>×</font></font></button>
                    <h4 class="modal-title" id="H5"><font><font>
								<b>MENSAGEM!!!</b>
							</font></font></h4>
                </div>
                <div id="mensagemErro" runat="server" class="modal-header alert alert-danger" style="padding-bottom: 2px; text-align: center;">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true"><font><font>×</font></font></button>
                    <h4 class="modal-title" id="H6"><font><font>
								<b>MENSAGEM!!!</b>
							</font></font></h4>
                </div>

                <div class="modal-body" style="padding-bottom: 20px">
                    <div class="row">
                        <div class="col-md-12">
                            <%--<asp:Label ID="lblMensagem" runat="server"></asp:Label>--%>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal"><font><font>FECHAR</font></font></button>

                </div>                
            </div>
        </div>
    </div>
   
 
</body>
</html>