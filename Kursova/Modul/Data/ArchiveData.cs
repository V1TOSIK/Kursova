using System;
namespace Kursova.Modul.Data
{
  public class ArchiveData
  {
    public int Id { get; set; }
    public int UserId { get; set; }
    public int DateId { get; set; }
    public DateTime ArchiveDateTime { get; set; }
    public string ExerciseName { get; set; }
    public double ConsumedCalories { get; set; }
    public double BurnedCalories { get; set; }
    public int Steps { get; set; }
    public double Traveled { get; set; }
    public int Pulse { get; set; }
    public string Pressure { get; set; }
    public string VolumeOxygenInBlood { get; set; }
  }

}

