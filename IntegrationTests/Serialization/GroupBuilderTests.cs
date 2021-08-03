using FluentValidation;
using Lib.Builders;
using Lib.Models.DataTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;

namespace IntegrationTests.Serialization
{


    public class GroupBuilderTests
    {
        private static JSGroupBuilder GetValidBuilder()
        {
            return new JSGroupBuilder().WithUid("Valid").WithUpdateDate(new DateTime(2021, 02, 01, 11, 20, 5, 100, DateTimeKind.Utc));
        }

        private static void SetValidEventBuilder(JSEventBuilder eventBuilder, string uid)
        {
            eventBuilder.WithUid(uid).WithUpdateDate(new DateTime(2021, 02, 01, 11, 20, 5, 100, DateTimeKind.Utc));
        }

        private static void SetValidTaskBuilder(JSTaskBuilder taskBuilder, string uid)
        {
            taskBuilder.WithUid(uid).WithUpdateDate(new DateTime(2021, 02, 01, 11, 20, 5, 100, DateTimeKind.Utc));
        }


        #region type

        [Fact]
        public async Task GivenAValidGroupBuilder_HasTypeInResult()
        {
            var result = await (await GetValidBuilder().BuildAsync()).GetJsonStreamAsync();
            var options = new JsonDocumentOptions
            {
                AllowTrailingCommas = true
            };
            using var document = await JsonDocument.ParseAsync(result, options);
            var rootElement = document.RootElement;
            var prop = rootElement.GetProperty("@type");
            Assert.Equal("jsgroup", prop.GetString());
        }

        #endregion

        #region source
        [Fact]
        public async Task GivenAValidGroupBuilderWithoutOptionalSource_SourceNotInResult()
        {
            var result = await (await GetValidBuilder().BuildAsync()).GetJsonStreamAsync();
            var options = new JsonDocumentOptions
            {
                AllowTrailingCommas = true
            };
            using var document = await JsonDocument.ParseAsync(result, options);
            var rootElement = document.RootElement;
            Assert.Throws<KeyNotFoundException>(() => rootElement.GetProperty("source"));
        }

        [Fact]
        public async Task GivenAValidGroupBuilderWithOptionalSource_SourceInResult()
        {
            var result = await (await GetValidBuilder().WithSource("https://uri.com").BuildAsync()).GetJsonStreamAsync();
            var options = new JsonDocumentOptions
            {
                AllowTrailingCommas = true
            };
            using var document = await JsonDocument.ParseAsync(result, options);
            var rootElement = document.RootElement;
            var prop = rootElement.GetProperty("source");
            Assert.Equal("https://uri.com", prop.GetString());
        }
        #endregion

        #region entries

        [Fact]
        public async Task GivenAValidGroupBuilder_HasEmptyArrayForEntries()
        {
            var result = await (await GetValidBuilder().BuildAsync()).GetJsonStreamAsync();
            var options = new JsonDocumentOptions
            {
                AllowTrailingCommas = true
            };
            using var document = await JsonDocument.ParseAsync(result, options);
            var rootElement = document.RootElement;
            var prop = rootElement.GetProperty("entries");
            Assert.Equal(0, prop.GetArrayLength());
        }

        [Fact]
        public async Task GivenAValidGroupBuilderWith1Entries_HasEventInPopulatedArray()
        {
            var result = await (await GetValidBuilder().WithEvent(e => SetValidEventBuilder(e, "Event1")).BuildAsync()).GetJsonStreamAsync();
            var options = new JsonDocumentOptions
            {
                AllowTrailingCommas = true
            };
            using var document = await JsonDocument.ParseAsync(result, options);
            var rootElement = document.RootElement;
            var entries = rootElement.GetProperty("entries").EnumerateArray();
            var uid = entries.ElementAt(0).GetProperty("uid");
            Assert.Equal("Event1", uid.GetString());
        }

        [Fact]
        public async Task GivenAValidGroupBuilderWithTwoOfSameEntries_HasPopulatedArraySize2()
        {
            var result = await (await GetValidBuilder().WithEvent(e => SetValidEventBuilder(e, "Event1")).WithEvent(e => SetValidEventBuilder(e, "Event2")).BuildAsync()).GetJsonStreamAsync();
            var options = new JsonDocumentOptions
            {
                AllowTrailingCommas = true
            };
            using var document = await JsonDocument.ParseAsync(result, options);
            var rootElement = document.RootElement;
            var entries = rootElement.GetProperty("entries").EnumerateArray();
            var event1 = entries.ElementAt(0).GetProperty("uid");
            var event2 = entries.ElementAt(1).GetProperty("uid");
            Assert.Equal("Event1", event1.GetString());
            Assert.Equal("Event2", event2.GetString());
        }

        [Fact]
        public async Task GivenAValidGroupBuilderWithTwoOfDifferentEntries_HasPopulatedArraySize2()
        {
            var result = await (await GetValidBuilder().WithEvent(e => SetValidEventBuilder(e, "Event1")).WithTask(t => SetValidTaskBuilder(t, "Task1")).BuildAsync()).GetJsonStreamAsync();
            var options = new JsonDocumentOptions
            {
                AllowTrailingCommas = true
            };
            using var document = await JsonDocument.ParseAsync(result, options);
            var rootElement = document.RootElement;
            var entries = rootElement.GetProperty("entries").EnumerateArray();
            var event1 = entries.ElementAt(0).GetProperty("uid");
            var event2 = entries.ElementAt(1).GetProperty("uid");
            Assert.Equal("Event1", event1.GetString());
            Assert.Equal("Task1", event2.GetString());
        }

        #endregion

    }
}
