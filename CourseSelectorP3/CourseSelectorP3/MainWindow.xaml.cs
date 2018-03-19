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

namespace CourseSelectorP3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        SheduleControl sheduleControl;
        DegreeNavigatorControl degreeNavigatorControl;
        public MainWindow()
        {
            // Initialize
            InitializeComponent();
            sheduleControl = new SheduleControl();
            degreeNavigatorControl = new DegreeNavigatorControl();
            this.ScheDeDisplay.Children.Add(sheduleControl);

            // Add button controls
            this.ScheduleButton.Click += ScheduleButton_Click;
            this.DeNaButton.Click += DeNaButton_Click;

        }

        private void DeNaButton_Click(object sender, RoutedEventArgs e)
        {
            this.ScheDeDisplay.Children.Clear();
            this.ScheDeDisplay.Children.Add(degreeNavigatorControl);
        }

        private void ScheduleButton_Click(object sender, RoutedEventArgs e)
        {
            this.ScheDeDisplay.Children.Clear();
            this.ScheDeDisplay.Children.Add(sheduleControl);
        }

        private void Label_MouseDown(object sender, MouseButtonEventArgs e)
        {
  

            for(int i = 0; i < 5; i++)
            {
                this.ClassList.Children.Add(new ClassItem());
            }
         



        }
    }
}
