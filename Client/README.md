# Manager de Competitii – Blazor WASM Frontend

## Overview
This is a Blazor WebAssembly (WASM) single-page application (SPA) frontend for the Manager de Competitii .NET 8 API. It is production-ready, accessible, responsive, and integrates with the backend endpoints as specified.

### Features
- Dashboard, Tournaments, Rounds, Competitions, Participants, Venues, Notifications, Matches, Live Match, Commands, Strategies, Settings
- Bootstrap 5 styling, mobile-first responsive UI
- Centralized API client with error handling, retries, and configurable base URL
- Visual feedback for async actions, error toasts, and request logging
- Unit tests for API client, LiveMatch, and Dashboard snapshot
- Lazy-loaded Notifications and Matches pages
- Accessibility best practices

## Getting Started

### Prerequisites
- .NET 8 SDK

### Development
1. Navigate to the solution root.
2. Build and run the solution:
   ```sh
   dotnet build
   dotnet run --project Server
   ```
3. Open https://localhost:{port} in your browser.

### Production Build
- The Server project will serve the built Client app from wwwroot.

## File Structure
- `Client/Pages/` – Blazor pages (one per feature)
- `Client/Services/PatternApiClient.cs` – API service layer
- `Client/Shared/` – Shared UI components
- `Client/Tests/` – Unit tests (bUnit, xUnit)
- `Client/wwwroot/` – Static assets (Bootstrap, CSS)

## Migration Note
To replace stub endpoints with real implementations:
- Implement the actual logic in your API controllers.
- Use constructor injection (DI) to inject services into controllers.
- Update the API client models if response shapes change.

## CORS/HTTPS
- If hosting frontend separately, enable CORS in backend.
- By default, static files are served from wwwroot.

## Contact
For questions, see the code comments or open an issue.
