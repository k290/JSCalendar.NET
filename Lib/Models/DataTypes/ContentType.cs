
using FluentValidation;
using FluentValidation.Validators;
using Lib.JsonConfiguration;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;

namespace Lib.Models.DataTypes
{

    //mhh todo cant rememvber what this was for. Seems like we were going to build something similar to UtcDate but for a ContentType field
    //public readonly struct ContentType
    //{
    //    private readonly string contentType;
    //    private readonly IDictionary<string, string> parameters;

    //    public ContentType(DateTime date)
    //    {
    //        this.date = date;
    //    }

    //    public static implicit operator DateTime(UtcDate u) => u.date;
    //    public static explicit operator UtcDate(DateTime d) => new(d);

    //    public override string ToString() => $"{date}";
    //}
}
