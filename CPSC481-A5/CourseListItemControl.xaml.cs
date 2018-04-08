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

namespace CPSC481_A5
{
    /// <summary>
    /// Interaction logic for CourseListItemControl.xaml
    /// </summary>
    public partial class CourseListItemControl : UserControl
    {
        const int ShortDescriptionHeight = 100;
        const int FullDescriptionHeight = 350;
        const int FullReview = 537;

        public RatingCell Star;
        public CourseListItemControl()
        {
            InitializeComponent();
            Star = new RatingCell();
            this.RatingStarContainer.Children.Add(Star);
        }


        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (this.Height == FullDescriptionHeight || this.Height == FullReview )
            {
                this.Height = ShortDescriptionHeight;
                this.MoreTextBlock.Text = "More...";
                this.AddButton1.Visibility = Visibility.Visible;

            }
            else if( this.Height == ShortDescriptionHeight)
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
            }
            else if (this.Height == FullReview)
            {
                this.Height = FullDescriptionHeight;
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
    }
}
