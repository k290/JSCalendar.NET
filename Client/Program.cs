using Lib.Builders;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Client
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var builtObject =  await new JSGroupBuilder()
                                 .WithUid("Group01")
                                 .WithRelatedTo("SomeId", r=>r.WithRelation("parent").WithRelation("child"))
                                 .WithRelatedTo("SomeOtherId", r=>r.WithRelation("sibling"))
                                 .WithEvent(e => e.WithUid("E1"))
                                 .WithEvent(e => e.WithUid("E2"))
                                 .WithTask(t => t.WithUid("T1"))
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
