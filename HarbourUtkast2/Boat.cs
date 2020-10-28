using System;
using System.Collections.Generic;
using System.Text;

namespace HarbourUtkast2
{
    class Boat
    {
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
            : base("", "", 0, 0, " ", 0)
        {
            FreeSlot = freeSlot;
        }
    }
}
