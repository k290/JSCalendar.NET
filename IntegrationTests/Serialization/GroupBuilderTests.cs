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
        private JSGroupBuilder GetValidBuilder()
        {
            return new JSGroupBuilder().WithUid("Valid").WithUpdateDate(new DateTime(2021, 02, 01, 11, 20, 5, 100, DateTimeKind.Utc));
        }

        private void SetValidEventBuilder(JSEventBuilder eventBuilder, string Uid)
        {
            eventBuilder.WithUid(Uid).WithUpdateDate(new DateTime(2021, 02, 01, 11, 20, 5, 100, DateTimeKind.Utc));
        }

        private void SetValidTaskBuilder(JSTaskBuilder taskBuilder)
        {
            taskBuilder.WithUid("Valid").WithUpdateDate(new DateTime(2021, 02, 01, 11, 20, 5, 100, DateTimeKind.Utc));
        }

        #region uid
        [Fact]
        public async Task GivenAGroupBuilderWithNoUID_ItThrowsValidationException()
        {

            await Assert.ThrowsAsync<ValidationException>(async () => await new JSGroupBuilder().BuildAsync());
        }

        [Fact]
        public async Task GivenAGroupBuilderWithBlankUID_ItThrowsValidationException()
        {

            await Assert.ThrowsAsync<ValidationException>(async () => await new JSGroupBuilder().WithUid("").BuildAsync());
        }

        [Fact]
        public async Task GivenAValidGroupBuilder_HasUidInResult()
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
        public async Task GivenAnGroupBuilderWithNoUpdateDate_ItThrowsValidationException()
        {

            await Assert.ThrowsAsync<ValidationException>(async () => await new JSGroupBuilder().WithUid("Invalid").BuildAsync());
        }

        [Fact]
        public async Task GivenAnGroupBuilderWithNonUtcUpdateDate_ItThrowsValidationException()
        {

            await Assert.ThrowsAsync<ValidationException>(async () => await new JSGroupBuilder().WithUid("Invalid").WithUpdateDate(new DateTime(2021, 02, 01, 11, 20, 5, 0)).BuildAsync());
        }

        [Fact]
        public async Task GivenAnGroupBuilderWithUpdateDate_HasUpdateDateInResultWithoutTrailingZerosInFraction()
        {
            var result = await (await new JSGroupBuilder().WithUid("Valid").WithUpdateDate(new DateTime(2021, 02, 01, 11, 20, 5, 100, DateTimeKind.Utc)).BuildAsync()).GetJsonStreamAsync();
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
        public async Task GivenAnGroupBuilderWithUpdateDateZeroMillis_HasNoMillisInResult()
        {
            var result = await (await new JSGroupBuilder().WithUid("Valid").WithUpdateDate(new DateTime(2021, 02, 01, 11, 20, 5, 0, DateTimeKind.Utc)).BuildAsync()).GetJsonStreamAsync();
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
        public async Task GivenAnGroupBuilderWithNonUtcCreateDate_ItThrowsValidationException()
        {

            await Assert.ThrowsAsync<ValidationException>(async () => await new JSGroupBuilder().WithUid("Invalid").WithUpdateDate(new DateTime(2021, 02, 01, 11, 20, 5, 0, DateTimeKind.Utc)).WithCreateDate(new DateTime(2021, 02, 01, 11, 20, 5, 0)).BuildAsync());
        }

        [Fact]
        public async Task GivenAValidGroupBuilderWithCreateDate_HasCreateDateInResultWithoutTrailingZerosInFraction()
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
        public async Task GivenAnGroupBuilderWithCreateDateZeroMillis_HasNoMillisInResult()
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
        public async Task GivenAValidGroupBuilderWithoutOptionalCreated_CreatedNotInResult()
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

        //todo create date not supplied

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
        public async Task GivenAValidGroupBuilderWith1Entries_HasPopulatedArraySize1()
        {
            var result = await (await GetValidBuilder().WithEvent(e => SetValidEventBuilder(e, "Event1")).BuildAsync()).GetJsonStreamAsync();
            var options = new JsonDocumentOptions
            {
                AllowTrailingCommas = true
            };
            using var document = await JsonDocument.ParseAsync(result, options);
            var rootElement = document.RootElement;
            var prop = rootElement.GetProperty("entries");
            Assert.Equal(1, prop.GetArrayLength());
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
            var prop = rootElement.GetProperty("entries");
            Assert.Equal(2, prop.GetArrayLength());
        }

        [Fact]
        public async Task GivenAValidGroupBuilderWithTwoOfDifferentEntries_HasPopulatedArraySize2()
        {
            var result = await (await GetValidBuilder().WithEvent(e => SetValidEventBuilder(e, "Event1")).WithTask(t => SetValidTaskBuilder(t)).BuildAsync()).GetJsonStreamAsync();
            var options = new JsonDocumentOptions
            {
                AllowTrailingCommas = true
            };
            using var document = await JsonDocument.ParseAsync(result, options);
            var rootElement = document.RootElement;
            var prop = rootElement.GetProperty("entries");
            Assert.Equal(2, prop.GetArrayLength());
        }

        #endregion

        #region relatedTo

        [Fact]
        public async Task GivenAValidGroupBuilderWithoutOptionalRelatedTo_RelatedToNotInResult()
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
        public async Task GivenAValidGroupBuilder_WithValidOptionalRelatedTo_RelatedToInResult()
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
        public async Task GivenAValidGroupBuilder_WithMultipleOptionalRelatedTo_BothRelatedToInResult()
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
        public async Task GivenAValidGroupBuilder_WithValidOptionalRelatedTo_RelatedToHasType()
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
        public async Task GivenAValidGroupBuilder_WithEmptyRelationsInRelatedTo_EmptyObjectInResult()
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
        public async Task GivenAValidGroupBuilder_WithRelationsInRelatedTo_RelationsExist()
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
        public async Task GivenAValidGroupBuilder_WithMultipleRelationsInRelatedTo_BothRelationsExist()
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
        public async Task GivenValidGroupBuilder_WithDuplicateRelatedTo_ValidationExceptionIsThrown()
        {
            await Assert.ThrowsAsync<ValidationException>(
                async () => await GetValidBuilder()
                .WithRelatedTo("SomeId", r => r.WithRelation(RelationType.Parent))
                .WithRelatedTo("SomeId", r => r.WithRelation(RelationType.Child))
                .BuildAsync());
        }

        [Fact]
        public async Task GivenValidGroupBuilder_WithDuplicateRelationsInRelatedTo_ValidationExceptionIsThrown()
        {
            await Assert.ThrowsAsync<ValidationException>(
                async () => await GetValidBuilder()
                .WithRelatedTo("SomeId", r => r.WithRelation(RelationType.Parent).WithRelation(RelationType.Parent))
                .BuildAsync());
        }
        #endregion

        #region prodId

        [Fact]
        public async Task GivenAValidGroupBuilder_WithOptionalProdId_HasProdIdInResult()
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
        public async Task GivenAValidGroupBuilder_WithoutOptionalProdId_NoProdIdInResult()
        {
            var result = await (await GetValidBuilder().WithUid("Valid").BuildAsync()).GetJsonStreamAsync();
            var options = new JsonDocumentOptions
            {
                AllowTrailingCommas = true
            };
            using var document = await JsonDocument.ParseAsync(result, options);
            var rootElement = document.RootElement;
            Assert.Throws<KeyNotFoundException>(() => rootElement.GetProperty("prodId"));
        }
        #endregion


    }
}
