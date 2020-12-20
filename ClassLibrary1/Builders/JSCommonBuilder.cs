using Lib.Models;
using Lib.Models.DataTypes;
using System;
using System.Collections.Generic;

namespace Lib.Builders
{
    public abstract class JSCommonBuilder<E, B> where B: JSCommonBuilder<E, B> where E : JSCommon
    {
        protected abstract E _jsCalendarObject { get; set;  }
        public B WithUid(string uid)
        {
            _jsCalendarObject.uid = uid;
            return (B)this;
        }

        public B WithRelatedTo(string id, Action<RelationBuilder> relationBuilderAction)
        {
            var relationBuilder = new RelationBuilder();
            relationBuilderAction(relationBuilder);
            if(_jsCalendarObject.relatedTos == null)
            {
                _jsCalendarObject.relatedTos = new Dictionary<string, Relation>();
            }
            _jsCalendarObject.relatedTos.Add(id, relationBuilder.Build()); //todo use tryadd method instead??
            return (B)this;
        }
    }
}