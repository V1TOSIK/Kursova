using System.Net;
using System.Windows;
using System.Windows.Controls;
using Kursova.View.UserInterface.Pages;

namespace Kursova.View.UserInterface
{
    public partial class Authorization_RegistrationWindow : Window
    {

        public Authorization_RegistrationWindow()
        {
            InitializeComponent();
        }
        private void Frame_Loaded(object sender, RoutedEventArgs e)
        {
            ChangePage(new AuthorizationPage());
        }
        public void ChangePage(Page newPage)
        {
            Authorization_RegistrationFrame.Content = newPage;
        }
        
    }
}
