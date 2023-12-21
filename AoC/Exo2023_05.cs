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
     * 
     * 595208875 too high
    */
    public class Program
    {
        public static Almanach almanach = new Almanach();
        public static void Main()
        {
            string text = Utils.ReadFile("C:\\Prototypes_Perso\\AdventOfCode\\AoC\\Exo2023_05.txt");
            string[] textSplitted = text.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

            // STEP 1
            //CreateMaps(textSplitted);
            //CreateSeeds(textSplitted[0]);
            //Console.WriteLine($"Smallest: {GetSmallestLocation(almanach.seeds, almanach.maps)}");

            // STEP 2
            CreateMaps(textSplitted);
            CreateRangedSeeds(textSplitted[0]);
            //Console.WriteLine($"Smallest: {almanach.seeds[0].GetLocation(almanach.maps)}");
            Console.WriteLine($"Smallest: {GetSmallestLocation(almanach.seeds, almanach.maps)}");
        }

        private static long GetSmallestLocation(List<Seed> seeds, List<Map> maps)
        {
            List<long> locations = new List<long>();

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
                    almanach.seeds.Add(new Seed(long.Parse(s)));
                }
            }
        }

        private static void CreateRangedSeeds(string input)
        {
            string[] splittedInput = input.Split(" ", StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i <= splittedInput.Length - 1; i++)
            {
                string s = splittedInput[i];
                if (char.IsDigit(s.First()))
                {
                    almanach.seeds.Add(new Seed(long.Parse(s), long.Parse(splittedInput[i+1])));
                    i++;
                }
            }
        }

        private static void CreateMaps(string[] input)
        {
            for (long i = 1; i < input.Length; i++)
            {
                if (!char.IsDigit(input[i].First()))
                {
                    Map mapToAdd = new Map();
                    almanach.maps.Add(mapToAdd);

                    //Console.WriteLine($"New map!");
                }
                else
                    almanach.maps.Last().AddEntry(input[i]);
            }
        }

        private static void MixMaps(List<Map> maps)
        {

        }
    }

    public class Seed
    {
        public long id = 0;
        public long seedRange = 0;

        public Seed (long _id)
        {
            id = _id;
            //Console.WriteLine($"New Seed: {id}");
        }

        public Seed(long _id, long _seedRange)
        {
            id = _id;
            seedRange = _seedRange;
            //Console.WriteLine($"New Ranged Seed: {id} -> {id + seedRange}");
        }

        public long GetLocation(List<Map> maps)
        {
            long tmp = 0;

            for (long i = 0; i <= seedRange; i++)
            {
                long derp = id + i;
                foreach (Map map in maps)
                {
                    derp = map.Convert(derp);
                }

                if (derp < tmp || tmp == 0)
                    tmp = derp;
                //Console.WriteLine($"From: {id + i}, Converted: {derp}");
            }
            //Console.WriteLine($"Smallest in seed range: {tmp}");

            return tmp;
        }
    }

    public class Entry
    {
        public long source;
        public long dest;
        public long range;

        public Entry(long _source, long _dest, long _range)
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
            long range = long.Parse(_entry.Split(" ", StringSplitOptions.RemoveEmptyEntries)[2]);
            long source = long.Parse(_entry.Split(" ", StringSplitOptions.RemoveEmptyEntries)[1]);
            long dest = long.Parse(_entry.Split(" ", StringSplitOptions.RemoveEmptyEntries)[0]);
            Entry entry = new Entry(source, dest, range);

            //Console.WriteLine($"New entry source: {entry.source} dest: {entry.dest} range: {entry.range}");
            sourceDest.Add(entry);
        }

        public long Convert(long sourceInput)
        {
            foreach (Entry entry in sourceDest)
            {
                if (sourceInput >= entry.source && sourceInput <= entry.source + entry.range)
                {
                    //Console.WriteLine(sourceInput - entry.source + entry.dest);
                    return sourceInput - entry.source + entry.dest;
                }
            }
            //.WriteLine(sourceInput);
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
