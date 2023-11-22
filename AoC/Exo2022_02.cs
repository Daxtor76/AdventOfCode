using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        public static Dictionary<string, int> choicesRules = new Dictionary<string, int>
        {
            { "A", 1 },
            { "B", 2 },
            { "C", 3 },
            { "X", 1 },
            { "Y", 2 },
            { "Z", 3 }
        };
        public static Dictionary<string, int> outcomeRules = new Dictionary<string, int>
        {
            { "lose", 0 },
            { "draw", 3 },
            { "victory", 6 }
        };
        public static void Main()
        {
            string text = Utils.ReadFile("C:\\Prototypes_Perso\\AdventOfCode\\AoC\\Exo2022_02.txt");
            string[] textSplitted = text.Split(Environment.NewLine);

            List<Round> rounds = InitRounds(textSplitted);

            int myFinalScore = CalculateFinalScore(choicesRules, outcomeRules, rounds);
        }

        public static int CalculateFinalScore(Dictionary<string, int> choicesRules, Dictionary<string, int> outcomeRules, List<Round> rounds)
        {
            int tmpScore = 0;
            foreach (Round round in rounds)
            {
                int roundScore = round.CalculateMyScore(choicesRules, outcomeRules);
                tmpScore += roundScore;
                Console.WriteLine($"+{roundScore}. New total: {tmpScore}");
            }

            return tmpScore;
        }

        public static List<Round> InitRounds(string[] data)
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

        public int CalculateMyScore(Dictionary<string, int> choicesRules, Dictionary<string, int> outcomeRules)
        {
            //Assert.IsTrue(Regex.IsMatch(choiceA, @"^[A-C]+$"), $"choiceA contains an unexpected character");
            Assert.IsTrue(
                opponentChoice.All(t => (t.ToString().Contains("A") || t.ToString().Contains("B") || t.ToString().Contains("C"))
                && t.ToString().Length == 1),
                "choiceA contains an unexpected character, too much characters or is empty");
            Assert.IsTrue(
                myChoice.All(t => (t.ToString().Contains("X") || t.ToString().Contains("Y") || t.ToString().Contains("Z"))
                && t.ToString().Length == 1),
                "choiceB contains an unexpected character, too much characters or is empty");

            if (choicesRules[opponentChoice] > choicesRules[myChoice])
            {
                //Console.WriteLine($"LOSE : {opponentChoice} vs {myChoice} -> you win { outcomeRules["lose"] + choicesRules[myChoice] } points.");
                return outcomeRules["lose"] + choicesRules[myChoice];
            }
            else if (choicesRules[opponentChoice] == choicesRules[myChoice])
            {
                //Console.WriteLine($"DRAW : {opponentChoice} vs {myChoice} -> you win {outcomeRules["draw"] + choicesRules[myChoice]} points.");
                return outcomeRules["draw"] + choicesRules[myChoice];
            }
            else
            {
                //Console.WriteLine($"VICTORY : {opponentChoice} vs {myChoice} -> you win {outcomeRules["victory"] + choicesRules[myChoice]} points.");
                return outcomeRules["victory"] + choicesRules[myChoice];
            }
        }
    }

    [TestClass]
    public class Tests
    {
        public List<Round> rounds = new List<Round>();
        [TestMethod]
        public void TestInitRound()
        {
            string[] entry = new string[]
            {
                "A Y",
                "B Z"
            };
            rounds = Program.InitRounds(entry);
            Assert.AreEqual(rounds.Count(), 2);
        }
        [TestMethod]
        public void TestVictory()
        {
            // A Y => I win with 8pts
            rounds.Add(new Round("A", "Y"));
            Assert.AreEqual(rounds[0].CalculateMyScore(Program.choicesRules, Program.outcomeRules), 8);
        }
        [TestMethod]
        public void TestLose()
        {
            // B X => I lose with 1pt
            rounds.Add(new Round("B", "X"));
            Assert.AreEqual(rounds[0].CalculateMyScore(Program.choicesRules, Program.outcomeRules), 1);
        }
        [TestMethod]
        public void TestDraw()
        {
            // C Z => draw with 6pts
            rounds.Add(new Round("C", "Z"));
            Assert.AreEqual(rounds[0].CalculateMyScore(Program.choicesRules, Program.outcomeRules), 6);
        }
        [TestMethod]
        public void TestGlobalScore()
        {
            // All previous scores added, total 15
            rounds.Add(new Round("A", "Y"));
            rounds.Add(new Round("B", "X"));
            rounds.Add(new Round("C", "Z"));
            Assert.AreEqual(Program.CalculateFinalScore(Program.choicesRules, Program.outcomeRules, rounds), 15);
        }
    }
}
