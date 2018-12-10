using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimingFramework
{
    public class Timer
    {
        //Variables!
        public delegate void Method(List<object> data);//Deligates!
        private Method ToCall;//Method to be tested
        private List<object> DataParsed;//Data to be used with the method
        private List<Double> TestData = new List<Double>();
        private DateTime TimeStampThen;
        private DateTime TimeStampNow;

        //Accessors!
        public void SetMethod(Method MethodToTime)
        {
            ToCall = MethodToTime;
        }
        public void SetData(List<object> DataToUse)
        {
            DataParsed = DataToUse;
        }

        //Run tests!
        private bool RunMethod()
        {
            if (ToCall != null && DataParsed != null)
            {
                ToCall(DataParsed);
                return true;
            }
            else
            {
                Console.WriteLine("FAILED : Method or Data was not set!");
                return false; 
            }
        }
        public void RunTest()
        {
            Console.WriteLine("Running Test!");
            TimeStampThen = DateTime.Now;
            bool Passed = RunMethod();
            TimeStampNow = DateTime.Now;
            if (Passed)
            {
                Console.WriteLine("Test Complete!");
                var Seconds = Math.Abs((TimeStampNow - TimeStampThen).TotalSeconds);
                TestData.Add(Seconds);
            }
            else
            {
                Console.WriteLine("Test Failed!");
            }
        }
        public void RunMultipleTests(int NumOfTests)
        {
            Console.WriteLine("Running Test!");
            for (int i = 0; i < NumOfTests; i++)
            {
                TimeStampThen = DateTime.Now;
                bool Passed = RunMethod();
                TimeStampNow = DateTime.Now;
                if (Passed)
                {
                    var Seconds = Math.Abs((TimeStampNow - TimeStampThen).TotalSeconds);
                    TestData.Add(Seconds);
                }
                else
                {
                    Console.WriteLine("Test Failed!");
                    i = NumOfTests + 1;
                }
            }
            Console.WriteLine("Test Complete!");
        }

        //Get/Edit test Data!
        public List<double> GetAllTestTimes()
        {
            return TestData;
        }
        public void WipeAllTestTimes()
        {
            TestData = new List<double>();
        }
        private void AddTestData(int TimeInSeconds)
        {
            TestData.Add(TimeInSeconds);
        }
    }
}
