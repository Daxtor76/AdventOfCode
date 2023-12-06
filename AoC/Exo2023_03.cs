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

namespace AoC2023_Exo3
{
    /*
     * On cherche 4361 dans l'exemple
     * 492202 too low
    */
    public class Program
    {
        public static void Main()
        {
            string text = Utils.ReadFile("E:\\Projets\\AdventOfCode\\AdventOfCode\\AoC\\Exo2023_03.txt");
            string[] textSplitted = text.Split(Environment.NewLine);

            Dictionary<Vector2, char> engineScheme = CreateScheme(textSplitted);
            List<Vector2> closedList = new List<Vector2>();
            int gridMaxX = textSplitted[1].Length;
            int gridMaxY = textSplitted.Length;
            int total = 0;

            // Parse chaque ligne
            // Si on tombe sur un digit => check all neighbours (8 directions)
            // Si symbole => check gauche et droite jusqu'à trouver un char != digit
            // Concat et créer un int à garder pour additionner après
            
            for (int i =  0; i < gridMaxX; i++)
            {
                for (int y = 0; y < gridMaxY; y++)
                {
                    Vector2 pos = new Vector2(i, y);
                    if (engineScheme.ContainsKey(pos) && !closedList.Contains(pos) && char.IsDigit(engineScheme[pos]))
                    {
                        if (CheckNeighbours(engineScheme, closedList, pos))
                        {
                            total += GetNumberFromPos(engineScheme, gridMaxY, closedList, pos);
                        }
                    }
                }
            }
            Console.WriteLine($"{total}");
        }

        private static int GetNumberFromPos(Dictionary<Vector2, char> db, int maxY, List<Vector2> closedDB, Vector2 pos)
        {
            string tmp = "";

            // Go left, get all digits from pos
            // Revert string
            // Go right, get all digits from pos
            for (int i = 0; i <= pos.Y; i++)
            {
                Vector2 newPos = new Vector2(pos.X, pos.Y - i);
                if (db.ContainsKey(newPos) && !closedDB.Contains(newPos))
                {
                    if (char.IsDigit(db[newPos]))
                    {
                        tmp += db[newPos];
                        closedDB.Add(newPos);
                    }
                    else
                    {
                        break;
                    }
                }
            }
            Console.WriteLine($"With left numbers: {tmp}");
            char[] tmpArray = tmp.ToCharArray();
            Array.Reverse(tmpArray);
            string reversedTmp = new string(tmpArray);
            Console.WriteLine($"With right numbers: {reversedTmp}");
            for (int y = 0; y < maxY - pos.Y; y++)
            {
                Vector2 newPos = new Vector2(pos.X, pos.Y + y);
                if (db.ContainsKey(newPos) && !closedDB.Contains(newPos))
                {
                    if (char.IsDigit(db[newPos]))
                    {
                        reversedTmp += db[newPos];
                        closedDB.Add(newPos);
                    }
                    else
                    {
                        break;
                    }
                }
            }

            Console.WriteLine($"number to add: {reversedTmp}");

            return int.Parse(reversedTmp);
        }

        public static bool CheckNeighbours(Dictionary<Vector2, char> db, List<Vector2> closedDB, Vector2 pos)
        {
            Vector2 right = pos + new Vector2(0, 1);
            Vector2 downRight = pos + new Vector2(-1, 1);
            Vector2 down = pos + new Vector2(-1, 0);
            Vector2 downLeft = pos + new Vector2(-1, -1);
            Vector2 left = pos + new Vector2(0, -1);
            Vector2 upLeft = pos + new Vector2(1, -1);
            Vector2 up = pos + new Vector2(1, 0);
            Vector2 upRight = pos + new Vector2(1, 1);

            if (db.ContainsKey(right) && IsAuthorizedSymbole(db[right]) && !closedDB.Contains(right))
            {
                return true;
            }
            if (db.ContainsKey(downRight) && IsAuthorizedSymbole(db[downRight]) && !closedDB.Contains(downRight))
            {
                return true;
            }
            if (db.ContainsKey(down) && IsAuthorizedSymbole(db[down]) && !closedDB.Contains(down))
            {
                return true;
            }
            if (db.ContainsKey(downLeft) && IsAuthorizedSymbole(db[downLeft]) && !closedDB.Contains(downLeft))
            {
                return true;
            }
            if (db.ContainsKey(left) && IsAuthorizedSymbole(db[left]) && !closedDB.Contains(left))
            {
                return true;
            }
            if (db.ContainsKey(upLeft) && IsAuthorizedSymbole(db[upLeft]) && !closedDB.Contains(upLeft))
            {
                return true;
            }
            if (db.ContainsKey(up) && IsAuthorizedSymbole(db[up]) && !closedDB.Contains(up))
            {
                return true;
            }
            if (db.ContainsKey(upRight) && IsAuthorizedSymbole(db[upRight]) && !closedDB.Contains(upRight))
            {
                return true;
            }

            return false;
        }

        public static bool IsAuthorizedSymbole(char c)
        {
            return c != 46 && !char.IsDigit(c);
        }

        public static Dictionary<Vector2, char> CreateScheme(string[] textSplitted)
        {
            Dictionary<Vector2, char> tmpScheme = new Dictionary<Vector2, char>();

            Console.Write("Current scheme:\n");
            for (int i = 0; i < textSplitted.Length; i++)
            {
                for (int y = 0; y < textSplitted[i].Length; y++)
                {
                    tmpScheme.Add(new Vector2(i, y), textSplitted[i][y]);
                    Console.Write(tmpScheme[new Vector2(i, y)]);
                }
                Console.Write("\n");
            }

            return tmpScheme;
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
