using Microsoft.AspNetCore.Authorization;

namespace CommandCenter.Authorization
{
    public class DashboardAdminRequirement : IAuthorizationRequirement
    {
        public DashboardAdminRequirement(string dashboardAdmin)
        {
            AdminName = dashboardAdmin;
        }

        public string AdminName { get; }
    }
}