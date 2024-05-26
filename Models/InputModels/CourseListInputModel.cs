using Microsoft.AspNetCore.Mvc;
using WebAppCourse.Customization.ModelBinders;
using WebAppCourse.Models.Options;

namespace WebAppCourse.Models.InputModels
{
    [ModelBinder(BinderType = typeof(CourseListInputModelBinder))]
    public class CourseListInputModel
    {
        public CourseListInputModel()
        {
        }

        public CourseListInputModel(string search, int page, string orderby, bool ascending,int limit, CoursesOptions coursesOptions)
        {
            var orderOptions = coursesOptions.Order;
            if(!orderOptions.Allow.Contains(orderby))
            {
                orderby = orderOptions.By;
                ascending = orderOptions.Ascending;
            }

            Search = search ?? "";
            Page = Math.Max(1, page);
            Orderby = orderby;
            Ascending = ascending;

            this.Limit = limit;
            this. Offset = (Page - 1) * Limit;
        }
        public string? Search {get;}
        public int Page {get;}
        public string? Orderby {get;}
        public bool Ascending {get;}
        public int Limit {get;}
        public int Offset {get;}
    }
}