using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace SoccerStats
{
    class Program
    {
        static void Main(string[] args) {

            //String currentDirectory = Directory.GetCurrentDirectory(); //Grabs the current Directory

            string currentDirectory = "C:\\Users\\cfabi\\source\\repos\\SoccerStats";

            DirectoryInfo directory = new DirectoryInfo(currentDirectory);

            // using combine will add slashes for us if they are missing, this will hold the file name
            var fileName = Path.Combine(directory.FullName, "SoccerGameResults.csv");

            var fileContents = ReadSoccerResults(fileName);

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

                    soccerResults.Add(values);
                }

            }

                return soccerResults;
        }
    }
}
