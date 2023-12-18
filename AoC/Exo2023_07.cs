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
using System.Net;
using System.Net.WebSockets;
using System.Numerics;
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
            'A'
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

            int test = 0;
            hands = OrderHands(GetHands(textSplitted));
            foreach (Hand hand in hands)
            {
                test += hand.GetHandScore();
                //Console.WriteLine($"hand: {hand.cards} - bid: {hand.bid} - type: {hand.type} - value: {GetHandTypeValue(hand)} - rank: {hand.rank} - score: {hand.GetHandScore()}");
                Console.WriteLine($"cards value: {GetCardsValue(hand)}");
            }
            Console.WriteLine($"{test}");

            Console.WriteLine(jackeline.ElapsedMilliseconds);
        }

        public static List<Hand> OrderHands(List<Hand> hands)
        {
            //List<Hand> tmp = hands.OrderBy(v => GetHandTypeValue(v)).ThenBy(b => GetCardsValue(b)).ToList();
            List<Hand> tmp = hands.OrderBy(GetHandTypeValue).ToList().Sort();

            foreach (Hand hand in tmp)
            {
                hand.rank = tmp.IndexOf(hand) + 1;
            }

            return tmp;
        }

        public static int GetCardsValue(Hand hand)
        {
            string tmp = "";

            foreach (char c in hand.cards)
            {
                tmp += cardsValues.FindIndex(t => t == c);
            }

            return int.Parse(tmp);
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

    public class Hand : IComparable
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

        public int CompareTo(object obj)
        {
            string otherCards = obj as string;
            if (otherCards != null)
            {
                for (int i = 0; i < cards.Length; i++)
                {
                    if (cards[i] == otherCards[i])
                        continue;

                    return Program.cardsValues.FindIndex(c => c == cards[i]).CompareTo(Program.cardsValues.FindIndex(t => t == otherCards[i]));
                }
            }
            else
                throw new ArgumentException("Other cards is empty");
        }

        public int GetHandScore()
        { 
            return bid * rank;
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
                else
                    return "TOAK";
            }
            else if (cardCount.Count() == 3)
                return "TP";
            else if (cardCount.Count() == 4)
                return "OP";
            else
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
