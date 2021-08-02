using FluentValidation;
using Lib.Models;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace Lib.Builders
{
    public class JSEventBuilder: JSCommonBuilder<JSEvent, JSEventBuilder, JSEventValidator>
    {
        protected override JSEvent JsCalendarObject { get; set; }

        public JSEventBuilder()
        {
            JsCalendarObject = new JSEvent();
        }


    }
}
