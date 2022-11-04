﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Numerics;
using System.Collections;
using static System.Formats.Asn1.AsnWriter;

namespace spaceShooter
{

    class spaceShooter
    {
        int width;
        int height;
        Board board;
        Player player;
        List<Enemy> enemies = new List<Enemy>();
        List<Shoot> shoots = new List<Shoot>();
        int enemySpawner = 10;
        int score = 0;
        int timeShoot = 0;
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
                board.Write(); //narysuj plansze
                player.Write(); //narysuj bohatera

                //ilsc przeciwnikow 
                for (int i=0; i < enemySpawner; i++)
                {
                    var enemy = new Enemy(1 + i, 6, 2); ;
                    enemies.Add(enemy);
                }



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
                        case ConsoleKey.P: //strzelanie
                            var bullet = new Bullet(player.posX);
                            player.bullets.Add(bullet); //jak klikne P to powstaje nowy pocisk
                            timeShoot++;
                            break;
                    }
                    consoleKey = ConsoleKey.N;
                    Thread.Sleep(50);

                    //wynik
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.SetCursorPosition(4, 2);
                    Console.Write("Punkty: " + score);
                    Console.ForegroundColor = ConsoleColor.White;

                    

                    //Bullets
                    for (int i = 0; i < player.bullets.Count; i++)
                    {
                        //wypisywanie pocisku
                        player.bullets[i].posY -= 1;
                        player.bullets[i].Write();


                        for (int k = 0; k < enemies.Count; k++)
                        {

                            if ((player.bullets[i].posY) + 1 == (enemies[k].posY) + 1 && player.bullets[i].posX == enemies[k].posX)
                            {
                                if (enemies[k].HP <= 1) //śmierć wroga
                                {
                                    score += enemies[k].HPMax;
                                    enemies.RemoveAt(k);
                                }
                                else
                                    enemies[k].HP--; //obrażenia dla wroga

                                Console.SetCursorPosition(player.bullets[i].posX, player.bullets[i].posY);
                                Console.Write(" ");
                                player.bullets.RemoveAt(i);
                                break;
                            }
                            else if (player.bullets[i].posY < 6) //usun bulleta jak wyjdzie poza plansze
                            {
                                Console.ForegroundColor = ConsoleColor.Blue;
                                Console.SetCursorPosition(player.bullets[i].posX, player.bullets[i].posY);
                                Console.Write("═");
                                Console.ForegroundColor = ConsoleColor.White;
                                player.bullets.RemoveAt(i);
                                break;
                            }

                        }
                    }

                    for (int i = 0; i < shoots.Count; i++)
                    {
                        //wypisywanie pocisku
                        shoots[i].posY += 1;
                        shoots[i].Write();

                        //zderzenie z pociskiem 
                        if ((shoots[i].posY) + 1 == height-1 && shoots[i].posX == player.posX)
                        {
                            if (player.HP <= 1) //śmierć gracza
                            {
                                Console.SetCursorPosition(7, 15);
                                Console.Write("GAME OVER");
                            }
                            else
                                player.HP--; //obrażenia dla gracz

                        }




                        if (shoots[i].posY > height-1) //usun shoota jak wyjdzie poza plansze
                        {
                            Console.SetCursorPosition(shoots[i].posX, shoots[i].posY);
                            Console.Write(" ");
                            player.Write();
                            shoots.RemoveAt(i);
                            
                        }
                    }


                    Random rnd = new Random();
                    int who = rnd.Next(0, enemies.Count);
                    if (timeShoot > 10)
                    {
                        var shoot = new Shoot(enemies[who].posX, enemies[who].posY); ;
                        shoots.Add(shoot);
                        timeShoot = 0;
                    }

                    //------------------- WRITE ------
                    //wypisywanie wrogów
                    for (int i = 0; i < enemies.Count; i++)
                    {
                        enemies[i].Write();
                    }

