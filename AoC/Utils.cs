using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AoC
{
    public static class Utils
    {
        public static string ReadFile(string path)
        {
            string fileContent = File.ReadAllText(path)
                        .TrimEnd('\n'); //Remove the last \n that is usually outside the scope of the puzzle

            return fileContent;
            //Console.WriteLine(fileContent);
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