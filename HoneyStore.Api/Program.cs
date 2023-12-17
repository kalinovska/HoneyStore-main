using HoneyStore.Api.Helpers;
using HoneyStore.BusinessLogic.Helpers;
using HoneyStore.BusinessLogic.Interfaces;
using HoneyStore.BusinessLogic.Services;
using HoneyStore.DataAccess.Context;
using HoneyStore.DataAccess.Identity;
using HoneyStore.DataAccess.UnitOfWork;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using HoneyStore.BusinessLogic.Profiles;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var services = builder.Services;
services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

#region Add CORS

services.AddCors(p => p.AddPolicy("corsapp", builder =>
{
    builder.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
}));

#endregion

#region Add Entity Framework and Identity Framework

services.AddDbContext<StoreDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
        b => b.MigrationsAssembly("HoneyStore"));
});

services.AddIdentity<User, Role>(options =>
{
    options.User.RequireUniqueEmail = false;
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength = 3;
}).AddEntityFrameworkStores<StoreDbContext>().AddDefaultTokenProviders();

#endregion

#region Add Authentication

var appSettings = builder.Configuration.GetSection("TokenSettings");
services.Configure<TokenSettings>(appSettings);


var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["TokenSettings:Key"] ?? string.Empty));

services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(config =>
{
    config.RequireHttpsMetadata = false;
    config.SaveToken = true;
    config.TokenValidationParameters = new TokenValidationParameters()
    {
        IssuerSigningKey = signingKey,
        ValidateAudience = true,
        ValidAudience = builder.Configuration["TokenSettings:Audience"],
        ValidateIssuer = true,
        ValidIssuer = builder.Configuration["TokenSettings:Issuer"],
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true
    };
});

#endregion

#region Add DI for application services

services.AddTransient<IUnitOfWork, UnitOfWork>();
services.AddTransient<IUserService, UserService>();
services.AddTransient<IProductService, ProductService>();
services.AddTransient<ICommentService, CommentService>();
services.AddTransient<ICategoryService, CategoryService>();
services.AddTransient<IProducerService, ProducerService>();
services.AddTransient<IWishService, WishService>();
services.AddTransient<ICartItemService, CartItemService>();
services.AddTransient<IOrderService, OrderService>();
services.AddTransient<IMapperFactory, MapperFactory>();

#endregion

#region Add AutoMapper

services.AddAutoMapper(typeof(BaseService).Assembly, typeof(Program).Assembly);

#endregion

var sp = services.BuildServiceProvider();

await CreateUsers(sp);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseHsts();
}

app.UseSwagger();
app.UseSwaggerUI();

app.UseCors("corsapp");

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseStaticFiles();
app.UseStaticFiles(new StaticFileOptions()
{
    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"Resources")),
    RequestPath = new PathString("/Resources")
});

app.MapControllers();

app.Run();

async Task CreateUsers(IServiceProvider serviceProvider)
{
    var roleManager = serviceProvider.GetRequiredService<RoleManager<Role>>();
    var userManager = serviceProvider.GetRequiredService<UserManager<User>>();
    string[] roleNames = { "Admin", "User", "Manager" };

    foreach (var roleName in roleNames)
    {
        var roleExist = await roleManager.RoleExistsAsync(roleName);
        if (!roleExist)
        {
            await roleManager.CreateAsync(new Role
            {
                Name = roleName
            });
        }
    }

    var user = await userManager.FindByEmailAsync("admin@gmail.com");

    if (user == null)
    {
        user = new User
        {
            FirstName = "Admin",
            LastName = "Admin",
            UserName = "admin@gmail.com",
            Email = "admin@gmail.com",
        };
        await userManager.CreateAsync(user, "Qwerty11");
    }
    await userManager.AddToRoleAsync(user, "Admin");


    var user1 = await userManager.FindByEmailAsync("manager@gmail.com");

    if (user1 == null)
    {
        user1 = new User
        {
            FirstName = "Manager",
            LastName = "Manager",
            UserName = "manager@gmail.com",
            Email = "manager@gmail.com",
        };
        await userManager.CreateAsync(user1, "Qwerty11");
    }
    await userManager.AddToRoleAsync(user1, "Manager");

    var user2 = await userManager.FindByEmailAsync("user@gmail.com");

    if (user2 == null)
    {
        user2 = new User
        {
            FirstName = "User",
            LastName = "User",
            UserName = "user@gmail.com",
            Email = "user@gmail.com",
        };
        await userManager.CreateAsync(user2, "Qwerty11");
    }
    await userManager.AddToRoleAsync(user2, "User");
}