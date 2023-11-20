using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC
{
    public class Program
    {
        public static void Main()
        {
            string text = Utils.ReadFile("C:\\Prototypes_Perso\\AdventOfCode\\AoC\\Sample.txt");
            string[] textSplitted = text.Split(Environment.NewLine);
            List<Elf> elves = [new Elf(0)];
            int counter = 0;

            for (int i = 0; i < textSplitted.Length; i++)
            {
                if (textSplitted[i] == "")
                {
                    elves.Add(new Elf(0));
                    counter++;
                    Console.WriteLine($"elf {counter - 1} : {elves[counter - 1].calories}");
                }
                else
                {
                    elves[counter].calories += int.Parse(textSplitted[i]);
                }
            }

            Console.WriteLine($"The fatest elf has {GetFatestElf(elves).calories} calogies.");
        }

        public static Elf GetFatestElf(List<Elf> list)
        {
            Elf tmp = new Elf(0);
            foreach (Elf elf in list)
            {
                tmp = tmp.calories < elf.calories ? elf : tmp;
            }
            return tmp;
        }
    }

    public class Elf
    {
        public int calories;

        public Elf(int pCalories) 
        {
            calories = pCalories;
        }
    }
}
