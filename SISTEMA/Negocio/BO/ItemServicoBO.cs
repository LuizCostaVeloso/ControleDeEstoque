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
    public class ItemServicoBO
    {
        #region ASSINATURA DOS METODOS, CRIAÇÃO DE OBJETO E CRIAÇÃO DE VARIÁVEL.
        /// <summary>
        /// Cria um objeto da camada DAO para dar acesso aos metodos inerente da Classe.
        /// </summary>
        ItemServicoDAO objItemServicoDAO;
        /// <summary>
        /// Cria uma lista do Tipo da Classe. 
        ///</summary>
        IList<ItemServico> listaItemServico;
        /// <summary>
        /// Cria um objeto do tipo BO.
        /// </summary>
        ServicoBO objServicoBO;        
        OrdemServicoBO objOrdemServicoBO;
        /// <summary>
        /// Cria o objeto para reservar um espaço no disco após instanciado.
        /// </summary>
        static ItemServico objItemServico;
        static Servico objServico;
        static OrdemServico objOrdemServico;

        #endregion

        #region CRUD
         /// <summary>
        ///   Método responsável em preparar os dados para gravar ou alterar enviando para a camada DAO. 
         /// </summary>
        /// <param name="objItemServico">Parâmetro que recebe as informações do intem de serviço.</param>
        /// <param name="servico">Parâmetro que recebe o id do serviço.</param>
        public void Gravar(ItemServico objItemServico, Servico servico)
        {
            try
            {
                objItemServicoDAO = new ItemServicoDAO();
                objServicoBO = new ServicoBO();
                objServico = objServicoBO.BuscarServico(servico);
                objItemServico._Preco = objServico._Preco;
                objItemServico._Servico._ServicoID = objServico._ServicoID;
                objItemServicoDAO.Gravar(objItemServico);
            }
            catch (Exception erro)
            {
                throw new Exception(erro.Message);
            }
      

        }
         /// <summary>
        ///   Método que recebe um id e chama o método excluir da camada DAO.
         /// </summary>
        /// <param name="itemServico">Parâmetro que recebe o id do Item de Serviço.</param>
        public void Excluir(ItemServico itemServico)
        {
            try
            {
                objItemServicoDAO = new ItemServicoDAO();
                objItemServicoDAO.ExcluirUm(itemServico);
            }
            catch (Exception erro)
            {
                throw new Exception(erro.Message);
            }
         

        }

        #endregion

        #region BUSCAS
        /// <summary>
        ///  Método responsável em ler a lista de serviço.
        /// </summary>
        /// <param name="objOS">Parâmetro a lista item de serviço.</param>
        /// <returns> retorna a lista do tipo data table</returns>
        public DataTable buscaListaDataTableItemServico(OrdemServico objOS)
        {
            try
            {
                objItemServicoDAO = new ItemServicoDAO();

                DataRow dr;
                DataTable dt = new DataTable();

                dt.Columns.Add("ItemServicoID");
                dt.Columns.Add("ServicoID");
                dt.Columns.Add("Nome do Serviço");
                dt.Columns.Add("Preço");
                dt.Columns.Add("O.S.ID");

                // Percorre a lista com o resultado da consulta

                if (objOS._ListaItemServico != null)
                {
                    foreach (ItemServico objItemServico in objOS._ListaItemServico)
                    {
                        dr = dt.NewRow();

                        dr["ItemServicoID"] = objItemServico._ItemServicoID;
                        dr["ServicoID"] = objItemServico._Servico._ServicoID;
                        dr["Nome do Serviço"] = objItemServico._Servico._NomeServico;
                        dr["Preço"] = objItemServico._Servico._Preco;
                        dr["O.S.ID"] = objItemServico._OrdemServico._OrdemServicoID;

                        dt.Rows.Add(dr);
                    }
                }
                return dt;
            }
            catch (Exception erro)
            {
                throw new Exception(erro.Message);
            }


        }      
         /// <summary>
        ///  Método responsável em buscar uma lista de item de serviço.
         /// </summary>
        /// <param name="item">Parâmetro que recebe o id da ordem de serviço.</param>
         /// <returns>Retorna uma lisata de serviços.</returns>
        public IList<ItemServico> buscarListaItemServico(ItemServico item)
        {
            try
            {
                objItemServicoDAO = new ItemServicoDAO();
                listaItemServico = new List<ItemServico>();
                listaItemServico = objItemServicoDAO.BuscarListaItemServico(item);
                return listaItemServico;
            }
            catch (Exception erro)
            {
                throw new Exception(erro.Message);
            }
           

        }

        #endregion 
        
    }

}
