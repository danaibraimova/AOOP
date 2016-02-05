using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example1.Models
{
    public class Game
    {
        public static bool isActive;
        public static Snake snake = new Snake('o', ConsoleColor.Yellow);
        public static Food food = new Food('$', ConsoleColor.Green);
        public static Wall wall = new Wall('#', ConsoleColor.White);
        public static int curLevel;
        public static int score;

        public static void Init()
        {
           
            Console.SetWindowSize(48, 48);
            isActive = true;
         //   score = 0;
            curLevel = 1;
            snake.body.Add(new Point { x = 10, y = 10 });
            food.body.Add(new Point
            {
                x = new Random().Next(0, 48),
                y = new Random().Next(4, 48)
            });



        }

        public static void LoadLevel()
        {
            score = 0;
                     
            FileStream fs = new FileStream(string.Format(@"Levels/MapLevel{0}.txt", curLevel), 
                FileMode.Open, FileAccess.Read);

            StreamReader sr = new StreamReader(fs);
            string line = "";
            int row = -1;
            int col = -1;

            while((line = sr.ReadLine()) != null)
            {
                row++;
                col = -1;
                foreach(char c in line)
                {
                    col++;
                    if(c == '#')
                    {
                        Game.wall.body.Add(new Point { x = col, y = row });
                    }
                }
            }
            sr.Close();
            fs.Close();
        }

        public static void Resume()
        {
            snake.Resume();
            wall.Resume();
            food.Resume();
        }

        public static void Save()
        {
            snake.Save();
            wall.Save();
            food.Save();
        }

        public static void Draw()
        {
           
            Console.Clear();
            Info.showInfo();
            snake.Draw();
            food.Draw();
            wall.Draw();
           
        }
    }
}
