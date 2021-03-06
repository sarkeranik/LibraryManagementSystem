using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Extensions.Logging_Request_Responses
{
    public class LoggingMiddleware
    {
        private RequestDelegate next;

        public LoggingMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            Console.WriteLine("LoggingMiddleware invoked.");

            var originalBody = context.Response.Body;
            using var newBody = new MemoryStream();
            context.Response.Body = newBody;

            try
            {
                await this.next(context);
            }
            finally
            {
                newBody.Seek(0, SeekOrigin.Begin);
                var bodyText = await new StreamReader(context.Response.Body).ReadToEndAsync();
                Console.WriteLine($"LoggingMiddleware: {bodyText}");
                newBody.Seek(0, SeekOrigin.Begin);
                await newBody.CopyToAsync(originalBody);
            }
        }
    }
}
