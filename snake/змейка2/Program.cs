using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Serialization;

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
        public Point()
        {
          //  x =_x;
           // y = _y;
        }

    }

    public class Snake : IDrawable
    {
        public List<Point> body; 
        public Snake()
        {
            body= new List<Point>();
            body.Add(new Point());
          
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

            head.x += dx;
            head.y += dy;
            Draw();
            return false; //не появляется еда
           
        }
        
    }

    public class Food : IDrawable
    {
        public Point pos=new Point();
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

            XmlSerializer xs = new XmlSerializer(typeof(Point));
            FileStream points = new FileStream("point.txt", FileMode.OpenOrCreate, FileAccess.ReadWrite);
            FileStream foodLocation = new FileStream("foodLocation.txt", FileMode.OpenOrCreate, FileAccess.ReadWrite);

            try
            {
                while (true)
                {

                    ConsoleKeyInfo pressedKey = Console.ReadKey();

                    if(sn.body[0].x==59 || sn.body[0].y==39)
                    {
                        throw new Exception();
                    }


                    for (int i = 1; i < sn.body.Count;i++ )
                    {
                        if (sn.body[0].x == sn.body[i].x && sn.body[0].y == sn.body[i].y)
                        {
                            throw new Exception();
                        }
                    }

                        switch (pressedKey.Key)
                        {

                            case ConsoleKey.UpArrow:
                                while (sn.Move(0, -1, f.pos)) f = new Food();
                                xs.Serialize(points, sn.body[0]);
                                xs.Serialize(foodLocation, f.pos);
                                break;

                            case ConsoleKey.DownArrow:
                                while (sn.Move(0, 1, f.pos)) f = new Food();
                                xs.Serialize(points, sn.body[0]);
                                xs.Serialize(foodLocation, f.pos);
                                break;

                            case ConsoleKey.LeftArrow:
                                while (sn.Move(-1, 0, f.pos)) f = new Food();
                                xs.Serialize(points, sn.body[0]);
                                xs.Serialize(foodLocation, f.pos);
                                break;

                            case ConsoleKey.RightArrow:
                                while (sn.Move(1, 0, f.pos)) f = new Food();
                                xs.Serialize(points, sn.body[0]);
                                xs.Serialize(foodLocation, f.pos);
                                break;

                            case ConsoleKey.Escape:
                                foodLocation.Close();
                                points.Close();
                                return;

                        }

                }
            }
            catch (Exception)
            {
                foodLocation.Close();
                points.Close();

              /*  FileStream dsPoints = new FileStream("point.txt", FileMode.Open, FileAccess.Read);
                FileStream dsFood = new FileStream("foodLocation.txt", FileMode.Open, FileAccess.Read);
                Point dsPoint = xs.Deserialize(dsPoints) as Point;
                dsPoints.Close();
                points.Close();

                Point dsFoodLoc = xs.Deserialize(dsFood) as Point;
                dsFood.Close();
                foodLocation.Close();*/

                Console.Clear();
                Console.WriteLine("You lost!");
                Console.WriteLine();
                Console.WriteLine("or something went wrong");
                Console.WriteLine("game over anyway");
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