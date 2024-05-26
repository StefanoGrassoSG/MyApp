namespace WebAppCourse.Models.ViewModels
{
    public class HomeViewModel
    {
        public List<CourseViewModel> BestRatingCourses {get;set;} = new List<CourseViewModel>();
        public List<CourseViewModel> MostRecentCourses {get;set;} = new List<CourseViewModel>();
    }
}