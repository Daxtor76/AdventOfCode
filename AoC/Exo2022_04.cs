using AoC2022_Exo2;
using Microsoft.VisualStudio.TestPlatform.CoreUtilities.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using NuGet.Frameworks;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace AoC2022_Exo4
{
    /*
    Infos
    */
    public class Program
    {
        // ICI LA LES VARIABLES, PAS EN DESSOUS -_-
        public static void Main()
        {
            string text = Utils.ReadFile("C:\\Prototypes_Perso\\AdventOfCode\\AoC\\Exo2022_04.txt");
            string[] textSplitted = text.Split(Environment.NewLine);
            int totalContained = 0;
            int totalOverlap = 0;

            for (int i = 0; i < textSplitted.Length; i++)
            {
                totalContained += isContained(textSplitted[i].ToString());
                totalOverlap += isOverlap(textSplitted[i].ToString());
            }

            Console.WriteLine(textSplitted.Length);
            Console.WriteLine(totalContained);
            Console.WriteLine(totalOverlap);
        }

        public static int isContained(string ens)
        {
            string[] delimitators = new string[2]
            {
                "-",
                ","
            };
            string[] tab = ens.Split(delimitators, 4, StringSplitOptions.None);

            int range1Start = int.Parse(tab[0]);
            int range1End = int.Parse(tab[1]);
            int range2Start = int.Parse(tab[2]);
            int range2End = int.Parse(tab[3]);

            // FOOTGUN => C'EST DE LA MERDE
            // Faire des classes propres svp
            bool twoContainsOne = isContained(range1Start, range1End, range2Start, range2End);
            bool oneContainsTwo = isContained(range2Start, range2End, range1Start, range1End);
            if (oneContainsTwo||twoContainsOne)
            {
                return 1;
            }
            return 0;
        }

        private static bool isContained(int range1Start, int range1End, int range2Start, int range2End)
        {
            bool startOk = range1Start >= range2Start;
            bool endOk = range1End <= range2End;

            return startOk && endOk;
        }

        public static int isOverlap(string ens)
        {
            string[] delimitators = new string[2]
            {
                "-",
                ","
            };
            string[] tab = ens.Split(delimitators, 4, StringSplitOptions.None);

            int range1Start = int.Parse(tab[0]);
            int range1End = int.Parse(tab[1]);
            int range2Start = int.Parse(tab[2]);
            int range2End = int.Parse(tab[3]);

            // FOOTGUN => C'EST DE LA MERDE
            // Faire des classes propres svp
            bool isOverlapping = isOverlap(range1Start, range1End, range2Start, range2End);
            if (isOverlapping)
            {
                return 1;
            }
            return 0;
        }

        private static bool isOverlap(int range1Start, int range1End, int range2Start, int range2End)
        {
            bool startOk = range1Start >= range2Start && range1Start <= range2End;
            bool endOk = range1End <= range2End && range1End >= range2Start;

            return startOk || endOk;
        }
    }
    [TestClass]
    public class Tests
    {
        [TestMethod]
        public void Test()
        {
            Assert.AreEqual(1, Program.isContained("6-6,4-6"));
            Assert.AreEqual(1, Program.isContained("4-6,6-6"));
            Assert.AreEqual(1, Program.isContained("2-8,3-7"));
            Assert.AreEqual(0, Program.isContained("0-1,3-7"));
            Assert.AreEqual(1, Program.isContained("2-16,15-16"));
            Assert.AreEqual(1, Program.isContained("1-5,1-3"));
            Assert.AreEqual(1, Program.isContained("0-1000,300-400"));
            Assert.AreEqual(0, Program.isContained("1000-2000,3000-4000"));
            Assert.AreEqual(1, Program.isContained("1000-1000,1000-1000"));
        }

        public string test = @"2-4,6-8
2-3,4-5
5-7,7-9
2-8,3-7
6-6,4-6
2-6,4-8"; 
        [TestMethod]
        public void TestTotal()
        {
            int totalContained = 0;
            string[] testSplitted = test.Split(Environment.NewLine);
            for (int i = 0; i < testSplitted.Length; i++)
            {
                totalContained += Program.isContained(testSplitted[i].ToString());
            }
            Assert.AreEqual(2, totalContained);
        }
    }
}
