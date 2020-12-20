using FluentValidation;
using Lib.Builders;
using System;
using System.Collections.Generic;
using System.Text.Json;
using Xunit;

namespace UnitTests.Serialization
{
    public class EventBuilderTests
    {
        [Fact]
        public void GivenAnEventBuilderWithNoUID_ItThrowsValidationException()
        {

            Assert.Throws<ValidationException>(() => new JSEventBuilder().Build());
        }

        [Fact]
        public void GivenAnEventBuilderWithBlankUID_ItThrowsValidationException()
        {

            Assert.Throws<ValidationException>(() => new JSEventBuilder().WithUid("").Build());
        }

        [Fact]
        public void GivenAValidEventBuilder_HasUidInResult()
        {
            var result = new JSEventBuilder().WithUid("Valid").Build().GetJson();
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
        public void GivenAValidEventBuilder_HasTypeInResult()
        {
            var result = new JSEventBuilder().WithUid("Valid").Build().GetJson();
            var options = new JsonDocumentOptions
            {
                AllowTrailingCommas = true
            };
            using (var document = JsonDocument.Parse(result, options))
            {
                var rootElement = document.RootElement;
                var prop = rootElement.GetProperty("@type");
                Assert.Equal("jsevent", prop.GetString());
            }
        }
    }
}
