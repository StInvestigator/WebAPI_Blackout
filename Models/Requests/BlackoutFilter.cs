namespace WebAPI1.Models.Requests
{
    public class BlackoutFilter
    {
        public DateOnly? Date { get; set; }
        public int? FromHour { get; set; }
    }
}
