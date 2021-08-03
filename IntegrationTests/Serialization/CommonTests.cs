
using FluentValidation;
using Lib.Builders;
using Lib.Models;
using Lib.Models.DataTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Xunit;

namespace IntegrationTests.Serialization
{

    public sealed class JSCommonTest : JSCommon
    {
        public override string Type => "common-test";
    }

    public sealed class JSCommonTestBuilder : JSCommonBuilder<JSCommonTest, JSCommonTestBuilder, JSCommonTestValidator>
    {
        protected override JSCommonTest JsCalendarObject { get; set; }

        public JSCommonTestBuilder()
        {
            JsCalendarObject = new JSCommonTest();
        }
    }

    public sealed class JSCommonTestValidator : AbstractValidator<JSCommonTest>
    {
        public JSCommonTestValidator()
        {
            Include(new JSCommonValidator());
        }
    }
    public class CommonTests
    {
        private static JSCommonTestBuilder GetValidBuilder()
        {
            return new JSCommonTestBuilder().WithUid("Valid").WithUpdateDate(new DateTime(2021, 02, 01, 11, 20, 5, 100, DateTimeKind.Utc));
        }

        #region uid
        [Fact]
        public async Task GivenABuilderWithNoUID_ItThrowsValidationException()
        {

            await Assert.ThrowsAsync<ValidationException>(async () => await new JSCommonTestBuilder().BuildAsync());
        }

        [Fact]
        public async Task GivenAnTestBuilderWithBlankUID_ItThrowsValidationException()
        {

            await Assert.ThrowsAsync<ValidationException>(async () => await new JSCommonTestBuilder().WithUid("").BuildAsync());
        }

        [Fact]
        public async Task GivenAValidTestBuilder_HasUidInResult()
        {
            var result = await (await GetValidBuilder().BuildAsync()).GetJsonStreamAsync();
            var options = new JsonDocumentOptions
            {
                AllowTrailingCommas = true
            };
            using var document = await JsonDocument.ParseAsync(result, options);
            var rootElement = document.RootElement;
            var prop = rootElement.GetProperty("uid");
            Assert.Equal("Valid", prop.GetString());
        }
        #endregion


        #region updated
        [Fact]
        public async Task GivenAnTestBuilderWithNoUpdateDate_ItThrowsValidationException()
        {

            await Assert.ThrowsAsync<ValidationException>(async () => await new JSCommonTestBuilder().WithUid("Invalid").BuildAsync());
        }

        [Fact]
        public async Task GivenAnTestBuilderWithNonUtcUpdateDate_ItThrowsValidationException()
        {

            await Assert.ThrowsAsync<ValidationException>(async () => await new JSCommonTestBuilder().WithUid("Invalid").WithUpdateDate(new DateTime(2021, 02, 01, 11, 20, 5, 0)).BuildAsync());
        }

        [Fact]
        public async Task GivenAnTestBuilderWithUpdateDate_HasUpdateDateInResultWithoutTrailingZerosInFraction()
        {
            var result = await (await new JSCommonTestBuilder().WithUid("Valid").WithUpdateDate(new DateTime(2021, 02, 01, 11, 20, 5, 100, DateTimeKind.Utc)).BuildAsync()).GetJsonStreamAsync();
            var options = new JsonDocumentOptions
            {
                AllowTrailingCommas = true
            };
            using var document = await JsonDocument.ParseAsync(result, options);
            var rootElement = document.RootElement;
            var prop = rootElement.GetProperty("updated");
            Assert.Equal("2021-02-01T11:20:05.1Z", prop.GetString());
        }

        [Fact]
        public async Task GivenAnTestBuilderWithUpdateDateZeroMillis_HasNoMillisInResult()
        {
            var result = await (await new JSCommonTestBuilder().WithUid("Valid").WithUpdateDate(new DateTime(2021, 02, 01, 11, 20, 5, 0, DateTimeKind.Utc)).BuildAsync()).GetJsonStreamAsync();
            var options = new JsonDocumentOptions
            {
                AllowTrailingCommas = true
            };
            using var document = await JsonDocument.ParseAsync(result, options);
            var rootElement = document.RootElement;
            var prop = rootElement.GetProperty("updated");
            Assert.Equal("2021-02-01T11:20:05Z", prop.GetString());
        }
        #endregion

        #region created
        [Fact]
        public async Task GivenAnTestBuilderWithNonUtcCreateDate_ItThrowsValidationException()
        {

            await Assert.ThrowsAsync<ValidationException>(async () => await new JSCommonTestBuilder().WithUid("Invalid").WithUpdateDate(new DateTime(2021, 02, 01, 11, 20, 5, 0, DateTimeKind.Utc)).WithCreateDate(new DateTime(2021, 02, 01, 11, 20, 5, 0)).BuildAsync());
        }

