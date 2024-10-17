using Adly.Domain.Entities.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.Security.Claims;

namespace IdentityTests.EFCore.Factories
{
    public class AppClaimPrincipalFactory : UserClaimsPrincipalFactory<UserEntity, RoleEntity>
    {
        public AppClaimPrincipalFactory(UserManager<UserEntity> userManager, RoleManager<RoleEntity> roleManager, IOptions<IdentityOptions> options) : base(userManager, roleManager, options)
        {
        }

       
        ///  جدید درست کردیم و به پروژه اضافه کردیمcalim custom یک
       
        protected override async Task<ClaimsIdentity> GenerateClaimsAsync(UserEntity user)
        {
            var claims=await base.GenerateClaimsAsync(user);

            claims.AddClaim(new Claim("AppName","IdentityTestsApplication"));

            return claims;
        }
    }
}
