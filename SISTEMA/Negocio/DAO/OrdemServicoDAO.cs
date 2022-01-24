using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Negocio.Model;
using System.Data;


namespace Negocio.DAO
{
    /// <summary>
    ///Classe DAO, responsável por fazer as persistências na tabela de serviço no banco. 
    /// </summary>
    public class OrdemServicoDAO
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
        /// Cria uma lista para receber as informações da Ordem de Serviço buscados no banco.
        /// </summary>
        IList<OrdemServico> listaOrdemServico;
        /// <summary>
        /// Cria um objeto do tipo Funcionário
        /// </summary>
        Funcionario objFuncionario;
        /// <summary>
        /// Cria um objeto do tipo Cliente.
        /// </summary>
        Cliente objCliente;
        /// <summary>
        ///  Cria um objeto do tipo Moto.
        /// </summary>
        Moto objMoto;
        /// <summary>
        /// Cria um objeto do tipo funcionário DAO para acessar metodos da classe DAO.
        /// </summary>
        FuncionarioDAO objFuncionarioDAO;
        /// <summary>
        /// Cria um objeto do tipo cliente DAO para acessar metodos da classe DAO.
        /// </summary>
        ClienteDAO objClienteDAO;
        /// <summary>
        /// Cria um objeto do tipo moto DAO para acessar metodos da classe DAO.
        /// </summary>
        MotoDAO objMotoDAO;
        /// <summary>
        /// Cria um objeto do tipo item Produto DAO para acessar metodos da classe DAO.
        /// </summary>
        ItemProdutoDAO objItemProdutoDAO;
        /// <summary>
        /// Cria um objeto do tipo item Serviço DAO para acessar metodos da classe DAO.
        /// </summary>
        ItemServicoDAO objItemServicoDAO;
        /// <summary>
        /// Cria um objeto do tipo Ordem de Serviço.
        /// </summary>
        OrdemServico objOrdemServico;
        /// <summary>
        /// Cria um objeto do tipo Ordem de Item Serviço.
        /// </summary>
        ItemProduto objItemProduto;
        /// <summary>
        /// Cria um objeto do tipo Ordem de item Produto.
        /// </summary>
        ItemServico objItemServico;
        /// <summary>
        /// Uma string usada como variavel.
        /// </summary>
        string verificador;
        /// <summary>
        /// Uma variavel do tipo data usada para testar se objeto preenchido e do tipo data.
        /// </summary>
        DateTime data;


        #endregion

