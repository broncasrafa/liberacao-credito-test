using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Application.Extensions;
using Dapper;


namespace Persistence.Repositories
{
    public abstract class ApplicationDbContext
    {
        private readonly IConfiguration _configuration;

        public ApplicationDbContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        private SqlConnection _Connection { get; set; }
        private SqlConnection SqlConnection()
        {
            _Connection = new SqlConnection(_configuration.GetSection("SqlConnection").GetSection("ApplicationDbConnection").Value);
            return _Connection;
        }


        /// <summary>
        /// Cria a conexão com a base de dados
        /// </summary>
        /// <returns>retorna um objeto SqlConnection</returns>
        public IDbConnection CreateConnection()
        {
            _Connection = SqlConnection();
            _Connection.Open();            
            return _Connection;
        }

        /// <summary>
        /// Cria os parametros da procedure como SQL TYPE do tipo TABLE
        /// </summary>
        /// <typeparam name="T">tipo do objeto</typeparam>
        /// <param name="listObj">lista de objetos</param>
        /// <param name="tableTypeName">nome do SQL TYPE do tipo TABLE</param>
        /// <returns>retorna os parametros para o Dapper</returns>
        public SqlMapper.ICustomQueryParameter GetTableAsParameters<T>(IList<T> listObj, string tableTypeName)
        {
            SqlMapper.ICustomQueryParameter tableParameters = listObj.ConvertToDataTable<T>().AsTableValuedParameter(tableTypeName);
            return tableParameters;
        }
    }
}
/*
create TYPE TP_NOME as TABLE (
    campos para os parametros
)
*/
