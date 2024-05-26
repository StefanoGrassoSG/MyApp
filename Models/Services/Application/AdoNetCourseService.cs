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
                if(courseDetailViewModel.Lessons != null) {
                    courseDetailViewModel.Lessons.Add(lessonViewModel);
                }
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

        public Task<List<CourseViewModel>> GetMostRecentCourses()
        {
            throw new NotImplementedException();
        }
        public Task<List<CourseViewModel>> GetBestRatingCourses()
        {
            throw new NotImplementedException();
        }

        public async Task<CourseDetailViewModel> CreateCourseAsync(CourseCreateInputModel model)
        {
            string title = model.Title;
            string author = "Mario Rossi";

            var dataSet = await db.Query($@"INSERT INTO Courses (Title, Author, ImagePath, CurrentPrice_Currency, CurrentPrice_Amount, FullPrice_Currency, FullPrice_Amount) VALUES ({title}, {author}, '/Courses/default.png', 'EUR', 0, 'EUR', 0);
                          SELECT SCOPE_IDENTITY();");

            int courseId = Convert.ToInt32(dataSet.Tables[0].Rows[0][0]);
            CourseDetailViewModel course = await GetCourse(courseId);
            return course;  
        }

        public Task<bool> IsTitleAvailableAsync(string title, int id)
        {
            throw new NotImplementedException();
        }

        public Task<CourseEditInputModel> GetCourseEditAsync(int id)
        {
            throw new NotImplementedException();
        }
        Task<CourseDetailViewModel> ICourseService.EditCourseAsync(CourseEditInputModel model)
        {
            throw new NotImplementedException();
        }
    }
}