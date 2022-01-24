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
    public class ClienteBO
    {
        #region ASSINATURAS DE METODOS E DECLAÇÕES
        /// <summary>
        /// Cria um objeto da camada DAO para dar acesso aos metodos inerente da Classe.
        /// </summary>
        ClienteDAO objClienteDAO;
        /// <summary>
        /// Cria uma lista do Tipo da Classe. 
        ///</summary>
        IList<Cliente> listaCliente;
        /// <summary>
        /// Cria as variaveis.
        /// </summary>
        string dataCadastro, id;
        int numInt;
        DateTime data;

        #endregion
        //---------------------------------///-------------------------///---------------------------------///
        #region CRUD
       
        /// <summary>
        ///  Método responsável em preparar os dados para gravar ou alterar enviando para a camada DAO. 
        /// </summary>
        /// <param name="objCliente">Parâmetro que recebe as informações do Cliente.</param>
        public void Gravar(Cliente objCliente)
        {
            try
            {
                dataCadastro = objCliente._DataCadastro.ToString();
                id = objCliente._ClienteID.ToString();

                if (!DateTime.TryParse(dataCadastro, out data))
                    throw new Exception("O campo Data não é Válida ex: 31/12/2013!!!!");
                // todo REFERENCIA (http://social.msdn.microsoft.com/Forums/vstudio/pt-BR/9bd51ae5-8622-4010-af1b-a547000cd7c1/c-string-possui-nmero-ou-caracteres)
                if (String.IsNullOrEmpty(objCliente._Nome))
                    throw new Exception("O campo Nome do Cliente é obrigatório!!!");
                if ((objCliente._Nome.Where(c => char.IsNumber(c)).Count() > 0))
                    throw new Exception("O Campo Nome só pode conter Letras!!!");                
                if (objCliente._TipoPessoa != 0 && objCliente._TipoPessoa != 1)
                    throw new Exception("Selecione o Tipo de Pessoa Física ou Jurídica!!!");
                if (objCliente._TipoPessoa == 0)
                {
                    if (String.IsNullOrEmpty(objCliente._CpfCnpj))
                        throw new Exception("O campo CPF é obrigatório!!!");
                    if (objCliente._CpfCnpj.Where(c => char.IsLetter(c)).Count() > 0)
                        throw new Exception("Digite um Número de CPF Válido ex:123.456.789-00!!!");
                    if (objCliente._Sexo == null)
                        throw new Exception("Selecione o Sexo do Cliente!!!");
                }
                if (objCliente._TipoPessoa == 1)
                {
                    if (String.IsNullOrEmpty(objCliente._CpfCnpj))
                        throw new Exception("O campo CNPJ é obrigatório!!!");
                    if (objCliente._CpfCnpj.Where(c => char.IsLetter(c)).Count() > 0)
                        throw new Exception("Digite um numero Válido para CNPJ!!!");
                }
                if (objCliente._Rg.Where(c => char.IsLetter(c)).Count() > 0)
                    throw new Exception("Digite um numero de RG Válido ex:234.544");
                if (objCliente._Cep.Where(c => char.IsLetter(c)).Count() > 0)
                    throw new Exception("Digite um Número de CEP Válido ex:28.383-888");
                if (String.IsNullOrEmpty(objCliente._Endereco))
                    throw new Exception("Campo Endereço Obrigatório!!!");
                if (String.IsNullOrEmpty(objCliente._Bairro))
                    throw new Exception("Campo Bairro Obrigatório!!!");
                if (String.IsNullOrEmpty(objCliente._Cidade))
                    throw new Exception("Campo Cidade Obrigatório!!!");
                if (objCliente._Cidade.Where(c => char.IsNumber(c)).Count() > 0)
                    throw new Exception("O Campo Cidade não pode Conter Números!!!");
                if (String.IsNullOrEmpty(objCliente._Telefone1))
                    throw new Exception("Campo Número de Telefone Obrigatório!!!");
                if (objCliente._Uf == 0)
                    throw new Exception("Campo selecionar UF obrigatório!!!");
                if (objCliente._Telefone1.Where(c => char.IsLetter(c)).Count() > 0)
                    throw new Exception("Campo Número de Telefone não pode Conter Letras!!!");
                if (objCliente._Telefone2.Where(c => char.IsLetter(c)).Count() > 0)
                    throw new Exception("Campo Número de Telefone não pode Conter Letras!!");
                

                objClienteDAO = new ClienteDAO();
                if (int.TryParse(id, out numInt) && objCliente._ClienteID != 0)
                {
                    try
                    {
                        objClienteDAO.Alterar(objCliente);
                    }
                    catch (Exception erro)
                    {

                        throw new Exception("Não foi possível Alterar. erro:" + erro.Message);
                    }

                }
                else
                {
                    //ClienteExiste();// todo LUIZ (CODIFICAR SE EXISTE CLIENTE NO BANCO COM O MESMO CPF OU CNPJ)
                    try
                    {
                        Cliente objCli = new Cliente();
                        objCli = objClienteDAO.BuscarCliente(objCliente);
                        if(objCli == null)
                             objClienteDAO.Gravar(objCliente);
                        else
                            throw new Exception("Já existe um cliente cadastrado com este CPF!!!");

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
        /// <param name="objCliente">Parâmetro que recebe o id do Cliente.</param>
        public void Excluir(Cliente objCliente)
        {
            try
            {
                objClienteDAO = new ClienteDAO();
                objClienteDAO.Excluir(objCliente);
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
        ///  Método responsável em buscar uma lista de Cliente.
       /// </summary>
        /// <param name="cliente">Parâmetro que recebe o nome do Cliente que deseja buscar.</param>
        /// <returns>Retorna uma lista com os clientes encontrados do tipo Data Table.</returns>
        public DataTable BuscarListaCliente(Cliente cliente)
        {
            try
            {
                objClienteDAO = new ClienteDAO();
                DataRow dr;
                DataTable dt = new DataTable();
                listaCliente = new List<Cliente>();

                dt.Columns.Add("ClienteID");
                dt.Columns.Add("Nome");
                dt.Columns.Add("CPF/CNPJ");
                dt.Columns.Add("Sexo");              
                dt.Columns.Add("Telefone 1");
                dt.Columns.Add("Telefone 2");              

                listaCliente = objClienteDAO.BuscarListaCliente(cliente);

                // Percorre a lista com o resultado da consulta

                if (listaCliente != null)
                {
                    foreach (Cliente objCliente in listaCliente)
                    {
                        dr = dt.NewRow();

                        dr["ClienteID"] = objCliente._ClienteID;
                        dr["Nome"] = objCliente._Nome;
                        dr["CPF/CNPJ"] = objCliente._CpfCnpj;
                        dr["Sexo"] = objCliente._Sexo;
                        dr["Telefone 1"] = objCliente._Telefone1;
                        dr["Telefone 2"] = objCliente._Telefone2;


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
        ///   Método responsável em buscar um Cliente especifico.
        /// </summary>
        /// <param name="cliente">Parâmetro que recebe o id do produto que deseja buscar.</param>
        /// <returns>Retorna um objeto preenchido com as informações do cliente especifico.</returns>
        public Cliente BuscarCliente(Cliente cliente)
        {
            try
            {

                ClienteDAO objClienteDAO = new ClienteDAO();
                Cliente objCliente = new Cliente();
                objCliente = objClienteDAO.BuscarCliente(cliente);
                return objCliente;
            }
            catch (Exception erro)
            {
                throw new Exception("Não foi possível Fazer a BUSCA. erro: " + erro.Message);
            }
        }

        #endregion
        //---------------------------------///-------------------------///---------------------------------///
       
    }
}

