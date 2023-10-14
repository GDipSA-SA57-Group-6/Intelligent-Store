using Microsoft.EntityFrameworkCore;
using ShoppingCart.DB;
using ShoppingCart.Models;

// Ϊ�˽��ѭ�������ı���
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Json.Serialization;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();







// Ϊ�˽��ѭ�������ı���
builder.Services.AddControllers().AddJsonOptions(x =>
    x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);





// Add services to the container.
// Enabling Session State
builder.Services.AddSession();





// ע��һ��ȫ��ά�����ѵ�¼��Ϣ�� ���ڸ���
builder.Services.AddSingleton<LoggedUser>(_ => new LoggedUser());





// Add Database Context as Dependency
builder.Services.AddDbContext<MyDbContext>(options =>
{
    var conn_str = builder.Configuration.GetConnectionString("conn_str");
    options.UseLazyLoadingProxies().UseSqlServer(conn_str);
});





var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();


// ʹ�����Լ�д�ķ��ʿ����м��
// app.UseMiddleware<AccessCheck>();



app.MapControllerRoute(
    name: "default",
    pattern: "{controller=ShoppingCart}/{action=Index}/{id?}");




// Ϊ�˽��ѭ�������ı���
app.MapControllers();




// Invoke it
InitDB(app.Services);

app.Run();

// Create our Database ʹ��entity framework, �Ҳ��õķ����ǹ������ݿ���ϵ �Լ� ����ɾ������
// ʹ��EF�����һ���ʹ��SQLClient���������������һ��
void InitDB(IServiceProvider serviceProvider)
{
    using var scope = serviceProvider.CreateScope();
    MyDbContext db = scope.ServiceProvider.GetRequiredService<MyDbContext>();
    // ��������Ժ�ر� 
    // db.Database.EnsureDeleted();
    // db.Database.EnsureCreated();
}

