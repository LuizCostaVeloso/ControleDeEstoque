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
    public class MotoBO
    {
        /// <summary>
        /// Classe da camada BO responsável pela Regra de negócio.
        /// </summary>
        #region ASSINATURAS DE METODOS
        /// <summary>
        /// Cria um objeto da camada DAO para dar acesso aos metodos inerente da Classe.
        /// </summary>
        MotoDAO objMotoDAO;
        /// <summary>
        /// Cria uma lista do Tipo da Classe. 
        ///</summary>
        IList<Moto> listaMoto;
        /// <summary>
        /// Cria o objeto para reservar um espaço no disco após instanciado.
        /// </summary>
        static Moto objMoto;
        /// <summary>
        /// Cria as variaveis.
        /// </summary>
        string id;
        int numInt;
        #endregion

        #region CRUD
        /// <summary>
        ///   Método responsável em preparar os dados para gravar ou alterar enviando para a camada DAO.
        /// </summary>
        /// <param name="objMoto">Parâmetro que recebe as informações do Produto.</param>
        public void Gravar(Moto objMoto)
        {
            try
            {
                id = objMoto._MotoID.ToString();

                if (String.IsNullOrEmpty(objMoto._Placa))
                    throw new Exception("O campo Placa é Obrigatório!!!");
                if (String.IsNullOrEmpty(objMoto._MarcaModelo))
                    throw new Exception("O campo Modelo/Marca é obrigatório!!!");
                if (String.IsNullOrEmpty(objMoto._AnoFabricacao))
                    throw new Exception("O campo Ano de Fabricação é obrigatório!!!");
                if ((objMoto._AnoFabricacao.Where(c => char.IsLetter(c)).Count() > 0))
                    throw new Exception("O campo Ano de Fabricação só aceita números!!!");  

                objMotoDAO = new MotoDAO();
                if (int.TryParse(id, out numInt) && objMoto._MotoID != 0)
                {
                    try
                    {
                        objMotoDAO.Alterar(objMoto);
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
                        Moto objM = new Moto();
                        objM._Placa = objMoto._Placa;
                        objM = objMotoDAO.BuscarMoto(objM);
                        if(objM !=null)
                            throw new Exception("Já existe uma Moto com esta Placa cadastrada!!!");
                        objMotoDAO.Gravar(objMoto);
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
        ///  Método que recebe um id e chama o método excluir da camada DAO.
        /// </summary>
        /// <param name="objMoto">Parâmetro que recebe o id da Moto.</param>
        public void Excluir(Moto objMoto)
        {
            try
            {
                objMotoDAO = new MotoDAO();
                objMotoDAO.Excluir(objMoto);
            }
            catch (Exception erro)
            {
                throw new Exception("Não foi Possível Excluir a Moto. erro: " + erro.Message);
            }

        }

        #endregion

        #region BUSCAS
        /// <summary>
        ///   Método responsável em buscar uma lista de Motos.
        /// </summary>
        /// <param name="moto">Parâmetro que recebe a placa da moto que deseja buscar.</param>
        /// <returns>Retorna uma lista com as motos encontradas.</returns>
        public DataTable BuscarListaMoto(Moto moto)
        {
            try
            {
                objMotoDAO = new MotoDAO();

                DataRow dr;
                DataTable dt = new DataTable();
                listaMoto = new List<Moto>();

                dt.Columns.Add("MotoID");
                dt.Columns.Add("Placa");               
                dt.Columns.Add("Modelo/Marca");
                dt.Columns.Add("Ano");
                dt.Columns.Add("Cor");               

                listaMoto = objMotoDAO.BuscarListaMoto(moto);

                if (listaMoto != null)
                {
                    foreach (Moto objMoto in listaMoto)
                    {
                        dr = dt.NewRow();

                        dr["MotoID"] = objMoto._MotoID;
                        dr["Placa"] = objMoto._Placa;
                      
                        dr["Modelo/Marca"] = objMoto._MarcaModelo;
                        dr["Ano"] = objMoto._AnoFabricacao;
                        dr["Cor"] = objMoto._CorPredominante;
                      

                        dt.Rows.Add(dr);
                    }
                }
                return dt;
            }
            catch (Exception erro)
            {
                throw new Exception("Não foi Possível Fazer a Busca. erro: " + erro.Message);
            }

        }
        /// <summary>
        ///    Método responsável em buscar uma moto especifico.
        /// </summary>
        /// <param name="moto">Parâmetro que recebe o id ou a placa de uma moto que deseja buscar.</param>
        /// <returns>Retorna um objeto preenchido com as informações de uma moto especifico.</returns>
        public Moto BuscarMoto(Moto moto)
        {
            try
            {
                objMotoDAO = new MotoDAO();
                objMoto = new Moto();
                objMoto = objMotoDAO.BuscarMoto(moto);
                return objMoto;
            }
            catch (Exception erro)
            {
                throw new Exception("Não foi Possível Fazer a Busca. erro: " + erro.Message);
            }
        }

        #endregion
       
    }
}
