using Grpc.Core;
using GrpcNotificationService.Protos;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;

namespace GrpcNotificationService.Services
{
    public class NotificationService : Notifier.NotifierBase
    {
        [Authorize]
        public override async Task<NotifierResponse> Notify(NotifierRequest request, ServerCallContext context)
        {
            var result = new NotifierResponse();
            try
            {
                var user = context?.GetHttpContext().User;
                result = new NotifierResponse
                {
                    Response = $"Ok from {request.Body}, {request.Service}, {request.Title}"
                };
            }
            catch (System.Exception ex)
            {

            }
            return await Task.FromResult(result);
        }
    }
}
