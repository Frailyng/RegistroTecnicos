using Microsoft.EntityFrameworkCore;
using RegistroTecnicos.Components;
using RegistroTecnicos.DAL;
using RegistroTecnicos.Services;

var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorComponents()
                .AddInteractiveServerComponents();
//Obtenemos el Constr para usarlo en el contexto
var ConStr = builder.Configuration.GetConnectionString("ConStr");

//Agregamos el contexto al builder con el ConStr
builder.Services.AddDbContext<Contexto>(Options => Options.UseSqlite(ConStr));

builder.Services.AddScoped<TecnicoService>();
builder.Services.AddScoped<TiposTecnicosService>();
builder.Services.AddScoped<ClientesServices>();
builder.Services.AddScoped<TrabajosService>();
builder.Services.AddScoped<PrioridadesService>();

builder.Services.AddBlazorBootstrap();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error", createScopeForErrors: true);
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();
            app.UseAntiforgery();

            app.MapRazorComponents<App>()
                .AddInteractiveServerRenderMode();

            app.Run();
