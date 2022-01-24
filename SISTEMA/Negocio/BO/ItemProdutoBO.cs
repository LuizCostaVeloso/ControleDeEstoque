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
    public class ItemProdutoBO
    {
       
        #region ASSINATURA DOS METODOS, CRIAÇÃO DE OBJETO E CRIAÇÃO DE VARIÁVEL.
        /// <summary>
        /// Cria um objeto da camada DAO para dar acesso aos metodos inerente da Classe.
        /// </summary>
        ItemProdutoDAO objItemProdutoDAO;
        ProdutoBO objProdutoBO;
        /// <summary>
        /// Cria uma lista do Tipo da Classe. 
        ///</summary>
        static IList<ItemProduto> listaItemProduto;
        /// <summary>
        /// Cria o objeto para reservar um espaço no disco após instanciado.
        /// </summary>
        static ItemProduto objItemProduto;
        static Produto objProduto;
        static OrdemServico objOrdemServico;
        /// <summary>
        /// Cria as variaveis.
        /// </summary>
        string id;
        int numInt;
        #endregion

        #region CRUD
        /// <summary>
        ///    Método responsável em preparar os dados para gravar ou alterar enviando para a camada DAO. 
        /// </summary>
        /// <param name="objItemProduto">Parâmetro que recebe as informações do item do Produto.</param>
        /// <param name="objProduto">Parâmetro que recebe as informações do Produto.</param>
        /// <param name="qtde">Inteiro que recebe a quantidade de Prduto.</param>
        public void Gravar(ItemProduto objItemProduto, Produto objProduto, int qtde)
        {
            try
            {
                id = objItemProduto._ItemProdutoID.ToString();

                objItemProdutoDAO = new ItemProdutoDAO();
                if (int.TryParse(id, out numInt) && objItemProduto._ItemProdutoID != 0)
                {
                    objItemProduto._ObjProduto._ProdutoID = objProduto._ProdutoID;
                    objItemProdutoDAO.Alterar(objItemProduto);
                    if (qtde != 0)
                    {
                        objProduto = new Produto();
                        if (objItemProduto._QuantidadeItem < qtde)
                        {
                            objProduto._EntradaEstoque = qtde - objItemProduto._QuantidadeItem;
                            objProduto._ProdutoID = objItemProduto._ObjProduto._ProdutoID;
                            objProdutoBO = new ProdutoBO();
                            objProdutoBO.Somar(objProduto);
                        }
                        if (objItemProduto._QuantidadeItem > qtde)
                        {
                            objProduto._EntradaEstoque = objItemProduto._QuantidadeItem - qtde;
                            objProduto._ProdutoID = objItemProduto._ObjProduto._ProdutoID;
                            objProdutoBO = new ProdutoBO();
                            objProdutoBO.Baixar(objProduto);
                        }
                    }
                }
                else
                {
                    objItemProduto._ObjProduto._ProdutoID = objProduto._ProdutoID;
                    objItemProduto._Preco = objProduto._PrecoVenda;
                    objItemProdutoDAO.Gravar(objItemProduto);
                    objProduto._EntradaEstoque = objItemProduto._QuantidadeItem;
                    objProdutoBO = new ProdutoBO();
                    objProdutoBO.Baixar(objProduto);
                }
            }
            catch (Exception erro)
            {
                throw new Exception(erro.Message);
            }     

        }
         /// <summary>
        ///  Método que recebe as informações do item de produto para exluir o item e devolver os produtos excluido para o estoque.
         /// </summary>
        /// <param name="itemProduto">Parâmetro que recebe as informações do item de produto.</param>
        public void Excluir(ItemProduto itemProduto)
        {
            try
            {
                objItemProdutoDAO = new ItemProdutoDAO();
                objProduto = new Produto();
                objProduto._ProdutoID = itemProduto._ObjProduto._ProdutoID;
                objProduto._EntradaEstoque = itemProduto._QuantidadeItem;
                objItemProdutoDAO.Excluir(itemProduto);
                objProdutoBO = new ProdutoBO();
                objProdutoBO.Somar(objProduto);
            }
            catch (Exception erro)
            {
                throw new Exception(erro.Message);
            }
    
        }

        #endregion

        #region BUSCAS
        /// <summary>
        /// Método responsável em ler a lista de Produto.
        /// </summary>
        /// <param name="objOS">Parâmetro a lista item de produto.</param>
        /// <returns>retorna a lista do tipo data table</returns>
        public DataTable listaIDataTableItemProduto(OrdemServico objOS)
        {
            try
            {
                objItemProdutoDAO = new ItemProdutoDAO();

                DataRow dr;
                DataTable dt = new DataTable();

                dt.Columns.Add("ItemProdutoID");
                dt.Columns.Add("ProdutoID");
                dt.Columns.Add("Qtde");
                dt.Columns.Add("Nome do Produto");
                dt.Columns.Add("Preço");
                dt.Columns.Add("Total");
                dt.Columns.Add("osID");



                //listaItemProduto = objItemProdutoDAO.buscarListaItemProduto(itemProduto);

                // Percorre a lista com o resultado da consulta

                if (objOS._ListaItemProduto != null)
                {
                    foreach (ItemProduto objItemProduto in objOS._ListaItemProduto)
                    {
                        dr = dt.NewRow();

                        dr["ItemProdutoID"] = objItemProduto._ItemProdutoID;
                        dr["ProdutoID"] = objItemProduto._ObjProduto._ProdutoID;
                        dr["Qtde"] = objItemProduto._QuantidadeItem;
                        dr["Nome do Produto"] = objItemProduto._ObjProduto._NomeProduto;
                        dr["Preço"] = objItemProduto._Preco;
                        dr["Total"] = objItemProduto._Total;
                        dr["osID"] = objItemProduto._OrdemServico._OrdemServicoID;
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
        ///  Método responsável em buscar uma lista de item de produto e calcular a quantidade vezes o preço.
        /// </summary>
        /// <param name="itemProduto">Parâmetro que recebe o ID da ordem de Serviço</param>
        /// <returns>Retorna uma lista de item de produto</returns>
        public IList<ItemProduto> buscarListaItemProduto(ItemProduto itemProduto)
        {
            try
            {
                objItemProdutoDAO = new ItemProdutoDAO();
                listaItemProduto = new List<ItemProduto>();
                listaItemProduto = objItemProdutoDAO.buscarListaItemProduto(itemProduto);
                if (listaItemProduto != null)
                {
                    foreach (ItemProduto objItemProduto in listaItemProduto)
                    {
                        objItemProduto._Total = objItemProduto._Preco * objItemProduto._QuantidadeItem;

                    }
                }
                return listaItemProduto; 

            }
            catch (Exception erro)
            {
                throw new Exception(erro.Message);
            }

        }
        /// <summary>
        ///  Método responsável em buscar uma lista de item de Produto.
        /// </summary>
        /// <param name="itemProduto">Parâmetro que recebe o id do item do produto.</param>
        /// <returns>Retorna um objeto preenchido com as informações do item de produto.</returns>
        public ItemProduto BuscarItemProduto(ItemProduto itemProduto)
        {
            try
            {
                objItemProdutoDAO = new ItemProdutoDAO();
                objItemProduto = new ItemProduto(objOrdemServico);
                objItemProduto = objItemProdutoDAO.BuscarItemProduto(itemProduto);
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
