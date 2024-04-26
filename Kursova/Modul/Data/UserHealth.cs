namespace Kursova.Modul.Data
{
  public class UserHealth
  {
    public int Id { get; set; }
    public int Pulse { get; set; }
    public string Pressure { get; set; }
    public string VolumeOxygenInBlood { get; set; }

    public int DateId { get; set; }
    public virtual UserDate Date { get; set; }
  }
}
