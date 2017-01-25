using System;
using System.Collections.Generic;
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
using Windows.Storage;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace MHRAApp.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Profile : Page
    {
        public static string AcademyUserName;
        //List<Avatar> Avatars;

        public Profile()
        {
            this.InitializeComponent();
            PageTitleTB.Text = "Academy Profile";

            //retrieve settings
            if (AcademyUserName != null)
            {
                UserName.Text = AcademyUserName;

                if (AcademyUserName.ToString() == "")
                {
                    UserName.Text = "Anonymous";
                }
            }
            else
            {
                UserName.Text = "Anonymous";
            }


            //populate avatars
            //Avatars = new List<Avatar>();
            //Avatars.Add(new Avatar { AvatarPath = "ms-appx:///Assets/UserImage/female1.png" });
            //Avatars.Add(new Avatar { AvatarPath = "ms-appx:///Assets/UserImage/female2.png" });
            //Avatars.Add(new Avatar { AvatarPath = "ms-appx:///Assets/UserImage/female3.png" });
            //Avatars.Add(new Avatar { AvatarPath = "ms-appx:///Assets/UserImage/male1.png" });
            //Avatars.Add(new Avatar { AvatarPath = "ms-appx:///Assets/UserImage/male2.png" });
            //Avatars.Add(new Avatar { AvatarPath = "ms-appx:///Assets/UserImage/male3.png" });

            //ui cleanup
            HideEditSection();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            if (Frame.CanGoBack)
            {
                Frame.GoBack();
            }
        }

        /*INITIATE UPDATE*/
        private void EditUser_Click(object sender, RoutedEventArgs e)
        {
            HideViewSection();
            ShowEditSection();
        }

        /*SAVE AND CANCEL BUTTON FUNCTIONALITY*/
        private void SaveUser_Click(object sender, RoutedEventArgs e)
        {
            AcademyUserName = EditUserName.Text;
            UserName.Text = AcademyUserName;
            App.SaveAppState();
            HideEditSection();
            ShowViewSection();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            HideEditSection();
            ShowViewSection();
        }

        /*HIDE OR SHOW SECTIONS*/
        private void HideViewSection()
        {
            ViewSection.Visibility = Visibility.Collapsed;
        }

        private void ShowViewSection()
        {
            ViewSection.Visibility = Visibility.Visible;
        }

        private void HideEditSection()
        {
            EditSection.Visibility = Visibility.Collapsed;
        }

        private void ShowEditSection()
        {
            EditSection.Visibility = Visibility.Visible;
            if (AcademyUserName != null)
            {
                UserName.Text = AcademyUserName;
            }
            else
            {
                UserName.Text = "Anonymous";
            }
        }
    }
}
