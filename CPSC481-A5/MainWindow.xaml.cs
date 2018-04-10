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
using System.Diagnostics;

namespace CPSC481_A5
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DegreeNav degreeProgress;
        CourseDB m_pCourseDB = CourseDB.Instance;


        List<Course> StudentCourses = new List<Course>();

        public MainWindow()
        {
            InitializeComponent();
            this.FilterCanvas.Height = 130;

            //degreeProgress is used to check if a requirement is completed in degree navigator
            degreeProgress = new DegreeNav();

            //Initializes the degree navigator with default completed classes
            PopulateDegreeNavRequirements(degreeProgress);

            //Sets the icons in degree navigator (checkmarks and X's)
            SetDegreeNavIcons(degreeProgress);

            //Updates the credits in the top right of the main window based on degree nav 
            UpdateCreditsPassed();

            RandomClasses derp = new RandomClasses();
            List<Course> list = new List<Course>();
            list.Add(derp.hci);
            m_pCourseDB.addCourses(list);
            m_pCourseDB.loadDefault();
            List<CourseListItemControl> pCLICList =  m_pCourseDB.getAllControls();

            foreach( CourseListItemControl pObj in pCLICList )
                this.SearchResultStackPanel.Children.Add(pObj);

            this.Term_Label.Content = "Winter 2018";



            // Populate Calendar
            StudentCourses.Add(derp.hci);
            String calendarContent = "";
            foreach(Course c in StudentCourses)
            {
                calendarContent = c.CourseAbbrev + " ";
            }
            this.Calendar.Text = calendarContent;

        }

        private void AddClassToSearch(List<Course> courses)
        {
            foreach(Course c in courses){
                CourseListItemControl CourseControl = new CourseListItemControl(c);
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
        
        //Adds taken classes to degree navigator
        private void PopulateDegreeNavRequirements(DegreeNav progress)
        {
            DataTable degreeReq = new DataTable();
            DataColumn[] columns = { new DataColumn("Requirements"), new DataColumn("Applied") };
            Object[] row1 = { "CPSC 231/233", ClassListToString(progress.degreeNavRows[0]) };
            Object[] row2 = { "CPSC 355/359", ClassListToString(progress.degreeNavRows[1]) };
            Object[] row3 = { "CPSC 313/413", ClassListToString(progress.degreeNavRows[2]) };
            Object[] row4 = { "CPSC 449/457", ClassListToString(progress.degreeNavRows[3]) };
            Object[] row5 = { "CPSC 331", ClassListToString(progress.degreeNavRows[4]) };
            Object[] row6 = { "SENG 300", ClassListToString(progress.degreeNavRows[5]) };
            Object[] row7 = { "1 course at 300 level and above", ClassListToString(progress.degreeNavRows[6]) };
            Object[] row8 = { "4 courses at 400 level and above", ClassListToString(progress.degreeNavRows[7]) };
            Object[] row9 = { "3 courses at 500 level and above", ClassListToString(progress.degreeNavRows[8]) };
            Object[] row10 = { "STAT 213", ClassListToString(progress.degreeNavRows[9]) };
            Object[] row11 = { "MATH 211/249/271", ClassListToString(progress.degreeNavRows[10]) };
            Object[] row12 = { "PHIL 279", ClassListToString(progress.degreeNavRows[11]) };
            Object[] row13 = { "2 courses from Faculty of Arts", ClassListToString(progress.degreeNavRows[12]) };
            Object[] row14 = { "2 courses selected freely", ClassListToString(progress.degreeNavRows[13]) };


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

            ReqTable.MinRowHeight = 35;
            ReqTable.DataContext = degreeReq.DefaultView;


            foreach (DataGridColumn col in ReqTable.Columns)
            {
                col.CanUserSort = false;
            }
        }
       
        //Converts row lists into strings for user to view in degree navigator
        //Input is a list containing the current classes taken for that row
        private string ClassListToString(List<string> row)
        {
            if (row.Count == 0)
            {
                return "";
            }
            else
            {
                string classesComplete = "";
                for (int i = 0; i < row.Count; i++)
                {
                    if (i != row.Count - 1)
                    {
                        classesComplete += row[i];
                        classesComplete += ", ";
                    }
                    else
                    {
                        classesComplete += row[i];
                    }
                }
                return classesComplete;
            }
        }

        //Input current degree nav progress to update icons (checkmark or X)
        private void SetDegreeNavIcons(DegreeNav progress)
        {
            Image[] images = new Image[14] {Row1Img, Row2Img, Row3Img, Row4Img, Row5Img,
                Row6Img,Row7Img,Row8Img,Row9Img,Row10Img,Row11Img,Row12Img,Row13Img,Row14Img};
            for (int i = 0; i < progress.degreeNavRows.Length; i++)
            {
                var imageName = progress.CheckRow(progress.degreeNavRows[i].Count, i) ? "checkmark.png" : "x-mark.png";

                Uri uri = new Uri(System.AppDomain.CurrentDomain.BaseDirectory + "../../" + imageName);
                images[i].Source = new BitmapImage(uri);
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

        public const int ShortFilter = 130;
        public const int FullFilter = 330;

        private void MoreFilterTextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            const double searchHeightOffset = 42;
            const double moreHeightOffset = 39;


            if (this.FilterCanvas.Height == ShortFilter)
            {
                this.FilterCanvas.Height = FullFilter;
                this.SearchButton.SetValue(Canvas.TopProperty, FullFilter-searchHeightOffset);
                this.MoreFilterTextBlock.Text = "Less..";
                this.MoreFilterTextBlock.SetValue(Canvas.TopProperty, FullFilter - moreHeightOffset);

                this.LeftGrid.RowDefinitions[0].Height = new GridLength(FullFilter);
            }
            else if (this.FilterCanvas.Height == FullFilter)
            {
                this.FilterCanvas.Height = ShortFilter;
                this.SearchButton.SetValue(Canvas.TopProperty, ShortFilter - searchHeightOffset);
                this.MoreFilterTextBlock.Text = "More..";
                this.MoreFilterTextBlock.SetValue(Canvas.TopProperty, ShortFilter - moreHeightOffset);
                this.LeftGrid.RowDefinitions[0].Height = new GridLength(ShortFilter);

            }

        }

        private void ContactAdvisorButton_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("https://success.ucalgary.ca/home.htm");
        }


        bool toggleSelectedClass = false;
        private void Calendar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            
            if (StudentCourses.Count()> 0 && toggleSelectedClass ==false)
            {
                this.SelectedClassStackPanel.Children.Clear();
                CourseListItemControl CourseControl = new CourseListItemControl(null);
                Course c = StudentCourses[0];
                CourseControl.CourseNameLabel.Content = c.CourseAbbrev + "\t" + c.CourseName;
                CourseControl.CourseDayLabel.Text = c.SceduleDayToString();
                CourseControl.CourseTime.Text = c.SceduleTimeToString();
                CourseControl.CourseRoom.Text = c.Location;
                CourseControl.ProfNameLabel.Text = c.ProfessorName + " User";
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

                CourseControl.RemoveButton.Visibility = Visibility.Visible;
                CourseControl.TutorialSelectedLabel.Visibility = Visibility.Visible;

                if (c.Tutorials.Count > 0)
                {
                    CourseControl.TutorialSelectedLabel.Text = c.Tutorials[0].ToString();
                }

                CourseControl.MoreTextBlock.Visibility= Visibility.Hidden;
                CourseControl.AddButton1.Visibility = Visibility.Hidden;
                CourseControl.Status.Visibility = Visibility.Hidden;
                CourseControl.StatusLabel1.Visibility = Visibility.Hidden;
                CourseControl.TutorialTimeDropDown.Visibility = Visibility.Hidden;
                
                this.SelectedClassStackPanel.Children.Add(CourseControl);

            
                toggleSelectedClass = true;

            }
            else
            {
                toggleSelectedClass = false;
                this.SelectedClassStackPanel.Children.Clear();
            }

        }

        //Updates the credits in the top right based on the degree nav
        private void UpdateCreditsPassed()
        {
            double creditsTaken = 0;
            foreach (List<string> rowOfDegreeNav in degreeProgress.degreeNavRows)
            {
                double classesInRow = rowOfDegreeNav.Count;
                creditsTaken += classesInRow * 3;
            }
            this.CreditsTaken.Text = "Credits Taken: " + creditsTaken;
            this.CreditsLeft.Text = "Credits Left: " + (75 - creditsTaken);
        }
    }
}
