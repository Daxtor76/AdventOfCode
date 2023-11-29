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
            Dictionary<int, List<char>> stacks = GetStacks(textFormatted);

            foreach (Instruction instr in instructions)
            {
                instr.Apply(stacks);
            }

            string finalStr = GetResult(stacks);
            Console.WriteLine(finalStr);
        }

        private static string GetResult(Dictionary<int, List<char>> stacks)
        {
            string tmp = "";
            foreach (List<char> list in stacks.Values)
            {
                tmp += list[^1];
            }

            return tmp;
        }

        private static Dictionary<int, List<char>> GetStacks(List<string> textFormatted)
        {
            Dictionary<int, List<char>> tmpDic = new Dictionary<int, List<char>>();
            for (int i = 0; i < textFormatted.Count; i++)
            {
                string line = textFormatted[i];
                if (!line.Contains("1-"))
                {
                    for (int y = 0; y < line.Length; y++)
                    {
                        if (!tmpDic.ContainsKey(y + 1))
                            tmpDic.Add(y + 1, new List<char>());

                        if (line[y] != (char)45)
                            tmpDic[y + 1]?.Add(line[y]);
                    }
                }
                else
                    break;
            }

            foreach (List<char> list in tmpDic.Values)
            {
                list.Reverse();
            }

            return tmpDic;
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

                newString = newString.Replace("    ", "- ");
                newString = newString.Replace("   ", "-");
                newString = newString.Replace(" ", "");
                newString = newString.Replace("[", "");
                newString = newString.Replace("]", "");
                result.Add(newString);
                Console.WriteLine(newString);
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

        public void Apply(Dictionary<int, List<char>> stacks)
        {
            for (int i = 0; i < _amount; i++)
            {
                stacks[_to].Add(stacks[_from][^1]);
                Console.WriteLine($"Moved {stacks[_from][^1]} from stack {_from} to {_to}");
                stacks[_from].RemoveAt(stacks[_from].Count - 1);
            }
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
