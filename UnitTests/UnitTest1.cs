using FluentValidation;
using Lib.Builders;
using System;
using Xunit;

namespace UnitTests
{
    public class UnitTest1
    {
        [Fact]
        public void GivenAGroupBuilderWithNoUID_Throws()
        {

            Assert.Throws<ValidationException>(() => new JSGroupBuilder().Build());
        }
    }
}
