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
using System.Xml;
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AoC2023_Exo7
{
    /*
     * 255260935 too high
     * 255048101 ok !
     * 
     * 253680676 too low
     * 253718286 ok !
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
            hands = OrderHands(GetHands(textSplitted), cardsValuesStep2);
            foreach (Hand hand in hands)
            {
                total += hand.GetHandScore();
                Console.WriteLine($"hand: {hand.cards} - bid: {hand.bid} - type: {hand.type} - value: {GetHandTypeValue(hand)} - rank: {hand.rank} - score: {hand.GetHandScore()}");
            }
            Console.WriteLine($"{total}");

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
                hand.type = hand.GetConvertedHandType(hand.GetHandType(false), hand.GetCardCount('J'));
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

        private Dictionary<char, int> CountCards(bool withJokers)
        {
            Dictionary<char, int> cardCount = new Dictionary<char, int>();
            foreach (char card in cards)
            {
                if (!withJokers && card == 'J')
                    continue;
                else if (!cardCount.ContainsKey(card))
                    cardCount.Add(card, 1);
                else
                    cardCount[card]++;
            }
            return cardCount;
        }

        public string GetConvertedHandType(string type, int jokersAmount)
        {
            string newType = type;

            for (int i = 0; i < jokersAmount; i++)
            {
                if (newType == "HC")
                    newType = "OP";
                else if (newType == "OP")
                {
                    // TOAK ou TP ? forcément TOAK car plus fort que TP
                    newType = "TOAK";
                }
                else if (newType == "TP")
                    newType = "FH";
                else if (newType == "FH")
                    continue;
                else if (newType == "TOAK")
                    newType = "FoOAK";
                else if (newType == "FoOAK")
                    newType = "FiOAK";
                else if (newType == "FiOAK")
                    continue;
            }

            return newType;
        }

        public string GetHandType(bool withJokers)
        {
            Dictionary<char, int> cardCount = CountCards(withJokers);

            if (cardCount.ContainsValue(5))
                return "FiOAK";

            if (cardCount.ContainsValue(4))
                return "FoOAK";

            if (cardCount.ContainsValue(3) && cardCount.ContainsValue(2))
                return "FH";

            if (cardCount.ContainsValue(3) && !cardCount.ContainsValue(2))
                return "TOAK";

            if (cardCount.Where((x) => x.Value == 2).Count() == 2)
                return "TP";

            if (cardCount.Where((x) => x.Value == 2).Count() == 1)
                return "OP";

            return "HC";
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
