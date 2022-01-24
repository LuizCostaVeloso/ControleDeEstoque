<%@ Page Title="" Language="C#" MasterPageFile="~/PaginaPrincipal.Master" AutoEventWireup="true" CodeBehind="WebCliente.aspx.cs" Inherits="Apresentacao.WebCliente" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
        <div class="col-md-12">
            <asp:Panel ID="panelBusca" runat="server">
                <%--CAMPO DE BUSCA  --%>
                <div class="form-group">
                    <div class="input-group col-md-5 pull-right">
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
    <asp:Label ID="lblClienteID" runat="server"></asp:Label>
    <%--ESTRUTURA--%>
    <fieldset>
        <legend>INFORMAÇÕES DO CLIENTE</legend>
        <asp:Panel ID="panelConteudo" runat="server">
            <%-- 1 linha--%>
            <div class="row">
                <div class="col-md-2">
                    <!-- Text input-->
                    <div class="form-group">
                        <label class=" control-label" for="txtDtCriacao">Data:</label>
                        <div>
                            <asp:TextBox ID="txtDtCriacao" runat="server"  class="form-control input-md text-center"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="col-md-7">
                    <!-- Text input-->
                    <div class="form-group">
                        <label class="control-label" for="txtNome"><em>*</em> Nome:</label>
                        <div class="input-group">
                            <span class="input-group-addon"><span class="glyphicon glyphicon-user"></span></span>
                            <asp:TextBox ID="txtNome" runat="server" type="text"  class="form-control input-md uppercase" MaxLength="63"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="col-md-3">
                    <!-- Multiple Radios (inline) -->
                    <div class="form-group">
                        <label class=" control-label" for="rbSexo" style="padding-bottom: 5px;"><em>*</em> Sexo:</label>
                        <div>
                            <label class="radio-inline" for="rbSexo-0">
                                <asp:RadioButton ID="rbSexo0" runat="server" GroupName="a" value="0" />
                                Masculino                                                      
                            </label>
                            <label class="radio-inline" for="rbSexo-1">
                                <asp:RadioButton ID="rbSexo1" runat="server" GroupName="a" value="1" />
                                Feminino
                            </label>
                        </div>
                    </div>
                </div>
            </div>
            <%-- 2 linha--%>
            <div class="row">
                <div class="col-md-3" style="width: 18%">
                    <!-- Multiple Radios (inline) -->
                    <div class="form-group">
                        <label class=" control-label" for="rbTipoPessoa" style="padding-bottom: 5px;"><em>*</em> Pessoa:</label>
                        <div>
                            <label class="radio-inline" for="rbTipoPessoa-0">
                                <asp:RadioButton ID="rbTipoPessoa0" runat="server" GroupName="b" value="0" AutoPostBack="true" OnCheckedChanged="rbTipoPessoa0_CheckedChanged" />
                                Física                                                       
                            </label>
                            <label class="radio-inline" for="rbTipoPessoa-1">
                                <asp:RadioButton ID="rbTipoPessoa1" runat="server" GroupName="b" value="1" AutoPostBack="true" OnCheckedChanged="rbTipoPessoa1_CheckedChanged" />
                                Jurídica
                            </label>
                        </div>
                    </div>
                </div>
                <div class="col-md-3">
                    <!-- Text input-->
                    <div class="form-group">
                        <label class=" control-label" for="txtCpf"><em>*</em> CPF/Cnpj:</label>
                        <div>
                            <asp:TextBox ID="txtCpf" runat="server"  class="form-control input-md cpf"></asp:TextBox>
                            <asp:TextBox ID="txtCnpj" runat="server" class="form-control input-md cnpj"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="col-md-2">
                    <!-- Text input-->
                    <div class="form-group">
                        <label class="control-label" for="txtRg">RG:</label>
                        <div>
                            <asp:TextBox ID="txtRg" runat="server"  CssClass="form-control input-md rg" MaxLength="11"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="col-md-2">
                    <!-- Text input-->
                    <div class="form-group">
                        <label class="control-label" for="txtOrgaoEmissor">Org. Exp.:</label>
                        <div>
                            <asp:TextBox ID="txtOrgaoEmissor" runat="server" CssClass="form-control input-md uppercase" MaxLength="6"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="col-md-3" style="width: 23.6%;">
                    <!-- Text input-->
                    <div class="form-group">
                        <label class="control-label" for="txtCep">CEP:</label>
                        <div>
                            <asp:TextBox ID="txtCep" runat="server"  class="form-control input-md cep" MaxLength="9"></asp:TextBox>
                        </div>
                    </div>
                </div>
            </div>

            <%--3 linha--%>
            <div class="row">

                <div class="col-md-6">
                    <!-- Text input-->
                    <div class="form-group">
                        <label class="control-label" for="txtEndereco"><em>*</em> Endereço:</label>
                        <div>
                            <asp:TextBox ID="txtEndereco" runat="server"  class="form-control input-md uppercase" MaxLength="57"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="col-md-3">
                    <!-- Text input-->
                    <div class="form-group">
                        <label class=" control-label" for="txtBairro"><em>*</em> Bairro:</label>
                        <div>
                            <asp:TextBox ID="txtBairro" runat="server"  class="form-control input-md uppercase" MaxLength="25"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="col-md-3">
                    <!-- Text input-->
                    <div class="form-group">
                        <label class="control-label" for="txtCidade"><em>*</em> Cidade:</label>
                        <div>
                            <asp:TextBox ID="txtCidade" runat="server"  class="form-control input-md uppercase" MaxLength="25"></asp:TextBox>
                        </div>
                    </div>
                </div>
            </div>
            <%-- 4 linha--%>
            <div class="row">
                <div class="col-md-2" style="width: 10%;">
                    <!-- Select Basic -->
                    <div class="form-group">
                        <label class="control-label" for="ddlUf"><em>*</em> UF:</label>
                        <div id="ddlUfdiv" runat="server">
                            <asp:DropDownList ID="ddlUf" runat="server" class="form-control"></asp:DropDownList>
                        </div>
                    </div>
                </div>
                <div class="col-md-7" style="width: 65.9%;">
                    <!-- Text input-->
                    <div class="form-group">
                        <label class="control-label" for="txtComplemento">Complemento:</label>
                        <div>
                            <asp:TextBox ID="txtComplemento" runat="server"  class="form-control input-md uppercase" MaxLength="77"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="col-md-3" style="width: 24.1%;">
                    <!-- Text input-->
                    <div class="form-group">
                        <label class="control-label" for="txtTel1"><em>*</em> Telefone (1):</label>
                        <div class="input-group">
                            <span class="input-group-addon"><span class="glyphicon glyphicon-earphone"></span></span>
                            <asp:TextBox ID="txtTel1" runat="server"  class="form-control input-md phone"></asp:TextBox>
                        </div>
                    </div>
                </div>               

            </div>
            <%--5 linha--%>
            <div class="row">
                 <div class="col-md-3" style="width: 24.1%;">
                    <!-- Text input-->
                    <div class="form-group">
                        <label class="control-label" for="txtTel2">Telefone (2):</label>
                        <div class="input-group">
                            <span class="input-group-addon"><span class="glyphicon glyphicon-earphone"></span></span>
                            <asp:TextBox ID="txtTel2" runat="server"  class="form-control input-md phone"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="col-md-6">
                    <!-- Text input-->
                    <div class="form-group">
                        <label class="control-label" for="txtEmail">E-mail:</label>
                        <div class="input-group">
                            <span class="input-group-addon">@</span>
                            <asp:TextBox ID="txtEmail" runat="server"  class="form-control input-md" MaxLength="52"></asp:TextBox>
                        </div>
                    </div>
                </div>
            </div>
            <%-- 6 linha--%>
            <div class="row">
                <div class="col-md-12">
                    <!-- Textarea -->
                    <div class="form-group">
                        <label class="control-label" for="txtObservacao">Observações:</label>
                        <div>
                            <asp:TextBox ID="txtObservacao" runat="server" TextMode="MultiLine"   class="form-control uppercase" MaxLength="300"></asp:TextBox>
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
                    <asp:Button ID="btnExcluir" runat="server" Text="Excluir" class="btn btn-danger" OnClick="btnExcluir_Click" />
                    <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" class="btn btn-warning" OnClick="btnCancelar_Click" />

                </div>
            </div>
        </div>
    </fieldset>

    <%-- modal da lista de clientes--%>

    <div class="modal fade" id="popUpLista" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true" style="">
        <div class="modal-dialog">
            <div class="modal-content" style="max-width: 800px;">
                <a class="list-group-item active"></a>
                <div class="modal-header" style="padding: 0px 15px;">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true"><font><font>×</font></font></button>
                    <h4 class="modal-title" id="myModalLabel"><font><font>
								<p>LISTA DE CLIENTES</p>
							</font></font></h4>
                </div>
                <div class="modal-body">
                    <div class="row scroll-grid">

                        <asp:GridView ID="gvListaCliente" runat="server" AutoGenerateColumns="False"
                            DataKeyNames="ClienteID" CssClass="table table-hover table-striped "
                            OnSelectedIndexChanged="gvListaCliente_SelectedIndexChanged" CellPadding="4" ForeColor="#333333">
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
                                <asp:BoundField DataField="ClienteID" HeaderText="ClienteID" Visible="false" />
                                <asp:BoundField DataField="Nome" HeaderText="Nome" />
                                <asp:BoundField DataField="CPF/CNPJ" HeaderText="CPF/CNPJ" />
                                <asp:BoundField DataField="Sexo" HeaderText="Sexo" />
                                <asp:BoundField DataField="Telefone 1" HeaderText="Telefone 1" />
                                <asp:BoundField DataField="Telefone 2" HeaderText="Telefone 2" />
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

    <div class="modal fade" id="popUpMensagem" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true" style="">
        <div class="modal-dialog">
            <div class="modal-content " style="width: 280px; margin: 0 auto;">
                <div id="mensagemSucesso" runat="server" class="modal-header alert alert-success" style="padding-bottom: 2px; text-align: center;">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true"><font><font>×</font></font></button>
                    <h4 class="modal-title" id="H1"><font><font>
								<b>MENSAGEM!!!</b>
							</font></font></h4>
                </div>
                <div id="mensagemErro" runat="server" class="modal-header alert alert-danger" style="padding-bottom: 2px; text-align: center;">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true"><font><font>×</font></font></button>
                    <h4 class="modal-title" id="H2"><font><font>
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
                            <asp:Label ID="Label1" runat="server">Tem Certeza que deseja Excluir?</asp:Label>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <asp:Button ID="OK" runat="server" Text="Sim" class="btn btn-primary" OnClick="OK_Click" />
                    <button type="button" class="btn btn-warning" data-dismiss="modal"><font><font>Cancelar</font></font></button>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
