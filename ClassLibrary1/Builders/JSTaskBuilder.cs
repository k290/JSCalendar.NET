using FluentValidation;
using Lib.Models;
using System;
using System.Text.Json;

namespace Lib.Builders
{
    public class JSTaskBuilder: JSCommonBuilder<JSTask, JSTaskBuilder>
    {
        protected override JSTask _jsCalendarObject { get; set;  }


        public JSTaskBuilder()
        {
            _jsCalendarObject = new JSTask();
        }


        public JSTask Build()
        {
            var validator = new JSTaskValidator();

            validator.ValidateAndThrow(_jsCalendarObject); //todo possibly use Async
            return _jsCalendarObject;
        }
    }
}
