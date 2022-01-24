<%@ Page Title="" Language="C#" MasterPageFile="~/PaginaPrincipal.Master" AutoEventWireup="true" CodeBehind="WebMoto.aspx.cs" Inherits="Apresentacao.WebMoto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">

        <div class="col-md-12">
            <asp:Panel ID="panelBusca" runat="server">
                <%--CAMPO DE BUSCA  --%>
                <div class="form-group">
                    <div class="input-group col-md-5 pull-right">
                        <asp:TextBox ID="txtBuscar" runat="server" class="form-control input-md"></asp:TextBox>

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
    <asp:Label ID="lblMotoID" runat="server" Text=""></asp:Label>
    <%--ESTRUTURA--%>
    <fieldset>
        <legend>INFORMAÇÕES DA MOTO</legend>
        <asp:Panel ID="panelConteudo" runat="server">
            <%-- 1 linha--%>
            <div class="row">
                <div class="col-md-2" style="width: 12%;">
                    <!-- Text input-->
                    <div class="form-group">
                        <label class=" control-label" for="txtPlaca"><em>*</em> Placa:</label>
                        <div>
                            <asp:TextBox ID="txtPlaca" runat="server" class="form-control input-md uppercase" MaxLength="8"></asp:TextBox>

                        </div>
                    </div>
                </div>
                <div class="col-md-3">
                    <!-- Text input-->
                    <div class="form-group">
                        <label class="control-label" for="txtModeloMarca">
                            <em>*</em>
                            Marca/Modelo:</label>
                        <div>
                            <asp:TextBox ID="txtModeloMarca" runat="server" type="text" class="form-control input-md uppercase" MaxLength="25"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="col-md-3">
                    <!-- Text input-->
                    <div class="form-group">
                        <label class=" control-label" for="txtChassi">Chassi:</label>
                        <div>
                            <asp:TextBox ID="txtChassi" runat="server"  class="form-control input-md uppercase" MaxLength="20"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="col-md-3">
                    <!-- Text input-->
                    <div class="form-group">
                        <label class=" control-label" for="txtCorPredominante">Cor:</label>
                        <div>
                            <asp:TextBox ID="txtCorPredominante" runat="server"  class="form-control input-md uppercase" MaxLength="25"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="col-md-1" style="width: 13%;">
                    <!-- Text input-->
                    <div class="form-group">
                        <label class=" control-label" for="txtAnoFabricacao">
                            <em>*</em>
                            Ano:</label>
                        <div>
                            <asp:TextBox ID="txtAnoFabricacao" runat="server"  class="form-control input-md text-center ano" MaxLength="4"></asp:TextBox>
                        </div>
                    </div>
                </div>
            </div>

            <%-- 2 linha--%>
            <div class="row">
            </div>
            <%-- 3 linha--%>
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
        <%-- 4 linha--%>
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
    <%--modal lista --%>
    <div class="modal fade" id="popUpLista" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true" style="">
        <div class="modal-dialog">
            <div class="modal-content" style="max-width: 800px;">
                <a class="list-group-item active"></a>
                <div class="modal-header" style="padding: 0px 15px;">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true"><font><font>×</font></font></button>
                    <h4 class="modal-title" id="myModalLabel"><font><font>
								<p>LISTA DE MOTO</p>
							</font></font></h4>
                </div>
                <div class="modal-body">
                    <div class="row scroll-grid">
                        <asp:GridView ID="gvListaMoto" runat="server" AutoGenerateColumns="False" OnSelectedIndexChanged="gvListaMoto_SelectedIndexChanged"
                            DataKeyNames="motoID" CssClass="table table-hover table-striped " CellPadding="4" ForeColor="#333333">
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
                                <asp:BoundField DataField="MotoID" HeaderText="MotoID" Visible="false" />
                                <asp:BoundField DataField="Placa" HeaderText="Placa" />
                                <asp:BoundField DataField="Modelo/Marca" HeaderText="Modelo/Marca" />
                                <asp:BoundField DataField="Cor" HeaderText="Cor" />
                                <asp:BoundField DataField="Ano" HeaderText="Ano" />
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
