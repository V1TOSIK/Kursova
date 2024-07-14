using System;
using System.Collections.Generic;
namespace Kursova.Modul.Data
{
  public class User
  {
    public int Id { get; set; }
    public string Name { get; set; }
    public int Password { get; set; }

    public int UserId { get; set; }
    public virtual List<UserDate> Date { get; set; }


  }
}
