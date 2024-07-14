using System.Windows.Controls;
using System.Windows;
using Kursova.Modul;
using Kursova.Modul.Data;
using System.Linq;
using System.Data.Entity;
using System.Threading.Tasks;
using System;

namespace Kursova.View.UserInterface.Pages
{
    public partial class RegistrationPage : Page
    {
        public RegistrationPage()
        {
            InitializeComponent();
        }

        private void Button_Registr_Click(object sender, RoutedEventArgs e)
        {
            using (MyDBContext _context = new MyDBContext())
            {
                string name = regText.inputText.Text;
                int password = regPass.inputPassword.Password.GetHashCode();
                int replyPassword = regPassReplie.inputPassword.Password.GetHashCode();

                if (string.IsNullOrEmpty(name) || replyPassword != password)
                {
                    ClearText();
                    MessageBox.Show("неправильно введений логін або пароль");
                    return;
                }
                else if (IsNameExists(name, _context))
                {
                    ClearText();
                    MessageBox.Show("таке ім'я вже є будьласка введіть інше ім'я");
                    return;
                }
                else
                {
                    var newUser = new User()
                    {
                        Name = name,
                        Password = password,
                    };

                    _context.Users.Add(newUser);
                    _context.SaveChangesAsync();
                }
                MessageBox.Show("реєстрація пройшла успішно");
                ClearText();
            }
        }
        private bool IsNameExists(string name, MyDBContext context)
        {
            if (name != null && name != string.Empty && context.Users != null)
            {
                return context.Users.Any(u => u.Name == name);
            }
            return false;
        }
        private void ClearText()
        {
            regText.inputText.Text = string.Empty;
            regPass.inputPassword.Password = string.Empty;
            regPassReplie.inputPassword.Password = string.Empty;
        }

        private void Button_Authorization_Click(object sender, RoutedEventArgs e)
        {
            Window parentWindow = Window.GetWindow(this);
            if (parentWindow != null)
            {
                parentWindow.Content = new AuthorizationPage();
            }

        }
    }
}
