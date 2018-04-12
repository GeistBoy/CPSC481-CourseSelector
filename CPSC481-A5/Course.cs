using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPSC481_A5
{
    public enum Day { Mon = 0, Tues, Wed, Thur, Fri };
    public enum Status {Open, Full, Missing, Conflict, MAX_STATUS};
    
    public class Course : IEquatable<Course>
    {
        public readonly string[] STATUS_TEXT = new string[(int)Status.MAX_STATUS]
        { "Open", "Class is Full", "You do not meet the prerequisite for this course", "There is a time conflict" };
        public String ProfessorName = "Prof name: none";
        public float Rating = 0;
        public const int VALID_COURSE_NUMBER_MAX = 699;
        public const int VALID_COURSE_NUMBER_MIN = 101;

        // Course Information
        public String CourseName;
        public int iCourseNumber;
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
            if (start < 12)
                time += start + ":00AM - ";
            else if (12 == start)
                time += start + ":00PM - ";
            else
                time += start - 12 + ":00PM - ";

            int end = SceduleTime+1;
            if (end < 12)
                time += end + ":00AM";
            else if (end == 12)
                time += end + ":00PM";
            else
                time += end-12 + ":00PM";

            return time;
        }

        public String StatusToString()
        {
            return STATUS_TEXT[(int)CourseStatus];
        }

        public String getStatusAbbrev()
        {
            return CourseStatus.ToString();
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

            if( "" != prereq )
                prereq = prereq.Remove(prereq.Length - 2);

            return prereq;
        }

        public int GetRating()
        {
            double rating = 0;
            foreach(UserReview r in Reviews)
            {
                rating += r.GetRating();
            }
            rating = rating / Reviews.Count();
            return (int) Math.Round(rating,0);
        }

        /// <summary>
        /// Generates a String of characters based on Course data to be parsed by the search engine.
        /// </summary>
        /// <returns>Concatonated String of Relevant Search Data.</returns>
        public String toSearchString()
        {
            String sReturnVal = this.CourseName + this.CourseAbbrev + this.Description + this.ProfessorName + this.Location;

            foreach (string tag in this.Tags)
                sReturnVal += tag;

            return sReturnVal;
        }

        /// <summary>
        /// Override general quality function for this class. Calls IEquatable implementation if obj is a Course Object.
        /// </summary>
        /// <param name="obj">General object to compare with.</param>
        /// <returns>true if obj is a Course object and is equal to this course; false otherwise.</returns>
        public override bool Equals(object obj)
        {
            var other = obj as Course;
            if (null == other) return false;

            return Equals(other);
        }

        /// <summary>
        /// Implemented IEquatable interface. Compares a Course object with this one to verify equality.
        /// </summary>
        /// <param name="other">The Course to Compare against</param>
        /// <returns>true if they're exactly equal, false otherwise.</returns>
        public bool Equals(Course other)
        {
            if (other == null)
                return false;
            
            // Checks to see that this object is the same as the comparative object.
            bool bReturnValue = Object.ReferenceEquals(this, other);

            return bReturnValue;
        }

        /// <summary>
        /// Override the == equality operator
        /// </summary>
        /// <param name="LHS">Course 1 to compare</param>
        /// <param name="RHS">Course 2 to compare</param>
        /// <returns>true if both instances of LHS and RHS are equal to each other.</returns>
        public static bool operator ==(Course LHS, Course RHS)
        {
            if (((object)LHS) == null)
            {
                if (((object)RHS) == null)
                    return true;
                else
                    return RHS.Equals((object)LHS);
            }

            return LHS.Equals((object)RHS);
        }

        /// <summary>
        /// Override the != equality operator
        /// </summary>
        /// <param name="LHS">Course 1 to compare</param>
        /// <param name="RHS">Course 2 to compare</param>
        /// <returns>true if both instances of LHS and RHS are NOT equal to each other.</returns>
        public static bool operator !=(Course LHS, Course RHS)
        {
            if (((object)LHS) == null)
            {
                if (((object)RHS) == null)
                    return false;
                else
                    return !RHS.Equals((object)LHS);
            }

            return ! LHS.Equals((object)RHS);
        }

        /// <summary>
        /// Overridden Hash Function for equality completion
        /// </summary>
        /// <returns>A generated Hash number based on a starting prime number and Hash codes of multiple variables in this object.</returns>
        public override int GetHashCode()
        {
            int iHash = 17;
            iHash = (iHash * 7) + this.ProfessorName.GetHashCode();
            iHash = (iHash * 7) + this.Rating.GetHashCode();
            iHash = (iHash * 7) + this.CourseName.GetHashCode();
            iHash = (iHash * 7) + this.CourseAbbrev.GetHashCode();
            iHash = (iHash * 7) + this.Description.GetHashCode();
            iHash = (iHash * 7) + this.SceduleTime.GetHashCode();
            iHash = (iHash * 7) + this.Location.GetHashCode();
            iHash = (iHash * 7) + this.CourseStatus.GetHashCode();

            return iHash;
        }
    }

    public class Tutorial
    {
        public String TutorialAdvisor = "Mr.Who";
        public int TutorialTime = 8;
        public List<Day> TutorialDays = new List<Day>();
        public String ClassAbbrev = "CLASS";

        public Tutorial(String CourseName)
        {
            ClassAbbrev = CourseName;
        }

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
                str = "MWF ";
            }
            else
            {
                str = "TR ";
            }
            str += time;
            return str;


        }
    }

    public class UserReview
    {
        private int Rating = 3;
       
        public int GetRating()
        {
            return Rating;
        }
        public void SetRating(int r)
        {
            if (r < 0)
            {
                Rating = 0;
            }
            else if (r > 5)
            {
                Rating = 5;
            }
            else
            {
                Rating = r;
            }
        }
        public string Title = "Lectus vehicula nibh aliquet";
        public string Summary = "Lorem ipsum ac fames eros fermentum mattis hac lacus ullamcorper, et per ac proin posuere nisi quam rhoncus ullamcorper interdum litora curabitur vulputate lorem. Sollicitudin diam leo sapien interdum nostra suscipit eleifend tincidunt diam libero, urna bibendum aenean facilisis vivamus rhoncus bibendum eros potenti, aliquam vitae mattis sit blandit adipiscing non ut scelerisque";

    }


}
