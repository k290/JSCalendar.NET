using Lib.Builders;
using System;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            var jsCalObject = new JSCalendarBuilder()
                          .SetParentAsGroup(g => g
                                 .WithEvent(e=>e.WithStart())
                                 .WithEvent(e=>e.WithStart())
                                 //.WithTask(t=>true) Need to figure out Task with nothing
                               )
                          .Build();

            var result = jsCalObject.GetSerializedResult();
            Console.WriteLine(result);
            Console.ReadKey();
        }
    }
}
