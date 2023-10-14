using Microsoft.EntityFrameworkCore;
using ShoppingCart.DB;
using ShoppingCart.Models;

// 为了解决循环依赖的报错
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Json.Serialization;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();







// 为了解决循环依赖的报错
builder.Services.AddControllers().AddJsonOptions(x =>
    x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);





// Add services to the container.
// Enabling Session State
builder.Services.AddSession();





// 注册一个全局维护的已登录信息表 用于跟踪
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


// 使用我自己写的访问控制中间件
// app.UseMiddleware<AccessCheck>();



app.MapControllerRoute(
    name: "default",
    pattern: "{controller=ShoppingCart}/{action=Index}/{id?}");




// 为了解决循环依赖的报错
app.MapControllers();




// Invoke it
InitDB(app.Services);

app.Run();

// Create our Database 使用entity framework, 我采用的方法是构建数据库表关系 以及 加入删除对象
// 使用EF，查找还是使用SQLClient，会更加清晰明了一点
void InitDB(IServiceProvider serviceProvider)
{
    using var scope = serviceProvider.CreateScope();
    MyDbContext db = scope.ServiceProvider.GetRequiredService<MyDbContext>();
    // 表创建完成以后关闭 
    // db.Database.EnsureDeleted();
    // db.Database.EnsureCreated();
}

