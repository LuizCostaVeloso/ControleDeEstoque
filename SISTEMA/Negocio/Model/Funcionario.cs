using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.Model
{
    /// <summary>
    /// A classe Funcionário é responsável por criar espaços no disco quando instanciada para manipulação
    /// das informações inerentes a este, que vão ser adicionadas conforme os nomes dos
    /// seus atributos.
    /// </summary>
    public class Funcionario
    {
                         
        #region ATRIBUTOS
        /// <summary>
        /// Cargo = aqui será definido pelo usuário o cargo que o funcionário exercera na
        /// empresa.
        /// </summary>
        private Cargo cargo;
        /// <summary>
        /// FuncinarioID = este ficará responsável por identificar unicamente os
        /// funcionários desta classe.
        /// </summary>
        private int funcionarioID;
        /// <summary>
        /// Bairro = aqui será inserido pelo usuário o nome do bairro onde o funcionário
        /// mora.
        /// </summary>
        private string bairro;
        /// <summary>
        /// Categoria = aqui será inserida a categoria o qual o funcionário pertence
        /// definido em sua habilitação.
        /// </summary>
        private string categoria;
        /// <summary>
        /// Nome = aqui será inserido pelo usuário o nome do novo funcionário da empresa.
        /// </summary>
        private string nome;
        /// <summary>
        /// CEP = aqui será inserido o cep da rua em que o funcionário mora.
        /// </summary>
        private string cep;
        /// <summary>
        /// CPF = aqui será inserido o numero de cadastro de pessoa física do funcionário.
        /// </summary>
        private string cpf;
        /// <summary>
        /// Data de Ativado = aqui será inserida a data em que o funcionário foi registrado no sistema.
        /// </summary>
        private DateTime dataAtivado;
        /// <summary>
        /// Data de desativação = aqui será inserid a data em que o funcionário passou a não exercer mais a sua função na empresa.
        /// </summary>
        private Nullable<DateTime> dataDesativado;
        /// <summary>
        /// Data de Nascimento = campo em que será inserido a data em que o funcionário
        /// nasceu.
        /// </summary>
        private DateTime dataNascimento;
        /// <summary>
        /// Endereço = aqui será inserido o nome da rua e o numero da casa do funcionário.
        /// </summary>
        private string endereco;
        /// <summary>
        /// Telefone = será aqui o numero do telefone do funcionário.
        /// </summary>
        private string telefone1;
        /// <summary>
        /// Telefone celular 1 = será inserido aqui o numero do telefone celular
        /// funcionário.
        /// </summary>
        private string telefone2;       
        /// <summary>
        /// Observação = aqui será inserida qualquer observação necessária sobre o
        /// funcionário.
        /// </summary>
        private string observacao;
        /// <summary>
        /// RG = aqui será inserido o registro único do funcionário.
        /// </summary>
        private string rg;
        /// <summary>
        /// Órgão expedidor = aqui o usuário irá inserir o órgão que expediu o registro
        /// único.
        /// </summary>
        private string orgaoEmissor;
        /// <summary>
        /// Habilitação = aqui será inserido o numero da habilitação do funcionário.
        /// </summary>
        private string habilitacao;
        /// <summary>
        /// Senha = aqui será definido uma senha para os funcionários que tiver que
        /// interagir com o sistema.
        /// </summary>
        private string senha;
        /// <summary>
        /// Login = aqui será criado o login de acesso que o funcionário irá utilizar para
        /// acessar o sistema.
        /// </summary>
        private string login;
        /// <summary>
        /// Complemento  =  será inserido qualquer complemento do endereço do funcionário.
        /// </summary>
        private string complemento;
        /// <summary>
        /// Login2 = atributo usado para comparação de usuário para saber se o mesmo digitou o usuário correto desejado.
        /// </summary>
        private string login2;
        /// <summary>
        /// Senha2 = atributo usado para comparação de senha para saber se o mesmo digitou a senha correta desejado.
        /// </summary>
        private string senha2;


        #endregion

        #region PROPRIEDADES  
        
        public string _Senha2
        {
            get { return senha2; }
            set { senha2 = value; }
        }
        public string _Login2
        {
            get { return login2; }
            set { login2 = value; }
        }

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

        public int _FuncionarioID
        {
            get { return funcionarioID; }
            set { funcionarioID = value; }
        }

        public string _Bairro
        {
            get { return bairro; }
            set { bairro = value; }
        }

        public Cargo _Cargo
        {
            get { return cargo; }
            set { cargo = value; }
        }

        public string _Categoria
        {
            get { return categoria; }
            set { categoria = value; }
        }

        public string _Nome
        {
            get { return nome; }
            set { nome = value; }
        }

        public string _Cep
        {
            get { return cep; }
            set { cep = value; }
        }

        public string _Cpf
        {
            get { return cpf; }
            set { cpf = value; }
        }

        public DateTime _DataAtivado
        {
            get { return dataAtivado; }
            set { dataAtivado = value; }
        }

        public Nullable<DateTime> _DataDesativado
        {
            get { return dataDesativado; }
            set { dataDesativado = value; }
        }

        public DateTime _DataNascimento
        {
            get { return dataNascimento; }
            set { dataNascimento = value; }
        }

        public string _Endereco
        {
            get { return endereco; }
            set { endereco = value; }
        }

        public string _Observacao
        {
            get { return observacao; }
            set { observacao = value; }
        }

        public string _Rg
        {
            get { return rg; }
            set { rg = value; }
        }

        public string _OrgaoEmissor
        {
            get { return orgaoEmissor; }
            set { orgaoEmissor = value; }
        }

        public string _Habilitacao
        {
            get { return habilitacao; }
            set { habilitacao = value; }
        }

        public string _Senha
        {
            get { return senha; }
            set { senha = value; }
        }

        public string _Login
        {
            get { return login; }
            set { login = value; }
        }

        public string _Complemento
        {
            get { return complemento; }
            set { complemento = value; }
        }

        #endregion

    }//end Funcionario

}//end namespace Model