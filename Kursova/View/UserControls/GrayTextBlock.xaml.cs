using System.Windows.Controls;

namespace Kursova.View.UserControls
{
  public partial class GrayTextBlock : UserControl
  {
    public GrayTextBlock()
    {
      InitializeComponent();
    }

    private string grayBind;

    public string GrayBind
    {
      get
      {
        return grayBind;
      }
      set
      {
        grayBind = value;
        GrayText.Text = grayBind;
      }
    }
  }
}
