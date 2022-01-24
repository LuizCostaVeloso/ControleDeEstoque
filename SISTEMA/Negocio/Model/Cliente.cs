using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.Model
{
    /// <summary>
    /// A classe Cliente é responsável por criar espaços no disco quando instanciada para manipulação das
    /// informações inerentes a este, que vão ser adicionadas conforme os nomes dos
    /// seus atributos.
    /// </summary>
    public class Cliente
    {
        #region ATRIBUTOS
        /// <summary>
        /// Cliente ID = este ficará responsável por identificar unicamente os clientes
        /// desta classe.
        /// </summary>
        private int clienteID;
        /// <summary>
        /// Nome = aqui será inserido pelo usuário o nome do novo funcionário da empresa.
        /// </summary>
        private string nome;
        /// <summary>
        /// Tipo pessoa = define se pessoa é do tipo física ou jurídica.
        /// </summary>
        private int tipoPessoa;
        /// <summary>
        /// CPF = aqui será inserido o numero de cadastro de pessoa física do cliente.
        /// </summary>
        private string cpfCnpj;        
        /// <summary>
        /// Sexo = será identificado aqui pelo usuário o sexo do cliente.
        /// </summary>
        private Sexo sexo;
        /// <summary>
        /// Data de Cadastro = aqui será inserida a data em que foi criada a cadastro do
        /// cliente.
        /// </summary>
        private DateTime dataCadastro;
        /// <summary>
        /// Endereço = aqui será inserido o nome da rua e o numero da casa do cliente.
        /// </summary>
        private string endereco;
        /// <summary>
        /// CEP = aqui será inserido o cep da rua em que o cliente mora.
        /// </summary>
        private string cep;
        /// <summary>
        /// Bairro = aqui será inserido pelo usuário o nome do bairro onde o cliente mora.
        /// </summary>
        private string bairro;
        /// <summary>
        /// Complemento = será inserido qualquer complemento do endereço do cliente.
        /// </summary>
        private string complemento;
        /// <summary>
        /// Cidade = aqui será inserida a cidade em que o cliente mora.
        /// </summary>
        private string cidade;
        /// <summary>
        /// Uf = aqui será inserido o estado em que o cliente mora.
        /// </summary>
        private Uf uf;
        /// <summary>
        /// Telefone fixo = será aqui o numero do telefone do cliente.
        /// </summary>
        private string telefone1;        
        /// <summary>
        /// Telefone celular 2 = será inserido aqui o numero do telefone cliente.
        /// </summary>
        private string telefone2;
        /// <summary>
        /// E-mail = será inserido aqui o e-mail do cliente.
        /// </summary>
        private string email;
        /// <summary>
        /// Observação = aqui será inserida qualquer observação necessária sobre o cliente. 
        /// </summary>
        private string observacao;
        /// <summary>
        /// RG = aqui será inserido o registro único do cliente.
        /// </summary>
        private string rg;
        /// <summary>
        /// Órgão expedidor = aqui o usuário irá inserir o órgão que expediu o registro
        /// único.
        /// </summary>
        private string orgaoEmissor;     

        #endregion
        //---------------------------------///-------------------------///---------------------------------///
        #region PROPRIDADES


        public string _Telefone2
        {
            get { return telefone2; }
            set { telefone2 = value; }
        }

        public string _Telefone1
        {
            get { return telefone1; }
            set { telefone1 = value; }
        }
        public int _ClienteID
        {
            get { return clienteID; }
            set { clienteID = value; }
        }

        public string _Nome
        {
            get { return nome; }
            set
            { nome = value; }
        }

        public int _TipoPessoa
        {
            get
            {
                return tipoPessoa;
            }
            set
            {
                tipoPessoa = value;
            }
        }

        public string _CpfCnpj
        {
            get
            {
                return cpfCnpj;
            }
            set
            {
                cpfCnpj = value;
            }
        }       

        public Sexo _Sexo
        {
            get
            {
                return sexo;
            }
            set
            {
                sexo = value;
            }
        }

        public DateTime _DataCadastro
        {
            get
            {
                return dataCadastro;
            }
            set
            {
                dataCadastro = value;
            }
        }

        public string _Endereco
        {
            get
            {
                return endereco;
            }
            set
            {
                endereco = value;
            }
        }

        public string _Cep
        {
            get
            {
                return cep;
            }
            set
            {
                cep = value;
            }
        }

        public string _Bairro
        {
            get
            {
                return bairro;
            }
            set
            {
                bairro = value;
            }
        }

        public string _Complemento
        {
            get
            {
                return complemento;
            }
            set
            {
                complemento = value;
            }
        }

        public string _Cidade
        {
            get
            {
                return cidade;
            }
            set
            {
                cidade = value;
            }
        }

        public Uf _Uf
        {
            get
            {
                return uf;
            }
            set
            {
                uf = value;
            }
        }

        public string _Email
        {
            get
            {
                return email;
            }
            set
            {
                email = value;
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

        public string _Rg
        {
            get
            {
                return rg;
            }
            set
            {
                rg = value;
            }
        }

        public string _OrgaoEmissor
        {
            get
            {
                return orgaoEmissor;
            }
            set
            {
                orgaoEmissor = value;
            }
        }        

        #endregion

    }//end Cliente
}