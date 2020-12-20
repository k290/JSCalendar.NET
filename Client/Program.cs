using Lib.Builders;
using System;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            var builtObject = new JSGroupBuilder()
                                 .WithUid("Group01")
                                 .WithEvent(e => e.WithUid("E1"))
                                 .WithEvent(e => e.WithUid("E2"))
                                 .WithTask(t => t.WithUid("T1"))
                          .Build();

            var result = builtObject.GetJson();


            //var jsCalObjectBad = new JSCalendarBuilder()
            //        .SetParentAsGroup(g => g
            //               .WithUid("Group01")
            //               .WithEvent(e => { })
            //               .WithEvent(e => e.WithUid("E2"))
            //               .WithTask(t => t.WithUid("T1"))
            //             )
            //        .Build();

            //var resultBad = jsCalObject.GetSerializedResult();
            Console.WriteLine(result);
            Console.ReadKey();
        }
    }
}
