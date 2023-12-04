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

namespace AoC2023_Exo1
{
    /*
    * 53293 too low
    */
    public class Program
    {
        // ICI LA LES VARIABLES, PAS EN DESSOUS -_-

        public static void Main()
        {
            string text = Utils.ReadFile("E:\\Projets\\AdventOfCode\\AdventOfCode\\AoC\\Exo2023_01.txt");
            string[] textSplitted = text.Split(Environment.NewLine);
            int total = 0;

            //Step 1
            /*foreach (string line in textSplitted)
            {
                total += ExtractNumbersFromString(line);
            }*/

            //Step 2
            foreach (string line in textSplitted)
            {
                Console.WriteLine($"{line} -> {ChangeLettersToDigits(SplittedInputLeftToRight(line))} -> {ExtractNumbersFromString(ChangeLettersToDigits(SplittedInputLeftToRight(line)))}");
                total += ExtractNumbersFromString(ChangeLettersToDigits(SplittedInputLeftToRight(line)));
            }

            Console.WriteLine(total);
        }

        public static string ChangeLettersToDigits(string[] inputSplitted)
        {
            Dictionary<string, int> db = new Dictionary<string, int>()
            {
                { "one", 1 },
                { "two", 2 },
                { "three", 3 },
                { "four", 4 },
                { "five", 5 },
                { "six", 6 },
                { "seven", 7 },
                { "eight", 8 },
                { "nine" , 9}
            };

            string lettersInDigits = "";
            for (int i = 0; i < inputSplitted.Length; i++)
            {
                if (db.ContainsKey(inputSplitted[i]) || char.IsDigit(inputSplitted[i].First()))
                {
                    lettersInDigits += db.ContainsKey(inputSplitted[i]) ? db[inputSplitted[i]].ToString() : inputSplitted[i].ToString();
                }
                else
                {
                    continue;
                }
            }
            //Console.WriteLine($"formatted string : {lettersInDigits}");
            
            return lettersInDigits;
        }

        public static string[] SplittedInputLeftToRight(string input)
        {
            Dictionary<string, int> db = new Dictionary<string, int>()
            {
                { "one", 1 },
                { "two", 2 },
                { "three", 3 },
                { "four", 4 },
                { "five", 5 },
                { "six", 6 },
                { "seven", 7 },
                { "eight", 8 },
                { "nine" , 9}
            };
            string strRegex = GetAllDBKeysFormatted(db);
            //Regex regex = new Regex(strRegex);
            List<string> inputSplitted = new List<string>();

            try
            {
                foreach (Match match in Regex.Matches(input, strRegex))
                {
                    inputSplitted.Add(match.Value);
                }
            }
            catch (RegexMatchTimeoutException)
            {
                // do nothing
            }

            inputSplitted.Add(SplittedInputRightToLeft(input)[0]);
            
            //inputSplitted = inputSplitted.Where(x => !string.IsNullOrEmpty(x)).ToArray();
            return inputSplitted.ToArray();
        }

        public static List<string> SplittedInputRightToLeft(string input)
        {
            Dictionary<string, int> db = new Dictionary<string, int>()
            {
                { "one", 1 },
                { "two", 2 },
                { "three", 3 },
                { "four", 4 },
                { "five", 5 },
                { "six", 6 },
                { "seven", 7 },
                { "eight", 8 },
                { "nine" , 9}
            };
            string strRegex = GetAllDBKeysFormatted(db);
            //Regex regex = new Regex(strRegex);
            List<string> inputSplitted = new List<string>();

            try
            {
                foreach (Match match in Regex.Matches(input, strRegex, RegexOptions.RightToLeft))
                {
                    inputSplitted.Add(match.Value);
                }
            }
            catch (RegexMatchTimeoutException)
            {
                // do nothing
            }

            return inputSplitted;
        }

        private static string GetAllDBKeysFormatted(Dictionary<string, int> db)
        {
            string tmp = "(";
            foreach (string s in db.Keys)
            {
                tmp += s;
                tmp += "|";
                tmp += db[s].ToString();
                tmp += "|";
            }
            tmp = tmp.Substring(0, tmp.Length - 1);
            tmp += ")";
            return tmp;
        }

        public static string WriteIntInLetters(int number)
        {
            Dictionary<int, string> db = new Dictionary<int, string>()
            {
                { 1, "one"},
                { 2, "two"},
                { 3, "three"},
                { 4, "four"},
                { 5, "five"},
                { 6, "six"},
                { 7, "seven"},
                { 8, "eight"},
                { 9, "nine"},
                { 10, "ten"},
                { 11, "eleven"},
                { 12, "twelve"},
                { 13, "thirteen"},
                { 15, "fifteen"},
                { 20, "twenty"},
                { 30, "thirty"},
                { 50, "fifty"}
            };
            string intInLetters = "";
            string strNumber = number.ToString();
            int firstDigit = int.Parse(strNumber.First().ToString());
            int lastDigit = int.Parse(strNumber.Last().ToString());

            Assert.IsTrue(number.ToString().Length < 3, "3 digits numbers are not supported");

            if (!db.ContainsKey(number))
            {
                if (firstDigit == 1)
                {
                    intInLetters = db[lastDigit] + "teen";
                }
                else if (firstDigit == 2 || firstDigit == 3 || firstDigit == 5)
                {
                    intInLetters = db[firstDigit*10] + db[lastDigit];
                }
                else
                {
                    if (lastDigit == 0)
                    {
                        intInLetters = db[firstDigit] + "ty";
                    }
                    else
                    {
                        intInLetters = db[firstDigit] + "ty" + db[lastDigit];
                    }
                }
            }
            else
            {
                intInLetters = db[number];
            }

            return intInLetters;
        }

        public static int ExtractNumbersFromString(string text)
        {
            string result = text.ToList().First(char.IsDigit).ToString();
            result += text.ToList().Last(char.IsDigit).ToString();

            //Console.WriteLine($"{text} -> {int.Parse(result)}");
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
        [TestMethod]
        public void TestStep2()
        {
            Assert.AreEqual(88, Program.ExtractNumbersFromString(Program.ChangeLettersToDigits(Program.SplittedInputLeftToRight("eight"))));
        }
    }
}
