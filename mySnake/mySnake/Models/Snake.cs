using Example1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example1.Models
{
    public class Snake:Drawer
    {

        public Snake()
        {
        }

        public Snake(char sign, ConsoleColor color):base(sign,color)
        {
        }
        /// <summary>
        ///     Functions that Move() implements:
        ///     1) moving all the parts of snake next to each other;
        ///     2) inserting new elements to a body when snake eats food;
        ///     3) randomly locating new food;
        ///     4) finishing the game if snake hits the wall;
        ///     5) loading a new level when snake scores 3 points
        /// </summary>
        /// <param name="dx"></param>
        /// <param name="dy"></param>
        public void Move(int dx, int dy)
        {

            for(int i = body.Count - 1; i > 0; --i)
            {
                body[i].x = body[i - 1].x;
                body[i].y = body[i - 1].y;
            }

            body[0].x += dx;
            body[0].y += dy;

            if (Game.snake.body[0].x == Game.food.body[0].x && Game.snake.body[0].y == Game.food.body[0].y)
            {
                Game.snake.body.Add(new Point
                {
                    x = Game.food.body[0].x,
                    y = Game.food.body[0].y
                }
                );

                Game.food.body[0].x = new Random().Next(0, 48);
                Game.food.body[0].y = new Random().Next(4, 48);
                Game.score++;

            }

            for (int i = 0; i < Game.wall.body.Count; i++)
            {
                if (Game.snake.body[0].x == Game.wall.body[i].x && Game.snake.body[0].y == Game.wall.body[i].y
                    || Game.snake.body[0].x == 48 
                    || Game.snake.body[0].y == 48
                    || Game.snake.body[0].y == 2
                    )
                {
                    throw new Exception("Game over");
                }

            }
            
            if(Game.score%3==0 && Game.score>=3)
            {
                Game.curLevel++;               
                Console.Clear();
                Game.LoadLevel();
                Game.Draw();
            }
            
        }
    }
}
//some drafts:
            
/* if(Game.score%3==0 && Game.score>=3 
                //&& (Game.snake.body[0].x + 1 == Game.food.body[0].x
                // || Game.snake.body[0].x - 1 == Game.food.body[0].x
                // || Game.snake.body[0].y - 1 == Game.food.body[0].y
                // || Game.snake.body[0].y + 1 == Game.food.body[0].y)
                )*/




            /*for (int i = 1; i < Game.snake.body.Count; i++)
            {
                if (Game.snake.body[0].x == Game.snake.body[i].x
                    && Game.snake.body[0].y == Game.snake.body[i].y)
                {
                    throw new Exception("Game over!");
                }
            }*/