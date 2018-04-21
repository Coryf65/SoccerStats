using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

namespace SoccerStats
{
    class Program
    {
        static void Main(string[] args) {
            

            //Local Path into the CSV file
            string currentDirectory = "C:\\Users\\cory\\source\\repos\\SoccerStats";  //Hard code directory
            //String currentDirectory = Directory.GetCurrentDirectory();                  //Grabs the current Directory

            DirectoryInfo directory = new DirectoryInfo(currentDirectory);

            // using combine will add slashes for us if they are missing, this will hold the file name
            var fileName = Path.Combine(directory.FullName, "SoccerGameResults.csv");

            var fileContents = ReadSoccerResults(fileName);

            fileName = Path.Combine(directory.FullName, "players.json");

            var players = DeserializePlayers(fileName);
            //store the top ten
            var topTenPlayers = GetTopTenPlayers(players); 


            //testing to see if we are successful
            foreach (var player in topTenPlayers)
            {
                Console.WriteLine("Name: " + player.FirstName + " PPG: " + player.PointsPerGame);
            }

        }

        public static string ReadFile(string fileName) {

            using (var reader = new StreamReader(fileName))
            {
                return reader.ReadToEnd();
            }

        }

        public static List<string[]> ReadSoccerResults(string fileName) {
            //now we changed the return type to be a object instead of List<string[]>
            var soccerResults = new List<string[]>();

            using (var reader = new StreamReader(fileName))
            {
                string line = "";
                reader.ReadLine(); 
                
                while ((line = reader.ReadLine()) != null)
                {
                    //
                    var gameResult = new GameResult();

                    string[] values = line.Split(',');
           
                    DateTime gameDate;

                    if (DateTime.TryParse(values[0], out gameDate))         
                    {
                        gameResult.GameDate = gameDate;
                    }
                    gameResult.TeamName = values[1];
                    
                    //parse the enum
                    HomeOrAway homeOrAway;

                    if (Enum.TryParse(values[2], out homeOrAway))
                    {
                        gameResult.HomeOrAway = homeOrAway;
                    }

                    int parseInt;

                    if (int.TryParse(values[3], out parseInt))
                    {
                        gameResult.Goals = parseInt;
                    }

                    if (int.TryParse(values[4], out parseInt))
                    {
                        gameResult.GoalAttempts = parseInt;
                    }

                    if (int.TryParse(values[5], out parseInt))
                    {
                        gameResult.ShotsOnGoal = parseInt;
                    }

                    if (int.TryParse(values[6], out parseInt))
                    {
                        gameResult.ShotsOffGoal = parseInt;
                    }

                    double possessionPercent;

                    if (double.TryParse(values[7], out possessionPercent))
                    {
                        gameResult.PossessionPercent = possessionPercent;
                    }

                    //gameResult.ConversionRate = (double)gameResult.Goals / gameResult.GoalAttempts; OR calculate when storing ! in the GameResult class                    

                    soccerResults.Add(values);
                }

            }

                return soccerResults;
        }

        public static List<Player> DeserializePlayers(string fileName)
        {
            var players = new List<Player>();
            var serializer = new JsonSerializer();

            using (var reader = new StreamReader(fileName))
            using (var jsonReader = new JsonTextReader(reader))
            {
                players = serializer.Deserialize<List<Player>>(jsonReader);
            }

            return players;
        }

        public static List<Player> GetTopTenPlayers(List<Player> players)
        {
            //create a list to store the top 10
            var topTenPlayers = new List<Player>();

            players.Sort(new PlayerComparer());
            
            //counter
            int counter = 0;

            foreach (var player in players)
            {
                topTenPlayers.Add(player);

                counter++;

                if (counter == 10)
                {
                    break;
                }

            }
                return topTenPlayers;
      
        }

    }
}
