using leave_it_small.Http.Requests;
using leave_it_small.Services;
using Microsoft.AspNetCore.Mvc;

namespace leave_it_small.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ShortenedUrlController(UrlShorteningService urlShorteningService) : Controller
{
    private readonly UrlShorteningService _urlShorteningService = urlShorteningService;


    [HttpPost("/")]
    public async Task<IActionResult> Shorten([FromBody] ShortenUrlRequest request)
    {
        if (!Uri.IsWellFormedUriString(request.Url, UriKind.Absolute))
        {
            return BadRequest("Invalid URL.");
        }

        var baseUrl = $"{Request.Scheme}://{Request.Host}";
        var shortenedUrl = await _urlShorteningService.CreateShortenedUrlAsync(request.Url, baseUrl);

        return Ok(new { shortenedUrl.ShortUrl });
    }

    [HttpGet("{code}")]
    public async Task<IActionResult> RedirectToLongUrl(string code)
    {
        var longUrl = await _urlShorteningService.GetLongUrlAsync(code);
        if (longUrl == null)
        {
            return NotFound();

        }
        return Redirect(longUrl);
    }
    
    [HttpGet("/couter/{code}")]
    public async Task<IActionResult> GetClickCounter(string code)
    {
        var clicks = await _urlShorteningService.GetClickCouter(code);
        if (clicks == null)
        {
            return NotFound();
        }
        return Ok(clicks);
    }
}