using Adly.Domain.Entities.User;
using IdentityTests.EFCore.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace IdentityTests.EFCore.Stores;
    public class AppUserStore(IdentityTestDbContext context,IdentityErrorDescriber? describer=null)
        :UserStore<UserEntity,RoleEntity,IdentityTestDbContext,Guid,
        UserClaimEntity,UserRoleEntity,UserLoginEntity,UserTokenEntity,RoleClaimEntity>(context,describer);
