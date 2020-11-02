using System;
using System.Collections.Generic;
using System.Text;

namespace HarbourUtkast2
{
    class Boat
    {
        public static int vesselsPerDay = 5;
        public static int approachingVessel;
        public static Random random = new Random();
        private static int GetValue(int a, int b)
        {
            int value = random.Next(a, b + 1);
            return value;
        }
        private static string GetID()
        {
            string ID = "";
            bool isRunning = true;
            do
            {
                for (int i = 0; i < 3; i++)
                {
                    int number = random.Next(0, 25 + 1);
                    char letter = (char)('a' + number);
                    ID += letter;
                }
                foreach (var item in Harbour.harbour)
                {
                    if (item.IdentityNumber.EndsWith(ID.ToUpper()))
                    {
                        isRunning = true;
                    }
                    else
                    {
                        isRunning = false;
                    }
                }
            } while (isRunning);
            return ID.ToUpper();
        }
        public static void GeneratingNewBoats()
        {
            for (int i = 0; i < vesselsPerDay; i++)
            {
                approachingVessel = random.Next(1, 4 + 1);
                switch (approachingVessel)
                {
                    case 1:
                        Boat rowingBoat = new RowingBoat($"Roddbåt\t", $"R-{GetID()}", GetValue(100, 300), GetValue(0, 3), tokenSign: "R", GetValue(1, 6), 1);
                        Harbour.PlaceVessel(1, rowingBoat);
                        break;
                    case 2:
                        Boat motorBoat = new MotorBoat($"Motorbåt", $"M-{GetID()}", GetValue(200, 3000), GetValue(0, 60), tokenSign: "M", GetValue(10, 1000), 3);
                        Harbour.PlaceVessel(1, motorBoat);
                        break;
                    case 3:
                        Boat sailingBoat = new SailingBoat($"Segelbåt", $"S-{GetID()}", GetValue(800, 6000), GetValue(0, 12), tokenSign: "S", GetValue(10, 60), 4);
                        Harbour.PlaceVessel(2, sailingBoat);
                        break;
                    case 4:
                        Boat cargoShip = new CargoShip($"Lastfartyg", $"L-{GetID()}", GetValue(3000, 20000), GetValue(0, 20), tokenSign: "L", GetValue(0, 500), 6);
                        Harbour.PlaceVessel(4, cargoShip);
                        break;
                }
            }
        }
        public string Type { get; set; }
        public string IdentityNumber { get; set; }
        public int Weight { get; set; }
        public int TopSpeed { get; set; }
        public string TokenSign { get; set; }
        public int SpecialProperty { get; set; }
        public int Counter { get; set; }
        public bool FreeSlot { get; set; }
        public Boat(string type, string identityNumber, int weight, int topSpeed, string tokenSign, int specialProperty)
        {
            Type = type;
            IdentityNumber = identityNumber;
            Weight = weight;
            TopSpeed = topSpeed;
            TokenSign = tokenSign;
            SpecialProperty = specialProperty;
        }
    }

    class RowingBoat : Boat
    {
        public RowingBoat(string type, string identityNumber, int weight, int topSpeed, string tokenSign, int specialProperty, int counter)
            : base(type, identityNumber, weight, topSpeed, "R", specialProperty)
        {
            Counter = counter;
        }
    }

    class MotorBoat : Boat
    {
        public MotorBoat(string type, string identityNumber, int weight, int topSpeed, string tokenSign, int specialProperty, int counter)
            : base(type, identityNumber, weight, topSpeed, "M", specialProperty)
        {
            Counter = counter;
        }
    }

    class SailingBoat : Boat
    {
        public SailingBoat(string type, string identityNumber, int weight, int topSpeed, string tokenSign, int specialProperty, int counter)
            : base(type, identityNumber, weight, topSpeed, "S", specialProperty)
        {
            Counter = counter;
        }
    }

    class CargoShip : Boat
    {
        public CargoShip(string type, string identityNumber, int weight, int topSpeed, string tokenSign, int specialProperty, int counter)
            : base(type, identityNumber, weight, topSpeed, "L", specialProperty)
        {
            Counter = counter;
        }
    }
    class DummyBoat : Boat
    {
        public DummyBoat(string type, string identityNumber, int weight, int topSpeed, string tokenSign, int specialProperty, bool freeSlot)
            : base(type, " ", 0, 0, " ", 0)
        {
            FreeSlot = freeSlot;
        }
    }
}
