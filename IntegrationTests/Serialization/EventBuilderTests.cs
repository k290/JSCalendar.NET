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
    public class EventBuilderTests
    {
        #region uid
        [Fact]
        public async Task GivenAnEventBuilderWithNoUID_ItThrowsValidationException()
        {

            await Assert.ThrowsAsync<ValidationException>(async () => await new JSEventBuilder().BuildAsync());
        }

        [Fact]
        public async Task GivenAnEventBuilderWithBlankUID_ItThrowsValidationException()
        {

            await Assert.ThrowsAsync<ValidationException>(async () => await new JSEventBuilder().WithUid("").BuildAsync());
        }

        [Fact]
        public async Task GivenAValidEventBuilder_HasUidInResult()
        {
            var result = await (await new JSEventBuilder().WithUid("Valid").BuildAsync()).GetJsonStreamAsync();
            var options = new JsonDocumentOptions
            {
                AllowTrailingCommas = true
            };
            using (var document = await JsonDocument.ParseAsync(result, options))
            {
                var rootElement = document.RootElement;
                var prop = rootElement.GetProperty("uid");
                Assert.Equal("Valid", prop.GetString());
            }
        }
        #endregion

        #region type
        [Fact]
        public async Task GivenAValidEventBuilder_HasTypeInResult()
        {
            var result = await (await new JSEventBuilder().WithUid("Valid").BuildAsync()).GetJsonStreamAsync();
            var options = new JsonDocumentOptions
            {
                AllowTrailingCommas = true
            };
            using (var document = await JsonDocument.ParseAsync(result, options))
            {
                var rootElement = document.RootElement;
                var prop = rootElement.GetProperty("@type");
                Assert.Equal("jsevent", prop.GetString());
            }
        }
        #endregion

        #region relatedTo
        [Fact]
        public async Task GivenAValidEventBuilderWithoutOptionalRelatedTo_RelatedToNotInResult()
        {
            var result = await (await new JSEventBuilder().WithUid("Valid").BuildAsync()).GetJsonStreamAsync();
            var options = new JsonDocumentOptions
            {
                AllowTrailingCommas = true
            };
            using (var document = await JsonDocument.ParseAsync(result, options))
            {
                var rootElement = document.RootElement;
                Assert.Throws<KeyNotFoundException>(() => rootElement.GetProperty("relatedTo"));
            }
        }

        [Fact]
        public async Task GivenAValidEventBuilder_WithValidOptionalRelatedTo_RelatedToInResult()
        {
            var result = await (await new JSEventBuilder().WithUid("Valid").WithRelatedTo("SomeId", r => { }).BuildAsync()).GetJsonStreamAsync();
            var options = new JsonDocumentOptions
            {
                AllowTrailingCommas = true
            };
            using (var document = await JsonDocument.ParseAsync(result, options))
            {
                var rootElement = document.RootElement;
                var relatedToProp = rootElement.GetProperty("relatedTo");
                var propExists = relatedToProp.TryGetProperty("SomeId", out _);
                Assert.True(propExists);

            }
        }

        [Fact]
        public async Task GivenAValidEventBuilder_WithMultipleOptionalRelatedTo_BothRelatedToInResult()
        {
            var result = await (await new JSEventBuilder().WithUid("Valid").WithRelatedTo("SomeId", r => { }).WithRelatedTo("SomeId2", r => { }).BuildAsync()).GetJsonStreamAsync();
            var options = new JsonDocumentOptions
            {
                AllowTrailingCommas = true
            };
            using (var document = await JsonDocument.ParseAsync(result, options))
            {
                var rootElement = document.RootElement;
                var relatedToProp = rootElement.GetProperty("relatedTo");
                var subProps = relatedToProp.EnumerateObject();
                Assert.Equal(2, subProps.Count());

            }
        }

        [Fact]
        public async Task GivenAValidEventBuilder_WithValidOptionalRelatedTo_RelatedToHasType()
        {
            var result = await (await new JSEventBuilder().WithUid("Valid").WithRelatedTo("SomeId", r => { }).BuildAsync()).GetJsonStreamAsync();
            var options = new JsonDocumentOptions
            {
                AllowTrailingCommas = true
            };
            using (var document = await JsonDocument.ParseAsync(result, options))
            {
                var rootElement = document.RootElement;
                var relatedToProp = rootElement.GetProperty("relatedTo");
                var relatedToIdProp = relatedToProp.GetProperty("SomeId");
                var typeProp = relatedToIdProp.GetProperty("@type");
                Assert.Equal("Relation", typeProp.GetString());

            }
        }

        [Fact]
        public async Task GivenAValidEventBuilder_WithEmptyRelationsInRelatedTo_EmptyObjectInResult()
        {
            var result = await (await new JSEventBuilder().WithUid("Valid").WithRelatedTo("SomeId", r => { }).BuildAsync()).GetJsonStreamAsync();
            var options = new JsonDocumentOptions
            {
                AllowTrailingCommas = true
            };
            using (var document = await JsonDocument.ParseAsync(result, options))
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
        public async Task GivenAValidEventBuilder_WithRelationsInRelatedTo_RelationsExist()
        {
            var result = await (await new JSEventBuilder().WithUid("Valid").WithRelatedTo("SomeId", r => r.WithRelation(RelationType.Parent)).BuildAsync()).GetJsonStreamAsync();
            var options = new JsonDocumentOptions
            {
                AllowTrailingCommas = true
            };
            using (var document = await JsonDocument.ParseAsync(result, options))
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
        public async Task GivenAValidEventBuilder_WithMultipleRelationsInRelatedTo_BothRelationsExist()
        {
            var result = await (await new JSEventBuilder().WithUid("Valid").WithRelatedTo("SomeId", r => r.WithRelation(RelationType.Parent).WithRelation(RelationType.Child)).BuildAsync()).GetJsonStreamAsync();
            var options = new JsonDocumentOptions
            {
                AllowTrailingCommas = true
            };
            using (var document = await JsonDocument.ParseAsync(result, options))
            {
                var rootElement = document.RootElement;
                var relatedToProp = rootElement.GetProperty("relatedTo");
                var relatedToIdProp = relatedToProp.GetProperty("SomeId");
                var relationProp = relatedToIdProp.GetProperty("relation");
                var subProps = relationProp.EnumerateObject();
                Assert.Equal(2, subProps.Count());
            }
        }

        [Fact]
        public async Task GivenValidEventBuilder_WithDuplicateRelatedTo_ValidationExceptionIsThrown()
        {
            await Assert.ThrowsAsync<ValidationException>(
                async () => await new JSEventBuilder().WithUid("Invalid")
                .WithRelatedTo("SomeId", r => r.WithRelation(RelationType.Parent))
                .WithRelatedTo("SomeId", r => r.WithRelation(RelationType.Child))
                .BuildAsync());
        }

        #endregion

        #region prodId

        [Fact]
        public async Task GivenAValidTaskBuilder_WithOptionalProdId_HasProdIdInResult()
        {
            var result = await (await new JSTaskBuilder().WithUid("Valid").WithProdId("A-GUID").BuildAsync()).GetJsonStreamAsync();
            var options = new JsonDocumentOptions
            {
                AllowTrailingCommas = true
            };
            using (var document = await JsonDocument.ParseAsync(result, options))
            {
                var rootElement = document.RootElement;
                var prop = rootElement.GetProperty("prodId");
                Assert.Equal("A-GUID", prop.GetString());
            }
        }

        [Fact]
        public async Task GivenAValidTaskBuilder_WithoutOptionalProdId_NoProdIdInResult()
        {
            var result = await (await new JSTaskBuilder().WithUid("Valid").BuildAsync()).GetJsonStreamAsync();
            var options = new JsonDocumentOptions
            {
                AllowTrailingCommas = true
            };
            using (var document = await JsonDocument.ParseAsync(result, options))
            {
                var rootElement = document.RootElement;
                Assert.Throws<KeyNotFoundException>(() => rootElement.GetProperty("prodId"));
            }
        }
        #endregion
    }
}
