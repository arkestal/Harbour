using System;

namespace Harbour
{
    class Program
    {
        public static int vesselsPerDay = 5;
        public static int x = 8;
        public static int y = 2;
        public static Boat[,] harbour = new Boat[x, y];
        public static Random random = new Random();
        public static DummyBoat dummy = new DummyBoat("", 0, 0, tokenSign:" ");
        public static RowingBoat rowingBoat;
        public static MotorBoat motorBoat;
        public static SailingBoat sailingBoat;
        public static CargoShip cargoShip;
        static void Main(string[] args)
        {
            EmptyHarbour();
            for (int i = 0; i < vesselsPerDay; i++)
            {
                int approachingVessel = random.Next(1, 4 + 1);
                switch (approachingVessel)
                {
                    case 1:
                        rowingBoat = new RowingBoat($"R-{GetID()}", GetValue(100, 300), GetValue(0, 3), tokenSign:"R", 1, GetValue(1, 6));
                        if (harbour[x - 1, 0] == dummy)
                        {
                            harbour[x - 1, 0] = rowingBoat;
                        }
                        else if (harbour[x - 1, y - 1] == dummy)
                        {
                            harbour[x - 1, y - 1] = rowingBoat;
                        }
                        else if (harbour[x - 2, 0] == dummy)
                        {
                            harbour[x - 2, 0] = rowingBoat;
                        }
                        else if (harbour[x - 2, y - 1] == dummy)
                        {
                            harbour[x - 2, y - 1] = rowingBoat;
                        }
                        else if (harbour[x - 3, 0] == dummy)
                        {
                            harbour[x - 3, 0] = rowingBoat;
                        }
                        else if (harbour[x - 1, 0] != dummy || harbour[x - 1, y - 1] != dummy || harbour[x - 2, 0] != dummy || harbour[x - 2, y - 1] != dummy || harbour[x - 3, 0] != dummy)
                        {
                            int index = Array.IndexOf(harbour, dummy);
                            harbour[index, 0] = rowingBoat;
                        }
                        break;
                    case 2:
                        motorBoat = new MotorBoat($"M-{GetID()}", GetValue(200, 3000), GetValue(0, 60), tokenSign:"M", 3, GetValue(10, 1000));
                        break;
                    case 3:
                        sailingBoat = new SailingBoat($"S-{GetID()}", GetValue(800, 6000), GetValue(0, 12), tokenSign:"S", 4, GetValue(10, 60));
                        break;
                    case 4:
                        cargoShip = new CargoShip($"L-{GetID()}", GetValue(3000, 20000), GetValue(0, 20), tokenSign:"L", 6, GetValue(0, 500));
                        break;
                }
            }
            Console.Clear();
            WriteOutHarbour();
            Console.ReadLine();
        }

        private static int GetValue(int a, int b)
        {
            int value = random.Next(a, b + 1);
            return value;
        }

        private static void EmptyHarbour()
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
                    harbour[j, i] = dummy;
                    if (harbour[j, i] == dummy)
                    {
                        Console.Write(dummy.TokenSign);

                    }
                    Console.Write(" |");
                }
                Console.WriteLine();
            }
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
                    if (harbour[j, i] is RowingBoat)
                    {
                        Console.Write(rowingBoat.TokenSign);
                    }
                    else if (harbour[j, i] is MotorBoat)
                    {
                        Console.Write(motorBoat.TokenSign);
                    }
                    else if (harbour[j, i] is SailingBoat)
                    {
                        Console.Write(sailingBoat.TokenSign);
                    }
                    else if (harbour[j, i] is CargoShip)
                    {
                        Console.Write(cargoShip.TokenSign);
                    }
                    else
                    {
                        Console.Write(dummy.TokenSign);
                    }
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
                int number = random.Next(0, 25 + 1);
                char letter = (char)('a' + number);
                ID += letter;
            }
            return ID.ToUpper();
        }
    }
}
