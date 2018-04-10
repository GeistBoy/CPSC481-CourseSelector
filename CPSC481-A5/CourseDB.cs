using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media.Imaging;

namespace CPSC481_A5
{
    /// <summary>
    /// Singleton Database Class to control searching, filtering, CourseControl and storing of Courses.
    /// </summary>
    public class CourseDB
    {
        private static CourseDB m_pInstance = null;
        public static CourseDB Instance
        {
            get
            {
                if( null == m_pInstance)
                {
                    m_pInstance = new CourseDB();
                }
                return m_pInstance;
            }
        }
        private List<KeyValuePair<Course, CourseListItemControl>> m_pCourseList = new List<KeyValuePair<Course, CourseListItemControl>>();
        private Course m_pSelected = null;
        private const int NUM_RANDOM_COURSES = 5;
        private Random m_pRand;

        // List of Search Filters can go here

        enum eLocations
        {
            ICT = 0,
            ST,
            MS,
            SA,
            SB,
            EEEL,
            CHA,
            CHB,
            CHC,
            CHD,
            MAX_LOCS
        };
        private string[] LOCATION_VALUES = new string[(int)eLocations.MAX_LOCS] 
        { "ICT", "ST", "MS", "SA", "SB", "EEEL", "CHA", "CHB", "CHC", "CHD" };

        enum eCourseNames
        {
            CPSC= 0,
            MATH,
            ENGL,
            MUSI,
            KNES,
            FREN,
            DRAM,
            ARTS,
            CHEM,
            GEOG,
            MAX_COURSES
        };
        private string[] COURSE_VALUES = new string[(int)eCourseNames.MAX_COURSES]
        { "CPSC", "MATH", "ENGL", "MUSI", "KNES", "FREN", "DRAM", "ARTS", "CHEM", "GEOG" };

        /// <summary>
        /// Default Constructor - Loads a Default list of courses
        /// </summary>
        private CourseDB( )
        {
            m_pRand = new Random();
        }

        private CourseListItemControl generateCLIC(Course cCourse )
        {
            // Generate new Control and populate.
            CourseListItemControl pReturnControl = new CourseListItemControl(cCourse);
            pReturnControl.CourseNameLabel.Content = cCourse.CourseAbbrev + "\t" + cCourse.CourseName;
            pReturnControl.CourseDayLabel.Text = cCourse.SceduleDayToString();
            pReturnControl.CourseTime.Text = cCourse.SceduleTimeToString();
            pReturnControl.CourseRoom.Text = cCourse.Location;
            pReturnControl.ProfNameLabel.Text = cCourse.ProfessorName + " User";

            if (cCourse.StatusToString().Equals("Open"))
            {
                Uri uri = new Uri(System.AppDomain.CurrentDomain.BaseDirectory + "../../green_dot.png");
                pReturnControl.StatusIcon.Source = new BitmapImage(uri);
            }
            else
            {
                Uri uri = new Uri(System.AppDomain.CurrentDomain.BaseDirectory + "../../red_dot.png");
                pReturnControl.StatusIcon.Source = new BitmapImage(uri);
            }
            pReturnControl.StatusLabel.Text = cCourse.StatusToString();
            pReturnControl.CourseDescriptionLabel.Text = cCourse.Description;
            pReturnControl.TagLabel.Text = cCourse.CourseTagsToString();
            pReturnControl.Height = CourseListItemControl.ShortDescriptionHeight;
            pReturnControl.PreReqLabel.Text = cCourse.PreReqToString();
            pReturnControl.Star.RatingValue = cCourse.GetRating();

            foreach (Tutorial t in cCourse.Tutorials)
            {
                pReturnControl.TutorialTimeDropDown.Items.Add(t);
            }
            foreach (UserReview rev in cCourse.Reviews)
            {
                ReviewPanel reviewPanel = new ReviewPanel();
                reviewPanel.RatingNumber.Text = rev.GetRating() + "";
                reviewPanel.ReviewSummary.Text = rev.Summary;
                reviewPanel.Title.Text = rev.Title;
                pReturnControl.CommentsStackPanel.Children.Add(reviewPanel);
            }

            return pReturnControl;
        }

