using PayrollManagement.Model.Data;
using PayrollManagement.Repo;
using PayrollManagement.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddScoped<DapperDbContext>();
builder.Services.AddScoped<IEmployeeRep, EmployeeRep>();
builder.Services.AddScoped<IEmpService, EmpService>();
builder.Services.AddScoped<IPayrollRep, PayrollRep>();

builder.Services.AddScoped<IPayrollService, PayrollService>();
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
app.UseDefaultFiles();

app.UseStaticFiles();
app.UseAuthorization();

app.MapControllers();

app.Run();
