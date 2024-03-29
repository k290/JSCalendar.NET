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
    public class TaskBuilderTests
    {

        private static JSTaskBuilder GetValidBuilder()
        {
            return new JSTaskBuilder().WithUid("Valid").WithUpdateDate(new DateTime(2021, 02, 01, 11, 20, 5, 100, DateTimeKind.Utc));
        }


        #region type
        [Fact]
        public async Task GivenAValidTaskBuilder_HasTypeInResult()
        {
            var result = await (await GetValidBuilder().BuildAsync()).GetJsonStreamAsync();
            var options = new JsonDocumentOptions
            {
                AllowTrailingCommas = true
            };
            using var document = await JsonDocument.ParseAsync(result, options);
            var rootElement = document.RootElement;
            var prop = rootElement.GetProperty("@type");
            Assert.Equal("jstask", prop.GetString());
        }
        #endregion
    }
}
