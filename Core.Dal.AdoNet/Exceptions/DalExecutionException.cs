namespace Core.Dal.AdoNet.Exceptions
{
    using System;

    public class DalExecutionException : Exception
    {
        public DalExecutionException() { }

        public DalExecutionException(string message) : base(message) { }

        public DalExecutionException(string message, Exception exception) : base(message, exception) { }
    }
}
