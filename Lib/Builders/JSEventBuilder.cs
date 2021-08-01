using FluentValidation;
using Lib.Models;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace Lib.Builders
{
    public class JSEventBuilder: JSCommonBuilder<JSEvent, JSEventBuilder>
    {
        protected override JSEvent JsCalendarObject { get; set; }

        public JSEventBuilder()
        {
            JsCalendarObject = new JSEvent();
        }



        public JSEvent Build()
        {
            var validator = new JSEventValidator();

            validator.ValidateAndThrow(JsCalendarObject);

            return JsCalendarObject;
        }

        public async Task<JSEvent> BuildAsync()
        {
            var validator = new JSEventValidator();

            await validator.ValidateAndThrowAsync(JsCalendarObject);

            return JsCalendarObject;
        }

    }
}
