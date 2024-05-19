namespace WebAppCourse.Models.Exceptions
{
    public class CourseNotFoundException : Exception
    {
        public CourseNotFoundException(int courseId) : base($"Corso {courseId} non trovato")
        {}
    }
}