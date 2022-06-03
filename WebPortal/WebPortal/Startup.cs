using Microsoft.AspNetCore.Builder;
using WebPortal.Logger;

namespace WebPortal
{
    public class Startup
    {
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddFile(Path.Combine(Directory.GetCurrentDirectory(), "Logs"));
        }
    }
}