        [Fact]
        public async Task GivenAValidTestBuilderWithCreateDate_HasCreateDateInResultWithoutTrailingZerosInFraction()
        {
            var result = await (await GetValidBuilder().WithCreateDate(new DateTime(2021, 02, 01, 11, 20, 5, 100, DateTimeKind.Utc)).BuildAsync()).GetJsonStreamAsync();
            var options = new JsonDocumentOptions
            {
                AllowTrailingCommas = true
            };
            using var document = await JsonDocument.ParseAsync(result, options);
            var rootElement = document.RootElement;
            var prop = rootElement.GetProperty("created");
            Assert.Equal("2021-02-01T11:20:05.1Z", prop.GetString());
        }

        [Fact]
        public async Task GivenAnTestBuilderWithCreateDateZeroMillis_HasNoMillisInResult()
        {
            var result = await (await GetValidBuilder().WithCreateDate(new DateTime(2021, 02, 01, 11, 20, 5, 0, DateTimeKind.Utc)).BuildAsync()).GetJsonStreamAsync();
            var options = new JsonDocumentOptions
            {
                AllowTrailingCommas = true
            };
            using var document = await JsonDocument.ParseAsync(result, options);
            var rootElement = document.RootElement;
            var prop = rootElement.GetProperty("created");
            Assert.Equal("2021-02-01T11:20:05Z", prop.GetString());
        }

        [Fact]
        public async Task GivenAValidTestBuilderWithoutOptionalCreated_CreatedNotInResult()
        {
            var result = await (await GetValidBuilder().BuildAsync()).GetJsonStreamAsync();
            var options = new JsonDocumentOptions
            {
                AllowTrailingCommas = true
            };
            using var document = await JsonDocument.ParseAsync(result, options);
            var rootElement = document.RootElement;
            Assert.Throws<KeyNotFoundException>(() => rootElement.GetProperty("created"));
        }

        #endregion

        #region relatedTo
        [Fact]
        public async Task GivenAValidTestBuilderWithoutOptionalRelatedTo_RelatedToNotInResult()
        {
            var result = await (await GetValidBuilder().BuildAsync()).GetJsonStreamAsync();
            var options = new JsonDocumentOptions
            {
                AllowTrailingCommas = true
            };
            using var document = await JsonDocument.ParseAsync(result, options);
            var rootElement = document.RootElement;
            Assert.Throws<KeyNotFoundException>(() => rootElement.GetProperty("relatedTo"));
        }

        [Fact]
        public async Task GivenAValidTestBuilder_WithValidOptionalRelatedTo_RelatedToInResult()
        {
            var result = await (await GetValidBuilder().WithRelatedTo("SomeId", r => { }).BuildAsync()).GetJsonStreamAsync();
            var options = new JsonDocumentOptions
            {
                AllowTrailingCommas = true
            };
            using var document = await JsonDocument.ParseAsync(result, options);
            var rootElement = document.RootElement;
            var relatedToProp = rootElement.GetProperty("relatedTo");
            var propExists = relatedToProp.TryGetProperty("SomeId", out _);
            Assert.True(propExists);
        }

        [Fact]
        public async Task GivenAValidTestBuilder_WithMultipleOptionalRelatedTo_BothRelatedToInResult()
        {
            var result = await (await GetValidBuilder().WithRelatedTo("SomeId", r => { }).WithRelatedTo("SomeId2", r => { }).BuildAsync()).GetJsonStreamAsync();
            var options = new JsonDocumentOptions
            {
                AllowTrailingCommas = true
            };
            using var document = await JsonDocument.ParseAsync(result, options);
            var rootElement = document.RootElement;
            var relatedToProp = rootElement.GetProperty("relatedTo");
            var subProps = relatedToProp.EnumerateObject();
            Assert.Equal(2, subProps.Count());
        }

        [Fact]
        public async Task GivenAValidTestBuilder_WithValidOptionalRelatedTo_RelatedToHasType()
        {
            var result = await (await GetValidBuilder().WithRelatedTo("SomeId", r => { }).BuildAsync()).GetJsonStreamAsync();
            var options = new JsonDocumentOptions
            {
                AllowTrailingCommas = true
            };
            using var document = await JsonDocument.ParseAsync(result, options);
            var rootElement = document.RootElement;
            var relatedToProp = rootElement.GetProperty("relatedTo");
            var relatedToIdProp = relatedToProp.GetProperty("SomeId");
            var typeProp = relatedToIdProp.GetProperty("@type");
            Assert.Equal("Relation", typeProp.GetString());
        }

