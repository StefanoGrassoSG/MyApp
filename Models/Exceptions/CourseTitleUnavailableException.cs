namespace WebAppCourse.Models.Exceptions
{
    public class CourseTitleUnavailableException : Exception
    {
        public CourseTitleUnavailableException(string title, Exception innerException) : base($"Il titolo '{title}' esiste già")
        {}
    }
}