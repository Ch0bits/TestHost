namespace TestHost.Controllers
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Http.Extensions;
    using Microsoft.AspNetCore.Mvc;

    public class DefaultController : Controller
    {
        public async Task<IActionResult> Index(CancellationToken token)
        {
            if (Request.Query.TryGetValue("delay", out var delay))
            {
                await Task.Delay(int.Parse(delay), token);
            }

            if (Request.Query.TryGetValue("code", out var code))
            {
                Response.StatusCode = int.Parse(code);
            }

            var model = new Model()
            {
                Method = Request.Method,
                Url = Request.GetDisplayUrl(),
                Headers = Request.Headers,
                RemoteIp = HttpContext.Connection.RemoteIpAddress?.ToString(),
                LocalIp = HttpContext.Connection.LocalIpAddress?.ToString(),
                TraceId = HttpContext.TraceIdentifier,
                ConnectionId = HttpContext.Connection.Id,
            };

            return View(model);
        }
    }

    public class Model
    {
        public string Url { get; init; } = string.Empty;
        public IHeaderDictionary Headers { get; init; } = new HeaderDictionary();
        public string Method { get; init; } = string.Empty;
        public string? RemoteIp { get; init; }
        public string? LocalIp { get; init; }
        public string ConnectionId { get; init; } = string.Empty;
        public string TraceId { get; init; } = string.Empty;
    }
}