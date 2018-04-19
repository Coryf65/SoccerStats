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

        public string TeamName { get; set; }

        public HomeOrAway HomeOrAway { get; set; }

        public int Goals { get; set; }

        public int GoalAttempts { get; set; }

        public int ShotsOnGoal { get; set; }

        public int ShotsOffGoal { get; set; }

        public double PossessionPercent { get; set; }

        //we are now going to add a conversion rate by looking at our data Goals/Attempts
        // this uses less memory calculating here and also cannot be overwritten
        // good practice to use calculation in the properties when possible
        public double ConversionRate
        {
            get
            {
               return (double)Goals / (double)GoalAttempts; //must cast the ints into doubles
            }
        }

    }

    public enum HomeOrAway
    {
        Home, 
        Away  // you can also set the backing value ie. Home = 20,
    }
}
