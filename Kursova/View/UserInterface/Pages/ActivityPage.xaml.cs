using Kursova.Modul;
using Kursova.Modul.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Kursova.View.UserInterface.Pages
{
  public partial class ActivityPage : Page
  {
    private MyDBContext context;
    private UserDate todayUserDate;
    public ActivityPage(MyDBContext context, UserDate todayUserDate)
    {
      this.context = context;
      this.todayUserDate = todayUserDate;
      InitializeComponent();
    }

    public void SaveActivityData()
    {
      try
      {
        if (todayUserDate == null || todayUserDate.Activity == null || todayUserDate.Activity.Count == 0)
        {
          todayUserDate.Activity = new List<UserActivity>();
        }

        var existingActivity = context.Activities.FirstOrDefault(a => a.DateId == todayUserDate.Id);

        if (existingActivity != null) { UpdateExistingActivity(existingActivity); }
        else CreateNewActivity();
      }
      catch (Exception ex)
      {

        MessageBox.Show($"error: {ex}");
      }
    }
    private void ClearText()
    {
      ExerciseBox.inputText.Text = string.Empty;
      Calories_upvolumeBox.inputText.Text = string.Empty;
      Calories_downvolumeBox.inputText.Text = string.Empty;
      StepsBox.inputText.Text = string.Empty;
      TraveledBox.inputText.Text = string.Empty;
    }
    private string ChekingActivity(out string exerciseData, out double consumedCaloriesData, out double burnedCalories, out int stepsData, out double traveledData)
    {
      string message = string.Empty;

      if (!string.IsNullOrEmpty(ExerciseBox.inputText.Text)) { exerciseData = ExerciseBox.inputText.Text; }
      else { exerciseData = string.Empty; }

      if (double.TryParse(Calories_upvolumeBox.inputText.Text, out consumedCaloriesData))
      {
        if (consumedCaloriesData < 0) message += "Ви не можете отримати вiд'ємну кiлькiсть калорiй!\n";
        if (consumedCaloriesData > 15000) message += "Отримати таку кiлькiсть калорiй за 1 день неможливо!\n";
      }

      if (double.TryParse(Calories_downvolumeBox.inputText.Text, out burnedCalories))
      {
        if (burnedCalories < 0) { message += "Ви не можете спалити вiд'ємну кiлькiсть калорiй!\n"; }
        if (burnedCalories > 20000) { message += "Спалити таку кiлькiсть калорiй за 1 день неможливо!\n"; }
      }

      if (int.TryParse(StepsBox.inputText.Text, out stepsData))
      {
        if (stepsData < 0) { message += "Кiлькiсть крокiв не може бути вiд'ємною!\n"; }
      }

      if (double.TryParse(TraveledBox.inputText.Text, out traveledData))
      {
        if (traveledData < 0) { message += "Кількість пройдених км не може бути від'ємною!\n"; }
      }

      return message;
    }

    private void UpdateExistingActivity(UserActivity existingActivity)
    {
      string message = ChekingActivity(out string exerciseData, out double consumedCaloriesData, out double burnedCalories, out int stepsData, out double traveledData);

      if (message == string.Empty)
      {
        existingActivity.ExerciseName = exerciseData;
        existingActivity.ConsumedCalories = consumedCaloriesData;
        existingActivity.BurnedCalories = burnedCalories;
        existingActivity.Steps = stepsData;
        existingActivity.Traveled = traveledData;
        context.SaveChanges();
        ClearText();
      }
      else
      {
        MessageBox.Show(message);
      }
    }
    private void CreateNewActivity()
    {
      string message = ChekingActivity(out string exerciseData, out double consumedCaloriesData, out double burnedCalories, out int stepsData, out double traveledData);

      if (message == string.Empty)
      {
        todayUserDate.Activity.Add(new UserActivity()
        {
          ExerciseName = exerciseData,
          ConsumedCalories = consumedCaloriesData,
          BurnedCalories = burnedCalories,
          Steps = stepsData,
          Traveled = traveledData,
        });
        context.SaveChanges();
        ClearText();
      }
      else
      {
        MessageBox.Show(message);
      }
    }
  }
}
