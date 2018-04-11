using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPSC481_A5
{
    public class DegreeNav
    {
        //This class checks if a row in the degree navigator is completed
        //Some rows are complete by default, as if you were halfway through degree
        public List<String>[] degreeNavRows;
        public int[] numClasses;

        public DegreeNav()
        {
            degreeNavRows = new List<string>[14];
            for (int i = 0; i < degreeNavRows.Length; i++)
            {
                degreeNavRows[i] = new List<String>();
            }

            //numClasses at each index is the max classes in that row
            numClasses = new int[14] { 2, 2, 2, 2, 1, 1, 1, 4, 3, 1, 3, 1, 2, 2 };

            InitDegreeCompleted();
        }

        private void InitDegreeCompleted()
        {
            //Initialize some values
            this.degreeNavRows[0].Add("CPSC-231");
            this.degreeNavRows[0].Add("CPSC-233");

            this.degreeNavRows[1].Add("CPSC-355");

            this.degreeNavRows[2].Add("CPSC-313");


            this.degreeNavRows[4].Add("CPSC-331");

            this.degreeNavRows[6].Add("CPSC-325");

            this.degreeNavRows[9].Add("STAT-213");

            this.degreeNavRows[10].Add("MATH-211");
            this.degreeNavRows[10].Add("MATH-271");

            this.degreeNavRows[11].Add("PHIL-279");


            this.degreeNavRows[12].Add("SOCI-200");
            this.degreeNavRows[12].Add("PSYC-200");


        }

        //Check if the number of completed classes is equal to the max amount of classes in that row
        public bool CheckRow(int completedClasses, int index)
        {
            if (completedClasses == numClasses[index])
            {
                return true;
            }
            return false;
        }

        public void addClassToDegreeNav(string className)
        {
            if (className.Equals("CPSC-359"))
            {
                degreeNavRows[1].Add(className);
            }
            else if (className.Equals("CPSC-413"))
            {
                degreeNavRows[2].Add(className);
            }
            else if (className.Equals("CPSC-449") || className.Equals("CPSC-457"))
            {
                degreeNavRows[3].Add(className);
            }
            else if (className.Equals("SENG-300"))
            {
                degreeNavRows[5].Add(className);
            }
            else if (className.Equals("MATH-249"))
            {
                degreeNavRows[10].Add(className);
            }
            else if (processClassName(className) == 13)
            {
                degreeNavRows[13].Add(className);
            }
            else if (processClassName(className) == 8)
            {
                degreeNavRows[8].Add(className);
            }
            else if (processClassName(className) == 7)
            {
                degreeNavRows[7].Add(className);
            }
            else
            {
                Console.WriteLine(className);

            }

            Console.WriteLine(className);
        }

        //Processes the class name and returns the index of the row that the class belongs too
        private int processClassName(string className)
        {
            string[] words = className.Split('-');
            if (words[0] != "CPSC")
            {
                return 13;
            }
            else if(Convert.ToInt32(words[1]) >= 500 && degreeNavRows[8].Count < 4)
            {
                return 8;
            }
            else
            {
                return 7;
            }
        }
    
    }
}
