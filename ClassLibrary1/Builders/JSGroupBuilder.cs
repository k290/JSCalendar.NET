using Lib.Models;
using System;
using System.Text.Json;

namespace Lib.Builders
{
    public class JSGroupBuilder: JSCommonBuilder<JSGroup, JSGroupBuilder>
    {
        protected override JSGroup _jsCalendarObject { get ; set; }

        public JSGroupBuilder()
        {
            _jsCalendarObject = new JSGroup();
        }

 
        public JSGroupBuilder WithEvent(Action<JSEventBuilder> eventBuilderAction)
        {
            var eventBuilder = new JSEventBuilder();
            eventBuilderAction(eventBuilder);
            _jsCalendarObject.AddEntry(eventBuilder.Build());
            return this;
        }

        public JSGroupBuilder WithTask(Action<JSTaskBuilder> taskBuilderAction)
        {
            var taskBuilder = new JSTaskBuilder();
            taskBuilderAction(taskBuilder);
            _jsCalendarObject.AddEntry(taskBuilder.Build());
            return this;
        }

        public JSGroupBuilder WithUid(string uid) //might want to autogenerate in the future and provide a GetUID method
        {
            _jsCalendarObject.uid = uid;
            return this;
        }

        public JSGroup Build()
        {
            return _jsCalendarObject;
        }
    }
}
