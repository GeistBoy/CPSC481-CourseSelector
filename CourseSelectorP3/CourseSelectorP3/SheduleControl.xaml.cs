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
    /// <summary>
    /// Interaction logic for SheduleControl.xaml
    /// </summary>
    public partial class SheduleControl : UserControl
    {
        public SheduleControl()
        {
            InitializeComponent();

            // Add Some Make up value to Combo box for term selection
            this.Term.Items.Add("Winter 2018");
            this.Term.Items.Add("Spring 2018");
            this.Term.Items.Add("Summer 2018");
            this.Term.Items.Add("Fall 2019");
            this.Term.Items.Add("Winter 2019");
            this.Term.SelectedIndex = 0;

            this.SaveButton.Click += SaveButton_Click;
            this.ContactAdvisorButton.Click += ContactAdvisorButton_Click;
        }

        private void ContactAdvisorButton_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("https://success.ucalgary.ca/home.htm");
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Schedule saved sucessfully!");
        }
    }
}
