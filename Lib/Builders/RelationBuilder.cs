using FluentValidation;
using Lib.Models;
using Lib.Models.DataTypes;
using System;
using System.Collections.Generic;
using System.Text.Json;

namespace Lib.Builders
{
    public class RelationBuilder
    {
        private Relation _relation { get; set; }

        public RelationBuilder()
        {
            _relation = new Relation();
        }

        public RelationBuilder WithRelation(RelationType relationType)
        {
            _relation.relations.Add(relationType, true);
            return this;
        }

        public Relation Build()
        {
            return _relation;
        }

    }
}
