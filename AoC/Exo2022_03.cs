using Microsoft.VisualStudio.TestTools.UnitTesting;
using NuGet.Frameworks;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace AoC2022_Exo3
{
    /*
    Infos
    */
    public class Program
    {
        // ICI LA LES VARIABLES, PAS EN DESSOUS -_-
        public static void Main()
        {
            string text = Utils.ReadFile("C:\\Prototypes_Perso\\AdventOfCode\\AoC\\Exo2022_03.txt");
            string[] textSplitted = text.Split(Environment.NewLine);
            List<string> firstCompartment = new List<string>();
            List<string> secondCompartment = new List<string>();
            int totalScore = 0;

            // Debug to get ascii values of chars
            //Console.WriteLine(((char)65).ToString()); //A
            //Console.WriteLine(Convert.ToInt32(char.Parse("a"))); // 97
            //Console.WriteLine(Convert.ToInt32(char.Parse("z"))); // 122
            //Console.WriteLine(Convert.ToInt32(char.Parse("A"))); // 65
            //Console.WriteLine(Convert.ToInt32(char.Parse("Z"))); // 90

            totalScore = GetTotalScore(textSplitted);
        }

        static int GetTotalScore(string[] backpacks)
        {
            int value = 0;

            for (int i = 0; i < backpacks.Count(); i++)
            {
                string part1 = backpacks[i].Substring(0, backpacks[i].Length/2);
                string part2 = backpacks[i].Substring(backpacks[i].Length/2);

                Assert.IsTrue(part1.Length == part2.Length, $"Compartments of bag {i} are not same size.");

                value += GetCommonLettersValue(part1, part2);
            }
            Console.WriteLine(value);

            return value;
        }

        static int GetCommonLettersValue(string str1, string str2)
        {
            Dictionary<char, int> lettersPriorities = new Dictionary<char, int>();
            HashSet<char> chars1 = new HashSet<char>(str1.ToCharArray());
            HashSet<char> chars2 = new HashSet<char>(str2.ToCharArray());
            int value = 0;

            lettersPriorities = GetPriorities();

            foreach (char c in chars1.Intersect(chars2))
            {
                Assert.IsNotNull(c, $"No common char between {str1} and {str2}");

                Console.WriteLine($"common char: {c}({lettersPriorities[c]})");
                value += lettersPriorities[c];
            }

            return value;
        }

        static Dictionary<char, int> GetPriorities()
        {
            Dictionary<char, int> tmpDico = new();

            for (int i = 1; i <= 26; i++)
            {
                tmpDico.Add((char)(96+i), i);
            }

            for (int i = 1; i <= 26; i++)
            {
                tmpDico.Add((char)(64 + i), i+26);
            }

            return tmpDico;
        }
    }
}
