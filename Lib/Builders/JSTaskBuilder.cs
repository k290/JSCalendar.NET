using FluentValidation;
using Lib.Models;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace Lib.Builders
{
    public class JSTaskBuilder: JSCommonBuilder<JSTask, JSTaskBuilder, JSTaskValidator>
    {
        protected override JSTask JsCalendarObject { get; set;  }


        public JSTaskBuilder()
        {
            JsCalendarObject = new JSTask();
        }
    }
}
