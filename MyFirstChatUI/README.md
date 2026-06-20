# MyFirstChatUI

MyFirstChatUI is a .NET 9 Blazor Server chat application that demonstrates building an interactive AI chat experience with Azure OpenAI and the `Microsoft.Extensions.AI` abstractions.

## Features

- Blazor interactive server-rendered chat UI
- Streaming assistant responses
- Markdown rendering for chat messages
- Image attachment support for user messages
- Azure OpenAI integration through `IChatClient`
- Sample application services and data models

## Prerequisites

- .NET 9 SDK
- An Azure OpenAI resource
- A deployed Azure OpenAI chat model deployment

## Configuration

The application reads Azure OpenAI settings from configuration using these keys:

- `Chat:AzureOpenAI:Endpoint`
- `Chat:AzureOpenAI:Key`

For local development, store secrets with .NET user secrets:

```powershell
dotnet user-secrets set "Chat:AzureOpenAI:Endpoint" "https://<your-resource-name>.openai.azure.com/"
dotnet user-secrets set "Chat:AzureOpenAI:Key" "<your-api-key>"
```

The chat model deployment is currently configured in `Program.cs`.

## Run the app

```powershell
dotnet restore
dotnet run
```

Open the local URL shown in the terminal and navigate to `/` or `/chat`.

## Project structure

- `Program.cs` - Configures Blazor services, Azure OpenAI, and application middleware.
- `Components/Pages/Chat.razor` - Main chat page and message handling logic.
- `Components/Listing/` - Supporting UI/data components.
- `Models/` - Application data models and services.

## Notes

Do not commit Azure OpenAI keys or other secrets to source control. Use user secrets for local development and secure configuration providers for deployed environments.
