using System;
using System.IO;
using System.Threading;
using System.Linq;

namespace HarbourUtkast2
{
    class Harbour
    {
        public static int x = 64;
        public static int daysPassed = 0;
        public static int rejectedBoats = 0;
        public static Boat[] harbour = new Boat[x];
        public static Boat[] secondaryHarbour = new Boat[x];
        public static void LoadData()
        {
            string dataFiles = File.ReadAllText("savedData.txt");
            if (new FileInfo("savedData.txt").Length == 0)
            {
                Console.Clear();
                Console.WriteLine("Vi börjar från dag 0, då det inte fanns något sparat!");
                Thread.Sleep(2000);
                Console.Clear();
            }
            else
            {
                string[] splitHarbour = dataFiles.Split('$');
                string[] harbour1 = splitHarbour[0].Split('\n', StringSplitOptions.RemoveEmptyEntries);
                string[] harbour2 = splitHarbour[1].Split('\n', StringSplitOptions.RemoveEmptyEntries);
                int counter = 0;
                foreach (var item in harbour1)
                {
                    if (item == harbour1[0])
                    {
                        string[] firstRow = item.Split(';');
                        daysPassed = int.Parse(firstRow[0]);
                        Harbour.rejectedBoats = int.Parse(firstRow[1]);
                    }
                    else
                    {
                        string[] boatValue = item.Split(';');
                        switch (boatValue[0])
                        {
                            case "Roddbåt\t":
                                Boat rowingBoat = new RowingBoat(boatValue[0], boatValue[1], int.Parse(boatValue[2]), int.Parse(boatValue[3]), boatValue[4], int.Parse(boatValue[5]), int.Parse(boatValue[6]));
                                harbour[counter] = rowingBoat;
                                break;
                            case "Motorbåt":
                                Boat motorBoat = new MotorBoat(boatValue[0], boatValue[1], int.Parse(boatValue[2]), int.Parse(boatValue[3]), boatValue[4], int.Parse(boatValue[5]), int.Parse(boatValue[6]));
                                harbour[counter] = motorBoat;
                                break;
                            case "Segelbåt":
                                Boat sailingBoat = new SailingBoat(boatValue[0], boatValue[1], int.Parse(boatValue[2]), int.Parse(boatValue[3]), boatValue[4], int.Parse(boatValue[5]), int.Parse(boatValue[6]));
                                harbour[counter] = sailingBoat;
                                break;
                            case "Katamaran":
                                Boat catamaran = new Catamaran(boatValue[0], boatValue[1], int.Parse(boatValue[2]), int.Parse(boatValue[3]), boatValue[4], int.Parse(boatValue[5]), int.Parse(boatValue[6]));
                                harbour[counter] = catamaran;
                                break;
                            case "Lastfartyg":
                                Boat cargoShip = new CargoShip(boatValue[0], boatValue[1], int.Parse(boatValue[2]), int.Parse(boatValue[3]), boatValue[4], int.Parse(boatValue[5]), int.Parse(boatValue[6]));
                                harbour[counter] = cargoShip;
                                break;
                            default:
                                Boat dummy = new DummyBoat(boatValue[0], boatValue[1], int.Parse(boatValue[2]), int.Parse(boatValue[3]), boatValue[4], int.Parse(boatValue[5]), bool.Parse(boatValue[6]));
                                harbour[counter] = dummy;
                                break;
                        }
                        counter++;
                    }
                }
                counter = 0;
                foreach (var item in harbour2)
                {
                    string[] boatValue = item.Split(';');
                    if (boatValue[0] == "\r")
                    {
                    }
                    else
                    {
                        switch (boatValue[0])
                        {
                            case "Roddbåt\t":
                                Boat rowingBoat = new RowingBoat(boatValue[0], boatValue[1], int.Parse(boatValue[2]), int.Parse(boatValue[3]), boatValue[4], int.Parse(boatValue[5]), int.Parse(boatValue[6]));
                                secondaryHarbour[counter] = rowingBoat;
                                break;
                            default:
                                Boat dummy = new DummyBoat(boatValue[0], boatValue[1], int.Parse(boatValue[2]), int.Parse(boatValue[3]), boatValue[4], int.Parse(boatValue[5]), bool.Parse(boatValue[6]));
                                secondaryHarbour[counter] = dummy;
                                break;
                        }
                        counter++;
                    }
                }
                Console.Clear();
                Console.WriteLine($"Laddar in sparad fil och börjar från dag {daysPassed}.");
                Thread.Sleep(2000);
                Console.Clear();
            }
        }
        public static void SaveData()
        {
            using StreamWriter sw = new StreamWriter("savedData.txt", false);
            sw.WriteLine($"{daysPassed};{Harbour.rejectedBoats}");
            foreach (var item in harbour)
            {
                if (item is DummyBoat)
                {
                    sw.WriteLine($"{item.Type};{item.IdentityNumber};{item.Weight};{item.TopSpeed};{item.TokenSign};{item.SpecialProperty};{item.FreeSlot}");
                }
                else
                {
                    sw.WriteLine($"{item.Type};{item.IdentityNumber};{item.Weight};{item.TopSpeed};{item.TokenSign};{item.SpecialProperty};{item.Counter}");
                }
            }
            sw.Write("$");
            foreach (var item in secondaryHarbour)
            {
                if (item is DummyBoat)
                {
                    sw.WriteLine($"{item.Type};{item.IdentityNumber};{item.Weight};{item.TopSpeed};{item.TokenSign};{item.SpecialProperty};{item.FreeSlot}");
                }
                else
                {
                    sw.WriteLine($"{item.Type};{item.IdentityNumber};{item.Weight};{item.TopSpeed};{item.TokenSign};{item.SpecialProperty};{item.Counter}");
                }
            }
        }
        public static void EmptyHarbour()
        {
            for (int i = 0; i < x; i++)
            {
                Boat dummy = new DummyBoat(" ", " ", 0, 0, tokenSign: " ", 0, true);
                harbour[i] = dummy;
            }
            for (int i = 0; i < x; i++)
            {
                Boat dummy = new DummyBoat(" ", " ", 0, 0, tokenSign: " ", 0, true);
                secondaryHarbour[i] = dummy;
            }
        }
        public static void WriteOutHarbour()
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
                    Console.Write($"{harbour[i].TokenSign} |");
                }
                else if (harbour[i] is MotorBoat)
                {
                    Console.Write($"{harbour[i].TokenSign} |");
                }
                else if (harbour[i] is SailingBoat)
                {
                    Console.Write($"{harbour[i].TokenSign} |> |");
                    i++;
                }
                else if (harbour[i] is Catamaran)
                {
                    Console.Write($"{harbour[i].TokenSign} |- |> |");
                    i += 2;
                }
                else if (harbour[i] is CargoShip)
                {
                    Console.Write($"{harbour[i].TokenSign} |- |- |> |");
                    i += 3;
                }
                else
                {
                    Console.Write($"{harbour[i].TokenSign} |");
                }
            }
            Console.WriteLine();
            Console.Write("|");
            for (int i = 0; i < x; i++)
            {
                if (secondaryHarbour[i] is RowingBoat)
                {
                    Console.Write($"{secondaryHarbour[i].TokenSign}");
                }
                else
                {
                    Console.Write($"{secondaryHarbour[i].TokenSign}");
                }
                Console.Write(" |");
            }
            Console.WriteLine($"\n\nPasserade dagar: {daysPassed}\n");
            Console.Write($"Antal båtar i hamn: {NumberOfBoats('R') + NumberOfBoats('M') + NumberOfBoats('S') + NumberOfBoats('K') + NumberOfBoats('L')}" +
                $"\t|Avvisade båtar: {Harbour.rejectedBoats}" +
                $"\n\t\t\t|\nRoddbåtar:   {NumberOfBoats('R')}\t\t|Total vikt i hamnen: \t{GameAndMethods.WeightCheck()} kg" +
                $"\nMotorbåtar:  {NumberOfBoats('M')}\t\t|Medeltal toppfart: \t{GameAndMethods.KnotToKMH(GameAndMethods.AvarageTopSpeed())} km/h" +
                $"\nSegelbåtar:  {NumberOfBoats('S')}\t\t|Antal lediga platser: \t{EmptySlots()}, plus {EmptyRowBoatSlot()} extra roddbåtsplats" +
                $"\nKatamaraner: {NumberOfBoats('K')}\t\t|" +
                $"\nLastfartyg:  {NumberOfBoats('L')}\t\t|\n");
            Console.WriteLine($"\nPlats\tBåttyp\t\tID-nr\tVikt/KG\tMaxhastighet\tÖvrigt\t\t|");
            Console.WriteLine("-------------------------------------------------------------------------");
            int listNumber = 1;
            foreach (var item in harbour)
            {
                if (harbour[listNumber - 1] is RowingBoat)
                {
                    Console.WriteLine($"{listNumber}\t{item.Type}\t{item.IdentityNumber}\t{item.Weight}\t{GameAndMethods.KnotToKMH(item.TopSpeed)} km/h\t{item.SpecialProperty} passagerare\t|");
                }
                if (secondaryHarbour[listNumber - 1] is RowingBoat)
                {
                    RowingBoat extraRowingBoat = (RowingBoat)secondaryHarbour[listNumber - 1];
                    Console.WriteLine($"{listNumber}\t{extraRowingBoat.Type}\t{extraRowingBoat.IdentityNumber}\t{extraRowingBoat.Weight}\t{GameAndMethods.KnotToKMH(extraRowingBoat.TopSpeed)} km/h\t{extraRowingBoat.SpecialProperty} passagerare\t|");
                }
                if (harbour[listNumber - 1] is MotorBoat)
                {
                    Console.WriteLine($"{listNumber}\t{item.Type}\t{item.IdentityNumber}\t{item.Weight}\t{GameAndMethods.KnotToKMH(item.TopSpeed)} km/h  \t{item.SpecialProperty} hk\t\t|");
                }
                if (harbour[listNumber - 1] is SailingBoat)
                {
                    Console.WriteLine($"{listNumber}-{listNumber + 1}\t{item.Type}\t{item.IdentityNumber}\t{item.Weight}\t{GameAndMethods.KnotToKMH(item.TopSpeed)} km/h\t{GameAndMethods.FeetToMetres(item.SpecialProperty)} meter lång\t|");
                    harbour[listNumber].FreeSlot = false;
                }
                if (harbour[listNumber - 1] is Catamaran)
                {
                    Console.WriteLine($"{listNumber}-{listNumber + 2}\t{item.Type}\t{item.IdentityNumber}\t{item.Weight}\t{GameAndMethods.KnotToKMH(item.TopSpeed)} km/h\t{item.SpecialProperty} bäddplatser\t|");
                    harbour[listNumber].FreeSlot = false;
                    harbour[listNumber + 1].FreeSlot = false;
                }
                if (harbour[listNumber - 1] is CargoShip)
                {
                    Console.WriteLine($"{listNumber}-{listNumber + 3}\t{item.Type}\t{item.IdentityNumber}\t{item.Weight}\t{GameAndMethods.KnotToKMH(item.TopSpeed)} km/h\t{item.SpecialProperty} containrar\t|");
                    harbour[listNumber].FreeSlot = false;
                    harbour[listNumber + 1].FreeSlot = false;
                    harbour[listNumber + 2].FreeSlot = false;
                }
                listNumber++;
            }
            Console.WriteLine("-------------------------------------------------------------------------");
            Console.Write("\n[ENTER]:Nästa dag\n[1]:\tStarta om\n[2]:\tAvsluta\nVälj: ");
        }
        public static void PlaceVessel(int value, Boat boat)
        {
            int placementIndex = 0;
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
                        if (value == 1 && Boat.approachingVessel == 1)
                        {
                            if (harbour[x - 1] is DummyBoat)
                            {
                                harbour[x - 1] = boat;
                            }
                            else if (secondaryHarbour[x - 1] is DummyBoat)
                            {
                                secondaryHarbour[x - 1] = boat;
                            }
                            else if (harbour[x - 2] is DummyBoat)
                            {
                                harbour[x - 2] = boat;
                            }
                            else if (secondaryHarbour[x - 2] is DummyBoat)
                            {
                                secondaryHarbour[x - 2] = boat;
                            }
                            else if (harbour[x - 3] is DummyBoat)
                            {
                                harbour[x - 3] = boat;
                            }
                            else
                            {
                                harbour[placementIndex] = boat;
                            }
                        }
                        else if (boat is MotorBoat)
                        {
                            harbour[placementIndex] = boat;
                        }
                        else if (boat is SailingBoat)
                        {
                            harbour[placementIndex - 1] = boat;
                            harbour[placementIndex].FreeSlot = false;
                            harbour[placementIndex].TokenSign = ">";
                        }
                        else if (boat is Catamaran)
                        {
                            harbour[placementIndex - 2] = boat;
                            harbour[placementIndex - 1].FreeSlot = false;
                            harbour[placementIndex].FreeSlot = false;
                            harbour[placementIndex - 1].TokenSign = "-";
                            harbour[placementIndex].TokenSign = ">";
                        }
                        else if (boat is CargoShip)
                        {
                            harbour[placementIndex - 3] = boat;
                            harbour[placementIndex - 2].FreeSlot = false;
                            harbour[placementIndex - 1].FreeSlot = false;
                            harbour[placementIndex].FreeSlot = false;
                            harbour[placementIndex - 2].TokenSign = "-";
                            harbour[placementIndex - 1].TokenSign = "-";
                            harbour[placementIndex].TokenSign = ">";
                        }
                        break;
                    }
                }
                else if (boat is RowingBoat || boat is MotorBoat || boat is SailingBoat || boat is Catamaran || boat is CargoShip)
                {
                    emptyslot = 0;
                }
                placementIndex++;
            }
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
        public static void DecreaseCounter()
        {
            daysPassed++;
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
                                harbour[index] = new DummyBoat(" ", " ", 0, 0, " ", 0, true);
                                harbour[index + 1].FreeSlot = true; harbour[index + 2].FreeSlot = true; harbour[index + 3].FreeSlot = true;
                                harbour[index + 1].TokenSign = " "; harbour[index + 2].TokenSign = " "; harbour[index + 3].TokenSign = " ";
                            }
                            else if (harbour[index] is Catamaran)
                            {
                                harbour[index] = new DummyBoat(" ", " ", 0, 0, " ", 0, true);
                                harbour[index + 1].FreeSlot = true; harbour[index + 2].FreeSlot = true;
                                harbour[index + 1].TokenSign = " "; harbour[index + 2].TokenSign = " ";
                            }
                            else if (harbour[index] is SailingBoat)
                            {
                                harbour[index] = new DummyBoat(" ", " ", 0, 0, " ", 0, true);
                                harbour[index + 1].FreeSlot = true;
                                harbour[index + 1].TokenSign = " ";
                            }
                            else
                            {
                                harbour[index] = new DummyBoat(" ", " ", 0, 0, " ", 0, true);
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
                            secondaryHarbour[index] = new DummyBoat(" ", " ", 0, 0, " ", 0, true);
                        }
                    }
                }
            }
        }
    }
}
