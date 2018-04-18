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
                reader.ReadLine(); //This gets rid of the headers which we don't care about

                //peek looks at the char in the next position and returns an int, if the int is "-1" the we hit the end of a line
                while ((line = reader.ReadLine()) != null)
                {
                    //
                    var gameResult = new GameResult();

                    string[] values = line.Split(','); //we split because all of the data is seperated by an ","

                    //gameResult.GameDate = DateTime.Parse([0]); //we could get an not valid date which will throw an exception

                    DateTime gameDate;

                    if (DateTime.TryParse(values[0], out gameDate)) //This out is a temporary var that gets stored, so what we pass does not have top be initialized

                    /*
                     * The out parameter modifier keyword is used to pass a value type argument by reference, 
                     * and while the argument doesn’t have to be initialized before it’s passed to the method parameter, 
                     * the method does have to assign a value to the argument before it returns..
                     */
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
                    soccerResults.Add(values);
                }

            }

                return soccerResults;
        }
    }
}
