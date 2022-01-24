using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Negocio.Model;

namespace Negocio.DAO
{    /// <summary>
    ///Classe DAO, responsável por fazer as persistências na tabela de Produto no banco. 
    /// </summary>
    public class ProdutoDAO
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
        IList<Produto> listaProduto;

        #endregion

        #region CRUD
         /// <summary>
        ///  Método que converte o objeto em liguagem sql e grava no banco as informações.
         /// </summary>
        /// <param name="objProduto">Informações do produto recebida como parâmetro.</param>
        public void Gravar(Produto objProduto)
        {
            try
            {
                sql.Remove(0, sql.Length);
                comando.Parameters.Clear();

                sql.Append("insert into Produto(estoqueMinimo,marcaModelo,nomeFornecedor, nomeProduto,");
                sql.Append("entradaEstoque, observacao, telefoneFornecedor,");
                sql.Append("precoCompra, precoVenda, estoque) values(@estoqueMinimo, @marcaModelo, @nomeFornecedor,");
                sql.Append("@nomeProduto, @entradaEstoque, @observacao,");
                sql.Append("@telefoneFornecedor, @precoCompra,	@precoVenda, @estoque)");

                comando.CommandText = sql.ToString();

                comando.Parameters.AddWithValue("@estoqueMinimo", objProduto._EstoqueMinimo);
                comando.Parameters.AddWithValue("@marcaModelo", objProduto._MarcaModelo);
                comando.Parameters.AddWithValue("@nomeFornecedor", objProduto._NomeFornecedor);
                comando.Parameters.AddWithValue("@estoque", objProduto._Estoque);
                comando.Parameters.AddWithValue("@nomeProduto", objProduto._NomeProduto);
                comando.Parameters.AddWithValue("@entradaEstoque", objProduto._EntradaEstoque);
                comando.Parameters.AddWithValue("@observacao", objProduto._Observacao);               
                comando.Parameters.AddWithValue("@telefoneFornecedor", objProduto._TelefoneFornecedor);
                comando.Parameters.AddWithValue("@precoCompra", objProduto._PrecoCompra);
                comando.Parameters.AddWithValue("@precoVenda", objProduto._PrecoVenda);
                
                ConexaoBanco.Crud(comando);
            }
            catch (Exception erro)
            {
                throw new Exception(erro.Message);
            }

        }
        /// <summary>
        ///  Método que converte o objeto em liguagem sql persistindo no banco as informações para alteração do produto. 
        /// </summary>
        /// <param name="objProduto">Informações do produto recebida como parâmetro</param>
        public void Alterar(Produto objProduto)
        {
            try
            {
                sql.Remove(0, sql.Length);
                comando.Parameters.Clear();

                sql.Append("update Produto set estoqueMinimo=@estoqueMinimo,marcaModelo=@marcaModelo,nomeFornecedor=@nomeFornecedor,");
                sql.Append("nomeProduto=@nomeProduto, entradaEstoque=@entradaEstoque, observacao=@observacao, estoque=@estoque,");
                sql.Append("telefoneFornecedor=@telefoneFornecedor, precoCompra=@precoCompra,");
                sql.Append("precoVenda=@precoVenda where produtoID=@produtoID");

                comando.CommandText = sql.ToString();

                comando.Parameters.AddWithValue("@estoqueMinimo", objProduto._EstoqueMinimo);
                comando.Parameters.AddWithValue("@marcaModelo", objProduto._MarcaModelo);
                comando.Parameters.AddWithValue("@nomeFornecedor", objProduto._NomeFornecedor);
                comando.Parameters.AddWithValue("@estoque", objProduto._Estoque);
                comando.Parameters.AddWithValue("@nomeProduto", objProduto._NomeProduto);
                comando.Parameters.AddWithValue("@entradaEstoque", objProduto._EntradaEstoque);
                comando.Parameters.AddWithValue("@observacao", objProduto._Observacao);               
                comando.Parameters.AddWithValue("@telefoneFornecedor", objProduto._TelefoneFornecedor);
                comando.Parameters.AddWithValue("@precoCompra", objProduto._PrecoCompra);
                comando.Parameters.AddWithValue("@precoVenda", objProduto._PrecoVenda);
                comando.Parameters.AddWithValue("@produtoID", objProduto._ProdutoID);
                
                ConexaoBanco.Crud(comando);
            }
            catch (Exception erro)
            {
                throw new Exception(erro.Message);
            }


        }
        /// <summary>
        ///  Método responsável em fazer a exclusão do produto no banco.
        /// </summary>
        /// <param name="objProduto">objeto preenchido com o id e passado como parâmetro para exclusão no banco.</param>
        public void Excluir(Produto objProduto)
        {
            try
            {
                sql.Remove(0, sql.Length);
                comando.Parameters.Clear();

                sql.Append("Delete from Produto where produtoID = @produtoID");

                comando.CommandText = sql.ToString();

                comando.Parameters.AddWithValue("produtoID", objProduto._ProdutoID);
                ConexaoBanco.Crud(comando);
            }
            catch (Exception)
            {
                throw new Exception("As informações da moto está vincunlado a uma Ordem de Serviço!!!");

            }
        }

