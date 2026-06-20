// dotnet add package Azure.AI.OpenAI
// dotnet add package Microsoft.Extensions.AI
// dotnet add package Microsoft.Extensions.AI.OpenAI
using Azure;
using Azure.AI.OpenAI;
using Microsoft.Extensions.AI;
using MyFirstChatUI.Components;
using MyFirstChatUI.Models;


var host = Host.CreateApplicationBuilder(args);

host.Configuration.AddUserSecrets<Program>();

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
	.AddInteractiveServerComponents(
	options =>
	{
		// Avoid enabling in production due to sensitive info in error details.
		options.DetailedErrors = builder.Environment.IsDevelopment();
	}
);

var endpoint = host.Configuration["Chat:AzureOpenAI:Endpoint"] ?? throw new InvalidOperationException("Chat:AzureOpenAI:Endpoint is not configured.");
var apikey = host.Configuration["Chat:AzureOpenAI:Key"] ?? throw new InvalidOperationException("Chat:AzureOpenAI:APIkey is not configured.");

string model = "gpt-5.2";
var client = new AzureOpenAIClient(new Uri(endpoint), new AzureKeyCredential(apikey));

IChatClient innerClient = client.GetChatClient(model).AsIChatClient();
builder.Services.AddChatClient(innerClient).UseLogging();


// Register CoffeeData service
builder.Services.AddScoped<CoffeeData>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Error", createScopeForErrors: true);
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.UseHttpsRedirection();

app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
	.AddInteractiveServerRenderMode();


app.Run();
