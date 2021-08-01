using Lib.Builders;
using Lib.Models.DataTypes;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Client
{
    class Program
    {
        static async Task Main()
        {
            var builtObject = await new JSGroupBuilder()
                                 .WithUid("group01")
                                 .WithRelatedTo("someId", r => r.WithRelation(RelationType.Parent).WithRelation(RelationType.Child))
                                 .WithRelatedTo("someOtherId", r => r.WithRelation(RelationType.Next))
                                 .WithEvent(e => e.WithUid("e1"))
                                 .WithEvent(e => e.WithUid("e2"))
                                 .WithTask(t => t.WithUid("t1"))
                          .BuildAsync();

            var result = await builtObject.GetJsonAsync();
            Console.WriteLine(result);


            //var builtObjectBad = new JSGroupBuilder()
            //                              .WithUid("Group01")
            //                              .WithEvent(e => { })
            //                              .WithEvent(e => e.WithUid("E2"))
            //                              .WithTask(t => t.WithUid("T1"))
            //                       .Build();

            //var resultBad = builtObject.GetJson();


            Console.ReadKey();
        }
    }
}
