namespace Dashboard.Authorization
{
    using Microsoft.AspNetCore.Authorization;

    public class DashboardAdminRequirement : IAuthorizationRequirement
    {
        public DashboardAdminRequirement(string dashboardAdmin)
        {
            AdminName = dashboardAdmin;
        }

        public string AdminName { get; }
    }
}