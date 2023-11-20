using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

internal class Program
{
    public static void Main(string[] args)
    {
        string path = "C:\\Prototypes_Perso\\AdventOfCode\\AoC\\Sample.txt";

        string fileContent = File.ReadAllText(path)
                    .TrimEnd('\n'); //Remove the last \n that is usually outside the scope of the puzzle

        Console.WriteLine(fileContent);
    }
}

[TestClass]
public class Test
{
    [TestMethod]
    public void PuzzleTest()
    {
        Assert.AreEqual(3, int.Parse("3"));
        string s = "boujour";
        string[] stableau = s.Split("j"); // retourne "bou" et "our"
    }
}