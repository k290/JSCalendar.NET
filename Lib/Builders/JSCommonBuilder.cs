using FluentValidation;
using Lib.Models;
using Lib.Models.DataTypes;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lib.Builders
{
    public abstract class JSCommonBuilder<E, B, V> where B: JSCommonBuilder<E, B, V> where E : JSCommon where V: AbstractValidator<E>, new()
    {
        protected abstract E JsCalendarObject { get; set;  }
        public B WithUid(string uid)
        {
            JsCalendarObject.Uid = uid;
            return (B)this;
        }

        public B WithRelatedTo(string id, Action<RelationBuilder> relationBuilderAction)
        {
            var relationBuilder = new RelationBuilder();
            relationBuilderAction(relationBuilder);
            if(JsCalendarObject.RelatedTos == null)
            {
                JsCalendarObject.RelatedTos = new Dictionary<string, Relation>();
            }
            var result = JsCalendarObject.RelatedTos.TryAdd(id, relationBuilder.Build());
            if (!result)
            {
                throw new ValidationException($"Related to must have unique ids: {id}");
            }
            return (B)this;
        }

        public B WithProdId(string prodId)
        {
            JsCalendarObject.ProdId = prodId;
            return (B)this;
        }

        public B WithUpdateDate(DateTime updateDate)
        {
            JsCalendarObject.Updated = updateDate;
            return (B)this;
        }

        public B WithCreateDate(DateTime createDate)
        {
            JsCalendarObject.Created = createDate;
            return (B)this;
        }

        public B WithSequence(uint sequence)
        {
            JsCalendarObject.Sequence = sequence;
            return (B)this;
        }

        public B WithMethod(MethodType method)
        {
            JsCalendarObject.Method = method;
            return (B)this;
        }

        public B WithTitle(string title)
        {
            JsCalendarObject.Title = title;
            return (B)this;
        }
        public E Build()
        {
            var validator = new V();

            validator.ValidateAndThrow(JsCalendarObject);

            return JsCalendarObject;
        }

        public async Task<E> BuildAsync()
        {
            var validator = new V();

            await validator.ValidateAndThrowAsync(JsCalendarObject);

            return JsCalendarObject;
        }


    }
}