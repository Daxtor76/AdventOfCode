using Microsoft.VisualStudio.TestTools.UnitTesting;
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

            firstCompartment = GetInitialCompartments(textSplitted, out List<string> secondHalfList);
            secondCompartment = secondHalfList;
            totalScore = GetTotalScore(firstCompartment, secondCompartment);
        }

        static int GetTotalScore(params List<string>[] compartments)
        {
            int value = 0;

            for (int i = 0; i < compartments[0].Count; i++)
            {
                Assert.IsTrue(compartments[0][i].Count() == compartments[1][i].Count(), $"Compartments of bag {i} are not same size.");

                for (int y = 0; y < compartments[1][i].Count(); y++)
                {
                    value += GetCommonLettersValue(compartments[0][i], compartments[1][i]);
                    break;
                }
            }
            Console.WriteLine(value);

            return value;
        }

        static int GetCommonLettersValue(string str1, string str2)
        {
            Dictionary<char, int> lettersPriorities = new Dictionary<char, int>();

            lettersPriorities = GetPriorities();
            for (int i = 0; i < str2.Length; i++)
            {
                if (str1.Contains(str2[i], StringComparison.Ordinal))
                {
                    Console.WriteLine($"Common char: {str2[i]}({lettersPriorities[str2[i]]})");
                    return lettersPriorities[str2[i]];
                }
            }

            return 0;
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

            /*foreach (KeyValuePair<char, int> kvp in tmpDico)
            {
                Console.WriteLine($"Key: {kvp.Key} / Value: {kvp.Value}");
            }*/

            return tmpDico;
        }

        static List<string> GetInitialCompartments(string[] data, out List<string> secondHalfList)
        {
            List<string> tmpList = new List<string>();
            secondHalfList = new List<string>();

            for (int i = 0; i < data.Length; i++)
            {
                string firstHalf = data[i].Substring(0, data[i].Length / 2);
                string secondHalf = data[i].Substring(data[i].Length / 2);

                tmpList.Add(firstHalf);
                secondHalfList.Add(secondHalf);
            }

            return tmpList;
        }
    }
}
