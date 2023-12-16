using AoC2022_Exo1;
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

        public static Dictionary<string, int> hands = new Dictionary<string, int>();
        public static void Main()
        {
            string text = Utils.ReadFile("E:\\Projets\\AdventOfCode\\AdventOfCode\\AoC\\Exo2023_07.txt");
            string[] textSplitted = text.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

            Stopwatch jackeline = Stopwatch.StartNew();

            hands = GetHands(textSplitted);
            foreach (string hand in hands.Keys)
            {
                Console.WriteLine(GetHandType(hand));
            }

            Console.WriteLine(jackeline.ElapsedMilliseconds);
        }

        public static char CompareCards(char card1, char card2)
        {
            if (card1 == card2)
                return 'E';
            else
                return GetCardValue(card1) > GetCardValue(card2) ? card1 : card2;
        }

        public static int GetCardValue(char card)
        {
            return cardsValues.IndexOf(card);
        }

        public static string GetHandType(string hand)
        {
            Dictionary<char, int> cardCount = new Dictionary<char, int>();

            foreach (char card in hand)
            {
                if (!cardCount.ContainsKey(card))
                    cardCount.Add(card, 1);
                else
                    cardCount[card]++;
            }

            if (cardCount.Count() == 1)
                return "FiOAK";
            else if (cardCount.Count() == 2)
                return "FoOAK";
            else if (cardCount.Count() == 3)
            {
                if (cardCount.ContainsValue(3))
                    return "TOAK";
                else
                    return "TP";
            }
            else if (cardCount.Count() == 4)
                return "OP";
            else
                return "HC";
        }

        public static int GetHandTypeValue(string handType)
        {
            return handTypesValues.FindIndex(t => t == handType);
        }

        public static Dictionary<string, int> GetHands(string[] input)
        {
            Dictionary<string, int> hands = new Dictionary<string, int>();

            foreach (string line in input)
            {
                string[] splittedInput = line.Split(" ", StringSplitOptions.RemoveEmptyEntries);
                hands.Add(splittedInput[0], int.Parse(splittedInput[1]));
                //Console.WriteLine($"hand: {splittedInput[0]} - bid: {splittedInput[1]}");
            }

            return hands;
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
