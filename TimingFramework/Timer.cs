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
        private List<int> TestData = new List<int>();

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

        //Get/Edit test Data!
        public List<int> AllTestTimes()
        {
            return TestData;
        }
        public void WipeAllTestTimes()
        {
            TestData = new List<int>();
        }
        private void AddTestData(int TimeInSeconds)
        {
            TestData.Add(TimeInSeconds);
        }
    }
}
