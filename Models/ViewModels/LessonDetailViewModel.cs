using System.Data;

namespace WebAppCourse.Models.ViewModels
{
    public class LessonDetailViewModel
    {   
       public int Id {get;set; }
       public string Description {get;set;} = string.Empty;
       public string Title {get;set;} = string.Empty;
       public TimeSpan Duration {get;set;}

    }
}