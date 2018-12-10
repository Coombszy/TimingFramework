using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimingFramework
{
    class Program
    {

        static void Main(string[] args)
        {
            Console.WriteLine("System Loaded");
            Timer TestTimer = new Timer();
            List<object> Data = new List<object>() { "TEST" };
            TestTimer.SetData(Data);
            TestTimer.SetMethod(ToParse);
            TestTimer.DEBUG_run();
        }
        public static void ToParse(List<object> data)
        {
            if (data.Contains("TEST"))
            {
                Console.WriteLine("Matched!");
            }
        }
    }
}
