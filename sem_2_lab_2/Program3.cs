using System;
abstract class Vessel
{
    public abstract void PrepareToMovement();
    public abstract void Move();
}
class SailingVessel : Vessel
{
    public override void PrepareToMovement()
    {
        Console.WriteLine("Sailing Vessel prepares to movement");
    }
    public override void Move()
    {
        Console.WriteLine("Movement of the Sailing Vessel");
    }
}
class Submarine : Vessel
{
    public override void PrepareToMovement()
    {
        Console.WriteLine("Submarine prepares to movement");
    }
    public override void Move()
    {
        Console.WriteLine("Movement of the Submarine");
    }
}
class Program
{
    static void Main(string[] args)
    {
        Vessel sailingVessel = new SailingVessel();
        Vessel submarine = new Submarine();

        sailingVessel.PrepareToMovement();
        sailingVessel.Move();

        submarine.PrepareToMovement();
        submarine.Move();

        Console.ReadLine();
    }
}
