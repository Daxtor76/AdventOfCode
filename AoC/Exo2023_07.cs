using AoC2022_Exo1;
using AoC2023_Exo4;
using AoC2023_Exo5;
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
using System.Linq.Expressions;
using System.Net;
using System.Net.WebSockets;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;
using System.Runtime.Serialization.Formatters;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AoC2023_Exo7
{
    /*
     * 255260935 too high
     * 255048101 ok !
    */
    public class Program
    {
        public static List<char> cardsValues = new List<char>
        {
            '2',
            '3',
            '4',
            '5',
            '6',
            '7',
            '8',
            '9',
            'T',
            'J',
            'Q',
            'K',
            'A',
        };
        public static List<char> cardsValuesStep2 = new List<char>
        {
            'J',
            '2',
            '3',
            '4',
            '5',
            '6',
            '7',
            '8',
            '9',
            'T',
            'Q',
            'K',
            'A',
        };
        public static List<string> handTypesValues = new List<string>
        {
            "HC",
            "OP",
            "TP",
            "TOAK",
            "FH",
            "FoOAK",
            "FiOAK"
        };

        public static List<Hand> hands = new List<Hand>();
        public static void Main()
        {
            string text = Utils.ReadFile("E:\\Projets\\AdventOfCode\\AdventOfCode\\AoC\\Exo2023_07.txt");
            string[] textSplitted = text.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

            Stopwatch jackeline = Stopwatch.StartNew();

            // STEP 1
            int total = 0;
            hands = OrderHands(GetHands(textSplitted), cardsValues);
            foreach (Hand hand in hands)
            {
                total += hand.GetHandScore();
                //Console.WriteLine($"hand: {hand.cards} - bid: {hand.bid} - type: {hand.type} - value: {GetHandTypeValue(hand)} - rank: {hand.rank} - score: {hand.GetHandScore()}");
            }
            Console.WriteLine($"{total}");

            // STEP 2
            // Nouvelle main = handtype + xJ

            Console.WriteLine($"Time elapsed: {jackeline.ElapsedMilliseconds}");
        }

        public static List<Hand> OrderHands(List<Hand> hands, List<char> cardsValuesData)
        {
            List<Hand> tmp = hands.OrderBy(x => GetHandTypeValue(x))
                                  .ThenBy(x => GetHandValue(x, cardsValuesData))
                                  .ToList();

            foreach (Hand hand in tmp)
            {
                hand.rank = tmp.IndexOf(hand) + 1;
            }

            return tmp;
        }

        public static Hand GetBestCombination(Hand hand)
        {
            List<Hand> combinations = new List<Hand>();

            // string.replace()

            return combinations.OrderBy(x => GetHandTypeValue(x)).ToList().Last();
        }

        public static int GetHandValue(Hand hand, List<char> data)
        {
            string tmp = "";
            foreach (char c in hand.cards)
            {
                tmp += FormatCardValue(data.FindIndex(t => t == c));
            }

            return int.Parse(tmp);
        }

        public static string FormatCardValue(int value)
        {
            return value.ToString().Length < 2 ? $"{value:D2}" : value.ToString();
        }

        public static int GetHandTypeValue(Hand hand)
        {
            return handTypesValues.FindIndex(t => t == hand.type);
        }

        public static List<Hand> GetHands(string[] input)
        {
            List<Hand> hands = new List<Hand>();

            foreach (string line in input)
            {
                string[] splittedInput = line.Split(" ", StringSplitOptions.RemoveEmptyEntries);

                Hand hand = new Hand(splittedInput[0], int.Parse(splittedInput[1]));
                hands.Add(hand);
            }

            return hands;
        }
    }

    public class Hand
    {
        public string cards = "";
        public string type = "";
        public int bid = 0;
        public int rank = 0;

        public Hand(string _cards, int _bid)
        {
            cards = _cards;
            bid = _bid;
            type = GetHandType();
        }

        public int GetHandScore()
        { 
            return bid * rank;
        }

        public int GetCardCount(char card)
        {
            int tmp = 0;
            foreach (char c in cards)
            {
                if (c == card)
                    tmp++;
            }

            return tmp;
        }

        public string GetNewHandType(string initialType)
        {
            string newType = "";

            

            return newType;
        }

        public string GetHandType()
        {
            Dictionary<char, int> cardCount = new Dictionary<char, int>();

            foreach (char card in cards)
            {
                if (!cardCount.ContainsKey(card))
                    cardCount.Add(card, 1);
                else
                    cardCount[card]++;
            }

            if (cardCount.Count() == 1)
                return "FiOAK";
            else if (cardCount.Count() == 2)
            {
                if (cardCount.ContainsValue(4) && cardCount.ContainsValue(1))
                    return "FoOAK";
                else if (cardCount.ContainsValue(3) && cardCount.ContainsValue(2))
                    return "FH";
            }
            else if (cardCount.Count() == 3)
            {
                if (cardCount.ContainsValue(3) && cardCount.ContainsValue(1))
                    return "TOAK";
                else
                    return "TP";
            }
            else if (cardCount.Count() == 4)
                return "OP";
            else
                return "HC";

            return "";
        }
    }

    [TestClass]
    public class Tests
    {
        [TestMethod]
        public void Test()
        {
        }
    }
}
