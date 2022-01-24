<%@ Page Title="" Language="C#" MasterPageFile="~/PaginaPrincipal.Master" AutoEventWireup="true" CodeBehind="WebCriarLogin.aspx.cs" Inherits="Apresentacao.WebCriarLogin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Panel ID="panelLogin" runat="server">
        <div class="row">
            <div class="col-md-12">
                <asp:Panel ID="panelBusca" runat="server">
                    <%--CAMPO DE BUSCA  --%>
                    <div class="form-group">
                        <div class="input-group col-md-5 pull-right">
                            <asp:TextBox ID="txtBuscar" runat="server" class="form-control input-md uppercase"></asp:TextBox>

                            <span class="input-group-addon">
                                <asp:LinkButton ID="btnBuscar" runat="server" OnClick="btnBuscar_Click">
                            <i class="glyphicon glyphicon-search"></i>
                                </asp:LinkButton>
                            </span>
                        </div>
                    </div>
                    <div class="modal fade" id="popUpLista" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true" style="">
                        <div class="modal-dialog">
                            <div class="modal-content" style="max-width: 800px;">
                                <a class="list-group-item active"></a>
                                <div class="modal-header" style="padding: 0px 15px;">
                                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true"><font><font>×</font></font></button>
                                    <h4 class="modal-title" id="myModalLabel"><font><font>
								<p>LISTA DE FUNCIONÁRIO</p>
							</font></font>
                                        <h4></h4>
                                    </h4>
                                </div>
                                <div class="modal-body">
                                    <div class="row scroll-grid">

                                        <asp:GridView ID="gvListaFuncionario" runat="server" AutoGenerateColumns="False"
                                            DataKeyNames="FuncionarioID" CssClass="table table-hover table-striped "
                                            OnSelectedIndexChanged="gvListaFuncionario_SelectedIndexChanged" CellPadding="4" ForeColor="#333333">
                                            <AlternatingRowStyle BackColor="White" />
                                            <Columns>
                                                <asp:TemplateField HeaderStyle-Width="1%" ShowHeader="False">
                                                    <ItemTemplate>
                                                        <span class="input-group-addon" style="border: 1px solid #ccc">
                                                            <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Select" Text="Selecionar"> <i class="glyphicon glyphicon-ok" ></i></asp:LinkButton>
                                                        </span>
                                                    </ItemTemplate>
                                                    <HeaderStyle Width="1%" />
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="FuncionarioID" HeaderText="FuncionarioID" Visible="false" />
                                                <asp:BoundField DataField="Nome" HeaderText="Nome" />
                                                <asp:BoundField DataField="Cargo" HeaderText="Cargo" />
                                                <asp:BoundField DataField="CPF" HeaderText="CPF" />
                                                <asp:BoundField DataField="Telefone" HeaderText="Telefone" />
                                                <asp:BoundField DataField="Situação" HeaderText="Situação" Visible="false" />
                                            </Columns>
                                            <EditRowStyle BackColor="#2461BF" />
                                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="#2A6496" Font-Size="Large" />
                                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                            <RowStyle CssClass="cursor-pointer" BackColor="#EFF3FB" />
                                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                            <SortedAscendingCellStyle BackColor="#F5F7FB" />
                                            <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                                            <SortedDescendingCellStyle BackColor="#E9EBEF" />
                                            <SortedDescendingHeaderStyle BackColor="#4870BE" />
                                        </asp:GridView>

                                    </div>
                                </div>
                                <div class="modal-footer">
                                    <button type="button" class="btn btn-default" data-dismiss="modal"><font><font>FECHAR</font></font></button>
                                </div>
                            </div>
                        </div>
                    </div>

                </asp:Panel>
            </div>
        </div>

        <fieldset>
            <legend>INFORMAÇÃO DE LOGIN</legend>
            <div class="col-md-4">
                <!-- Text input-->
                <div class="form-group">
                    <label class="control-label" for="txtLogin">Nome:</label>
                    <div>
                        <asp:Label ID="lblNome" runat="server"></asp:Label>
                    </div>
                </div>
                <!-- Text input-->
                <div class="form-group">
                    <label class="control-label" for="txtLogin">CPF:</label>
                    <div>
                        <asp:Label ID="lblCPF" runat="server"></asp:Label>
                    </div>
                </div>

            </div>
            <div class="col-md-6">
            </div>

            <div class="row">
                <%-- <div class="col-md-3">
                </div>--%>
                <div class="col-md-6">
                    <asp:Panel ID="pnlUsuario" runat="server">
                        <div class="row">
                            <div class="form-group col-md-6">
                                <label class="control-label" for="txtLogin">Usuário:</label>
                                <div>
                                    <asp:Label ID="lblUsuario" class="control-label text-primary" runat="server" Font-Bold="True" Font-Size="Large"></asp:Label>
                                </div>
                            </div>

                            <div class="form-group col-md-6">
                                <label class="control-label" for="txtSenhaAtual"><em>*</em> Digite sua Senha Atual:</label>
                                <div>
                                    <asp:TextBox ID="txtSenhaAtual" runat="server" type="password" class="form-control input-md" MaxLength="25"></asp:TextBox>
                                </div>
                            </div>

                        </div>
                    </asp:Panel>




                    <asp:Panel ID="pnlGerente" runat="server">

                        <div class="row">
                            <div class="form-group col-md-6">
                                <label class="control-label" for="txtSenhaGerente"><em>*</em> Digite sua Senha:</label>
                                <div>
                                    <asp:TextBox ID="txtSenhaGerente" runat="server" type="password" class="form-control input-md" MaxLength="25"></asp:TextBox>
                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <!-- Text input-->
                            <div class="form-group col-md-6">
                                <label class="control-label" for="txtLogin"><em>*</em> Usuário:</label>
                                <div>
                                    <asp:TextBox ID="txtLogin" runat="server" class="form-control input-md" MaxLength="25"></asp:TextBox>
                                </div>
                            </div>


                            <!-- Text input-->
                            <div class="form-group col-md-6">
                                <label class="control-label" for="txtLogin2"><em>*</em> Usuário:</label>
                                <div>
                                    <asp:TextBox ID="txtLogin2" runat="server"  class="form-control input-md" MaxLength="25"></asp:TextBox>
                                </div>
                            </div>
                        </div>


                    </asp:Panel>

                    <div class="row">
                        <div class="form-group col-md-6">
                            <label class="control-label" for="txtSenha"><em>*</em> Senha:</label>
                            <div>
                                <asp:TextBox ID="txtSenha" runat="server"  type="password" class="form-control input-md" MaxLength="25"></asp:TextBox>
                            </div>
                        </div>


                        <!-- Text input-->
                        <div class="form-group col-md-6">
                            <label class="control-label" for="txtSenha2"><em>*</em> Senha:</label>
                            <div>
                                <asp:TextBox ID="txtSenha2" runat="server"  type="password" class="form-control input-md" MaxLength="25"></asp:TextBox>
                            </div>
                        </div>
                    </div>

                    <div class="row navbar-right">
                        <div class="form-group ">
                            <div class="col-md-12">
                                <asp:Button ID="btnGravar" runat="server" Text="Gravar" class="btn btn-primary" OnClick="btnGravar_Click" />
                                <asp:Button ID="btnGravarSenha" runat="server" Text="Gravar" class="btn btn-primary" OnClick="btnGravarSenha_Click" />
                                <asp:Button ID="btnEditar" runat="server" Text="Editar" class="btn btn-primary" OnClick="btnEditar_Click" />
                                <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" class="btn btn-warning" OnClick="btnCancelar_Click" />
                                <asp:Button ID="btnCancelarSenha" runat="server" Text="Cancelar" class="btn btn-warning" OnClick="btnCancelarSenha_Click" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>

        </fieldset>

    </asp:Panel>

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
                            <asp:Label ID="lblMensagem" runat="server"></asp:Label>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal"><font><font>FECHAR</font></font></button>

                </div>
            </div>
        </div>
    </div>
</asp:Content>
