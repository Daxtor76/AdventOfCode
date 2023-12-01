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
     Former un nombre avec les deux chiffres (premier et dernier) char de chaque string
     Faire la somme de tout ça
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
            }

            Console.WriteLine(total);*/

            //Step 2
            /*foreach (string line in textSplitted)
            {
                total += GetIntFromLetters(SplittedInput(line));
            }*/
            GetIntFromLetters(SplittedInput("abcninetytwoabc"));
        }

        public static int GetIntFromLetters(string[] inputSplitted)
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
                { "nine" , 9},
                { "ten" , 10},
                { "eleven" , 11},
                { "twelve" , 12},
                { "thirteen" , 13},
                { "fifteen" , 15},
                { "twenty" , 20},
                { "thirty" , 30},
                { "fifty" , 50}
            };

            int lettersInInt = 0;
            for (int i = 0; i < inputSplitted.Length; i++)
            {
                if (!db.ContainsKey(inputSplitted[i]) || char.IsDigit(inputSplitted[i].ToCharArray().First()))
                {
                    continue;
                }
                else
                {
                    if (inputSplitted.Length == 1)
                        lettersInInt = db[inputSplitted[i]];
                    if (inputSplitted.Length > 1)
                    {
                        lettersInInt = db[inputSplitted[i]];
                        if (inputSplitted[i+1] == "ty")
                            lettersInInt *= 10;
                        else if (inputSplitted[i+1] == "teen")
                            lettersInInt += 10;
                        i += 1;
                    }
                    if (inputSplitted.Length > 2)
                    {
                        lettersInInt += db[inputSplitted[i+2]];
                        i += 1;
                    }
                }
            }

            return lettersInInt;
        }

        private static string[] SplittedInput(string input)
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
                { "nine" , 9},
                { "ten" , 10},
                { "eleven" , 11},
                { "twelve" , 12},
                { "thirteen" , 13},
                { "fifteen" , 15},
                { "twenty" , 20},
                { "thirty" , 30},
                { "fifty" , 50}
            };
            string strRegex = GetAllDBKeysFormatted(db);
            Regex regex = new Regex(strRegex);
            string[] inputSplitted = regex.Split(input);
            inputSplitted = inputSplitted.Where(x => !string.IsNullOrEmpty(x)).ToArray();
            return inputSplitted;
        }

        private static string GetAllDBKeysFormatted(Dictionary<string, int> db)
        {
            string tmp = "(";
            foreach (string s in db.Keys)
            {
                tmp += s;
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
