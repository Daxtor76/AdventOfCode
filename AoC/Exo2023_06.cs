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
using System.Runtime.Serialization.Formatters;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AoC2023_Exo6
{
    /*
    */
    public class Program
    {
        public static List<Race> races = new List<Race>();
        public static Boat boat = new Boat();
        public static void Main()
        {
            string text = Utils.ReadFile("C:\\Prototypes_Perso\\AdventOfCode\\AoC\\Exo2023_06.txt");
            string[] textSplitted = text.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            string[] durations = textSplitted[0].Split(": ", StringSplitOptions.RemoveEmptyEntries);
            string[] records = textSplitted[1].Split(": ", StringSplitOptions.RemoveEmptyEntries);
            Stopwatch jackeline = Stopwatch.StartNew();

            // STEP 1
            //races = GetRaces(durations, records);
            //Console.WriteLine($"Winning strategies value: {GetFinalValue(races, boat)}");

            // STEP 2
            races = GetBigRace(durations, records);
            Console.WriteLine($"Winning strategies amount: {races[0].GetWinningStrategiesAmount(boat)}");
            Console.WriteLine(jackeline.ElapsedMilliseconds);
        }

        public static long GetFinalValue(List<Race> races, Boat boat)
        {
            long value = 1;
            foreach (Race race in races)
            {
                value *= race.GetWinningStrategiesAmount(boat);
            }

            return value;
        }

        public static List<Race> GetBigRace(string[] durations, string[] records)
        {
            string duration = string.Concat(durations[1].Split(" ", StringSplitOptions.RemoveEmptyEntries));
            string record = string.Concat(records[1].Split(" ", StringSplitOptions.RemoveEmptyEntries));
            List<Race> result = new List<Race>();

            Race race = new Race(long.Parse(duration), long.Parse(record));
            //Console.WriteLine($"New race! duration: {race.duration}, record: {race.record}");
            result.Add(race);

            return result;
        }

        public static List<Race> GetRaces(string[] durations, string[] records)
        {
            string[] durationsByRace = durations[1].Split(" ", StringSplitOptions.RemoveEmptyEntries);
            string[] recordsByRace = records[1].Split(" ", StringSplitOptions.RemoveEmptyEntries);
            List<Race> result = new List<Race>();

            for (long i = 0; i < durationsByRace.Length; i++)
            {
                Race race = new Race(long.Parse(durationsByRace[i]), long.Parse(recordsByRace[i]));
                //Console.WriteLine($"New race! duration: {race.duration}, record: {race.record}");
                result.Add(race);
            }

            return result;
        }
    }

    public class Boat
    {
        // mm/ms
        public long speed = 0;
        public Boat() 
        {
        }

        public long GiveSpeedToBoat(long inputDuration)
        {
            speed = inputDuration;
            //Console.WriteLine($"Boat speed changed to {speed}");
            return speed;
        }

        public long GetTravelDistance(Race race, long boatSpeed)
        {
            long moveDuration = race.duration - GiveSpeedToBoat(boatSpeed);

            return moveDuration * boatSpeed;
        }
    }

    public class Race
    {
        public long duration = 0;
        public long record = 0;

        public Race(long _duration, long _record)
        {
            duration = _duration;
            record = _record;
        }

        public long GetWinningStrategiesAmount(Boat boat)
        {
            long winStratValue = 0;
            for (int i = 0; i <= duration; i++)
            {
                long travelDist = boat.GetTravelDistance(this, i);
                winStratValue += travelDist > record ? 1 : 0;
                //Console.WriteLine($"Travel dist: {travelDist}, record: {record} => {winStratValue} ways to win");
            };

            return winStratValue;
        }
    }

    [TestClass]
    public class Tests
    {
        Race testRace = new Race(7, 9);
        Boat testBoat = new Boat();
        [TestMethod]
        public void TestDistanceAndSpeed()
        {
            Assert.AreEqual(0, testBoat.GetTravelDistance(testRace, 0));
            Assert.AreEqual(6, testBoat.GetTravelDistance(testRace, 1));
            Assert.AreEqual(10, testBoat.GetTravelDistance(testRace, 2));
            Assert.AreEqual(12, testBoat.GetTravelDistance(testRace, 3));
            Assert.AreEqual(12, testBoat.GetTravelDistance(testRace, 4));
            Assert.AreEqual(10, testBoat.GetTravelDistance(testRace, 5));
            Assert.AreEqual(6, testBoat.GetTravelDistance(testRace, 6));
            Assert.AreEqual(0,testBoat.GetTravelDistance(testRace, 7));
        }
    }
}
