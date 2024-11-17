using Library.BLL;
using LibrarySimulator;
using System.Reflection;

// ������� ������ �� ������ ����, ������� ������� �� ������������ �����
// ����������� �������� �� ��������, ���������: book, visitor, operation, logger
// ����������� ������, ����������� � ������ �������� ������ ������
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper([Assembly.GetExecutingAssembly()]);

ConfigureServices.ConfigureLogging(builder);
ConfigureServices.ConfigureDatabase(builder);
ConfigureServices.ConfigureAuth(builder);
ConfigureServices.AddFluentValidation(builder);

builder.Services.AddDependencies();

var app = builder.Build();

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
