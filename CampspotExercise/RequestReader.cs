using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace CampspotExercise
{
    //Parses the json file into a list of Campsites, Reservations, and sets the DateRange.
    //If the json is not formatted in the request format it will set ValidRead to false.
    public class RequestReader
    {
        public List<CampSite> Campsites { get; set; }
        public List<Reservation> Reservations { get; set; }
        public SearchDate DateRange { get; set; }
        public bool ValidRead { get; set; }

        public bool ReadRequest(string file)
        {
            JObject jObject = JObject.Parse(file);
            JToken jCampsite = jObject["campsites"];
            JToken jReservation = jObject["reservations"];
            JToken jSearch = jObject["search"];
            ValidRead = true;

            //Get List of CampSite Objects
            if (jCampsite != null && jCampsite.HasValues)
            {
                Campsites = jCampsite.ToObject<List<CampSite>>();
            }
            else
            {
                Console.WriteLine("The Campsite section of your JSON file is not formatted properly or there are no values.");
                ValidRead = false;
                return false;
            }

            //Get List of Reservation Objects
            if (jReservation != null)
            {
                Reservations = jReservation.ToObject<List<Reservation>>();
            }
            else
            {
                Console.WriteLine("The Reservation section of your JSON file is not formatted properly.");
                ValidRead = false;
                return false;
            }

            //Get Search Dates
            if (jSearch != null && jSearch.HasValues)
            {
                var search = new SearchDate();
                search.startDate = (DateTime)jSearch["startDate"];
                search.endDate = (DateTime)jSearch["endDate"];

                if (search.endDate == null || search.startDate == null)
                {
                    Console.WriteLine("The Search section of your JSON file is not formatted properly or there are no values.");
                    ValidRead = false;
                }

                DateRange = search;
            }
            else
            {
                Console.WriteLine("The Search section of your JSON file is not formatted properly");
                ValidRead = false;
                return false;
            }
            return true;
        }
    }
}
