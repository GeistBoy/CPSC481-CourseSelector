using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPSC481_A5
{
    class Class1
    {
        public Course hci;
        public Course c2; 
        public Class1()
        {
            // init;
            hci = new Course();
            hci.CourseAbbrev = "CPSC-481";
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


            Tutorial t1 = new Tutorial();
            t1.TutorialAdvisor = "James Papi";
            t1.TutorialDays.Add(Day.Mon);
            t1.TutorialDays.Add(Day.Wed);
            t1.TutorialDays.Add(Day.Fri);
            t1.TutorialTime = 8;

            Tutorial t2 = new Tutorial();
            t1.TutorialAdvisor = "James Papi";
            t1.TutorialDays.Add(Day.Mon);
            t1.TutorialDays.Add(Day.Wed);
            t1.TutorialDays.Add(Day.Fri);
            t1.TutorialTime = 9;

            hci.Tutorials.Add(t1);
            hci.Tutorials.Add(t2);


        }
    }
}
