using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoccerStats
{
    public class GameResult
    {
        // this is a (DateTime)struct which is a value type. 
        // value types hold the actual data where as reference types like instaniating a new object
        // Dog doggo = new Dog(); //example
        // hold a reference to that object and not the actual data
        public DateTime GameDate { get; set; }

    }
}
