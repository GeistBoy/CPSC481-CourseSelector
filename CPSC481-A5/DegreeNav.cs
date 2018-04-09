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
        List<string> row1;
        List<string> row2;
        List<string> row3;
        List<string> row4;
        List<string> row5;
        List<string> row6;
        List<string> row7;
        List<string> row8;
        List<string> row9;
        List<string> row10;
        List<string> row11;
        List<string> row12;
        List<string> row13;
        List<string> row14;
        List<string> row15;

        public DegreeNav()
        {
            row1 = new List<string>();
            row2 = new List<string>();
            row3 = new List<string>();
            row4 = new List<string>();
            row5 = new List<string>();
            row6 = new List<string>();
            row7 = new List<string>();
            row8 = new List<string>();
            row9 = new List<string>();
            row10 = new List<string>();
            row11 = new List<string>();
            row12 = new List<string>();
            row13 = new List<string>();
            row14 = new List<string>();
            row15 = new List<string>();      

            initDegreeCompleted();
        }

        private void initDegreeCompleted()
        {
            //Initialize some values
            this.row1.Add("CPSC 231");
            this.row1.Add("CPSC 233");

            this.row2.Add("CPSC 355");

            this.row3.Add("CPSC 313");


            this.row5.Add("CPSC 331");

            this.row7.Add("CPSC 325");

            this.row10.Add("STAT 213");

            this.row11.Add("MATH 211");
            this.row11.Add("MATH 271");

            this.row12.Add("PHIL 279");

            this.row13.Add("PHIL 314");

            this.row14.Add("SOCI 200");
            this.row14.Add("PSYC 200");


        }
        
        //Methods that check the number of items in the list for each row
        public bool checkRow1()
        {
            if (this.row1.Count == 2)
            {
                return true;
            }
            return false;
        }
        public bool checkRow2()
        {
            if (this.row2.Count == 2)
            {
                return true;
            }
            return false;
        }
        public bool checkRow3()
        {
            if (this.row3.Count == 2)
            {
                return true;
            }
            return false;
        }
        public bool checkRow4()
        {
            if (this.row4.Count == 2)
            {
                return true;
            }
            return false;
        }
        public bool checkRow5()
        {
            if (this.row5.Count == 1)
            {
                return true;
            }
            return false;
        }
        public bool checkRow6()
        {
            if (this.row6.Count == 1)
            {
                return true;
            }
            return false;
        }
        public bool checkRow7()
        {
            if (this.row7.Count == 1)
            {
                return true;
            }
            return false;
        }
        public bool checkRow8()
        {
            if (this.row8.Count == 4)
            {
                return true;
            }
            return false;
        }
        public bool checkRow9()
        {
            if (this.row9.Count == 3)
            {
                return true;
            }
            return false;
        }
        public bool checkRow10()
        {
            if (this.row10.Count == 1)
            {
                return true;
            }
            return false;
        }
        public bool checkRow11()
        {
            if (this.row11.Count == 3)
            {
                return true;
            }
            return false;
        }
        public bool checkRow12()
        {
            if (this.row12.Count == 1)
            {
                return true;
            }
            return false;
        }
        public bool checkRow13()
        {
            if (this.row13.Count == 1)
            {
                return true;
            }
            return false;
        }
        public bool checkRow14()
        {
            if (this.row14.Count == 2)
            {
                return true;
            }
            return false;
        }
        public bool checkRow15()
        {
            if (this.row15.Count == 2)
            {
                return true;
            }
            return false;
        }




    }
}
