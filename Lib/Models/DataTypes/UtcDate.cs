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

    public readonly struct UtcDate
    {
        private readonly DateTime date;

        public UtcDate(DateTime date)
        {
            this.date = date;
        }

        public static implicit operator DateTime(UtcDate u) => u.date;
        public static explicit operator UtcDate(DateTime d) => new(d);

        public override string ToString() => $"{date}";
    }

    public class UtcDateValidator : AbstractValidator<UtcDate>
    {
        public UtcDateValidator()
        {

            RuleFor(e => e).Must((context, date) => ((DateTime)date).Kind == DateTimeKind.Utc).WithMessage("DateTime Kind for updated must be UTC");

        }
    }

    public class NullUtcDateValidator : AbstractValidator<UtcDate?>
    {
        public NullUtcDateValidator()
        {

            RuleFor(e => e).Must((context, date) => date is null || ((DateTime)date).Kind == DateTimeKind.Utc).WithMessage("DateTime Kind for updated must be UTC");

        }
    }
}
