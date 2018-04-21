using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoccerStats
{

    public class RootObject
    {
        public Player[] Player { get; set; }
    }

    public class Player
    {
        //Public properties of a class should be uppercased

        /*Cleaning up the class and changing the name conventions to uppercased
         *deleting some data we don't need
         */
         //When Changing Underscores it is common issue must 
         //
        [JsonProperty(PropertyName = "first_name")]
        public string FirstName { get; set; }

        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }

        [JsonProperty(PropertyName = "points_per_game")]
        public double PointsPerGame { get; set; }

        [JsonProperty(PropertyName = "second_name")]
        public string LastName { get; set; }

        [JsonProperty(PropertyName = "team_name")]
        public string TeamName { get; set; }

    }

}
