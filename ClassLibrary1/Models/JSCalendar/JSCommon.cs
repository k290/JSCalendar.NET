﻿using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Lib.Models
{


    public abstract class JSCommon
    {
        [JsonIgnore]
        public abstract string type { get; }
        public string uid { get; internal set; }
        public string relatedTo { get; internal set; }
    }

    public class JSCommonValidator : AbstractValidator<JSCommon>
    {
        public JSCommonValidator()
        {
            RuleFor(e => e.type).NotEmpty();
            RuleFor(e => e.uid).NotEmpty();
        }
    }
}