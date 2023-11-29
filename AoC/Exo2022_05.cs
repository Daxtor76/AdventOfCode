using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using NuGet.Frameworks;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace AoC2022_Exo5
{
    /*
    Infos
    */
    public class Program
    {
        // ICI LA LES VARIABLES, PAS EN DESSOUS -_-
        List<List<char>> stacks = new List<List<char>>();
        public static void Main()
        {
            string text = Utils.ReadFile("C:\\Prototypes_Perso\\AdventOfCode\\AoC\\Exo2022_05.txt");
            string[] textSplitted = text.Split(Environment.NewLine);

            foreach (string line in textSplitted)
            {
                string newString = line;
                char leftCrochet = (char)91;
                char rightCrochet = (char)93;
                char nothing = (char)0;

                newString = newString.Replace(leftCrochet, nothing);
                newString = newString.Replace(rightCrochet, nothing);
                Console.WriteLine(newString);
            }
        }
    }

    public class Instruction()
    {
        public int _amount;
        public int _from;
        public int _to;

        public Instruction(int amount, int from, int to) : this()
        {
            _amount = amount;
            _from = from;
            _to = to;
        }
    }
}
