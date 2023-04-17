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

    }

    public void Move()
    {

    }
}

class Submarine : IVessel
{
    public void PrepareToMovement()
    {

    }

    public void Move()
    {

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