using System.Text.Json.Serialization;

namespace WebAppCourse.Models.Options
{
    public partial class CoursesOptions
    {
        public int PerPage {get;set;}
        public int InHome {get;set;}
        public OrderOptions Order {get;set;}
    }

    public partial class OrderOptions
    {
        public string By {get;set;}
        public bool Ascending {get;set;}
        public string[] Allow {get;set;}
    }
}