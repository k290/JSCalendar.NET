using FluentValidation;
using Lib.Models;
using System;
using System.Text.Json;

namespace Lib.Builders
{
    public class JSEventBuilder
    {
        private JSEvent _jsEvent;

        public JSEventBuilder()
        {
            _jsEvent = new JSEvent();
        }


        public JSEventBuilder WithUid(string uid)
        {
            _jsEvent.uid = uid;
            return this;
        }

        public JSEvent Build()
        {
            var validator = new JSEventValidator();

            validator.ValidateAndThrow(_jsEvent); //todo possibly use Async

            return _jsEvent;
        }

    }
}
