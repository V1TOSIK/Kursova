namespace Kursova.Modul.Data
{
  public class UserActivity
  {
    public int Id { get; set; }
    public string ExerciseName { get; set; }
    public double ConsumedCalories { get; set; }
    public double BurnedCalories { get; set; }
    public int Steps { get; set; }
    public double Traveled { get; set; }

    public int DateId { get; set; }
    public virtual UserDate Date { get; set; }
  }
}
