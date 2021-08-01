using FluentValidation;
using Lib.Models;
using Lib.Models.DataTypes;
using System;
using System.Collections.Generic;

namespace Lib.Builders
{
    public abstract class JSCommonBuilder<E, B> where B: JSCommonBuilder<E, B> where E : JSCommon
    {
        protected abstract E JsCalendarObject { get; set;  }
        public B WithUid(string uid)
        {
            JsCalendarObject.uid = uid;
            return (B)this;
        }

        public B WithRelatedTo(string id, Action<RelationBuilder> relationBuilderAction)
        {
            var relationBuilder = new RelationBuilder();
            relationBuilderAction(relationBuilder);
            if(JsCalendarObject.relatedTos == null)
            {
                JsCalendarObject.relatedTos = new Dictionary<string, Relation>();
            }
            var result = JsCalendarObject.relatedTos.TryAdd(id, relationBuilder.Build());
            if (!result)
            {
                throw new ValidationException($"Related to must have unique ids: {id}");
            }
            return (B)this;
        }

        public B WithProdId(string prodId)
        {
            JsCalendarObject.prodId = prodId;
            return (B)this;
        }
    }
}