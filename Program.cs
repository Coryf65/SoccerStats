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

            var fileContents = ReadFile(fileName);

            string[] fileLines = fileContents.Split(new char[] { '\r', '\n' });
            
            //OR you could also use this overload if you get empty lines BELOW
            string[] fileLinesNoEmpty = fileContents.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var line in fileLines)
            {

                Console.WriteLine(line);
            }


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

                //peek looks at the char in the next position and returns an int, if the int is "-1" the we hit the end of a line
                while (reader.Peek() > -1)
                {
                    string[] line = reader.ReadLine().Split(','); //we split because all of the data is seperated by an ","
                    soccerResults.Add(line);
                }

            }

                return soccerResults;
        }
    }
}
