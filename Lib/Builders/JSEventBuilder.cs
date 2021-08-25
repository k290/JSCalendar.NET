using FluentValidation;
using Lib.Models;
using System;
using System.Text.Json;
using System.Threading.Tasks;
//todo https://stackoverflow.com/questions/55827354/github-repository-not-listing-in-google-search-no-way-to-submit-url
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
