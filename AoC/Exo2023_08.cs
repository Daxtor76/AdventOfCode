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
using System.Security.AccessControl;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AoC2023_Exo8
{
    /*
    */
    public class Program
    {
        public static string[] separators = new string[]
        { 
            "=",
            ","
        };
        public static string instructions = "";
        public static Dictionary<string, Node> nodes = new Dictionary<string, Node>();

        public static string currentNodeId = null;
        public static List<string> currentNodes = new List<string>();
        public static List<long> stepsAmounts = new List<long>();
        public static void Main()
        {
            string text = Utils.ReadFile("E:\\Projets\\AdventOfCode\\AdventOfCode\\AoC\\Exo2023_08.txt");
            string[] textSplitted = text.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            instructions = textSplitted[0];
            nodes = GetNodes(textSplitted);
            int stepsCount = 0;
            int iterator = 0;

            Stopwatch jackeline = Stopwatch.StartNew();

            // STEP 1
            /*currentNode = nodes.Where(x => x.id == "AAA").ToList()[0];
            while(currentNode.id != "ZZZ")
            {
                currentNode = nodes.Where(x => x.id == currentNode.GetNextNodeId(instructions[iterator])).ToList()[0];
                stepsCount++;
                iterator++;

                if (iterator > instructions.Length - 1)
                    iterator = 0;
            }*/

            // STEP 2
            currentNodes = GetStartingNodes(nodes);
            for (int i = 0; i < currentNodes.Count; i++)
            {
                stepsCount = 0;
                while (currentNodes[i].Last() != 'Z')
                {
                    Node node = nodes[currentNodes[i]];
                    currentNodes[i] = node.GetNextNodeId(instructions[iterator]);

                    iterator++;
                    stepsCount++;

                    if (iterator > instructions.Length - 1)
                        iterator = 0;
                }
                stepsAmounts.Add(stepsCount);
            }

            var lcm = stepsAmounts[0];
            for (int y = 1; y < stepsAmounts.Count; y++)
            {
                lcm = LowestCommonMultiple(lcm, stepsAmounts[y]);
            }
            Console.WriteLine($"Required Steps: {lcm}");

            Console.WriteLine($"Time elapsed: {jackeline.ElapsedMilliseconds}");
        }

        public static long LowestCommonMultiple(long a, long b)
        {
            return a * b / GreatestCommonDivisor(a, b);
        }

        public static long GreatestCommonDivisor(long a, long b)
        {
            while (b != 0)
            {
                var t = b;
                b = a % b;
                a = t;
            }

            return a;
        }

        public static bool AreAllEndNodes(List<string> nodes)
        {
            return nodes.Where(x => x.Last() != 'Z').ToArray().Count() == 0;
        }

        public static List<string> GetStartingNodes(Dictionary<string, Node> nodes)
        {
            List<string> result = new List<string>();

            foreach (Node node in nodes.Values)
            {
                if (node.id.Last() == 'A')
                    result.Add(node.id);
            }

            return result;
        }

        public static Dictionary<string, Node> GetNodes(string[] input)
        {
            Dictionary<string, Node> tmp = new Dictionary<string, Node>();

            for (int i = 1; i < input.Length; i++)
            {
                string[] splittedInput = input[i].Split("=", StringSplitOptions.TrimEntries);
                string[] directions = splittedInput[1].Split(",", StringSplitOptions.TrimEntries);

                Node node = new Node(
                    splittedInput[0], 
                    directions[0].Remove(0, 1), 
                    directions[1].Remove(directions[1].Length - 1, 1));

                tmp.Add(node.id, node);

                //Console.WriteLine($"New node: id: {node.id} left: {node.left} right: {node.right}");
            }

            return tmp;
        }
    }

    public class Node
    {
        public string id = "";
        public string left = "";
        public string right = "";

        public Node(string _id, string _left, string _right)
        {
            id = _id;
            left = _left;
            right = _right;
        }
        public string GetNextNodeId(char instruction)
        {
            return instruction == 'L' ? left : right;
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
