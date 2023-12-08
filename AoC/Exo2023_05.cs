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

            Console.WriteLine(GetSmallestLocation(almanach.seeds, almanach.maps));

            //DEBUG SEEDS
            /*foreach (Seed seed in almanach.seeds)
            {
                Console.WriteLine(seed.id);
            }*/

            /* DEBUG MAPS
            foreach (Map map in almanach.maps)
            {
                Console.WriteLine($"New map:");
                foreach (Int64 i in map.destSources.Keys.ToList())
                {
                    Console.WriteLine($"Dest: {i}, Source: {map.destSources[i]}");
                }
            }*/
        }

        private static Int64 GetSmallestLocation(List<Seed> seeds, List<Map> maps)
        {
            List<Int64> locations = new List<Int64>();

            foreach (Seed seed in seeds)
            {
                locations.Add(seed.GetLocation(maps));
                Console.WriteLine(seed.GetLocation(maps));
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
                    almanach.seeds.Add(new Seed(Int64.Parse(s)));
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
                }
                else
                    almanach.maps.Last().AddEntry(input[i]);
            }
        }
    }

    public class Seed
    {
        public Int64 id = 0;

        public Seed (Int64 _id)
        {
            this.id = _id;
        }

        public Int64 GetLocation(List<Map> maps)
        {
            Int64 loc = id;

            foreach(Map map in maps)
            {
                loc = map.Convert(loc);
            }

            return loc;
        }
    }

    public class Map
    {
        public List<Vector3> sourceDest = new List<Vector3>();
        public void AddEntry(string _entry)
        {
            Int64 range = Int64.Parse(_entry.Split(" ", StringSplitOptions.RemoveEmptyEntries)[2]);
            Int64 source = Int64.Parse(_entry.Split(" ", StringSplitOptions.RemoveEmptyEntries)[1]);
            Int64 dest = Int64.Parse(_entry.Split(" ", StringSplitOptions.RemoveEmptyEntries)[0]);

            Vector3 entry = new Vector3((Int64)source, (Int64)dest, (Int64)range);
            sourceDest.Add(entry);
        }

        public Int64 Convert(Int64 sourceInput)
        {
            foreach (Vector3 entry in sourceDest)
            {
                Int64 calculatedRange = (Int64)entry.Y - (Int64)entry.X;
                if (sourceInput >= entry.X && sourceInput <= entry.X + entry.Z)
                {
                    return (Int64)(sourceInput - entry.X + entry.Y);
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
