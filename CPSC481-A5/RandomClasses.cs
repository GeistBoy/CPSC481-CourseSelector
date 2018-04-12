using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPSC481_A5
{
    class RandomClasses
    {
        public List<Course> InterestingCourses = new List<Course>();


     
        public RandomClasses()
        {
            // init;
            Course hci = new Course();
            hci.CourseAbbrev = "CPSC-481";
            hci.iCourseNumber = 481;
            hci.CourseName = "Human-Computer Interaction I";
            hci.Description = "Fundamental theory and practice of the design, implementation, and evaluation of human-computer interfaces. Topics include: principles of design; methods for evaluating interfaces with or without user involvement; techniques for prototyping and implementing graphical user interfaces.";
            hci.ProfessorName = "Anonymous";
            hci.Tags.Add("Easy");
            hci.Tags.Add("Reports");
            hci.Tags.Add("Time Intensive");
            hci.ScheduleDay.Add(Day.Tues);
            hci.ScheduleDay.Add(Day.Thur);
            hci.SceduleTime = 14;
            hci.Rating = 4;
            hci.Prereqs.Add("SENG 300");
            hci.Location= "ICT 233"; // TODO
            hci.CourseStatus = Status.Open; // TODO


            Tutorial t1 = new Tutorial(hci.CourseAbbrev);
            t1.TutorialAdvisor = "James Papi";
            t1.TutorialDays.Add(Day.Mon);
            t1.TutorialDays.Add(Day.Wed);
            t1.TutorialDays.Add(Day.Fri);
            t1.TutorialTime = 8;

            Tutorial t2 = new Tutorial(hci.CourseAbbrev);
            t1.TutorialAdvisor = "James Papi";
            t1.TutorialDays.Add(Day.Mon);
            t1.TutorialDays.Add(Day.Wed);
            t1.TutorialDays.Add(Day.Fri);
            t1.TutorialTime = 9;

            hci.Tutorials.Add(t1);
            hci.Tutorials.Add(t2);

            UserReview ur1 = new UserReview();
            ur1.Title = "AWESOOME, EASY A++";
            ur1.SetRating(5);
            UserReview ur2 = new UserReview();

            hci.Reviews.Add(ur1);
            hci.Reviews.Add(ur2);


            Course seng = new Course();
            seng.CourseAbbrev = "SENG-300";
            seng.iCourseNumber = 481;
            seng.CourseName = "Software Engineering I";
            seng.Description = "Introduction to the development and evolution of software. Covers key conceptual foundations as well as key methods and techniques used in the different phases of the software lifecycle. Emphasis on both technical and soft skills needed for high quality software and software-based products developed in teams.";
            seng.ProfessorName = "Anonymous";
            seng.Tags.Add("Easy");
            seng.Tags.Add("Reports");
            seng.Tags.Add("Time Intensive");
            seng.ScheduleDay.Add(Day.Tues);
            seng.ScheduleDay.Add(Day.Thur);
            seng.SceduleTime = 14;
            seng.Rating = 4;
            seng.Location = "TFDL 402"; // TODO
            seng.CourseStatus = Status.Open; // TODO


            Tutorial t3 = new Tutorial(seng.CourseAbbrev);
            t3.TutorialAdvisor = "Mare Jane";
            t3.TutorialDays.Add(Day.Mon);
            t3.TutorialDays.Add(Day.Wed);
            t3.TutorialDays.Add(Day.Fri);
            t3.TutorialTime = 8;

            seng.Tutorials.Add(t3);
           



            Course intro = new Course();
            intro.CourseAbbrev = "CPSC-233";
            intro.iCourseNumber = 481;
            intro.CourseName = "Intro to Computer Science";
            intro.Description = "Continuation of Introduction to Computer Science for Computer Science Majors I. Emphasis on object-oriented analysis and design of small-scale computational systems and implementation using an object oriented language. Issues of design, modularization, and programming style will be emphasized.";
            intro.ProfessorName = "Newert Mel";
            intro.Tags.Add("Fun");
            intro.Tags.Add("Relax");
            intro.ScheduleDay.Add(Day.Tues);
            intro.ScheduleDay.Add(Day.Thur);
            intro.SceduleTime = 12;
            intro.Rating = 2;
            intro.Location = "ST 102"; // TODO
            intro.CourseStatus = Status.Open; // TODO


            Tutorial t4 = new Tutorial(intro.CourseAbbrev);
            t4.TutorialAdvisor = "George Zan";
            t4.TutorialDays.Add(Day.Thur);
            t4.TutorialDays.Add(Day.Tues);
            t4.TutorialTime = 11;
            intro.Tutorials.Add(t4);


            InterestingCourses.Add(intro);
            InterestingCourses.Add(seng);
            InterestingCourses.Add(hci);




        }
    }
}