        [Fact]
        public async Task GivenAValidTestBuilder_WithEmptyRelationsInRelatedTo_EmptyObjectInResult()
        {
            var result = await (await GetValidBuilder().WithRelatedTo("SomeId", r => { }).BuildAsync()).GetJsonStreamAsync();
            var options = new JsonDocumentOptions
            {
                AllowTrailingCommas = true
            };
            using var document = await JsonDocument.ParseAsync(result, options);
            var rootElement = document.RootElement;
            var relatedToProp = rootElement.GetProperty("relatedTo");
            var relatedToIdProp = relatedToProp.GetProperty("SomeId");
            var relationProp = relatedToIdProp.GetProperty("relation");
            Assert.Equal(JsonValueKind.Object, relationProp.ValueKind);
            Assert.Equal(JsonValueKind.Undefined, relationProp.EnumerateObject().Current.Value.ValueKind);
        }


        [Fact]
        public async Task GivenAValidTestBuilder_WithRelationsInRelatedTo_RelationsExist()
        {
            var result = await (await GetValidBuilder().WithRelatedTo("SomeId", r => r.WithRelation(RelationType.Parent)).BuildAsync()).GetJsonStreamAsync();
            var options = new JsonDocumentOptions
            {
                AllowTrailingCommas = true
            };
            using var document = await JsonDocument.ParseAsync(result, options);
            var rootElement = document.RootElement;
            var relatedToProp = rootElement.GetProperty("relatedTo");
            var relatedToIdProp = relatedToProp.GetProperty("SomeId");
            var relationProp = relatedToIdProp.GetProperty("relation");
            var relationTypeProp = relationProp.GetProperty("parent");
            Assert.True(relationTypeProp.GetBoolean());
        }

        [Fact]
        public async Task GivenAValidTestBuilder_WithMultipleRelationsInRelatedTo_BothRelationsExist()
        {
            var result = await (await GetValidBuilder().WithRelatedTo("SomeId", r => r.WithRelation(RelationType.Parent).WithRelation(RelationType.Child)).BuildAsync()).GetJsonStreamAsync();
            var options = new JsonDocumentOptions
            {
                AllowTrailingCommas = true
            };
            using var document = await JsonDocument.ParseAsync(result, options);
            var rootElement = document.RootElement;
            var relatedToProp = rootElement.GetProperty("relatedTo");
            var relatedToIdProp = relatedToProp.GetProperty("SomeId");
            var relationProp = relatedToIdProp.GetProperty("relation");
            var subProps = relationProp.EnumerateObject();
            Assert.Equal(2, subProps.Count());
        }

        [Fact]
        public async Task GivenValidTestBuilder_WithDuplicateRelatedTo_ValidationExceptionIsThrown()
        {
            await Assert.ThrowsAsync<ValidationException>(
                async () => await GetValidBuilder()
                .WithRelatedTo("SomeId", r => r.WithRelation(RelationType.Parent))
                .WithRelatedTo("SomeId", r => r.WithRelation(RelationType.Child))
                .BuildAsync());
        }

        [Fact]
        public async Task GivenValidTestBuilder_WithDuplicateRelationsInRelatedTo_ValidationExceptionIsThrown()
        {
            await Assert.ThrowsAsync<ValidationException>(
                async () => await GetValidBuilder()
                .WithRelatedTo("SomeId", r => r.WithRelation(RelationType.Parent).WithRelation(RelationType.Parent))
                .BuildAsync());
        }

        #endregion

        #region prodId

        [Fact]
        public async Task GivenAValidTestBuilder_WithOptionalProdId_HasProdIdInResult()
        {
            var result = await (await GetValidBuilder().WithProdId("A-GUID").BuildAsync()).GetJsonStreamAsync();
            var options = new JsonDocumentOptions
            {
                AllowTrailingCommas = true
            };
            using var document = await JsonDocument.ParseAsync(result, options);
            var rootElement = document.RootElement;
            var prop = rootElement.GetProperty("prodId");
            Assert.Equal("A-GUID", prop.GetString());
        }

        [Fact]
        public async Task GivenAValidTestBuilder_WithoutOptionalProdId_NoProdIdInResult()
        {
            var result = await (await GetValidBuilder().BuildAsync()).GetJsonStreamAsync();
            var options = new JsonDocumentOptions
            {
                AllowTrailingCommas = true
            };
            using var document = await JsonDocument.ParseAsync(result, options);
            var rootElement = document.RootElement;
            Assert.Throws<KeyNotFoundException>(() => rootElement.GetProperty("prodId"));
        }
        #endregion

