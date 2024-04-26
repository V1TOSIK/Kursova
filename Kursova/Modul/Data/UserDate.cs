using System;
using System.Collections.Generic;
namespace Kursova.Modul.Data
{
  public class UserDate
  {
    public int Id { get; set; }
    public DateTime Datetime { get; set; }

    public int UserId { get; set; }
    public virtual UserData User { get; set; }
    public virtual List<UserActivity> Activity { get; set; }
    public virtual List<UserHealth> Health { get; set; }
  }
}
