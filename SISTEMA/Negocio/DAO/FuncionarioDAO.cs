using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Negocio.Model;

namespace Negocio.DAO
{
    /// <summary>
    ///Classe DAO, responsável por fazer as persistências na tabela de serviço no banco. 
    /// </summary>
    public class FuncionarioDAO
    {
        #region ASSINATURA DOS METODOS, CRIAÇÃO DE OBJETO E CRIAÇÃO DE VARIÁVEL.
        /// <summary>
        /// Instancia de um novo objeto do tipo StringBuilder.
        /// </summary>
        StringBuilder sql = new StringBuilder();
        /// <summary>
        ///   Instancia de um novo objeto do tipo SqlCommand.
        /// </summary>
        SqlCommand comando = new SqlCommand();
        /// <summary>
        /// Cria uma lista do tipo funcionário.
        /// </summary>
        IList<Funcionario> listaFuncionario;
        /// <summary>
        /// Cria uma variavel do tipo DataTime.
        /// </summary>
        DateTime data;      

        #endregion

        #region CRUD
        /// <summary>
        ///   Método que converte o objeto em linguagem sql e grava no banco as informações.
        /// </summary>
        /// <param name="objFuncionario">Informações são passada no objeto como parâmetro para pessistir no banco.</param>
        public void Gravar(Funcionario objFuncionario)
        {
            try
            {
                sql.Remove(0, sql.Length);
                comando.Parameters.Clear();

                sql.Append("INSERT INTO Funcionario(bairro,cargo,categoria,nome,cep,cpf,dataAtivado,");
                sql.Append("dataNascimento,endereco,telefone1,telefone2,observacao,");
                sql.Append("rg,orgaoEmissor,habilitacao,complemento)");
                sql.Append("values (@bairro,@cargo,@categoria,@nome,@cep,@cpf,@dataAtivado,");
                sql.Append("@dataNascimento,@endereco,@telefone1,@telefone2,@observacao,");
                sql.Append("@rg,@orgaoEmissor,@habilitacao,@complemento)");

                comando.CommandText = sql.ToString();

                comando.Parameters.AddWithValue("@bairro", objFuncionario._Bairro);
                comando.Parameters.AddWithValue("@cargo", objFuncionario._Cargo);
                comando.Parameters.AddWithValue("@categoria", objFuncionario._Categoria);
                comando.Parameters.AddWithValue("@nome", objFuncionario._Nome);
                comando.Parameters.AddWithValue("@cep", objFuncionario._Cep);
                comando.Parameters.AddWithValue("@cpf", objFuncionario._Cpf);
                comando.Parameters.AddWithValue("@dataAtivado", objFuncionario._DataAtivado);               
                comando.Parameters.AddWithValue("@dataNascimento", objFuncionario._DataNascimento);
                comando.Parameters.AddWithValue("@endereco", objFuncionario._Endereco);
                comando.Parameters.AddWithValue("@telefone1", objFuncionario._Telefone1);
                comando.Parameters.AddWithValue("@telefone2", objFuncionario._Telefone2);
                comando.Parameters.AddWithValue("@observacao", objFuncionario._Observacao);
                comando.Parameters.AddWithValue("@rg", objFuncionario._Rg);
                comando.Parameters.AddWithValue("@orgaoEmissor", objFuncionario._OrgaoEmissor);
                comando.Parameters.AddWithValue("@habilitacao", objFuncionario._Habilitacao);
                comando.Parameters.AddWithValue("@complemento", objFuncionario._Complemento);

                ConexaoBanco.Crud(comando);
            }
            catch (Exception erro)
            {
                throw new Exception(erro.Message);
            }

        }
        /// <summary>
        /// Método que converte o objeto em liguagem sql persistindo no banco as informações para alteração.
        /// </summary>
        /// <param name="objFuncionario">Informações são passada no objeto como parâmetro especificando quais informações deseja alterar</param>
        public void Alterar(Funcionario objFuncionario)
        {
            try
            {
                sql.Remove(0, sql.Length);
                comando.Parameters.Clear();
                sql.Append("update Funcionario set nome=@nome, cpf=@cpf,");
                sql.Append(" endereco=@endereco, cep=@cep, bairro=@bairro,");
                sql.Append("complemento=@complemento, telefone1=@telefone1,");
                sql.Append("telefone2=@telefone2,");
                sql.Append("observacao=@observacao, rg=@rg, orgaoEmissor=@orgaoEmissor,");
                sql.Append("cargo=@cargo, categoria=@categoria,dataAtivado=@dataAtivado,dataDesativado=@dataDesativado,");
                sql.Append("habilitacao=@habilitacao, dataNascimento=@dataNascimento where funcionarioID=@funcionarioID");

                comando.CommandText = sql.ToString();

                comando.Parameters.AddWithValue("@nome", objFuncionario._Nome);
                comando.Parameters.AddWithValue("@cpf", objFuncionario._Cpf);
                comando.Parameters.AddWithValue("@endereco", objFuncionario._Endereco);
                comando.Parameters.AddWithValue("@cep", objFuncionario._Cep);
                comando.Parameters.AddWithValue("@bairro", objFuncionario._Bairro);
                comando.Parameters.AddWithValue("@complemento", objFuncionario._Complemento);
                comando.Parameters.AddWithValue("@telefone1", objFuncionario._Telefone1);
                comando.Parameters.AddWithValue("@telefone2", objFuncionario._Telefone2);
                comando.Parameters.AddWithValue("@observacao", objFuncionario._Observacao);
                comando.Parameters.AddWithValue("@rg", objFuncionario._Rg);
                comando.Parameters.AddWithValue("@orgaoEmissor", objFuncionario._OrgaoEmissor);
                comando.Parameters.AddWithValue("@cargo", objFuncionario._Cargo);
                comando.Parameters.AddWithValue("@categoria", objFuncionario._Categoria);
                comando.Parameters.AddWithValue("@dataAtivado", objFuncionario._DataAtivado);
                //todo RECH esta condição confere se o objFuncionario._DataDesativado esta chegando null definido na BO
                //quando a OS sofrer alteração mas ainda não está finalizada? convertendo o null do obj em DBNull valor aceitado no banco.
                //RESPOSTA DO PROFESSOR RECH=> QUE NÃO HAVERIA PROBLEMA EM USAR ESTA CONDIÇÃO            
                if (objFuncionario._DataDesativado == null)
                    comando.Parameters.AddWithValue("@dataDesativado", DBNull.Value);
                else
                    comando.Parameters.AddWithValue("@dataDesativado", objFuncionario._DataDesativado);
                comando.Parameters.AddWithValue("@habilitacao", objFuncionario._Habilitacao);
                comando.Parameters.AddWithValue("@dataNascimento", objFuncionario._DataNascimento);
                comando.Parameters.AddWithValue("@funcionarioID", objFuncionario._FuncionarioID);

                ConexaoBanco.Crud(comando);
            }
            catch (Exception erro)
            {
                throw new Exception(erro.Message);
            }

        }
        public void GravarSenhaLogin(Funcionario objFuncionario)
        {
            try
            {
                sql.Remove(0, sql.Length);
                comando.Parameters.Clear();
                sql.Append("update Funcionario set login=@login,senha=@senha where funcionarioID=@funcionarioID");

                comando.CommandText = sql.ToString();

                comando.Parameters.AddWithValue("@funcionarioID", objFuncionario._FuncionarioID);
                comando.Parameters.AddWithValue("@senha", objFuncionario._Senha);
                comando.Parameters.AddWithValue("@login", objFuncionario._Login);

                ConexaoBanco.Crud(comando);
            }
            catch (Exception erro)
            {
                throw new Exception(erro.Message);
            }
   
        }
         /// <summary>
        ///  Método responsável em fazer a exclusão no banco das informações desejada.
         /// </summary>
        /// <param name="funcionario">objeto preenchido com o id e passado como parâmetro para exclusão no banco das informações especificas.</param>
        public void Excluir(Funcionario funcionario)
        {
            try
            {
                sql.Remove(0, sql.Length);
                comando.Parameters.Clear();

                sql.Append("Delete from Funcionario where funcionarioID = @funcionarioID");

                comando.CommandText = sql.ToString();

                comando.Parameters.AddWithValue("@funcionarioID", funcionario._FuncionarioID);
                ConexaoBanco.Crud(comando);
            }
            catch (Exception erro)
            {
                throw new Exception(erro.Message);
            }

        }

