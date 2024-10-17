using Adly.Domain.Entities.User;
using IdentityTests.EFCore.Context;
using IdentityTests.EFCore.Factories;
using IdentityTests.EFCore.Stores;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<IdentityTestDbContext>(options => 
{
    options.UseSqlServer("Data Source=localhost;Initial Catalog=IdentityTests_DB;Integrated Security=true;Encrypt=false");
});


builder.Services.AddScoped<IUserClaimsPrincipalFactory<UserEntity>,AppClaimPrincipalFactory>();
builder.Services.AddScoped<IUserStore<UserEntity>,AppUserStore>();
builder.Services.AddScoped<IRoleStore<RoleEntity>, AppRoleStore>();

/// Identity کانفیگ های 
builder.Services.AddIdentity<UserEntity, RoleEntity>(options =>
{
    ///if true=>Encrypt UserName,PhoneNumber,EmailAddress,... 
    options.Stores.ProtectPersonalData = false;

    options.Password.RequireDigit = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireLowercase = false;
    options.Password.RequiredUniqueChars = 0;///در رمز عبور را مشخص میکنیم unique تعداد کاراکترهای 
    options.Password.RequireUppercase = false;

    options.SignIn.RequireConfirmedAccount = false;
    options.SignIn.RequireConfirmedPhoneNumber = false;
    options.SignIn.RequireConfirmedEmail = false;
})
    .AddUserStore<AppUserStore>()
    .AddRoleStore<AppRoleStore>()
    .AddEntityFrameworkStores<IdentityTestDbContext>()
    .AddDefaultTokenProviders()
    .AddClaimsPrincipalFactory<AppClaimPrincipalFactory>() ///پیش فرض درست میکنیم claim یک 
    .AddApiEndpoints();//پیاده سازی میشه Minimal Api به طور کامل با استفاده از Identity های  Feature تمامی 

var app = builder.Build();

//پیاده سازی میکند minimal Api را به صورت  Identity متد های مربوط به
app.MapGroup("Identity")
    .MapIdentityApi<UserEntity>();

// Create New User
app.MapPost("/CreateTestUser", async (UserManager<UserEntity> userManager) => 
{
    var user = new UserEntity("FirstName", "LastName", "UserName", "email@email.com");
     
    var creationResult=await userManager.CreateAsync(user,"Test@1234");

    return Results.Ok(creationResult);
});


//Create Test Role
app.MapPost("/CreateTestRole", async (RoleManager<RoleEntity> roleManager) => 
{
    var role = new RoleEntity("Test Role", "Test");

    var creationResult=await roleManager.CreateAsync(role);

    return Results.Ok(creationResult);
})
    .WithName("CreateTestRole")
    .WithOpenApi();

//AddTestUserToTestRole
app.MapPost("/AddTestUserToTestRole",async (
    UserManager<UserEntity> userManager
    ,RoleManager<RoleEntity> roleManager) => 
{
    var user =await userManager.FindByNameAsync("UserName");

    var role =await roleManager.FindByNameAsync("Test");


    var addTestUserToTestRoleResult = await userManager.AddToRoleAsync(user, role.Name);
    

    return Results.Ok(addTestUserToTestRoleResult);

}).
WithName("AddTestUserToTestRole")
.WithOpenApi();


//Get User
app.MapGet("/GetUser",async (UserManager<UserEntity> userManager) => 
{
    var user = await userManager.Users.FirstOrDefaultAsync(c => c.FirstName.Equals("FirstName"));

    var role=await userManager.GetRolesAsync(user);

    return Results.Ok(user);
});

//GerTestUser
app.MapGet("/GetTestUser", async(string password,
    SignInManager < UserEntity > signInManager, UserManager < UserEntity > userManager) =>
{
    var user = await userManager.FindByNameAsync("UserName");

    var checkResult = await signInManager.CheckPasswordSignInAsync(user,password,true);

    return Results.Ok(checkResult);

})
    .WithName("GetTestUser")
    .WithOpenApi();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast")
.WithOpenApi();

app.Run();

internal record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
