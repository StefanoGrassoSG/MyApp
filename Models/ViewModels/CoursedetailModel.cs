using System.Data;
using WebAppCourse.Models.Enums;
using WebAppCourse.Models.ValueTypes;

namespace WebAppCourse.Models.ViewModels
{
    public class CourseDetailViewModel : CourseViewModel
    {   
        public string? Description {get;set;}
        public List<LessonViewModel>? Lessons {get;set;}

        public TimeSpan TotalCoursesDuration 
        {
            get => TimeSpan.FromSeconds(Lessons?.Sum(obj => obj.Duration.TotalSeconds) ?? 0);
        }

        public CourseDetailViewModel()
        {
            Lessons = new List<LessonViewModel>();
        }

        public static CourseDetailViewModel FromDataRow(DataRow dataRow)
        {
            var courseDetailViewModel = new CourseDetailViewModel
            {
                Id = (int) dataRow["Id"],
                Title = (string) dataRow["Title"],
                Description = (string) dataRow["Description"],
                ImagePath = (string) dataRow["ImagePath"],
                Author = (string) dataRow["Author"],
                Rating = (decimal) dataRow["Rating"],
                FullPrice = new Money(Enum.Parse<Currency>((string) dataRow["FullPrice_Currency"]),(decimal) dataRow["FullPrice_Amount"]),
                CurrentPrice = new Money(Enum.Parse<Currency>((string) dataRow["CurrentPrice_Currency"]),(decimal) dataRow["CurrentPrice_Amount"]),
                Lessons = new List<LessonViewModel>()
            };
            return courseDetailViewModel;
        }
    }
}