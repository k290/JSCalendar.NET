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
            return this as B;
        }

        public B WithRelatedTo(string id, Action<RelationBuilder> relationBuilderAction)
        {
            var relationBuilder = new RelationBuilder();
            relationBuilderAction(relationBuilder);
            if(_jsCalendarObject.relatedTo == null)
            {
                _jsCalendarObject.relatedTo = new Dictionary<string, Relation>();
            }
            _jsCalendarObject.relatedTo.Add(id, relationBuilder.Build()); //todo use tryadd method instead??
            return this as B;
        }
    }
}