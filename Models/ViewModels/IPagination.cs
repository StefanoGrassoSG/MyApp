namespace WebAppCourse.Models.ViewModels
{
    public interface IPagination
    {
        int currentPage {get;}
        int TotalResults {get;}
        int ResultPerPage {get;}

        string Search {get;}
        string OrderBy {get;}
        bool Ascending {get;}
    }
}