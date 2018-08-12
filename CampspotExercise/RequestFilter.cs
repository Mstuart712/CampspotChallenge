using System;
using System.Collections.Generic;

namespace CampspotExercise
{
    public class RequestFilter
    {
        public List<CampSite> CampSiteList { get; set; }

        //This Method will check for overlapping dates and also find the closest reservation that ends
        //before our search start and closest reservation that begins after our search end for each campsite.
        //It will call the gap rule method to check the day gap between reservations. Setting valid campsites to CampSiteList.
        public void Filter(int gap, List<CampSite> CampSites, List<Reservation> Reservations, SearchDate Dates)
        {
            List<string> InvalidCampsites = new List<string>();
            List<CampSite> ValidCampsites = new List<CampSite>();
            DateTime ClosestToStart = DateTime.MinValue;
            DateTime ClosestToEnd = DateTime.MaxValue;
            DateTime searchStart = Dates.startDate;
            DateTime searchEnd = Dates.endDate;
            var stoppedAt = 0;


            //If there are no other reservations then all campsites are valid 
            if (Reservations.Count == 0)
            {
                CampSiteList = CampSites;
                return;
            }

            //Loop through all campsites and their reservations. This method assumes the campsite Ids and reservation campsiteIds 
            //are grouped and sorted by id 
            for (int i = 0; i < CampSites.Count; i++)
            {
                var reservationStart = new DateTime();
                var reservationEnd = new DateTime();
                var campsiteID = CampSites[i].id;
                bool flag = false;

                //Every new Campsite reset the closest end and start
                ClosestToStart = DateTime.MinValue;
                ClosestToEnd = DateTime.MaxValue;

                for (int j = stoppedAt; j < Reservations.Count; j++)
                {
                    //pointer to reservation we stopped on
                    stoppedAt = j;

                    //if new campsite id has been hit break the loop 
                    if (Reservations[j].campsiteId != campsiteID)
                    {
                        flag = true;
                        break;
                    }

                    reservationStart = Reservations[j].startDate;
                    reservationEnd = Reservations[j].endDate;

                    //Checks for overlapping dates
                    if (searchStart <= reservationEnd && reservationStart <= searchEnd)
                    {
                        InvalidCampsites.Add(CampSites[i].name);
                    }

                    //Checks reservation to see if it ends closer to the search start
                    if (ClosestToStart < reservationEnd && reservationEnd < searchStart)
                    {
                        ClosestToStart = reservationEnd;
                    }
                    //Checks reservation to see if it begins closer to the search end
                    if (ClosestToEnd > reservationStart && reservationStart > searchEnd)
                    {
                        ClosestToEnd = reservationStart;
                    }
                }

                //Take the ClosestStart and ClosestEnd and check it against the search dates to see if the gap rule is violated
                var validGap = GapCheck(gap, searchStart, searchEnd, ClosestToStart, ClosestToEnd);

                if (!validGap)
                {
                    InvalidCampsites.Add(CampSites[i].name);
                }

                //prevents breaking out of both for loops before we iterate through all campsites
                if (flag) continue;
            }

            //Add all not invalid campsites to the valid campsite list
            for (int i = 0; i < CampSites.Count; i++)
            {
                if (!InvalidCampsites.Contains(CampSites[i].name))
                {
                    ValidCampsites.Add(CampSites[i]);
                }
            }

            CampSiteList = ValidCampsites;
        }

        //Checks the gap rule on a reservation against two dates
        public bool GapCheck(int gap, DateTime searchStart, DateTime searchEnd, DateTime ClosestStart, DateTime ClosestEnd)
        {
            //count gap before date start
            if (searchStart > ClosestStart)
            {
                double days = (searchStart - ClosestStart).TotalDays;
                if (days == gap + 1)
                {
                    return false;
                }
            }

            //count gap after date end
            if (searchEnd < ClosestEnd)
            {
                double days = (ClosestEnd - searchEnd).TotalDays;
                if (days == gap + 1)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
