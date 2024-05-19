using System.Data;
using System.Data.SqlClient;
using Microsoft.CodeAnalysis;
using Microsoft.Extensions.Options;
using WebAppCourse.Models.Options;

namespace WebAppCourse.Models.Services.Infrastructure
{
    public class SqlDatabaseAccessor : IDatabase
    {
        private readonly IOptionsMonitor<ConnectionStringsOptions> options;

        public SqlDatabaseAccessor(IOptionsMonitor<ConnectionStringsOptions> options)
        {
            this.options = options;
        }
        public async Task<DataSet> Query(FormattableString fQuery)
        {   
            var queryArguments = fQuery.GetArguments();
            var sqlParameters = new List<SqlParameter>();
            for(var i = 0; i<queryArguments.Length;i++)
            {
                var parameter = new SqlParameter(i.ToString(), queryArguments[i]);
                sqlParameters.Add(parameter);
                queryArguments[i] = "@" + i;
            }
            string query = fQuery.ToString();
            string connectionString = options.CurrentValue.Default;
            using (var conn = new SqlConnection(connectionString))
            {
                await conn.OpenAsync();
                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddRange(sqlParameters.ToArray());
                    using (var reader = await cmd.ExecuteReaderAsync())
                   {
                        var dataSet = new DataSet();

                        do
                        {
                            var dataTable = new DataTable();
                            dataSet.Tables.Add(dataTable);
                            dataTable.Load(reader);
                        } while(!reader.IsClosed);

                        return dataSet;
                   }
                }
            }
        }
    }
}