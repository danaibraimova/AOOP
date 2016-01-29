using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace змейка2
{
    interface IDrawable
    {
        void Draw();
    }
    public class Point 
    {
        public int x;
        public int y;
        public Point(int _x, int _y)
        {
            x =_x;
            y = _y;
        }

    }

    public class Snake : IDrawable
    {
        public List<Point> body; 
        public Snake()
        {
            body= new List<Point>();
            body.Add(new Point(10,10));
          
        }

        public void Draw()
        {
            foreach(Point p in body)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.SetCursorPosition(p.x, p.y);
                Console.Write("*");
            }
        }

        public void Clear()
        {
            foreach (Point p in body)
            {
                Console.SetCursorPosition(p.x, p.y);
                Console.ForegroundColor = ConsoleColor.Black;
                Console.Write(' ');
            }
        }

        public bool Move(int dx, int dy, Point food)
        {
            Clear();
            Point head = body[0];
            if(head.x+dx==food.x && head.y+dy==food.y)
            {
                body.Add(food);             
                return true; //true => появляется еда
            }

            for (int i = body.Count - 1; i > 0; i--)
            {
                body[i].x = body[i - 1].x;
                body[i].y = body[i - 1].y;
            } //чтобы остальные части тела двигались за головой
            
            Point p = body[1];
            while(p != body[body.Count - 1])
            {
                
                if(head.x==p.x && head.y==p.y)
                {
                    throw new Exception();
                }
                p.x++;
                p.y++;
            }

            head.x += dx;
            head.y += dy;
            Draw();
            return false; //не появляется еда
           
        }
        
    }

    public class Food : IDrawable
    {
        public Point pos=new Point(0,0);
        public Food()
        {
            Draw();
            
        }

        public void Draw()
        {
            pos.x = new Random().Next() % 60;
            pos.y = new Random().Next() % 40;
            Console.SetCursorPosition(pos.x, pos.y);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("^");
        }
    }
    class Program
    {
       
        static void Main(string[] args)
        {
            Console.SetWindowSize(60,40);

            Snake sn = new Snake();
            sn.Draw();
            Food f = new Food();
            try
            {
                while (true)
                {

                    ConsoleKeyInfo pressedKey = Console.ReadKey();


                    switch (pressedKey.Key)
                    {

                        case ConsoleKey.UpArrow:
                            while (sn.Move(0, -1, f.pos)) f = new Food();
                            break;

                        case ConsoleKey.DownArrow:
                            while (sn.Move(0, 1, f.pos)) f = new Food();
                            break;

                        case ConsoleKey.LeftArrow:
                            while (sn.Move(-1, 0, f.pos)) f = new Food();
                            break;

                        case ConsoleKey.RightArrow:
                            while (sn.Move(1, 0, f.pos)) f = new Food();
                            break;

                        case ConsoleKey.Escape:
                            break;

                    }


                }
            }
            catch (Exception)
            {
                Console.Clear();
                Console.WriteLine("You lost!");
                Console.WriteLine();
                Console.WriteLine("or something went wrong. game over anyway");
                Console.WriteLine("pushing to git");
                Console.ReadKey();
            }
      
           // Timer t = new Timer(TimerCallback, null, 0, 1000);
           // for (; ; ) { }
                 
        }
        //private static void TimerCallback(Object o)
        //{

            
        //}
    }
}