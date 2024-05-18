using System;
using WebAppCourse.Models.Enums;
using WebAppCourse.Models.ValueTypes;
using WebAppCourse.Models.ViewModels;

namespace WebAppCourse.Models.Services.Application
{
    public class CourseService : ICourseService
    {
        public List<CourseViewModel> GetCourses() 
        {
            var courseList = new List<CourseViewModel>();
            var rand = new Random();
            for (int i = 1; i <=21; i++)
            {
                var price = Math.Round(Convert.ToDecimal(rand.NextDouble() * 40 + 10),2);
                var fullP = Math.Round( Convert.ToDecimal(rand.NextDouble() * 40 + 10),2);
                var course = new CourseViewModel
                {
                    Id = i,
                    Title = $"Corso {i}",
                    CurrentPrice = new Money(Currency.EUR, price <= fullP ? fullP : price),
                    FullPrice = new Money(Currency.EUR , price >= fullP ? fullP : price),
                    Author = "Nome e Cognome",
                    Rating = Math.Round(rand.NextDouble() * 5.0, 1),
                    ImagePath = "/logo7.png"
                };
                courseList.Add(course);
            }
            return courseList;
        }

        public CourseDetailViewModel GetCourse(int id)
        {
            var rand = new Random();
            var price = Math.Round(Convert.ToDecimal(rand.NextDouble() * 40 + 10),2);
            var fullP = Math.Round( Convert.ToDecimal(rand.NextDouble() * 40 + 10),2);
            var course = new CourseDetailViewModel 
            {
                Id = id,
                Title = $"Corso {id}",
                CurrentPrice = new Money(Currency.EUR, price <= fullP ? fullP : price),
                FullPrice = new Money(Currency.EUR , price >= fullP ? fullP : price),
                Author = "Nome e Cognome",
                Rating = Math.Round(rand.NextDouble() * 5.0, 1),
                ImagePath = "/logo7.png",
                Description = $"Descrizione {id}",
                Lessons = new List<LessonViewModel>()
            };

            for(var i = 1;i<=5;i++) {
                var lessons = new LessonViewModel
                {
                    Title = $"Lezione {i}",
                    Duration = TimeSpan.FromSeconds(rand.Next(40, 90))
                };
                course.Lessons.Add(lessons);
            }
            return course;
        } 
    }
}