        #region sequence
        [Fact]
        public async Task GivenAnTestBuilderWithoutSequence_HasSequenceInResultWithDefaultZero()
        {
            var result = await (await GetValidBuilder().BuildAsync()).GetJsonStreamAsync();
            var options = new JsonDocumentOptions
            {
                AllowTrailingCommas = true
            };
            using var document = await JsonDocument.ParseAsync(result, options);
            var rootElement = document.RootElement;
            var prop = rootElement.GetProperty("sequence");
            Assert.Equal<uint>(0, prop.GetUInt32());
        }

        [Fact]
        public async Task GivenAnTestBuilderWithSequence_HasSequenceInResulto()
        {
            var result = await (await GetValidBuilder().WithSequence(2).BuildAsync()).GetJsonStreamAsync();
            var options = new JsonDocumentOptions
            {
                AllowTrailingCommas = true
            };
            using var document = await JsonDocument.ParseAsync(result, options);
            var rootElement = document.RootElement;
            var prop = rootElement.GetProperty("sequence");
            Assert.Equal<uint>(2, prop.GetUInt32());
        }


        #endregion

        #region method

        [Fact]
        public async Task GivenAValidTestBuilder_WithOptionalMethod_HasMethodInResult()
        {
            var result = await (await GetValidBuilder().WithMethod(MethodType.DeclineCounter).BuildAsync()).GetJsonStreamAsync();
            var options = new JsonDocumentOptions
            {
                AllowTrailingCommas = true
            };
            using var document = await JsonDocument.ParseAsync(result, options);
            var rootElement = document.RootElement;
            var prop = rootElement.GetProperty("method");
            Assert.Equal("declinecounter", prop.GetString());
        }

        [Fact]
        public async Task GivenAValidTestBuilder_WithoutOptionalMethod_NoMethodInResult()
        {
            var result = await (await GetValidBuilder().BuildAsync()).GetJsonStreamAsync();
            var options = new JsonDocumentOptions
            {
                AllowTrailingCommas = true
            };
            using var document = await JsonDocument.ParseAsync(result, options);
            var rootElement = document.RootElement;
            Assert.Throws<KeyNotFoundException>(() => rootElement.GetProperty("method"));
        }
        #endregion

        #region title

        [Fact]
        public async Task GivenAValidTestBuilder_WithTitle_HasTitleInResult()
        {
            var result = await (await GetValidBuilder().WithTitle("TestTitle").BuildAsync()).GetJsonStreamAsync();
            var options = new JsonDocumentOptions
            {
                AllowTrailingCommas = true
            };
            using var document = await JsonDocument.ParseAsync(result, options);
            var rootElement = document.RootElement;
            var prop = rootElement.GetProperty("title");
            Assert.Equal("TestTitle", prop.GetString());
        }

        [Fact]
        public async Task GivenAValidTestBuilder_WithoutOptionalProdId_EmptyTitleInResult()
        {
            var result = await (await GetValidBuilder().BuildAsync()).GetJsonStreamAsync();
            var options = new JsonDocumentOptions
            {
                AllowTrailingCommas = true
            };
            using var document = await JsonDocument.ParseAsync(result, options);
            var rootElement = document.RootElement;
            var prop = rootElement.GetProperty("title");
            Assert.Equal(string.Empty, prop.GetString());
        }
        #endregion

        #region description

        [Fact]
        public async Task GivenAValidTestBuilder_WithDescription_HasDescriptionInResult()
        {
            var result = await (await GetValidBuilder().WithDescription("TestDescription").BuildAsync()).GetJsonStreamAsync();
            var options = new JsonDocumentOptions
            {
                AllowTrailingCommas = true
            };
            using var document = await JsonDocument.ParseAsync(result, options);
            var rootElement = document.RootElement;
            var prop = rootElement.GetProperty("description");
            Assert.Equal("TestDescription", prop.GetString());
        }

        [Fact]
        public async Task GivenAValidTestBuilder_WithoutOptionalProdId_EmptyDescriptionInResult()
        {
            var result = await (await GetValidBuilder().BuildAsync()).GetJsonStreamAsync();
            var options = new JsonDocumentOptions
            {
                AllowTrailingCommas = true
            };
            using var document = await JsonDocument.ParseAsync(result, options);
            var rootElement = document.RootElement;
            var prop = rootElement.GetProperty("description");
            Assert.Equal(string.Empty, prop.GetString());
        }
        #endregion
    }
}
