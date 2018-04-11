
﻿using System;
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

namespace CPSC481_A5
{
    /// <summary>
    /// Interaction logic for CourseListItemControl.xaml
    /// </summary>
    public partial class CourseListItemControl : UserControl
    {
        public const int ShortDescriptionHeight = 110;
        public const int FullDescriptionHeight = 350;
        public const int FullReview = 537;
        private Course pAssociatedCourse;
        private CourseDB m_pCourseDB = CourseDB.Instance;

        public RatingCell Star;
        public CourseListItemControl(Course pCourseToAssociate)
        {
            InitializeComponent();
            Star = new RatingCell();
            pAssociatedCourse = pCourseToAssociate;
            this.RatingStarContainer.Children.Add(Star);

            ToolTipService.SetBetweenShowDelay(StatusPanel, 0);
        }


        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            applyTextBlock_MouseDown();
            m_pCourseDB.selectCourse(pAssociatedCourse);
        }

        /// <summary>
        /// TextBlock Mouse Down Functionality, made public to be manipulated by the Database.
        /// </summary>
        public void applyTextBlock_MouseDown()
        {
            if (this.Height == FullDescriptionHeight || this.Height == FullReview)
            {
                this.Height = ShortDescriptionHeight;
                this.MoreTextBlock.Text = "More...";
                this.AddButton1.Visibility = Visibility.Visible;

            }
            else if (this.Height == ShortDescriptionHeight)
            {
                this.Height = FullDescriptionHeight;
                this.MoreTextBlock.Text = "Less...";
                this.AddButton1.Visibility = Visibility.Hidden;
            }
        }

        private void CommentAndReviewTextBox_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (this.Height == FullDescriptionHeight)
            {
                this.Height = FullReview;
                this.CommentAndReviewTextBox.ToolTip = "Collapse";

            }
            else if (this.Height == FullReview)
            {
                this.Height = FullDescriptionHeight;
                this.CommentAndReviewTextBox.ToolTip = "Expand";
            }
        }

        private void ReviewTitleTextBox_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (this.Height == FullDescriptionHeight)
            {
                this.Height = FullReview;
            }
            else if (this.Height == FullReview)
            {
                this.Height = FullDescriptionHeight;
            }
        }

        private void StatusIcon_MouseMove(object sender, MouseEventArgs e)
        {
           this.StatusPanel.ToolTip = pAssociatedCourse.StatusToString();
        }

    }
}
