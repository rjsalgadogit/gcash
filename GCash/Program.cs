using GCash.Models.StoredProcedure;
using GCash.SequelConnection;
using GCash.Services;
using GCash.Services.Interfaces;
using Sequel.Service;
using Sequel.Service.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Add dependency injections (services & stored procedures)
builder.Services.AddScoped<ISequelConnection, AppSequelConnection>();
builder.Services.AddScoped<IRecordService, RecordService>();
builder.Services.AddScoped<ISequelService<GetRecord>, SequelService<GetRecord>>();
builder.Services.AddScoped<ISequelService<ReadRecord>, SequelService<ReadRecord>>();
builder.Services.AddScoped<ISequelService<UpdateRecord>, SequelService<UpdateRecord>>();
builder.Services.AddScoped<ISequelService<DeleteRecord>, SequelService<DeleteRecord>>();

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

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