        #region CRUD
         /// <summary>
        ///  Método que converte o objeto em liguagem sql e grava no banco as informações.
         /// </summary>
        /// <param name="objOrdemServico">Informações do produto recebida como parâmetro.</param>
         /// <returns>Após gravar as informações retorna o int id da ordem de serviço.</returns>
        public int Gravar(OrdemServico objOrdemServico)
        {
            try
            {
                sql.Remove(0, sql.Length);
                comando.Parameters.Clear();

                sql.Append("Insert Into OrdemServico(dataAberturaOS, km, observacao, clienteID,funcionarioID,motoID)");
                sql.Append("values (@dataAberturaOS, @km, @observacao, @clienteID,@funcionarioID,@motoID) ");
                sql.Append("SET @ordemServicoID = SCOPE_IDENTITY()");

                comando.CommandText = sql.ToString();

                comando.Parameters.AddWithValue("@dataAberturaOS", objOrdemServico._DataAberturaOS);
                comando.Parameters.AddWithValue("@km", objOrdemServico._Km);
                comando.Parameters.AddWithValue("@observacao", objOrdemServico._Observacao);
                comando.Parameters.AddWithValue("@clienteID", objOrdemServico._Cliente._ClienteID);
                comando.Parameters.AddWithValue("@funcionarioID", objOrdemServico._Funcionario._FuncionarioID);
                comando.Parameters.AddWithValue("@motoID", objOrdemServico._Moto._MotoID);

                comando.Parameters.AddWithValue("@ordemServicoID", 0).Direction = ParameterDirection.Output;

                ConexaoBanco.Crud(comando);
                int idOrdemSercico = Convert.ToInt32(comando.Parameters["@ordemServicoID"].Value);
                return idOrdemSercico;
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }            

        }
         /// <summary>
        ///   Método que converte o objeto em liguagem sql persistindo no banco as informações para alteração das informações do produto. 
         /// </summary>
        /// <param name="objOrdemServico">Informações da ordem de serviço recebida como parâmetro pelo objeto</param>
        public void Alterar(OrdemServico objOrdemServico)
        {
            try
            {
                sql.Remove(0, sql.Length);
                comando.Parameters.Clear();

                sql.Append("update OrdemServico set dataAberturaOS=@dataAberturaOS, km=@km, observacao=@observacao,dataFechamentoOS=@dataFechamentoOS,");
                sql.Append("clienteID=@clienteID,funcionarioID=@funcionarioID,motoID=@motoID, ");
                sql.Append("desconto=@desconto,");
                sql.Append("tipoDesconto=@tipoDesconto where ordemServicoID=@ordemServicoID");

                comando.CommandText = sql.ToString();

                comando.Parameters.AddWithValue("@dataAberturaOS", objOrdemServico._DataAberturaOS);
                comando.Parameters.AddWithValue("@km", objOrdemServico._Km);
                comando.Parameters.AddWithValue("@observacao", objOrdemServico._Observacao);
                comando.Parameters.AddWithValue("@clienteID", objOrdemServico._Cliente._ClienteID);
                comando.Parameters.AddWithValue("@funcionarioID", objOrdemServico._Funcionario._FuncionarioID);
                comando.Parameters.AddWithValue("@motoID", objOrdemServico._Moto._MotoID);
                comando.Parameters.AddWithValue("@ordemServicoID", objOrdemServico._OrdemServicoID);
                if (objOrdemServico._DataFechamentoOS == null)
                    comando.Parameters.AddWithValue("@dataFechamentoOS", DBNull.Value);
                else
                    comando.Parameters.AddWithValue("@dataFechamentoOS", objOrdemServico._DataFechamentoOS);
                if (objOrdemServico._Desconto == null)
                    comando.Parameters.AddWithValue("@desconto", DBNull.Value);
                else
                    comando.Parameters.AddWithValue("@desconto", objOrdemServico._Desconto);

                if (objOrdemServico._TipoDesconto == null)
                    comando.Parameters.AddWithValue("@tipoDesconto", DBNull.Value);
                else
                    comando.Parameters.AddWithValue("@tipoDesconto", objOrdemServico._TipoDesconto);

                ConexaoBanco.Crud(comando);
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }  
        }
        /// <summary>
        ///   Método responsável em fazer a exclusão da ordem de serviço no banco.
        /// </summary>
        /// <param name="objOrdemServico">objeto preenchido com o id e passado como parâmetro para exclusão no banco.</param>
        public void Excluir(OrdemServico objOrdemServico)
        {
            try
            {
                sql.Remove(0, sql.Length);
                comando.Parameters.Clear();

                sql.Append("Delete from OrdemServico where ordemServicoID = @ordemServicoID");

                comando.CommandText = sql.ToString();

                comando.Parameters.AddWithValue("@ordemServicoID", objOrdemServico._OrdemServicoID);
                ConexaoBanco.Crud(comando);
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }            
 
        }

        #endregion

