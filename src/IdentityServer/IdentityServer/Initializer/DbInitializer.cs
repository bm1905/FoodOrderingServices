using System.Security.Claims;
using IdentityModel;
using IdentityServer.DbContext;
using IdentityServer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace IdentityServer.Initializer
{
    public class DbInitializer : IDbInitializer
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public DbInitializer(ApplicationDbContext db, UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _db = db;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public void Initialize()
        {
            // _db.Database.EnsureCreated();
            // _db.Database.Migrate();
            
            if (_roleManager.FindByNameAsync(Config.Admin).Result == null)
            {
                _roleManager.CreateAsync(new IdentityRole(Config.Admin)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(Config.Customer)).GetAwaiter().GetResult();
            }
            else
            {
                return;
            }

            ApplicationUser adminUser = new ApplicationUser()
            {
                UserName = "admin",
                Email = "admin@admin.com",
                EmailConfirmed = true,
                PhoneNumber = "1111111111",
                FirstName = "Bijay",
                LastName = "Admin"
            };

            _userManager.CreateAsync(adminUser, "Admin123*").GetAwaiter().GetResult();
            _userManager.AddToRoleAsync(adminUser, Config.Admin).GetAwaiter().GetResult();

            var temp1 = _userManager.AddClaimsAsync(adminUser, new Claim[]
            {
                new(JwtClaimTypes.Name, adminUser.FirstName + " " + adminUser.LastName),
                new(JwtClaimTypes.GivenName, adminUser.FirstName),
                new(JwtClaimTypes.FamilyName, adminUser.LastName),
                new(JwtClaimTypes.Role, Config.Admin)
            }).Result;

            ApplicationUser customerUser = new ApplicationUser()
            {
                UserName = "customer",
                Email = "customer@customer.com",
                EmailConfirmed = true,
                PhoneNumber = "1111111111",
                FirstName = "Bijay",
                LastName = "Customer"
            };

            _userManager.CreateAsync(customerUser, "Customer123*").GetAwaiter().GetResult();
            _userManager.AddToRoleAsync(customerUser, Config.Customer).GetAwaiter().GetResult();

            var temp2 = _userManager.AddClaimsAsync(customerUser, new Claim[]
            {
                new(JwtClaimTypes.Name, customerUser.FirstName + " " + customerUser.LastName),
                new(JwtClaimTypes.GivenName, customerUser.FirstName),
                new(JwtClaimTypes.FamilyName, customerUser.LastName),
                new(JwtClaimTypes.Role, Config.Customer)
            }).Result;
        }
    }
}