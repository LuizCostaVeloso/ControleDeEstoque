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
    public class ServicoDAO
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
        /// Instancia de uma lista para receber as informações dos produtos buscados no banco.
        /// </summary>
        IList<Servico> listaServico;

        #endregion
        //---------------------------------///-------------------------///---------------------------------///
        #region CRUD
        /// <summary>
        /// Método que converte o objeto em liguagem sql e grava no banco as informações.
        /// </summary>
        /// <param name="objServico">Informações do serviço recebida como parâmetro.</param>
        public void Gravar(Servico objServico)
        {
            try
            {
                sql.Remove(0, sql.Length);
                comando.Parameters.Clear();

                sql.Append("Insert Into Servico(nomeServico,preco, observacao, marcaModelo) values(@nomeServico, @preco, @observacao, @marcaModelo)");

                comando.CommandText = sql.ToString();

                comando.Parameters.AddWithValue("@nomeServico", objServico._NomeServico);
                comando.Parameters.AddWithValue("@preco", objServico._Preco);
                comando.Parameters.AddWithValue("@observacao", objServico._Observacao);
                comando.Parameters.AddWithValue("@marcaModelo", objServico._MarcaModelo);

                ConexaoBanco.Crud(comando);
            }
            catch (Exception erro)
            {
                throw new Exception(erro.Message);
            }
        }
        /// <summary>
        /// Método que converte o objeto em liguagem sql persistindo no banco as informações para alteração do serviço. 
        /// </summary>
        /// <param name="objServico">Informações do serviço recebida como parâmetro.</param>
        public void Alterar(Servico objServico)
        {
            try
            {
                sql.Remove(0, sql.Length);
                comando.Parameters.Clear();

                sql.Append("update Servico set nomeServico=@nomeServico, preco=@preco, ");
                sql.Append("observacao = @observacao, marcaModelo=@marcaModelo where servicoID=@servicoID");

                comando.CommandText = sql.ToString();

                comando.Parameters.AddWithValue("@nomeServico", objServico._NomeServico);
                comando.Parameters.AddWithValue("@preco", objServico._Preco);
                comando.Parameters.AddWithValue("@servicoID", objServico._ServicoID.ToString());
                comando.Parameters.AddWithValue("@observacao", objServico._Observacao);
                comando.Parameters.AddWithValue("@marcaModelo", objServico._MarcaModelo);
                ConexaoBanco.Crud(comando);
            }
            catch (Exception erro)
            {
                throw new Exception(erro.Message);
            }
        }
        /// <summary>
        ///  Método responsável em fazer a exclusão do serviço no banco.
        /// </summary>
        /// <param name="objServico">Informações do serviço recebida como parâmetro.</param>
        public void Excluir(Servico objServico)
        {
            try
            {
                sql.Remove(0, sql.Length);
                comando.Parameters.Clear();

                sql.Append("Delete from Servico where servicoID = @servicoID");

                comando.CommandText = sql.ToString();

                comando.Parameters.AddWithValue("@servicoID", objServico._ServicoID);
                ConexaoBanco.Crud(comando);
            }
            catch (Exception)
            {
                throw new Exception("As informações da moto está vincunlado a uma Ordem de Serviço!!!");
            }
        }

        #endregion
        //---------------------------------///-------------------------///---------------------------------///
        #region BUSCAS
        /// <summary>
        /// Método responsável em fazer a busca dos serviços solicitados.
        /// </summary>
        /// <param name="servico">Nome do serviço passado com parâmetro dentro do objeto serviço.</param>
        /// <returns>Este método retorna uma lista de serviço.</returns>
        public IList<Servico> BuscarListaServico(Servico servico)
        {
            try
            {
                sql.Remove(0, sql.Length);
                comando.Parameters.Clear();

                sql.Append("Select * from Servico where nomeServico Like @nomeServico");
                comando.Parameters.AddWithValue("@nomeServico", "%" + servico._NomeServico + "%");
                comando.CommandText = sql.ToString();
                SqlDataReader dr = ConexaoBanco.Selecionar(comando);
                listaServico = new List<Servico>();

                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        Servico objServico = new Servico();
                        objServico._ServicoID = (int)dr["servicoID"];
                        objServico._NomeServico = (string)dr["nomeServico"];
                        objServico._Preco = (decimal)dr["preco"];
                        objServico._Observacao = (string)dr["observacao"];
                        objServico._MarcaModelo = (string)dr["marcaModelo"];
                        listaServico.Add(objServico);

                    }
                }
                else
                {
                    listaServico = null;
                }
                return listaServico;
            }
            catch (Exception erro)
            {
                throw new Exception(erro.Message);
            }
        }
        /// <summary>
        /// Método responsável em fazer a busca do serviços solicitados.
        /// </summary>
        /// <param name="servico">Id do serviço passado com parâmetro dentro do objeto serviço.</param>
        /// <returns>Este método retorna um objeto do tipo serviço.</returns>
        public Servico BuscarServico(Servico servico)
        {
            try
            {
                sql.Remove(0, sql.Length);
                comando.Parameters.Clear();

                sql.Append("Select * from Servico where servicoID = @servicoID");
                comando.Parameters.AddWithValue("@servicoID", servico._ServicoID);
                comando.CommandText = sql.ToString();
                SqlDataReader dr = ConexaoBanco.Selecionar(comando);
                Servico objServico = new Servico();
                if (dr.HasRows)
                {
                    dr.Read();

                    objServico._ServicoID = (int)dr["servicoID"];
                    objServico._NomeServico = (string)dr["nomeServico"];
                    objServico._Preco = (decimal)dr["preco"];
                    objServico._Observacao = (string)dr["observacao"];
                    objServico._MarcaModelo = (string)dr["marcaModelo"];
                }
                else
                {
                    objServico = null;
                }
                return objServico;
            }
            catch (Exception erro)
            {
                throw new Exception(erro.Message);
            }
        }

        #endregion
        //---------------------------------///-------------------------///---------------------------------/// 
    }
}

