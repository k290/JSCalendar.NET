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

 
        public JSCalendarBuilder SetParentAsEvent(Action<JSEventBuilder> eventBuilderAction)
        {
            var eventBuilder = new JSEventBuilder();
            eventBuilderAction(eventBuilder);
            _jsCalendar.SetParent(eventBuilder.Build());
            return this;
        }

        public JSCalendarBuilder SetParentAsTask(Action<JSTaskBuilder> taskBuilderAction)
        {
            var taskBuilder = new JSTaskBuilder();
            taskBuilderAction(taskBuilder);
            _jsCalendar.SetParent(taskBuilder.Build());
            return this;
        }

        public JSCalendarBuilder SetParentAsGroup(Action<JSGroupBuilder> groupBuilderAction)
        {
            var groupBuilder = new JSGroupBuilder();
            groupBuilderAction(groupBuilder);
            _jsCalendar.SetParent(groupBuilder.Build());
            return this;
        }


        public JSCalendar Build()
        {
            //Validate JSCalendar Object. Todo generalize and validate on all 
            if(_jsCalendar.parentNode == null)
            {
                throw new ApplicationException("Must have a parent node");
            }

            return _jsCalendar;
        }
    }
}
