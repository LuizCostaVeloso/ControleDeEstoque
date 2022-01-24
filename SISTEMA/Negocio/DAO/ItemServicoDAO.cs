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
    public class ItemServicoDAO
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
        /// Cria uma lista statica do tipo item de serviço.
        /// </summary>
        static IList<ItemServico> listaItemServico;
        /// <summary>
        /// Cria os objetos
        /// </summary>
        ServicoDAO objServicoDAO;
        ItemServico objItemServico;
        /// <summary>
        /// Cria os objetos do tipo statico.
        /// </summary>
        static Servico objServico;
        static OrdemServico objOrdemServico;
        /// <summary>
        /// Cria uma variavel.
        /// </summary>
        string variavel;
        int numInt;

        #endregion

        #region CRUD
         /// <summary>
        ///  Método que converte o objeto em linguagem sql e grava no banco as informações.
         /// </summary>
        /// <param name="objItemServico">Informações são passada no objeto como parâmetro para pessistir no banco.</param>
        public void Gravar(ItemServico objItemServico)
        {
            try
            {
                sql.Remove(0, sql.Length);
                comando.Parameters.Clear();

                sql.Append("Insert Into ItemServico (servicoID, ordemServicoID, itemPreco) values");
                sql.Append("(@servicoID,@ordemServicoID, @itemPreco)");

                comando.CommandText = sql.ToString();

                comando.Parameters.AddWithValue("@servicoID", objItemServico._Servico._ServicoID);
                comando.Parameters.AddWithValue("@itemPreco", objItemServico._Preco);
                comando.Parameters.AddWithValue("@ordemServicoID", objItemServico._OrdemServico._OrdemServicoID);

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
        /// <param name="objItemServico">Informações são passada no objeto como parâmetro especificando quais informações deseja alterar</param>
        public void Alterar(ItemServico objItemServico)
        {
            try
            {
                sql.Remove(0, sql.Length);
                comando.Parameters.Clear();

                sql.Append("update ItemServico set servicoID=@servicoID");
                sql.Append("where itemServicoID=@itemServicoID");

                comando.CommandText = sql.ToString();

                comando.Parameters.AddWithValue("@itemServicoID", objItemServico._ItemServicoID);
                comando.Parameters.AddWithValue("@servicoID", objItemServico._Servico._ServicoID);

                ConexaoBanco.Crud(comando);
            }
            catch (Exception erro)
            {
                throw new Exception(erro.Message);
            }



        }        

        /// <summary>
        ///   Método responsável em fazer a exclusão no banco das informações desejada.
        /// </summary>
        /// <param name="objItemServico">objeto preenchido com o id e passado como parâmetro para exclusão no banco das informações especificas.</param>
        public void ExcluirUm(ItemServico objItemServico)
        {
            try
            {
                sql.Remove(0, sql.Length);
                comando.Parameters.Clear();
                sql.Append("Delete from ItemServico where itemServicoID = @itemServicoID");
                comando.CommandText = sql.ToString();
                comando.Parameters.AddWithValue("@itemServicoID", objItemServico._ItemServicoID);
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
        ///   Método responsável em fazer a busca das informações desejada pela placa da moto.
        /// </summary>
        /// <param name="item">objeto preenchido com o id e passado como parâmetro para busca no banco.</param>
        /// <returns>Este método retorna uma lista de Item de Serviço.</returns>
        public IList<ItemServico> BuscarListaItemServico(ItemServico item)
        {
            try
            {
                sql.Remove(0, sql.Length);
                comando.Parameters.Clear();

                sql.Append("Select * from ItemServico where ordemServicoID = @ordemServicoID");
                comando.Parameters.AddWithValue("@ordemServicoID", item._OrdemServico._OrdemServicoID);
                comando.CommandText = sql.ToString();
                SqlDataReader dr = ConexaoBanco.Selecionar(comando);
                listaItemServico = new List<ItemServico>();
                objServicoDAO = new ServicoDAO();
                objOrdemServico = new OrdemServico();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        objItemServico = new ItemServico(objOrdemServico);
                        objItemServico._Preco = (decimal)dr["itemPreco"];
                        objItemServico._OrdemServico._OrdemServicoID = (int)dr["ordemServicoID"];
                        objItemServico._ItemServicoID = (int)dr["itemServicoID"];
                        objItemServico._Servico._ServicoID = (int)dr["servicoID"];
                        objItemServico._Servico = objServicoDAO.BuscarServico(objItemServico._Servico);
                        listaItemServico.Add(objItemServico);

                    }
                }
                else
                {
                    listaItemServico = null;
                }
                return listaItemServico;
            }
            catch (Exception erro)
            {
                throw new Exception(erro.Message);
            }

        }
        /// <summary>
        ///   Método responsável em fazer a busca das informações do item especifico.
        /// </summary>
        /// <param name="itemServicoID">Id do item é passado como parâmetro dentro do objeto para fazer uma busca especifica.</param>
        /// <returns>Retorna um objeto com as informações preenchidas.</returns>
        public ItemServico BuscarItemServico(int itemServicoID)
        {
            try
            {
                sql.Remove(0, sql.Length);
                comando.Parameters.Clear();

                sql.Append("Select * from ItemServico where itemServicoID = @itemServicoID");
                comando.Parameters.AddWithValue("@itemServicoID", itemServicoID);
                comando.CommandText = sql.ToString();
                SqlDataReader dr = ConexaoBanco.Selecionar(comando);
                ItemServico objItemServico = new ItemServico(objOrdemServico);
                objServicoDAO = new ServicoDAO();
                if (dr.HasRows)
                {
                    dr.Read();

                    objItemServico._Preco = (decimal)dr["itemPreco"];
                    objItemServico._OrdemServico._OrdemServicoID = (int)dr["ordemServicoID"];
                    objItemServico._ItemServicoID = (int)dr["itemServicoID"];
                    objItemServico._Servico._ServicoID = (int)dr["servicoID"];
                    objItemServico._Servico = objServicoDAO.BuscarServico(objItemServico._Servico);
                }
                else
                {
                    objItemServico = null;
                }
                return objItemServico;
            }
            catch (Exception erro)
            {
                throw new Exception(erro.Message);
            }     
           
        }

        #endregion

    }
}