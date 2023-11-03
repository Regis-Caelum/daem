// using System.Security.Claims;
// using System.Text.Encodings.Web;
// using Microsoft.AspNetCore.Authentication;
// using Microsoft.Extensions.Options;
//
// namespace YabieBackend.Handlers
// {
//     public class AuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
//     {
//         public AuthenticationHandler(
//             IOptionsMonitor<AuthenticationSchemeOptions> options,
//             ILoggerFactory logger,
//             UrlEncoder encoder,
//             ISystemClock clock)
//             : base(options, logger, encoder, clock)
//         {
//         }
//
//         protected override Task<AuthenticateResult> HandleAuthenticateAsync()
//         {
//             if (!Request.Headers.ContainsKey("Authorization"))
//                 return Task.FromResult(AuthenticateResult.Fail("Authentication failed."));
//             var authHeaderValue = Request.Headers["Authorization"].ToString();
//             
//             if (!authHeaderValue.StartsWith("Bearer "))
//                 return Task.FromResult(AuthenticateResult.Fail("Authentication failed."));
//             var token = authHeaderValue.Substring("Bearer ".Length);
//
//             try
//             {
//                 // Verify the Firebase ID token
//                 var decodedToken = FirebaseAuth.DefaultInstance
//                     .VerifyIdTokenAsync(token)
//                     .GetAwaiter()
//                     .GetResult();
//
//                 // Use the decodedToken to validate and identify the user
//                 var identity = new ClaimsIdentity("custom");
//                 identity.AddClaim(new Claim(ClaimTypes.Name, decodedToken.Uid));
//                 var principal = new ClaimsPrincipal(identity);
//                 var ticket = new AuthenticationTicket(principal, Scheme.Name);
//                 return Task.FromResult(AuthenticateResult.Success(ticket));
//             }
//             catch (Exception)
//             {
//                 return Task.FromResult(AuthenticateResult.Fail("Authentication failed."));
//             }
//         }
//     }
// }