using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MySql.Data.MySqlClient;
using SchoolFeedback.Models.Options;
using SchoolFeedback.Models.ValueTypes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolFeedback.Services.Infrastructure
{
    public class MySqlServices : IMySqlServices
    {
        private readonly IOptionsMonitor<MySqlOptions> _mySqlOptions;
        private readonly ILogger<MySqlServices> _logger;
        public MySqlServices(IOptionsMonitor<MySqlOptions> MySqlOptions, ILogger<MySqlServices> logger)
        {
            _mySqlOptions = MySqlOptions;
            _logger = logger;
        }

        /// <summary>
        /// Crea la connessione ad database ed esegue una query
        /// </summary>
        /// <param name="query">query da inviare al db</param>
        /// <returns>DataSet di prodotti</returns>
        public DataSet Query(FormattableString formattableQuery)
        {
            var queryArguments = formattableQuery.GetArguments(); //ottengo gli argomenti della formattableQuery
            var mysqlParameters = new List<MySqlParameter>(); //creo una lista di mysqlParameter
            for (var x = 0; x < queryArguments.Length; x++) //ciclo tutti gli argomenti
            {
                if (queryArguments[x] is Sql)
                    continue;
                var parameter = new MySqlParameter(x.ToString(), queryArguments[x]); //creo un istanza di mysqlparameter
                mysqlParameters.Add(parameter); //aggiungo il parametro alla lista
                queryArguments[x] = "@" + x; //riscrivo l'argomento
            }
            string query = formattableQuery.ToString(); //riformatto la query

            var connectionString = string.Format(_mySqlOptions.CurrentValue.ConnectionString, _mySqlOptions.CurrentValue.Server, _mySqlOptions.CurrentValue.Database, _mySqlOptions.CurrentValue.Username, _mySqlOptions.CurrentValue.Password);

            using (var conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    _logger.LogInformation("Database connection open.");
                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddRange(mysqlParameters.ToArray()); //aggiungo i paramtri
                        using (var reader = cmd.ExecuteReader())
                        {
                            var dataSet = new DataSet();
                            do
                            {
                                var dataTable = new DataTable();
                                dataSet.Tables.Add(dataTable);
                                dataTable.Load(reader);
                            } while (!reader.IsClosed);
                            _logger.LogInformation("Database connection close.");
                            return dataSet;
                        }
                    }
                }
                catch (Exception e)
                {
                    _logger.LogWarning(e.Message);
                    throw new Exception(e.Message);
                }
            }


        }
    }
}
