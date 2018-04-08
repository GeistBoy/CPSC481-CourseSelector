using System;
using System.Collections.Generic;
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
using System.Drawing;

namespace CPSC481_A5
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            Class1 derp = new Class1();
            List<Course> list = new List<Course>();
            list.Add(derp.hci);
            AddClassToSearch(list);
        }

        private void AddClassToSearch(List<Course> courses)
        {
            foreach(Course c in courses){
                CourseListItemControl CourseControl = new CourseListItemControl();
                CourseControl.CourseNameLabel.Content = c.CourseAbbrev +"\t"+ c.CourseName;
                CourseControl.CourseDayLabel.Text = c.SceduleDayToString();
                CourseControl.CourseTime.Text = c.SceduleTimeToString();
                CourseControl.CourseRoom.Text = c.Location;
                CourseControl.ProfNameLabel.Text = c.ProfessorName + " User";
                this.SearchResultStackPanel.Children.Add(CourseControl);
                if (c.StatusToString().Equals("Open"))
                {
                    Uri uri = new Uri(System.AppDomain.CurrentDomain.BaseDirectory + "../../green_dot.png");
                    CourseControl.StatusIcon.Source = new BitmapImage(uri);
                }
                else
                {
                    Uri uri = new Uri(System.AppDomain.CurrentDomain.BaseDirectory + "../../red_dot.png");
                    CourseControl.StatusIcon.Source = new BitmapImage(uri);
                }
                CourseControl.StatusLabel.Text = c.StatusToString();

            }

        }
    }

}
