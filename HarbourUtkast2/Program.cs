using System;
using System.Linq;

namespace HarbourUtkast2
{
    class Program
    {
        public static int vesselsPerDay = 3;
        public static int x = 64;
        public static int y = 64;
        public static int approachingVessel;
        public static int placementIndex;
        public static int daysPassed = 0;
        public static Boat[] harbour = new Boat[x];
        public static Boat[] secondaryHarbour = new Boat[y];
        public static Random random = new Random();

        public static int sailingBoatValue = 0;
        public static int cargoShipValue = 0;
        static void Main(string[] args)
        {
            bool isRunning = true;
            EmptyHarbour();
            while (isRunning)
            {
                NewDayHappenings();
                DecreaseCounter();
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
                switch (approachingVessel)
                {
                    case 1:
                        //Boat rowingBoat = new RowingBoat($"Roddbåt\t", $"R-{GetID()}", GetValue(100, 300), GetValue(0, 3), tokenSign: "R", GetValue(1, 6), 1);
                        //if (harbour[x - 1] is DummyBoat)
                        //{
                        //    harbour[x - 1] = rowingBoat;
                        //}
                        //else if (secondaryHarbour[y - 1] is DummyBoat)
                        //{
                        //    secondaryHarbour[y - 1] = rowingBoat;
                        //}
                        //else if (harbour[x - 2] is DummyBoat)
                        //{
                        //    harbour[x - 2] = rowingBoat;
                        //}
                        //else if (secondaryHarbour[y - 2] is DummyBoat)
                        //{
                        //    secondaryHarbour[y - 2] = rowingBoat;
                        //}
                        //else if (harbour[x - 3] is DummyBoat)
                        //{
                        //    harbour[x - 3] = rowingBoat;
                        //}
                        //else if (!(harbour[x - 1] is DummyBoat) || !(harbour[x - 2] is DummyBoat) || !(harbour[x - 3] is DummyBoat) || !(secondaryHarbour[y - 1] is DummyBoat) || !(secondaryHarbour[y - 2] is DummyBoat))
                        //{
                        //    PlaceVessel(1, rowingBoat);
                        //}
                        break;
                    case 2:
                        Boat motorBoat = new MotorBoat($"Motorbåt", $"M-{GetID()}", GetValue(200, 3000), GetValue(0, 60), tokenSign: "M", GetValue(10, 1000), 3);
                        PlaceVessel(1, motorBoat);
                        break;
                    case 3:
                        Boat sailingBoat = new SailingBoat($"Segelbåt", $"S-{GetID()}", GetValue(800, 6000), GetValue(0, 12), tokenSign: "S", GetValue(10, 60), 4);
                        PlaceVessel(2, sailingBoat);
                        break;
                    case 4:
                        Boat cargoShip = new CargoShip($"Lastfartyg", $"L-{GetID()}", GetValue(3000, 20000), GetValue(0, 20), tokenSign: "L", GetValue(0, 500), 6);
                        PlaceVessel(4, cargoShip);
                        break;
                }
            }
        }

        private static void DecreaseCounter()
        {
            for (int index = 0; index < harbour.Length; index++)
            {
                if (!(harbour[index] is DummyBoat))
                {
                    if (harbour[index].Counter > 0)
                    {
                        harbour[index].Counter--;
                    }
                    if (harbour[index].Counter < 1)
                    {
                        if (harbour[index] is CargoShip)
                        {
                            harbour[index] = new DummyBoat("", "", 0, 0, " ", 0, true);
                            harbour[index + 1].FreeSlot = true;
                            harbour[index + 2].FreeSlot = true;
                            harbour[index + 3].FreeSlot = true;

                        }
                        else if (harbour[index] is SailingBoat)
                        {
                            harbour[index] = new DummyBoat("", "", 0, 0, " ", 0, true);
                            harbour[index + 1].FreeSlot = true;
                        }
                        else
                        {
                            harbour[index] = new DummyBoat("", "", 0, 0, " ", 0, true);
                        }
                    }
                }
            }
            for (int index = 0; index < secondaryHarbour.Length; index++)
            {
                if (secondaryHarbour[index] is RowingBoat)
                {
                    if (secondaryHarbour[index].Counter > 0)
                    {
                        secondaryHarbour[index].Counter--;
                    }
                    else if (secondaryHarbour[index].Counter < 1)
                    {
                        secondaryHarbour[index] = new DummyBoat("", "", 0, 0, " ", 0, true);
                    }
                }
            }
        }

