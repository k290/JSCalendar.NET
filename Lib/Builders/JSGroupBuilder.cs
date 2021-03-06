﻿using FluentValidation;
using Lib.Models;
using System;
using System.Text.Json;
using System.Threading.Tasks;

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

        public JSGroupBuilder WithSource(string source)
        {
            _jsCalendarObject.AddSource(source);
            return this;
        }

        public JSGroup Build()
        {
            var validator = new JSGroupValidator();

            validator.ValidateAndThrow(_jsCalendarObject);
            return _jsCalendarObject;
        }

        public async Task<JSGroup> BuildAsync()
        {
            var validator = new JSGroupValidator();

            await validator.ValidateAndThrowAsync(_jsCalendarObject);

            return _jsCalendarObject;
        }
    }
}