        #region BUSCAS
         /// <summary>
        /// Método responsável em fazer a busca das ordem de serviço solicitadas.
         /// </summary>
         /// <param name="buscarOS">string recebida como parâmetro para ser usada como condição para o tipo de busca desejada.</param>
         /// <returns>retorna uma lista de ordem de serviços conforme solicitação</returns>
        public IList<OrdemServico> BuscarListaOrdemServico(string buscarOS)
        {
            try
            {
                sql.Remove(0, sql.Length);
                comando.Parameters.Clear();
                switch (buscarOS)
                {
                    case "todos":
                        {
                            sql.Append("Select * from OrdemServico order by dataAberturaOS desc");
                            break;
                        }
                    case "abertas":
                        {
                            sql.Append("Select * from OrdemServico where dataFechamentoOS is null order by dataAberturaOS desc");
                            break;
                        }
                    case "fechada":
                        {
                            sql.Append("Select * from OrdemServico where dataFechamentoOS is not null order by dataAberturaOS desc");

                            break;
                        }
                }
                comando.CommandText = sql.ToString();
                SqlDataReader dr = ConexaoBanco.Selecionar(comando);

                listaOrdemServico = new List<OrdemServico>();
                objClienteDAO = new ClienteDAO();
                objFuncionarioDAO = new FuncionarioDAO();
                objMotoDAO = new MotoDAO();
                objItemProdutoDAO = new ItemProdutoDAO();
                objItemServicoDAO = new ItemServicoDAO();

                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        objOrdemServico = new OrdemServico();
                        objItemProduto = new ItemProduto(objOrdemServico);
                        objItemServico = new ItemServico(objOrdemServico);
                        objFuncionario = new Funcionario();
                        objCliente = new Cliente();
                        objMoto = new Moto();
                        objOrdemServico._DataAberturaOS = (DateTime)dr["dataAberturaOS"];
                        objOrdemServico._Km = (string)dr["km"];
                        objOrdemServico._Observacao = (string)dr["observacao"];
                        objOrdemServico._OrdemServicoID = (int)dr["ordemServicoID"];
                        string dataFinal = dr["dataFechamentoOS"].ToString();
                        if (DateTime.TryParse(dataFinal, out data))
                            objOrdemServico._DataFechamentoOS = (DateTime)dr["dataFechamentoOS"];
                        objFuncionario._FuncionarioID = (int)dr["funcionarioID"];
                        objOrdemServico._Funcionario = objFuncionarioDAO.BuscarFuncionario(objFuncionario);
                        objMoto._MotoID = (int)dr["motoID"];
                        objOrdemServico._Moto = objMotoDAO.BuscarMoto(objMoto);
                        objCliente._ClienteID = (int)dr["clienteID"];
                        objOrdemServico._Cliente = objClienteDAO.BuscarCliente(objCliente);
                        verificador = dr["desconto"].ToString();
                        if (!String.IsNullOrEmpty(verificador))
                            objOrdemServico._Desconto = (decimal)dr["desconto"];
                        verificador = dr["tipoDesconto"].ToString();
                        if (!String.IsNullOrEmpty(verificador))
                            objOrdemServico._TipoDesconto = (int)dr["tipoDesconto"];
                        objItemProduto._OrdemServico._OrdemServicoID = (int)dr["ordemServicoID"];
                        objOrdemServico._ListaItemProduto = objItemProdutoDAO.buscarListaItemProduto(objItemProduto);
                        objItemServico._OrdemServico._OrdemServicoID = (int)dr["ordemServicoID"];
                        objOrdemServico._ListaItemServico = objItemServicoDAO.BuscarListaItemServico(objItemServico);

                        listaOrdemServico.Add(objOrdemServico);

                    }

                }
                else
                {
                    listaOrdemServico = null;
                }
                return listaOrdemServico;
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }            

        }
        /// <summary>
        ///  Método que busca uma ordem de serviço.
        /// </summary>
        /// <param name="ordem">parâmetro preenchido com o id para fazer a busca da ordem de serviço especifica.</param>
        /// <returns>retorna um ordem de serviço dentro do objeto.</returns>
        public OrdemServico BuscarOrdemServico(OrdemServico ordem)
        {
            try
            {
                sql.Remove(0, sql.Length);
                comando.Parameters.Clear();

                sql.Append("Select * from ordemServico where ordemServicoID = @ordemServicoID");
                comando.Parameters.AddWithValue("@ordemServicoID", ordem._OrdemServicoID);
                comando.CommandText = sql.ToString();
                SqlDataReader dr = ConexaoBanco.Selecionar(comando);
                listaOrdemServico = new List<OrdemServico>();
                objClienteDAO = new ClienteDAO();
                objFuncionarioDAO = new FuncionarioDAO();
                objMotoDAO = new MotoDAO();
                objItemProdutoDAO = new ItemProdutoDAO();
                objItemServicoDAO = new ItemServicoDAO();

                if (dr.HasRows)
                {
                    dr.Read();

                    objOrdemServico = new OrdemServico();
                    objItemProduto = new ItemProduto(objOrdemServico);
                    objItemServico = new ItemServico(objOrdemServico);
                    objFuncionario = new Funcionario();
                    objCliente = new Cliente();
                    objMoto = new Moto();
                    objOrdemServico._DataAberturaOS = (DateTime)dr["dataAberturaOS"];
                    objOrdemServico._Km = (string)dr["km"];
                    objOrdemServico._Observacao = (string)dr["observacao"];
                    objOrdemServico._OrdemServicoID = (int)dr["ordemServicoID"];
                    string dataFinal = dr["dataFechamentoOS"].ToString();
                    if (DateTime.TryParse(dataFinal, out data))
                        objOrdemServico._DataFechamentoOS = (DateTime)dr["dataFechamentoOS"];
                    objFuncionario._FuncionarioID = (int)dr["funcionarioID"];
                    objOrdemServico._Funcionario = objFuncionarioDAO.BuscarFuncionario(objFuncionario);
                    objMoto._MotoID = (int)dr["motoID"];
                    objOrdemServico._Moto = objMotoDAO.BuscarMoto(objMoto);
                    objCliente._ClienteID = (int)dr["clienteID"];
                    objOrdemServico._Cliente = objClienteDAO.BuscarCliente(objCliente);
                    verificador = dr["desconto"].ToString();
                    if (!String.IsNullOrEmpty(verificador))
                        objOrdemServico._Desconto = (decimal)dr["desconto"];
                    verificador = dr["tipoDesconto"].ToString();
                    if (!String.IsNullOrEmpty(verificador))
                        objOrdemServico._TipoDesconto = (int)dr["tipoDesconto"];
                    objItemProduto._OrdemServico._OrdemServicoID = (int)dr["ordemServicoID"];
                    objOrdemServico._ListaItemProduto = objItemProdutoDAO.buscarListaItemProduto(objItemProduto);
                    objItemServico._OrdemServico._OrdemServicoID = (int)dr["ordemServicoID"];
                    objOrdemServico._ListaItemServico = objItemServicoDAO.BuscarListaItemServico(objItemServico);
                }
                else
                {
                    objOrdemServico = null;
                }
                return objOrdemServico;

            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }            

           
        }

        #endregion

    }
}

