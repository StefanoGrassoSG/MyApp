using System.Data;

namespace WebAppCourse.Models.Services.Infrastructure
{
    public interface IDatabase
    {
        DataSet Query(FormattableString query);
    }
}