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
        private MyDBContext _context;
        private User _user;
        private UserDate _todayUserDate;
        public ArchivePage(MyDBContext context, User user, UserDate todayUserDate)
        {
            InitializeComponent();
            _context = context;
            _user = user;
            _todayUserDate = todayUserDate;
            
                LoadData(context);
            
        }
        private void LoadData(MyDBContext context)
        {
            var combinedData = from date in context.Dates
                               join activity in context.Activities on date.Id equals activity.DateId
                               join health in context.Healths on date.Id equals health.DateId
                               where date.UserId == _user.Id
                               select new ArchiveData()
                               {
                                   UserId = _user.Id,
                                   DateId = _todayUserDate.Id,
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
