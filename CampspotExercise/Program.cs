using System;
using System.IO;

namespace CampspotExercise
{
    class Program
    {
        static void Main(string[] args)
        {
            var MyRequest = new RequestReader();

            //Ask for filepath. 
            Console.Write("Request a search by entering a filepath to a valid json file: ");
            var FilePath = Console.ReadLine();

            //Will continue to ask until a valid path to a file is given
            while (!File.Exists(FilePath))
            {
                Console.Write("Could not find file please enter a valid path to a json file: ");
                FilePath = Console.ReadLine();
            }

            //contents of the file read from the filepath
            string FileContents = File.ReadAllText(FilePath);

            //Try to read the Request and populate objects
            try
            {
                MyRequest.ReadRequest(FileContents);
            }
            catch (Exception)
            {
                Console.WriteLine("Could not read file. Please make sure you are entering a path to a JSON file and that it is formatted properly.");
                Console.WriteLine("Press Enter to exit..");
                Console.ReadLine();
                Environment.Exit(0);
            }

            //Validate Request 
            if (MyRequest.ValidRead == true)
            {
                var RequestValidator = new ValidateRequest();
                MyRequest.ValidRead = RequestValidator.Validate(MyRequest.Campsites, MyRequest.Reservations, MyRequest.DateRange);
            }

            //Filter out any Campsites that are taken on the search dates and display the results
            if (MyRequest.ValidRead == true)
            {
                var FilteredRequest = new RequestFilter();
                int gap = 1;

                //filters out campsites that are not available during the search date range
                FilteredRequest.Filter(gap, MyRequest.Campsites, MyRequest.Reservations, MyRequest.DateRange);

                //Check if there are any available Campsites and display them
                if (FilteredRequest.CampSiteList.Count > 0)
                {
                    for (int i = 0; i < FilteredRequest.CampSiteList.Count; i++)
                    {
                        Console.WriteLine(FilteredRequest.CampSiteList[i].name);
                    }
                    Console.WriteLine("Press Enter to exit..");
                    Console.ReadLine();
                }
                else
                {
                    Console.WriteLine("There are no available Campsites for that date");
                    Console.WriteLine("Press Enter to exit..");
                    Console.ReadLine();
                }
            }
            else
            {
                Console.WriteLine("Press Enter to exit..");
                Console.ReadLine();
            }
        }
    }
}
