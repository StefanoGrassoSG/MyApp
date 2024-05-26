using System.Data;

namespace WebAppCourse.Models.ViewModels
{
    public class LessonViewModel
    {   
       public int Id {get;set; }
       public string Description {get;set;} = string.Empty;
       public string Title {get;set;} = string.Empty;
       public TimeSpan Duration {get;set;}

        public static LessonViewModel FromDataRow(DataRow dataRow)
        {
            var lessonViewModel = new LessonViewModel
            {
                Title = (string) dataRow["Title"],
                Duration = TimeSpan.Parse((string)dataRow["Duration"])
            };
            return lessonViewModel;
        }
    }
}