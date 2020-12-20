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
        private Relation relation { get; set; }

        public RelationBuilder()
        {
            relation = new Relation();
        }

        public RelationBuilder WithRelation(string relationType)
        {
            relation.relation.Add(relationType, true);
            return this;
        }

        public Relation Build()
        {
            return relation;
        }

    }
}
