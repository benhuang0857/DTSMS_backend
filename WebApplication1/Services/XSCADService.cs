using Grpc.AspNetCore;
using Grpc.Core;

namespace WebApplication1.Services
{
    public class XSCADService
    {
        public override Task<XSCADResponse> XSCADProto(XSCADRequest request, ServerCallContext context)
        {
            return Task.FromResult(new XSCADResponse { Message = $"Hello, {request.Name}!" });
        }
    }
}
