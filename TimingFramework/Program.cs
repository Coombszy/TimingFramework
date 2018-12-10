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
            TestTimer.RunMultipleTests(10);
            Console.WriteLine(TestTimer.GetAllTestTimes()[0]+"");
        }
        public static void ToParse(List<object> data)
        {
            for(int i = 0; i < 909000000; i++)
            {
                float x = (i * 3) / 9;
            }
        }
    }
}
