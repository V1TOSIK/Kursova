using System.Windows;
using System.Windows.Controls;

namespace Kursova.View.UserControls
{
  public partial class newPasswordBlock : UserControl
  {
    public newPasswordBlock()
    {
      InitializeComponent();
    }
    private string placeholderPass;

    public string PlaceholderPass
    {
      get { return placeholderPass; }
      set
      {
        placeholderPass = value;
        BackgroundText.Text = placeholderPass;
      }
    }
    private void inputPassword_PasswordChanged(object sender, RoutedEventArgs e)
    {
      if (string.IsNullOrEmpty(inputPassword.Password))
        BackgroundText.Visibility = Visibility.Visible;
      else BackgroundText.Visibility = Visibility.Hidden;
    }
  }
}
