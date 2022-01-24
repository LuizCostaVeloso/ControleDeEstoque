using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.Model
{
    /// <summary>
    /// A classe Serviço é responsável por criar espaços no disco quando instanciada para manipulação das
    /// informações inerentes a este, que vão ser adicionadas conforme os nomes dos
    /// seus atributos.
    /// </summary>
    public class Servico
    {
        //---------------------------------///-------------------------///---------------------------------///
        #region ATRIBUTOS

        /// <summary>
        /// Nome do Serviço = Campo que será inserido o nome do serviço que a loja irá disponibilizar aos
        /// clientes.
        /// </summary>
        private string nomeServico;
        /// <summary>
        /// Serviço ID = Este ficara responsável pelo identificador único dos serviços
        /// </summary>
        private int servicoID;
        /// <summary>
        ///Preço = Campo que será inserido o preço que custará o serviço.
        /// </summary>
        private decimal preco;
        /// <summary>
        /// Observação = aqui será inserida qualquer observação necessária que diz respeito a serviço.
        /// </summary>
        private string observacao;
        /// <summary>
        /// Marca e Modelo = será definido aqui para qual a marca e modelo de moto é aquele serviço.
        /// </summary>
        private string marcaModelo; 
        #endregion
        //---------------------------------///-------------------------///---------------------------------///
        #region PROPRIEDADES 
        /// <summary>
        /// Marca e Modelo = será definido aqui para qual a marca e modelo de moto é aquele serviço.
        /// </summary>
        public string _MarcaModelo
        {
            get { return marcaModelo; }
            set { marcaModelo = value; }
        }  
        /// <summary>
        /// Nome do Serviço = Campo que será inserido o nome do serviço que a loja irá disponibilizar aos
        /// clientes.
        /// </summary>
        public string _NomeServico
        {
            get
            {
                return nomeServico;
            }
            set
            {
                nomeServico = value;
            }
        }  
        /// <summary>
        /// Serviço ID = Este ficara responsável pelo identificador único dos serviços
        /// </summary>
        public int _ServicoID
        {
            get
            {
                return servicoID;
            }
            set
            {
                servicoID = value;
            }
        }
        /// <summary>
        ///Preço = Campo que será inserido o preço que custará o serviço.
        /// </summary>
        public decimal _Preco
        {
            get
            {
                return preco;
            }
            set
            {
                preco = value;
            }
        }
        /// <summary>
        /// Observação = aqui será inserida qualquer observação necessária que diz respeito a serviço.
        /// </summary>
        public string _Observacao
        {
            get { return observacao; }
            set { observacao = value; }
        } 
        #endregion 
    }//end Servico  
}//end namespace Model