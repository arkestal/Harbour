using System;

namespace HarbourUtkast2
{
    class Program
    {
        public static int vesselsPerDay = 5;
        public static int x = 64;
        public static int y = 64;
        public static int approachingVessel;
        public static int placementIndex;
        public static Boat[] harbour = new Boat[x];
        public static Boat[] secondaryHarbour = new Boat[y];
        public static Random random = new Random();
        public static DummyBoat dummy = new DummyBoat("", "", 0, 0, tokenSign: " ");
        public static RowingBoat rowingBoat;
        public static MotorBoat motorBoat;
        public static SailingBoat sailingBoat;
        public static CargoShip cargoShip;
        static void Main(string[] args)
        {
            bool isRunning = true;
            EmptyHarbour();
            while (isRunning)
            {
                NewDayHappenings();
                Console.Clear();
                WriteOutHarbour();
                placementIndex = 0;
                string input = Console.ReadLine();
                if (input == "q")
                {
                    isRunning = false;
                }
            }
        }

        private static void NewDayHappenings()
        {
            for (int i = 0; i < vesselsPerDay; i++)
            {
                approachingVessel = random.Next(1, 4 + 1);
                switch (3)
                {
                    case 1:
                        rowingBoat = new RowingBoat($"Roddbåt\t", $"R-{GetID()}", GetValue(100, 300), GetValue(0, 3), tokenSign: "R", 1, GetValue(1, 6));
                        if (harbour[x - 1] == dummy)
                        {
                            harbour[x - 1] = rowingBoat;
                        }
                        else if (secondaryHarbour[y - 1] == dummy)
                        {
                            secondaryHarbour[y - 1] = rowingBoat;
                        }
                        else if (harbour[x - 2] == dummy)
                        {
                            harbour[x - 2] = rowingBoat;
                        }
                        else if (secondaryHarbour[y - 2] == dummy)
                        {
                            secondaryHarbour[y - 2] = rowingBoat;
                        }
                        else if (harbour[x - 3] == dummy)
                        {
                            harbour[x - 3] = rowingBoat;
                        }
                        else if (harbour[x - 1] != dummy || harbour[x - 2] != dummy || harbour[x - 3] != dummy || secondaryHarbour[y - 1] != dummy || secondaryHarbour[y - 2] != dummy)
                        {
                            PlaceVessel(1);
                        }
                        break;
                    case 2:
                        motorBoat = new MotorBoat($"Motorbåt", $"M-{GetID()}", GetValue(200, 3000), GetValue(0, 60), tokenSign: "M", 3, GetValue(10, 1000));
                        PlaceVessel(1);
                        break;
                    case 3:
                        sailingBoat = new SailingBoat($"Segelbåt", $"S-{GetID()}", GetValue(800, 6000), GetValue(0, 12), tokenSign: "S", 4, GetValue(10, 60));
                        PlaceVessel(2);
                        break;
                    case 4:
                        cargoShip = new CargoShip($"Lastfartyg", $"L-{GetID()}", GetValue(3000, 20000), GetValue(0, 20), tokenSign: "L", 6, GetValue(0, 500));
                        PlaceVessel(4);
                        break;
                }
            }
        }

        private static Boat DecreaseCounter(Boat boat)
        {
            Boat[] temp = harbour.Clone() as Boat[];

            int index = 0;
            //int index2 = 0;
            foreach (var slot in harbour)
            {
                slot.Counter--;
                if (slot.Counter < 0)
                {
                    slot.Counter = 0;
                }
                if (slot.Counter == 0 && !(slot is DummyBoat))
                {

                    temp[index] = dummy;
                }
                index++;
                if (index > x)// && index2 == 0)
                {
                    index = 0;
                    //index2++;
                }
            }
            harbour = temp;
            return boat;
        }

