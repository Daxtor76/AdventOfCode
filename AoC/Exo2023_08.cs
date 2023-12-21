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

namespace AoC2023_Exo8
{
    /*
    */
    public class Program
    {
        public static void Main()
        {
            string text = Utils.ReadFile("C:\\Prototypes_Perso\\AdventOfCode\\AoC\\Exo2023_08.txt");
            string[] textSplitted = text.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

            Stopwatch jackeline = Stopwatch.StartNew();

            // STEP 1

            Console.WriteLine($"Time elapsed: {jackeline.ElapsedMilliseconds}");
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
