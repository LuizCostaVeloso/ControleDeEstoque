using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Negocio.Model;

namespace Negocio.DAO
{
    /// <summary>
    ///Classe DAO, responsável por fazer as persistências na tabela de serviço no banco. 
    /// </summary>
    public class MotoDAO
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
        /// Cria uma lista do tipo Moto.
        /// </summary>
        IList<Moto> listaMoto;

        #endregion

        #region CRUD
        /// <summary>
        /// Método que converte o objeto em linguagem sql e grava no banco as informações.
        /// </summary>
        /// <param name="objMoto">Informações são passada no objeto como parâmetro para pessistir no banco.</param>
        public void Gravar(Moto objMoto)
        {
            try
            {
                sql.Remove(0, sql.Length);
                comando.Parameters.Clear();

                sql.Append("Insert Into Moto(anoFabricacao,chassi,corPredominante,marcaModelo,observacao,placa) values");
                sql.Append("(@anoFabricacao,@chassi,@corPredominante,@marcaModelo,@observacao,@placa)");

                comando.CommandText = sql.ToString();

                comando.Parameters.AddWithValue("@anoFabricacao", objMoto._AnoFabricacao);
                comando.Parameters.AddWithValue("@chassi", objMoto._Chassi);
                comando.Parameters.AddWithValue("@corPredominante", objMoto._CorPredominante);
                comando.Parameters.AddWithValue("@marcaModelo", objMoto._MarcaModelo);
                comando.Parameters.AddWithValue("@observacao", objMoto._Observacao);
                comando.Parameters.AddWithValue("@placa", objMoto._Placa);
                ConexaoBanco.Crud(comando);
            }
            catch (Exception erro)
            {
                throw new Exception(erro.Message);
            }

        }
         /// <summary>
        ///  Método que converte o objeto em liguagem sql persistindo no banco as informações para alteração. 
         /// </summary>
        /// <param name="objMoto">Informações são passada no objeto como parâmetro especificando quais informações deseja alterar.</param>
        public void Alterar(Moto objMoto)
        {
            try
            {
                sql.Remove(0, sql.Length);
                comando.Parameters.Clear();

                sql.Append("update Moto set anoFabricacao=@anoFabricacao, chassi=@chassi, corPredominante=@corPredominante,");
                sql.Append("marcaModelo=@marcaModelo, observacao=@observacao, placa=@placa where motoID=@motoID");

                comando.CommandText = sql.ToString();

                comando.Parameters.AddWithValue("@anoFabricacao", objMoto._AnoFabricacao);
                comando.Parameters.AddWithValue("@chassi", objMoto._Chassi);
                comando.Parameters.AddWithValue("@corPredominante", objMoto._CorPredominante);
                comando.Parameters.AddWithValue("@marcaModelo", objMoto._MarcaModelo);
                comando.Parameters.AddWithValue("@observacao", objMoto._Observacao);
                comando.Parameters.AddWithValue("@placa", objMoto._Placa);
                comando.Parameters.AddWithValue("@motoID", objMoto._MotoID);
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
        /// <param name="objMoto">objeto preenchido com o id e passado como parâmetro para exclusão no banco das informações especificas.</param>
        public void Excluir(Moto objMoto)
        {
            try
            {
                sql.Remove(0, sql.Length);
                comando.Parameters.Clear();

                sql.Append("Delete from Moto where motoID = @motoID");

                comando.CommandText = sql.ToString();

                comando.Parameters.AddWithValue("@motoID", objMoto._MotoID);
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
        ///   Método responsável em fazer a busca das informações desejada pela placa da moto.
         /// </summary>
         /// <param name="moto">objeto preenchido com as informações da placa e passado como parâmetro para busca no banco.</param>
        /// <returns>Este método retorna uma lista de Moto.</returns>
        public IList<Moto> BuscarListaMoto(Moto moto)
        {
            try
            {
                sql.Remove(0, sql.Length);
                comando.Parameters.Clear();

                sql.Append("Select * from Moto where placa Like @placa");
                comando.Parameters.AddWithValue("@placa", "%" + moto._Placa + "%");
                comando.CommandText = sql.ToString();
                SqlDataReader dr = ConexaoBanco.Selecionar(comando);
                listaMoto = new List<Moto>();

                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        Moto objMoto = new Moto();
                        objMoto._AnoFabricacao = (string)dr["anoFabricacao"];
                        objMoto._Chassi = (string)dr["chassi"];
                        objMoto._CorPredominante = (string)dr["corPredominante"];
                        objMoto._MarcaModelo = (string)dr["marcaModelo"];
                        objMoto._Observacao = (string)dr["observacao"];
                        objMoto._Placa = (string)dr["placa"];
                        objMoto._MotoID = (int)dr["motoID"];
                        listaMoto.Add(objMoto);
                    }
                }
                else
                {
                    listaMoto = null;
                }
                return listaMoto;
            }
            catch (Exception erro)
            {
                throw new Exception(erro.Message);
            }
        }
        /// <summary>
        ///  Método responsável em fazer a busca das informações de uma moto especifica.
        /// </summary>
        /// <param name="moto">Id ou placa da moto é passado como parâmetro dentro do objeto para fazer a busca da moto especifica.</param>
        /// <returns>Este método retorna um objeto do tipo Moto.</returns>
        public Moto BuscarMoto(Moto moto)
        {
            
            try
            {
                sql.Remove(0, sql.Length);
                comando.Parameters.Clear();                
                if (moto._Placa != null)
                {
                    sql.Append("Select * from Moto where placa = @placa");
                    comando.Parameters.AddWithValue("@placa", moto._Placa);
                }
                else
                {
                    sql.Append("Select * from Moto where motoID = @motoID");
                    comando.Parameters.AddWithValue("@motoID", moto._MotoID);
                }

                comando.CommandText = sql.ToString();
                SqlDataReader dr = ConexaoBanco.Selecionar(comando);
                Moto objMoto = new Moto();
                if (dr.HasRows)
                {
                    dr.Read();

                    objMoto._AnoFabricacao = (string)dr["anoFabricacao"];
                    objMoto._Chassi = (string)dr["chassi"];
                    objMoto._CorPredominante = (string)dr["corPredominante"];
                    objMoto._MarcaModelo = (string)dr["marcaModelo"];
                    objMoto._Observacao = (string)dr["observacao"];
                    objMoto._Placa = (string)dr["placa"];
                    objMoto._MotoID = (int)dr["motoID"];
                }
                else
                {
                    objMoto = null;
                }
                return objMoto;
            }
            catch (Exception erro)
            {
                throw new Exception(erro.Message);
            }
        }        

        #endregion
        
    }
}
