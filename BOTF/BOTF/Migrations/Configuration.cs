namespace BOTF.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Web;
    using System.Web.Security;
    using WebMatrix.WebData;
    using BOTF.Infrastructure;
    internal sealed class Configuration : DbMigrationsConfiguration<BOTF.Infrastructure.ContextDb>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }
       
        protected override void Seed(BOTF.Infrastructure.ContextDb context)
        {
            if (!WebSecurity.Initialized)
            {
                 WebSecurity.InitializeDatabaseConnection("DefaultConnection", "UserProfile", "UserId", "Username", true);
            }
          

            /*Create an admin user the first tip code migration is run*/
            var roles = (SimpleRoleProvider)Roles.Provider;
            var membership = (SimpleMembershipProvider)Membership.Provider;
            if (!roles.RoleExists("Admin"))
            {
                roles.CreateRole("Admin");
            }
            if (membership.GetUser("admin", false) == null)
            {
                membership.CreateUserAndAccount("admin", "admin228");
            
                
                
            }
            if (!roles.GetRolesForUser("admin").Contains("Admin"))
            {
                roles.AddUsersToRoles(new[] { "admin" }, new[] { "Admin" });
            }
          
        }
    }
}
