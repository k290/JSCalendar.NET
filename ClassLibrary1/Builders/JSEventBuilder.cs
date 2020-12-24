using FluentValidation;
using Lib.Models;
using System;
using System.Text.Json;
using System.Threading.Tasks;

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

            validator.ValidateAndThrow(_jsCalendarObject);

            return _jsCalendarObject;
        }

        public async Task<JSEvent> BuildAsync()
        {
            var validator = new JSEventValidator();

            await validator.ValidateAndThrowAsync(_jsCalendarObject);

            return _jsCalendarObject;
        }

    }
}
