using Kursova.Modul;
using Kursova.Modul.Data;
using Kursova.View.UserInterface.Pages;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Linq;
using System.Windows.Threading;

namespace Kursova
{
    public partial class MainWindow : Window
    {
        private MyDBContext _context;
        private User _user;
        private UserDate _todayUserDate;
        private ActivityPage _activityPage;
        private HealthyPage _healthyPage;
        private DispatcherTimer _timer;
        private DateTime _firstDateTime;
        public MainWindow(User user, MyDBContext context)
        {
            InitializeComponent();
            _context = context;
            _user = user;
            _firstDateTime = DateTime.Today;
            InitializeTimer();

        }
        private void InitializeTimer()
        {
            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromSeconds(1);
            _timer.Tick += Timer_Tick;
            _timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            UpdateData();
        }
        private void UpdateData()
        {
                _todayUserDate = GetTodayUserDate();
                if (_todayUserDate != null)
                {
                    DateTimeBlock.Text = DateTime.Today.ToString("dd.MM.yyyy");
                    if (_activityPage == null || _firstDateTime != _todayUserDate.Datetime) _activityPage = new ActivityPage(_context, _todayUserDate);
                    if (_healthyPage == null || _firstDateTime != _todayUserDate.Datetime) _healthyPage = new HealthyPage(_context,_todayUserDate);
                }
                _activityPage.AverageData(_context);
                _healthyPage.AverageData(_context);
                if (_todayUserDate.Datetime != _firstDateTime) _firstDateTime = _todayUserDate.Datetime;
            
               
           
        }
        private void SaveDataButton_Click(object sender, RoutedEventArgs e)
        {
            if (_activityPage != null && mainFrame.Content == _activityPage) _activityPage.SaveActivityData();
            if (_healthyPage != null && mainFrame.Content == _healthyPage) _healthyPage.SaveHealthData();

        }
        private UserDate GetTodayUserDate()
        {
            var today = DateTime.Today;
            return _context.Dates.FirstOrDefault(d => d.UserId == _user.Id && d.Datetime == today) ?? CreateTodayUserDate(today);
        }
        private UserDate CreateTodayUserDate(DateTime today)
        {
            _todayUserDate = new UserDate()
            {
                Datetime = today,
                UserId = _user.Id,
            };
            _context.Dates.Add(_todayUserDate);
            _context.SaveChangesAsync();
            return _todayUserDate;

        }
        private void activity_button_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            NavigatePage(_activityPage);
        }

        private void health_page_button_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            NavigatePage(_healthyPage);
        }

        private void archive_page_button_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            NavigatePage(new ArchivePage(_context,_user, _todayUserDate));

        }
        private void NavigatePage(Page page)
        {
            Frame parentFrame = mainFrame;
            if (parentFrame != null)
            {
                parentFrame.Content = page;
            }

        }
    }

}
