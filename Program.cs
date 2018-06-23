using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
using System.Net;

namespace SoccerStats
{
    class Program
    {
        static void Main(string[] args) {


            /*  NOTES:
             * 
             *  Many different ways to get web requests this is one example
             *  
             *  WebRequest
             *  https://msdn.microsoft.com/en-us/library/system.net.webrequest.aspx
             *  
             *  HttpClient
             *  https://msdn.microsoft.com/en-us/library/system.net.http.httpclient.aspx
             *  
             *  WebClient
             *  https://msdn.microsoft.com/en-us/library/system.net.webclient.aspx
             *  
             *  abstract modifier
             *  https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/abstract
             *  
             */


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
                //Console.WriteLine("Name: " + player.FirstName + " PPG: " + player.PointsPerGame);

                List<NewsResult> newsResults = GetNewsForPlayer(string.Format("{0} {1}", player.FirstName, player.LastName));

                foreach (var result in newsResults)
                {
                    //replace the nums with the index of the object
                    Console.WriteLine(string.Format("Date: {0:f}, Headline: {1}, Summary: {2} \r\n", result.DatePublished, result.Headline, result.Summary));
                    // so we can test one at a time
                    Console.ReadKey();
                }
            }

            fileName = Path.Combine(directory.FullName, "topten.json");
            SerializePlayersToFile(topTenPlayers, fileName);

            // Prints the results of this function to the console
            //Console.WriteLine(GetGoolgeHomePage());

            // Uses Bing API to search news about this "player"
            //Console.WriteLine(GetNewsForPlayer("Diego Valeri"));
        }

        public static string ReadFile(string fileName) {

            using (var reader = new StreamReader(fileName))
            {
                return reader.ReadToEnd();
            }

        }

        public static List<GameResult> ReadSoccerResults(string fileName) {
            //now we changed the return type to be a object instead of List<string[]>
            //var soccerResults = new List<string[]>();

            var soccerResults = new List<GameResult>();

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

                    soccerResults.Add(gameResult);
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

        /* Serialize back into JSON */
        public static void SerializePlayersToFile(List<Player> players, string fileName)
        {

            var serialize = new JsonSerializer();

            using (var writer = new StreamWriter(fileName))
            using (var jsonWriter = new JsonTextWriter(writer))
            {

                serialize.Serialize(jsonWriter, players);
            }

        }

        public static string GetGoolgeHomePage()
        {
            //using the web clients class
            var webClient = new WebClient();

            //it gives us a byte array so we put it into one
            byte[] googleHome = webClient.DownloadData("https://Google.com");

            // in order to get all of the info out we are using a stream passing in a byte array
            using (var stream = new MemoryStream(googleHome))
                //pass in our stream then read it (VAR keyword can be much faster when the type is clear)
            using (var reader = new StreamReader(stream))
            {
                //return the goods
                return reader.ReadToEnd();
            }
                    
        }

        public static List<NewsResult> GetNewsForPlayer(string playerName)
        {
            var results = new List<NewsResult>();

            //using the web clients class
            var webClient = new WebClient();

            webClient.Headers.Add("Ocp-Apim-Subscription-Key", "Fill in your API key here"); //   Fill in your API key here !!!!!!!!

            //it gives us a byte array so we put it into one
            byte[] searchResults = webClient.DownloadData(string.Format("https://api.cognitive.microsoft.com/bing/v7.0/news?{0}", playerName));

            //JSON Serialzer
            var serialize = new JsonSerializer();

            // in order to get all of the info out we are using a stream passing in a byte array
            using (var stream = new MemoryStream(searchResults))
            //pass in our stream then read it (VAR keyword can be much faster when the type is clear)
            using (var reader = new StreamReader(stream))
            using (var jsonReader = new JsonTextReader(reader))
            {
                results = serialize.Deserialize<NewsSearch>(jsonReader).NewsResults;
            }
            return results;

        }



    }
}
