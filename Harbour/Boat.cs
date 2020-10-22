using System;
using System.Collections.Generic;
using System.Text;

namespace Harbour
{
    class Boat
    {
        public string IdentityNumber { get; set; }
        public int Weight { get; set; }
        public int TopSpeed { get; set; }
        public string TokenSign { get; set; }
        public int Counter { get; set; }
        public Boat(string identityNumber, int weight, int topSpeed, string tokenSign)
        {
            IdentityNumber = identityNumber;
            Weight = weight;
            TopSpeed = topSpeed;
            TokenSign = tokenSign;
        }
    }
    
    class RowingBoat : Boat
    {
        public int Passengers { get; set; }
        public RowingBoat(string identityNumber, int weight, int topSpeed, string tokenSign, int counter, int passengers)
            : base(identityNumber, weight, topSpeed, "R")
        {
            Counter = counter;
            Passengers = passengers;
        }
    }

    class MotorBoat : Boat
    {
        public int HorsePower { get; set; }
        public MotorBoat(string identityNumber, int weight, int topSpeed, string tokenSign, int counter, int horsePower)
            : base(identityNumber, weight, topSpeed, "M")
        {
            Counter = counter;
            HorsePower = horsePower;
        }
    }

    class SailingBoat : Boat
    {
        public int BoatLenght { get; set; }
        public SailingBoat(string identityNumber, int weight, int topSpeed, string tokenSign, int counter, int boatLenght)
            : base(identityNumber, weight, topSpeed, "S")
        {
            Counter = counter;
            BoatLenght = boatLenght;
        }
    }

    class CargoShip : Boat
    {
        public int ContainerCargo { get; set; }
        public CargoShip(string identityNumber, int weight, int topSpeed, string tokenSign, int counter, int containerCargo)
            : base(identityNumber, weight, topSpeed, "L")
        {
            Counter = counter;
            ContainerCargo = containerCargo;
        }
    }
    class DummyBoat : Boat
    {
        public DummyBoat(string identityNumber, int weight, int topSpeed, string tokenSign)
            : base("", 0, 0, " ")
        {

        }
    }
}
