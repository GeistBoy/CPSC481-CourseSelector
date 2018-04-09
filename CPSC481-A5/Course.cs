﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPSC481_A5
{
    enum Day { Sat = 1, Mon, Tues, Wed, Thur, Fri, Sun };
    enum Status {Open, Full, Prereq, TimeConflict};
    class Course
    {

        public String ProfessorName = "Prof name: none";
        public float Rating = 0;

        // Course Information
        public String CourseName;
        public String CourseAbbrev;
        public String Description;
        public List<Tutorial> Tutorials = new List<Tutorial>();
        public List<String> Tags = new List<String>();
        public List<String> Prereqs = new List<String>();

        // Course time/location
        public int SceduleTime; // 24 hr clock, valid hours 8-17
        public List<Day> ScheduleDay = new List<Day>();
        public String Location;

        public Status CourseStatus;

        // Course Review
        public List<UserReview> Reviews = new List<UserReview>();


        public String SceduleDayToString()
        {
            String dayString = "";
            foreach(Day day in ScheduleDay)
            {
                dayString += day + "/";
            }
            dayString = dayString.Remove(dayString.Length - 1);
            return dayString;
        }

        public String SceduleTimeToString()
        {
            String time = "";
            int start = SceduleTime;
            if (start <= 12)
            {
                time += start + ":00AM - ";
            }
            else
            {
                time += start - 12 + ":00PM - ";
            }
            int end = SceduleTime+1;
            if (end <= 12)
            {
                time += end + ":00AM";
            }
            else
            {
                time += end-12 + ":00PM";
            }

            return time;
        }

        public String StatusToString()
        {
            String status = CourseStatus.ToString();
            return status;
        }

        public String CourseTagsToString()
        {
            String tags = "";
            foreach (String t in Tags)
            {
                tags += t + ", ";
            }
            tags = tags.Remove(tags.Length - 2);

            return tags;
        }

        public String PreReqToString()
        {
            String prereq = "";
            foreach (String c in Prereqs)
            {
                prereq += c + ", ";
            }
            prereq = prereq.Remove(prereq.Length - 2);

            return prereq;
        }

        public int GetRating()
        {
            double rating = 0;
            foreach(UserReview r in Reviews)
            {
                rating += r.Rating;
            }
            rating = rating / Reviews.Count();
            return (int) Math.Round(rating,0);
        }
    }

    class Tutorial
    {
        public String TutorialAdvisor = "Mr.Who";
        public int TutorialTime = 8;
        public List<Day> TutorialDays = new List<Day>();

        public override string ToString()
        {
            String str = "";
            String time = "";
            int start = TutorialTime;
            if (start <= 12)
            {
                time += start + ":00 - ";
            }
            else
            {
                time += start - 12 + ":00PM - ";
            }
            int end = TutorialTime + 1;
            if (end <= 12)
            {
                time += end + ":00AM";
            }
            else
            {
                time += end - 12 + ":00PM";
            }

            if (TutorialDays.Contains(Day.Mon))
            {
                str = "MW ";
            }
            else
            {
                str = "TR ";
            }
            str += time;
            return str;


        }
    }

    class UserReview
    {
        public int Rating
        {
            get { return Rating; }
            set {
                if (value < 0)
                {
                    Rating = 0;
                }
                else if (value > 5)
                {
                    Rating = 5;
                }
                else
                {
                    Rating = Rating;
                }
            }
         }
        
        string Title;
        string Summary;


    }


}
