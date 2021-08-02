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


    }
}