        private static void PlaceVessel(int value)
        {
            int emptyslot = 0;
            foreach (var element in harbour)
            {
                if (harbour[placementIndex] is DummyBoat || harbour[placementIndex] is RowingBoat)
                {
                    emptyslot++;
                    if (emptyslot == value)
                    {
                        if (value == 1 && approachingVessel == 1)
                        {
                            if (harbour[placementIndex] is RowingBoat)
                            {
                                secondaryHarbour[placementIndex] = rowingBoat;
                            }
                            else
                            {
                                harbour[placementIndex] = rowingBoat;
                            }
                        }
                        else if (value == 1 && approachingVessel == 2 && !(harbour[placementIndex] is RowingBoat))
                        {
                            harbour[placementIndex] = motorBoat;
                        }
                        else if (value == 2)
                        {
                            harbour[placementIndex - 1] = sailingBoat;
                            harbour[placementIndex] = sailingBoat;
                        }
                        else if (value == 4)
                        {
                            harbour[placementIndex - 3] = cargoShip;
                            harbour[placementIndex - 2] = cargoShip;
                            harbour[placementIndex - 1] = cargoShip;
                            harbour[placementIndex] = cargoShip;
                        }
                        break;
                    }

                }
                //else if (harbour[placementIndex, 0] is RowingBoat && value == 1)
                //{
                //    harbour[placementIndex, 1] = rowingBoat;
                //    placementIndex++;
                //    break;
                //}
                else if (harbour[placementIndex] is RowingBoat || harbour[placementIndex] is MotorBoat || harbour[placementIndex] is SailingBoat || harbour[placementIndex] is CargoShip)
                {
                    emptyslot = 0;
                }
                else
                {
                    Harbour.rejectedBoats++;
                }
                placementIndex++;
                if (placementIndex == x)
                {
                    break;
                }
            }
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

            Console.Write("|");
            for (int i = 0; i < x; i++)
            {
                harbour[i] = dummy;
                if (harbour[i] == dummy)
                {
                    Console.Write(dummy.TokenSign);

                }
                secondaryHarbour[i] = dummy;
                if (secondaryHarbour[i] == dummy)
                {
                    Console.Write(dummy.TokenSign);
                }
                Console.Write(" |");
            }
            Console.WriteLine();

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

            Console.Write("|");
            for (int i = 0; i < x; i++)
            {
                if (harbour[i] is RowingBoat)
                {
                    Console.Write(rowingBoat.TokenSign);
                }
                else if (harbour[i] is MotorBoat)
                {
                    Console.Write(motorBoat.TokenSign);
                }
                else if (harbour[i] is SailingBoat)
                {
                    Console.Write(sailingBoat.TokenSign);
                }
                else if (harbour[i] is CargoShip)
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
            Console.Write("|");
            for (int i = 0; i < y; i++)
            {
                if (secondaryHarbour[i] is RowingBoat)
                {
                    Console.Write(rowingBoat.TokenSign);
                }
                else
                {
                    Console.Write(dummy.TokenSign);
                }
                Console.Write(" |");
            }
            Console.WriteLine();

            Console.WriteLine("\n");
            Console.WriteLine($"Plats\tBåttyp\t\tID-nr\tVikt\tMaxhastighet\tÖvrigt");
            int listNumber = 1;
            foreach (var item in harbour)
            {
                if (harbour[listNumber - 1] is RowingBoat)
                {
                    Console.WriteLine($"{listNumber}\t{item.Type}\t{item.IdentityNumber}\t{item.Weight} kg\t\t{KnotToKMH(item.TopSpeed)} km/h\t");
                }
                if (secondaryHarbour[listNumber - 1] is RowingBoat)
                {
                    RowingBoat extraRowingBoat = (RowingBoat)secondaryHarbour[listNumber - 1];
                    Console.WriteLine($"{listNumber}\t{extraRowingBoat.Type}\t{extraRowingBoat.IdentityNumber}\t{extraRowingBoat.Weight} kg\t\t{KnotToKMH(extraRowingBoat.TopSpeed)} km/h\t");
                }
                if (harbour[listNumber - 1] is MotorBoat)
                {
                    Console.WriteLine($"{listNumber}\t{item.Type}\t{item.IdentityNumber}\t{item.Weight} kg\t\t{KnotToKMH(item.TopSpeed)} km/h\t");
                }
                if (harbour[listNumber - 1] is SailingBoat)
                {
                    if (sailingBoat.IdentityNumber == harbour[listNumber - 1].IdentityNumber)
                    {

                    }
                    else
                    {
                        Console.WriteLine($"{listNumber}-{listNumber + 1}\t{item.Type}\t{item.IdentityNumber}\t{item.Weight} kg\t\t{KnotToKMH(item.TopSpeed)} km/h\t");
                    }
                }
                if (harbour[listNumber - 1] is CargoShip)
                {
                    if (cargoShip.IdentityNumber == harbour[listNumber - 1].IdentityNumber)
                    {

                    }
                    else
                    {
                        if (cargoShip.Weight > 9999)
                        {
                            Console.WriteLine($"{listNumber}-{listNumber + 3}\t{item.Type}\t{item.IdentityNumber}\t{item.Weight} kg\t{KnotToKMH(item.TopSpeed)} km/h\t");
                        }
                        else
                        {
                            Console.WriteLine($"{listNumber}-{listNumber + 3}\t{item.Type}\t{item.IdentityNumber}\t{item.Weight} kg\t\t{KnotToKMH(item.TopSpeed)} km/h\t");
                        }
                    }
                }
                listNumber++;
            }

            //DecreaseCounter(rowingBoat);
            //DecreaseCounter(motorBoat);
            //DecreaseCounter(sailingBoat);
            //DecreaseCounter(cargoShip);
        }

        private static decimal KnotToKMH(decimal knot)
        {
            decimal knotToKMH = 1.852M;
            decimal kMH = knot * knotToKMH;
            return (int)kMH;
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
