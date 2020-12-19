using System;

namespace Builder
{
    public class JSCalendarBuilder
    {
        private JSCalendar _jsCalendar;

        public JSCalendarBuilder()
        {
            _jsCalendar = new JSCalendar();
        }

 
        public JSCalendarBuilder Event()
        {
            return this;
        }
  

        public string Build()
        {
            return _jsCalendar.ToString();
        }
    }
}
