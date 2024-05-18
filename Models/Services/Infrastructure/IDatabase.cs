using System.Data;

namespace WebAppCourse.Models.Services.Infrastructure
{
    public interface IDatabase
    {
        Task<DataSet> Query(FormattableString query);
    }
}