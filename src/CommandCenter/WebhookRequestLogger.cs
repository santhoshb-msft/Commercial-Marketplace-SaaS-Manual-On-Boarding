using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.IO;

namespace CommandCenter
{
    public class WebhookRequestLogger
    {
        private readonly RequestDelegate next;
        private readonly ILogger<WebhookRequestLogger> logger;
        private readonly RecyclableMemoryStreamManager memoryStreamManager;

        public WebhookRequestLogger(RequestDelegate next, ILoggerFactory loggerFactory)
        {
            this.next = next;
            logger = loggerFactory.CreateLogger<WebhookRequestLogger>();
            memoryStreamManager = new RecyclableMemoryStreamManager();
        }

        public async Task Invoke(HttpContext context)
        {
            if (context.Request.Path.Value.ToLower().Contains("webhook"))
            {
                context.Request.EnableBuffering();

                await using var requestStreamCopy = memoryStreamManager.GetStream();
                await context.Request.Body.CopyToAsync(requestStreamCopy);
                context.Request.Body.Position = 0;
                context.Request.Body.Seek(0, SeekOrigin.Begin);

                logger.LogInformation($"Webhook raw request {ReadStream(requestStreamCopy)}");
            }
        }

        private static string ReadStream(Stream stream)
        {
            const int readChunkBufferLength = 4096;

            stream.Seek(0, SeekOrigin.Begin);

            using var textWriter = new StringWriter();
            using var reader = new StreamReader(stream);

            var readChunk = new char[readChunkBufferLength];
            int readChunkLength;

            do
            {
                readChunkLength = reader.ReadBlock(readChunk,
                    0,
                    readChunkBufferLength);
                textWriter.Write(readChunk, 0, readChunkLength);
            } while (readChunkLength > 0);

            return textWriter.ToString();
        }
    }
}