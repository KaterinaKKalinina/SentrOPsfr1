namespace SeniorCenterWebApp.Models
{
    public class TestResult
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }  // <-- добавляем навигационное свойство
        public string HobbyResult { get; set; }
        //public DateTime DateTaken { get; set; }
        public DateTimeOffset DateTaken { get; set; }
    }
}
