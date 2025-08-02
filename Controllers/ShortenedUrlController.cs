using leave_it_small.Http.Requests;
using leave_it_small.Services;
using Microsoft.AspNetCore.Mvc;

namespace leave_it_small.Controllers;

[ApiController]
[Route("api")]
public class ShortenedUrlController(UrlShorteningService urlShorteningService) : Controller
{
    private readonly UrlShorteningService _urlShorteningService = urlShorteningService;


    [HttpPost("shorten")]
    public async Task<IActionResult> Shorten([FromBody] ShortenUrlRequest request)
    {
        if (!Uri.IsWellFormedUriString(request.Url, UriKind.Absolute))
        {
            return BadRequest("Invalid URL.");
        }

        var baseUrl = $"{Request.Scheme}://{Request.Host}";
        var result = await _urlShorteningService.CreateShortenedUrlAsync(request.Url, baseUrl);

        return Ok(result);
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
    
    
     [HttpGet("links")]
    public async Task<IActionResult> GetAllShortenedUrls()
    {
        return Ok("response");
    }

    [HttpDelete("links/{code}")]
    public async Task<IActionResult> DeleteShortenedUrl(string code)
    {
        var result =  await _urlShorteningService.DeleteShortUrlAsync(code);
        return Ok(result);    
    }
}