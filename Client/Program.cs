using Builder;
using System;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            var jsCalObject = new JSCalendarBuilder()
                          .SetParentAsGroup(b => b.WithStart())
                          .Build();

            var result = jsCalObject.GetSerializedResult();
            Console.WriteLine(result);
            Console.ReadKey();
        }
    }
}
