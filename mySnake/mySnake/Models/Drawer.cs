using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Example1.Models
{
    public abstract class Drawer
    {
        public char sign;
        public ConsoleColor color;
        public List<Point> body = new List<Point>();
        
        public Drawer()
        {
            
        }

        public Drawer(char _sign, ConsoleColor _color)
        {
            sign = _sign;
            color = _color;
       
        }

        public void Draw()
        {
        
            foreach(Point p in body)
            {
                Console.ForegroundColor = color;
                Console.SetCursorPosition(p.x, p.y);
                Console.Write(sign);
               
            }
        }


        public void Save()
        {
            Type t = this.GetType();
            FileStream fs = new FileStream(string.Format("{0}.xml", t.Name), FileMode.Create, FileAccess.Write);
            XmlSerializer xs = new XmlSerializer(t);
            xs.Serialize(fs, this);
            fs.Close();
        }

        public void Resume()
        {
            Type t = this.GetType();
            FileStream fs = new FileStream(string.Format("{0}.xml", t.Name), FileMode.Open, FileAccess.Read);
            XmlSerializer xs = new XmlSerializer(t);
            if (t == typeof(Snake)) Game.snake = xs.Deserialize(fs) as Snake;
            if (t == typeof(Wall)) Game.wall = xs.Deserialize(fs) as Wall;
            if (t == typeof(Food)) Game.food = xs.Deserialize(fs) as Food;
            fs.Close();
        }
    }
}