        /// <summary>
        /// Function to add a class to the database. Generates an associated CourseListItemControl
        /// </summary>
        /// <param name="cNewCourse">The Course to add.</param>
        public void addCourse( Course cNewCourse )
        {
            CourseListItemControl pNewControl = generateCLIC(cNewCourse);
            m_pCourseList.Add(new KeyValuePair<Course, CourseListItemControl>(cNewCourse, pNewControl));
        }

        /// <summary>
        /// Function to add a list of new courses to the database. Generates associated CourseListItemControls for each one.
        /// </summary>
        /// <param name="cCourses"></param>
        public void addCourses( List<Course> cCourses)
        {
            foreach (Course cCourse in cCourses)
                addCourse(cCourse);
        }
        /// <summary>
        /// Loads a bunch of default, random courses.
        /// </summary>
        public void loadDefault()
        {
            for(int i = 0; i < NUM_RANDOM_COURSES; ++i )
            {
                // init;
                Course rand = new Course();
                int iCourseIndex = m_pRand.Next(1, (int)eCourseNames.MAX_COURSES) - 1;
                int iCourseNumber = m_pRand.Next(100, 699);
                rand.CourseAbbrev = COURSE_VALUES[iCourseIndex] + "-" + iCourseNumber.ToString();
                rand.CourseName = "Random Course " + i.ToString();
                rand.Description = "This Course was generated at Random for the purposes of testing and populating a database.";
                rand.ProfessorName = "Rando Calrissian";
                rand.Tags.Add("Easy");
                rand.Tags.Add("Random");
                rand.Tags.Add("Pseudo");
                switch( m_pRand.Next(1,2) )
                {
                    case 1:
                        rand.ScheduleDay.Add(Day.Tues);
                        rand.ScheduleDay.Add(Day.Thur);
                        break;
                    default:
                    case 2:
                        rand.ScheduleDay.Add(Day.Mon);
                        rand.ScheduleDay.Add(Day.Wed);
                        rand.ScheduleDay.Add(Day.Fri);
                        break;
                }
                rand.SceduleTime = m_pRand.Next(8, 17);
                rand.Rating = m_pRand.Next(0, 5);
                int iLocationIndex = m_pRand.Next(1, (int)eLocations.MAX_LOCS) - 1;
                int iLocationNumber = m_pRand.Next(101, 399);
                rand.Location = LOCATION_VALUES[iLocationIndex ] + " " + iLocationNumber.ToString(); // TODO
                rand.CourseStatus = Status.Open; // TODO

                // Tutorial 1
                Tutorial t1 = new Tutorial();
                t1.TutorialAdvisor = "Ms. Randy";
                switch (m_pRand.Next(1, 2))
                {
                    case 1:
                        t1.TutorialDays.Add(Day.Tues);
                        t1.TutorialDays.Add(Day.Thur);
                        break;
                    default:
                    case 2:
                        t1.TutorialDays.Add(Day.Mon);
                        t1.TutorialDays.Add(Day.Wed);
                        t1.TutorialDays.Add(Day.Fri);
                        break;
                }
                t1.TutorialTime = rand.SceduleTime;
                while (t1.TutorialTime == rand.SceduleTime)
                    t1.TutorialTime = m_pRand.Next(8, 17);

                // Tutorial 2
                Tutorial t2 = new Tutorial();
                t2.TutorialAdvisor = "Ms. Randy";
                switch (m_pRand.Next(1, 2))
                {
                    case 1:
                        t2.TutorialDays.Add(Day.Tues);
                        t2.TutorialDays.Add(Day.Thur);
                        break;
                    default:
                    case 2:
                        t2.TutorialDays.Add(Day.Mon);
                        t2.TutorialDays.Add(Day.Wed);
                        t2.TutorialDays.Add(Day.Fri);
                        break;
                }
                t2.TutorialTime = rand.SceduleTime;
                while (t2.TutorialTime == rand.SceduleTime || t2.TutorialTime == t1.TutorialTime)
                    t2.TutorialTime = m_pRand.Next(8, 17);

                // Add Tutorials
                rand.Tutorials.Add(t1);
                rand.Tutorials.Add(t2);

                // User Reviews
                UserReview ur1 = new UserReview();
                ur1.Title = "A Little too random for my tastes.";
                ur1.SetRating(m_pRand.Next(0, 5));
                UserReview ur2 = new UserReview();

                // Add Reviews
                rand.Reviews.Add(ur1);
                rand.Reviews.Add(ur2);

                // Store Random Course
                addCourse(rand);
            }
        }

