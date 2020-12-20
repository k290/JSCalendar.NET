using Lib.Builders;
using System;
using System.Collections.Generic;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            var builtObject = new JSGroupBuilder()
                                 .WithUid("Group01")
                                 .WithRelatedTo("SomeId", r=>r.WithRelation("parent").WithRelation("child"))
                                 .WithRelatedTo("SomeOtherId", r=>r.WithRelation("sibling"))
                                 .WithEvent(e => e.WithUid("E1"))
                                 .WithEvent(e => e.WithUid("E2"))
                                 .WithTask(t => t.WithUid("T1"))
                          .Build();

            var result = builtObject.GetJson();
            Console.WriteLine(result);


            var builtObjectBad = new JSGroupBuilder()
                                          .WithUid("Group01")
                                          .WithEvent(e => { })
                                          .WithEvent(e => e.WithUid("E2"))
                                          .WithTask(t => t.WithUid("T1"))
                                   .Build();

            var resultBad = builtObject.GetJson();


            Console.ReadKey();
        }
    }
}
