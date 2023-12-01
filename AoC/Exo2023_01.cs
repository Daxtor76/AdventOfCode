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
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace AoC2023_Exo1
{
    /*
     Former un nombre avec les deux chiffres (premier et dernier) char de chaque string
     Faire la somme de tout ça
    */
    public class Program
    {
        // ICI LA LES VARIABLES, PAS EN DESSOUS -_-

        public static Dictionary<string, int> writingDB = new Dictionary<string, int>()
        {
            {"un", 1},
            {"two", 2},
            {"three", 3},
            {"four", 4},
            {"five", 5},
            {"six", 6},
            {"seven", 7},
            {"eight", 8},
            {"nine", 9},
            {"ten", 10},
            {"eleven", 11},
            {"twelve", 12},
            {"thirteen", 13},
            {"fifteen", 15},
            {"twenty", 20},
            {"thirty", 30},
            {"fourty", 40},
            {"fifty", 50},
            {"sixty", 60},
            {"seventy", 70},
            {"eighty", 80},
            {"ninety", 90},
        };

        public static void Main()
        {
            string text = Utils.ReadFile("E:\\Projets\\AdventOfCode\\AdventOfCode\\AoC\\Exo2023_01.txt");
            string[] textSplitted = text.Split(Environment.NewLine);
            int total = 0;

            foreach (string line in textSplitted)
            {
                total += ExtractNumbersFromString(line);
            }

            Console.WriteLine(total);

            //Console.WriteLine(ExtractNumbersFromStringWithLetters(textSplitted[0]));
        }

        /*public static int ExtractNumbersFromStringWithLetters(string text)
        {
            string[] cuts = text.Split(writingDB.Keys);
        }*/

        public static int ExtractNumbersFromString(string text)
        {
            string result = text.ToList().First(char.IsDigit).ToString();
            result += text.ToList().Last(char.IsDigit).ToString();

            //Console.WriteLine($"{int.Parse(result)} -> {text}");
            return int.Parse(result);
        }
    }

    [TestClass]
    public class Tests
    {
        [TestMethod]
        public void Test()
        {
            Assert.AreEqual(12, Program.ExtractNumbersFromString("12"));
            Assert.AreEqual(12, Program.ExtractNumbersFromString("1abc2"));
            Assert.AreEqual(15, Program.ExtractNumbersFromString("a1b2c3d4e5f"));
        }
    }
}
