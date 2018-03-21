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
using System.Windows.Media.Animation;

namespace CourseSelectorP3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        SheduleControl sheduleControl;
        DegreeNavigatorControl degreeNavigatorControl;

        int toggle = 0;

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

            ClassItem class1 = new ClassItem();
            class1.AddClass.Content = "Remove ";
            class1.AddClass.Click -= class1.AddButton_Click;
            class1.AddClass.Click += class1.RemoveButton_Click;
            this.ShoppingCartList.Children.Add(class1);

            this.FilterCanvas.Visibility = Visibility.Hidden;
            

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

        private void FilterLabel_MouseDown(object sender, MouseButtonEventArgs e)
        {
            toggle++;
            if (toggle%2 == 0)
            {
                Storyboard sb = this.FindResource("MoveScrollerDown_Copy1") as Storyboard;
                sb.Begin();
                this.FilterCanvas.Visibility = Visibility.Hidden;
            }
            else
            {
                Storyboard sb = this.FindResource("MoveScrollerDown") as Storyboard;
                sb.Begin();
                this.FilterCanvas.Visibility = Visibility.Visible;
            }
        }

        private void SignInButton_Click(object sender, RoutedEventArgs e)
        {
            this.LoginCanvas.Visibility = Visibility.Hidden;
        }

        private void OkSearchButton_Click(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < 2; i++)
            {
                this.ClassList.Children.Add(new ClassItem());
            }

        }
    }
}
