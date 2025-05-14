namespace LMS.Common.Exceptions
{
    public class DatabaseException : Exception
    {
        public int SqlErrorCode { get; }

        public DatabaseException(string message, int errorCode) : base(message)
        {
            SqlErrorCode = errorCode;
        }
    }
}
