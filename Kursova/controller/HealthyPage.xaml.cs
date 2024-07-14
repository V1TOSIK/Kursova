using Kursova.Modul;
using Kursova.Modul.Data;
using System.Windows.Controls;
using System.Collections.Generic;
using System.Windows;
using System;
using System.Linq;

namespace Kursova.View.UserInterface.Pages
{
    public partial class HealthyPage : Page
    {
        private MyDBContext _context;
        private UserDate _todayUserDate;
        public HealthyPage(MyDBContext context,UserDate todayUserDate)
        {
            InitializeComponent();
            _context = context;
            _todayUserDate = todayUserDate;
        }
        public void SaveHealthData()
        {
                if (_todayUserDate == null || _todayUserDate.Health == null || _todayUserDate.Health.Count == 0)
                {
                    _todayUserDate.Health = new List<UserHealth>();
                }

                CreateNewHealth(_todayUserDate);
            
                
            
           
        }

        private void ClearText()
        {
            PulseBox.inputText.Text = string.Empty;
            PressureBox.inputText.Text = string.Empty;
            OxygenInBloodBox.inputText.Text = string.Empty;
        }
        private string ChekingHealth(out int pulse, out string pressure, out int volumeOxygenInBlood)
        {
            string message = string.Empty;

            if (int.TryParse(PulseBox.inputText.Text, out pulse))
            {
                if (pulse < 0) message += "Пульс не може бути від'ємним!\n";
                if (pulse > 300) message += "Такий пульс неможливий!\n";
            }

            if (string.IsNullOrEmpty(PressureBox.inputText.Text)) { pressure = string.Empty; }
            else { pressure = PressureCalculate(PressureBox.inputText.Text, ref message); }

            if (int.TryParse(OxygenInBloodBox.inputText.Text, out volumeOxygenInBlood))
            {
                if (volumeOxygenInBlood < 60) message += "Кількість кисню в крові не може бути менше 60%\n";
                if (volumeOxygenInBlood > 100) message += "Кількість кисню в крові не може бути більше 100%\n";
            }

            return message;
        }
        private string PressureCalculate(string pressure, ref string message)
        {
            pressure.Trim();
            string[] pressures = pressure.Split('/', '.', ',', ' ');
            if (int.TryParse(pressures[0], out int UpperPressure))
            {
                if (UpperPressure < 90) { message += "Верхній(Систолічний) тиск не може бути менше 90\n"; }
                if (UpperPressure > 180) { message += "Верхній(Систолічний) тиск не може бути більше 180\n"; }
            }
            else message += "Верхній (Систолічний) тиск має недопустимі значення\n";
            if (int.TryParse(pressures[1], out int LowerPressure))
            {
                if (LowerPressure < 60) { message += "Нижній(Діастолічний) тиск не може бути менше 60\n"; }
                if (LowerPressure > 100) { message += "Нижній(Діастолічний) тиск не може бути більше 100\n"; }
            }
            else message += "Нижній (Діастолічний) тиск має недопустимі значення\n";
            return $"{UpperPressure}/{LowerPressure}";
        }
        private void CreateNewHealth(UserDate todayUserDate)
        {
            string message = ChekingHealth(out int pulse, out string pressure, out int volumeOxygenInBlood);

            if (message == string.Empty)
            {
                todayUserDate.Health.Add(new UserHealth()
                {
                    Pulse = pulse,
                    Pressure = pressure,
                    VolumeOxygenInBlood = volumeOxygenInBlood,
                }

              );
                _context.SaveChangesAsync();
                ClearText();
            }
            else MessageBox.Show(message);
        }
        public void AverageData(MyDBContext context)
        {
            if (context.Healths.Where(a => a.Date.UserId == _todayUserDate.UserId).Any())
            {
                int AveragePulse = Convert.ToInt32(context.Healths.Where(a => a.Date.UserId == _todayUserDate.UserId)
                                                                  .Where(a => a.Pulse != 0)
                                                                  .Average(t => t.Pulse));
                var AveragePressure = AveragePressureCalculate(context);

                int AverageOxygenInBlood = Convert.ToInt32(context.Healths.Where(a => a.Date.UserId == _todayUserDate.UserId)
                                                                          .Where(a => a.VolumeOxygenInBlood != 0)
                                                                          .Average(t => t.VolumeOxygenInBlood));


                AveragePulseBox.GrayText.Text = $"{AveragePulse} уд/хв";
                AveragePressureBox.GrayText.Text = $"{AveragePressure}";
                AverageOxygenInBloodBox.GrayText.Text = $"{AverageOxygenInBlood}%";
            }
        }
        private string AveragePressureCalculate(MyDBContext context)
        {
            int UpperSum = 0, LowerSum = 0, count = 0;
            foreach (var item in context.Healths)
            {
                string[] pressures = item.Pressure.Split('/', '.', ',', ' ');
                if (int.TryParse(pressures[0], out int UpperPressure) && int.TryParse(pressures[1], out int LowerPressure))
                {
                    UpperSum += UpperPressure;
                    LowerSum += LowerPressure;
                    count++;
                }
            }
            if (count > 0)
            {
                int resultUpperPressure = Convert.ToInt32(UpperSum / count);
                int resultLowerPressure = Convert.ToInt32(LowerSum / count);
                return $"{resultUpperPressure}/{resultLowerPressure}";
            }
            return "";
        }
    }
}
