using Tasks;
using Tasks.RandomNumber;

namespace WebTasks;

public sealed class Settings
{
	public List<string>? BlackList { get; set; }
}

public static class DiContainerBuilder
{
	public static WebApplicationBuilder BuildContainer(string[] args)
	{
		var builder = WebApplication.CreateBuilder(args);
		var randomApiUrl = builder.Configuration["RandomApi"];
		var settings = builder.Configuration.GetRequiredSection("Settings").Get<Settings>();

		RandomNumberApi.Url = randomApiUrl;
		StringHandler.BlackList = settings?.BlackList;

		builder.Services.AddControllers();
		builder.Services.AddEndpointsApiExplorer();
		builder.Services.AddSwaggerGen();

		return builder;
	}
}
