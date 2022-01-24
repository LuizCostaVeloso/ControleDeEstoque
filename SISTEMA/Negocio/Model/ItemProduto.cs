using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.Model
{
    /// <summary>
    /// A classe ItemProduto é responsável por criar espaços no disco quando instanciada para manipulação
    /// das informações inerentes a este, que vão ser adicionadas conforme os nomes dos
    /// seus atributos. Também ficará responsável em processar os serviços criando uma
    /// lista conforme a inserção de quantidades.
    /// </summary>
    public class ItemProduto
    {
        #region ATRIBUTOS

        /// <summary>
        /// Este ficara responsável pelo identificador únicamente o item de produto.
        /// </summary>
        private int itemProdutoID;
        /// <summary>
        /// Campo que será inserido a quantidade do produto a ser vendida.
        /// </summary>
        private int quantidadeItem;
        /// <summary>
        ///Total = aqui será inserido automaticamente o total gerado de quantidade de produto e preço unitário.
        /// </summary>
        private decimal total;
        /// <summary>
        ///Preço = Campo que será responsável por receber 
        ///automaticamente o preço do produto que estará vindo do banco de dados.
        /// </summary>
        private decimal preco;
        /// <summary>
        ///Este é responsável por criar o objeto do tipo Produto.
        /// </summary>
        private Produto objProduto;
        /// <summary>
        ///Este é responsável por ligar a ordem de serviço com o produto.  
        ///</summary>
        private OrdemServico ordemServico;

        #endregion

        #region METODOS

        /// <summary>
        /// Método construtor que representa a composição entre item de Produto e ordem de serviço ligando à ordem de serviço a classe Produto estanciando o Produto.
        /// </summary>
        /// <param name="OS">Parâmetro que passa ao método construtor item de Produto a ordem de serviço a qual os Proutos pertencem.</param>
        public ItemProduto(OrdemServico OS)
        {
            objProduto = new Produto();
            _OrdemServico = OS;
        }

        #endregion

        #region PROPRIEDADES

        public OrdemServico _OrdemServico
        {
            get { return ordemServico; }
            set { ordemServico = value; }
        }

        public decimal _Total
        {
            get { return total; }
            set { total = value; }
        }

        public decimal _Preco
        {
            get { return preco; }
            set { preco = value; }
        }

        public Produto _ObjProduto
        {
            get { return objProduto; }
            set { objProduto = value; }
        }

        public int _ItemProdutoID
        {
            get { return itemProdutoID; }
            set { itemProdutoID = value; }
        }

        public int _QuantidadeItem
        {
            get { return quantidadeItem; }
            set { quantidadeItem = value; }
        }
        #endregion

    }//end ItemProduto

}//end namespace Model