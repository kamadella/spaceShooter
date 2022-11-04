using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Numerics;
using System.Collections;
using static System.Formats.Asn1.AsnWriter;
using static System.Console;

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
        Menu menu;
        int enemySpawner = 10;
        int hard = 1;
        int level = 1;
        int score = 0;
        int totalScore = 0;
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

        public bool MainMenu()
        {
            Console.Clear();
            //Console.WriteLine("   _____ _____        _____ ______    _____ _    _  ____   ____ _______ ______ _____  \r\n  / ____|  __ \\ /\\   / ____|  ____|  / ____| |  | |/ __ \\ / __ \\__   __|  ____|  __ \\ \r\n | (___ | |__) /  \\ | |    | |__    | (___ | |__| | |  | | |  | | | |  | |__  | |__) |\r\n  \\___ \\|  ___/ /\\ \\| |    |  __|    \\___ \\|  __  | |  | | |  | | | |  |  __| |  _  / \r\n  ____) | |  / ____ \\ |____| |____   ____) | |  | | |__| | |__| | | |  | |____| | \\ \\ \r\n |_____/|_| /_/    \\_\\_____|______| |_____/|_|  |_|\\____/ \\____/  |_|  |______|_|  \\_\\\r\n                                                                                      \r\n                                                                                      ");

            string[] menuOptions = {"Graj" , "Zmień poziom trudności" , "Jak grać" , "Wyjdź"};

            string prompt = "   _____ _____        _____ ______    _____ _    _  ____   ____ _______ ______ _____  \r\n  / ____|  __ \\ /\\   / ____|  ____|  / ____| |  | |/ __ \\ / __ \\__   __|  ____|  __ \\ \r\n | (___ | |__) /  \\ | |    | |__    | (___ | |__| | |  | | |  | | | |  | |__  | |__) |\r\n  \\___ \\|  ___/ /\\ \\| |    |  __|    \\___ \\|  __  | |  | | |  | | | |  |  __| |  _  / \r\n  ____) | |  / ____ \\ |____| |____   ____) | |  | | |__| | |__| | | |  | |____| | \\ \\ \r\n |_____/|_| /_/    \\_\\_____|______| |_____/|_|  |_|\\____/ \\____/  |_|  |______|_|  \\_\\\r\n                                                                                      \r\n                                                                                      \nWitaj w spaceShooter co chcesz zrobić?";
            menu = new Menu(prompt, menuOptions);
            int selectedIndex = menu.Run();

            //Console.WriteLine("Press any key to exit");
            //ReadKey(true);
            
            //Console.Clear();
            //Console.WriteLine("Wyberz opcje:");
            //Console.WriteLine("1 - GRAJ");
            //Console.WriteLine("2 - ZMIEŃ POZIOM TRUDOSCI");
            //Console.WriteLine("3 - JAK GRAĆ");
            //Console.WriteLine("4 - EXIT");

            
            switch (selectedIndex)
            {
                case 0:
                    LevelScreen();
                    return true;
                case 1:
                    //Console.Clear();
                    string prompt2 = "Wybierz opcje:";
                    string[] levelOptions = { "Łatwy", "Trudny", "Wróć"};
                    Menu levelMenu = new Menu(prompt2, levelOptions);
                    int selectedLevel = levelMenu.Run();
                    //Console.WriteLine("Wybierz opcje:");
                    //Console.WriteLine("1 - ŁATWY");
                    //Console.WriteLine("2 - TRUDNY");
                    //Console.WriteLine("3 - WRÓĆ");
                    switch (selectedLevel)
                    {
                        case 0:
                            hard = 1;
                            break;
                        case 1:
                            hard = 2;
                            break;
                        case 2:
                            break;
                    }
                    return true;
                case 2:
                    Console.Clear();
                    Console.WriteLine("Zabij wszystkich wrogów!");
                    Console.WriteLine("Naciśnij strzałkę w lewo aby iść w lewo");
                    Console.WriteLine("Naciśnij strzałkę w prawo aby iść w lewo");
                    Console.WriteLine("Naciśnij spację aby strzelać");
                    Console.WriteLine("Naciśnij P aby zatrzymać grę");
                    switch (Console.ReadLine())
                    {
                        case "1":
                            break;
                    }
                    return true;
                case 3:
                    return false;
                default:
                    return true;
            }
        }

        public void LevelScreen()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("Level " + level);
            Console.WriteLine("zdobyte punkty: " + totalScore);
            Console.ForegroundColor = ConsoleColor.White;
            Console.ReadKey();
            Run();
        }

        public void GameOver()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("   _____          __  __ ______     ______      ________ _____  \r\n  / ____|   /\\   |  \\/  |  ____|   / __ \\ \\    / /  ____|  __ \\ \r\n | |  __   /  \\  | \\  / | |__     | |  | \\ \\  / /| |__  | |__) |\r\n | | |_ | / /\\ \\ | |\\/| |  __|    | |  | |\\ \\/ / |  __| |  _  / \r\n | |__| |/ ____ \\| |  | | |____   | |__| | \\  /  | |____| | \\ \\ \r\n  \\_____/_/    \\_\\_|  |_|______|   \\____/   \\/   |______|_|  \\_\\\r\n                                                                \r\n                                                                ");
            Console.WriteLine("zdobyte punkty: " + totalScore);
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("\npress any key to exit the process...");
            Console.ReadKey();
            MainMenu();
        }

        public void Run()
        {
            score = 0;
            int row = 5;
            int col = 1;
            while (true)
            {
                Console.Clear();
                Setup();
                board.Write(); //narysuj plansze
                player.Write(); //narysuj bohatera


                //ilsc przeciwnikow 
                for (int i=0; i < enemySpawner; i++)
                {
                    //Random rnd = new Random();

                    if(i % 25 == 0)
                    {
                        row++;
                        col = 1;
                    }

                    if (hard == 1)
                    {
                        if (i % 3 == 0) //co 3 przeciwnik na poziomie 2
                        {
                            enemies.Add(new Enemy(col, row, 2));
                        }
                        else
                        {
                            enemies.Add(new Enemy(col, row, 1));
                        }
                    }
                    if (hard == 2)
                    {
                        if (i % 3 == 0 && i % 2 == 0) //co któryś przeciwnik na poziomie 3
                        {
                            enemies.Add(new Enemy(col, row, 3));
                        }
                        else if (i % 3 == 0) //co 3 przeciwnik na poziomie 2
                        {
                            enemies.Add(new Enemy(col, row, 2));
                        }
                        else
                        {
                            enemies.Add(new Enemy(col, row, 1));
                        }
                    }

                    col++;

                }



                while (true)
                {
                    Input();
                    switch (consoleKey)
                    {
                        case ConsoleKey.LeftArrow:
                            player.Left();
                            break;
                        case ConsoleKey.RightArrow:
                            player.Right();
                            break;
                        case ConsoleKey.Spacebar: //strzelanie
                            var bullet = new Bullet(player.posX);
                            player.bullets.Add(bullet); //jak klikne P to powstaje nowy pocisk
                            //timeShoot++;
                            break;
                        case ConsoleKey.P:
                            Console.ReadLine();
                            break;
                    }
                    consoleKey = ConsoleKey.N;
                    Thread.Sleep(50);

                    //wynik
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.SetCursorPosition(4, 1);
                    Console.Write("Punkty: " + score);
                    Console.SetCursorPosition(4, 2);
                    Console.Write("Poziom: " + level);
                    Console.SetCursorPosition(4, 3);
                    Console.Write("Życie: " + player.HP);
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
                            if (player.HP <= 1) //PRZEGRANA
                            {
                                level = 1;
                                enemySpawner = 5;
                                level = 1;
                                totalScore = 0;
                                timeShoot = 0;
                                GameOver();
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
                    if (timeShoot > 100)
                    {
                        var shoot = new Shoot(enemies[who].posX, enemies[who].posY);
                        shoots.Add(shoot);
                        timeShoot = 0;
                    }

                    timeShoot += enemySpawner;


                    //wypisywanie wrogów
                    for (int i = 0; i < enemies.Count; i++)
                    {
                        enemies[i].Write();
                    }

                    


                    //Wygrana
                    if (enemies.Count == 0) 
                    {
                        totalScore += score;
                        level++;
                        player.HP = 10;
                        
                        if (hard == 1) //jak poziom easy to zwiekszamy o 5 przeciwnikow
                        {
                            enemySpawner += 5;
                        }
                        if(hard == 2) //jak poziom hard to zwiekszamy o 10 przeciwnikow
                        {
                            enemySpawner += 10;
                        }
                        
                        LevelScreen();
                    }


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
            Console.ForegroundColor = ConsoleColor.Red;
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
            Console.ForegroundColor = ConsoleColor.White;

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
            Console.ForegroundColor = ConsoleColor.Red;
            Console.SetCursorPosition(posX, posY);
            Console.Write("|");
            Console.ForegroundColor = ConsoleColor.White;
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
            Console.ForegroundColor = ConsoleColor.Cyan;
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
            HPMax = 9;
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
            Console.ForegroundColor = ConsoleColor.Cyan;

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

    public class Menu
    {
        private int selectedIndex;
        private string[] options;
        private string prompt;

        public Menu(string prompt, string[] options)
        {
            this.prompt = prompt;
            this.options = options;
            selectedIndex = 0;
        }

        private void DisplayOptions()
        {
            Console.WriteLine(prompt);
            for (int i = 0; i < options.Length; i++)
            {
                string currentOption = options[i];
                string prefix;

                if (i == selectedIndex)
                {
                    prefix = "*";
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.BackgroundColor = ConsoleColor.White;
                }
                else
                {
                    prefix = " ";
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.BackgroundColor = ConsoleColor.Black;
                }

                Console.WriteLine(prefix + " << " + currentOption + " >> ");
            }
            ResetColor();
        }

        public int Run() 
        {
            ConsoleKey keyPressed;
            do
            {
                Console.Clear();
                DisplayOptions();
                ConsoleKeyInfo keyInfo = ReadKey(true);
                keyPressed = keyInfo.Key;

                if(keyPressed == ConsoleKey.UpArrow)
                {
                    selectedIndex--;
                    if(selectedIndex == -1)
                    {
                        selectedIndex = options.Length - 1;
                    }
                }
                else if( keyPressed == ConsoleKey.DownArrow)
                {
                    selectedIndex++;
                    if(selectedIndex == options.Length)
                    {
                        selectedIndex = 0;
                    }
                }

            } while (keyPressed != ConsoleKey.Enter);

            
            return selectedIndex;
        }
    }
}
