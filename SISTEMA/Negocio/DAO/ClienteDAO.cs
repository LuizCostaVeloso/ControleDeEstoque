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
    public class ClienteDAO
    {
        #region ASSINATURA DOS METODOS

        /// <summary>
        /// Instancia de um novo objeto do tipo StringBuilder.
        /// </summary>
        StringBuilder sql = new StringBuilder();
        /// <summary>
        ///   Instancia de um novo objeto do tipo SqlCommand.
        /// </summary>
        SqlCommand comando = new SqlCommand();
        /// <summary>
        /// Cria lista de cliente.
        /// </summary>
        IList<Cliente> listaCliente;

        #endregion
        //---------------------------------///-------------------------///---------------------------------///
        #region CRUD
         /// <summary>
        ///  Método que converte o objeto em linguagem sql e grava no banco as informações.
         /// </summary>
        /// <param name="objCliente">Informações são passada no objeto como parâmetro para pessistir no banco.</param>
        public void Gravar(Cliente objCliente)
        {
            try
            {
                sql.Remove(0, sql.Length);
                comando.Parameters.Clear();

                sql.Append("INSERT INTO Cliente(nome, tipoPessoa, cpfCnpj, ");
                sql.Append("sexo, dataCadastro, endereco, cep, bairro,");
                sql.Append("complemento, cidade, uf, telefone1, telefone2,");
                sql.Append("email, observacao, rg, orgaoEmissor)");
                sql.Append("VALUES (@nome, @tipoPessoa, @cpfCnpj,");
                sql.Append("@sexo, @dataCadastro, @endereco, @cep, @bairro,");
                sql.Append("@complemento, @cidade, @uf, @telefone1, @telefone2, ");
                sql.Append("@email, @observacao, @rg, @orgaoEmissor)");

                comando.CommandText = sql.ToString();

                comando.Parameters.AddWithValue("@nome", objCliente._Nome);
                comando.Parameters.AddWithValue("@tipoPessoa", objCliente._TipoPessoa);
                comando.Parameters.AddWithValue("@cpfCnpj", objCliente._CpfCnpj);              
                comando.Parameters.AddWithValue("@sexo", objCliente._Sexo);
                comando.Parameters.AddWithValue("@dataCadastro", objCliente._DataCadastro);
                comando.Parameters.AddWithValue("@endereco", objCliente._Endereco);
                comando.Parameters.AddWithValue("@cep", objCliente._Cep);
                comando.Parameters.AddWithValue("@bairro", objCliente._Bairro);
                comando.Parameters.AddWithValue("@complemento", objCliente._Complemento);
                comando.Parameters.AddWithValue("@cidade", objCliente._Cidade);
                comando.Parameters.AddWithValue("@uf", objCliente._Uf);
                comando.Parameters.AddWithValue("@telefone1", objCliente._Telefone1);
                comando.Parameters.AddWithValue("@telefone2", objCliente._Telefone2);                
                comando.Parameters.AddWithValue("@email", objCliente._Email);
                comando.Parameters.AddWithValue("@observacao", objCliente._Observacao);
                comando.Parameters.AddWithValue("@rg", objCliente._Rg);
                comando.Parameters.AddWithValue("@orgaoEmissor", objCliente._OrgaoEmissor);               

                ConexaoBanco.Crud(comando);
            }
            catch (Exception erro)
            {
                throw new Exception(erro.Message);
            }
        }
        /// <summary>
        ///   Método que converte o objeto em liguagem sql persistindo no banco as informações para alteração. 
        /// </summary>
        /// <param name="objCliente">Informações são passada no objeto como parâmetro especificando quais informações deseja alterar.</param>
        public void Alterar(Cliente objCliente)
        {
            try
            {
                sql.Remove(0, sql.Length);
                comando.Parameters.Clear();

                sql.Append("update Cliente set nome=@nome, tipoPessoa=@tipoPessoa, cpfCnpj=@cpfCnpj,");
                sql.Append("sexo=@sexo, dataCadastro=@dataCadastro, endereco=@endereco,");
                sql.Append("cep=@cep, bairro=@bairro,complemento=@complemento, cidade=@cidade, uf=@uf,");
                sql.Append("telefone1=@telefone1, telefone2=@telefone2,");
                sql.Append("email=@email, observacao=@observacao, rg=@rg,");
                sql.Append("orgaoEmissor=@orgaoEmissor where clienteID=@clienteID");

                comando.CommandText = sql.ToString();

                comando.Parameters.AddWithValue("@nome", objCliente._Nome);
                comando.Parameters.AddWithValue("@tipoPessoa", objCliente._TipoPessoa);
                comando.Parameters.AddWithValue("@cpfCnpj", objCliente._CpfCnpj);             
                comando.Parameters.AddWithValue("@sexo", objCliente._Sexo);
                comando.Parameters.AddWithValue("@dataCadastro", objCliente._DataCadastro);
                comando.Parameters.AddWithValue("@endereco", objCliente._Endereco);
                comando.Parameters.AddWithValue("@cep", objCliente._Cep);
                comando.Parameters.AddWithValue("@bairro", objCliente._Bairro);
                comando.Parameters.AddWithValue("@complemento", objCliente._Complemento);
                comando.Parameters.AddWithValue("@cidade", objCliente._Cidade);
                comando.Parameters.AddWithValue("@uf", objCliente._Uf);
                comando.Parameters.AddWithValue("@telefone1", objCliente._Telefone1);
                comando.Parameters.AddWithValue("@telefone2", objCliente._Telefone2);                
                comando.Parameters.AddWithValue("@email", objCliente._Email);
                comando.Parameters.AddWithValue("@observacao", objCliente._Observacao);
                comando.Parameters.AddWithValue("@rg", objCliente._Rg);
                comando.Parameters.AddWithValue("@orgaoEmissor", objCliente._OrgaoEmissor);
                comando.Parameters.AddWithValue("@clienteID", objCliente._ClienteID);

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
        /// <param name="objCliente">>objeto preenchido com o id e passado como parâmetro para exclusão no banco das informações especificas.</param>
        public void Excluir(Cliente objCliente)
        {
            try
            {
                sql.Remove(0, sql.Length);
                comando.Parameters.Clear();

                sql.Append("Delete from Cliente where clienteID = @clienteID");

                comando.CommandText = sql.ToString();

                comando.Parameters.AddWithValue("@clienteID", objCliente._ClienteID);
                ConexaoBanco.Crud(comando);
            }
            catch (Exception)
            {
                throw new Exception("As informações do Cliente está vincunlado a uma Ordem de Serviço!!!");
            }
        }

        #endregion
        //---------------------------------///-------------------------///---------------------------------///
        #region BUSCAS
          /// <summary>
        ///   Método responsável em fazer a busca das informações desejada pelo id da moto.
          /// </summary>
        /// <param name="cliente">Nome do Cliente passado como parâmetro dentro do objeto Cliente.</param>
        /// <returns>Este método retorna uma lista de Cliente</returns>
        public IList<Cliente> BuscarListaCliente(Cliente cliente)
        {
            try
            {
                sql.Remove(0, sql.Length);
                comando.Parameters.Clear();

                sql.Append("Select * from Cliente where nome Like @nome");
                comando.Parameters.AddWithValue("@nome", "%" + cliente._Nome + "%");
                comando.CommandText = sql.ToString();
                SqlDataReader dr = ConexaoBanco.Selecionar(comando);
                listaCliente = new List<Cliente>();

                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        Cliente objCliente = new Cliente();
                        objCliente._Nome = (string)dr["nome"];
                        objCliente._TipoPessoa = (int)dr["tipoPessoa"];
                        objCliente._CpfCnpj = (string)dr["cpfCnpj"];                     
                        objCliente._Sexo = (Sexo)Enum.Parse(typeof(Sexo), dr["sexo"].ToString());
                        objCliente._DataCadastro = (DateTime)dr["dataCadastro"];
                        objCliente._Endereco = (string)dr["endereco"];
                        objCliente._Cep = (string)dr["cep"];
                        objCliente._Bairro = (string)dr["bairro"];
                        objCliente._Complemento = (string)dr["complemento"];
                        objCliente._Cidade = (string)dr["cidade"];
                        objCliente._Uf = (Uf)Enum.Parse(typeof(Uf), dr["uf"].ToString());
                        objCliente._Telefone1 = (string)dr["telefone1"];
                        objCliente._Telefone2 = (string)dr["telefone2"];                       
                        objCliente._Email = (string)dr["email"];
                        objCliente._Observacao = (string)dr["observacao"];
                        objCliente._Rg = (string)dr["rg"];
                        objCliente._OrgaoEmissor = (string)dr["orgaoEmissor"];
                        objCliente._ClienteID = (int)dr["clienteID"];

                        listaCliente.Add(objCliente);
                    }
                }
                else
                {
                    listaCliente = null;
                }
                return listaCliente;
            }
            catch (Exception erro)
            {
                throw new Exception(erro.Message);
            }
        }
        /// <summary>
        ///  Método responsável em fazer a busca das informações do Cliente especifico.
        /// </summary>
        /// <param name="cliente">Este objeto é preenchido com o CPF/CNPJ ou id para fazer uma busca especifica passado no método como parâmetro</param>
        /// <returns>Este retorna um objeto do tipo cliente</returns>
        public Cliente BuscarCliente(Cliente cliente)
        {
            try
            {                
                sql.Remove(0, sql.Length);
                comando.Parameters.Clear();
                if (!String.IsNullOrEmpty(cliente._CpfCnpj))
                {
                    sql.Append("Select * from Cliente where cpfCnpj = @cpfCnpj");
                    comando.Parameters.AddWithValue("@cpfCnpj", cliente._CpfCnpj);
                }
                else
                {
                    sql.Append("Select * from Cliente where clienteID = @clienteID");
                    comando.Parameters.AddWithValue("@clienteID", cliente._ClienteID);
                }

                comando.CommandText = sql.ToString();
                SqlDataReader dr = ConexaoBanco.Selecionar(comando);
                Cliente objCliente = new Cliente();
                if (dr.HasRows)
                {
                    dr.Read();

                    objCliente._Nome = (string)dr["nome"];
                    objCliente._TipoPessoa = (int)dr["tipoPessoa"];
                    objCliente._CpfCnpj = (string)dr["cpfCnpj"];                    
                    objCliente._Sexo = (Sexo)Enum.Parse(typeof(Sexo), dr["sexo"].ToString());
                    objCliente._DataCadastro = (DateTime)dr["dataCadastro"];
                    objCliente._Endereco = (string)dr["endereco"];
                    objCliente._Cep = (string)dr["cep"];
                    objCliente._Bairro = (string)dr["bairro"];
                    objCliente._Complemento = (string)dr["complemento"];
                    objCliente._Cidade = (string)dr["cidade"];
                    objCliente._Uf = (Uf)Enum.Parse(typeof(Uf), dr["uf"].ToString());
                    objCliente._Telefone1 = (string)dr["telefone1"];
                    objCliente._Telefone2 = (string)dr["telefone2"];                    
                    objCliente._Email = (string)dr["email"];
                    objCliente._Observacao = (string)dr["observacao"];
                    objCliente._Rg = (string)dr["rg"];
                    objCliente._OrgaoEmissor = (string)dr["orgaoEmissor"];
                    objCliente._ClienteID = (int)dr["clienteID"];
                }
                else
                {
                    objCliente = null;
                }
                return objCliente;
            }
            catch (Exception erro)
            {
                throw new Exception(erro.Message);
            }
        }

        #endregion     
    }
}
