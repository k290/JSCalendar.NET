using FluentValidation;
using Lib.Models;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace Lib.Builders
{
    public class JSTaskBuilder: JSCommonBuilder<JSTask, JSTaskBuilder>
    {
        protected override JSTask JsCalendarObject { get; set;  }


        public JSTaskBuilder()
        {
            JsCalendarObject = new JSTask();
        }


        public JSTask Build()
        {
            var validator = new JSTaskValidator();

            validator.ValidateAndThrow(JsCalendarObject);
            return JsCalendarObject;
        }

        public async Task<JSTask> BuildAsync()
        {
            var validator = new JSTaskValidator();

            await validator.ValidateAndThrowAsync(JsCalendarObject);
            return JsCalendarObject;
        }

    }
}
