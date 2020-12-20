using FluentValidation;
using Lib.Models;
using System;
using System.Text.Json;
using System.Threading.Tasks;

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

        public async Task<JSTask> BuildAsync()
        {
            var validator = new JSTaskValidator();

            await validator.ValidateAndThrowAsync(_jsCalendarObject); //todo possibly use Async
            return _jsCalendarObject;
        }

    }
}
