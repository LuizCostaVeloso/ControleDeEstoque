using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace Negocio
{
    public static class ConexaoBanco
    {
       
        public static SqlConnection Conectar()
        {
            SqlConnection.ClearAllPools();
            //string stringConexao = "Data Source = LUIZ; Initial Catalog = SICSEP; User = sa; password = 123";
           
            string stringConexao = ConfigurationManager.ConnectionStrings["conexaoWebConfig"].ConnectionString;
            SqlConnection conexao = new SqlConnection(stringConexao);
            conexao.Open();
            return conexao;
        }

        public static void Crud(SqlCommand comando)
        {
           
            SqlConnection con = Conectar();
            comando.Connection = con;
            comando.ExecuteNonQuery();
            con.Close();
        }

        public static SqlDataReader Selecionar(SqlCommand comando)
        {
            SqlConnection con = Conectar();
            comando.Connection = con;
            SqlDataReader leitorDR = comando.ExecuteReader(CommandBehavior.CloseConnection);
            return leitorDR;
        }
    }
}
