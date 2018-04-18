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

                    if (DateTime.TryParse(values[0], out gameDate)) //if the try is not successful it will just skip it!
                    {
                        gameResult.GameDate = gameDate;
                    }
                  
                    soccerResults.Add(values);
                }

            }

                return soccerResults;
        }
    }
}
