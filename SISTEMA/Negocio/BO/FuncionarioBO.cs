using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Negocio.Model;
using Negocio.DAO;

namespace Negocio.BO
{

    /// <summary>
    /// Classe da camada BO responsável pela Regra de negócio.
    /// </summary>
    public class FuncionarioBO
    {
        #region ASSINATURA DOS METODOS, CRIAÇÃO DE OBJETO E CRIAÇÃO DE VARIÁVEL.
        /// <summary>
        /// Cria o objeto para reservar um espaço no disco após instanciado.
        /// </summary>
        static Funcionario objFuncionario;
        /// <summary>
        /// Cria um objeto da camada DAO para dar acesso aos metodos inerente da Classe.
        /// </summary>
        FuncionarioDAO objFuncionarioDAO;
        /// <summary>
        /// Cria uma lista do Tipo da Classe. 
        ///</summary>
        IList<Funcionario> listFuncionario;
        /// <summary>
        /// Cria as variaveis.
        /// </summary>
        string id, dtNascimento, dtAtivado, habilitacao, dtDesativado;
        int numInt;
        DateTime data;
        //todo TAREFAS pesquisar metodos que consiga pegar parte de um nome de pessoa para gravar no usuário
        #endregion

        #region CRUD
        /// <summary>
        ///   Método responsável em gravar ou alterar a senha do usuário. 
        /// </summary>
        /// <param name="objFuncionario">Parâmetro que recebe as informações do funcionário.</param>
        /// <param name="cargo">Parâmetro que recebe o cargo do funcionário.</param>
        public void GravarSenha(Funcionario objFuncionario, string cargo)
        {
            try
            {
                id = objFuncionario._FuncionarioID.ToString();
                dtDesativado = objFuncionario._DataDesativado.ToString();

                if ((objFuncionario._Senha == "") || (objFuncionario._Senha2 == ""))
                    throw new Exception("O campos Senha é obrigatório!!!");
                if (objFuncionario._Senha != objFuncionario._Senha2)
                    throw new Exception("Os Campos da Senha não são iguais, digite novamente!!!");
                if (!DateTime.TryParse(dtDesativado, out data))
                    objFuncionario._DataDesativado = null;
                objFuncionarioDAO = new FuncionarioDAO();
                if (int.TryParse(id, out numInt) && (objFuncionario._FuncionarioID != 0) && (cargo == "GERENTE"))
                {
                    if ((objFuncionario._Login == "") || (objFuncionario._Login2 == ""))
                        throw new Exception("O campo do usuário é obrigatório!!!");
                    if (objFuncionario._Login != objFuncionario._Login2)
                        throw new Exception("Os Campos de usuário não são iguais, digite novamente!!!");
                    Funcionario objFunc = new Funcionario();
                    objFunc = objFuncionarioDAO.BuscarFuncionarioLogin(objFuncionario);
                    if ((objFunc != null) && (objFunc._FuncionarioID != objFuncionario._FuncionarioID))
                        throw new Exception("Usuário já existe no sistema!!!");

                    objFuncionarioDAO.GravarSenhaLogin(objFuncionario);

                }
                else
                {
                    Funcionario objFunc = new Funcionario();
                    objFunc = objFuncionarioDAO.BuscarFuncionarioLogin(objFuncionario);
                    if ((objFunc != null) && (objFunc._FuncionarioID != objFuncionario._FuncionarioID))
                        throw new Exception("Usuário já existe no sistema!!!");
                    objFuncionarioDAO.GravarSenhaLogin(objFuncionario);
                }
            }
            catch (Exception erro)
            {
                throw new Exception(erro.Message);
            }
        }
        /// <summary>
        /// Método responsável em preparar os dados para gravar ou alterar enviando para a camada DAO. 
        /// </summary>
        /// <param name="objFuncionario">Parâmetro que recebe as informações do Funcionário.</param>
        public void Gravar(Funcionario objFuncionario)
        {
            try
            {
                dtAtivado = objFuncionario._DataAtivado.ToString();
                dtNascimento = objFuncionario._DataNascimento.ToString();
                habilitacao = objFuncionario._Habilitacao;
                id = objFuncionario._FuncionarioID.ToString();
                dtDesativado = objFuncionario._DataDesativado.ToString();


                if (!DateTime.TryParse(dtNascimento, out data))
                    throw new Exception("Campo data Nascimento está vazio ou não é uma data válida. ex: 31/12/2013!!!");
                if ((objFuncionario._Nome.Where(c => char.IsNumber(c)).Count() > 0))
                    throw new Exception("Digite apenas Letras no Campo Nome!!!");
                if (String.IsNullOrEmpty(objFuncionario._Nome))
                    throw new Exception("Digite o Nome do Funcionário!!!");
                if (objFuncionario._Cargo == 0)
                    throw new Exception("Selecione o Cargo do Funcionário!!!");
                if ((objFuncionario._Habilitacao.Where(c => char.IsLetter(c)).Count() > 0))
                    throw new Exception("Digite apenas Números no Campo Habilitação!!!");
                if ((objFuncionario._Categoria.Where(c => char.IsNumber(c)).Count() > 0))
                    throw new Exception("Digite apenas Letras no Campo Categoria!!!");
                if (String.IsNullOrEmpty(objFuncionario._Cpf))
                    throw new Exception("Digite o Número do CPF!!!");
                if ((objFuncionario._Cpf.Where(c => char.IsLetter(c)).Count() > 0))
                    throw new Exception("Digite apenas Números no Campo CPF!!");
                if ((objFuncionario._Rg.Where(c => char.IsLetter(c)).Count() > 0))
                    throw new Exception("O campo RG so aceita números!!!");
                if ((objFuncionario._OrgaoEmissor.Where(c => char.IsNumber(c)).Count() > 0))
                    throw new Exception("Digite apenas Letras no Campo Categoria!!!");
                if ((objFuncionario._Cep.Where(c => char.IsLetter(c)).Count() > 0))
                    throw new Exception("Digite apenas Números no Campo CEP!");
                if (String.IsNullOrEmpty(objFuncionario._Endereco))
                    throw new Exception("Digite o Endereço!!!");
                if (String.IsNullOrEmpty(objFuncionario._Bairro))
                    throw new Exception("Digite o Bairro!!!");
                if (String.IsNullOrEmpty(objFuncionario._Telefone1))
                    throw new Exception("Digite o Número de Telefone!!!");
                if (objFuncionario._Telefone1.Where(c => char.IsLetter(c)).Count() > 0)
                    throw new Exception("Campo Número de Telefone não pode Conter Letras!!!");
                if (objFuncionario._Telefone2.Where(c => char.IsLetter(c)).Count() > 0)
                    throw new Exception("Campo Número de Telefone não pode Conter Letras!!!");
                if (!DateTime.TryParse(dtDesativado, out data))
                    objFuncionario._DataDesativado = null;
                objFuncionarioDAO = new FuncionarioDAO();
                if (int.TryParse(id, out numInt) && objFuncionario._FuncionarioID != 0)
                {
                    try
                    {
                        objFuncionarioDAO.Alterar(objFuncionario);
                    }
                    catch (Exception erro)
                    {

                        throw new Exception("Não foi possível Alterar. erro:" + erro.Message);
                    }
                }
                else
                {
                    try
                    {
                        objFuncionarioDAO.Gravar(objFuncionario);
                    }
                    catch (Exception erro)
                    {

                        throw new Exception("Não foi possível Salvar. erro:" + erro.Message);
                    }


                }
            }
            catch (Exception erro)
            {
                throw new Exception(erro.Message);
            }

        }
        /// <summary>
        ///  Método que recebe um id e chama o método excluir da camada DAO.
        /// </summary>
        /// <param name="objFuncionario">Parâmetro que recebe o id do Funcionário.</param>
        public void Excluir(Funcionario objFuncionario)
        {
            try
            {
                objFuncionarioDAO = new FuncionarioDAO();
                objFuncionarioDAO.Excluir(objFuncionario);
            }
            catch (Exception erro)
            {

                throw new Exception("Não foi possível Excluir. erro:" + erro.Message);
            }
        }

