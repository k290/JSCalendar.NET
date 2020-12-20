using FluentValidation;
using Lib.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
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
                Assert.Throws<KeyNotFoundException>(() => rootElement.GetProperty("source"));
            }
        }

        [Fact]
        public void GivenAValidGroupBuilderWithOptionalSource_SourceInResult()
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
            var result = new JSGroupBuilder().WithUid("Valid").WithEvent(e => e.WithUid("Event1")).Build().GetJson();
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
            var result = new JSGroupBuilder().WithUid("Valid").WithEvent(e => e.WithUid("Event1")).WithEvent(e => e.WithUid("Event2")).Build().GetJson();
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
            var result = new JSGroupBuilder().WithUid("Valid").WithEvent(e => e.WithUid("Event1")).WithTask(t => t.WithUid("Task1")).Build().GetJson();
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
        public void GivenAValidGroupBuilderWithoutOptionalRelatedTo_RelatedToNotInResult()
        {
            var result = new JSGroupBuilder().WithUid("Valid").Build().GetJson();
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
        public void GivenAValidGroupBuilder_WithValidOptionalRelatedTo_RelatedToInResult()
        {
            var result = new JSGroupBuilder().WithUid("Valid").WithRelatedTo("SomeId", r => { }).Build().GetJson();
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
        public void GivenAValidGroupBuilder_WithMultipleOptionalRelatedTo_BothRelatedToInResult()
        {
            var result = new JSGroupBuilder().WithUid("Valid").WithRelatedTo("SomeId", r => { }).WithRelatedTo("SomeId2", r=> { }).Build().GetJson();
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
        public void GivenAValidGroupBuilder_WithValidOptionalRelatedTo_RelatedToHasType()
        {
            var result = new JSGroupBuilder().WithUid("Valid").WithRelatedTo("SomeId", r => { }).Build().GetJson();
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
        public void GivenAValidGroupBuilder_WithEmptyRelationsInRelatedTo_EmptyObjectInResult()
        {
            var result = new JSGroupBuilder().WithUid("Valid").WithRelatedTo("SomeId", r => { }).Build().GetJson();
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
        public void GivenAValidGroupBuilder_WithRelationsInRelatedTo_RelationsExist()
        {
            var result = new JSGroupBuilder().WithUid("Valid").WithRelatedTo("SomeId", r =>r.WithRelation("parent")).Build().GetJson();
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
        public void GivenAValidGroupBuilder_WithMultipleRelationsInRelatedTo_BothRelationsExist()
        {
            var result = new JSGroupBuilder().WithUid("Valid").WithRelatedTo("SomeId", r => r.WithRelation("parent").WithRelation("child")).Build().GetJson();
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
