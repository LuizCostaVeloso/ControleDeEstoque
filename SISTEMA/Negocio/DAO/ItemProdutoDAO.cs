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
    public class ItemProdutoDAO
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
        /// Cria lista e objetos statico.
        /// </summary>
        static IList<ItemProduto> listaItemProduto;
        static ItemProduto objItemProduto;
        static OrdemServico objOrdemSevico;
        /// <summary>
        /// Cria um objeto do tipo DAO;
        /// </summary>
        ProdutoDAO objProdutoDAO;
       


        #endregion

        #region CRUD
        /// <summary>
        ///    Método que converte o objeto em linguagem sql e grava no banco as informações.
        /// </summary>
        /// <param name="objItemProduto">Informações são passada no objeto como parâmetro para pessistir no banco.</param>
 
        public void Gravar(ItemProduto objItemProduto)
        {
            try
            {
                sql.Remove(0, sql.Length);
                comando.Parameters.Clear();

                sql.Append("Insert Into ItemProduto(produtoID, quantidadeItem, ordemServicoID, preco ) values");
                sql.Append("(@produtoID, @quantidadeItem, @ordemServicoID, @preco )");

                comando.CommandText = sql.ToString();

                comando.Parameters.AddWithValue("@produtoID", objItemProduto._ObjProduto._ProdutoID);
                comando.Parameters.AddWithValue("@quantidadeItem", objItemProduto._QuantidadeItem);
                comando.Parameters.AddWithValue("@ordemServicoID", objItemProduto._OrdemServico._OrdemServicoID);

                comando.Parameters.AddWithValue("@preco", objItemProduto._Preco);

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
        /// <param name="objItemProduto">Informações são passada no objeto como parâmetro especificando quais informações deseja alterar</param>
        public void Alterar(ItemProduto objItemProduto)
        {
            try
            {
                sql.Remove(0, sql.Length);
                comando.Parameters.Clear();

                sql.Append("update ItemProduto set produtoID=@produtoID, quantidadeItem=@quantidadeItem,ordemServicoID=@ordemServicoID,");
                sql.Append("preco=@preco where itemProdutoID=@itemProdutoID");

                comando.CommandText = sql.ToString();

                comando.Parameters.AddWithValue("@produtoID", objItemProduto._ObjProduto._ProdutoID);
                comando.Parameters.AddWithValue("@quantidadeItem", objItemProduto._QuantidadeItem);
                comando.Parameters.AddWithValue("@ordemServicoID", objItemProduto._OrdemServico._OrdemServicoID);
                comando.Parameters.AddWithValue("@preco", objItemProduto._Preco);
                comando.Parameters.AddWithValue("@itemProdutoID", objItemProduto._ItemProdutoID);

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
        /// <param name="itemProduto">objeto preenchido com o id e passado como parâmetro para exclusão no banco das informações especificas.</param>
        public void Excluir(ItemProduto itemProduto)
        {
            try
            {
                sql.Remove(0, sql.Length);
                comando.Parameters.Clear();

                sql.Append("Delete from ItemProduto where ItemProdutoID = @ItemProdutoID");

                comando.CommandText = sql.ToString();

                comando.Parameters.AddWithValue("@ItemProdutoID", itemProduto._ItemProdutoID);
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
        ///  Método responsável em fazer a busca das informações desejada pelo id da moto.
        /// </summary>
        /// <param name="itemProduto">Id da ordem de serviço e passado dentro do item como parâmetro.</param>
        /// <returns>Este método retorna uma lista de Item de Produto</returns>
        public IList<ItemProduto> buscarListaItemProduto(ItemProduto itemProduto)
        {
            try
            {

                sql.Remove(0, sql.Length);
                comando.Parameters.Clear();
                sql.Append("Select * from ItemProduto where ordemServicoID=@ordemServicoID");
                comando.Parameters.AddWithValue("@ordemServicoID", itemProduto._OrdemServico._OrdemServicoID);
                comando.CommandText = sql.ToString();
                SqlDataReader dr = ConexaoBanco.Selecionar(comando);
                listaItemProduto = new List<ItemProduto>();
                objProdutoDAO = new ProdutoDAO();
                objOrdemSevico = new OrdemServico();

                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        objItemProduto = new ItemProduto(objOrdemSevico);
                        objItemProduto._QuantidadeItem = (int)dr["quantidadeItem"];
                        objItemProduto._ObjProduto._ProdutoID = (int)dr["produtoID"];
                        objItemProduto._ObjProduto = objProdutoDAO.BuscarProduto(objItemProduto._ObjProduto);
                        objItemProduto._ItemProdutoID = (int)dr["ItemProdutoID"];
                        objItemProduto._OrdemServico._OrdemServicoID = (int)dr["ordemServicoID"];

                        objItemProduto._Preco = (decimal)dr["preco"];
                        listaItemProduto.Add(objItemProduto);

                    }
                }
                else
                {
                    listaItemProduto = null;
                }
                return listaItemProduto;
            }
            catch (Exception erro)
            {
                throw new Exception(erro.Message);
            }

        }
        /// <summary>
        ///   Método responsável em fazer a busca das informações do item especifico.
        /// </summary>
        /// <param name="itemProduto">Id do item é passado como parâmetro dentro do objeto para fazer uma busca especifica.</param>
        /// <returns>Retorna um objeto com as informações preenchidas.</returns>
        public ItemProduto BuscarItemProduto(ItemProduto itemProduto)
        {
            try
            {
                sql.Remove(0, sql.Length);
                comando.Parameters.Clear();

                sql.Append("Select * from ItemProduto where itemProdutoID = @itemProdutoID");
                comando.Parameters.AddWithValue("@itemProdutoID", itemProduto._ItemProdutoID);
                comando.CommandText = sql.ToString();
                SqlDataReader dr = ConexaoBanco.Selecionar(comando);
                objOrdemSevico = new OrdemServico();
                objItemProduto = new ItemProduto(objOrdemSevico);
                objProdutoDAO = new ProdutoDAO();
                if (dr.HasRows)
                {
                    dr.Read();

                    objItemProduto._QuantidadeItem = (int)dr["quantidadeItem"];
                    objItemProduto._ObjProduto._ProdutoID = (int)dr["produtoID"];
                    objItemProduto._ObjProduto = objProdutoDAO.BuscarProduto(objItemProduto._ObjProduto);
                    objItemProduto._ItemProdutoID = (int)dr["ItemProdutoID"];
                    objItemProduto._Preco = (decimal)dr["preco"];

                }
                else
                {
                    objItemProduto = null;
                }
                return objItemProduto;
            }
            catch (Exception erro)
            {
                throw new Exception(erro.Message);
            }

      
        }

        #endregion

    }
}
