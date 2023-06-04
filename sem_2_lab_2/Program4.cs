using System;

interface IVessel
{
    void PrepareToMovement();
    void Move();
}

class SailingVessel : IVessel
{
    public void PrepareToMovement()
    {
        Console.WriteLine("Sailing Vessel prepares to movement");
    }

    public void Move()
    {
        Console.WriteLine("Movement of the Sailing Vessel");
    }
}

class Submarine : IVessel
{
    public void PrepareToMovement()
    {
        Console.WriteLine("Submarine prepares to movement");
    }

    public void Move()
    {
        Console.WriteLine("Movement of the Submarine");
    }
}

class Program
{
    static void Main(string[] args)
    {
        IVessel vessel1 = new SailingVessel();
        IVessel vessel2 = new Submarine();

        vessel1.PrepareToMovement();
        vessel1.Move();

        vessel2.PrepareToMovement();
        vessel2.Move();
    }
}