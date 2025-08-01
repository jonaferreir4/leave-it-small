using leave_it_small.Data;
using leave_it_small.Models;
using leave_it_small.utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

namespace leave_it_small.Services;

public class UrlShorteningService(ApplicationDbContext _context)
{
    private readonly Random _random = new();

    public async Task<string> GenerateUniqueCode()
    {
        var codeChars = new char[ShortLinkSettings.Length];
        int maxValue = ShortLinkSettings.Alphabet.Length;

        while (true)
        {
            for (var i = 0; i < ShortLinkSettings.Length; i++)
            {
                var randomIndex = _random.Next(maxValue);

                codeChars[i] = ShortLinkSettings.Alphabet[randomIndex];
            }

            var code = new string(codeChars);

            if (!await _context.ShortenedUrls.AnyAsync(s => s.Code == code))
            {
                return code;
            }
        }
    }

    public async Task<ShortenedUrl> CreateShortenedUrlAsync(string originalUrl, string baseUrl)
    {
        var code = await GenerateUniqueCode();

        var shortenedUrl = new ShortenedUrl
        {
            Id = Guid.NewGuid(),
            LongUrl = originalUrl,
            Code = code,
            ShortUrl = $"{baseUrl}/{code}",
            CreatedOnUtc = DateTime.UtcNow
        };

        _context.ShortenedUrls.Add(shortenedUrl);
        await _context.SaveChangesAsync();

        return shortenedUrl;
    }

    public async Task<string?> GetLongUrlAsync(string code)
    {
        var entity = await _context.ShortenedUrls.FirstOrDefaultAsync(s => s.Code == code);
        if (entity == null) return null;

        entity.Click += 1;
        await _context.SaveChangesAsync();
        return entity.LongUrl;
    }


    public async Task<int?> GetClickCouter(string code)
    {
        var entity = await _context.ShortenedUrls.FirstOrDefaultAsync(s => s.Code == code);
        return entity?.Click;
    }
    }