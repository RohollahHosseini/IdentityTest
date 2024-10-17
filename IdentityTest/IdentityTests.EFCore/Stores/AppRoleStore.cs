using Adly.Domain.Entities.User;
using IdentityTests.EFCore.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace IdentityTests.EFCore.Stores;
    public class AppRoleStore(IdentityTestDbContext context,IdentityErrorDescriber? describer=null)
        :RoleStore<RoleEntity,IdentityTestDbContext,Guid>(context,describer);
   
