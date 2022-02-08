# ExakisNelite.AspNetCore.SignalR.Extensions

This package is a compilation of extension methods for the [Microsoft.AspNetCore.SignalR](https://www.nuget.org/packages/Microsoft.AspNetCore.SignalR/) package.

## User Id Provider extensions
`IUserIdProvider` is a provider abstraction for configuring the "User ID" for a connection.

### AddObjectIdentifierBasedUserIdProvider
Adds a `IUserIdProvider` that returns the unique identifier of the logged in user. The user Id comes the following claims in order:
- http://schemas.microsoft.com/identity/claims/objectidentifier
- http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier
- sub
- http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name

#### Configuration
```csharp
public void ConfigureServices(IServiceCollection services)
{
  //...
  services.AddObjectIdentifierBasedUserIdProvider();
  //...
}
```

### AddEmailBasedUserIdProvider
Adds a `IUserIdProvider` that returns a customisable value based on the claims of the logged in user.

#### Configuration
```csharp
public void ConfigureServices(IServiceCollection services)
{
  //...
  var userIdProvider = 
  services.AddEmailBasedUserIdProvider();
  //...
}
```

### AddClaimsPrincipalBasedUserIdProvider
Adds a `IUserIdProvider` that returns the email address of the logged in user. The value comes from the claim http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress:

#### Configuration
```csharp
public void ConfigureServices(IServiceCollection services)
{
  //...
  Func<ClaimsPrincipal, string?> userIdAccessor = (principal) => principal.FindFirst(ClaimTypes.PrimarySid)!.Value!;
  
  services.AddClaimsPrincipalBasedUserIdProvider(userIdAccessor);
  //...
}
```
