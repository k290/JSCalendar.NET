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
                                 .WithSequence(1)
                                 .WithTitle("mytitle")
                                 .WithDescription("mydesc")
                                 .WithDescriptionContentType("html", new Dictionary<string, string> { { "charset", "utf-8" }, { "otherparam", "thing" } })
                                 .WithMethod(MethodType.Publish)
                                 .WithUpdateDate(new DateTime(2021, 02, 01, 11, 20, 5, 0,  DateTimeKind.Utc))
                                 .WithCreateDate(new DateTime(2021, 01, 01, 11, 20, 5, 120, DateTimeKind.Utc))
                                 .WithRelatedTo("someId", r => r.WithRelation(RelationType.Parent).WithRelation(RelationType.Child))
                                 .WithRelatedTo("someOtherId", r => r.WithRelation(RelationType.Next))
                                 .WithEvent(e => e.WithUid("e1").WithUpdateDate(new DateTime(2021, 02, 01, 11, 20, 5, 0, DateTimeKind.Utc)))
                                 .WithEvent(e => e.WithUid("e2").WithUpdateDate(new DateTime(2021, 02, 01, 11, 20, 5, 700, DateTimeKind.Utc)))
                                 .WithTask(t => t.WithUid("t1").WithUpdateDate(new DateTime(2021, 02, 01, 11, 20, 5, 77, DateTimeKind.Utc)))
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
