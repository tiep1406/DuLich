using DemoCrud.Responsitory;
using DuLich.Middleware;
using DuLich.Repository.DBContext;
using DuLich.Repository.DiemThamQuan;
using DuLich.Repository.NguoiDung;
using DuLich.Repository.Tour;
using DuLich.Service;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<AppDBContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("AppDbContext")));

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
    });
builder.Services.AddHttpContextAccessor();
builder.Services.AddControllers();

builder.Services
    .AddScoped<IUploadService, UploadService>()
    .AddScoped<INguoiDungRepository, NguoiDungRepository>()
    .AddScoped<ITourRepository, TourRepository>()
    .AddScoped<IDiemThamQuanRepository, DiemThamQuanRepository>()
    .AddScoped<IVanChuyenRepositoty, VanChuyenRepository>()
    .AddScoped<IKhachSanRepositoty, KhachSanRepository>()
    .AddScoped<INhaHangRepositoty, NhaHangRepository>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(s =>
{
    s.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Description = @"JWT authorization header using the Bearer sheme. \r\n\r\n
                        Enter 'Bearer' [space] and then your token in the text input below.
                        \r\n\r\nExample: 'Bearer 12345abcdef'",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });
    s.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        {
            new OpenApiSecurityScheme()
            {
                Reference = new OpenApiReference()
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                Scheme = "oauth2",
                Name = "Bearer",
                In = ParameterLocation.Header
            },
            new List<string>()
        }
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseStaticFiles();
app.UseMiddleware<ExceptionMiddleware>();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
using (var scope = app.Services.CreateScope())
{
    using (var appContext = scope.ServiceProvider.GetRequiredService<AppDBContext>())
    {
        try
        {
            appContext.Database.Migrate();
        }
        catch (Exception ex)
        {
            throw;
        }
    }
}
app.Run();