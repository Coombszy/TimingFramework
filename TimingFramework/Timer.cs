using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Reflection;

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
            LoadingBar Bar = new LoadingBar(NumOfTests,0);
            Console.WriteLine("Running Tests!");
            for (int i = 0; i < NumOfTests; i++)
            {
                Bar.Draw(i+1);
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
                    Console.WriteLine("Tests Failed!");
                    i = NumOfTests + 1;
                }
            }
            Console.WriteLine("Tests Complete!");
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
        public void CreateCSV()
        {
            string NameAndLoc = "data.csv";
            if (TestData == null || TestData.Count == 0)
            {
                Console.WriteLine("Failed to Write to a CSV!");
                return; 
            }
            string newLine = Environment.NewLine;

            using (var sw = new StreamWriter(NameAndLoc))
            {
                foreach (double item in TestData)
                {
                    sw.Write(item);
                    sw.Write(newLine);
                }
            }
            Console.WriteLine("Successfully Wrote to a CSV!");
        }
    }
}
