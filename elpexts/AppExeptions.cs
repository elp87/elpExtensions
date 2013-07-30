using System;

namespace elp.Extensions
{
    public class elpDateTimeParseExeption : ApplicationException
    {
        public DateTime ErrorTimeStamp { get; set; }
        public string CauseOfError { get; set; }
        public elpDateTimeParseExeption() { }
        public elpDateTimeParseExeption(string message, string cause, DateTime time)
            : base(message)
        {
            CauseOfError = cause;
            ErrorTimeStamp = time;
        }
    }

}
