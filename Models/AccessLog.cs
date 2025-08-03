namespace leave_it_small.Models;

public class AccessLog
{
    public Guid Id { get; set; }
    public Guid ShortenedUrlId { get; set; }
    public string IpAdress { get; set; } = string.Empty;
    public string UserAgent { get; set; } = string.Empty;
    public DateTime AccessDate { get; set; }

    public ShortenedUrl shortenedUrl { get; set; }

}