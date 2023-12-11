using AoC2022_Exo1;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using NuGet.Frameworks;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.WebSockets;
using System.Numerics;
using System.Runtime.Serialization.Formatters;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AoC2023_Exo5
{
    /*
     * 323142463 too low
     * 323142464 too low
    */
    public class Program
    {
        public static Almanach almanach = new Almanach();
        public static void Main()
        {
            string text = Utils.ReadFile("E:\\Projets\\AdventOfCode\\AdventOfCode\\AoC\\Exo2023_05.txt");
            string[] textSplitted = text.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

            // STEP 1
            CreateMaps(textSplitted);
            CreateSeeds(textSplitted[0]);
            Console.WriteLine($"Smallest: {GetSmallestLocation(almanach.seeds, almanach.maps)}");
        }

        private static BigInteger GetSmallestLocation(List<Seed> seeds, List<Map> maps)
        {
            List<BigInteger> locations = new List<BigInteger>();

            foreach (Seed seed in seeds)
            {
                locations.Add(seed.GetLocation(maps));
            }
            locations = locations.Order().ToList();

            return locations[0];
        }

        private static void CreateSeeds(string input)
        {
            string[] splittedInput = input.Split(" ", StringSplitOptions.RemoveEmptyEntries);

            foreach (string s in splittedInput)
            {
                if (char.IsDigit(s.First()))
                {
                    almanach.seeds.Add(new Seed(BigInteger.Parse(s)));
                }
            }
        }

        private static void CreateMaps(string[] input)
        {
            for (Int64 i = 1; i < input.Length; i++)
            {
                if (!char.IsDigit(input[i].First()))
                {
                    Map mapToAdd = new Map();
                    almanach.maps.Add(mapToAdd);

                    Console.WriteLine($"New map!");
                }
                else
                    almanach.maps.Last().AddEntry(input[i]);
            }
        }
    }

    public class Seed
    {
        public BigInteger id = 0;

        public Seed (BigInteger _id)
        {
            id = _id;
            Console.WriteLine($"New Seed: {id}");
        }

        public BigInteger GetLocation(List<Map> maps)
        {
            BigInteger loc = id;

            foreach(Map map in maps)
            {
                loc = map.Convert(loc);
            }

            return loc;
        }
    }

    public class Entry
    {
        public BigInteger source;
        public BigInteger dest;
        public BigInteger range;

        public Entry(BigInteger _source, BigInteger _dest, BigInteger _range)
        {
            source = _source;
            dest = _dest;
            range = _range;
        }
    }

    public class Map
    {
        public List<Entry> sourceDest = new List<Entry>();
        public void AddEntry(string _entry)
        {
            BigInteger range = BigInteger.Parse(_entry.Split(" ", StringSplitOptions.RemoveEmptyEntries)[2]);
            BigInteger source = BigInteger.Parse(_entry.Split(" ", StringSplitOptions.RemoveEmptyEntries)[1]);
            BigInteger dest = BigInteger.Parse(_entry.Split(" ", StringSplitOptions.RemoveEmptyEntries)[0]);
            Entry entry = new Entry(source, dest, range);

            Console.WriteLine($"New entry source: {entry.source} dest: {entry.dest} range: {entry.range}");
            sourceDest.Add(entry);
        }

        public BigInteger Convert(BigInteger sourceInput)
        {
            foreach (Entry entry in sourceDest)
            {
                if (sourceInput >= entry.source && sourceInput <= entry.source + entry.range)
                {
                    return sourceInput - entry.source + entry.dest;
                }
            }
            return sourceInput;
        }
    }
    public class Almanach
    {
        public List<Map> maps = new List<Map>();
        public List<Seed> seeds = new List<Seed>();
    }

    [TestClass]
    public class Tests
    {
        [TestMethod]
        public void Test()
        {
        }
    }
}
