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
using System.Windows.Controls.Primitives;

namespace CPSC481_A5
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DegreeNav degreeProgress;
        Schedule schedule;
        CourseDB m_pCourseDB = CourseDB.Instance;

        List<Course> StudentCourses = new List<Course>();
        List<Tutorial> StudentTutorials = new List<Tutorial>();

        public MainWindow()
        {
            InitializeComponent();

            initFilterCombos();
            this.SubjectCombo.ItemsSource = CourseDB.Instance.COURSE_VALUES;

            //degreeProgress is used to check if a requirement is completed in degree navigator
            degreeProgress = new DegreeNav();

            //schedule is responsed for schedule data
            schedule = new Schedule();

            //Initializes the degree navigator with default completed classes
            PopulateDegreeNavRequirements(degreeProgress);

            //Sets the icons in degree navigator (checkmarks and X's)
            SetDegreeNavIcons(degreeProgress);

            // initialize schedule
            updateCalendar();

            //Updates the credits in the top right of the main window based on degree nav 
            UpdateCreditsPassed();

            RandomClasses derp = new RandomClasses();
            m_pCourseDB.addCourses(derp.InterestingCourses);
            m_pCourseDB.loadDefault();
            List<CourseListItemControl> pCLICList =  m_pCourseDB.getAllControls();

            foreach( CourseListItemControl pObj in pCLICList)
            {
                this.SearchResultStackPanel.Children.Add(pObj);
                pObj.window = this;
            }
                

            this.Term_Label.Content = "Winter 2018";

            triggerNoSearchOverlay();
        }

        public void initFilterCombos()
        {
            m_pCourseDB.m_pTimeCombos[0, 0] = this.MondayStartCBO;
            m_pCourseDB.m_pTimeCombos[0, 1] = this.MondayEndCBO;
            m_pCourseDB.m_pTimeCombos[1, 0] = this.TuesdayStartCBO;
            m_pCourseDB.m_pTimeCombos[1, 1] = this.TuesdayEndCBO;
            m_pCourseDB.m_pTimeCombos[2, 0] = this.WednesdayStartCBO;
            m_pCourseDB.m_pTimeCombos[2, 1] = this.WednesdayEndCBO;
            m_pCourseDB.m_pTimeCombos[3, 0] = this.ThursdayStartCBO;
            m_pCourseDB.m_pTimeCombos[3, 1] = this.ThursdayEndCBO;
            m_pCourseDB.m_pTimeCombos[4, 0] = this.FridayStartCBO;
            m_pCourseDB.m_pTimeCombos[4, 1] = this.FridayEndCBO;

            m_pCourseDB.m_pSubjectCombo = this.SubjectCombo;

            m_pCourseDB.m_pComparisonCombo = this.ComparisonCombo;

            m_pCourseDB.clearFilters();
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

        public const int ShortFilter = 74;
        public const int FullFilter = 202;

        private bool bShort = true;

        private void MoreFilterTextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            toggleFilter();
        }

        /// <summary>
        /// Functionality to toggle Filter Overlay
        /// </summary>
        private void toggleFilter()
        {
            if (bShort)
            {
                bShort = false;
                this.MoreFilterTextBlock.Text = "Less..";
                this.LeftGrid.RowDefinitions[0].Height = new GridLength(FullFilter);
                this.FilterShadow.Visibility = Visibility.Visible;
                this.HideFilter.Visibility = Visibility.Hidden;
            }
            else
            {
                bShort = true;
                this.MoreFilterTextBlock.Text = "More..";
                this.LeftGrid.RowDefinitions[0].Height = new GridLength(0);
                this.FilterShadow.Visibility = Visibility.Hidden;
                this.HideFilter.Visibility = Visibility.Visible;
            }
        }

        private void triggerNoSearchOverlay()
        {
            if (this.SearchResultStackPanel.Children.Count == 0)
                this.NoSearchCanvas.Visibility = Visibility.Visible;
            else
                this.NoSearchCanvas.Visibility = Visibility.Hidden;
        }

        private void ContactAdvisorButton_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("https://success.ucalgary.ca/home.htm");
        }

        // Clears Filters and associated checkboxes.
        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            m_pCourseDB.clearFilters();

            WCheck.IsChecked = false;
            MCheck.IsChecked = false;
            TCheck.IsChecked = false;
            RCheck.IsChecked = false;
            FCheck.IsChecked = false;
            AvailableCoursesCheck.IsChecked = false;

        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            this.SearchResultStackPanel.Children.Clear();

            // Convert CourseNumber Text, any issues? default to 0
            if( this.CourseNumber.Text != "")
                m_pCourseDB.m_iCourseNum = Convert.ToInt32(this.CourseNumber.Text);
            
            List<Course> pFilteredCourses = m_pCourseDB.searchCourses(this.Keyword_Text.Text);

            if (!bShort)
                toggleFilter();

            if(pFilteredCourses.Count() > 0)
            {
                List<CourseListItemControl> pCntrls = m_pCourseDB.getControls(pFilteredCourses);

                foreach (CourseListItemControl pItem in pCntrls)
                    this.SearchResultStackPanel.Children.Add(pItem);
            }

            triggerNoSearchOverlay();
        }

        /**
            Massive Section of ComboBox Events
        */
        /**
          Monday
        */
        private void MondayStartCBO_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {m_pCourseDB.modTimes(0, 0);}
        private void MondayEndCBO_SelectionChanged(object sender, SelectionChangedEventArgs e)
        { m_pCourseDB.modTimes(0, 1); }

        /**
          Tuesday
        */
        private void TuesdayStartCBO_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {m_pCourseDB.modTimes(1, 0);}

        private void TuesdayEndCBO_SelectionChanged(object sender, SelectionChangedEventArgs e)
        { m_pCourseDB.modTimes(1, 1);}

        /**
          Wednesday
        */
        private void WednesdayStartCBO_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {m_pCourseDB.modTimes(2, 0);}
        private void WednesdayEndCBO_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {m_pCourseDB.modTimes(2, 1);}

        /**
          Thursday
        */
        private void ThursdayStartCBO_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {m_pCourseDB.modTimes(3, 0);}
        private void ThursdayEndCBO_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {m_pCourseDB.modTimes(3, 1);}

        /**
          Friday
        */
        private void FridayStartCBO_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {m_pCourseDB.modTimes(4, 0);}
        private void FridayEndCBO_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {m_pCourseDB.modTimes(4, 1);}

        /// <summary>
        /// Function to enable Search on Enter
        /// </summary>
        private void Keyword_Text_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                SearchButton_Click(sender, e);
        }

        /**
         * Check Box Event Section
         */
        private void AvailableCoursesCheck_Clicked(object sender, RoutedEventArgs e)
        {m_pCourseDB.m_bAvailableOnly = (bool)this.AvailableCoursesCheck.IsChecked;}
        private void FCheck_Click(object sender, RoutedEventArgs e)
        {m_pCourseDB.m_bDayBooleans[(int)Day.Fri] = (bool)this.FCheck.IsChecked;}
        private void MCheck_Click(object sender, RoutedEventArgs e)
        {m_pCourseDB.m_bDayBooleans[(int)Day.Mon] = (bool)this.MCheck.IsChecked;}
        private void TCheck_Click(object sender, RoutedEventArgs e)
        {m_pCourseDB.m_bDayBooleans[(int)Day.Tues] = (bool)this.TCheck.IsChecked;}
        private void WCheck_Click(object sender, RoutedEventArgs e)
        {m_pCourseDB.m_bDayBooleans[(int)Day.Wed] = (bool)this.WCheck.IsChecked;}
        private void RCheck_Click(object sender, RoutedEventArgs e)
        {m_pCourseDB.m_bDayBooleans[(int)Day.Thur] = (bool)this.RCheck.IsChecked;}

        bool toggleSelectedClass = false;

        private void Calendar_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            if (button.Content.Equals("No class"))
            {
                return;
            }

            if (StudentCourses.Count()> 0 && toggleSelectedClass ==false)
            {
                this.SelectedClassStackPanel.Children.Clear();
                CourseListItemControl CourseControl = new CourseListItemControl(null);
                Course c = new Course();
                for(int i=0; i < StudentCourses.Count; i++)
                {
                    if(StudentCourses[i].CourseAbbrev.Equals((sender as Button).Content))
                    {
                        c = StudentCourses[i];
                        switch (i)
                        {
                            case 0:CourseControl.RemoveButton.Click += RemoveButton_Click0; break;
                            case 1: CourseControl.RemoveButton.Click += RemoveButton_Click1; break;
                            case 2: CourseControl.RemoveButton.Click += RemoveButton_Click2; break;
                            case 3: CourseControl.RemoveButton.Click += RemoveButton_Click3; break;
                            case 4: CourseControl.RemoveButton.Click += RemoveButton_Click4; break;
                        }
                        break;
                    }
                }

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

        // brutal way to remove
        private void RemoveButton_Click0(object sender, RoutedEventArgs e)
        {
            this.SelectedClassStackPanel.Children.RemoveAt(this.SelectedClassStackPanel.Children.Count-1);
            if (StudentCourses[0] != null)
                removeCourse(StudentCourses[0]);
        }
        private void RemoveButton_Click1(object sender, RoutedEventArgs e)
        {
            this.SelectedClassStackPanel.Children.RemoveAt(this.SelectedClassStackPanel.Children.Count - 1);
            if (StudentCourses[1] != null)
                removeCourse(StudentCourses[1]);
        }
        private void RemoveButton_Click2(object sender, RoutedEventArgs e)
        {
            this.SelectedClassStackPanel.Children.RemoveAt(this.SelectedClassStackPanel.Children.Count - 1);
            if (StudentCourses[2] != null)
                removeCourse(StudentCourses[2]);
        }
        private void RemoveButton_Click3(object sender, RoutedEventArgs e)
        {
            this.SelectedClassStackPanel.Children.RemoveAt(this.SelectedClassStackPanel.Children.Count - 1);
            if (StudentCourses[3] != null)
                removeCourse(StudentCourses[3]);
        }
        private void RemoveButton_Click4(object sender, RoutedEventArgs e)
        {
            this.SelectedClassStackPanel.Children.RemoveAt(this.SelectedClassStackPanel.Children.Count - 1);
            if (StudentCourses[4] != null)
                removeCourse(StudentCourses[4]);
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

        //============================================================================
        // functions for calendar

        // Initialize calendar
        private void initCalendar()
        {
            ScheduleTable.MinRowHeight = (double)364 / 11;
            ScheduleTable.DataContext = schedule.dataTable.DefaultView;
        }

        // set calendar
        private void updateCalendar()
        {
            // Populate Calendar
            List<Button> buttons = new List<Button>();
            buttons.Add(this.Calendar1);
            buttons.Add(this.Calendar2);
            buttons.Add(this.Calendar3);
            buttons.Add(this.Calendar4);
            buttons.Add(this.Calendar5);

            foreach (Button button in buttons)
                button.Content = "No class";

            for(int i=0; i < StudentCourses.Count; i++)
            {
                buttons[i].Content = StudentCourses[i].CourseAbbrev;
            }
            ScheduleTable.RowHeight = (double)364 / 11;
            ScheduleTable.DataContext = schedule.dataTable.DefaultView;

        }

        //adding course
        public void addCourse(Course course, Tutorial tutorial)
        {
            
            if (StudentCourses.Contains(course))
            {
                MessageBox.Show(course.CourseAbbrev + " is already in your time table, please enroll another course");
            }else if(schedule.isConflict(course, tutorial )){
                MessageBox.Show(course.CourseAbbrev + " has time conflict with current schedule");
            }else if(StudentCourses.Count == 5){
                MessageBox.Show("You have reach the maximum amount of courses");
                }
            else if(course.CourseStatus != Status.Open)
            { 
                MessageBox.Show(course.StatusToString());
            }
            else {
                StudentCourses.Add(course);
                if (tutorial != null)
                {
                    StudentTutorials.Add(tutorial);
                }
                schedule.update(StudentCourses, StudentTutorials);
                updateCalendar();
                degreeProgress.addClassToDegreeNav(course.CourseAbbrev);

                PopulateDegreeNavRequirements(degreeProgress);
                SetDegreeNavIcons(degreeProgress );
            }
        }

        //remove course
        public void removeCourse(Course course)
        {
            StudentCourses.Remove(course);
            foreach (Tutorial t in StudentTutorials)
            {
                if (t.ClassAbbrev.Equals(course.CourseAbbrev) )
                {
                    
                    StudentTutorials.Remove(t);
                    break;
                }
            }
            schedule.update(StudentCourses, StudentTutorials);
            updateCalendar();
        }

    }
}
