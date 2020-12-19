using Builder;
using System;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            var jsCalObject = new JSCalendarBuilder()
                          .WithEvent(b => b.WithStart())
                          .Build();

            var result = jsCalObject.ToString();
            Console.WriteLine(result);
            Console.ReadKey();
        }
    }
}
