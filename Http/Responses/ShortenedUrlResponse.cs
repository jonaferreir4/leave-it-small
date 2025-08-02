namespace leave_it_small.Http.Responses;
    public record ShortenedUrlResponse( string ShortUrl, string OriginalUrl, int Clicks);