        private static void PlaceVessel(int value, Boat boat)
        {
            int emptyslot = 0;
            foreach (var element in harbour)
            {
                if (placementIndex == x - 1)
                {
                    break;
                }
                if (element is RowingBoat)
                {
                    if (!(secondaryHarbour[placementIndex] is RowingBoat))
                    {
                        secondaryHarbour[placementIndex] = boat;
                        break;
                    }
                    //else
                    //{
                    //    Harbour.rejectedBoats++;
                    //}
                }
                else if (harbour[placementIndex].FreeSlot == true)
                {
                    emptyslot++;
                    if (value == emptyslot)
                    {
                        if (value == 1 && approachingVessel == 1)
                        {
                            harbour[placementIndex] = boat;
                        }
                        else if (boat is MotorBoat)
                        {
                            harbour[placementIndex] = boat;
                        }
                        else if (boat is SailingBoat)
                        {
                            harbour[placementIndex - 1] = boat;
                            harbour[placementIndex].FreeSlot = false;
                            //harbour[placementIndex] = sailingBoat;
                        }
                        else if (boat is CargoShip)
                        {
                            harbour[placementIndex - 3] = boat;
                            harbour[placementIndex - 2].FreeSlot = false;
                            harbour[placementIndex - 1].FreeSlot = false;
                            harbour[placementIndex].FreeSlot = false;
                            //harbour[placementIndex - 2] = cargoShip;
                            //harbour[placementIndex - 1] = cargoShip;
                            //harbour[placementIndex] = cargoShip;
                        }
                        break;
                    }
                }
                else if (boat is RowingBoat || boat is MotorBoat || boat is SailingBoat || boat is CargoShip)
                {
                    emptyslot = 0;
                }
                //else
                //{
                //    Harbour.rejectedBoats++;
                //    break;
                //}
                placementIndex++;
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
                Boat dummy = new DummyBoat("", "", 0, 0, tokenSign: " ", 0, true);
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
                    Console.Write("R");
                }
                else if (harbour[i] is MotorBoat)
                {
                    Console.Write("M");
                }
                else if (harbour[i] is SailingBoat)
                {
                    Console.Write("S");
                }
                else if (harbour[i] is CargoShip)
                {
                    Console.Write("L");
                }
                else
                {
                    Console.Write(" ");
                }
                Console.Write(" |");
            }
            Console.WriteLine();
            Console.Write("|");
            for (int i = 0; i < y; i++)
            {
                if (secondaryHarbour[i] is RowingBoat)
                {
                    Console.Write("R");
                }
                else
                {
                    Console.Write(" ");
                }
                Console.Write(" |");
            }
            Console.WriteLine();

