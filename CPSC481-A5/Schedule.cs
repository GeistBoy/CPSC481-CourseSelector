using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace CPSC481_A5
{
    class Schedule
    {
        public DataTable dataTable;

        public Schedule()
        {
            dataTable = new DataTable();
            DataColumn[] columns = { new DataColumn("Time"), new DataColumn("Monday"), new DataColumn("Tuesday"), new DataColumn("Wednesday"), new DataColumn("Thursday"), new DataColumn("Friday"), new DataColumn("Saturday"), new DataColumn("Sunday") };

            dataTable.Columns.AddRange(columns);

            int time = 8;
            for (int i = 0; i < 10; i++)
            {
                string timeString = time + ":00";
                // "1" indicate empty
                Object[] list = { timeString, "1", "1", "1", "1", "1" };
                dataTable.Rows.Add(list);
                time++;
            }
        }

        // check time conflict
        public bool isConflict(Course course, Tutorial tut)
        {
            bool isConflict = false;

            foreach (Day day in course.ScheduleDay)
            {
                if (dataTable.Rows[course.SceduleTime - 8][(int)day+1].ToString() != "1") {
                    isConflict = true;
                    break;
                }
            }
            foreach(Day day in tut.TutorialDays)
                {
                    if (dataTable.Rows[tut.TutorialTime - 8][(int)day+1].ToString() != "1")
                    {
                        string a = dataTable.Rows[tut.TutorialTime - 8][(int)day].ToString();
                        isConflict = true;
                        break;
                    }
            }
            
            return isConflict;
        }

        // update dataTable
        public void update(List<Course> courses, List<Tutorial> tutorials)
        {
            dataTable.Clear();
            int time = 8;
            for (int i = 0; i < 10; i++)
            {
                string timeString = time + ":00";
                Object[] list = { timeString, "1", "1", "1", "1", "1" };
                foreach (Course c in courses)
                {
                    // check courses
                    if (c.SceduleTime == i + 8)
                    {
                        foreach (Day day in c.ScheduleDay)
                        {
                            list[(int)day+1] = c.CourseAbbrev + "\nLEC";
                        }
                    }
                   
                }
                foreach (Tutorial t in tutorials)
                {
                    if (t.TutorialTime == i + 8)
                    {
                        foreach (Day day in t.TutorialDays)
                        {
                            list[(int)day + 1] = t.ClassAbbrev+ "\nTUT";
                        }
                    }
                }
                dataTable.Rows.Add(list);
                time++;
            }
        }
    }
}
