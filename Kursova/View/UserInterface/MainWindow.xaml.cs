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
    private MyDBContext context = new MyDBContext();
    private UserData user = new UserData();
    private UserDate todayUserDate;
    private ActivityPage activityPage;
    private HealthyPage healthyPage;
    private DispatcherTimer timer;
    private DateTime firstDateTime;
    public MainWindow(UserData user, MyDBContext context)
    {
      InitializeComponent();
      this.context = context;
      this.user = user;
      firstDateTime = DateTime.Today;
      InitializeTimer();

    }
    private void InitializeTimer()
    {
      
      timer = new DispatcherTimer();
      timer.Interval = TimeSpan.FromSeconds(1);
      timer.Tick += Timer_Tick;
      timer.Start();
      /*MessageBox.Show("good");*/
    }

    private void Timer_Tick(object sender, EventArgs e)
    {
      UpdateData();
    }
    private void UpdateData() 
    {
      try
      {
        todayUserDate = GetTodayUserDate();
        if (todayUserDate != null)
        {
          DateTimeBlock.Text = DateTime.Today.ToString("dd.MM.yyyy");
          if (activityPage == null || firstDateTime != todayUserDate.Datetime) activityPage = new ActivityPage(context, todayUserDate);
          if (healthyPage == null || firstDateTime != todayUserDate.Datetime) healthyPage = new HealthyPage(context, todayUserDate);
        }
        activityPage.AverageData();
        healthyPage.AverageData();
        if(todayUserDate.Datetime != firstDateTime) firstDateTime = todayUserDate.Datetime;
      }
      catch (Exception ex)
      {
        MessageBox.Show("error: " + ex.Message);
        throw;
      }
    }
    private void SaveDataButton_Click(object sender, RoutedEventArgs e)
    {
      if (activityPage != null && mainFrame.Content == activityPage) activityPage.SaveActivityData();
      if (healthyPage != null && mainFrame.Content == healthyPage) healthyPage.SaveHealthData();
      
    }
    private UserDate GetTodayUserDate() { 
    var today = DateTime.Today;
    return context.Dates.FirstOrDefault(t => t.UserId == user.Id && t.Datetime == today) ?? CreateTodayUserDate(today);
    }
    private UserDate CreateTodayUserDate(DateTime today) {
      todayUserDate = new UserDate()
      {
        Datetime = today,
        UserId = user.Id,
      };
      context.Dates.Add(todayUserDate);
      context.SaveChanges();
      return todayUserDate;

    }
    private void activity_button_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
    {
      NavigatePage(activityPage);
    }

    private void health_page_button_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
    {
      NavigatePage(healthyPage);
    }

    private void archive_page_button_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
    {
      NavigatePage(new ArchivePage(context, user, todayUserDate));

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
