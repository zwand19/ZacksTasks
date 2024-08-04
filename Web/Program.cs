using Managers.Tasks;
using Microsoft.EntityFrameworkCore;
using Models;
using Repositories;
using Repositories.Tasks;

var builder = WebApplication.CreateBuilder(args);

// TODO: pull all interfaces by assembly (Autofac)
builder.Services.AddControllers();
builder.Services.AddTransient<IRepositoryUtility<ZackTask>, RepositoryUtility<ZackTask>>();
builder.Services.AddTransient<ITasksRepository, TasksRepository>();
builder.Services.AddTransient<ITasksManager, TasksManager>();
builder.Services.AddMvc(options => options.EnableEndpointRouting = false);
builder.Services.AddDbContext<ZackTasksDbContext>(options =>
  options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddSpaStaticFiles(configuration => configuration.RootPath = "ClientApp/dist");

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
  // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
  app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

if (app.Environment.IsDevelopment())
{
  // no caching
  app.UseSpaStaticFiles();
}
else
{
  app.UseSpaStaticFiles(new StaticFileOptions
  {
    OnPrepareResponse = context =>
    {
      context.Context.Response.Headers.Add("Cache-Control", "max-age=31536000");
      context.Context.Response.Headers.Add("Expires", "31536000");
    }
  });
}

app.UseMvc(routes => {routes.MapRoute(name: "default", template: "{controller}/{action=Index}/{id?}");});

app.UseSpa(spa =>
{
  spa.Options.SourcePath = "ClientApp";
  spa.Options.DefaultPageStaticFileOptions = new StaticFileOptions
  {
    OnPrepareResponse = context =>
    {
      if (context.File.Name == "index.html")
      {
        context.Context.Response.Headers.Add("Cache-Control", "no-cache, no-store");
        context.Context.Response.Headers.Add("Expires", "-1");
      }
    }
  };

  if (app.Environment.IsDevelopment())
  {
    spa.UseProxyToSpaDevelopmentServer("http://localhost:4200");
  }
});

app.Run();