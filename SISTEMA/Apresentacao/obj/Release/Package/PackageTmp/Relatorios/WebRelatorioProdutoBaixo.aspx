<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebRelatorioProdutoBaixo.aspx.cs" Inherits="Apresentacao.Relatorios.WebRelatorioProdutoBaixo" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
     <title>Mix Motos</title>
    <link href="../css/bootstrap.css" rel="stylesheet" />
    <link href="../css/Impressao.css" rel="stylesheet" />
    </head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <p style="color:#2A6496">Relatório de Produto Baixo</p>
            <asp:GridView ID="gvListaProduto" runat="server" AutoGenerateColumns="False" 
                DataKeyNames="ProdutoID" CssClass="table table-hover table-striped " CellPadding="4" ForeColor="#333333" Width="100%">
                <AlternatingRowStyle BackColor="White" />
                <Columns>
                    <asp:BoundField DataField="ProdutoID" HeaderText="ProdutoID" Visible="false" />
                    <asp:BoundField DataField="Nome do Produto" HeaderText="Nome do Produto" />
                    <asp:BoundField DataField="Marca/Modelo" HeaderText="Marca/Modelo"/>
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
    </form>
</body>
</html>
