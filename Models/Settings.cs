using System.Collections.Generic;

namespace MRobot.Api.Models
{
  public class Settings
  {
    public Theme Theme { get; set; }
    public bool VacationMode { get; set; }
    public string Country { get; set; }
    public IReadOnlyList<int> AvailableHours { get; set; }
    public string EmailAddress { get; set; }
  }
}
