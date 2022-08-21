using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using static System.Net.Mime.MediaTypeNames;

namespace StringPerformanceTest
{
    [MemoryDiagnoser]
    public class Benchmark
    {
        string[] Lines;

        public int NumberOfLines;

        [Params("Bacon", "pork", "prosciutto")]
        public string SearchValue;

        [Params("Files/Bacon10.txt", "Files/Bacon25.txt", "Files/Bacon50.txt")]
        public string FileToRead;


        [GlobalSetup]
        public void GlobalSetup()
        {
            string fileLocation = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), FileToRead);
            Lines = File.ReadAllLines(fileLocation);
            NumberOfLines = Lines.Count();
        }

        //Winning Method
        [Benchmark]
        public int CountOccurrences()
        {
            int count = 0;
            GlobalSetup();

            // Loops through each line if the textfile
            foreach (string line in Lines)
            {
                //Increments count by one for every Regex match on each line.
                count += Regex.Matches(line, SearchValue, RegexOptions.IgnoreCase).Count;
            }
            return count;
        }


        //[Benchmark]
        //public int CountOccurrences()
        //{
        //    int count = 0;
        //    GlobalSetup();

        //    for (int i = 0; i < NumberOfLines; i++)
        //    {
        //        int a = 0;
        //        while ((a = Lines[i].ToLower().IndexOf(SearchValue.ToLower(), a)) != -1)
        //        {
        //            a += SearchValue.Length;
        //            count++;
        //        }
        //    }
        //    return count;
        //}

        //[Benchmark]
        //public int CountOccurrences2()
        //{
        //    int count = 0;
        //    GlobalSetup();

        //    Parallel.ForEach(Lines, line =>
        //    {
        //        int a = 0;
        //        while ((a = line.ToLower().IndexOf(SearchValue.ToLower(), a)) != -1)
        //        {
        //            a += SearchValue.Length;
        //            count++;
        //        }
        //    });
        //    return count;
        //}

        //[Benchmark]
        //public int CountOccurrences3()
        //{
        //    int count = 0;
        //    GlobalSetup();

        //    foreach (string line in Lines)
        //    {
        //        count += Regex.Matches(line, SearchValue, RegexOptions.IgnoreCase).Count;
        //    }
        //    return count;
        //}

        //[Benchmark]
        //public int CountOccurrences4()
        //{
        //    int count = 0;

        //    var obj = new Object();
        //    GlobalSetup();

        //    Parallel.ForEach(Lines, line =>
        //    {
        //        lock (obj)
        //        count += Regex.Matches(line, SearchValue, RegexOptions.IgnoreCase).Count;
        //    });
        //    return count;
        //}


        //[Benchmark]
        //public int CountOccurrences5()
        //{
        //    int count = 0;
        //    GlobalSetup();

        //    foreach(string line in Lines)
        //    {
        //        int a = 0;
        //        while ((a = line.ToLower().IndexOf(SearchValue.ToLower(), a)) != -1)
        //        {
        //            a += SearchValue.Length;
        //            count++;
        //        }
        //    }
        //    return count;
        //}

        //[Benchmark]
        //public int CountOccurrences6()
        //{
        //    int count = 0;
        //    GlobalSetup();

        //    Parallel.For(0, NumberOfLines, i =>
        //    {
        //        int a = 0;
        //        while ((a = Lines[i].ToLower().IndexOf(SearchValue.ToLower(), a)) != -1)
        //        {
        //            a += SearchValue.Length;
        //            count++;
        //        }
        //    });
        //    return count;
        //}

        //[Benchmark]
        //public int CountOccurrences7()
        //{
        //    int count = 0;
        //    var obj = new Object();
        //    GlobalSetup();

        //    Parallel.For(0, NumberOfLines, i =>
        //    {
        //        lock (obj)
        //        count += Regex.Matches(Lines[i], SearchValue, RegexOptions.IgnoreCase).Count;
        //    });
        //    return count;
        //}

        //[Benchmark]
        //public int CountOccurrences8()
        //{
        //    int count = 0;
        //    GlobalSetup();

        //    for (int i = 0; i < NumberOfLines; i++)
        //    {
        //        count += Regex.Matches(Lines[i], SearchValue, RegexOptions.IgnoreCase).Count;
        //    }
        //    return count;
        //}
    }





    public class Program
    {
        public static void Main(string[] args)
        {
            //int numOfOccurences = 0;
            //Benchmark benchmark = new Benchmark();
            //numOfOccurences = benchmark.CountOccurrences8();
            //Console.WriteLine("Number of occurences of " + benchmark.SearchValue + " is: " + numOfOccurences);
            BenchmarkRunner.Run<Benchmark>();
        }
    }
}
