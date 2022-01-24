<%@ Page Title="" Language="C#" MasterPageFile="~/PaginaPrincipal.Master" AutoEventWireup="true" CodeBehind="WebOrdemServicoC.aspx.cs" Inherits="Apresentacao.WebOrdemServicoC" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Panel ID="panelBusca" runat="server">
 <div class="row">
            <%--CAMPO DE BUSCA  --%>
            <div class="col-md-12">
                <div class="form-group col-md-5 pull-right">
                    <div class="input-group">
                        <span class="input-group-addon" style="padding: 1px 4px; font-size: 16px;">
                            <asp:DropDownList ID="ddlTipoBusca" runat="server" class="form-control" Width="150px" Style="font-size: 16px; height: 25px; padding: 2px 4px;" OnSelectedIndexChanged="ddlTipoBusca_SelectedIndexChanged" AutoPostBack="true">
                                <asp:ListItem Value="0">Abertas</asp:ListItem>
                                <asp:ListItem Value="1">Fechadas</asp:ListItem>
                                <asp:ListItem Value="2">Todas</asp:ListItem>
                                 <asp:ListItem Value="3">Data</asp:ListItem>

                            </asp:DropDownList>
                        </span>                        
                            <asp:TextBox ID="txtBuscar" runat="server" CssClass="form-control input-md date"></asp:TextBox>                     

                        <span class="input-group-addon">
                            <asp:LinkButton ID="btnBuscar" runat="server"  OnClick="btnBuscar_Click">
                            <i class="glyphicon glyphicon-search"></i>
                            </asp:LinkButton>
                        </span>                       
                    </div>
                </div>
            </div>

        </div>

        <div class="row">
            <div class="col-md-12">
                <div class=" scroll-grid col-md-12">
                    <asp:GridView ID="gvListaOS" runat="server" AutoGenerateColumns="False" OnSelectedIndexChanged="gvListaOS_SelectedIndexChanged"
                        DataKeyNames="osID" CssClass="table table-hover table-striped " CellPadding="4" ForeColor="#333333" OnRowDataBound="gvListaOS_RowDataBound">
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
                            <asp:BoundField DataField="osID" HeaderText="osID" Visible="false" />
                            <asp:BoundField DataField="Data de Início" HeaderText="Data de Início" />
                            <asp:BoundField DataField="Cliente" HeaderText="Cliente" />
                            <asp:BoundField DataField="Mecânico" HeaderText="Mecânico" />
                            <asp:BoundField DataField="Placa" HeaderText="Placa" />
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
        </div>
    </asp:Panel>

    <asp:Label ID="lblOrdemServicoID" runat="server" Text=""></asp:Label>
    <asp:Label ID="lblClienteID" runat="server"></asp:Label>
    <asp:Label ID="lblMotoID" runat="server"></asp:Label>
    <asp:Label ID="lblItemProdutoID" runat="server"></asp:Label>
    <asp:Label ID="lblQtdeAterior" runat="server"></asp:Label>
    <asp:Label ID="lblProdutoID" runat="server"></asp:Label>
    <%--ESTRUTURA--%>
    <asp:Panel ID="panelConteudo" runat="server">
        <fieldset>
            <legend>INFORMAÇÕES DA ORDEM DE SERVIÇO</legend>

            <asp:Label ID="lblIdLi" runat="server" Text="Label"></asp:Label>
            <div class="bs-example bs-example-tabs">
                <ul id="Ul1" class="nav nav-tabs" style="font: bold 20px arial;">
                    <asp:Literal ID="Literal1" runat="server"></asp:Literal>
                    <asp:Literal ID="Literal2" runat="server"></asp:Literal>

                </ul>
                <div id="myTabContent" class="tab-content " style="padding-top: 14px; min-height: 330px;">
                    <asp:Literal ID="Literal1b" runat="server"></asp:Literal>
                    <%-- 1 linha--%>
                    <div class="row ">

                        <div class="col-md-2">
                            <!-- Text input-->
                            <div class="form-group">
                                <label class=" control-label" for="txtDataAbertura">Data Inicio:</label>
                                <div>
                                    <asp:TextBox ID="txtDataAbertura" runat="server" class="form-control input-md"></asp:TextBox>
                                </div>
                            </div>
                        </div>

                        <div class="col-md-2">
                            <!-- Text input-->
                            <div class="form-group">
                                <label class="control-label" for="txtDataFinalizacao">Data Final:</label>
                                <div>
                                    <asp:TextBox ID="txtDataFinalizacao" runat="server" type="text" class="form-control input-md"></asp:TextBox>
                                </div>
                            </div>
                        </div>

                        <div class="col-md-3">
                            <!-- Select Basic -->
                            <div class="form-group">
                                <label class="control-label" for="ddlNomeMecanico">Mecânico:</label>
                                <div id="ddlUfdiv" runat="server">
                                    <asp:DropDownList ID="ddlNomeMecanico" runat="server" CssClass="form-control uppercase">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>

                        <div class="form-group col-md-5">
                            <div class="input-group col-md-12">
                                <label class=" control-label" for="txtNomeCliente">Nome do Cliente:</label>
                                <div class="input-group">
                                    <span class="input-group-addon"><span class="glyphicon glyphicon-user"></span></span>
                                    <asp:TextBox ID="txtNomeCliente" runat="server" class="form-control input-md uppercase"></asp:TextBox>


                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-3">
                            <!-- Text input-->
                            <div class="form-group">
                                <label class=" control-label" for="txtTelefone"><em>*</em> Telefone:</label>
                                <div class="input-group">
                                    <span class="input-group-addon"><span class="glyphicon glyphicon-earphone"></span></span>
                                    <asp:TextBox ID="txtTelefone" runat="server" class="form-control input-md"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <!-- Text input-->
                            <div class="form-group">
                                <label class=" control-label" for="txtCnpjCpf">CNPJ/CPF:</label>
                                <div>
                                    <asp:TextBox ID="txtCnpjCpf" runat="server" class="form-control input-md"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <!-- Text input-->
                            <div class="form-group">
                                <label class=" control-label" for="txtEndereco">Endereço:</label>
                                <div>
                                    <asp:TextBox ID="txtEndereco" runat="server" class="form-control input-md uppercase"></asp:TextBox>
                                </div>
                            </div>
                        </div>

                    </div>
                    <div class="row">
                        <div class="col-md-3">
                            <!-- Text input-->
                            <div class="form-group">
                                <label class=" control-label" for="txtBairro">Bairro:</label>
                                <div>
                                    <asp:TextBox ID="txtBairro" runat="server" class="form-control input-md uppercase"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="form-group col-md-2">
                            <div class="input-group">
                                <label class=" control-label" for="txtPlaca">Placa:</label>
                                <div class="input-group col-md-12">
                                    <asp:TextBox ID="txtPlaca" runat="server" class="form-control input-md uppercase"></asp:TextBox>


                                </div>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <!-- Text input-->
                            <div class="form-group">
                                <label class=" control-label" for="txtMarcaModelo">Marca/Modelo:</label>
                                <div>
                                    <asp:TextBox ID="txtMarcaModelo" runat="server" class="form-control input-md uppercase"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <!-- Text input-->
                            <div class="form-group">
                                <label class=" control-label" for="txtAno">Ano:</label>
                                <div>
                                    <asp:TextBox ID="txtAno" runat="server" class="form-control input-md text-center"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <!-- Text input-->
                            <div class="form-group">
                                <label class=" control-label" for="txtKm">Km:</label>
                                <div>
                                    <asp:TextBox ID="txtKm" runat="server" class="form-control input-md"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <!-- Textarea -->
                            <div class="form-group">
                                <label class="control-label" for="txtObservacao">Observações:</label>
                                <div>
                                    <asp:TextBox ID="txtObservacao" runat="server" TextMode="MultiLine" class="form-control uppercase"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                    <asp:Literal ID="Literal1c" runat="server"></asp:Literal>



                    <asp:Literal ID="Literal2b" runat="server"></asp:Literal>
                    <div class="row">
                        <div class="col-md-12">
                            <div class="col-md-6">
                                <div class="row">
                                    <div class=" scroll-grid col-md-12" style="max-height: 243px">
                                        <asp:GridView ID="gvProdutosInseridos" runat="server" AutoGenerateColumns="False" DataKeyNames="ItemProdutoID" CssClass="table table-hover table-striped "
                                            CellPadding="4" ForeColor="#333333">
                                            <AlternatingRowStyle BackColor="White" />
                                            <Columns>
                                                <asp:BoundField DataField="ItemProdutoID" HeaderText="ItemProdutoID" Visible="false" />
                                                <asp:BoundField DataField="Qtde" HeaderText="Qtde" ItemStyle-HorizontalAlign="Center" />
                                                <asp:BoundField DataField="Nome do Produto" HeaderText="Nome do Produto" />
                                                <asp:BoundField DataField="Preço" HeaderText="Preço" ItemStyle-HorizontalAlign="Right" />
                                                <asp:BoundField DataField="Total" HeaderText="Total" ItemStyle-HorizontalAlign="Right" />
                                                <asp:BoundField DataField="ProdutoID" HeaderText="ProdutoID" Visible="false" />
                                                <asp:BoundField DataField="osID" HeaderText="osID" Visible="false" />
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
                                    <%-- <div class="row">--%>

                                    <%--</div>--%>
                                </div>

                            </div>
                            <div class="col-md-6">
                                <div class="row">
                                    <div class="scroll-grid col-md-12" style="max-height: 243px">
                                        <asp:GridView ID="gvServicoInseridos" runat="server" AutoGenerateColumns="False"
                                            DataKeyNames="ItemServicoID" CssClass="table table-hover table-striped " CellPadding="4" ForeColor="#333333">
                                            <AlternatingRowStyle BackColor="White" />
                                            <Columns>
                                                <asp:BoundField DataField="ItemServicoID" HeaderText="ItemServicoID" Visible="false" />
                                                <asp:BoundField DataField="ServicoID" HeaderText="ServicoID" Visible="false" />
                                                <asp:BoundField DataField="Nome do Serviço" HeaderText="Nome do Serviço" />
                                                <asp:BoundField DataField="Preço" HeaderText="Preço" ItemStyle-HorizontalAlign="Right" />
                                                <asp:BoundField DataField="O.S.ID" HeaderText="O.S.ID" Visible="false" />
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
                            </div>
                        </div>
                    </div>
                    <asp:Literal ID="Literal2c" runat="server"></asp:Literal>
                </div>
            </div>


            <div class="row">
                <div>
                       <div class="form-group col-md-3 pull-right">
                        <label class="col-md-5 control-label" for="textinput">Subtotal:</label>
                        <div class="col-md-7 input-group">
                            <span class="input-group-addon"><span class="glyphicon glyphicon-usd"></span></span>
                            <asp:TextBox ID="txtTotalOS" runat="server" type="text" class="form-control input-md text-right money"></asp:TextBox>
                        </div>
                    </div>
                         <div class="form-group col-md-3 pull-right">
                        <label class="col-md-5 control-label" for="txtPrecoProduto">Total de Serviços:</label>
                        <div class="col-md-7 input-group">
                            <span class="input-group-addon"><span class="glyphicon glyphicon-usd"></span></span>
                            <asp:TextBox ID="txtTotalServicoColuna" runat="server" type="text" class="form-control input-md text-right money"></asp:TextBox>
                        </div>
                    </div>

                    <!-- Text input-->
                    <div class="form-group col-md-3 pull-right">
                        <label class="col-md-5 control-label" for="txtPrecoProduto">Total de Produto:</label>
                        <div class="col-md-7 input-group">
                            <span class="input-group-addon"><span class="glyphicon glyphicon-usd"></span></span>
                            <asp:TextBox ID="txtTotalProdutoColuna" runat="server" type="text" class="form-control input-md text-right money"></asp:TextBox>
                        </div>
                    </div>


               
                    <%--  </div>--%>
                 
                </div>
            </div>
            <div class="row">
                <div>
                    <div class="form-group col-md-3 pull-right" id="divReal" runat="server">
                        <label class="col-md-5 control-label text-right" for="textinput">Desconto:</label>
                        <div class="col-md-7 input-group">
                            <span class="input-group-addon"><span class="glyphicon glyphicon-usd"></span></span>
                            <asp:TextBox ID="txtDescontoReal" runat="server" type="text" class="form-control input-md text-right money"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group col-md-3 pull-right" id="divPorcento" runat="server">
                        <label class="col-md-5 control-label text-right" for="textinput">Desconto:</label>
                        <div class="col-md-7 input-group">
                            <span class="input-group-addon"><span>%</span></span>
                            <asp:TextBox ID="txtDescontoPorcento" runat="server" type="text" class="form-control input-md text-center porcento"></asp:TextBox>
                        </div>
                    </div>
                </div>

            </div>
            <div class="row">
                <div>
                    <div class="form-group col-md-4 pull-right">
                        <label class="col-md-6 control-label text-right" for="textinput">Total à Pagar:</label>
                        <div class="col-md-6 input-group">
                            <span class="input-group-addon"><span class="glyphicon glyphicon-usd"></span></span>
                            <asp:TextBox ID="txtTotalComDesconto" runat="server" type="text" class="form-control input-md text-right money"></asp:TextBox>
                        </div>
                    </div>
                </div>
            </div>
        </fieldset>
    </asp:Panel>
    <div class="row navbar-right">
        <div class="form-group ">
            <div class="col-md-12">
                <asp:Button ID="btnGravar" runat="server" Text="Finalizar" class="btn btn-primary" OnClick="btnGravar_Click" />
                <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" class="btn btn-warning" OnClick="btnCancelar_Click" />
                <asp:Button ID="btnImprimir" runat="server" Text="Imprimir" class="btn btn-default" OnClick="btnImprimir_Click" />
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
                            <asp:Label ID="lblMensagemConfirmacao" runat="server"></asp:Label>
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

    <asp:HiddenField ID="Hidden2" runat="server" />
    <script type="text/javascript">
        $(function () {
            $('#a').on('click', function () { $("#<%=Hidden2.ClientID %>").val('a'); })
            $('#b').on('click', function () { $("#<%=Hidden2.ClientID %>").val('b'); })
            $('#c').on('click', function () { $("#<%=Hidden2.ClientID%>").val('c'); })

        });

    </script>
</asp:Content>
