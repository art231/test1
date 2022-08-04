namespace WebApiTest
{
    public class MobileStatistics
    {
        public int Id { get; set; }
        public string? Title { get; set; }

        public DateTime LastStatistics { get; set; }

        public string? VersionClient { get; set; }

        public string? Type { get; set; }
    }
    
}