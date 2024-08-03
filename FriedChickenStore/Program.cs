using FriedChickenStore.CoreHelper;
using FriedChickenStore.Data;
using FriedChickenStore.Model.Entity;
using FriedChickenStore.Repository;
using FriedChickenStore.Service;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
/*builder.Services.AddScoped(typeof(IGenericService<,>), typeof(GenericService<,>));*/
/*builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));*/
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
/*builder.Services.AddAutoMapper(typeof(AutoMapperProfile));*/

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IOrderDetailRepository, OrderDetailRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();


var types = Assembly.GetExecutingAssembly().GetTypes();
foreach (var type in types)
{
    if (type.Name.EndsWith("Service"))
    {
        builder.Services.AddScoped(type);
    }
}


builder.Services.AddDbContext<ApplicationDbContext>(options =>
 {
     options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));

 }
 );

//JWT Authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options => {
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
    options.Events = new JwtBearerEvents
    {
        OnAuthenticationFailed = context =>
        {
            Console.WriteLine("Authentication failed: " + context.Exception.Message);
            Console.WriteLine("Scheme: " + context.Scheme);
            Console.WriteLine("Token: " + context.Request.Headers["Authorization"]);
            return Task.CompletedTask;
        }
    };
});



builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
