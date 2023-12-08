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
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AoC2023_Exo5
{
    /*
    */
    public class Program
    {
        public static void Main()
        {
            string text = Utils.ReadFile("E:\\Projets\\AdventOfCode\\AdventOfCode\\AoC\\Exo2023_05.txt");
            string[] textSplitted = text.Split(Environment.NewLine);
            string[] separators = new string[]
            {
                ":",
                "|",
            };

            // STEP 1

            // STEP 2
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
