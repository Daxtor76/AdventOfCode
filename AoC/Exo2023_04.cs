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
        public static List<Card> winCards = new List<Card>();
        public static List<Card> myCards = new List<Card>();

        public static void Main()
        {
            string text = Utils.ReadFile("E:\\Projets\\AdventOfCode\\AdventOfCode\\AoC\\Exo2023_04.txt");
            string[] textSplitted = text.Split(Environment.NewLine);
            string[] separators = new string[]
            {
                ":",
                "|",
            };
            int total = 0;
            SplitCards(textSplitted, separators);

            for (int i = 0; i < myCards.Count(); i++)
            {
                total += myCards[i].CardScore(GetWinningNumbersAmount(winCards[i], myCards[i]));
            }
            Console.WriteLine($"Total: {total}");
        }

        public static int GetWinningNumbersAmount(Card winCard, Card comparedCard)
        {
            int amount = 0;

            foreach (int nb in winCard.numbers.Keys)
            {
                foreach (int otherNb in comparedCard.numbers.Keys)
                {
                    if (nb == otherNb)
                    {
                        amount++;
                        winCard.numbers[nb] = true;
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
                    Console.WriteLine($"WinCard: {str}");
                }
                winCards.Add(winCard);
                foreach (string str in formattedInput[2].Split(' ', StringSplitOptions.RemoveEmptyEntries).ToArray())
                {
                    myCard.AddNumber(int.Parse(str));
                    Console.WriteLine($"MyCard: {str}");
                }
                myCards.Add(myCard);
            }
        }
    }

    public class Card
    {
        public Dictionary<int, bool> numbers = new Dictionary<int, bool>();
        public int numbersAmount = 0;

        public Card(params int[] _numbers)
        {
            foreach (int i in _numbers)
            {
                numbers.Add(i, false);
            }
        }

        public void AddNumber(int number)
        {
            numbers.Add(number, false);
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
