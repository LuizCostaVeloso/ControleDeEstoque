<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebOrdemServicoImpressao.aspx.cs" Inherits="Apresentacao.Relatorios.WebOrdemServicoImpressao" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Mix Motos</title>
    <link href="../css/bootstrap.css" rel="stylesheet" />
    <link href="../css/Impressao.css" rel="stylesheet" />
    <script type="text/javascript">
        function imprimir() {
            window.Button1.style.display = "none";

            window.print();

            window.Button1.style.display = "";
        }</script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <div class="row text-center" style="margin-bottom: 6px; background-color: #507CD1">
                <p>Ordem de Serviço</p>
            </div>
            <div class="row div">
                <div style="width: 231px; float: left;" class="div">
                    <label>Data Inicio:</label>
                    <asp:Label ID="lblDataAbertura" runat="server"></asp:Label>
                </div>
                <div style="width: 231px; float: left;" class="div">
                    <label>Data Final:</label>
                    <asp:Label ID="lblDataFinalizacao" runat="server"></asp:Label>
                </div>
                <div style="width: 150px; float: left">
                    <label>Tel.:</label>
                    <asp:Label ID="lblTelefone" runat="server"></asp:Label>
                </div>
            </div>
            <div class="row div">
                <div style="width: 453px; float: left;" class="div">
                    <label>Nome do Cliente:</label>
                    <asp:Label ID="lblNomeCliente" runat="server"></asp:Label>
                </div>
                <div style="width: 208px; float: left;" class="div">
                    <label>CNPJ/CPF:</label>
                    <asp:Label ID="lblCnpjCpf" runat="server"></asp:Label>
                </div>
            </div>
            <div class="row">
                <div style="width: 400px; float: left;">
                    <label>Endereço:</label>
                    <asp:Label ID="lblEndereco" runat="server" ></asp:Label>
                </div>
                <!-- Text input-->
                <div style="width: 262px; float: left;">
                    <label>Bairro:</label>
                    <asp:Label ID="lblBairro" runat="server"></asp:Label>
                </div>
            </div>
            <div class="row">
                <div style="width: 150px; float: left;">
                    <label>Placa:</label>
                    <asp:Label ID="lblPlaca" runat="server"></asp:Label>
                </div>

                <div style="width: 422px; float: left;">
                    <label>Marca/Modelo:</label>
                    <asp:Label ID="lblMarcaModelo" runat="server" ></asp:Label>
                </div>
                <div style="width: 90px; float: left;">
                    <label>Ano:</label>
                    <asp:Label ID="lblAno" runat="server" ></asp:Label>
                </div>
            </div>
            <div class="row">
                <div style="width: 231px; float: left;">
                    <label>Km:</label>
                    <asp:Label ID="lblKm" runat="server" ></asp:Label>
                </div>
                <div style="width: 429px; float: left;">
                    <label>Mecânico:</label>
                    <asp:Label ID="lblNomeMecanico" runat="server"></asp:Label>
                </div>



            </div>

            <asp:Panel ID="pnlprodutoInserido" runat="server" CssClass="row">
                <asp:GridView ID="gvProdutosInseridos" runat="server" AutoGenerateColumns="False" DataKeyNames="ItemProdutoID" CssClass="table table-hover table-striped "
                    CellPadding="4" ForeColor="#333333">
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:BoundField DataField="ItemProdutoID" HeaderText="ItemProdutoID" Visible="false" />
                        <asp:BoundField DataField="Nome do Produto" HeaderText="Nome do Produto" />
                        <asp:BoundField DataField="Qtde" HeaderText="Qtde" ItemStyle-HorizontalAlign="Center" />
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
                <div class="clear pull-right" style="margin-right: 3px; width: 300px; text-align: right; margin-bottom: 4px;">
                    <label>Total de Produto:</label>
                    <asp:Label ID="lblTotalProdutoColuna" runat="server" Text="Label"></asp:Label>
                </div>
            </asp:Panel>
            <asp:Panel ID="pnlServicoInserido" runat="server" CssClass="row">

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
                <div class="clear pull-right" style="margin-right: 3px; width: 300px; text-align: right; margin-bottom: 4px;">
                    <label class="control-label" for="txtPrecoProduto">Total de Serviços:</label>
                    <asp:Label ID="lblTotalServicoColuna" runat="server" Text="Label"></asp:Label>
                </div>
            </asp:Panel>
            <div class="row">
                <div class="clear pull-right col-md-5" style="margin-right: -13px; width: 300px; text-align: right;">
                    <label style="font-size: 13px;">Subtotal:</label>
                    <asp:Label ID="lblTotalOS" runat="server" CssClass="font"></asp:Label>
                </div>
            </div>
            <div class="row" id="divDesconto" runat="server">
                <div class="clear pull-right col-md-5" style="margin-right: -13px; width: 300px; text-align: right;">
                    <label style="font-size: 13px;">Desconto:</label>
                    <asp:Label ID="lblDesconto" runat="server" CssClass="font"></asp:Label>
                </div>
            </div>
            <div class="row">
                <div class="clear pull-right col-md-5" style="margin-right: -13px; width: 300px; text-align: right;">
                    <label style="font-size: 13px;">Total à Pagar:</label>
                    <asp:Label ID="lblTotalComDesconto" runat="server" CssClass="font"></asp:Label>
                </div>
            </div>
        </div>  
       

    </form>
</body>
</html>
