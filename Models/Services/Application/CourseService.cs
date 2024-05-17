using System;
using WebAppCourse.Models.Enums;
using WebAppCourse.Models.ValueTypes;
using WebAppCourse.Models.ViewModels;

namespace WebAppCourse.Models.Services.Application
{
    public class CourseService 
    {
        public List<CourseViewModel> GetServices() 
        {
            var courseList = new List<CourseViewModel>();
            var rand = new Random();
            for (int i = 0; i <=20; i++)
            {
                var price = Math.Round(Convert.ToDecimal(rand.NextDouble() * 40 + 10),2);
                var fullP = Math.Round( Convert.ToDecimal(rand.NextDouble() * 40 + 10),2);
                var course = new CourseViewModel
                {
                    Id = 1,
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
    }
}