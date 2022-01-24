using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Negocio.Model;
using Negocio.DAO;
using System.Data;

namespace Negocio.BO
{
    /// <summary>
    /// Classe da camada BO responsável pela Regra de negócio.
    /// </summary>
    public class ProdutoBO
    {
        #region ASSINATURAS DE METODOS
        /// <summary>
        /// Cria um objeto da camada DAO para dar acesso aos metodos inerente da Classe.
        /// </summary>
        ProdutoDAO objProdutoDAO;
        /// <summary>
        /// Cria uma lista do Tipo da Classe. 
        ///</summary>
        IList<Produto> listaProduto;
        /// <summary>
        /// Cria o objeto para reservar um espaço no disco após instanciado.
        /// </summary>
        Produto objProduto;
        /// <summary>
        /// Cria as variaveis.
        /// </summary>
        string estoqueMinimo, precoCompra, precoVenda, entradaEstoque, id;
        decimal numDecimal;
        int numInt;

        #endregion

        #region CRUD
        /// <summary>
        ///   Método responsável em preparar os dados para gravar ou alterar enviando para a camada DAO. 
        /// </summary>
        /// <param name="objProduto">Parâmetro que recebe as informações do Produto.</param>
        public void Gravar(Produto objProduto)
        {    /// PERGUNTA PARA A PARTE ESCRITA (UM PRODUTO PODE SER USADO EM VARIAS MOTO COMO POR EXEMPLO
            //UM TIPO DE PNEU PODE SER USADO NA XRE E TORNADO SENDO QUE O MESMO ESTOQUE)

            try
            {
                id = objProduto._ProdutoID.ToString();
                estoqueMinimo = objProduto._EstoqueMinimo.ToString();
                precoCompra = objProduto._PrecoCompra.ToString();
                precoVenda = objProduto._PrecoVenda.ToString();
                entradaEstoque = objProduto._EntradaEstoque.ToString();


                if (String.IsNullOrEmpty(objProduto._NomeProduto))
                    throw new Exception("O campo Nome é obrigatório!!!");
                if (String.IsNullOrEmpty(objProduto._EstoqueMinimo.ToString()))
                    throw new Exception("Digite o Estoque Minimo!!!");
                if (!int.TryParse(estoqueMinimo, out numInt))
                    throw new Exception("O campo Estoque Minimo só aceita Números!!!");
                if (String.IsNullOrEmpty(objProduto._PrecoCompra.ToString()))
                    throw new Exception("O campo Preço de Custo é obrigatório!!!");
                if (!Decimal.TryParse(precoCompra, out numDecimal))
                    throw new Exception("Digite um Preço de Custo Válido ex: 10,34");
                if (String.IsNullOrEmpty(precoVenda))
                    throw new Exception("O campo Preço de Venda é obrigatório!!!");
                if (!Decimal.TryParse(precoVenda, out numDecimal))
                    throw new Exception("Digite um Preço de Venda Válido ex: 10,34");

                objProdutoDAO = new ProdutoDAO();
                if (int.TryParse(id, out numInt) && objProduto._ProdutoID != 0)
                {
                    try
                    {
                        if (String.IsNullOrEmpty(entradaEstoque))
                            objProduto._EntradaEstoque = 0;   
                        if (!int.TryParse(entradaEstoque, out numInt))
                            throw new Exception("O campo Entrada de Estoque só aceita Números!!!");

                        objProduto._Estoque = objProduto._EntradaEstoque + objProduto._Estoque;
                        objProdutoDAO.Alterar(objProduto);
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
                        if (String.IsNullOrEmpty(entradaEstoque))
                            throw new Exception("O campo Entrada de Estoque é Obrigatório!!!");
                        if (!int.TryParse(entradaEstoque, out numInt))
                            throw new Exception("O campo Entrada de Estoque só aceita Números!!!");

                        objProduto._Estoque = objProduto._EntradaEstoque;
                        objProdutoDAO.Gravar(objProduto);
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
        /// Método responsável em baixar os produtos que sairem da loja.
        /// </summary>
        /// <param name="produto">Parâmetro que recebe as informaçõs do produto com a quantidade que saiu.</param>
        public void Baixar(Produto produto)
        {
            try
            {
                objProduto = new Produto();
                objProdutoDAO = new ProdutoDAO();
                objProduto = objProdutoDAO.BuscarProduto(produto);
                objProduto._Estoque = objProduto._Estoque - produto._EntradaEstoque;
                objProdutoDAO.Alterar(objProduto);
            }
            catch (Exception erro)
            {

                throw new Exception("Não foi possível Alterar. erro:" + erro.Message);
            }
        }
        /// <summary>
        ///   Método que recebe um id e chama o método excluir da camada DAO.
        /// </summary>
        /// <param name="objProduto">Parâmetro que recebe o id do Produto.</param>
        public void Excluir(Produto objProduto)
        {
            try
            {
                objProdutoDAO = new ProdutoDAO();
                objProdutoDAO.Excluir(objProduto);
            }
            catch (Exception erro)
            {
                throw new Exception("Não foi Possível Excluir. erro: " + erro.Message);

            }
        }

        #endregion

        #region BUSCAS
        /// <summary>
        ///  Método responsável em buscar uma lista de Produtos.
        /// </summary>
        /// <param name="produto">Parâmetro que recebe o nome do produto que deseja buscar.</param>
        /// <returns>Retorna uma lista com os produtos encontradas.</returns>
        public DataTable BuscarListaProdutoBaixo(Produto produto)
        {
            try
            {
                DataRow dr;
                DataTable dt = new DataTable();
                listaProduto = new List<Produto>();

                dt.Columns.Add("ProdutoID");
                dt.Columns.Add("Nome do Produto");
                dt.Columns.Add("Preço");
                dt.Columns.Add("Estoque");
                dt.Columns.Add("Marca/Modelo");
                dt.Columns.Add("Situação");

                objProdutoDAO = new ProdutoDAO();
                listaProduto = objProdutoDAO.BuscarListaProduto(produto);

                // Percorre a lista com o resultado da consulta

                if (listaProduto != null)
                {
                    foreach (Produto objProduto in listaProduto)
                    {
                        dr = dt.NewRow();  

                        if (objProduto._EstoqueMinimo >= objProduto._Estoque)
                        {
                            dr["ProdutoID"] = objProduto._ProdutoID;
                            dr["Nome do Produto"] = objProduto._NomeProduto;
                            dr["Preço"] = objProduto._PrecoVenda;
                            dr["Estoque"] = objProduto._Estoque;
                            dr["Marca/Modelo"] = objProduto._MarcaModelo;
                            dr["Situação"] = "Baixo";
                            dt.Rows.Add(dr);
                        }
                    }
                }
                return dt;
            }
            catch (Exception erro)
            {
                throw new Exception("Não foi Possível Localizar o Produto. erro: " + erro.Message);

            }


        }
        /// <summary>
        ///  Método responsável em buscar uma lista de Produtos.
        /// </summary>
        /// <param name="produto">Parâmetro que recebe o nome do produto que deseja buscar.</param>
        /// <returns>Retorna uma lista com os produtos encontradas.</returns>
        public DataTable BuscarListaProduto(Produto produto)
        {
            try
            {
                DataRow dr;
                DataTable dt = new DataTable();
                listaProduto = new List<Produto>();

                dt.Columns.Add("ProdutoID");
                dt.Columns.Add("Nome do Produto");
                dt.Columns.Add("Preço");
                dt.Columns.Add("Estoque");
                dt.Columns.Add("Marca/Modelo");
                dt.Columns.Add("Situação");

                objProdutoDAO = new ProdutoDAO();
                listaProduto = objProdutoDAO.BuscarListaProduto(produto);

                // Percorre a lista com o resultado da consulta

                if (listaProduto != null)
                {
                    foreach (Produto objProduto in listaProduto)
                    {
                        dr = dt.NewRow();

                        dr["ProdutoID"] = objProduto._ProdutoID;
                        dr["Nome do Produto"] = objProduto._NomeProduto;
                        dr["Preço"] = objProduto._PrecoVenda;
                        dr["Estoque"] = objProduto._Estoque;
                        dr["Marca/Modelo"] = objProduto._MarcaModelo;
                        if (objProduto._EstoqueMinimo >= objProduto._Estoque)
                            dr["Situação"] = "Baixo";
                        else
                            dr["Situação"] = "Ok";
                        dt.Rows.Add(dr);
                    }
                }
                return dt;
            }
            catch (Exception erro)
            {
                throw new Exception("Não foi Possível Localizar o Produto. erro: " + erro.Message);

            }


        }
        /// <summary>
        ///  Método responsável em buscar um produto especifico.
        /// </summary>
        /// <param name="produto">Parâmetro que recebe o id do produto que deseja buscar.</param>
        /// <returns>Retorna um objeto preenchido com as informações de um Produto especifico.</returns>
        public Produto BuscarProduto(Produto produto)
        {
            try
            {
                objProdutoDAO = new ProdutoDAO();
                objProduto = new Produto();
                objProduto = objProdutoDAO.BuscarProduto(produto);
                return objProduto;
            }
            catch (Exception erro)
            {
                throw new Exception("Não foi Possível Localizar o Produto. erro: " + erro.Message);

            }

        }

        #endregion

        #region METODOS Usados pelo subordinados a Ordem de Serviço


        /// <summary>
        /// Metodo responsável em adicionar o produto no estoque
        /// </summary>
        /// <param name="produto">Parâmetro que recebe a nova quantidade do produto.</param>
        public void Somar(Produto produto)
        {
            try
            {
                objProduto = new Produto();
                objProdutoDAO = new ProdutoDAO();
                objProduto = objProdutoDAO.BuscarProduto(produto);
                objProduto._Estoque = objProduto._Estoque + produto._EntradaEstoque;
                objProdutoDAO.Alterar(objProduto);
            }
            catch (Exception erro)
            {

                throw new Exception("Não foi possível Alterar. erro:" + erro.Message);
            }
        }

        #endregion
    }
}
