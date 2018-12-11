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
            /*
             * Example on how to use the Timer System!
             * 
             * 
            Timer TestTimer = new Timer();
            List<object> Data = new List<object>() { "TEST" };
            TestTimer.SetData(Data);
            TestTimer.SetMethod(ExampleMethodToTest);
            TestTimer.RunMultipleTests(10);
            TestTimer.CreateCSV();*/

            //--Test Data definition--
            Timer tmr = new Timer();
            //List<object> data = CreateTestDataSTRING(100000, 4);
            //List<object> data = CreateTestDataINT(10000000);
            //List<object> data = CreateTestDataBINARY(1000);
            List<object> data = new List<object> { 10, 3, 1, 7 };


            //--Setting Test data--
            tmr.SetData(data);
            tmr.SetMethod(SubSequence);//<--- set method to test in here

            //--Test Types--
            tmr.RunMultipleTests(25);
            //tmr.RunTest();

            //--Exporting Data from the tests--
            Console.WriteLine("Average time: " + tmr.GetAverageTime() + " Seconds");
            //tmr.WriteAllData();
            tmr.WriteAlgorithmData();
            //tmr.CreateCSV();


        }
        public static List<object> CreateTestDataINT(int ListSize)
        {
            List<object> Data = new List<object>();
            Random Rand = new Random();
            for (int i = 0; i < ListSize; i++)
            {
                Data.Add(Rand.Next(0, 100));
            }
            return Data;
        }
        public static List<object> CreateTestDataSTRING(int ListSize, int StringSize)
        {
            Random rnd = new Random();
            List<object> NewStrings = new List<object>();
            string tmp;
            for (int i = 0; i < ListSize; i++)
            {
                tmp = "";
                for(int j = 0; j < StringSize; j++)
                {
                    tmp += (char)rnd.Next(65, 91);
                }
                NewStrings.Add(tmp);
            }
            return NewStrings;
        }
        public static List<object> CreateTestDataBINARY(int ListSize)
        {
            List<object> data = new List<object>();
            Random rnd = new Random();
            for(int i = 0; i < ListSize; i++)
            {
                data.Add(rnd.Next(0, 2));
            }
            return data;
        }
        public static void WriteList(List<object> data)
        {
            foreach (object item in data)
            {
                Console.WriteLine(item);
            }
        }

        //Example Methods to Test!
        public static List<object> Example_last(List<object> data)
        { 
            data.Last();
            return data;
        }
        public static List<object> Example_reverse(List<object> data)
        {
            data.Reverse();
            return data;
        }
        public static List<object> Example_sort(List<object> data)
        {
            data.Sort();
            return data;
        }

        //My Algorithms
        public static List<object> Shuffle(List<object> data)
        {
            Random rnd = new Random();
            object temp;
            int targ = 0;
            for (int i = 0; i < data.Count; i++)
            {
                temp = data[i];
                targ = rnd.Next(0, data.Count());
                data[i] = data[targ];
                data[targ] = temp;
            }
            return data;
        }
        public static List<object> Reverse(List<object> data)
        {
            object temp;
            int targ = 0;
            for (int i = 0; i < Math.Floor((decimal)((data.Count-1) / 2)); i++)
            {
                temp = data[i];
                targ = data.Count-(i+1);
                data[i] = data[targ];
                data[targ] = temp;
            }
            return data;
        }
        public static List<object> MechaPairing(List<object> data)
        {
            List<object> AllCombos = new List<object>();
            Queue<object> Lock = new Queue<object>(data);
            Queue<object> Rotator = new Queue<object>(data);
            for (int i = 0; i < data.Count - 1; i++)
            {
                Rotator.Enqueue(Rotator.Dequeue());
                for (int j = 0; j < data.Count; j++)
                {
                    AllCombos.Add(Lock.ElementAt(j) + " - " + Rotator.ElementAt(j));
                }
            }
            return AllCombos;
        }
        public static List<object> MakeGroups(List<object> data)
        {
            int GroupSize = 4; //This would normally be a parameter - Would require Rewrite of testing framework to introduce an additional parameter
            Random rnd = new Random();
            int target, maxValue = data.Count;
            Object[] Groups = new Object[(int)Math.Floor((decimal)maxValue/GroupSize)];
            for (int i = 0; i < maxValue; i ++)
            {
                target = rnd.Next(0, data.Count);
                Groups[(i % Groups.Length)] += data[target]+" ";
                data.RemoveAt(target);
            }
            return Groups.ToList();
        }
        public static List<object> RemoveDuplicates(List<object> data)
        {
            List<object> NewData = new List<object>();
            foreach(object item in data)
            {
                if(NewData.Contains(item) == false)
                {
                    NewData.Add(item);
                }
            }
            return NewData;
        }
        public static List<object> MostFrequent(List<object> data)
        {
            Dictionary<object, int> Freq = new Dictionary<object, int>();
            foreach(object item in data)
            {
                if (Freq.ContainsKey(item) == false)
                {
                    Freq.Add(item, 1);
                }
                else
                {
                    Freq[item] += 1;
                }
            }
            var Ordered = from entry in Freq orderby entry.Value descending select entry;
            List<object> OrderedList = new List<object>();
            foreach(KeyValuePair<object, int> Pair in Ordered)
            {
                OrderedList.Add(Pair.Key);
            }
            return 
            OrderedList;
        }
        public static List<object> SortBinary(List<object> data)
        {
            List<object> SortedData = new List<object>();
            foreach(object item in data)
            {
                if ((int)item == 0)
                {
                    SortedData.Insert(0, item);
                }
                else
                {
                    SortedData.Add(item);
                }
            }
            return SortedData;
        }
        public static List<object> SubSequence(List<object> data)
        {
            List<object> NumbersToFind = new List<object>() { 8,10,21,22,17};
            List<object> NumbersFound = new List<object>();
            int total = 0;
            foreach(object item in data)
            {
                total += (int)item;
            }
            foreach (object NumberToFind in NumbersToFind)
            {
                for (int i = 0; i < data.Count(); i++)
                {
                    if(data.Contains(NumberToFind))
                    {
                        NumbersFound.Add(NumberToFind);
                    }
                    if(total == (int)NumberToFind)
                    {
                        NumbersFound.Add(NumberToFind);
                    }
                    try
                    {
                        if (((int)data[i] + (int)(data[i + 1])) == (int)NumberToFind)
                        {
                            NumbersFound.Add(NumberToFind);
                        }
                    }
                    catch { }
                    try
                    {
                        if (((int)data[i] + (int)(data[i + 1]) + (int)(data[i - 1])) == (int)NumberToFind)
                        {
                            NumbersFound.Add(NumberToFind);

                        }
                    }
                    catch { }
                }
            }
            return NumbersFound;
        }
    }
}