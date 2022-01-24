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
    public class OrdemServicoBO
    {
        #region ASSINATURA DOS METODOS, CRIAÇÃO DE OBJETO E CRIAÇÃO DE VARIÁVEL.
        /// <summary>
        /// Cria o objeto para reservar um espaço no disco após instanciado.
        /// </summary>
        OrdemServicoDAO objOrdemServicoDAO;       
        static OrdemServico objOrdemServico;
        static ItemServico objItemServico;
        ItemServicoBO objItemServicoBO;
        static ItemProduto objItemProduto;
        ItemProdutoBO objItemProdutoBO;
        /// <summary>
        /// Cria uma lista do Tipo da Classe. 
        ///</summary>
        IList<OrdemServico> listaOrdemServico;
        /// <summary>
        /// Cria as variaveis.
        /// </summary>
        string id, dataFechamento, variavel;
        int numInt;
        decimal numDecimal;
        DateTime data;
        #endregion        

        #region CRUD
         /// <summary>
        /// Método responsável em preparar os dados para gravar ou alterar enviando para a camada DAO. 
        /// </summary>
        /// <param name="objOrdemServico">Parâmetro que recebe as informações da ordem de serviço.</param>
         /// <returns></returns>
        public int Gravar(OrdemServico objOrdemServico)
        {
            try
            {
                dataFechamento = objOrdemServico._DataFechamentoOS.ToString();
                id = objOrdemServico._OrdemServicoID.ToString();
                variavel = objOrdemServico._Cliente._ClienteID.ToString();
                if (!decimal.TryParse(variavel, out numDecimal))
                    throw new Exception("Selecione um Cliente na Lista de Busca para esta Ordem de Serviço!!!");
                variavel = objOrdemServico._Moto._MotoID.ToString();
                if (!decimal.TryParse(variavel, out numDecimal))
                    throw new Exception("Selecione uma Moto na Lista de Busca para esta Ordem de Serviço!!!");
                if (!DateTime.TryParse(dataFechamento, out data))
                    objOrdemServico._DataFechamentoOS = null;
                objOrdemServicoDAO = new OrdemServicoDAO();
                int idOrdemServico;
                if (int.TryParse(id, out numInt) && objOrdemServico._OrdemServicoID != 0)
                {
                    objOrdemServicoDAO.Alterar(objOrdemServico);
                    idOrdemServico = 0;
                }
                else
                {
                    idOrdemServico = objOrdemServicoDAO.Gravar(objOrdemServico);
                }
                return idOrdemServico;
            }
            catch (Exception erro)
            {
                throw new Exception(erro.Message);
            }

        }
         /// <summary>
        /// Método que recebe um id e chama o método excluir da camada DAO.  
         /// </summary>
        /// <param name="objOrdemServico">Parâmetro que recebe o id da ordem de serviço.</param>
        public void Excluir(OrdemServico objOrdemServico)
        {
            try
            {
                objItemProdutoBO = new ItemProdutoBO();
                objOrdemServicoDAO = new OrdemServicoDAO();
                objItemServicoBO = new ItemServicoBO();
                if (objOrdemServico._DataFechamentoOS == null)
                {
                    if (objOrdemServico._ListaItemProduto != null)
                    {
                        foreach (var item in objOrdemServico._ListaItemProduto)
                        {
                            objItemProduto = new ItemProduto(objOrdemServico);
                            objItemProduto._ItemProdutoID = item._ItemProdutoID;
                            objItemProduto._ObjProduto._ProdutoID = item._ObjProduto._ProdutoID;
                            objItemProduto._QuantidadeItem = item._QuantidadeItem;
                            objItemProdutoBO.Excluir(objItemProduto);
                        }
                    }

                    if (objOrdemServico._ListaItemServico != null)
                    {
                        foreach (var item in objOrdemServico._ListaItemServico)
                        {
                            objItemServico = new ItemServico(objOrdemServico);
                            objItemServico._ItemServicoID = item._ItemServicoID;
                            objItemServicoBO.Excluir(objItemServico);
                        }
                    }

                    objOrdemServicoDAO.Excluir(objOrdemServico);

                }
                else
                {
                    throw new Exception("Esta ordem de serviço não pode ser Excluida pois já foi finalizada!!!");
                }
            }
            catch (Exception erro)
            {
                throw new Exception(erro.Message);
            }
        }

        #endregion

        #region BUSCAS
         /// <summary>
        /// Método responsável em buscar uma lista de ordem de serviço.
        /// </summary>
        /// <param name="buscarOS">Parâmetro que recebe uma string com o tipe de ordem de serviço.</param>
        /// <returns>Retorna uma lista com as ordem de serviços encontradas.</returns>
        public DataTable BuscarListaOrdemServico(string buscarOS)
        {
            try
            {
                objOrdemServicoDAO = new OrdemServicoDAO();

                DataRow dr;
                DataTable dt = new DataTable();
                listaOrdemServico = new List<OrdemServico>();

                dt.Columns.Add("osID");
                dt.Columns.Add("Cliente");
                dt.Columns.Add("Mecânico");
                dt.Columns.Add("Telefone");
                dt.Columns.Add("Placa");
                dt.Columns.Add("Situação");
                dt.Columns.Add("Data de Início");


                listaOrdemServico = objOrdemServicoDAO.BuscarListaOrdemServico(buscarOS);

                // Percorre a lista com o resultado da consulta

                if (listaOrdemServico != null)
                {
                    foreach (OrdemServico objOrdemServico in listaOrdemServico)
                    {
                        dr = dt.NewRow();
                        dr["Data de Início"] = objOrdemServico._DataAberturaOS.ToString();
                        dr["osID"] = objOrdemServico._OrdemServicoID;
                        dr["Cliente"] = objOrdemServico._Cliente._Nome;
                        dr["Mecânico"] = objOrdemServico._Funcionario._Nome;
                        dr["Telefone"] = objOrdemServico._Cliente._Telefone1;
                        dr["Placa"] = objOrdemServico._Moto._Placa;
                        if (objOrdemServico._DataFechamentoOS != null)
                            dr["Situação"] = "FINALIZADA";
                        else
                            dr["Situação"] = "ABERTA";
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

        public DataTable BuscarListaOrdemServicoData(string buscarOS , DateTime data)
        {
            try
            {
                objOrdemServicoDAO = new OrdemServicoDAO();

                DataRow dr;
                DataTable dt = new DataTable();
                listaOrdemServico = new List<OrdemServico>();

                dt.Columns.Add("osID");
                dt.Columns.Add("Cliente");
                dt.Columns.Add("Mecânico");
                dt.Columns.Add("Telefone");
                dt.Columns.Add("Placa");
                dt.Columns.Add("Situação");
                dt.Columns.Add("Data de Início");


                listaOrdemServico = objOrdemServicoDAO.BuscarListaOrdemServico(buscarOS);

                // Percorre a lista com o resultado da consulta

                if (listaOrdemServico != null)
                {
                    foreach (OrdemServico objOrdemServico in listaOrdemServico)
                    {
                        //if (objOrdemServico._DataAberturaOS.Year == data.Year && objOrdemServico._DataAberturaOS.Month == data.Month && objOrdemServico._DataAberturaOS.Day == data.Day)
                        if (objOrdemServico._DataAberturaOS.Date == data.Date)
                        {
                            dr = dt.NewRow();
                            dr["Data de Início"] = objOrdemServico._DataAberturaOS.ToString();
                            dr["osID"] = objOrdemServico._OrdemServicoID;
                            dr["Cliente"] = objOrdemServico._Cliente._Nome;
                            dr["Mecânico"] = objOrdemServico._Funcionario._Nome;
                            dr["Telefone"] = objOrdemServico._Cliente._Telefone1;
                            dr["Placa"] = objOrdemServico._Moto._Placa;
                            if (objOrdemServico._DataFechamentoOS != null)
                                dr["Situação"] = "FINALIZADA";
                            else
                                dr["Situação"] = "ABERTA";
                            dt.Rows.Add(dr);
                        }
                       
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
        /// Método responsável em buscar uma ordem de serviço especifica.
        /// </summary>
        /// <param name="ordem">Recebe o id da ordem de serviço que deseja buscar.</param>
        /// <returns>Retorna um objeto com preenchido com as informações da ordem de serviço selecionada.</returns>
        public OrdemServico BuscarOrdemServico(OrdemServico ordem)
        {
            try
            {
                objOrdemServicoDAO = new OrdemServicoDAO();
                objOrdemServico = new OrdemServico();
                objOrdemServico = objOrdemServicoDAO.BuscarOrdemServico(ordem);
                if (objOrdemServico._ListaItemProduto != null)
                {
                    foreach (var itemProduto in objOrdemServico._ListaItemProduto)
                    {
                        itemProduto._Total = itemProduto._QuantidadeItem * itemProduto._Preco;
                        objOrdemServico._ValorTotalProduto = objOrdemServico._ValorTotalProduto + itemProduto._Total;
                    }
                }
                if (objOrdemServico._ListaItemServico != null)
                {
                    foreach (var itemServico in objOrdemServico._ListaItemServico)
                    {
                        objOrdemServico._ValorTotalServicos = objOrdemServico._ValorTotalServicos + itemServico._Preco;
                    }
                }
                objOrdemServico._ValorTotal = objOrdemServico._ValorTotalProduto + objOrdemServico._ValorTotalServicos;

                if ((objOrdemServico._TipoDesconto == 0) && (objOrdemServico._Desconto != null))
                    objOrdemServico._TotalOSdesconto = objOrdemServico._ValorTotal - Convert.ToDecimal(objOrdemServico._Desconto);

                else if ((objOrdemServico._TipoDesconto == 1) && (objOrdemServico._Desconto != null))
                    objOrdemServico._TotalOSdesconto = objOrdemServico._ValorTotal * ((100 - Convert.ToDecimal(objOrdemServico._Desconto)) / 100);

                else
                    objOrdemServico._TotalOSdesconto = objOrdemServico._ValorTotal;                  
                return objOrdemServico;
            }
            catch (Exception erro)
            {
                throw new Exception(erro.Message);
            }


        }

        #endregion

        #region METODOS DE APOIO
        /// <summary>
        /// Método que recebe o objeto OS e ler a lista de item de serviço e calcula.
        /// </summary>
        /// <param name="OS">Parâmetro que recebe a lista de serviços</param>
        /// <returns>Retorna a lista de serviços calculada.</returns>
        public OrdemServico CalcularServico(OrdemServico OS)
        {
            try
            {
                OS._ValorTotalServicos = 0;
                OS._ValorTotal = 0;
                if (OS._ListaItemServico != null)
                {
                    foreach (var objItemS in OS._ListaItemServico)
                    {
                        OS._ValorTotalServicos = OS._ValorTotalServicos + objItemS._Preco;
                    }
                }
                variavel = OS._ValorTotalProduto.ToString();
                if (!decimal.TryParse(variavel, out numDecimal))
                    OS._ValorTotalProduto = 0;
                OS._ValorTotal = OS._ValorTotalProduto + OS._ValorTotalServicos;
                OS._TotalOSdesconto = OS._ValorTotal;

                return OS;
            }
            catch (Exception erro)
            {
                throw new Exception(erro.Message);
            }

        }
        /// <summary>
        /// Método que recebe o objeto OS e ler a lista de item de produto e calcula.
        /// </summary>
        /// <param name="OS">Parâmetro que recebe a lista de produtos</param>
        /// <returns>Retorna a lista de produtos calculado.</returns>
        public OrdemServico CaucularProduto(OrdemServico OS)
        {
            try
            {
                decimal x;
                OS._ValorTotalProduto = 0;
                OS._ValorTotal = 0;

                if (OS._ListaItemProduto != null)
                {
                    foreach (var objItemP in OS._ListaItemProduto)
                    {
                        OS._ValorTotalProduto += objItemP._Total;
                    }
                }
                variavel = OS._ValorTotalServicos.ToString();
                if (!decimal.TryParse(variavel, out numDecimal))
                    OS._ValorTotalServicos = 0;
                OS._ValorTotal = OS._ValorTotalProduto + OS._ValorTotalServicos;
                OS._TotalOSdesconto = OS._ValorTotal;

                return OS;
            }
            catch (Exception erro)
            {
                throw new Exception(erro.Message);
            }
           
        }

        #endregion
    }
}

