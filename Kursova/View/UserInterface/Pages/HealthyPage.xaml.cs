using Kursova.Modul;
using Kursova.Modul.Data;
using System.Windows.Controls;
using System.Collections.Generic;
using System.Windows;
using System;

namespace Kursova.View.UserInterface.Pages
{
  public partial class HealthyPage : Page
  {
    private MyDBContext context;
    private UserDate todayUserDate;
    public HealthyPage(MyDBContext context, UserDate todayUserDate)
    {
      InitializeComponent();
      this.context = context;
      this.todayUserDate = todayUserDate;
    }
    public void SaveHealthData()
    {
      try
      {
        if (todayUserDate == null || todayUserDate.Health == null || todayUserDate.Health.Count == 0)
        {
          todayUserDate.Health = new List<UserHealth>();
        }

        CreateNewHealth();
      }
      catch (Exception ex)
      {

        MessageBox.Show($"error: {ex}");
      }
    }

    private void ClearText()
    {
      PulseBox.inputText.Text = string.Empty;
      PressureBox.inputText.Text = string.Empty;
      OxygenInBloodBox.inputText.Text = string.Empty;
    }
    private string ChekingHealth(out int pulse, out string pressure, out string volumeOxygenInBlood)
    {
      string message = string.Empty;

      if (int.TryParse(PulseBox.inputText.Text, out pulse))
      {
        if (pulse < 0) message += "Пульс не може бути від'ємним!\n";
        if (pulse > 300) message += "Такий пульс неможливий!\n";
      }

      if (string.IsNullOrEmpty(PressureBox.inputText.Text)) { pressure = string.Empty; }
      else { pressure = PressureBox.inputText.Text; }

      if (string.IsNullOrEmpty(OxygenInBloodBox.inputText.Text)) { volumeOxygenInBlood = string.Empty; }
      else { volumeOxygenInBlood = OxygenInBloodBox.inputText.Text; }

      return message;
    }
    private void CreateNewHealth()
    {
      string message = ChekingHealth(out int pulse, out string pressure, out string volumeOxygenInBlood);

      if (message == string.Empty)
      {
        todayUserDate.Health.Add(new UserHealth()
        {
          Pulse = pulse,
          Pressure = pressure,
          VolumeOxygenInBlood = volumeOxygenInBlood,
        }

      );
        context.SaveChanges();
        ClearText();
      }
      else MessageBox.Show(message);
    }
  }
}
