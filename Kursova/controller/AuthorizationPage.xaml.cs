using System;
using System.Data.Entity;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Kursova.Modul;
using Kursova.Modul.Data;

namespace Kursova.View.UserInterface.Pages
{
    public partial class AuthorizationPage : Page
    {
        private Window _parentWindow;
        private MyDBContext _context = new MyDBContext();
        public AuthorizationPage()
        {
            InitializeComponent();
        }


        private void Button_Authorization_Click(object sender, RoutedEventArgs e)
        {
            _parentWindow = Window.GetWindow(this);

            User user = _context.GetUserByName(logTextBox.inputText.Text);
            int inputedPassword = logPassBox.inputPassword.Password.GetHashCode();


            if (user == null || user.Password != inputedPassword)
            {
                ClearText();
                MessageBox.Show("Неправильний логін або пароль");
                return;
            }
            BecomeToNewWindow(user, _context);

        }
        private void ClearText()
        {
            logTextBox.inputText.Text = string.Empty;
            logPassBox.inputPassword.Password = string.Empty;
        }
        private void BecomeToNewWindow(User user, MyDBContext context)
        {
            var toMainWindowButton = new MainWindow(user, context);
            toMainWindowButton.Show();
            _parentWindow.Close();
        }
        private void Button_Registration_Click(object sender, RoutedEventArgs e)
        {
            Window parentWindow = Window.GetWindow(this);
            if (parentWindow != null)
                parentWindow.Content = new RegistrationPage();
        }
    }
}
