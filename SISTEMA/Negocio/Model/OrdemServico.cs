using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.Model
{
    /// <summary>
    /// A classe OrdemServico é responsável por criar espaços no disco quando instanciada para manipulação
    /// das informações inerentes a este, que vão ser adicionadas conforme os nomes dos
    /// seus atributos. Também é responsável em buscar em suas classes agregadas as informações de
    /// cliente, moto, funcionário, produto e serviços que comporão a ordem de serviço.
    /// Está fará processamento e controle dos produtos e serviços.
    /// </summary>
    public class OrdemServico
    {
        #region ATRIBUTOS
        /// <summary>
        /// Ordem de serviço ID = este ficará responsável por identificar unicamente todas
        /// as ordens de serviço.
        /// </summary>
        private int ordemServicoID;
        /// <summary>
        /// Data de abertura da OS = será inserida aqui a data e hora de criação da ordem de
        /// serviço pelo sistema para controle de inicio do serviço.
        /// </summary>
        private DateTime dataAberturaOS;
        /// <summary>
        /// Data de fechamento da OS = será inserida aqui a data e hora em que a ordem de serviço
        /// foi finalizada para controle de tempo final do serviço.
        /// </summary>
        private Nullable<DateTime> dataFechamentoOS;
        /// <summary>
        /// Km = aqui será inserido a numeração atual da quilometragem em que a moto deu
        /// entrada para efetuar algum tipo de serviço na loja.
        /// </summary>
        private string km;
        /// <summary>
        /// Observação = aqui será inserida qualquer observação necessária que diz respeito
        /// aquela ordem de serviço.
        /// </summary>
        private string observacao;
        /// <summary>
        /// Valor total = aqui será colocado automaticamente o valor total dos serviços e
        /// produto sem desconto realizados nesta ordem de serviço para apenas manipulação dos dados.
        /// </summary>
        private decimal valorTotalOS;
        /// <summary>
        /// Valor totalProdutos = aqui será colocado automaticamente o valor total dos 
        /// produtos utilizados na moto nesta ordem de serviço para apenas manipulação dos dados.
        /// </summary>
        private decimal valorTotalProduto;
        /// <summary>
        /// Valor total Serviços = aqui será colocado automaticamente o valor total dos
        /// serviços realizados na moto nesta ordem de serviço para apenas manipulação dos dados.
        /// </summary>
        private decimal valorTotalServicos;
        /// <summary>
        /// Desconto = será inserido o valor de desconto do produto se houver.
        /// </summary>
        private Nullable<decimal> desconto;
        /// <summary>
        ///Total da ordem de Serviço com o Desconto = Este é responsável por receber o valor 
        ///a ser pago após desconto dado no total da ordem de serviço para apenas manipulação dos dados...
        /// </summary>
        private decimal totalOSdesconto;
        /// <summary>
        ///Tipo de Desconto = Este é responsável por definir se o desconto será feito
        ///por em espécie ou em porcentagem.
        /// </summary>
        private Nullable<int> tipoDesconto;

        /// <summary>
        /// Lista de Item de Produto = será lido aqui todos os produtos inseridos pertencente a uma ordem de serviço especifico
        /// </summary>
        private IList<ItemProduto> listaItemProduto; 
        /// <summary>
        //Moto = responsável por criar um objeto do tipo Moto para armazenamento de todas as Informações inerentes a esta.
        /// </summary>
        private Moto moto;
        /// <summary>
        ///Cliente = responsável por criar um objeto do tipo Cliente para armazenamento de todas as Informações inerentes a este.
        /// </summary>
        private Cliente cliente;
        /// <summary>
        ///Funcionário = responsável por criar um objeto do tipo Funcionário para armazenamento de todas as Informações inerentes a este.
        /// </summary>
        private Funcionario funcionario;
        /// <summary>
        /// Lista de Item de Serviço = será lido aqui todos os serviços inseridos pertencente a uma ordem de serviço especifico
        /// </summary>
        private IList<ItemServico> listaItemServico;



        #endregion

        #region METODOS
        /// <summary>
        /// Ordem de Serviço = Método construtor que agrega ao objeto ordem de serviços todas as informações pertencentes a uma ordem de serviço.
        /// </summary>
        public OrdemServico()
        {
            moto = new Moto();
            cliente = new Cliente();
            funcionario = new Funcionario();
            listaItemServico = new List<ItemServico>();
            listaItemProduto = new List<ItemProduto>();

        }

        #endregion

        #region PROPRIEDADES

        public IList<ItemProduto> _ListaItemProduto
        {
            get { return listaItemProduto; }
            set { listaItemProduto = value; }
        }

        public IList<ItemServico> _ListaItemServico
        {
            get { return listaItemServico; }
            set { listaItemServico = value; }
        }

        public decimal _TotalOSdesconto
        {
            get { return totalOSdesconto; }
            set { totalOSdesconto = value; }
        }

        public Funcionario _Funcionario
        {
            get { return funcionario; }
            set { funcionario = value; }
        }

        public Cliente _Cliente
        {
            get { return cliente; }
            set { cliente = value; }
        }

        public Moto _Moto
        {
            get { return moto; }
            set { moto = value; }
        }

        public int _OrdemServicoID
        {
            get { return ordemServicoID; }
            set { ordemServicoID = value; }
        }

        public DateTime _DataAberturaOS
        {
            get { return dataAberturaOS; }
            set { dataAberturaOS = value; }
        }

        public Nullable<DateTime> _DataFechamentoOS
        {
            get { return dataFechamentoOS; }
            set { dataFechamentoOS = value; }
        }

        public string _Km
        {
            get { return km; }
            set { km = value; }
        }

        public string _Observacao
        {
            get { return observacao; }
            set { observacao = value; }
        }

        public decimal _ValorTotal
        {
            get { return valorTotalOS; }
            set { valorTotalOS = value; }
        }

        public decimal _ValorTotalProduto
        {
            get { return valorTotalProduto; }
            set { valorTotalProduto = value; }
        }


        public decimal _ValorTotalServicos
        {
            get { return valorTotalServicos; }
            set { valorTotalServicos = value; }
        }


        public Nullable<decimal> _Desconto
        {
            get { return desconto; }
            set { desconto = value; }
        }

        public Nullable<int> _TipoDesconto
        {
            get { return tipoDesconto; }
            set { tipoDesconto = value; }
        }

        #endregion

    }//end OrdemServico

}//end namespace Model
