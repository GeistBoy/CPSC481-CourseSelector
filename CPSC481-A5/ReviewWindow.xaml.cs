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
using System.Windows.Shapes;

namespace CPSC481_A5
{

    /// <summary>
    /// Interaction logic for ReviewWindow.xaml
    /// </summary>
    public partial class ReviewWindow : Window
    {
        public event EventHandler<CustomEventArgs> RaiseCustomEvent;

        public ReviewWindow()
        {
            InitializeComponent();
            this.Required1.Visibility = Visibility.Hidden;
            this.Required2.Visibility = Visibility.Hidden;
            this.Required3.Visibility = Visibility.Hidden;


        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {

            // Title box check
            String title = "";
            if (String.IsNullOrEmpty(this.TitleTextBox.Text))
            {
                this.Required1.Visibility = Visibility.Visible;
            }
            else
            {
                this.Required1.Visibility = Visibility.Hidden;
                title = this.TitleTextBox.Text;
            }

            // Comment check
            String comment = "";
            if (String.IsNullOrEmpty(this.CommentTextBox.Text))
            {
                this.Required2.Visibility = Visibility.Visible;
            }
            else
            {
                this.Required2.Visibility = Visibility.Hidden;
                comment = this.CommentTextBox.Text;
            }

            // Radio Button check
            int rating = -1;
            if (this.RadioButton1.IsChecked == true)
            {
                rating = 1;
            }
            else if (this.RadioButton2.IsChecked == true)
            {
                rating = 2;
            }
            else if (this.RadioButton3.IsChecked == true)
            {
                rating = 3;
            }

            else if (this.RadioButton4.IsChecked == true)
            {
                rating = 4;
            }
            else if (this.RadioButton5.IsChecked == true)
            {
                rating = 5;
            }

            if (rating == -1)
            {
                this.Required3.Visibility = Visibility.Visible;
            }
            else
            {
                this.Required3.Visibility = Visibility.Hidden;
            }
            if (rating != -1 && !String.IsNullOrEmpty(title) && !String.IsNullOrEmpty(comment))
            {
                this.Close();
                RaiseCustomEvent(this, new CustomEventArgs(title, comment, rating));

            }
        }
    }

    public class CustomEventArgs : EventArgs
    {
        public UserReview userReview;

        public CustomEventArgs(String title, String summary, int rating)
        {
            userReview = new UserReview();
            userReview.SetRating(rating);
            userReview.Summary = summary;
            userReview.Title = title;
        }

        public UserReview GetReview()
        {
            return userReview;
        }
    }
}
