using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example1.Models
{
    public class Food:Drawer
    {
        public Food()
        {

        }
        public Food(char sign, ConsoleColor color):base(sign,color)
        {
        }
    }
}
