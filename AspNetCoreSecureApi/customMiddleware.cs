using Microsoft.Identity.Web;
using System.Diagnostics;
using System.Security.Claims;

namespace AspNetCoreSecureApi;
public class CustomMiddleware
{
    private readonly RequestDelegate _next;
    public CustomMiddleware(RequestDelegate next)
    {
        _next = next;
    }
    public async Task Invoke(HttpContext httpContext)
    {
        var User = httpContext.User;

        var ip = httpContext.Connection.RemoteIpAddress.ToString();
        await _next(httpContext);

        String log;
        try
        {
            var x = User.Claims.First(x => x.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name").Value;
            log = $"Request content ip:  {ip} and email of requested user: {x}";
        }
        catch (Exception ex) {
            log = $"Request content ip:  {ip}";
        }
        WriteFile(log);

    }

    private void WriteFile(String log) {

    string docPath =
      Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

        // Write the string array to a new file named "WriteLines.txt".
        using (StreamWriter outputFile = new StreamWriter(Path.Combine(docPath, "log.txt"), true))
        {
            outputFile.WriteLine(log);
        }
    }
}
// Extension method used to add the middleware to the HTTP request pipeline.
public static class CustomMiddlewareExtensions
{
    public static IApplicationBuilder UseCustomMiddleware(
        this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<CustomMiddleware>();
    }
}