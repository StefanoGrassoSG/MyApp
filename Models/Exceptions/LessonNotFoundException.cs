namespace WebAppCourse.Models.Exceptions
{
    public class LessonNotFoundException : Exception
    {
        public LessonNotFoundException(int lessonId) : base($"Lezione {lessonId} non trovata")
        {}
    }
}