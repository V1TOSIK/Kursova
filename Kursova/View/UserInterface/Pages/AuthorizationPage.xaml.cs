using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Kursova.Modul;

namespace Kursova.View.UserInterface.Pages
{
  public partial class AuthorizationPage : Page
  {
    MyDBContext context = new MyDBContext();
    public AuthorizationPage()
    {
      InitializeComponent();
    }


    private void Button_Authorization_Click(object sender, RoutedEventArgs e)
    {
      try
      {
        Window parentWindow = Window.GetWindow(this);
        var name = logTextBox.inputText.Text;
        var user = context.Users.FirstOrDefault(u => u.Name == name);


        if (user == null || user.Password != logPassBox.inputPassword.Password)
        {
          ClearText();
          MessageBox.Show("Неправильний логін або пароль");
          return;
        }

        var enterButton = new MainWindow(user, context);
        enterButton.Show();
        parentWindow.Close();
      }
      catch (Exception ex)
      {

        MessageBox.Show($"exeption:{ex}");
      }
    }
    void ClearText()
    {
      logTextBox.inputText.Text = string.Empty;
      logPassBox.inputPassword.Password = string.Empty;
    }

    private void Button_Registration_Click(object sender, RoutedEventArgs e)
    {
      RegistrationPage registrationPage = new RegistrationPage(context);
      Window parentWindow = Window.GetWindow(this);
      if (parentWindow != null)
      {
        parentWindow.Content = registrationPage;
      }

    }
  }
}
