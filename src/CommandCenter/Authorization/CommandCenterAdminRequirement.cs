using Microsoft.AspNetCore.Authorization;

namespace CommandCenter.Authorization
{
    public class CommandCenterAdminRequirement : IAuthorizationRequirement
    {
        public CommandCenterAdminRequirement(string commandCenterAdmin)
        {
            AdminName = commandCenterAdmin;
        }

        public string AdminName { get; }
    }
}