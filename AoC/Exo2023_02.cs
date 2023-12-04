using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using NuGet.Frameworks;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Formatters;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace AoC2023_Exo2
{
    /*
    */
    public class Program
    {
        public static Dictionary<string, int> totalCubes = new Dictionary<string, int>
        {
            {"red", 12},
            {"green", 13},
            {"blue", 14}
        };

        public static void Main()
        {
            string text = Utils.ReadFile("E:\\Projets\\AdventOfCode\\AdventOfCode\\AoC\\Exo2023_02.txt");
            string[] textSplitted = text.Split(Environment.NewLine);
            List<Game> games = new List<Game>();

            games = GetGames(textSplitted);

            // STEP 1
            Console.WriteLine($"Total possible games id: {CompareCubesOfAllGames(games)}");
        }

        public static int CompareCubesOfAllGames(List<Game> games)
        {
            int total = 0;
            for (int i = 0; i < games.Count; i++)
            {
                foreach (Set set in games[i]._sets)
                {
                    if (set.cubes["red"] > totalCubes["red"])
                    {
                        goto End;
                    }
                    if (set.cubes["green"] > totalCubes["green"])
                    {
                        goto End;
                    }
                    if (set.cubes["blue"] > totalCubes["blue"])
                    {
                        goto End;
                    }
                }
                total += i + 1;
                End: continue;
            }

            return total;
        }

        private static List<Game> GetGames(string[] input)
        {
            List<Game> list = new List<Game>();
            for (int i = 0; i < input.Length; i++)
            {
                Game game = new Game(GetSets(input[i]));
                list.Add(game);

                Console.WriteLine($"Game {i+1} contains {game._sets.Count()} sets");
                foreach(Set set in game._sets)
                {
                    Console.WriteLine($"Red: {set.cubes["red"]}, Green: {set.cubes["green"]}, Blue: {set.cubes["blue"]}");
                }
            }

            return list;
        }

        public static List<Set> GetSets(string gameInput)
        {
            string[] separators = [":",";"];
            List<Set> sets = new List<Set>();

            foreach (string set in gameInput.Split(separators,  StringSplitOptions.RemoveEmptyEntries))
            {
                if (!set.Contains("Game"))
                {
                    string[] sep = [","," "];
                    string[] cubes = set.Split(sep, StringSplitOptions.RemoveEmptyEntries);

                    sets.Add(GetCubes(cubes));
                }
            }

            return sets;
        }

        public static Set GetCubes(string[] input)
        {
            Set tmpSet = new Set();

            for (int i = 0; i < input.Length; i++)
            {
                if (int.TryParse(input[i], out int value))
                {
                    tmpSet.cubes[input[i+1]] = value;
                }
            }

            return tmpSet;
        }
    }

    public class Game
    {
        public List<Set> _sets = new List<Set>();

        public Game(List<Set> sets)
        {
            _sets = sets;
        }   
    }

    public class Set
    {
        public Dictionary<string, int> cubes = new Dictionary<string, int>
        {
            {"red", 0},
            {"green", 0},
            {"blue", 0}
        };
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
