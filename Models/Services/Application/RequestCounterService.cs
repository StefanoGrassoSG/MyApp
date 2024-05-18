
namespace WebAppCourse.Models.Services.Application
{
    public class RequestCounterService
    {
        private int _requestCount = 0;

        public int GetRequestCount()
        {
            return this._requestCount;
        }
        public void IncrementRequestCount()
        {
            Interlocked.Increment(ref _requestCount);
        }   
    }
}