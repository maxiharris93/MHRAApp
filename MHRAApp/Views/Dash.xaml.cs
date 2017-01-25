using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using MHRAApp.Models;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace MHRAApp.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Dash : Page
    {
        /*private ObservableCollection<Horse> Horses;*/

        public Dash()
        {
            this.InitializeComponent();
            PageTitleTB.Text = "My Academy Dashboard";

            /*Horses = new ObservableCollection<Horse>();
            HorseManager.GetAllHorses(Horses);*/
        }

        private void GoToYourLessons_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MyLessons));
        }

        private void GoToYourHorses_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MyHorses));
        }

        private void GoToYourStudents_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MyStudents));
        }

        private void Profile_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Profile));
        }
    }
}
