using FluentValidation;
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

        public JSGroupBuilder WithSource(string source)  //todo MUST be a URI
        {
            _jsCalendarObject.AddSource(source);
            return this;
        }

        public JSGroup Build()
        {
            var validator = new JSGroupValidator();

            validator.ValidateAndThrow(_jsCalendarObject); //todo possibly use Async
            return _jsCalendarObject;
        }
    }
}
