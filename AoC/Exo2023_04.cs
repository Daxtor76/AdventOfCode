using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using NuGet.Frameworks;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.WebSockets;
using System.Numerics;
using System.Runtime.Serialization.Formatters;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AoC2023_Exo4
{
    /*
     * On cherche 13 dans l'exemple
    */
    public class Program
    {
        public static List<Set> sets = new List<Set>();

        public static void Main()
        {
            string text = Utils.ReadFile("E:\\Projets\\AdventOfCode\\AdventOfCode\\AoC\\Exo2023_04.txt");
            string[] textSplitted = text.Split(Environment.NewLine);
            string[] separators = new string[]
            {
                ":",
                "|",
            };

            SplitCards(textSplitted, separators);
            Console.WriteLine($"Total: {CardsScores(sets)}");
        }
        public static int CardsScores(List<Set> sets)
        {
            int total = 0;

            for (int i = 0; i < sets.Count(); i++)
            {
                total += sets[i].score;
            }
            return total;
        }

        public static int GetWinningNumbersAmount(Card winCard, Card comparedCard)
        {
            int amount = 0;

            foreach (int nb in winCard.numbers)
            {
                foreach (int otherNb in comparedCard.numbers)
                {
                    if (nb == otherNb)
                    {
                        amount++;
                    }
                }
            }

            return amount;
        }

        public static void SplitCards(string[] input, string[] separators)
        {
            foreach (string line in input)
            {
                string[] formattedInput = line.Split(separators, StringSplitOptions.RemoveEmptyEntries).ToArray();
                Card winCard = new Card();
                Card myCard = new Card();
                foreach (string str in formattedInput[1].Split(' ', StringSplitOptions.RemoveEmptyEntries).ToArray())
                {
                    winCard.AddNumber(int.Parse(str));
                }
                foreach (string str in formattedInput[2].Split(' ', StringSplitOptions.RemoveEmptyEntries).ToArray())
                {
                    myCard.AddNumber(int.Parse(str));
                }
                Set set = new Set(winCard, myCard);
                set.score = set.myCard.CardScore(GetWinningNumbersAmount(set.winCard, set.myCard));
                sets.Add(set);
            }
        }
    }

    public class Set
    {
        public Card winCard = new Card();
        public Card myCard = new Card();
        public int score = 0;

        public Set(Card _winCard, Card _myCard)
        {
            winCard = _winCard;
            myCard = _myCard;
        }
    }

    public class Card
    {
        public List<int> numbers = new List<int>();
        public int numbersAmount = 0;

        public Card(params int[] _numbers)
        {
            foreach (int i in _numbers)
            {
                numbers.Add(i);
            }
        }

        public void AddNumber(int number)
        {
            numbers.Add(number);
        }

        public int CardScore(int matchingNumbersAmount)
        {
            int finalScore = matchingNumbersAmount > 0 ? 1 : 0;
            for (int i = 0; i < matchingNumbersAmount - 1; i++)
            {
                finalScore *= 2;
            }

            return finalScore;
        }
    }

    [TestClass]
    public class Tests
    {
        [TestMethod]
        public void Test()
        {
            Card winCard = new Card(41, 48, 83, 86, 17);
            Card myCard = new Card(83, 86, 6, 31, 17, 9, 48, 53);
            Assert.AreEqual(8, myCard.CardScore(Program.GetWinningNumbersAmount(winCard, myCard)));
        }
    }
}
