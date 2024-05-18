using System.Data;
using WebAppCourse.Models.Enums;
using WebAppCourse.Models.ValueTypes;

namespace WebAppCourse.Models.ViewModels
{
    public class CourseViewModel
    {
        public int Id {get;set;}
        public string Title {get;set;}
        public string ImagePath {get;set;}
        public string Author {get;set;}
        public double Rating {get;set;}
        public Money FullPrice {get;set;}
        public Money CurrentPrice {get;set;}

        public static CourseViewModel FromDataRow(DataRow courseRow)
        {
            var courseViewModel = new CourseViewModel
            {
                Id = (int) courseRow["Id"],
                Title = (string) courseRow["Title"],
                ImagePath = (string) courseRow["ImagePath"],
                Author = (string) courseRow["Author"],
                Rating = (double)(decimal) courseRow["Rating"],
                FullPrice = new Money(Enum.Parse<Currency>((string) courseRow["FullPrice_Currency"]),(decimal) courseRow["FullPrice_Amount"]),
                CurrentPrice = new Money(Enum.Parse<Currency>((string) courseRow["CurrentPrice_Currency"]),(decimal) courseRow["CurrentPrice_Amount"])
            };
            return courseViewModel;
        }
    }
}