        #endregion

        #region BUSCAS
        /// <summary>
        /// Método responsável em fazer a busca dos produtos solicitados.
        /// </summary>
        /// <param name="produto">Nome do serviço passado com parâmetro dentro do objeto produto.</param>
        /// <returns>Este método retorna uma lista de produto.</returns>
        public IList<Produto> BuscarListaProduto(Produto produto)
        {
            try
            {
                sql.Remove(0, sql.Length);
                comando.Parameters.Clear();

                sql.Append("Select * from Produto where nomeProduto Like @nomeProduto");
                comando.Parameters.AddWithValue("@nomeProduto", "%" + produto._NomeProduto + "%");
                comando.CommandText = sql.ToString();
                SqlDataReader dr = ConexaoBanco.Selecionar(comando);
                listaProduto = new List<Produto>();

                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        Produto objProduto = new Produto();
                        objProduto._EstoqueMinimo = (int)dr["estoqueMinimo"];
                        objProduto._MarcaModelo = (string)dr["marcaModelo"];
                        objProduto._NomeFornecedor = (string)dr["nomeFornecedor"];
                        objProduto._NomeProduto = (string)dr["nomeProduto"];
                        objProduto._EntradaEstoque = (int)dr["entradaEstoque"];
                        objProduto._Observacao = (string)dr["observacao"];                        
                        objProduto._ProdutoID = (int)dr["produtoID"];
                        objProduto._TelefoneFornecedor = (string)dr["telefoneFornecedor"];
                        objProduto._PrecoCompra = (decimal)dr["precoCompra"];
                        objProduto._PrecoVenda = (decimal)dr["precoVenda"];
                        objProduto._Estoque = (int)dr["estoque"];                      
                        listaProduto.Add(objProduto);

                    }
                }
                else
                {
                    listaProduto = null;
                }
                return listaProduto;
            }
            catch (Exception erro)
            {
                throw new Exception(erro.Message);
            }
        }
        /// <summary>
        /// Método responsável em fazer a busca do produto solicitados.
        /// </summary>
        /// <param name="produto">Id do serviço passado como parâmetro dentro do objeto serviço para fazer a busca do produto.</param>
        /// <returns>Este método retorna um objeto do tipo serviço.</returns>
        public Produto BuscarProduto(Produto produto)
        {
            try
            {
                sql.Remove(0, sql.Length);
                comando.Parameters.Clear();

                sql.Append("Select * from Produto where produtoID = @produtoID");
                comando.Parameters.AddWithValue("@produtoID", produto._ProdutoID);

                comando.CommandText = sql.ToString();

                SqlDataReader dr = ConexaoBanco.Selecionar(comando);
                Produto objProduto = new Produto();
                if (dr.HasRows)
                {
                    dr.Read();

                    objProduto._EstoqueMinimo = (int)dr["estoqueMinimo"];
                    objProduto._MarcaModelo = (string)dr["marcaModelo"];
                    objProduto._NomeFornecedor = (string)dr["nomeFornecedor"];
                    objProduto._NomeProduto = (string)dr["nomeProduto"];
                    objProduto._EntradaEstoque = (int)dr["entradaEstoque"];
                    objProduto._Observacao = (string)dr["observacao"];                    
                    objProduto._ProdutoID = (int)dr["produtoID"];
                    objProduto._TelefoneFornecedor = (string)dr["telefoneFornecedor"];
                    objProduto._PrecoCompra = (decimal)dr["precoCompra"];
                    objProduto._PrecoVenda = (decimal)dr["precoVenda"];
                    objProduto._Estoque = (int)dr["estoque"];
                   
                }
                else
                {
                    objProduto = null;
                }
                return objProduto;
            }
            catch (Exception erro)
            {
                throw new Exception(erro.Message);
            }
        }

        #endregion
    }
}
