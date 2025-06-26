namespace DOS.Core.Exceptions
{
    namespace DOS.Core.Exceptions
    {
        public class AppException : Exception
        {
            public int StatusCode { get; }

            public AppException() { }

            public AppException(string message) : base(message) { }

            public AppException(string message, Exception innerException) : base(message, innerException) { }

            public AppException(string message, int statusCode) : base(message)
            {
                StatusCode = statusCode;
            }
        }
    }
}
