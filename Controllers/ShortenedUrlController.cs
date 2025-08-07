using leave_it_small.Http.Requests;
using leave_it_small.Services;
using Microsoft.AspNetCore.Mvc;

namespace leave_it_small.Controllers;

[ApiController]
[Route("")]
public class ShortenedUrlController(
    UrlShorteningService urlShorteningService,
    IConfiguration _configuration
    ) : Controller
{
    private readonly UrlShorteningService _urlShorteningService = urlShorteningService;


    [HttpGet("api")]
    public IActionResult ApiRoot()
    {
        return Ok(new { message = "API is running" });
    }

    [HttpGet("api/links")]
    public async Task<IActionResult> GetAllShortenedUrls()
    {
        var result = await _urlShorteningService.GetAllUrlsAsync();
        return Ok(result);
    }

    [HttpDelete("api/links/{code}")]
    public async Task<IActionResult> DeleteShortenedUrl(string code)
    {
        var result = await _urlShorteningService.DeleteShortUrlAsync(code);
        return Ok(result);
    }

    [HttpPost("api/shorten")]
    public async Task<IActionResult> Shorten([FromBody] ShortenUrlRequest request)
    {

        if (!Uri.IsWellFormedUriString(request.Url, UriKind.Absolute))
        {
            return BadRequest("Invalid URL.");
        }

        try
        {
            var  domain = Environment.GetEnvironmentVariable("DOMAIN_NAME");
            var baseUrl = $"{Request.Scheme}://{domain}";
            var result = await _urlShorteningService.CreateShortenedUrlAsync(request.Url, baseUrl);
            return Ok(result);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("{code}")]
    public async Task<IActionResult> RedirectToLongUrl(string code)
    {
        var ipAddress = HttpContext.Connection.RemoteIpAddress?.ToString();
        var userAgent = Request.Headers.UserAgent.ToString();

        var longUrl = await _urlShorteningService.GetLongUrlAsync(code, ipAddress, userAgent);
        if (longUrl == null)
        {
            return NotFound();

        }
        return Redirect(longUrl);
    }


}