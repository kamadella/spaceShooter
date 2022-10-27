using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Numerics;

namespace spaceShooter
{

    class spaceShooter
    {
        int width;
        int height;
        Board board;
        Player player;
        ConsoleKeyInfo keyInfo;
        ConsoleKey consoleKey;


        public spaceShooter(int width, int height)
        {
            this.width = width;
            this.height = height;
            board = new Board(width,height);
        }

        public void Setup()
        {
            player = new Player(15,width);
            keyInfo = new ConsoleKeyInfo();
            consoleKey = new ConsoleKey();
        }

        void Input()
        {
            if (Console.KeyAvailable)
            {
                keyInfo = Console.ReadKey(true);
                consoleKey = keyInfo.Key;
            }
        }

        public void Run()
        {
            while (true)
            {
                Console.Clear();
                Setup();
                board.Write();
                player.Write();
                while (true)
                {
                    Input();
                    switch (consoleKey)
                    {
                        case ConsoleKey.A:
                            player.Left();
                            break;
                        case ConsoleKey.D:
                            player.Right();
                            break;
                    }
                    consoleKey = ConsoleKey.N;
                    Thread.Sleep(100);
                }
            }
            
        }
    }

    public class Enemy
    {

        public Enemy(int toDraw, int level)
        {
            while (toDraw > 0)
            {

                toDraw--;
            }
        }

    }

    public class Bullet
    {
        
    }

    public class Player
    {
        public int X { set; get; }
        int boardWidth;
        
        int HP;
        int HPMax;
        Vector<Bullet> bullets;

        public Player(int x, int boardWidth)
        {
            X = x;
            HPMax = 10;
            HP = HPMax;
            this.boardWidth = boardWidth;
        }

        public void Left()
        {
            if (X-1 != 0)
            {
                X--;
                Write();
                Console.Write(" ");
            }
        }

        public void Right()
        {
            if (X+1 != boardWidth+1)
            {            
                X++;
                Console.SetCursorPosition(X-1, 20);
                Console.Write(" ");
                Write();

            }
        }

        public void Write()
        {
            Console.ForegroundColor = ConsoleColor.Red;

            Console.SetCursorPosition(X, 20);
            Console.Write("▲");
         
            Console.ForegroundColor = ConsoleColor.White;
        }
    }

    public class Board //wyświetlanie planszy
    {
        public int Height { get; set; }
        public int Width { get; set; }

        public Board()
        {
            Height = 30;
            Width = 20;
        }
        public Board(int width, int height)
        {
            Height = height;
            Width = width;
        }

        public void Write() 
        {
            for (int i = 1; i <= Width; i++)
            {
                Console.SetCursorPosition(i, 0);
                Console.Write("─");
            }

            for (int i = 0; i <= Width; i++)
            {
                Console.SetCursorPosition(i, (Height+1));
                Console.Write("─");
            }

            for (int i = 1; i <= Height; i++)
            {
                Console.SetCursorPosition(0, i);
                Console.Write("│");
            }

            for (int i = 0; i <= Height; i++)
            {
                Console.SetCursorPosition((Width + 1), i);
                Console.Write("│");
            }

            Console.SetCursorPosition(0,0);
            Console.Write("┌");
            Console.SetCursorPosition(Width+1, 0);
            Console.Write("┐");
            Console.SetCursorPosition(0, Height+1);
            Console.Write("└");
            Console.SetCursorPosition(Width+1, Height+1);
            Console.Write("┘");


            for (int i = 0; i <= Width-1; i++)
            {
                Console.SetCursorPosition(i+1, 5);
                Console.Write("─");
            }


        }
    }
}
