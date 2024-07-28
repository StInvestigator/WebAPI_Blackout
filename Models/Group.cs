using WebAPI1.Models;

namespace WebAPI_Blackout.Models
{
    public class Group
    {
        public Group() 
        {
            Schedule = new List<PowerStatus>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<PowerStatus> Schedule {  get; set; }

        public void fillTheNextDay()
        {
            var today = Schedule.Where(x => x.Date == DateOnly.FromDateTime(DateTime.Now));
            foreach (var item in today)
            {
                Schedule.Append(new PowerStatus
                {
                    Date = item.Date.AddDays(1),
                    Hour = item.Hour,
                    State = item.State == PowerState.On? 
                        PowerState.Maybe : 
                        item.State == PowerState.Off? 
                        PowerState.On : 
                        PowerState.Off
                });
            }
        }
    }
}