        #endregion

        #region BUSCAS
        /// <summary>
        ///    Método responsável em fazer a busca das informações desejada pelo pelo cargo do Mecânico ou pelo Nome do Funcionário.
        /// </summary>
        /// <param name="funcionario">Nome do cliente ou cargo Mecânico e passado com0 parâmetro dentro do objeto Funcionário.</param>
        /// <returns>Retorna uma lista de Funcionários.</returns>
        public IList<Funcionario> BuscarListaFuncionario(Funcionario funcionario)
        {
            try
            {
                sql.Remove(0, sql.Length);
                comando.Parameters.Clear();

                if (funcionario._Cargo == (Cargo)Enum.Parse(typeof(Cargo), "MECANICO"))
                {
                    sql.Append("Select * from Funcionario where cargo=4 and dataDesativado is null order by nome");
                }
                else
                {
                    sql.Append("Select * from Funcionario where nome Like @nome");
                    comando.Parameters.AddWithValue("@nome", "%" + funcionario._Nome + "%");
                }
                comando.CommandText = sql.ToString();
                SqlDataReader dr = ConexaoBanco.Selecionar(comando);
                listaFuncionario = new List<Funcionario>();

                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        Funcionario objFuncionario = new Funcionario();
                        objFuncionario._Nome = (string)dr["nome"];
                        objFuncionario._Cpf = (string)dr["cpf"];
                        objFuncionario._Endereco = (string)dr["endereco"];
                        objFuncionario._Cep = (string)dr["cep"];
                        objFuncionario._Bairro = (string)dr["bairro"];
                        objFuncionario._Complemento = (string)dr["complemento"];
                        objFuncionario._Telefone1 = (string)dr["telefone1"];
                        objFuncionario._Telefone2 = (string)dr["telefone2"];
                        objFuncionario._Observacao = (string)dr["observacao"];
                        objFuncionario._Rg = (string)dr["rg"];
                        objFuncionario._OrgaoEmissor = (string)dr["orgaoEmissor"];
                        objFuncionario._DataNascimento = (DateTime)dr["dataNascimento"];
                        objFuncionario._FuncionarioID = (int)dr["funcionarioID"];
                        objFuncionario._Cargo = (Cargo)Enum.Parse(typeof(Cargo), dr["cargo"].ToString());
                        objFuncionario._Categoria = (string)dr["categoria"];
                        objFuncionario._DataAtivado = (DateTime)dr["dataAtivado"];
                        if (dr["dataDesativado"] != DBNull.Value)
                            objFuncionario._DataDesativado = (DateTime)dr["dataDesativado"];
                        objFuncionario._Habilitacao = (string)dr["habilitacao"];
                        objFuncionario._FuncionarioID = (int)dr["funcionarioID"];
                        if (dr["login"] != DBNull.Value)
                            objFuncionario._Login = (string)dr["login"];
                        listaFuncionario.Add(objFuncionario);
                    }
                }
                else
                {
                    listaFuncionario = null;
                }
                return listaFuncionario;
            }
            catch (Exception erro)
            {
                throw new Exception(erro.Message);
            }


        }
        /// <summary>
        ///  Método responsável em fazer a busca das informações desejada pelo id funcionário especifico.
        /// </summary>
        /// <param name="funcionario">Id do funcionário é passado como parâmetro dentro do objeto para fazer uma busca especifica.</param>
        /// <returns>Retorna um objeto com as informações preenchidas.</returns>
        public Funcionario BuscarFuncionario(Funcionario funcionario)
        {
            try
            {
                sql.Remove(0, sql.Length);
                comando.Parameters.Clear();


                sql.Append("Select * from Funcionario where funcionarioID = @funcionarioID");
                comando.Parameters.AddWithValue("@funcionarioID", funcionario._FuncionarioID);

                comando.CommandText = sql.ToString();
                SqlDataReader dr = ConexaoBanco.Selecionar(comando);
                Funcionario objFuncionario = new Funcionario();
                if (dr.HasRows)
                {
                    dr.Read();

                    objFuncionario._Nome = (string)dr["nome"];
                    objFuncionario._Cpf = (string)dr["cpf"];
                    objFuncionario._Endereco = (string)dr["endereco"];
                    objFuncionario._Cep = (string)dr["cep"];
                    objFuncionario._Bairro = (string)dr["bairro"];
                    objFuncionario._Complemento = (string)dr["complemento"];
                    objFuncionario._Telefone1 = (string)dr["telefone1"];
                    objFuncionario._Telefone2 = (string)dr["telefone2"];

                    objFuncionario._Observacao = (string)dr["observacao"];
                    objFuncionario._Rg = (string)dr["rg"];
                    objFuncionario._OrgaoEmissor = (string)dr["orgaoEmissor"];
                    objFuncionario._DataNascimento = (DateTime)dr["dataNascimento"];
                    objFuncionario._FuncionarioID = (int)dr["funcionarioID"];
                    objFuncionario._Cargo = (Cargo)Enum.Parse(typeof(Cargo), dr["cargo"].ToString());
                    objFuncionario._Categoria = (string)dr["categoria"];
                    objFuncionario._DataAtivado = (DateTime)dr["dataAtivado"];
                    string dataDesativado = dr["dataDesativado"].ToString();
                    if (DateTime.TryParse(dataDesativado, out data))
                        objFuncionario._DataDesativado = (DateTime)dr["dataDesativado"];
                    objFuncionario._Habilitacao = (string)dr["habilitacao"];
                    objFuncionario._FuncionarioID = (int)dr["funcionarioID"];
                    if (dr["login"] != DBNull.Value)
                        objFuncionario._Login = (string)dr["login"];

                }
                else
                {
                    objFuncionario = null;
                }
                return objFuncionario;
            }
            catch (Exception erro)
            {
                throw new Exception(erro.Message);
            }


        }
        /// <summary>
        /// Metodo responsável em verificar no banco se o usuário tem permissão.
        /// </summary>
        /// <param name="funcionario">Login e Senha são passados como parâmetro dentro do objeto para fazer uma busca especifica.</param>
        /// <returns>Retorna um objeto do tipo funcionário especifico.</returns>
        public Funcionario BuscarFuncionarioLoginSenha(Funcionario funcionario)
        {
            try
            {
                sql.Remove(0, sql.Length);
                comando.Parameters.Clear();

                sql.Append("Select * from Funcionario where login = @login and senha=@senha");
                comando.Parameters.AddWithValue("@login", funcionario._Login);
                comando.Parameters.AddWithValue("@senha", funcionario._Senha);

                comando.CommandText = sql.ToString();
                SqlDataReader dr = ConexaoBanco.Selecionar(comando);
                Funcionario objFuncionario = new Funcionario();
                if (dr.HasRows)
                {
                    dr.Read();

                    objFuncionario._Nome = (string)dr["nome"];
                    objFuncionario._Cpf = (string)dr["cpf"];
                    objFuncionario._Endereco = (string)dr["endereco"];
                    objFuncionario._Cep = (string)dr["cep"];
                    objFuncionario._Bairro = (string)dr["bairro"];
                    objFuncionario._Complemento = (string)dr["complemento"];
                    objFuncionario._Telefone1 = (string)dr["telefone1"];
                    objFuncionario._Telefone2 = (string)dr["telefone2"];

                    objFuncionario._Observacao = (string)dr["observacao"];
                    objFuncionario._Rg = (string)dr["rg"];
                    objFuncionario._OrgaoEmissor = (string)dr["orgaoEmissor"];
                    objFuncionario._DataNascimento = (DateTime)dr["dataNascimento"];
                    objFuncionario._FuncionarioID = (int)dr["funcionarioID"];
                    objFuncionario._Cargo = (Cargo)Enum.Parse(typeof(Cargo), dr["cargo"].ToString());
                    objFuncionario._Categoria = (string)dr["categoria"];
                    objFuncionario._DataAtivado = (DateTime)dr["dataAtivado"];
                    string dataDesativado = dr["dataDesativado"].ToString();
                    if (DateTime.TryParse(dataDesativado, out data))
                        objFuncionario._DataDesativado = (DateTime)dr["dataDesativado"];
                    objFuncionario._Habilitacao = (string)dr["habilitacao"];
                    objFuncionario._FuncionarioID = (int)dr["funcionarioID"];
                    if (dr["login"] != DBNull.Value)
                        objFuncionario._Login = (string)dr["login"];

                }
                else
                {
                    objFuncionario = null;
                }
                return objFuncionario;
            }
            catch (Exception erro)
            {
                throw new Exception(erro.Message);
            }


        }
       /// <summary>
        ///  Método responsável em verificar se ja existe no banco um login especifico.
       /// </summary>
        /// <param name="funcionario">Login é passado como parâmetro dentro do objeto para fazer uma busca especifica.</param>
       /// <returns>Retorna um objeto do tipo funcioário</returns>
        public Funcionario BuscarFuncionarioLogin(Funcionario funcionario)
        {
            try
            {
                sql.Remove(0, sql.Length);
                comando.Parameters.Clear();

                sql.Append("Select * from Funcionario where login = @login");
                comando.Parameters.AddWithValue("@login", funcionario._Login);

                comando.CommandText = sql.ToString();
                SqlDataReader dr = ConexaoBanco.Selecionar(comando);
                Funcionario objFuncionario = new Funcionario();
                if (dr.HasRows)
                {
                    dr.Read();

                    objFuncionario._FuncionarioID = (int)dr["funcionarioID"];

                    if (dr["login"] != DBNull.Value)
                        objFuncionario._Login = (string)dr["login"];

                }
                else
                {
                    objFuncionario = null;
                }
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
