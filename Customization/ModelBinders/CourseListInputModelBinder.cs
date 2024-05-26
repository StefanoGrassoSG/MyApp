using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Options;
using WebAppCourse.Models.InputModels;
using WebAppCourse.Models.Options;

namespace WebAppCourse.Customization.ModelBinders
{
    public class CourseListInputModelBinder : IModelBinder
    {
        private readonly IOptionsMonitor<CoursesOptions> courseOptions;
        public CourseListInputModelBinder(IOptionsMonitor<CoursesOptions> courseOptions)
        {
            this.courseOptions = courseOptions;
        }
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            string search = bindingContext.ValueProvider.GetValue("Search").FirstValue ?? string.Empty;
            int page = Convert.ToInt32(bindingContext.ValueProvider.GetValue("Page").FirstValue);
            string orderby = bindingContext.ValueProvider.GetValue("Orderby").FirstValue ?? string.Empty;;
            bool ascending = Convert.ToBoolean(bindingContext.ValueProvider.GetValue("Ascending").FirstValue);

            var inputModel = new CourseListInputModel(search, page, orderby, ascending,courseOptions.CurrentValue.PerPage, courseOptions.CurrentValue);

            bindingContext.Result = ModelBindingResult.Success(inputModel);

            return Task.CompletedTask;
        }
    }
}