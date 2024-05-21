using System.Data;
using WebAppCourse.Models.ViewModels;
using WebAppCourse.Models.Services.Infrastructure;
using Microsoft.Extensions.Options;
using WebAppCourse.Models.Options;
using WebAppCourse.Models.InputModels;

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

        public async Task<ListViewModel<CourseViewModel>> GetCourses(CourseListInputModel courseListInputModel)
        {
            string direction = courseListInputModel.Ascending ? "ASC" : "DESC";
            FormattableString query = $"SELECT Id,Title,ImagePath,Author,Rating,FullPrice_Amount,FullPrice_Currency,CurrentPrice_Amount,CurrentPrice_Currency FROM Courses WHERE Title LIKE {'%' + courseListInputModel.Search + '%'} ORDER BY {courseListInputModel.Orderby} {direction} LIMIT {courseListInputModel.Limit} OFFSET {courseListInputModel.Offset}";
            DataSet dataSet = await db.Query(query);
            var dataTable = dataSet.Tables[0];
            var results = new ListViewModel<CourseViewModel>
            {
                Results = new List<CourseViewModel>(),
                TotalCount = 0
            };
            foreach (var courseRow in dataTable.Rows)
            {
                var course = CourseViewModel.FromDataRow((DataRow)courseRow);
                results.Results.Add(course);
                results.TotalCount++;
            }
            return results;
        }
    }
}