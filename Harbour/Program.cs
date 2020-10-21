using System;

namespace Harbour
{
    class Program
    {
        public static int vesselsPerDay = 5;
        public static int x = 64;
        public static int y = 2;
        public static char[,] harbour = new char[x, y];
        public static Random random = new Random();
        static void Main(string[] args)
        {
            WriteOutHarbour();
            for (int i = 0; i < vesselsPerDay; i++)
            {
                int approachingVessel = random.Next(1, 4 + 1);
                switch (approachingVessel)
                {
                    case 1:
                        RowingBoat rowingBoat = new RowingBoat($"R-{GetID()}", GetWeight(100, 300), GetSpeed(0, 3), SpecialStuff(1, 6));
                        break;
                    case 2:
                        MotorBoat motorBoat = new MotorBoat($"M-{GetID()}", GetWeight(200, 3000), GetSpeed(0, 60), SpecialStuff(10, 1000));
                        break;
                    case 3:
                        SailingBoat sailingBoat = new SailingBoat($"S-{GetID()}", GetWeight(800, 6000), GetSpeed(0, 12), SpecialStuff(10, 60));
                        break;
                    case 4:
                        CargoShip cargoShip = new CargoShip($"L-{GetID()}", GetWeight(3000, 20000), GetSpeed(0, 20), SpecialStuff(0, 500));
                        break;
                }
            }
            Console.ReadLine();
        }

        private static int SpecialStuff(int a, int b)
        {
            int numberOfPassengers = random.Next(a, b + 1);
            return numberOfPassengers;
        }

        private static int GetSpeed(int a, int b)
        {
            int topSpeed = random.Next(a, b + 1);
            return topSpeed;
        }

        private static int GetWeight(int a, int b)
        {
            int weight = random.Next(a, b + 1);
            return weight;
        }

        private static void WriteOutHarbour()
        {
            int number = 1;
            for (int i = 0; i < x; i++)
            {
                Console.Write("|");
                if (number < 10)
                {
                    Console.Write($" {number}");
                }
                else
                {
                    Console.Write(number);
                }
                number++;
            }
            Console.WriteLine("|");
            for (int i = 0; i < y; i++)
            {
                Console.Write("|");
                for (int j = 0; j < x; j++)
                {
                    harbour[j, i] = ' ';
                    Console.Write(harbour[j, i]);
                    Console.Write(" |");
                }
                Console.WriteLine();
            }
        }
        private static string GetID()
        {
            string ID = "";
            for (int i = 0; i < 3; i++)
            {
                int num = random.Next(0, 25 + 1);
                char let = (char)('a' + num);
                ID += let;
            }
            return ID.ToUpper();
        }
    }
}
