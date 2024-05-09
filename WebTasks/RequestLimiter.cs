namespace WebTasks;

public class RequestLimiter
{
	readonly RequestDelegate next;
	readonly SemaphoreSlim semaphore;
	readonly HashSet<string> activeUsers = new();

	public static int Limit { get; set; }

	public RequestLimiter(RequestDelegate next)
	{
		this.next = next;
		semaphore = new SemaphoreSlim(Limit);
	}

	public async Task Invoke(HttpContext context)
	{
		var ipAddress = context.Connection.RemoteIpAddress.ToString();

		if (!semaphore.Wait(0))
		{
			context.Response.StatusCode = StatusCodes.Status503ServiceUnavailable;
			await context.Response.WriteAsync("Number of concurrent users exceeded."); //Выдает каракули на русском
			return;
		}

		try
		{
			lock (activeUsers) activeUsers.Add(ipAddress);

			await next(context);
		}
		finally
		{
			semaphore.Release();
			lock (activeUsers) activeUsers.Remove(ipAddress);
		}
	}
}