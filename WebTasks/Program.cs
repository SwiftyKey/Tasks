using WebTasks;

var builder = DiContainerBuilder.BuildContainer(args);

var app = builder.Build();
app.UseMiddleware<RequestLimiter>();

if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
