using FluentValidation;
using Lib.Builders;
using System;
using System.Collections.Generic;
using System.Text.Json;
using Xunit;

namespace UnitTests.Serialization
{
    public class GroupBuilderTests
    {
        [Fact]
        public void GivenAGroupBuilderWithNoUID_ItThrowsValidationException()
        {

            Assert.Throws<ValidationException>(() => new JSGroupBuilder().Build());
        }

        [Fact]
        public void GivenAGroupBuilderWithBlankUID_ItThrowsValidationException()
        {

            Assert.Throws<ValidationException>(() => new JSGroupBuilder().WithUid("").Build());
        }

        [Fact]
        public void GivenAValidGroupBuilder_HasUidInResult()
        {
            var result = new JSGroupBuilder().WithUid("Valid").Build().GetJson();
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
        public void GivenAValidGroupBuilder_HasTypeInResult()
        {
            var result = new JSGroupBuilder().WithUid("Valid").Build().GetJson();
            var options = new JsonDocumentOptions
            {
                AllowTrailingCommas = true
            };
            using (var document = JsonDocument.Parse(result, options))
            {
                var rootElement = document.RootElement;
                var prop = rootElement.GetProperty("@type");
                Assert.Equal("jsgroup", prop.GetString());
            }
        }
        [Fact]
        public void GivenAValidGroupBuilderWithoutOptionalSource_SourceNotInResult()
        {
            var result = new JSGroupBuilder().WithUid("Valid").Build().GetJson();
            var options = new JsonDocumentOptions
            {
                AllowTrailingCommas = true
            };
            using (var document = JsonDocument.Parse(result, options))
            {
                var rootElement = document.RootElement;
                Assert.Throws<KeyNotFoundException>(()=>rootElement.GetProperty("source"));
            }
        }

        [Fact]
        public void GivenAValidGroupBuilderWithtOptionalSource_SourceInResult()
        {
            var result = new JSGroupBuilder().WithUid("Valid").WithSource("https://uri.com").Build().GetJson();
            var options = new JsonDocumentOptions
            {
                AllowTrailingCommas = true
            };
            using (var document = JsonDocument.Parse(result, options))
            {
                var rootElement = document.RootElement;
                var prop = rootElement.GetProperty("source");
                Assert.Equal("https://uri.com", prop.GetString());
            }
        }

        [Fact]
        public void GivenAValidGroupBuilder_HasEmptyArrayForEntries()
        {
            var result = new JSGroupBuilder().WithUid("Valid").Build().GetJson();
            var options = new JsonDocumentOptions
            {
                AllowTrailingCommas = true
            };
            using (var document = JsonDocument.Parse(result, options))
            {
                var rootElement = document.RootElement;
                var prop = rootElement.GetProperty("entries");
                Assert.Equal(0, prop.GetArrayLength());
            }
        }

        [Fact]
        public void GivenAValidGroupBuilderWith1Entries_HasPopulatedArraySize1()
        {
            var result = new JSGroupBuilder().WithUid("Valid").WithEvent(e=>e.WithUid("Event1")).Build().GetJson();
            var options = new JsonDocumentOptions
            {
                AllowTrailingCommas = true
            };
            using (var document = JsonDocument.Parse(result, options))
            {
                var rootElement = document.RootElement;
                var prop = rootElement.GetProperty("entries");
                Assert.Equal(1, prop.GetArrayLength());
            }
        }

        [Fact]
        public void GivenAValidGroupBuilderWithTwoOfSameEntries_HasPopulatedArraySize2()
        {
            var result = new JSGroupBuilder().WithUid("Valid").WithEvent(e => e.WithUid("Event1")).WithEvent(e=>e.WithUid("Event2")).Build().GetJson();
            var options = new JsonDocumentOptions
            {
                AllowTrailingCommas = true
            };
            using (var document = JsonDocument.Parse(result, options))
            {
                var rootElement = document.RootElement;
                var prop = rootElement.GetProperty("entries");
                Assert.Equal(2, prop.GetArrayLength());
            }
        }

        [Fact]
        public void GivenAValidGroupBuilderWithTwoOfDifferentEntries_HasPopulatedArraySize2()
        {
            var result = new JSGroupBuilder().WithUid("Valid").WithEvent(e => e.WithUid("Event1")).WithTask(t=>t.WithUid("Task1")).Build().GetJson();
            var options = new JsonDocumentOptions
            {
                AllowTrailingCommas = true
            };
            using (var document = JsonDocument.Parse(result, options))
            {
                var rootElement = document.RootElement;
                var prop = rootElement.GetProperty("entries");
                Assert.Equal(2, prop.GetArrayLength());
            }
        }

 
    }
}
