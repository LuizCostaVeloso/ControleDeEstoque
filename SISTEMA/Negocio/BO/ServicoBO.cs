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
    public class ServicoBO
    {
        #region ASSINATURAS DE METODOS E DECRARAÇÕES
        /// <summary>
        /// Cria uma lista do Tipo da Classe. 
        ///</summary>
        IList<Servico> listaServico;
        /// <summary>
        /// Cria um objeto da camada DAO para dar acesso aos metodos inerente da Classe.
        /// </summary>
        ServicoDAO objServicoDAO;
        /// <summary>
        /// Cria o objeto para reservar um espaço no disco após instanciado.
        /// </summary>
        Servico objServico;
        /// <summary>
        /// Cria as variaveis.
        /// </summary>
        string valor, id;
        decimal numDecimal;
        int numInt;
        #endregion
        //---------------------------------///-------------------------///---------------------------------///
        #region CRUD
        /// <summary>
        /// Método responsável em preparar os dados para gravar ou alterar enviando para a camada DAO. 
        /// </summary>
        /// <param name="objServico">Parâmetro que recebe as informações do Serviço.</param>
        public void Gravar(Servico objServico)
        {
            try
            {
                valor = objServico._Preco.ToString();
                id = objServico._ServicoID.ToString();
                objServicoDAO = new ServicoDAO();
                if (String.IsNullOrEmpty(objServico._NomeServico))
                    throw new Exception("O campo Nome é obrigatório!!!");
                if (String.IsNullOrEmpty(objServico._MarcaModelo))
                    throw new Exception("O campo Marca/Modelo é obrigatório!!!");
                //todo REFERENCIA (RELATIVO AO TryParse http://msdn.microsoft.com/en-us/library/9zbda557.aspx)    
                if (String.IsNullOrEmpty(objServico._Preco.ToString()))
                    throw new Exception("O campo Preço é obriatório!!!");
                if (!Decimal.TryParse(valor, out numDecimal))
                    throw new Exception("Digite um Preço Válido ex: 10,34");
                else if (int.TryParse(id, out numInt) && objServico._ServicoID != 0)
                {
                    try
                    {
                        objServicoDAO.Alterar(objServico);
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
                        objServicoDAO.Gravar(objServico);
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
        /// Método que recebe um id e chama o método excluir da camada DAO.
        /// </summary>
        /// <param name="objServico">Parâmetro que recebe o id do Serviço.</param>
        public void Excluir(Servico objServico)
        {
            try
            {
                objServicoDAO = new ServicoDAO();
                objServicoDAO.Excluir(objServico);
            }
            catch (Exception erro)
            {

                throw new Exception("Não foi possível EXCLUIR. erro: " + erro.Message);
            }
        }
        #endregion
        //---------------------------------///-------------------------///---------------------------------///
        #region BUSCAS
        /// <summary>
        /// Método responsável em buscar uma lista de Serviços.
        /// </summary>
        /// <param name="servico">Parâmetro que recebe o nome do serviço.</param>
        /// <returns>Retorna uma lista com os serviços encontradas.</returns>
        public DataTable BuscarListaServico(Servico servico)
        {
            objServicoDAO = new ServicoDAO();
            try
            {
                DataRow dr;
                DataTable dt = new DataTable();
                listaServico = new List<Servico>();

                dt.Columns.Add("ServicoID");
                dt.Columns.Add("Nome do Serviço");
                dt.Columns.Add("Marca/Modelo");
                dt.Columns.Add("Preço");

                listaServico = objServicoDAO.BuscarListaServico(servico);

                // Percorre a lista com o resultado da consulta

                if (listaServico != null)
                {
                    foreach (Servico objServico in listaServico)
                    {
                        dr = dt.NewRow();

                        dr["ServicoID"] = objServico._ServicoID;
                        dr["Nome do Serviço"] = objServico._NomeServico;
                        dr["Preço"] = objServico._Preco;
                        dr["Marca/Modelo"] = objServico._MarcaModelo;
                        dt.Rows.Add(dr);
                    }
                }
                return dt;
            }
            catch (Exception erro)
            {
                throw new Exception("Não foi possível Fazer a BUSCA. erro: " + erro.Message);
            }
        }
        /// <summary>
        /// Método responsável em buscar um tipo de Serviços.
        /// </summary>
        /// <param name="servico">Parâmetro que recebe o id do Serviço.</param>
        /// <returns>Retorna um objeto com preenchido com as informações de um serviço especifico.</returns>
        public Servico BuscarServico(Servico servico)
        {
            try
            {
                objServicoDAO = new ServicoDAO();
                objServico = new Servico();
                objServico = objServicoDAO.BuscarServico(servico);
                return objServico;
            }
            catch (Exception erro)
            {
                throw new Exception("Não foi possível Fazer a BUSCA. erro: " + erro.Message);
            }
        }
        #endregion
    }
}