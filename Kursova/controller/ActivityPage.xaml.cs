﻿using Kursova.Modul;
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
        private MyDBContext _context;
        private UserDate _todayUserDate;
        public ActivityPage(MyDBContext context,UserDate todayUserDate)
        {
            _context = context;
            _todayUserDate = todayUserDate;
            InitializeComponent();
        }

        public void SaveActivityData()
        {
            using (var _context = new MyDBContext())
            {
                if (_todayUserDate == null || _todayUserDate.Activity == null || _todayUserDate.Activity.Count == 0)
                {
                    _todayUserDate.Activity = new List<UserActivity>();
                }

                var existingActivity = _context.Activities.FirstOrDefault(a => a.DateId == _todayUserDate.Id);

                if (existingActivity != null) { UpdateExistingActivity(existingActivity, _context); }
                else CreateNewActivity(_context);
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

            if (string.IsNullOrEmpty(ExerciseBox.inputText.Text)) { exerciseData = string.Empty; }
            else { exerciseData = ExerciseBox.inputText.Text.ToLower(); }

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
        private void UpdateExistingActivity(UserActivity existingActivity, MyDBContext context)
        {
            string message = ChekingActivity(out string exerciseData, out double consumedCaloriesData, out double burnedCalories, out int stepsData, out double traveledData);

            if (message == string.Empty)
            {
                existingActivity.ExerciseName = exerciseData;
                existingActivity.ConsumedCalories = consumedCaloriesData;
                existingActivity.BurnedCalories = burnedCalories;
                existingActivity.Steps = stepsData;
                existingActivity.Traveled = traveledData;
                context.SaveChangesAsync();
                ClearText();
            }
            else
            {
                MessageBox.Show(message);
            }
        }
        private void CreateNewActivity(MyDBContext context)
        {
            string message = ChekingActivity(out string exerciseData, out double consumedCaloriesData, out double burnedCalories, out int stepsData, out double traveledData);

            if (message == string.Empty)
            {
                _todayUserDate.Activity.Add(new UserActivity()
                {
                    ExerciseName = exerciseData,
                    ConsumedCalories = consumedCaloriesData,
                    BurnedCalories = burnedCalories,
                    Steps = stepsData,
                    Traveled = traveledData,
                });
                context.SaveChangesAsync();
                ClearText();
            }
            else
            {
                MessageBox.Show(message);
            }
        }

        public void AverageData(MyDBContext context)
        {
            if (context.Activities.Where(a => a.Date.UserId == _todayUserDate.UserId).Any())
            {
                string AverageExerciseName = ExerciseModa(context);

                double AverageCalories_down = context.Activities.Where(a => a.Date.UserId == _todayUserDate.UserId)
                                                                .Average(t => t.ConsumedCalories);
                double AverageCalories_up = context.Activities.Where(a => a.Date.UserId == _todayUserDate.UserId)
                                                              .Average(t => t.BurnedCalories);
                int AverageSteps = Convert.ToInt32(context.Activities.Where(a => a.Date.UserId == _todayUserDate.UserId)
                                                                     .Average(t => t.Steps));
                double AverageTraveled = context.Activities.Where(a => a.Date.UserId == _todayUserDate.UserId)
                                                           .Average(t => t.Traveled);

                AverageExerciseBox.GrayText.Text = $"{AverageExerciseName}";
                AverageClories_upvolumeBox.GrayText.Text = $"{AverageCalories_down.ToString("0.00")} кал";
                AverageCalories_downvolumeBox.GrayText.Text = $"{AverageCalories_up.ToString("0.00")} кал";
                AverageStepsBox.GrayText.Text = $"{AverageSteps} кроків";
                AverageTraveledBox.GrayText.Text = $"{AverageTraveled.ToString("0.00")} км";
            }
        }
        private string ExerciseModa(MyDBContext context)
        {
            List<string> ExName = new List<string>();

            foreach (var activity in context.Activities)
            {
                string[] moda = activity.ExerciseName.Split(' ', ',');
                ExName.AddRange(moda);
            }
            string MostRepeatedExercise = "";
            int maxCount = 0;
            foreach (var exercise in ExName)
            {
                int count = ExName.Count(e => e == exercise);
                if (count > maxCount)
                {
                    maxCount = count;
                    MostRepeatedExercise = exercise;
                }
            }
            return MostRepeatedExercise;
        }
    }
}
