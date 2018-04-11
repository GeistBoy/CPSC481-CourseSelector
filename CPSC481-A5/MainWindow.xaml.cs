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

        public MainWindow()
        {
            InitializeComponent();

            initFilterCombos();
            this.SubjectCombo.ItemsSource = CourseDB.Instance.COURSE_VALUES;

            //degreeProgress is used to check if a requirement is completed in degree navigator
            degreeProgress = new DegreeNav();

            //Initializes the degree navigator with default completed classes
            PopulateDegreeNavRequirements(degreeProgress);

            //Sets the icons in degree navigator (checkmarks and X's)
            SetDegreeNavIcons(degreeProgress);

            RandomClasses derp = new RandomClasses();
            List<Course> list = new List<Course>();
            list.Add(derp.hci);
            m_pCourseDB.addCourses(list);
            m_pCourseDB.loadDefault();
            List<CourseListItemControl> pCLICList =  m_pCourseDB.getAllControls();

            foreach( CourseListItemControl pObj in pCLICList )
                this.SearchResultStackPanel.Children.Add(pObj);

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
            Object[] row13 = { "PHIL 314", ClassListToString(progress.degreeNavRows[12]) };
            Object[] row14 = { "2 courses from Faculty of Arts", ClassListToString(progress.degreeNavRows[13]) };
            Object[] row15 = { "2 courses selected freely", ClassListToString(progress.degreeNavRows[14]) };


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
            Image[] images = new Image[15] {Row1Img, Row2Img, Row3Img, Row4Img, Row5Img,
                Row6Img,Row7Img,Row8Img,Row9Img,Row10Img,Row11Img,Row12Img,Row13Img,Row14Img,Row15Img};
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

        // Clears Filters
        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            m_pCourseDB.clearFilters();
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            this.SearchResultStackPanel.Children.Clear();
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
        /// <summary>
        /// Monday Start
        /// </summary>
        private void MondayStartCBO_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            m_pCourseDB.modTimes(0, 0);
        }

        private void MondayEndCBO_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            m_pCourseDB.modTimes(0, 1);
        }

        private void TuesdayStartCBO_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            m_pCourseDB.modTimes(1, 0);
        }

        private void TuesdayEndCBO_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            m_pCourseDB.modTimes(1, 1);
        }

        private void WednesdayStartCBO_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            m_pCourseDB.modTimes(2, 0);
        }

        private void WednesdayEndCBO_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            m_pCourseDB.modTimes(2, 1);
        }

        private void ThursdayStartCBO_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            m_pCourseDB.modTimes(3, 0);
        }

        private void ThursdayEndCBO_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            m_pCourseDB.modTimes(3, 1);
        }

        private void FridayStartCBO_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            m_pCourseDB.modTimes(4, 0);
        }

        private void FridayEndCBO_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            m_pCourseDB.modTimes(4, 1);
        }

        private void Keyword_Text_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                SearchButton_Click(sender, e);
        }
    }
}
