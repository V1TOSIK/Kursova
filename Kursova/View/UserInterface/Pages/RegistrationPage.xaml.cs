using System.Windows.Controls;
using System.Windows;
using Kursova.Modul;
using Kursova.Modul.Data;
using System.Linq;

namespace Kursova.View.UserInterface.Pages
{
  public partial class RegistrationPage : Page
  {
    private MyDBContext context;
    public RegistrationPage(MyDBContext context)
    {
      InitializeComponent();
      this.context = context;
    }

    private void Button_Registr_Click(object sender, RoutedEventArgs e)
    {
      try
      {
        var name = regText.inputText.Text;
        var password = regPass.inputPassword.Password;
        var replypassword = regPassReplie.inputPassword.Password;
        var existname = IsNameExists(name);

        if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(password) || replypassword != password)
        {
          ClearText();
          MessageBox.Show("неправильно введений логін або пароль");
          return;
        }
        else if (existname)
        {
          ClearText();
          MessageBox.Show("таке ім'я вже є будьласка введіть інше ім'я");
          return;
        }
        else
        {
          var newUser = new UserData()
          {
            Name = name,
            Password = password,
          };

          context.Users.Add(newUser);
          context.SaveChanges();
        }
        MessageBox.Show("реєстрація пройшла успішно");
        ClearText();

      }
      catch (System.Data.Entity.Core.EntityCommandExecutionException ex)
      {
        MessageBox.Show($"Помилка виконання команди бази даних: {ex.InnerException.Message}");
      }


    }
    bool IsNameExists(string name)
    {
      if (name != null && context.Users != null && name != string.Empty)
      {
        return context.Users.Any(u => u.Name == name);
      }
      return false;
    }
    void ClearText()
    {
      regText.inputText.Text = string.Empty;
      regPass.inputPassword.Password = string.Empty;
      regPassReplie.inputPassword.Password = string.Empty;
    }

    private void Button_Authorization_Click(object sender, RoutedEventArgs e)
    {
      AuthorizationPage authorizationPage = new AuthorizationPage();
      Window parentWindow = Window.GetWindow(this);
      if (parentWindow != null)
      {
        parentWindow.Content = authorizationPage;
      }

    }
  }
}
