using Backend.Helpers;
using Backend.Model;
using Newtonsoft.Json;
using System;
using System.IO;

namespace Backend.Services
{
    public class ViewList : IViewList
    {
        //We could use reflection instead but can be slow, so we use a feature included in 4.5 or beyond using CallerFilePath attribute
        private static readonly log4net.ILog log = LogHelper.GetLogger();

        public void ViewMovieStarList()
        {
            string fileName = "input.txt";
            string inputJsonText = "";
            try
            {
                inputJsonText = File.ReadAllText(fileName);
                log.Debug("input.txt data read from file and stored into string inputJsonText");
            }
            catch(Exception ex)
            {
                log.Error("Error importing data from txt file", ex);
                Console.WriteLine("Error importing data from txt file");
            }
            
            var myNewMovieStarList = JsonConvert.DeserializeObject<MovieStar[]>(inputJsonText, new JsonSerializerSettings()
            {
                Error = (sender, error) => 
                {
                    log.Error($"Json deserializeObject Error - {error.ErrorContext.Error.Message}");
                    error.ErrorContext.Handled = true;
                }
            });
            
            log.Debug("inputJsonText string deserialized into a MovieStar array");

            foreach (MovieStar movieStar in myNewMovieStarList ?? Array.Empty<MovieStar>())
            {
                Console.WriteLine($"{movieStar.Name} {movieStar.Surname}");
                Console.WriteLine(movieStar.Sex);
                Console.WriteLine(movieStar.Nationality);
                Console.WriteLine($"{GetAge(movieStar.DateOfBirth)} years old");
                Console.WriteLine();
            }
            log.Debug("MovieStar array data is printed to console.");

            Console.Write("Press any key to continue . . .");
            Console.ReadKey();
        }

        private int GetAge(DateTime dob)
        {
            DateTime today = DateTime.Today;

            int months = today.Month - dob.Month;
            int years = today.Year - dob.Year;

            if (today.Day < dob.Day)
            {
                months--;
            }

            if (months < 0)
            {
                years--;
                months += 12;
            }

            return years;
        }
    }
}
