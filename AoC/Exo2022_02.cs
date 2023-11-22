using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace AoC2022_Exo2
{
    /*
    1st column (opponent): A = Rock(1), B = Paper(2), C = Scissors(3)
    2nd column (me): X = Rock(1), Y = Paper(2), Z = Scissors(3)
    Match outcome: Lose(0), Draw(3), Victory(6)
    */
    public class Program
    {
        public static void Main()
        {
            string text = Utils.ReadFile("C:\\Prototypes_Perso\\AdventOfCode\\AoC\\Exo2022_02.txt");
            string[] textSplitted = text.Split(Environment.NewLine);
            Dictionary<string, int> choicesRules = new Dictionary<string, int>
            {
                { "A", 1 },
                { "B", 2 },
                { "C", 3 },
                { "X", 1 },
                { "Y", 2 },
                { "Z", 3 }
            };
            Dictionary<string, int> outcomeRules = new Dictionary<string, int>
            {
                { "lose", 0 },
                { "draw", 3 },
                { "victory", 6 },
            };
            List<Round> rounds = InitRounds(textSplitted);
            int myFinalScore = CalculateFinalScore(choicesRules, outcomeRules, rounds);
        }

        static int CalculateFinalScore(Dictionary<string, int> choicesRules, Dictionary<string, int> outcomeRules, List<Round> rounds)
        {
            int tmpScore = 0;
            foreach (Round round in rounds)
            {
                tmpScore += round.CalculateMyScore(round.opponentChoice, round.myChoice, choicesRules, outcomeRules);
            }
            Console.WriteLine(tmpScore);

            return tmpScore;
        }

        static List<Round> InitRounds(string[] data)
        {
            List<Round> tmpRounds = new List<Round>();
            for (int i = 0; i < data.Length; i++)
            {
                tmpRounds.Add(new Round(data[i].Substring(0, 1), data[i].Substring(data[i].Length - 1, 1)));
            }
            return tmpRounds;
        }
    }

    public class Round
    {
        public string opponentChoice;
        public string myChoice;
        public Round(string p_opponentChoice, string p_myChoice) 
        { 
            opponentChoice = p_opponentChoice;
            myChoice = p_myChoice;
        }

        public int CalculateMyScore(string choiceA, string choiceB, Dictionary<string, int> choicesRules, Dictionary<string, int> outcomeRules)
        {
            //Assert.IsTrue(Regex.IsMatch(choiceA, @"^[A-C]+$"), $"choiceA contains an unexpected character");
            Assert.IsTrue(
                choiceA.All(t => (t.ToString().Contains("A") || t.ToString().Contains("B") || t.ToString().Contains("C"))
                && t.ToString().Length == 1),
                "choiceA contains an unexpected character, too much characters or is empty");
            Assert.IsTrue(
                choiceB.All(t => (t.ToString().Contains("X") || t.ToString().Contains("Y") || t.ToString().Contains("Z"))
                && t.ToString().Length == 1),
                "choiceB contains an unexpected character, too much characters or is empty");

            if (choicesRules[choiceA] > choicesRules[choiceB])
            {
                Console.WriteLine($"LOSE : { choiceA } vs { choiceB } -> you win { outcomeRules["lose"] + choicesRules[choiceB] } points.");
                return outcomeRules["lose"] + choicesRules[choiceB];
            }
            else if (choicesRules[choiceA] == choicesRules[choiceB])
            {
                Console.WriteLine($"DRAW : {choiceA} vs {choiceB} -> you win {outcomeRules["draw"] + choicesRules[choiceB]} points.");
                return outcomeRules["draw"] + choicesRules[choiceB];
            }
            else
            {
                Console.WriteLine($"VICTORY : {choiceA} vs {choiceB} -> you win {outcomeRules["victory"] + choicesRules[choiceB]} points.");
                return outcomeRules["victory"] + choicesRules[choiceB];
            }
        }
    }

    [TestClass]
    public class Test
    {
        [TestMethod]
        public void PuzzleTest()
        {
            Assert.AreEqual(3, int.Parse("3"));
        }
    }
}
