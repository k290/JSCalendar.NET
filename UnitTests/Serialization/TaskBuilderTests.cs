using FluentValidation;
using Lib.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;

namespace UnitTests.Serialization
{
    public class TaskBuilderTests
    {
        [Fact]
        public async Task GivenATaskBuilderWithNoUID_ItThrowsValidationException()
        {

            await Assert.ThrowsAsync<ValidationException>(async () => await new JSTaskBuilder().BuildAsync());
        }

        [Fact]
        public async Task GivenATaskBuilderWithBlankUID_ItThrowsValidationException()
        {

           await Assert.ThrowsAsync<ValidationException>(async () => await new JSTaskBuilder().WithUid("").BuildAsync());
        }

        [Fact]
        public async Task GivenAValidTaskBuilder_HasUidInResult()
        {
            var result = await (await new JSTaskBuilder().WithUid("Valid").BuildAsync()).GetJsonAsync();
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
        public async Task GivenAValidTaskBuilder_HasTypeInResult()
        {
            var result = await (await new JSTaskBuilder().WithUid("Valid").BuildAsync()).GetJsonAsync();
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

        [Fact]
        public async Task GivenAValidTaskBuilderWithoutOptionalRelatedTo_RelatedToNotInResult()
        {
            var result = await (await new JSTaskBuilder().WithUid("Valid").BuildAsync()).GetJsonAsync();
            var options = new JsonDocumentOptions
            {
                AllowTrailingCommas = true
            };
            using (var document = JsonDocument.Parse(result, options))
            {
                var rootElement = document.RootElement;
                Assert.Throws<KeyNotFoundException>(() => rootElement.GetProperty("relatedTo"));
            }
        }

        [Fact]
        public async Task GivenAValidTaskBuilder_WithValidOptionalRelatedTo_RelatedToInResult()
        {
            var result = await (await new JSTaskBuilder().WithUid("Valid").WithRelatedTo("SomeId", r => { }).BuildAsync()).GetJsonAsync();
            var options = new JsonDocumentOptions
            {
                AllowTrailingCommas = true
            };
            using (var document = JsonDocument.Parse(result, options))
            {
                var rootElement = document.RootElement;
                var relatedToProp = rootElement.GetProperty("relatedTo");
                var propExists = relatedToProp.TryGetProperty("SomeId", out _);
                Assert.True(propExists);

            }
        }

        [Fact]
        public async Task GivenAValidTaskBuilder_WithMultipleOptionalRelatedTo_BothRelatedToInResult()
        {
            var result = await (await new JSTaskBuilder().WithUid("Valid").WithRelatedTo("SomeId", r => { }).WithRelatedTo("SomeId2", r => { }).BuildAsync()).GetJsonAsync();
            var options = new JsonDocumentOptions
            {
                AllowTrailingCommas = true
            };
            using (var document = JsonDocument.Parse(result, options))
            {
                var rootElement = document.RootElement;
                var relatedToProp = rootElement.GetProperty("relatedTo");
                var subProps = relatedToProp.EnumerateObject();
                Assert.Equal(2, subProps.Count());

            }
        }

        [Fact]
        public async Task GivenAValidTaskBuilder_WithValidOptionalRelatedTo_RelatedToHasType()
        {
            var result = await (await new JSTaskBuilder().WithUid("Valid").WithRelatedTo("SomeId", r => { }).BuildAsync()).GetJsonAsync();
            var options = new JsonDocumentOptions
            {
                AllowTrailingCommas = true
            };
            using (var document = JsonDocument.Parse(result, options))
            {
                var rootElement = document.RootElement;
                var relatedToProp = rootElement.GetProperty("relatedTo");
                var relatedToIdProp = relatedToProp.GetProperty("SomeId");
                var typeProp = relatedToIdProp.GetProperty("@type");
                Assert.Equal("Relation", typeProp.GetString());

            }
        }

        [Fact]
        public async Task GivenAValidTaskBuilder_WithEmptyRelationsInRelatedTo_EmptyObjectInResult()
        {
            var result = await (await new JSTaskBuilder().WithUid("Valid").WithRelatedTo("SomeId", r => { }).BuildAsync()).GetJsonAsync();
            var options = new JsonDocumentOptions
            {
                AllowTrailingCommas = true
            };
            using (var document = JsonDocument.Parse(result, options))
            {
                var rootElement = document.RootElement;
                var relatedToProp = rootElement.GetProperty("relatedTo");
                var relatedToIdProp = relatedToProp.GetProperty("SomeId");
                var relationProp = relatedToIdProp.GetProperty("relation");
                Assert.Equal(JsonValueKind.Object, relationProp.ValueKind);
                Assert.Equal(JsonValueKind.Undefined, relationProp.EnumerateObject().Current.Value.ValueKind);

            }
        }


        [Fact]
        public async Task GivenAValidTaskBuilder_WithRelationsInRelatedTo_RelationsExist()
        {
            var result = await (await new JSTaskBuilder().WithUid("Valid").WithRelatedTo("SomeId", r => r.WithRelation("parent")).BuildAsync()).GetJsonAsync();
            var options = new JsonDocumentOptions
            {
                AllowTrailingCommas = true
            };
            using (var document = JsonDocument.Parse(result, options))
            {
                var rootElement = document.RootElement;
                var relatedToProp = rootElement.GetProperty("relatedTo");
                var relatedToIdProp = relatedToProp.GetProperty("SomeId");
                var relationProp = relatedToIdProp.GetProperty("relation");
                var relationTypeProp = relationProp.GetProperty("parent");
                Assert.True(relationTypeProp.GetBoolean());
            }
        }

        [Fact]
        public async Task GivenAValidTaskBuilder_WithMultipleRelationsInRelatedTo_BothRelationsExist()
        {
            var result = await (await new JSTaskBuilder().WithUid("Valid").WithRelatedTo("SomeId", r => r.WithRelation("parent").WithRelation("child")).BuildAsync()).GetJsonAsync();
            var options = new JsonDocumentOptions
            {
                AllowTrailingCommas = true
            };
            using (var document = JsonDocument.Parse(result, options))
            {
                var rootElement = document.RootElement;
                var relatedToProp = rootElement.GetProperty("relatedTo");
                var relatedToIdProp = relatedToProp.GetProperty("SomeId");
                var relationProp = relatedToIdProp.GetProperty("relation");
                var subProps = relationProp.EnumerateObject();
                Assert.Equal(2, subProps.Count());
            }
        }
    }
}
