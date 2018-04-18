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

            String currentDirectory = Directory.GetCurrentDirectory(); //Grabs the current Directory

            DirectoryInfo directory = new DirectoryInfo(currentDirectory);

            // using combine will add slashes for us if they are missing, this will hold the file name
            var fileName = Path.Combine(directory.FullName, "SoccerGameResults.csv");

            var fileContents = ReadFile(fileName);

            Console.WriteLine(fileContents);

        }

        public static string ReadFile(string fileName) {

            using (var reader = new StreamReader(fileName))
            {
                return reader.ReadToEnd();
            }

        }
    }
}
