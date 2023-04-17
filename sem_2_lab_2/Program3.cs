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

    }
    public override void Move()
    {

    }
}
class Submarine : Vessel
{
    public override void PrepareToMovement()
    {

    }
    public override void Move()
    {

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
    }
}
