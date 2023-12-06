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
        public static void Main()
        {
            string text = Utils.ReadFile("E:\\Projets\\AdventOfCode\\AdventOfCode\\AoC\\Exo2023_04.txt");
            string[] textSplitted = text.Split(Environment.NewLine);
            string[] separators = new string[]
            {
                ":",
                "|",
                " "
            };
            string[] textFormatted = FormatText(textSplitted, separators);

            List<Card> winCards = new List<Card>();
            List<Card> myCards = new List<Card>();
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

        public static string[] FormatText(string[] input, string[] separators)
        {
            string[] formattedInput = [];
            foreach(string str in input)
            {
                formattedInput = str.Split(separators, StringSplitOptions.RemoveEmptyEntries).ToArray();
            }

            return formattedInput;
        }
    
    }

    public class Card
    {
        public Dictionary<int, bool> numbers = new Dictionary<int, bool>();
        public int numbersAmount = 0;

        public Card(int amount) 
        {
            numbersAmount = amount;
        }

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
            Card card1 = new Card(41, 48, 83, 86, 17);
            Card card2 = new Card(83, 86, 6, 31, 17, 9, 48, 53);
            Assert.AreEqual(8, card1.CardScore(Program.GetWinningNumbersAmount(card2, card1)));
        }
    }
}
