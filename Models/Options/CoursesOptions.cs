using System.Text.Json.Serialization;

namespace WebAppCourse.Models.Options
{
    public partial class CoursesOptions
    {
        public int PerPage {get;set;}
        public int InHome {get;set;}
        public OrderOptions Order {get;set;} = new OrderOptions();
    }

    public partial class OrderOptions
    {
        public string By {get;set;} = string.Empty;
        public bool Ascending {get;set;}
        public string[] Allow {get;set;} = [];
    }
}