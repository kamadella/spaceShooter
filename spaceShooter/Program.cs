using System;

namespace spaceShooter
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.CursorVisible = false;
            spaceShooter spaceShooter = new spaceShooter(25,20);  //szerokosc 30 wysokosc 20 
            spaceShooter.Run();
            Console.ReadKey();
        }
    }
}