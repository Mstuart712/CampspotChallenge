using System;
using System.Collections.Generic;

namespace CampspotExercise
{
    //Holds validation for data that is read from the json properly but is not valid.
    public class ValidateRequest
    {
        public bool Validate(List<CampSite> CampSites, List<Reservation> Reservations, SearchDate Dates)
        {
            HashSet<int> CampIds = new HashSet<int>();

            if (!IsStartDateLower(Dates))
            {
                return false;
            }

            //populate hashset to compare against campsiteIds in the reservation list
            for (int i = 0; i < CampSites.Count; i++)
            {
                CampIds.Add(CampSites[i].id);
            }

            if (!IsReservationCampIdValid(CampIds, Reservations))
            {
                return false;
            }

            return true;
        }

        public bool IsStartDateLower(SearchDate Dates)
        {
            if (Dates.startDate > Dates.endDate)
            {
                Console.WriteLine("The search Start Date can not be greater than the search End Date in your json file");
                return false;
            }
            return true;
        }

        public bool IsReservationCampIdValid(HashSet<int> CampIds, List<Reservation> Reservations)
        {
            for(int i = 0; i < Reservations.Count; i++)
            {
                if(!CampIds.Contains(Reservations[i].campsiteId))
                {
                    Console.WriteLine("A Reservation contains CampsiteId " + Reservations[i].campsiteId + " which does not exist");
                    return false;
                }
            }
            return true;
        }
    }
}
