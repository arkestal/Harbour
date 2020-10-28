using System;
using System.Linq;

namespace HarbourUtkast2
{
    class Program
    {
        public static int vesselsPerDay = 5;
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
                DecreaseCounter();
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
                switch (approachingVessel)
                {
                    case 1:
                        Boat rowingBoat = new RowingBoat($"Roddbåt\t", $"R-{GetID()}", GetValue(100, 300), GetValue(0, 3), tokenSign: "R", GetValue(1, 6), 1);
                        if (harbour[x - 1] is DummyBoat)
                        {
                            harbour[x - 1] = rowingBoat;
                        }
                        else if (secondaryHarbour[y - 1] is DummyBoat)
                        {
                            secondaryHarbour[y - 1] = rowingBoat;
                        }
                        else if (harbour[x - 2] is DummyBoat)
                        {
                            harbour[x - 2] = rowingBoat;
                        }
                        else if (secondaryHarbour[y - 2] is DummyBoat)
                        {
                            secondaryHarbour[y - 2] = rowingBoat;
                        }
                        else if (harbour[x - 3] is DummyBoat)
                        {
                            harbour[x - 3] = rowingBoat;
                        }
                        else if (!(harbour[x - 1] is DummyBoat) || !(harbour[x - 2] is DummyBoat) || !(harbour[x - 3] is DummyBoat) || !(secondaryHarbour[y - 1] is DummyBoat) || !(secondaryHarbour[y - 2] is DummyBoat))
                        {
                            PlaceVessel(1, rowingBoat);
                        }
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
                        if (harbour[index].Counter == 0)
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
            }
            for (int index = 0; index < secondaryHarbour.Length; index++)
            {
                if (secondaryHarbour[index] is RowingBoat)
                {
                    if (secondaryHarbour[index].Counter > 0)
                    {
                        secondaryHarbour[index].Counter--;
                        if (secondaryHarbour[index].Counter == 0)
                        {
                            secondaryHarbour[index] = new DummyBoat("", "", 0, 0, " ", 0, true);
                        }
                    }
                }
            }
        }

        private static void PlaceVessel(int value, Boat boat)
        {
            placementIndex = 0;
            int emptyslot = 0;
            foreach (var element in harbour)
            {
                if (placementIndex == x - 1)
                {
                    Harbour.rejectedBoats++;
                    break;
                }
                if (element is RowingBoat)
                {
                    if (!(secondaryHarbour[placementIndex] is RowingBoat))
                    {
                        secondaryHarbour[placementIndex] = boat;
                        break;
                    }
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
                        }
                        else if (boat is CargoShip)
                        {
                            harbour[placementIndex - 3] = boat;
                            harbour[placementIndex - 2].FreeSlot = false;
                            harbour[placementIndex - 1].FreeSlot = false;
                            harbour[placementIndex].FreeSlot = false;
                        }
                        break;
                    }
                }
                else if (boat is RowingBoat || boat is MotorBoat || boat is SailingBoat || boat is CargoShip)
                {
                    emptyslot = 0;
                }
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
            Console.WriteLine($"Passerade dagar: {daysPassed}\n");
            Console.Write($"Antal båtar i hamn: {NumberOfBoats('R') + NumberOfBoats('M') + (NumberOfBoats('S')) + (NumberOfBoats('L'))}" +
                $"\t|Avvisade båtar: {Harbour.rejectedBoats}" +
                $"\n\t\t\t|\nRoddbåtar:  {NumberOfBoats('R')}\t\t|Totala vikten på båtarna i hamn: {WeightCheck()} kg" +
                $"\nMotorbåtar: {NumberOfBoats('M')}\t\t|Medeltal toppfart: {KnotToKMH(AvarageTopSpeed())}" +
                $"\nSegelbåtar: {NumberOfBoats('S')}\t\t|Antal lediga platser: {EmptySlots()}, plus {EmptyRowBoatSlot()} extra roddbåtsplats(er)" +
                $"\nLastfartyg: {NumberOfBoats('L')}\t\t|\n");
            Console.WriteLine($"\nPlats\tBåttyp\t\tID-nr\tVikt/KG\tMaxhastighet\tÖvrigt\t\t|");
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
                    Console.WriteLine($"{listNumber}-{listNumber + 1}\t{item.Type}\t{item.IdentityNumber}\t{item.Weight}\t{KnotToKMH(item.TopSpeed)} km/h\t\t{item.SpecialProperty} meter lång\t|");
                    harbour[listNumber].FreeSlot = false;
                }
                if (harbour[listNumber - 1] is CargoShip)
                {
                    Console.WriteLine($"{listNumber}-{listNumber + 3}\t{item.Type}\t{item.IdentityNumber}\t{item.Weight}\t{KnotToKMH(item.TopSpeed)} km/h\t\t{item.SpecialProperty} containrar\t|");
                    harbour[listNumber].FreeSlot = false;
                    harbour[listNumber + 1].FreeSlot = false;
                    harbour[listNumber + 2].FreeSlot = false;
                }
                listNumber++;
            }
            daysPassed++;
        }

        private static object EmptyRowBoatSlot()
        {
            int emptyRowBoatCounter = 0;
            var q7 = harbour
                .Where(h => h.TokenSign == "R");
            foreach (var item in q7)
            {
                emptyRowBoatCounter++;
            }
            var q8 = secondaryHarbour
                .Where(s => s.TokenSign == "R");
            foreach (var item in q8)
            {
                emptyRowBoatCounter--;
            }
            return emptyRowBoatCounter;
        }

        private static int EmptySlots()
        {
            int emptyCounter = 0;

            var q6 = harbour
                .Where(h => h.FreeSlot == true);
            foreach (var item in q6)
            {
                emptyCounter++;
            }

            return emptyCounter;
        }

        private static decimal AvarageTopSpeed()
        {
            double harbourSpeed = 0;
            double counter = 0;

            var q4 = harbour
                .Where(h => h.TokenSign != " ")
                .Select(h => h.TopSpeed);
            foreach (var item in q4)
            {
                harbourSpeed += item;
                counter++;
            }

            var q5 = secondaryHarbour
                .Where(s => s.TokenSign == "R")
                .Select(s => s.TopSpeed);
            foreach (var item in q5)
            {
                harbourSpeed += item;
                counter++;
            }
            double value = harbourSpeed / counter;
            return (int)value;
        }

        private static int WeightCheck()
        {
            int harbourWeight = 0;
            var q2 = harbour
                .Where(h => h.Weight > 0)
                .Select(h => h.Weight);
            foreach (var item in q2)
            {
                harbourWeight += item;
            }

            var q3 = secondaryHarbour
                .Where(s => s.Weight > 0)
                .Select(s => s.Weight);
            foreach (var item in q3)
            {
                harbourWeight += item;
            }
            return harbourWeight;
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