            Console.WriteLine("\n");
            Console.WriteLine($"Passerade dagar: {daysPassed}");
            Console.Write($"Antal båtar i hamn: {NumberOfBoats('R') + NumberOfBoats('M') + (NumberOfBoats('S')) + (NumberOfBoats('L'))}" +
                $"\tTotal vikt av båtar: " +
                $"\n\nRoddbåtar:  {NumberOfBoats('R')}" +
                $"\nMotirbåtar: {NumberOfBoats('M')}" +
                $"\nSegelbåtar: {NumberOfBoats('S')}" +
                $"\nLastfartyg: {NumberOfBoats('L')}\n");
            Console.WriteLine($"\nPlats\tBåttyp\t\tID-nr\tVikt/KG\tMaxhastighet\tÖvrigt\t\t|Avvisade båtar: {Harbour.rejectedBoats}");
            Console.WriteLine("--------------------------------------------------------------------------------------------");
            int listNumber = 1;
            foreach (var item in harbour)
            {
                if (harbour[listNumber - 1] is RowingBoat)
                {
                    Console.WriteLine($"{listNumber}\t{item.Type}\t{item.IdentityNumber}\t{item.Weight}\t{KnotToKMH(item.TopSpeed)} km/h\t\t{item.SpecialProperty} passagerare\t|");
                }
                if (secondaryHarbour[listNumber - 1] is RowingBoat)
                {
                    RowingBoat extraRowingBoat = (RowingBoat)secondaryHarbour[listNumber - 1];
                    Console.WriteLine($"{listNumber}\t{extraRowingBoat.Type}\t{extraRowingBoat.IdentityNumber}\t{extraRowingBoat.Weight}\t{KnotToKMH(extraRowingBoat.TopSpeed)} km/h\t\t{extraRowingBoat.SpecialProperty} passagerare\t|");
                }
                if (harbour[listNumber - 1] is MotorBoat)
                {
                    Console.WriteLine($"{listNumber}\t{item.Type}\t{item.IdentityNumber}\t{item.Weight}\t{KnotToKMH(item.TopSpeed)} km/h  \t{item.SpecialProperty} hästkrafter\t|");
                }
                if (harbour[listNumber - 1] is SailingBoat)
                {
                    if (sailingBoatValue == 1)
                    {
                        sailingBoatValue = 0;
                    }
                    else if (item.IdentityNumber == harbour[listNumber - 1].IdentityNumber)
                    {
                        Console.WriteLine($"{listNumber}-{listNumber + 1}\t{item.Type}\t{item.IdentityNumber}\t{item.Weight}\t{KnotToKMH(item.TopSpeed)} km/h\t\t{item.SpecialProperty} meter lång\t|");
                        sailingBoatValue++;
                    }
                }
                if (harbour[listNumber - 1] is CargoShip)
                {
                    if (cargoShipValue > 0)
                    {
                        if (cargoShipValue == 3)
                        {
                            cargoShipValue = 0;
                        }
                        else
                        {
                            cargoShipValue++;
                        }
                    }
                    else
                    {
                        Console.WriteLine($"{listNumber}-{listNumber + 3}\t{item.Type}\t{item.IdentityNumber}\t{item.Weight}\t{KnotToKMH(item.TopSpeed)} km/h\t\t{item.SpecialProperty} containrar\t|");
                        cargoShipValue++;
                    }
                }
                listNumber++;
            }
            daysPassed++;
            //foreach (var item in harbour)
            //{
            //    DecreaseCounter(item);

            //}
            //int decreaseCounter = 0;
            //foreach (var item in harbour)
            //{
            //    if (item.Counter == 0)
            //    {
            //        if (item is RowingBoat || item is MotorBoat)
            //        {
            //            harbour[decreaseCounter] = dummy;
            //        }
            //        else if (item is SailingBoat)
            //        {
            //            harbour[decreaseCounter] = dummy;
            //            harbour[decreaseCounter + 1] = dummy;
            //        }
            //        else if (item is CargoShip)
            //        {
            //            harbour[decreaseCounter] = dummy;
            //            harbour[decreaseCounter + 1] = dummy;
            //            harbour[decreaseCounter + 2] = dummy;
            //            harbour[decreaseCounter + 3] = dummy;
            //        }
            //    }
            //    decreaseCounter++;
            //}
            //foreach (var item in secondaryHarbour)
            //{
            //    DecreaseCounter(item);

            //}
            //decreaseCounter = 0;
            //foreach (var item in secondaryHarbour)
            //{
            //    DecreaseCounter(item);
            //    if (item.Counter == 0)
            //    {
            //        if (item is RowingBoat)
            //        {
            //            harbour[decreaseCounter] = dummy;
            //        }
            //        else
            //        {
            //            item.Counter = 0;
            //        }
            //    }
            //    decreaseCounter++;
            //}
        }

        private static int NumberOfBoats(char v)
        {
            int number = 0;
            var q = harbour
                .Where(h => h.IdentityNumber.StartsWith(v));
            foreach (var item in q)
            {
                number++;
            }
            if (v == 'R')
            {
                var q1 = secondaryHarbour
                    .Where(s => s.IdentityNumber.StartsWith(v));
                foreach (var item in q1)
                {
                    number++;
                }
            }
            return number;
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
