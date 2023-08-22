using Application.Abstractions.IRepositories;
using Application.Abstractions.IServices;
using Application.Services;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;
using Persistence.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

var connectionString = builder.Configuration.GetConnectionString("ShopezyConnectionString");
builder.Services.AddDbContext<ApplicationDbContext>(option => option.UseMySql(connectionString,ServerVersion.AutoDetect(connectionString)));
#region 
builder.Services.AddScoped<IUserRepository,UserRepository>();
builder.Services.AddScoped<IUserService,UserService>();

builder.Services.AddScoped<ICustomerRepository,CustomerRepository>();
builder.Services.AddScoped<ICustomerService,CustomerService>();

builder.Services.AddScoped<IManagerRepository,ManagerRepository>();
builder.Services.AddScoped<IManagerService,ManagerService>();

builder.Services.AddScoped<IProductRepository,ProductRepository>();
builder.Services.AddScoped<IProductService,ProductService>();

builder.Services.AddScoped<IBrandRepository,BrandRepository>();
builder.Services.AddScoped<IBrandService,BrandService>();

builder.Services.AddScoped<ISaleRepository,SaleRepository>();
builder.Services.AddScoped<ISaleService,SaleService>();

builder.Services.AddScoped<IRoleRepository,RoleRepository>();
builder.Services.AddScoped<IRoleService,RoleService>();

builder.Services.AddScoped<IOrderRepository,OrderRepository>();
builder.Services.AddScoped<IOrderService,OrderServices>();

builder.Services.AddScoped<IOrderProductRepository,OrderProductRepository>();

builder.Services.AddScoped<IBrandRepository,BrandRepository>();
builder.Services.AddScoped<IBrandService,BrandService>();

#endregion
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
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

app.UseAuthorization();

app.MapControllers();

app.Run();
