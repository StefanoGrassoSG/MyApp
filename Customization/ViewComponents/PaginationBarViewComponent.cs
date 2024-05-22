using Microsoft.AspNetCore.Mvc;
using WebAppCourse.Models.ViewModels;

namespace WebAppCourse.Customization.ViewComponents
{
    public class PaginationBarViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(IPagination model)
        {
            
            return View(model);
        }
    }
}