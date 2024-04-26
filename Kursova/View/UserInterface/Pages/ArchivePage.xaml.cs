using Kursova.Modul;
using Kursova.Modul.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Kursova.View.UserInterface.Pages
{
  public partial class ArchivePage : Page
  {
    private MyDBContext context;
    private UserData user;
    private UserDate todayUserDate;
    public ArchivePage(MyDBContext context, UserData user, UserDate todayUserDate)
    {
      InitializeComponent();
      this.context = context;
      this.user = user;
      this.todayUserDate = todayUserDate;

      LoadData();
    }
    private void LoadData()
    {
      var combinedData = from date in context.Dates
                         join activity in context.Activities on date.Id equals activity.DateId
                         join health in context.Healths on date.Id equals health.DateId
                         where date.UserId == user.Id
                         select new ArchiveData()
                         {
                           UserId = user.Id,
                           DateId = todayUserDate.Id,
                           ArchiveDateTime = date.Datetime,
                           ExerciseName = activity.ExerciseName,
                           ConsumedCalories = activity.ConsumedCalories,
                           BurnedCalories = activity.BurnedCalories,
                           Steps = activity.Steps,
                           Traveled = activity.Traveled,
                           Pulse = health.Pulse,
                           Pressure = health.Pressure,
                           VolumeOxygenInBlood = health.VolumeOxygenInBlood
                         };
      myDataGrid.ItemsSource = combinedData.ToList();
    }
  }
}
