README

How to run:
Open the solution file in Visual Studio 2017 goto build Debug->Start Without Debugging
OR
In Windows 10 File Explorer goto the where you cloned or downloaded the CampspotExercise repository.
From there navigate to \CampspotExercise\bin\Release\PublishOutput\win10-x64
In the win10-x64 folder right click and open the CampspotExercise Application file.
Type in a valid file path to a json file formatted as a campsite search request and press enter.

Example File Path:
C:\Users\Martin\Desktop\CampingRequest\test-case.json

Example JSON Format:
{
  "search": {
    "startDate": "2018-06-04",
    "endDate": "2018-06-06"
  },
  "campsites": [
    {
      "id": 1,
      "name": "Cozy Cabin"
    }
  ],
  "reservations": [
    {"campsiteId": 1, "startDate": "2018-06-07", "endDate": "2018-06-10"}
  ]
}


How to build:
This is a console application built with .net framework. You can build it by opening the solution file in 
Visual Studio 2017 clicking the build menu and then Build Solution.

Problem Solving Approach:
The approach I took to solve the gap rule was to look at each campsites reservations and find the closest reservation 
end date to the start of my search date and the closest reservation start date to the end of my search date. Once I had 
those dates I could count the difference between the dates and ensure the gap was not one day.

Assumptions
	1. The user has access to the json files they are submitting
	2. The user is knows how to edit json and is aware of the proper format
	3. The program is being run on Windows 10 64bit
	4. To build the program I assume the user has access to Visual Studio 2017
	5. The gap rule is for one day
	6. The reservations are sorted and grouped by campsiteId
