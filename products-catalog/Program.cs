using MySqlConnector;
using System.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

internal class Program
{
	private static void Main(string[] args)
	{
		var builder = WebApplication.CreateBuilder(args);

		// Add services to the container.
		builder.Services.AddRazorPages();
		builder.Services.AddDistributedMemoryCache();
		builder.Services.AddHttpContextAccessor();
		builder.Services.AddSession(options =>
		{
			options.IdleTimeout = TimeSpan.FromSeconds(1800);
			options.Cookie.HttpOnly = true;
			options.Cookie.IsEssential = true;
		});
		var app = builder.Build();

		// Configure the HTTP request pipeline.
		if (!app.Environment.IsDevelopment())
		{
			app.UseExceptionHandler("/Error");
			// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
			app.UseHsts();
		}

		app.UseSession();
		app.UseHttpsRedirection();
		app.UseStaticFiles();
		app.UseRouting();

		app.UseAuthorization();

		app.MapRazorPages();

		app.Run();
	}
}