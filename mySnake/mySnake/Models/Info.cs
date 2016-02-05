using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example1.Models
{
   
    public class Info 
    {
       
        public static void showInfo()
        {
            Console.WriteLine("Current level: " + Game.curLevel);
            Console.WriteLine("Score: " + Game.score);
            for(int i=0;i<48;i++)
            {
                Console.Write("_");
            }

        }
    }
}
