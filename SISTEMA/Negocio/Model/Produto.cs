using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.Model
{
    /// <summary>
    /// A classe Produto é responsável por criar espaços no disco quando instanciada para manipulação das
    /// informações inerentes a este, que vão ser adicionadas conforme os nomes dos
    /// seus atributos.
    /// </summary>
    public class Produto
    {
        
       #region ATRIBUTOS

        /// <summary>
        /// Produto ID = este ficará responsável por identificar unicamente todos os
        /// produtos desta classe.
        /// </summary>
        private int produtoID;
        /// <summary>
        /// Nome do produto = será inserido aqui o nome do produto.
        /// </summary>
        private string nomeProduto;
        /// <summary>
        /// Nome do fornecedor = será inserido aqui o nome do fornecedor que forneceu o
        /// produto.
        /// </summary>
        private string nomeFornecedor;
        /// <summary>
        /// Estoque mínimo = o usuário definira aqui um numero mínimo do produto em estoque.
        /// </summary>
        private int estoqueMinimo;
        /// <summary>
        /// Estoque = fornecerá automaticamente para o usuário a quantidade do
        /// produto que estiver em estoque no momento da inserção.
        /// </summary>
        private int estoque;
        /// <summary>
        /// Entrada de estoque = será inserido a nova quantidade de produtos que foi comprada
        /// para loja.
        /// </summary>
        private int entradaEstoque;
        /// <summary>
        /// Marca e Modelo = será definida a marca e o modelo do produto.
        /// </summary>
        private string marcaModelo;
        /// <summary>
        ///Preço de compra = será inserido aqui o valor que o produto custou para empresa.
        /// </summary>
        private decimal precoCompra;       
        /// <summary>
        /// Preço de venda = aqui será inserido automaticamente o valor que custara para o
        /// cliente o produto.
        /// </summary>
        private decimal precoVenda;
        /// <summary>
        /// Telefone do fornecedor = será inserido aqui o numero do telefone do fornecedor
        /// para eventual contato com o mesmo.
        /// </summary>
        private string telefoneFornecedor;
        /// <summary>
        ///Observação = aqui será inserida qualquer observação inerente ao produto.
        /// </summary>
        private string observacao;
       

        #endregion

        #region Propriedades       
        /// <summary>
        /// Produto ID = este ficará responsável por identificar unicamente todos os
        /// produtos desta classe.
        /// </summary>
        public int _ProdutoID
        {
            get
            {
                return produtoID;
            }
            set
            {
                produtoID = value;
            }
        }
        /// <summary>
        /// Nome do produto = será inserido aqui o nome do produto.
        /// </summary>
        public string _NomeProduto
        {
            get
            {
                return nomeProduto;
            }
            set
            {
                nomeProduto = value;
            }
        }
        /// <summary>
        /// Nome do fornecedor = será inserido aqui o nome do fornecedor que forneceu o
        /// produto.
        /// </summary>
        public string _NomeFornecedor
        {
            get
            {
                return nomeFornecedor;
            }
            set
            {
                nomeFornecedor = value;
            }
        }
        /// <summary>
        /// Estoque mínimo = o usuário definira aqui um numero mínimo do produto em estoque.
        /// </summary>
        public int _EstoqueMinimo
        {
            get
            {
                return estoqueMinimo;
            }
            set
            {
                estoqueMinimo = value;
            }
        }
        /// <summary>
        /// Estoque = fornecerá automaticamente para o usuário a quantidade do
        /// produto que estiver em estoque no momento da inserção.
        /// </summary>
        public int _Estoque
        {
            get
            {
                return estoque;
            }
            set
            {
                estoque = value;
            }
        }
        /// <summary>
        /// Entrada de estoque = será inserido a nova quantidade de produtos que foi comprada
        /// para loja.
        /// </summary>
        public int _EntradaEstoque
        {
            get
            {
                return entradaEstoque;
            }
            set
            {
                entradaEstoque = value;
            }
        }
        /// <summary>
        /// Marca e Modelo = será definida a marca e o modelo do produto.
        /// </summary>
        public string _MarcaModelo
        {
            get
            {
                return marcaModelo;
            }
            set
            {
                marcaModelo = value;
            }
        }
        /// <summary>
        ///Preço de compra = será inserido aqui o valor que o produto custou para empresa.
        /// </summary>
        public decimal _PrecoCompra
        {
            get
            {
                return precoCompra;
            }
            set
            {
                precoCompra = value;
            }
        }
        /// <summary>
        /// Preço de venda = aqui será inserido automaticamente o valor que custara para o
        /// cliente o produto.
        /// </summary>
        public decimal _PrecoVenda
        {
            get
            {
                return precoVenda;
            }
            set
            {
                precoVenda = value;
            }
        }
        /// <summary>
        /// Telefone do fornecedor = será inserido aqui o numero do telefone do fornecedor
        /// para eventual contato com o mesmo.
        /// </summary>
        public string _TelefoneFornecedor
        {
            get
            {
                return telefoneFornecedor;
            }
            set
            {
                telefoneFornecedor = value;
            }
        }
        /// <summary>
        ///Observação = aqui será inserida qualquer observação inerente ao produto.
        /// </summary>
        public string _Observacao
        {
            get { return observacao; }
            set { observacao = value; }
        } 
        #endregion

    }//end Produto

}//end namespace Model
