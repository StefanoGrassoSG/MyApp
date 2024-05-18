using System.Data;
using System.Data.SqlClient;

namespace WebAppCourse.Models.Services.Infrastructure
{
    public class SqlDatabaseAccessor : IDatabase
    {
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

            using (var conn = new SqlConnection("Server=localhost\\SQLEXPRESS;Database=WEBAPPCOURSE;Trusted_Connection=True"))
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