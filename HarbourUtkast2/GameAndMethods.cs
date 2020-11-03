using System;
using System.Threading;
using System.IO;
using System.Linq;

namespace HarbourUtkast2
{
    class GameAndMethods
    {
        public static void Run()
        {
            bool isRunning = true;
            bool newLoad = true;
            Harbour.EmptyHarbour();
            do
            {
                Console.WriteLine("Välkommen till hamnen!\n\n[1] Ny simulering\n[2] Ladda senaste simulering");
                Console.Write("Välj: ");
                ConsoleKey newOrLoad = Console.ReadKey().Key;
                Console.Clear();
                switch (newOrLoad)
                {
                    case ConsoleKey.D1:
                        Console.WriteLine("Startar från dag 0!");
                        newLoad = false;
                        Thread.Sleep(2000);
                        break;
                    case ConsoleKey.D2:
                        Harbour.LoadData();
                        newLoad = false;
                        break;
                    default:
                        Console.WriteLine("Vänligen välj [1] eller [2]!");
                        Thread.Sleep(1000);
                        break;
                }

            } while (newLoad);
            while (isRunning)
            {
                newLoad = false;
                Console.Clear();
                Harbour.WriteOutHarbour();
                ConsoleKey keyChoice = Console.ReadKey().Key;
                Console.Clear();
                switch (keyChoice)
                {
                    case ConsoleKey.Enter:
                        break;
                    case ConsoleKey.D1:
                        newLoad = true;
                        break;
                    case ConsoleKey.D2:
                        isRunning = false;
                        Console.WriteLine("Avslutar!");
                        Thread.Sleep(1000);
                        break;
                    default:
                        break;
                }
                if (newLoad)
                {
                    StartOver();
                    Console.WriteLine("Startar om från dag 0!");
                    Thread.Sleep(2000);
                }
                else if (isRunning == false)
                {
                    Harbour.SaveData();
                }
                else
                {
                    Harbour.DecreaseCounter();
                    Boat.GeneratingNewBoats();
                    Harbour.SaveData();
                }
            }
        }
        private static void StartOver()
        {
            File.WriteAllText("savedData.txt", string.Empty);
            Harbour.EmptyHarbour();
            Harbour.daysPassed = 0;
        }
        public static decimal FeetToMetres(decimal length)
        {
            decimal feet = 0.3048M;
            decimal metre = length * feet;
            return Math.Round(metre, 1);
        }
        public static int AvarageTopSpeed()
        {
            int harbourSpeed = 0;
            int counter = 0;
            var q4 = Harbour.harbour
                .Where(h => h.Weight > 0)
                .Select(h => h.TopSpeed);
            foreach (var item in q4)
            {
                harbourSpeed += item;
                counter++;
            }
            var q5 = Harbour.secondaryHarbour
                .Where(s => s.Weight > 0)
                .Select(s => s.TopSpeed);
            foreach (var item in q5)
            {
                harbourSpeed += item;
                counter++;
            }
            if (counter == 0)
            {
                return 0;
            }
            else
            {
                int value = harbourSpeed / counter;
                return value;
            }
        }
        public static int WeightCheck()
        {
            int harbourWeight = 0;
            var q2 = Harbour.harbour
                .Where(h => h.Weight > 0)
                .Select(h => h.Weight);
            foreach (var item in q2)
            {
                harbourWeight += item;
            }
            var q3 = Harbour.secondaryHarbour
                .Where(s => s.Weight > 0)
                .Select(s => s.Weight);
            foreach (var item in q3)
            {
                harbourWeight += item;
            }
            return harbourWeight;
        }
        public static decimal KnotToKMH(decimal knot)
        {
            decimal knotToKMH = 1.852M;
            decimal kMH = knot * knotToKMH;
            return Math.Round(kMH, 1);
        }
    }
}

