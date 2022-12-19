using System.Runtime.Serialization;

namespace Space.Controllers
{
    public class ExceptionController
    {
        [Serializable]
        public class ConcurrencyException : Exception
        {
            public ConcurrencyException()
            {
            }

            public ConcurrencyException(string? message) : base(message)
            {
            }

            public ConcurrencyException(string? message, Exception? innerException) : base(message, innerException)
            {
            }

            protected ConcurrencyException(SerializationInfo info, StreamingContext context) : base(info, context)
            {
            }
        }

        [Serializable]
        public class DatabaseAccessException : Exception
        {
            public DatabaseAccessException()
            {
            }

            public DatabaseAccessException(string? message) : base(message)
            {
            }

            public DatabaseAccessException(string? message, Exception? innerException) : base(message, innerException)
            {
            }

            protected DatabaseAccessException(SerializationInfo info, StreamingContext context) : base(info, context)
            {
            }
        }
    }
}
