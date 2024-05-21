using System.Data;
using WebAppCourse.Models.ViewModels;
using WebAppCourse.Models.Services.Infrastructure;
using Microsoft.Extensions.Options;
using WebAppCourse.Models.Options;

namespace WebAppCourse.Models.Services.Application
{
    public class AdoNetCourseService : ICourseService
    {
        private readonly IDatabase db;
        private readonly IOptionsMonitor<CoursesOptions> courseOptions;
        public AdoNetCourseService(IDatabase db, IOptionsMonitor<CoursesOptions> coursesOptions)
        {
            this.db = db;
            this.courseOptions = coursesOptions;
        }   
        public async Task<CourseDetailViewModel> GetCourse(int id)
        {
            FormattableString query = $@"SELECT Id,Title,Description,ImagePath,Author,Rating,FullPrice_Amount,FullPrice_Currency,CurrentPrice_Amount,CurrentPrice_Currency FROM Courses WHERE Id={id}
            ; SELECT Id,Title,Description,Duration FROM Lessons WHERE CourseId={id}";
            DataSet dataSet = await db.Query(query);

            var courseTable = dataSet.Tables[0];
            if(courseTable.Rows.Count != 1) {
                throw new InvalidOperationException($"Did not return 1 row for course {id}");
            }
            var courseRow = courseTable.Rows[0];
            var courseDetailViewModel = CourseDetailViewModel.FromDataRow(courseRow);

            var lessonDataTable = dataSet.Tables[1];

            foreach (DataRow lessonRow in lessonDataTable.Rows)
            {
                var lessonViewModel = LessonViewModel.FromDataRow(lessonRow);
                courseDetailViewModel.Lessons.Add(lessonViewModel);
            }
            return courseDetailViewModel;
        }

        public async Task<List<CourseViewModel>> GetCourses(string search, int page, string orderby, bool ascending)
        {
            page = Math.Max(1, page);
            int limit = courseOptions.CurrentValue.PerPage;
            int offset = (page - 1) * limit;
            var orderOptions = courseOptions.CurrentValue.Order;
            if(!orderOptions.Allow.Contains(orderby)) 
            {
                orderby = orderOptions.By;
                ascending = orderOptions.Ascending;
            }
            if(orderby == "CurrentPrice") 
            {
                orderby = "CurrentPrice_Amount";
            }
            string direction = ascending ? "ASC" : "DESC";
            FormattableString query = $"SELECT Id,Title,ImagePath,Author,Rating,FullPrice_Amount,FullPrice_Currency,CurrentPrice_Amount,CurrentPrice_Currency FROM Courses WHERE Title LIKE {'%' + search + '%'} ORDER BY {orderby} {direction} LIMIT {limit} OFFSET {offset}";
            DataSet dataSet = await db.Query(query);
            var dataTable = dataSet.Tables[0];
            var courseList = new List<CourseViewModel>();
            foreach (var courseRow in dataTable.Rows)
            {
                var course = CourseViewModel.FromDataRow((DataRow)courseRow);
                courseList.Add(course);
            }
            return courseList;
        }
    }
}