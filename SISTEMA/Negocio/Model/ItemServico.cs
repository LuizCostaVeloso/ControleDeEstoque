using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.Model
{

    /// <summary>
    /// A classe ItemServico é responsável por criar espaços no disco quando instanciada para manipulação
    /// das informações inerentes a este, que vão ser adicionadas conforme os nomes dos
    /// seus atributos. Também ficará responsável em processar os serviços criando uma
    /// lista conforme a inserção de quantidades.
    /// </summary>
    public class ItemServico
    {
        #region ATRIBUTOS
        /// <summary>
        /// Item Serviço ID = Este ficara responsável pelo identificador único do item de Serviço. 
        /// </summary>
        private int itemServicoID;
        /// <summary>
        ///Item Preço = Este é responsável por armazenar o preço do produto que está vindo da classe Produto.
        /// </summary>
        private decimal preco;
        /// <summary>
        //Este é responsável por criar o objeto da classe Serviço.
        /// </summary>
        private Servico servico;
        /// <summary>
        //Este é responsável ligar a Ordem de Serviço ao Item de Serviço.
        /// </summary>
        private OrdemServico ordemServico;

        #endregion

        #region METODO
         /// <summary>
        /// Método construtor que representa a composição entre item de serviço e ordem de serviço ligando à ordem de serviço a classe serviço estanciando o serviço.
         /// </summary>
        /// <param name="OS">Parâmetro que passa ao método construtor item de serviço a ordem de serviço a qual os serviços pertencem.</param>
        public ItemServico(OrdemServico OS)
        {
            _OrdemServico = OS;
            servico = new Servico();
        }
        #endregion

        #region PROPRIEDADES

        public decimal _Preco
        {
            get { return preco; }
            set { preco = value; }
        }

        public Servico _Servico
        {
            get { return servico; }
            set { servico = value; }
        }

        public int _ItemServicoID
        {
            get { return itemServicoID; }
            set { itemServicoID = value; }
        }

        public OrdemServico _OrdemServico
        {
            get { return ordemServico; }
            set { ordemServico = value; }
        }

        #endregion

    }//end ItemServico

}//end namespace Model

