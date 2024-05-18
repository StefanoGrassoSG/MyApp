namespace WebAppCourse.Models.ViewModels
{
    public class CourseDetailViewModel : CourseViewModel
    {   
        public string Description {get;set;}
        public List<LessonViewModel> Lessons {get;set;}

        public TimeSpan TotalCoursesDuration 
        {
            get => TimeSpan.FromSeconds(Lessons?.Sum(obj => obj.Duration.TotalSeconds) ?? 0);
        }
    }
}