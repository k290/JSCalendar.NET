using FluentValidation;
using Lib.Builders;
using System;
using System.Collections.Generic;
using System.Text.Json;
using Xunit;

namespace UnitTests.Serialization
{
    public class TaskBuilderTests
    {
        [Fact]
        public void GivenATaskBuilderWithNoUID_ItThrowsValidationException()
        {

            Assert.Throws<ValidationException>(() => new JSTaskBuilder().Build());
        }

        [Fact]
        public void GivenATaskBuilderWithBlankUID_ItThrowsValidationException()
        {

            Assert.Throws<ValidationException>(() => new JSTaskBuilder().WithUid("").Build());
        }

        [Fact]
        public void GivenAValidTaskBuilder_HasUidInResult()
        {
            var result = new JSTaskBuilder().WithUid("Valid").Build().GetJson();
            var options = new JsonDocumentOptions
            {
                AllowTrailingCommas = true
            };
            using (var document = JsonDocument.Parse(result, options))
            {
                var rootElement = document.RootElement;
                var prop = rootElement.GetProperty("uid");
                Assert.Equal("Valid", prop.GetString());
            }
        }

        [Fact]
        public void GivenAValidTaskBuilder_HasTypeInResult()
        {
            var result = new JSTaskBuilder().WithUid("Valid").Build().GetJson();
            var options = new JsonDocumentOptions
            {
                AllowTrailingCommas = true
            };
            using (var document = JsonDocument.Parse(result, options))
            {
                var rootElement = document.RootElement;
                var prop = rootElement.GetProperty("@type");
                Assert.Equal("jstask", prop.GetString());
            }
        } 
    }
}
