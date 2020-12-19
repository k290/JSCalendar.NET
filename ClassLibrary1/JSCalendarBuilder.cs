using Models;
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

 
        public JSCalendarBuilder WithEvent(Action<JSEventBuilder> eventBuilderAction)
        {
            var eventBuilder = new JSEventBuilder();
            eventBuilderAction(eventBuilder);
            _jsCalendar.AddEvent(eventBuilder.Build());
            return this;
        }
  

        public JSCalendar Build()
        {
            return _jsCalendar;
        }
    }
}
