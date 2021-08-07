using Grpc.Core.Interceptors;
using Grpc.Core;
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Formatting = Newtonsoft.Json.Formatting;

namespace GrpcNotificationService.Interceptors
{
    public class GlobalServerLoggerInterceptor : Interceptor
    {
        private readonly ILogger<GlobalServerLoggerInterceptor> logger;

        public GlobalServerLoggerInterceptor(ILogger<GlobalServerLoggerInterceptor> logger)
        {
            this.logger = logger;
        }

        public override async Task<TResponse> UnaryServerHandler<TRequest, TResponse>(TRequest request, ServerCallContext context, UnaryServerMethod<TRequest, TResponse> continuation)
        {
            logger.LogInformation($"{Environment.NewLine}GRPC Request{Environment.NewLine}Method: {context.Method}{Environment.NewLine}Data: {JsonConvert.SerializeObject(request, Formatting.Indented)}");

            var response = await base.UnaryServerHandler(request, context, continuation);

            logger.LogInformation($"{Environment.NewLine}GRPC Response{Environment.NewLine}Method: {context.Method}{Environment.NewLine}Data: {JsonConvert.SerializeObject(response, Formatting.Indented)}");

            return response;
        }
    }
}