        #endregion

        #region BUSCAS

        /// <summary>
        ///   Método responsável em buscar uma lista de Funcionários.
        /// </summary>
        /// <param name="situacao">Parâmetro que recebe a situação do funcionário se ativo ou inativo.</param>
        /// <param name="nomeFuncionario">Parâmetro que recebe o nome do funcionário.</param>
        /// <returns>Retorna uma lista de funionário do tipo data table.</returns>
        public DataTable BuscarListaFuncionario(string situacao, Funcionario nomeFuncionario)
        {
            try
            {
                objFuncionarioDAO = new FuncionarioDAO();

                DataRow dr;
                DataTable dt = new DataTable();
                listFuncionario = new List<Funcionario>();
                dt.Columns.Add("FuncionarioID");
                dt.Columns.Add("Nome");
                dt.Columns.Add("Cargo");
                dt.Columns.Add("CPF");
                dt.Columns.Add("Telefone");
                dt.Columns.Add("Situação");




                listFuncionario = objFuncionarioDAO.BuscarListaFuncionario(nomeFuncionario);

                // Percorre a lista com o resultado da consulta

                if (listFuncionario != null)
                {
                    foreach (Funcionario objFuncionario in listFuncionario)
                    {
                        dr = dt.NewRow();
                        dr["FuncionarioID"] = objFuncionario._FuncionarioID;
                        dr["Nome"] = objFuncionario._Nome;
                        dr["Cargo"] = objFuncionario._Cargo;
                        dr["CPF"] = objFuncionario._Cpf;
                        dr["Telefone"] = objFuncionario._Telefone1;
                        if (situacao == "Ativos" && objFuncionario._DataDesativado == null)
                        {
                            dr["Situação"] = "Ativo";
                            dt.Rows.Add(dr);
                        }
                        if (situacao == "Inativos" && objFuncionario._DataDesativado != null)
                        {
                            dr["Situação"] = "Desativado";
                            dt.Rows.Add(dr);
                        }
                        if (situacao == "Todos")
                        {
                            if (objFuncionario._DataDesativado == null)
                                dr["Situação"] = "Ativo";
                            else
                                dr["Situação"] = "Desativado";

                            dt.Rows.Add(dr);
                        }




                    }
                }
                else
                    throw new Exception("Funcionário não Cadastrado!");

                return dt;
            }
            catch (Exception erro)
            {

                throw new Exception("Não foi possível fazer a Busca. erro:" + erro.Message);
            }

        }
        /// <summary>
        /// Método responsavel em buscar uma lista de Mecânico.
        /// </summary>
        /// <returns>Retorna os mecânicos ativos.</returns>
        public IList<Funcionario> BuscarListaMecanico()
        {
            objFuncionario = new Funcionario();
            objFuncionario._Cargo = (Cargo)Enum.Parse(typeof(Cargo), "MECANICO");
            objFuncionarioDAO = new FuncionarioDAO();
            IList<Funcionario> listaFuncionario = objFuncionarioDAO.BuscarListaFuncionario(objFuncionario);
            if (listaFuncionario != null)
            {
                foreach (var obj in listaFuncionario)
                {
                    obj._Nome = obj._Nome.TrimStart();
                    int pos1 = obj._Nome.IndexOf(" ");
                    string nome = obj._Nome.Substring(pos1);
                    nome = nome.TrimStart();
                    int pos2 = nome.IndexOf(" ");
                    pos1 += pos2 + 1;
                    obj._Nome = obj._Nome.Substring(0, pos1);

                }
            }
            return listaFuncionario;
        }
        /// <summary>
        ///  Método responsável em buscar um funcionário especifico.
        /// </summary>
        /// <param name="funcionario">Parâmetro que recebe o id do funcionário que deseja buscar.</param>
        /// <returns>Retorna um objeto preenchido com as informações de um funcionário especifico.</returns>
        public Funcionario BuscarFuncionario(Funcionario funcionario)
        {
            try
            {
                objFuncionarioDAO = new FuncionarioDAO();
                objFuncionario = new Funcionario();
                objFuncionario = objFuncionarioDAO.BuscarFuncionario(funcionario);
                return objFuncionario;
            }
            catch (Exception erro)
            {

                throw new Exception("Não foi possível fazer a Busca. erro:" + erro.Message);
            }
        }
        /// <summary>
        ///   Método responsável em verificar se um funcionário com login e senha passado no parâmetro.
        /// </summary>
        /// <param name="funcionario">Parâmetro que recebe o login e senha do funcionário.</param>
        /// <returns>retorna um objeto do tipo funcioário.</returns>
        public Funcionario BuscarLoginSenha(Funcionario funcionario)
        {
            try
            {

                if (String.IsNullOrEmpty(funcionario._Login))
                    throw new Exception("Usuário Obrigatório!!!");
                if (String.IsNullOrEmpty(funcionario._Senha))
                    throw new Exception("Digite sua Senha Atual!!!");
                objFuncionarioDAO = new FuncionarioDAO();
                objFuncionario = new Funcionario();
                objFuncionario = objFuncionarioDAO.BuscarFuncionarioLoginSenha(funcionario);
                if (objFuncionario != null)
                {
                    if (objFuncionario._DataDesativado != null)
                        throw new Exception("Você não tem permissão para acessar o sistema!!!");
                }
                else
                    throw new Exception("Senha atual inválida!!!");
                return objFuncionario;
            }
            catch (Exception erro)
            {
                throw new Exception(erro.Message);
            }
        }


        #endregion


    }
}
