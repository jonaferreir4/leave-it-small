namespace leave_it_small.Models;

public class ShortenedUrl
{
    public Guid Id { get; set; }
    public string LongUrl { get; set; } = string.Empty;
    public string ShortUrl { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
    public int Click { get; set; }
    public DateTime CreatedOnUtc { get; set; }

    public ICollection<AccessLog> AccessLogs { get; set; } = [];
   
}