using FluentValidation;
using Lib.Models;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace Lib.Builders
{
    public class JSGroupBuilder: JSCommonBuilder<JSGroup, JSGroupBuilder>
    {
        protected override JSGroup JsCalendarObject { get ; set; }

        public JSGroupBuilder()
        {
            JsCalendarObject = new JSGroup();
        }

 
        public JSGroupBuilder WithEvent(Action<JSEventBuilder> eventBuilderAction)
        {
            var eventBuilder = new JSEventBuilder();
            eventBuilderAction(eventBuilder);
            JsCalendarObject.Entries.Add(eventBuilder.Build());
            return this;
        }

        public JSGroupBuilder WithTask(Action<JSTaskBuilder> taskBuilderAction)
        {
            var taskBuilder = new JSTaskBuilder();
            taskBuilderAction(taskBuilder);
            JsCalendarObject.Entries.Add(taskBuilder.Build());
            return this;
        }

        public JSGroupBuilder WithSource(string source)
        {
            JsCalendarObject.Source = source;
            return this;
        }

        public JSGroup Build()
        {
            var validator = new JSGroupValidator();

            validator.ValidateAndThrow(JsCalendarObject);
            return JsCalendarObject;
        }

        public async Task<JSGroup> BuildAsync()
        {
            var validator = new JSGroupValidator();

            await validator.ValidateAndThrowAsync(JsCalendarObject);

            return JsCalendarObject;
        }
    }
}
