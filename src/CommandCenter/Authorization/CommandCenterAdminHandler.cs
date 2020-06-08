using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace CommandCenter.Authorization
{
    public class CommandCenterAdminHandler : AuthorizationHandler<CommandCenterAdminRequirement>
    {
        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            CommandCenterAdminRequirement requirement)
        {
            if (context.User == null) return Task.CompletedTask;

            if (context.User.Identity.GetUserEmail().GetDomainNameFromEmail()
                == requirement.AdminName.GetDomainNameFromEmail()) context.Succeed(requirement);

            return Task.CompletedTask;
        }
    }
}