namespace WebAppCourse.Models.Exceptions
{
    public class OptimisticException : Exception
    {
        public OptimisticException(Exception innerException) : base($"Il corso è stato appena modificato da un'altro utente.")
        {}
    }
}