using System;

namespace elp87.Helpers.Controls.InternalClasses
{
    internal class DateTimeControlContext
    {
        public DateTimeControlContext()
        {
        }

        public DateTimeControlContext(DateTime date)
            : this()
        {
            Date = date;
        }

        public DateTime Date { get; set; }
    }
}
