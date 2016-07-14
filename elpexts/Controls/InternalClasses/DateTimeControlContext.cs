using System;

namespace elp87.Helpers.Controls.InternalClasses
{
    internal class DateTimeControlContext
    {
        private DateTime _date;

        public DateTimeControlContext()
        {
            _date = DateTime.Now;
        }

        public DateTimeControlContext(DateTime date)
            : this()
        {
            Date = date;
        }

        public DateTime Date {
            get { return _date; }
            set { _date = new DateTime(value.Year, value.Month, value.Day, _date.Hour, _date.Minute, _date.Second); ; }
        }

        public int Hour
        {
            get { return _date.Hour; }
            set { _date = new DateTime(_date.Year, _date.Month, _date.Day, value, _date.Minute, _date.Second); }
        }
    }
}
