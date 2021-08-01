using FluentValidation;
using Lib.Models;
using Lib.Models.DataTypes;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;

namespace Lib.Builders
{
    public class RelationBuilder
    {
        private Relation Relation { get; set; }

        public RelationBuilder()
        {
            Relation = new Relation();
        }

        public RelationBuilder WithRelation(RelationType relationType)
        {
            var result = Relation.relations.TryAdd(relationType, true);
            if (!result)
            {
                throw new ValidationException($"Relation must be unique in a RelatedTo. Duplicate value: {relationType}");
            }
            return this;
        }

        public Relation Build()
        {
            return Relation;
        }

    }
}
