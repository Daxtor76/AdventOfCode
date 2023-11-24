using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
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
        public static Dictionary<char, int> lettersPriorities = new Dictionary<char, int>();
        public static void Main()
        {
            string text = Utils.ReadFile("C:\\Prototypes_Perso\\AdventOfCode\\AoC\\Exo2022_03.txt");
            string[] textSplitted = text.Split(Environment.NewLine);
            int totalScoreByBackpack = 0;
            int totalScoreByGroup = 0;

            lettersPriorities = GetPriorities();

            // Debug to get ascii values of chars
            //Console.WriteLine(((char)65).ToString()); //A
            //Console.WriteLine(Convert.ToInt32(char.Parse("a"))); // 97
            //Console.WriteLine(Convert.ToInt32(char.Parse("z"))); // 122
            //Console.WriteLine(Convert.ToInt32(char.Parse("A"))); // 65
            //Console.WriteLine(Convert.ToInt32(char.Parse("Z"))); // 90

            //totalScoreByBackpack = GetTotalScoreByBackPack(textSplitted);
            totalScoreByGroup = GetTotalScoreByGroup(textSplitted, 3);
        }

        static int GetTotalScoreByGroup(string[] backPacks, int groupLength)
        {
            int value = 0;

            for (int i = 0; i < backPacks.Count(); i+=groupLength)
            {
                HashSet<char> c = GetCommonLetters(backPacks[i], backPacks[i + 1]);
                value += GetCommonLettersValue(c, backPacks[i+2]);
            }
            Console.WriteLine(value);

            return value;
        }

        static int GetTotalScoreByBackPack(string[] backPacks)
        {
            int value = 0;

            for (int i = 0; i < backPacks.Count(); i++)
            {
                string part1 = backPacks[i].Substring(0, backPacks[i].Length/2);
                string part2 = backPacks[i].Substring(backPacks[i].Length/2);

                Assert.IsTrue(part1.Length == part2.Length, $"Compartments of bag {i} are not same size.");

                value += GetCommonLettersValue(part1, part2);
            }
            //Console.WriteLine(value);

            return value;
        }

        static HashSet<char> GetCommonLetters(string str1, string str2)
        {
            HashSet<char> chars1 = new HashSet<char>(str1.ToCharArray());
            HashSet<char> chars2 = new HashSet<char>(str2.ToCharArray());
            HashSet<char> common = new();

            foreach (char c in chars1.Intersect(chars2))
            {
                Assert.IsNotNull(c, $"No common char between {str1} and {str2}");

                Console.WriteLine($"common char: {c}({lettersPriorities[c]})");
                common.Add(c);
            }

            return common;
        }

        static int GetCommonLettersValue(string str1, string str2)
        {
            HashSet<char> chars1 = new HashSet<char>(str1.ToCharArray());
            HashSet<char> chars2 = new HashSet<char>(str2.ToCharArray());
            int value = 0;

            foreach (char c in chars1.Intersect(chars2))
            {
                Assert.IsNotNull(c, $"No common char between {str1} and {str2}");

                Console.WriteLine($"Finalcommon char: {c}({lettersPriorities[c]})");
                value += lettersPriorities[c];
            }

            return value;
        }

        static int GetCommonLettersValue(HashSet<char> chars, string str2)
        {
            HashSet<char> chars2 = new HashSet<char>(str2.ToCharArray());
            int value = 0;

            foreach (char c in chars.Intersect(chars2))
            {
                Assert.IsNotNull(c, $"No common char between {chars} and {str2}");

                Console.WriteLine($"Final common char: {c}({lettersPriorities[c]})");
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
