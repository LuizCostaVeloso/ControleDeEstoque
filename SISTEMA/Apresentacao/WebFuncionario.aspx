<%@ Page Title="" Language="C#" MasterPageFile="~/PaginaPrincipal.Master" AutoEventWireup="true" CodeBehind="WebFuncionario.aspx.cs" Inherits="Apresentacao.WebFuncionario" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
        <div class="col-md-12">
            <asp:Panel ID="panelBusca" runat="server">
                <%--CAMPO DE BUSCA  --%>
                <div class="form-group">
                    <div class="input-group col-md-7 pull-right">
                        <span class="input-group-addon" style="padding: 1px 4px; font-size: 16px;">
                            <asp:DropDownList ID="ddlTipoBusca" runat="server" class="form-control" Width="108px" Style="font-size: 16px; height: 25px; padding: 2px 4px;">
                                <asp:ListItem Value="0">Ativos</asp:ListItem>
                                <asp:ListItem Value="1">Inativos</asp:ListItem>
                                <asp:ListItem Value="2">Todos</asp:ListItem>
                            </asp:DropDownList>
                        </span>
                        <asp:TextBox ID="txtBuscar" runat="server"  class="form-control input-md uppercase"></asp:TextBox>

                        <span class="input-group-addon">
                            <asp:LinkButton ID="btnBuscar" runat="server" OnClick="btnBuscar_Click">
                            <i class="glyphicon glyphicon-search"></i>
                            </asp:LinkButton>
                        </span>
                        <span class="input-group-addon">
                            <asp:LinkButton ID="btnNovo" runat="server" OnClick="btnNovo_Click">
                            <i class="glyphicon glyphicon-plus"></i>
                            </asp:LinkButton>
                        </span>
                    </div>
                </div>
            </asp:Panel>
        </div>

    </div>
    <asp:Label ID="lblFuncionarioID" runat="server"></asp:Label>
    <%--ESTRUTURA--%>
    <fieldset>
        <legend>INFORMAÇÕES DO FUNCIONÁRIO</legend>
        <asp:Panel ID="panelConteudo" runat="server">
            <%-- 1 linha--%>
            <div class="row">
                <div class="col-md-2">
                    <!-- Text input-->
                    <div class="form-group">
                        <label class="control-label" for="txtDtNascimento"><em>*</em> Nascimento:</label>
                        <div>
                            <asp:TextBox ID="txtDtNascimento" runat="server" type="text"  class="form-control input-md text-center date"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="col-md-6">
                    <!-- Text input-->
                    <div class="form-group">
                        <label class="control-label" for="txtNome"><em>*</em> Nome:</label>
                        <div class="input-group">
                            <span class="input-group-addon"><span class="glyphicon glyphicon-user"></span></span>
                            <asp:TextBox ID="txtNome" runat="server" type="text"  class="form-control input-md uppercase" MaxLength="52"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="col-md-2">
                    <!-- Text input-->
                    <div class="form-group">
                        <label class=" control-label" for="txtDataAtivado">Ativo:</label>
                        <div>
                            <asp:TextBox ID="txtDataAtivado" runat="server"  class="form-control input-md text-center"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="col-md-2">
                    <!-- Text input-->
                    <div class="form-group">
                        <label class="control-label" for="txtDataDesativado">Inativo:</label>
                        <div>
                            <asp:TextBox ID="txtDataDesativado" runat="server" type="text"  class="form-control input-md text-center"></asp:TextBox>
                        </div>
                    </div>
                </div>
            </div>

            <%-- 2 linha--%>
            <div class="row">
                <div class="col-md-2">
                    <!-- Select Basic -->
                    <div class="form-group">
                        <label class="control-label" for="ddlCargo"><em>*</em> Cargo:</label>
                        <div id="ddlUfdiv" runat="server">
                            <asp:DropDownList ID="ddlCargo" runat="server" class="form-control"></asp:DropDownList>
                        </div>
                    </div>
                </div>
                <div class="col-md-3">
                    <!-- Text input-->
                    <div class="form-group">
                        <label class="control-label" for="txtHabilitacao">Habilitação:</label>
                        <div>
                            <asp:TextBox ID="txtHabilitacao" runat="server" type="text"  class="form-control input-md" MaxLength="12"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="col-md-2">
                    <!-- Text input-->
                    <div class="form-group">
                        <label class="control-label" for="txtCategoria">Categoria:</label>
                        <div>
                            <asp:TextBox ID="txtCategoria" runat="server" type="text"  class="form-control input-md uppercase" MaxLength="2"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="col-md-3">
                    <!-- Text input-->
                    <div class="form-group">
                        <label class="control-label" for="txtCpf"><em>*</em> CPF:</label>
                        <div>
                            <asp:TextBox ID="txtCpf" runat="server" type="text"  class="form-control input-md cpf" MaxLength="14"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="col-md-2">
                    <!-- Text input-->
                    <div class="form-group">
                        <label class="control-label" for="txtRg">RG:</label>
                        <div>
                            <asp:TextBox ID="txtRg" runat="server"  class="form-control input-md rg" MaxLength="11"></asp:TextBox>
                        </div>
                    </div>
                </div>

            </div>

            <%--3 linha--%>
            <div class="row">
                <div class="col-md-2" style="width: 12%;">
                    <!-- Text input-->
                    <div class="form-group">
                        <label class="control-label" for="txtOrgaoEmissor">Org. Exp.:</label>
                        <div>
                            <asp:TextBox ID="txtOrgaoEmissor" runat="server"  class="form-control input-md uppercase" MaxLength="6"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="col-md-2" style="width: 21.2%;">
                    <!-- Text input-->
                    <div class="form-group">
                        <label class="control-label" for="txtCep">CEP:</label>
                        <div>
                            <asp:TextBox ID="txtCep" runat="server"  class="form-control input-md cep" MaxLength="9"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="col-md-5">
                    <!-- Text input-->
                    <div class="form-group">
                        <label class="control-label" for="txtEndereco"><em>*</em> Endereço:</label>
                        <div>
                            <asp:TextBox ID="txtEndereco" runat="server" class="form-control input-md uppercase" MaxLength="46"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="col-md-3">
                    <!-- Text input-->
                    <div class="form-group">
                        <label class="control-label" for="txtBairro"><em>*</em> Bairro:</label>
                        <div>
                            <asp:TextBox ID="txtBairro" runat="server" class="form-control input-md uppercase" MaxLength="25"></asp:TextBox>
                        </div>
                    </div>
                </div>

            </div>

            <%-- 4 linha--%>
            <div class="row">
                <div class="col-md-6">
                    <!-- Text input-->
                    <div class="form-group">
                        <label class="control-label" for="txtComplemento">Complemento:</label>
                        <div>
                            <asp:TextBox ID="txtComplemento" runat="server"  class="form-control input-md uppercase" MaxLength="62"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="col-md-3">
                    <!-- Text input-->
                    <div class="form-group">
                        <label class="control-label" for="txtTel1"><em>*</em> Telefone:</label>
                        <div class="input-group">
                            <span class="input-group-addon"><span class="glyphicon glyphicon-earphone"></span></span>
                            <asp:TextBox ID="txtTel1" runat="server" class="form-control input-md phone" MaxLength="14"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="col-md-3">
                    <!-- Text input-->
                    <div class="form-group">
                        <label class="control-label" for="txtTel2">Telefone:</label>
                        <div class="input-group">
                            <span class="input-group-addon"><span class="glyphicon glyphicon-earphone"></span></span>
                            <asp:TextBox ID="txtTel2" runat="server" class="form-control input-md phone" MaxLength="14"></asp:TextBox>
                        </div>
                    </div>
                </div>
            </div>

        </asp:Panel>
        <%-- 5 linha--%>

        <asp:Panel ID="panelObservacao" runat="server">
            <%--linha 6--%>
            <div class="row">
                <div class="col-md-12">
                    <!-- Textarea -->
                    <div class="form-group">
                        <label class="control-label" for="txtObservacao">Observações:</label>
                        <div>
                            <asp:TextBox ID="txtObservacao" runat="server" TextMode="MultiLine" class="form-control uppercase" MaxLength="300"></asp:TextBox>
                        </div>
                    </div>
                </div>
            </div>
        </asp:Panel>

        <div class="row navbar-right">
            <div class="form-group ">
                <div class="col-md-12">
                    <asp:Button ID="btnGravar" runat="server" Text="Gravar" class="btn btn-primary" OnClick="btnGravar_Click" />
                    <asp:Button ID="btnEditar" runat="server" Text="Editar" class="btn btn-primary" OnClick="btnEditar_Click" />
                    <asp:Button ID="btnDesativar" runat="server" Text="Desativar" class="btn btn-danger" OnClick="btnDesativar_Click" />
                    <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" class="btn btn-warning" OnClick="btnCancelar_Click" />
                </div>
            </div>
        </div>

    </fieldset>

    <%-- modal da lista--%>

    <div class="modal fade" id="popUpLista" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true" style="">
        <div class="modal-dialog">
            <div class="modal-content" style="max-width: 800px;">
                <a class="list-group-item active"></a>
                <div class="modal-header" style="padding: 0px 15px;">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true"><font><font>×</font></font></button>
                    <h4 class="modal-title" id="myModalLabel"><font><font>
								<p>LISTA DE FUNCIONÁRIO</p>
							</font></font></h4>
                </div>
                <div class="modal-body">

                    <div class="row scroll-grid">
                        <asp:GridView ID="gvListaFuncionario" runat="server" AutoGenerateColumns="False" OnRowDataBound="gvListaFuncionario_RowDataBound"
                            DataKeyNames="FuncionarioID" CssClass="table table-hover table-striped "
                            OnSelectedIndexChanged="gvListaFuncionario_SelectedIndexChanged" CellPadding="4" ForeColor="#333333">
                            <AlternatingRowStyle BackColor="White" />
                            <Columns>
                                <asp:TemplateField ShowHeader="False" HeaderStyle-Width="1%">
                                    <ItemTemplate>
                                        <span class="input-group-addon" style="border: 1px solid #ccc">
                                            <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Select"
                                                Text="Selecionar"> <i class="glyphicon glyphicon-ok" ></i></asp:LinkButton>
                                        </span>
                                    </ItemTemplate>

                                    <HeaderStyle Width="1%"></HeaderStyle>
                                </asp:TemplateField>
                                <asp:BoundField DataField="FuncionarioID" HeaderText="FuncionarioID" Visible="false" />
                                <asp:BoundField DataField="Nome" HeaderText="Nome" />
                                <asp:BoundField DataField="Cargo" HeaderText="Cargo" />
                                <asp:BoundField DataField="CPF" HeaderText="CPF" />
                                <asp:BoundField DataField="Telefone" HeaderText="Telefone" />
                                <asp:BoundField DataField="Situação" HeaderText="Situação" />
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


    <%--MODAL MENSAGEM DE ERRRO--%>

        <div class="modal fade" id="popUpMensagem1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true" style="">
            <div class="modal-dialog">
                <div class="modal-content " style="width: 280px; margin: 0 auto;">
                    <div id="mensagemSucesso1" runat="server" class="modal-header alert alert-success" style="padding-bottom: 2px; text-align: center;">
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true"><font><font>×</font></font></button>
                        <h4 class="modal-title" id="H1"><font><font>
								<b>MENSAGEM!!!</b>
							</font></font></h4>
                    </div>
                    <div id="mensagemErro1" runat="server" class="modal-header alert alert-danger" style="padding-bottom: 2px; text-align: center;">
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true"><font><font>×</font></font></button>
                        <h4 class="modal-title" id="H2"><font><font>
								<b>MENSAGEM!!!</b>
							</font></font></h4>
                    </div>

                    <div class="modal-body" style="padding-bottom: 20px">
                        <div class="row">
                            <div class="col-md-12">
                                <asp:Label ID="lblMensagem1" runat="server"></asp:Label>
                            </div>
                        </div>
                    </div>
                    <div class="modal-footer">                     
                        <asp:Button ID="btnFechar" runat="server" Text="FECHAR" class="btn btn-default" OnClick="btnFechar_Click" />
                    </div>
                </div>
            </div>
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

        <%-- modal mensagem de confirmação --%>
        <div class="modal fade" id="popUpMensagemConfirmacao" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true" style="">
            <div class="modal-dialog">
                <div class="modal-content " style="width: 280px; margin: 0 auto;">
                    <div id="Div2" runat="server" class="modal-header alert alert-warning" style="padding-bottom: 2px; text-align: center;">
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true"><font><font>×</font></font></button>
                        <h4 class="modal-title" id="H3"><font><font>
								<b>MENSAGEM!!!</b>
							</font></font></h4>
                    </div>
                    <div class="modal-body" style="padding-bottom: 20px">
                        <div class="row">
                            <div class="col-md-12">
                                <asp:Label ID="Label1" runat="server">Tem Certeza que deseja Desativar este Funcionário?</asp:Label>
                            </div>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <asp:Button ID="OK" runat="server" Text="Sim" class="btn btn-primary" OnClick="OK_Click" />
                        <button type="" class="btn btn-warning" data-dismiss="modal"><font><font>Cancelar</font></font></button>
                    </div>
                </div>
            </div>
        </div>

        <%-- modal criar login e senha --%>
        <div class="modal fade" id="popUpMensagemLogin" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true" style="">
            <div class="modal-dialog">
                <div class="modal-content " style="width: 280px; margin: 0 auto;">
                    <div id="po" runat="server" class="modal-header alert alert-warning" style="padding-bottom: 2px; text-align: center;">
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true"><font><font>×</font></font></button>
                        <h4 class="modal-title" id="H4"><font><font>
								<b>MENSAGEM!!!</b>
							</font></font></h4>
                    </div>
                    <div class="modal-body" style="padding-bottom: 20px">
                        <div class="row">
                            <div class="col-md-12">
                                <asp:Label ID="lblMensagemLogin" runat="server"></asp:Label>
                            </div>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <asp:Button ID="btnLogin" runat="server" Text="Sim" class="btn btn-primary" OnClick="btnLogin_Click" />
                        <button type="button" class="btn btn-default" data-dismiss="modal"><font><font>Cancelar</font></font></button>
                    </div>
                </div>
            </div>
        </div>
</asp:Content>
