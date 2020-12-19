using Builder;
using System;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            var result = new JSCalendarBuilder().Event().Build();
            Console.WriteLine(result);
            Console.ReadKey();
        }
    }
}
