using System.Windows;
using System.Windows.Controls;

namespace Kursova.View.UserControls
{
  public partial class newTextBlock : UserControl
  {
    public newTextBlock()
    {
      InitializeComponent();
    }

    private string placeholder;

    public string Placeholder
    {
      get { return placeholder; }
      set
      {
        placeholder = value;
        BackgroundText.Text = placeholder;
      }
    }

    private void inputText_TextChanged(object sender, TextChangedEventArgs e)
    {
      if (string.IsNullOrEmpty(inputText.Text))
        BackgroundText.Visibility = Visibility.Visible;
      else BackgroundText.Visibility = Visibility.Hidden;

    }
  }
}
