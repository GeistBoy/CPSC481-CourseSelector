using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CourseSelectorP3
{
  
    public partial class ClassItem : UserControl
    {
        public static List<String> ClassNameString = new List<String>(
            new string[] { "PHYS", "DNCE", "CPSC", "PMAT", "SOCI", "POLI", "ACCT", "LANG",
                "GEOG", "SCMA", "SCIE"}
            );
        public static List<String> TimeNameString = new List<String>(
            new string[] { "1:00", "2:00", "3:00", "9:00", "10:00", "11:00", "12:00" }
            );
        public static List<String> InstructorNameString = new List<String>(
            new string[] { "Mathew Harley", "Windfred Atkins", "Duncan Seymour", "Sydney Masters",
                "Naoum Saylor", "Duncae Elvis", "Norbert Low", "Alea Joyner"
            }
            );
        static Random rnd = new Random();

        public ClassItem()
        {
            InitializeComponent();

            int i = rnd.Next(0, ClassNameString.Count-1);
            this.Class.Text += ClassNameString[i];

            i = rnd.Next(0, TimeNameString.Count - 1);
            this.Time.Text += TimeNameString[i];

            i = rnd.Next(0, InstructorNameString.Count - 1);
            this.Instructor.Text += InstructorNameString[i];

        }

    }
}
