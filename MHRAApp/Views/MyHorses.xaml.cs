using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Notifications;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using MHRAApp.Models;
using Microsoft.Toolkit.Uwp.Notifications;
using Microsoft.AspNetCore.Http;
using System.Collections.ObjectModel;
using Windows.UI.Xaml.Media.Imaging;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace MHRAApp.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MyHorses : Page
    {
        Horse horse;
        //List<DisciplineCategory> horses { get; set; }

        //PAGE RELATED FUNCTIONS
        public MyHorses()
        {
            this.InitializeComponent();
            PageTitleTB.Text = "My Horses";

            //ui cleanup
            ShowGridViewSection();
            HideViewSection();
            HideNewSection();
            HideEditSection();
            HideBottomAppBars();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            //database work
            using (var db = new HorseContext())
            {
                HorsesGridView.ItemsSource = db.Horses.ToList();

                /*var moviesByCategories = db.Horses.GroupBy(x => x.HorseDiscipline)
                    .Select(x => new DisciplineCategory { Discipline = x.Key, Horses = x.ToList() });

                horses = moviesByCategories.ToList();*/
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            if (Frame.CanGoBack)
            {
                Frame.GoBack();
            }
        }

        private void Profile_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Profile));
        }

        private void BackToGridView_Click(object sender, RoutedEventArgs e)
        {
            HideViewSection();
            HideNewSection();
            HideEditSection();
            HideBottomAppBars();
            ShowGridViewSection();
        }

        //INITIATE CREATE
        private void AddHorse_Click(object sender, RoutedEventArgs e)
        {
            //ui cleanup
            HideGridViewSection();
            HideViewSection();
            HideEditSection();
            AddHorse.Visibility = Visibility.Collapsed;
            HideBottomAppBars();
            ShowNewSection();
        }

        //INITIATE READ
        private void HorsesGridView_ItemClick(object sender, ItemClickEventArgs e)
        {
            //database work
            horse = (Horse)e.ClickedItem;
            ViewDetails(horse);

            //ui cleanup
            HideGridViewSection();
            ShowViewSection();
            ShowHorseName.Text = horse.Name;
            ShowHorseAge.Text = horse.Age.ToString();
            ShowHorseBreed.Text = horse.Breed;
            ShowHorseDiscipline.Text = horse.HorseDiscipline;
            ShowHorseNotes.Text = horse.Notes;
            HideEditSection();
            ShowBottomAppBars();
        }

        //VIEW DETAILS OF SELECTED HORSE
        private Horse ViewDetails(Horse _horse)
        {
            using (var db = new HorseContext())
            {
                return (from p in db.Horses
                        where p.HorseId.Equals(horse.HorseId)
                        select p).FirstOrDefault();
            }
        }

        //INITIATE UPDATE
        private void ChangeHorse_Click(object sender, RoutedEventArgs e)
        {
            //ui cleanup
            HideViewSection();
            HideBottomAppBars();
            ShowEditSection();
        }

        //INITIATE DELETE
        private void RemoveHorse_Click(object sender, RoutedEventArgs e)
        {
            //ui cleanup
            HideEditSection();
        }

        private void DeleteConfirmation_Click(object sender, RoutedEventArgs e)
        {
            //database work
            using (var db = new HorseContext())
            {
                db.Remove(horse);
                db.SaveChanges();

                HorsesGridView.ItemsSource = db.Horses.ToList();
            }

            //ui cleanup
            ShowGridViewSection();
            HideViewSection();
            HorsesGridView.SelectedIndex = -1;
            HideBottomAppBars();
            RemoveHorse.Flyout.Hide();
        }

        //SAVE AND CANCEL BUTTON FUNCTIONALITY
        private void SaveNewHorse_Click(object sender, RoutedEventArgs e)
        {
            //database work
            using (var db = new HorseContext())
            {
                horse = new Horse
                {
                    Name = txtNewHorseName.Text,
                    Age = Convert.ToInt32(NewHorseAge.Value),
                    Breed = txtNewHorseBreed.Text,
                    HorseDiscipline = NewHorseDiscipline.SelectedValue.ToString(),
                    Notes = txtNewHorseNotes.Text
                };
                db.Horses.Add(horse);
                db.SaveChanges();

                //ui cleanup
                ShowGridViewSection();
                HideNewSection();
                AddHorse.Visibility = Visibility.Visible;
                HorsesGridView.ItemsSource = db.Horses.ToList();

                //adaptive tile and toast notification
                InitializeTile();
                InitializeToast();
            }

        }

        private void CancelNewSave_Click(object sender, RoutedEventArgs e)
        {
            //ui cleanup
            ShowGridViewSection();
            HideNewSection();
            AddHorse.Visibility = Visibility.Visible;
        }

        private void SaveExistingHorse_Click(object sender, RoutedEventArgs e)
        {
            //database work
            using (var db = new HorseContext())
            {
                if (horse.HorseId > 0)
                {
                    horse.Name = txtEditHorseName.Text;
                    horse.Age = Convert.ToInt32(EditHorseAge.Value);
                    horse.Breed = txtEditHorseBreed.Text;
                    horse.HorseDiscipline = EditHorseDiscipline.SelectedValue.ToString();
                    horse.Notes = txtEditHorseNotes.Text;

                    db.Attach(horse);
                    db.Update(horse);
                }

                db.SaveChanges();

                //ui cleanup
                HorsesGridView.ItemsSource = db.Horses.ToList();
                ShowHorseName.Text = horse.Name;
                ShowHorseAge.Text = horse.Age.ToString();
                ShowHorseBreed.Text = horse.Breed;
                ShowHorseDiscipline.Text = horse.HorseDiscipline;
                ShowHorseNotes.Text = horse.Notes;
                ShowViewSection();
                HideEditSection();
                ShowBottomAppBars();
            }
        }

        private void CancelExistingSave_Click(object sender, RoutedEventArgs e)
        {
            //ui cleanup
            ViewDetails(horse);
            ShowViewSection();
            HideEditSection();
            ShowBottomAppBars();
        }

        //HIDE OR SHOW SECTIONS
        private void HideViewSection()
        {
            ViewSection.Visibility = Visibility.Collapsed;
        }

        private void ShowViewSection()
        {
            ViewSection.Visibility = Visibility.Visible;
        }

        private void HideNewSection()
        {
            NewSection.Visibility = Visibility.Collapsed;
        }

        private void ShowNewSection()
        {
            NewSection.Visibility = Visibility.Visible;
            txtNewHorseName.Text = "";
            NewHorseAge.Value = 5;
            EditHorseDiscipline.SelectedValue = "None";
            txtNewHorseNotes.Text = "";
        }

        private void HideEditSection()
        {
            EditSection.Visibility = Visibility.Collapsed;
        }

        private void ShowEditSection()
        {
            //ui cleanup
            EditSection.Visibility = Visibility.Visible;
            txtEditHorseName.Text = horse.Name;
            EditHorseAge.Value = horse.Age;
            txtEditHorseBreed.Text = horse.Breed;
            EditHorseDiscipline.SelectedValue = horse.HorseDiscipline;
            txtEditHorseNotes.Text = horse.Notes;
        }

        private void HideGridViewSection()
        {
            BackToGridView.Visibility = Visibility.Visible;
            GridViewSection.Visibility = Visibility.Collapsed;
        }

        private void ShowGridViewSection()
        {
            HorsesGridView.SelectedIndex = -1;
            AddHorse.Visibility = Visibility.Visible;
            BackToGridView.Visibility = Visibility.Collapsed;
            GridViewSection.Visibility = Visibility.Visible;
        }

        private void HideBottomAppBars()
        {
            ChangeHorse.Visibility = Visibility.Collapsed;
            RemoveHorse.Visibility = Visibility.Collapsed;
        }

        private void ShowBottomAppBars()
        {
            ChangeHorse.Visibility = Visibility.Visible;
            RemoveHorse.Visibility = Visibility.Visible;
        }

        private void InitializeTile()
        {
            //tile visual
            TileContent content = new TileContent()
            {
                Visual = new TileVisual()
                {
                    TileMedium = new TileBinding()
                    {
                        Content = new TileBindingContentAdaptive()
                        {
                            Children =
                                {
                                    new AdaptiveText()
                                    {
                                        Text = horse.Name
                                    },
                                    new AdaptiveText()
                                    {
                                        Text = "misses you!!! =("
                                    }
                                }
                        }
                    }
                }
            };
            var tileNotification = new TileNotification(content.GetXml());
            TileUpdateManager.CreateTileUpdaterForApplication().Update(tileNotification);
        }

        private void InitializeToast()
        {
            //toast visual
            ToastVisual visual = new ToastVisual()
            {
                BindingGeneric = new ToastBindingGeneric()
                {
                    Children =
                        {
                            new AdaptiveText()
                            {
                                Text = horse.Name
                            },
                            new AdaptiveText()
                            {
                                Text = "misses you!!! =("
                            }
                        }
                }
            };
            ToastContent toastContent = new ToastContent() { Visual = visual };
            var toast = new ToastNotification(toastContent.GetXml());
            ToastNotificationManager.CreateToastNotifier().Show(toast);
        }
    }
}
