using FluentValidation;
using Lib.Models;
using System;
using System.Text.Json;

namespace Lib.Builders
{
    public class JSEventBuilder: JSCommonBuilder<JSEvent, JSEventBuilder>
    {
        protected override JSEvent _jsCalendarObject { get; set; }

        public JSEventBuilder()
        {
            _jsCalendarObject = new JSEvent();
        }



        public JSEvent Build()
        {
            var validator = new JSEventValidator();

            validator.ValidateAndThrow(_jsCalendarObject); //todo possibly use Async

            return _jsCalendarObject;
        }

    }
}
