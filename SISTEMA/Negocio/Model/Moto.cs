using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.Model
{
    /// <summary>
    /// A classe Moto é responsável por criar espaços no disco quando instanciada para manipulação das
    /// informações inerentes a este, que vão ser adicionadas conforme os nomes dos
    /// seus atributos.
    /// </summary>
    public class Moto
    {
        #region ATRIBUTOS
        /// <summary>
        /// Moto ID = este ficará responsável por identificar unicamente todas as motos
        /// desta classe.
        /// </summary>
        private int motoID;
        /// <summary>
        /// Placa = será inserida aqui o numero da placa da moto.
        /// </summary>
        private string placa;
        /// <summary>
        /// Chassi = será inserido aqui o numero do chassi da moto.
        /// </summary>
        private string chassi;
        /// <summary>
        /// Marca Modelo = será inserida aqui o modelo e marca da moto.
        /// </summary>
        private string marcaModelo;
        /// <summary>
        /// Ano de fabricação = será inserido aqui o ano em que a moto foi fabricada a moto.
        /// </summary>
        private string anoFabricacao;
        /// <summary>
        /// Cor predominante = a cor da moto que mais predomina definida no documento da
        /// moto.
        /// </summary>
        private string corPredominante;
        /// <summary>
        /// Observação = aqui será inserida qualquer observação necessária sobre a moto.
        /// </summary>
        private string observacao;
        #endregion       

        #region PROPRIEDADES       

        public int _MotoID
        {
            get
            {
                return motoID;
            }
            set
            {
                motoID = value;
            }
        }

        public string _Placa
        {
            get
            {
                return placa;
            }
            set
            {
                placa = value;
            }
        }

        public string _Chassi
        {
            get
            {
                return chassi;
            }
            set
            {
                chassi = value;
            }
        }

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

        public string _AnoFabricacao
        {
            get
            {
                return anoFabricacao;
            }
            set
            {
                anoFabricacao = value;
            }
        }

        public string _CorPredominante
        {
            get
            {
                return corPredominante;
            }
            set
            {
                corPredominante = value;
            }
        }

        public string _Observacao
        {
            get
            {
                return observacao;
            }
            set
            {
                observacao = value;
            }
        }

        #endregion

    }//end Moto
}
