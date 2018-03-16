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
    /// Interaction logic for DegreeNavigatorControl.xaml
    /// </summary>
    public partial class DegreeNavigatorControl : UserControl
    {
        public DegreeNavigatorControl()
        {
            InitializeComponent();
            this.ContactAdvisorButton.Click += ContactAdvisorButton_Click;
        }

        private void ContactAdvisorButton_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("https://success.ucalgary.ca/home.htm");
        }
    }
}