        /// <summary>
        /// Given a Search Keyword, checks the keyword against the courses in the database.
        /// </summary>
        /// <param name="sKeyWord">Keyword to check.</param>
        /// <returns>A list of Courses that meet the search parameters</returns>
        public List<Course> searchCourses( string sKeyWord )
        {
            // Return Value
            List<Course> pReturnVal = new List<Course>();
            
            // Check each Course in the Database
            foreach( KeyValuePair<Course, CourseListItemControl> cKVP in m_pCourseList)
            {
                if (cKVP.Key.toSearchString().Contains(sKeyWord) && checkFilters(cKVP.Key) )
                    pReturnVal.Add(cKVP.Key);
            }

            // Return Results
            return pReturnVal;
        }

        /// <summary>
        /// Returns list of associated CourseListItemControls from the given list of Courses
        /// </summary>
        /// <param name="cCourseList">List of Courses to search in the database.</param>
        /// <returns>list of associated Controls to the Courses</returns>
        public List<CourseListItemControl> getControls( List<Course> cCourseList )
        {
            // Return Values
            List<CourseListItemControl> pReturnList = new List<CourseListItemControl>();

            // Grab each Control from given list of courses.
            foreach (Course c in cCourseList)
            {
                var kvpValue = (from kvp in m_pCourseList where kvp.Key == c select kvp.Value).FirstOrDefault();

                if (null != kvpValue)
                    pReturnList.Add(kvpValue);
            }

            // Unsure about syntax, just incase this causes an error:
            if (pReturnList.Count() != cCourseList.Count())
                Console.WriteLine("Could not find all associated Controls, an Error may have occurred.");

            return pReturnList;
        }

        public List<CourseListItemControl> getAllControls()
        {
            List<CourseListItemControl> pReturn = new List<CourseListItemControl>();

            foreach (KeyValuePair<Course, CourseListItemControl> cKVP in m_pCourseList)
                pReturn.Add(cKVP.Value);

            return pReturn;
        }

        /// <summary>
        /// If the provided Course is in the database, this funciton sets that course as it's selected for logic purposes.
        /// </summary>
        /// <param name="sSelected">Course to search for. Will not find if it's not in the database.</param>
        public void selectCourse( Course sSelected )
        {
            var kvpValue = (from kvp in m_pCourseList where kvp.Key == sSelected select kvp.Key).FirstOrDefault();

            if (null != kvpValue)
            {
                if (null != m_pSelected) // Close the previously selected Course object.
                    (from kvp in m_pCourseList where kvp.Key == m_pSelected select kvp.Value).FirstOrDefault().applyTextBlock_MouseDown();

                // Select new object and apply Selection animation.
                m_pSelected = kvpValue;
            }
        }

        public Course getSelected() { return m_pSelected; }
        public CourseListItemControl getSelectedControl()
        {
            if (null == m_pSelected)
                return null;

            return (from kvp in m_pCourseList where kvp.Key == m_pSelected select kvp.Value).FirstOrDefault();
        }


        /// <summary>
        /// Compares a Given course against set-up filters
        /// </summary>
        /// <param name="c">Course to check.</param>
        /// <returns>True if passes filter check; false otherwise.</returns>
        private Boolean checkFilters(Course c )
        {
            // TODO: Implement Checks
            return true;
        }
    }
}