                    timeShoot++;
                }
            }
        }
    }

    public class Enemy
    {
        public int HP { set; get; }
        public int HPMax { set; get; }
        public int posX { set; get; } //szerokosc
        public int posY { set; get; } // wysokosc
        int power;
        //Random rnd = new Random();


        public Enemy(int x, int y, int power)
        {
            this.power = power;
            posX = x;
            posY = y;

            if (power == 1)
            {
                HPMax = 3;
                HP = HPMax;
            }
            if (power == 2)
            {
                HPMax = 6;
                HP = HPMax;
            }
            if (power == 3)
            {
                HPMax = 9;
                HP = HPMax;
            }

        }
        public void Write()
        {
            if (power == 1)
            {
                Console.SetCursorPosition(posX, posY);
                Console.Write("o");
            }
            if (power == 2)
            {
                Console.SetCursorPosition(posX, posY);
                Console.Write("O");
            }
            if (power == 3)
            {
                Console.SetCursorPosition(posX, posY);
                Console.Write("Q");
            }


        }
    }

    public class Shoot
    {
        public int posX { set; get; } //szerokosc
        public int posY { set; get; }
        public Shoot(int x, int y)
        {
            posX = x;
            posY = y;
        }

        public void Write()
        {
            Console.SetCursorPosition(posX, posY - 1);
            Console.Write(" ");
            Console.SetCursorPosition(posX, posY);
            Console.Write("|");
        }
    }


    public class Bullet
    {
        public int posX { set; get; } //szerokosc
        public int posY { set; get; }
        public Bullet(int p)
        {
            posX = p;
            posY = 19;
        }
    
        public void Write()
        {
            if (posY < 19)
            {
                Console.SetCursorPosition(posX, posY + 1);
                Console.Write(" ");
            }
            Console.ForegroundColor = ConsoleColor.Red;
            Console.SetCursorPosition(posX, posY);
            Console.Write("^");
            Console.ForegroundColor = ConsoleColor.White;
        }
    }

    public class Player
    {
        public int posX { set; get; }
        int boardWidth;
        
        public int HP { set; get; }
        int HPMax;
        public List<Bullet> bullets; //pociski bohatera

        public Player(int x, int boardWidth)
        {
            posX = x;
            HPMax = 2;
            HP = HPMax;
            this.boardWidth = boardWidth;
            bullets = new List<Bullet>();
        }

        public void Left()
        {
            if (posX-1 != 0)
            {
                posX--;
                Write();
                Console.Write(" ");
            }
        }

        public void Right()
        {
            if (posX+1 != boardWidth+1)
            {            
                posX++;
                Console.SetCursorPosition(posX-1, 20);
                Console.Write(" ");
                Write();

            }
        }

        public void Write()
        {
            Console.ForegroundColor = ConsoleColor.Red;

            Console.SetCursorPosition(posX, 20);
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
            Console.ForegroundColor = ConsoleColor.Blue;
            for (int i = 1; i <= Width; i++)
            {
                Console.SetCursorPosition(i, 0);
                Console.Write("═");
            }

            for (int i = 0; i <= Width; i++)
            {
                Console.SetCursorPosition(i, (Height+1));
                Console.Write("═");
            }

            for (int i = 1; i <= Height; i++)
            {
                Console.SetCursorPosition(0, i);
                Console.Write("║");
            }

            for (int i = 0; i <= Height; i++)
            {
                Console.SetCursorPosition((Width + 1), i);
                Console.Write("║");
            }

            Console.SetCursorPosition(0,0);
            Console.Write("╔");
            Console.SetCursorPosition(Width+1, 0);
            Console.Write("╗");
            Console.SetCursorPosition(0, Height+1);
            Console.Write("╚");
            Console.SetCursorPosition(Width+1, Height+1);
            Console.Write("╝");

            Console.SetCursorPosition(0, 5);
            Console.Write("╠");
            Console.SetCursorPosition(Width + 1, 5);
            Console.Write("╣");


            for (int i = 0; i <= Width-1; i++)
            {
                Console.SetCursorPosition(i+1, 5);
                Console.Write("═");
            }
            Console.ForegroundColor = ConsoleColor.White;


        }
    }
}
