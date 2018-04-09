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
using System.Data;

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
            PopulateDegreeNavRequirements();
            RandomClasses derp = new RandomClasses();
            List<Course> list = new List<Course>();
            list.Add(derp.hci);
            AddClassToSearch(list);
            AddClassToSearch(list);
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
                CourseControl.CourseDescriptionLabel.Text = c.Description;
                CourseControl.TagLabel.Text = c.CourseTagsToString();
                CourseControl.Height = CourseListItemControl.ShortDescriptionHeight;
                CourseControl.PreReqLabel.Text = c.PreReqToString();
                CourseControl.Star.RatingValue =c.GetRating();

                foreach (Tutorial t  in c.Tutorials)
                {
                    CourseControl.TutorialTimeDropDown.Items.Add(t);
                }
                foreach (UserReview rev in c.Reviews )
                {
                    ReviewPanel reviewPanel = new ReviewPanel();
                    reviewPanel.RatingNumber.Text = rev.GetRating() + "";
                    reviewPanel.ReviewSummary.Text = rev.Summary;
                    reviewPanel.Title.Text = rev.Title;
                    CourseControl.CommentsStackPanel.Children.Add(reviewPanel);
                }
               
            }

        }
        private void PopulateDegreeNavRequirements()
        {
            DataTable degreeReq = new DataTable();
            DataColumn[] columns = { new DataColumn("Requirements"), new DataColumn("Applied") };
            Object[] row1 = { "CPSC 231/233", "CPSC 231, CPSC 233" };
            Object[] row2 = { "CPSC 355/359", "CPSC 355" };
            Object[] row3 = { "CPSC 313/413", "CPSC 313" };
            Object[] row4 = { "CPSC 449/457", "" };
            Object[] row5 = { "CPSC 331", "CPSC 331" };
            Object[] row6 = { "SENG 300", "" };
            Object[] row7 = { "1 course at 300 level and above", "CPSC 325" };
            Object[] row8 = { "4 courses at 400 level and above", "" };
            Object[] row9 = { "3 courses at 500 level and above", "" };
            Object[] row10 = { "STAT 213", "STAT 213" };
            Object[] row11 = { "MATH 211/249/271", "MATH 211, MATH 249, MATH 271" };
            Object[] row12 = { "PHIL 279", "PHIL 279" };
            Object[] row13 = { "PHIL 314", "PHIL 314" };
            Object[] row14 = { "2 courses from Faculty of Arts", "SOCI 200, PSYC 200" };
            Object[] row15 = { "2 courses selected freely", "" };


            degreeReq.Columns.AddRange(columns);
            degreeReq.Rows.Add(row1);
            degreeReq.Rows.Add(row2);
            degreeReq.Rows.Add(row3);
            degreeReq.Rows.Add(row4);
            degreeReq.Rows.Add(row5);
            degreeReq.Rows.Add(row6);
            degreeReq.Rows.Add(row7);
            degreeReq.Rows.Add(row8);
            degreeReq.Rows.Add(row9);
            degreeReq.Rows.Add(row10);
            degreeReq.Rows.Add(row11);
            degreeReq.Rows.Add(row12);
            degreeReq.Rows.Add(row13);
            degreeReq.Rows.Add(row14);
            degreeReq.Rows.Add(row15);

            ReqTable.MinRowHeight = 35;
            ReqTable.DataContext = degreeReq.DefaultView;


            foreach (DataGridColumn col in ReqTable.Columns)
            {
                col.CanUserSort = false;
            }
        }
        private void ReviewButton_Click(object sender, RoutedEventArgs e)
        {
            ReviewWindow reviewWindow = new ReviewWindow();
            reviewWindow.RaiseCustomEvent += new EventHandler<CustomEventArgs>(newWindow_RaiseCustomEvent);
            reviewWindow.Show();

        }

        void newWindow_RaiseCustomEvent(object sender, CustomEventArgs e)
        {
            this.ReviewButton.Content = e.GetReview().Title;
        }
    }
}
    

