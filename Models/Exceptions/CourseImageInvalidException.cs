namespace WebAppCourse.Models.Exceptions
{
    public class CourseImageInvalidException : Exception
    {
        public CourseImageInvalidException(Exception ex) : base("L'immagine selezionata non è valida")
        {}
    }
}