<%@ Page Title="" Language="C#" MasterPageFile="~/PaginaPrincipal.Master" AutoEventWireup="true" CodeBehind="WebOrdemServicoA.aspx.cs" Inherits="Apresentacao.WebOrdemServicoA" %>

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
                        <span class="input-group-addon">
                            <asp:LinkButton ID="btnNovo" runat="server" OnClick="btnNovo_Click">
                            <i class="glyphicon glyphicon-plus"></i>
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
                    <asp:Literal ID="Literal3" runat="server"></asp:Literal>

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
                            <div class="form-group">
                                <label class=" control-label" for="txtDataFinalizacao">Data Final:</label>
                                <div id="divDataFinalizacao" runat="server">
                                    <asp:TextBox ID="txtDataFinalizacao" runat="server" class="form-control input-md"></asp:TextBox>
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
                                <label class=" control-label" for="txtNomeCliente"><em>*</em> Nome do Cliente:</label>
                                <div class="input-group">
                                    <span class="input-group-addon"><span class="glyphicon glyphicon-user"></span></span>
                                    <asp:TextBox ID="txtNomeCliente" runat="server" class="form-control input-md uppercase"></asp:TextBox>

                                    <span class="input-group-addon" id="spanBuscarCliente" runat="server">
                                        <asp:LinkButton ID="btnBuscarCliente" runat="server" OnClick="btnBuscarCliente_Click">
                            <i class="glyphicon glyphicon-search"></i>
                                        </asp:LinkButton>
                                    </span>
                                    <span class="input-group-addon" style="padding: 0px 10px;" id="spanLimparInfCliente" runat="server">
                                        <asp:LinkButton ID="btnLimparInfCliente" runat="server" OnClick="btnLimparInfCliente_Click">
                              <i > <span class="input-group-addon" style="padding:0px; border:0;"><img src="img/vassoura.png" /></span></i>
                                        </asp:LinkButton>
                                    </span>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-3">
                            <!-- Text input-->
                            <div class="form-group">
                                <label class=" control-label" for="txtTelefone">Telefone:</label>
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
                                <label class=" control-label" for="txtPlaca"><em>*</em> Placa:</label>
                                <div class="input-group col-md-12">
                                    <asp:TextBox ID="txtPlaca" runat="server" class="form-control input-md uppercase"></asp:TextBox>

                                    <span class="input-group-addon" id="spanBuscarPlaca" runat="server">
                                        <asp:LinkButton ID="btnBuscarPlaca" runat="server" OnClick="btnBuscarPlaca_Click">
                            <i class="glyphicon glyphicon-search"></i>
                                        </asp:LinkButton>
                                    </span>
                                    <span class="input-group-addon" style="padding: 0px 10px;" id="spanLimparInfPlaca" runat="server">
                                        <asp:LinkButton ID="btnLimparInfPlaca" runat="server" OnClick="btnLimparInfPlaca_Click">
                              <i > <span class="input-group-addon" style="padding:0px; border:0;"><img src="img/vassoura.png" /></span></i>
                                        </asp:LinkButton>
                                    </span>
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
                    <div class="col-md-12">
                        <div class="col-md-6">
                            <div class="row">
                                <div class="form-group col-md-12">
                                    <div class="input-group">
                                        <label class=" control-label" for="txtNomeProduto">Nome do Produto:</label>
                                        <div class="input-group col-md-12">
                                            <asp:TextBox ID="txtNomeProduto" runat="server" class="form-control input-md uppercase" Width="469px"></asp:TextBox>

                                            <span class="input-group-addon" id="spanBuscarProduto" runat="server">
                                                <asp:LinkButton ID="btnBuscarProduto" runat="server" OnClick="btnBuscarProduto_Click">
                            <i class="glyphicon glyphicon-search"></i>
                                                </asp:LinkButton>
                                            </span>
                                            <span class="input-group-addon" style="padding: 0px 10px;" id="spanLimparInsercaoProduto" runat="server">
                                                <asp:LinkButton ID="btnLimparInsercaoProduto" runat="server" OnClick="btnLimparInsercaoProduto_Click">
                              <i > <span class="input-group-addon" style="padding:0px; border:0;"><img src="img/vassoura.png" /></span></i>
                                                </asp:LinkButton>
                                            </span>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <%--<div class="col-md-12">--%>
                                <div class="col-md-3">
                                    <!-- Text input-->
                                    <div class="form-group">
                                        <label class="control-label" for="txtQuantidade">Quantidade.:</label>
                                        <div class="input-group">
                                            <asp:TextBox ID="txtQuantidade" runat="server" type="text" class="form-control input-md text-center" AutoPostBack="true" OnTextChanged="txtQuantidade_TextChanged"></asp:TextBox>
                                            <span class="input-group-addon" style="padding: 0px 10px;" id="spanLimparQuantidade" runat="server">
                                                <asp:LinkButton ID="btnLimparQuantidade" runat="server" OnClick="btnLimparQuantidade_Click">
                              <i > <span class="input-group-addon" style="padding:0px; border:0;"><img src="img/vassoura.png" /></span></i>
                                                </asp:LinkButton>
                                            </span>
                                        </div>

                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <!-- Text input-->
                                    <div class="form-group">
                                        <label class="control-label" for="txtPrecoProduto">Preço:</label>
                                        <div class="input-group">
                                            <span class="input-group-addon"><span class="glyphicon glyphicon-usd"></span></span>
                                            <asp:TextBox ID="txtPrecoProduto" runat="server" type="text" class="form-control input-md text-right money"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-5">
                                    <!-- Text input-->
                                    <div class="form-group">
                                        <div class="input-group">
                                            <label class="control-label" for="txtTotalProdutoLinha">Total:</label>
                                            <div class="input-group">
                                                <span class="input-group-addon"><span class="glyphicon glyphicon-usd"></span></span>
                                                <asp:TextBox ID="txtTotalProdutoLinha" runat="server" type="text" class="form-control input-md text-right money"></asp:TextBox>
                                                <span class="input-group-addon" id="spanInserirProduto" runat="server">
                                                    <asp:LinkButton ID="btnInserirProduto" runat="server" OnClick="btnInserirProduto_Click">
                            <i class="glyphicon glyphicon-save"></i>
                                                    </asp:LinkButton>
                                                </span>

                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <%--</div>--%>
                            </div>                         
                        </div>
                        <div class="col-md-6">
                            <div class="row">
                                <div class=" scroll-grid">
                                    <asp:GridView ID="gvProdutosInseridos" runat="server" AutoGenerateColumns="False" DataKeyNames="ItemProdutoID,ProdutoID,Qtde,Nome do Produto" CssClass="table table-hover table-striped "
                                        OnRowCommand="gvProdutosInseridos_RowCommand" CellPadding="4" ForeColor="#333333">
                                        <AlternatingRowStyle BackColor="White" />
                                        <Columns>
                                            <asp:TemplateField HeaderStyle-Width="1%" ShowHeader="False">
                                                <ItemTemplate>
                                                    <span class="input-group-addon" style="border: 1px solid #ccc">
                                                        <asp:LinkButton ID="btnEditar" runat="server" CausesValidation="False" CommandArgument="<%#((GridViewRow)Container).RowIndex%>"
                                                            Text="Selecionar" CommandName="Editar"> <i class="glyphicon glyphicon-pencil"></i></asp:LinkButton>
                                                    </span>
                                                </ItemTemplate>
                                                <HeaderStyle Width="1%" />
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="ItemProdutoID" HeaderText="ItemProdutoID" Visible="false" />
                                            <asp:BoundField DataField="Qtde" HeaderText="Qtde" />
                                            <asp:BoundField DataField="Nome do Produto" HeaderText="Nome do Produto" />
                                            <asp:BoundField DataField="Preço" HeaderText="Preço" ItemStyle-HorizontalAlign="Right" />
                                            <asp:BoundField DataField="Total" HeaderText="Total" ItemStyle-HorizontalAlign="Right"/>
                                            <asp:BoundField DataField="ProdutoID" HeaderText="ProdutoID" Visible="false" />
                                            <asp:BoundField DataField="osID" HeaderText="osID" Visible="false" />
                                            <asp:TemplateField HeaderStyle-Width="1%" ShowHeader="False">
                                                <ItemTemplate>
                                                    <span class="input-group-addon" style="border: 1px solid #ccc">
                                                        <asp:LinkButton ID="btnExcluir" runat="server" CausesValidation="False" CommandArgument="<%#((GridViewRow)Container).RowIndex%>"
                                                            Text="Selecionar" CommandName="Excluir"> <i class="glyphicon glyphicon-trash"></i></asp:LinkButton>
                                                    </span>
                                                </ItemTemplate>
                                                <HeaderStyle Width="1%" />
                                            </asp:TemplateField>
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
                    <asp:Literal ID="Literal2c" runat="server"></asp:Literal>

                    <asp:Literal ID="Literal3b" runat="server"></asp:Literal>
                    <div class="col-md-12">

                        <div class="col-md-6">
                            <div class="row">
                                <div class="form-group col-md-12">
                                    <div class="input-group">
                                        <label class=" control-label" for="txtNomeServico">Serviço:</label>
                                        <div class="input-group col-md-12">
                                            <asp:TextBox ID="txtNomeServico" runat="server" class="form-control input-md uppercase" Width="469px"></asp:TextBox>

                                            <span class="input-group-addon">
                                                <asp:LinkButton ID="btnBuscarServico" runat="server" OnClick="btnBuscarServico_Click">
                            <i class="glyphicon glyphicon-search"></i>
                                                </asp:LinkButton>
                                            </span>
                                        </div>
                                    </div>
                                </div>
                            </div>                        

                        </div>
                        <div class="col-md-6">
                            <div class="row">
                                <div class="col-md-12 scroll-grid">
                                    <asp:GridView ID="gvServicoInseridos" runat="server" AutoGenerateColumns="False" OnSelectedIndexChanged="gvServicoInseridos_SelectedIndexChanged"
                                        DataKeyNames="ItemServicoID" CssClass="table table-hover table-striped " CellPadding="4" ForeColor="#333333">
                                        <AlternatingRowStyle BackColor="White" />
                                        <Columns>

                                            <asp:BoundField DataField="ItemServicoID" HeaderText="ItemServicoID" Visible="false" />
                                            <asp:BoundField DataField="ServicoID" HeaderText="ServicoID" Visible="false" />
                                            <asp:BoundField DataField="Nome do Serviço" HeaderText="Nome do Serviço" />
                                            <asp:BoundField DataField="Preço" HeaderText="Preço" ItemStyle-HorizontalAlign="Right"/>
                                            <asp:BoundField DataField="O.S.ID" HeaderText="O.S.ID" Visible="false" />
                                            <asp:TemplateField HeaderStyle-Width="1%" ShowHeader="False">
                                                <ItemTemplate>
                                                    <span class="input-group-addon" style="border: 1px solid #ccc">
                                                        <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Select"
                                                            Text="Selecionar"> <i class="glyphicon glyphicon-trash"></i></asp:LinkButton>
                                                    </span>
                                                </ItemTemplate>
                                                <HeaderStyle Width="1%" />
                                            </asp:TemplateField>
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
                    <asp:Literal ID="Literal3c" runat="server"></asp:Literal>
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
                    <div class="col-md-5">
                    </div>
                    <div class="form-group col-md-4">
                        <label class="col-md-8 control-label text-right" for="textinput">Tipo de Desconto:</label>
                        <div class="col-md-4">
                            <label class="radio-inline" for="rbTipoDesconto-0">
                                <asp:RadioButton ID="rbTipoDesconto0" runat="server" GroupName="a" value="0" AutoPostBack="true" Checked="true" OnCheckedChanged="rbTipoDesconto0_CheckedChanged" />
                                $
                            </label>
                            <label class="radio-inline" for="rbTipoDesconto-1">
                                <asp:RadioButton ID="rbTipoDesconto1" runat="server" GroupName="a" value="1" AutoPostBack="true" OnCheckedChanged="rbTipoDesconto1_CheckedChanged" />
                                %                                                      
                            </label>

                        </div>
                    </div>
                    <div class="form-group col-md-3" id="divReal" runat="server">
                        <label class="col-md-5 control-label text-right" for="txtDescontoReal">Desconto:</label>
                        <div class="col-md-7 input-group">
                            <span class="input-group-addon"><span class="glyphicon glyphicon-usd"></span></span>
                            <asp:TextBox ID="txtDescontoReal" runat="server" type="text" OnTextChanged="txtDescontoReal_TextChanged" class="form-control input-md text-right money" AutoPostBack="true"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group col-md-3" id="divPorcento" runat="server">
                        <label class="col-md-5 control-label text-right" for="txtDescontoPorcento">Desconto:</label>
                        <div class="col-md-7 input-group">
                            <span class="input-group-addon"><span>%</span></span>
                            <asp:TextBox ID="txtDescontoPorcento" runat="server" type="text" OnTextChanged="txtDescontoPorcento_TextChanged" class="form-control input-md text-center porcento" AutoPostBack="true"></asp:TextBox>
                        </div>
                    </div>
                </div>

            </div>
            <div class="row">
                <div>
                    <div class="form-group col-md-4 pull-right">
                        <label class="col-md-6 control-label text-right" for="txtTotalComDesconto">Total à Pagar:</label>
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
                <asp:Button ID="btnGravar" runat="server" Text="Gravar" class="btn btn-primary" OnClick="btnGravar_Click" />
                <asp:Button ID="btnEditar" runat="server" Text="Editar" class="btn btn-primary" OnClick="btnEditar_Click" />               
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

    <%-- modal da lista de clientes--%>
    <div class="modal fade" id="popUpListaCliente" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true" style="">
        <div class="modal-dialog">
            <div class="modal-content" style="max-width: 800px;">
                <a class="list-group-item active"></a>
                <div class="modal-header" style="padding: 0px 15px;">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true"><font><font>×</font></font></button>
                    <h4 class="modal-title" id="H4"><font><font>
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

    <%-- modal da lista de moto--%>
    <div class="modal fade" id="popUpListaMoto" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true" style="">
        <div class="modal-dialog">
            <div class="modal-content" style="max-width: 800px;">
                <a class="list-group-item active"></a>
                <div class="modal-header" style="padding: 0px 15px;">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true"><font><font>×</font></font></button>
                    <h4 class="modal-title" id="H5"><font><font>
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

    <%-- modal da lista de serviços--%>
    <div class="modal fade" id="popUpListaServico" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true" style="">
        <div class="modal-dialog">
            <div class="modal-content" style="max-width: 800px;">
                <a class="list-group-item active"></a>
                <div class="modal-header" style="padding: 0px 15px;">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true"><font><font>×</font></font></button>
                    <h4 class="modal-title" id="myModalLabel"><font><font>
								<p>LISTA DE SERVIÇOS</p>
							</font></font></h4>
                </div>
                <div class="modal-body">
                    <div class="row scroll-grid">
                        <asp:GridView ID="gvListaServico" runat="server" AutoGenerateColumns="False" OnSelectedIndexChanged="gvInserirServico_SelectedIndexChanged"
                            DataKeyNames="ServicoID" CssClass="table table-hover table-striped " CellPadding="4" ForeColor="#333333">
                            <AlternatingRowStyle BackColor="White" />
                            <Columns>
                                <asp:TemplateField ShowHeader="False" HeaderStyle-Width="1%">
                                    <ItemTemplate>
                                        <span class="input-group-addon" style="border: 1px solid #ccc">
                                            <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Select"
                                                Text="Selecionar"> <i class="glyphicon glyphicon-save" ></i></asp:LinkButton>
                                        </span>
                                    </ItemTemplate>

                                    <HeaderStyle Width="1%"></HeaderStyle>
                                </asp:TemplateField>
                                <asp:BoundField DataField="Nome do Serviço" HeaderText="Nome do Serviço" />
                                <asp:BoundField DataField="Marca/Modelo" HeaderText="Modelo" />
                                <asp:BoundField DataField="Preço" HeaderText="Preço" ItemStyle-HorizontalAlign="Right"/>
                                <asp:BoundField DataField="ServicoID" HeaderText="ServicoID" Visible="false" />
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

    <%--MODAL LISTA produto--%>
    <div class="modal fade" id="popUpListaProduto" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true" style="">
        <div class="modal-dialog">
            <div class="modal-content" style="max-width: 800px;">
                <a class="list-group-item active"></a>
                <div class="modal-header" style="padding: 0px 15px;">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true"><font><font>×</font></font></button>
                    <h4 class="modal-title" id="H6"><font><font>
								<p>LISTA DE PRODUTO</p>
							</font></font></h4>
                </div>
                <div class="modal-body">
                    <div class="row scroll-grid">
                        <asp:GridView ID="gvListaProduto" runat="server" AutoGenerateColumns="False" OnRowDataBound="gvListaProduto_RowDataBound"
                            DataKeyNames="ProdutoID" CssClass="table table-hover table-striped " OnSelectedIndexChanged="gvListaProduto_SelectedIndexChanged" CellPadding="4" ForeColor="#333333">
                            <AlternatingRowStyle BackColor="White" />
                            <Columns>
                                <asp:TemplateField ShowHeader="False" HeaderStyle-Width="1%">
                                    <ItemTemplate>
                                        <span class="input-group-addon" style="border: 1px solid #ccc">
                                            <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Select"
                                                Text="Selecionar"> <i class="glyphicon glyphicon-ok"></i></asp:LinkButton>
                                        </span>
                                    </ItemTemplate>

                                    <HeaderStyle Width="1%"></HeaderStyle>
                                </asp:TemplateField>
                                <asp:BoundField DataField="ProdutoID" HeaderText="ProdutoID" Visible="false" />
                                <asp:BoundField DataField="Nome do Produto" HeaderText="Nome do Produto" />
                                <asp:BoundField DataField="Marca/Modelo" HeaderText="Marca/Modelo" />
                                <asp:BoundField DataField="Preço" HeaderText="Preço" ItemStyle-HorizontalAlign="Right"/>
                                <asp:BoundField DataField="Estoque" HeaderText="Estoque" ItemStyle-HorizontalAlign="Center"/>
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


    <asp:HiddenField ID="Hidden2" runat="server" />
    <script type="text/javascript">
        $(function () {
            $('#a').on('click', function () { $("#<%=Hidden2.ClientID %>").val('a'); })
            $('#b').on('click', function () { $("#<%=Hidden2.ClientID %>").val('b'); })
            $('#c').on('click', function () { $("#<%=Hidden2.ClientID%>").val('c'); })

        });

    </script>
</asp:Content>
