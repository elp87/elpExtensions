using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace elp.Extensions
{
    public class ElpNullSourceException : ApplicationException
    {
        public DateTime ErrorTimeStamp { get; set; }
        public string CauseOfError { get; set; }
        public ElpNullSourceException() { }
        public ElpNullSourceException(string message, string cause, DateTime time)
            : base(message)
        {
            CauseOfError = cause;
            ErrorTimeStamp = time;
        }
    }

    public class ElpInvalidHeaderException : ApplicationException
    {
        public DateTime ErrorTimeStamp { get; set; }
        public string CauseOfError { get; set; }
        public ElpInvalidHeaderException() { }
        public ElpInvalidHeaderException(string message, string cause, DateTime time)
            : base(message)
        {
            CauseOfError = cause;
            ErrorTimeStamp = time;
        }
    }

    public class ElpLineAccessException : ApplicationException
    {
        public DateTime ErrorTimeStamp { get; set; }
        public string CauseOfError { get; set; }
        public ElpLineAccessException() { }
        public ElpLineAccessException(string message, string cause, DateTime time)
            : base(message)
        {
            CauseOfError = cause;
            ErrorTimeStamp = time;
        }
    }
}
