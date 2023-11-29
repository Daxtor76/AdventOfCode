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

namespace AoC2022_Exo5
{
    /*
    Infos
    */
    public class Program
    {
        // ICI LA LES VARIABLES, PAS EN DESSOUS -_-
        public static List<Instruction> instructions = new List<Instruction>();
        public static string[] instrSeparators = new string[]
        {
            "move",
            "from",
            "to"
        };

        public static void Main()
        {
            string text = Utils.ReadFile("C:\\Prototypes_Perso\\AdventOfCode\\AoC\\Exo2022_05.txt");
            string[] textSplitted = text.Split(Environment.NewLine);
            List<string> textFormatted = FormatText(textSplitted);
            List<Instruction> instructions = GetInstructions(textFormatted);
        }

        private static List<Instruction> GetInstructions(List<string> textFormatted)
        {
            List<Instruction> tmpList = new List<Instruction>();
            foreach (string line in textFormatted)
            {
                if (line.Contains(instrSeparators[0]) || line.Contains(instrSeparators[1]) || line.Contains(instrSeparators[2]))
                {
                    Instruction instr = new Instruction(line);
                    tmpList.Add(instr);

                    Console.WriteLine($"move : {instr._amount} from : {instr._from} to {instr._to}");
                }
            }

            return tmpList;
        }

        private static List<string> FormatText(string[] textSplitted)
        {
            List<string> result = new List<string>();
            foreach (string line in textSplitted)
            {
                string newString = line;

                newString = newString.Replace("   ", "-");
                newString = newString.Replace(" ", "");
                newString = newString.Replace("[", "");
                newString = newString.Replace("]", "");
                result.Add(newString);
                //Console.WriteLine(newString);
            }

            return result;
        }
    }

    public class Instruction()
    {
        public int _amount;
        public int _from;
        public int _to;

        public Instruction(string rawInstr) : this()
        {
            string raw = rawInstr.Replace(" ", "");
            string[] instrValues = raw.Split(Program.instrSeparators, StringSplitOptions.RemoveEmptyEntries);
            _amount = int.Parse(instrValues[0]);
            _from = int.Parse(instrValues[1]);
            _to = int.Parse(instrValues[2]);
        }
    }
    [TestClass]
    public class Tests
    {
        [TestMethod]
        public void Test1Digit()
        {
            Instruction rawInstr = new Instruction("move1from2to3");
            Assert.AreEqual(1, rawInstr._amount);
            Assert.AreEqual(2, rawInstr._from);
            Assert.AreEqual(3, rawInstr._to);
        }
        [TestMethod]
        public void Test2Digits()
        {
            Instruction rawInstr = new Instruction("move12from23to34");
            Assert.AreEqual(12, rawInstr._amount);
            Assert.AreEqual(23, rawInstr._from);
            Assert.AreEqual(34, rawInstr._to);
        }
        [TestMethod]
        public void Test2DigitsWithSpaces()
        {
            Instruction rawInstr = new Instruction(" move12from23to34 ");
            Assert.AreEqual(12, rawInstr._amount);
            Assert.AreEqual(23, rawInstr._from);
            Assert.AreEqual(34, rawInstr._to);
        }
    }
}
