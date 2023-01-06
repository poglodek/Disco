using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace Disco.Shared.Test.Factories;

public class AllowAnonymous : IAuthorizationHandler
{
    public Task HandleAsync(AuthorizationHandlerContext context)
    {
        foreach (IAuthorizationRequirement requirement in context.PendingRequirements.ToList())
            context.Succeed(requirement); //Simply pass all requirements
        
        return Task.CompletedTask;
    }
}