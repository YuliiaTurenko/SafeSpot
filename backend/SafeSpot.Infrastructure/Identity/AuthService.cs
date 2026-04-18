using Google.Apis.Auth;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SafeSpot.Application.Abstractions;
using SafeSpot.Application.DTOs.Auth;
using SafeSpot.Persistence.Identity;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SafeSpot.Infrastructure.Identity;

public class AuthService : IAuthService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IEmailService _emailService;
    private readonly IConfiguration _config;
    private readonly ILocalizationService _loc;
    private readonly IHttpContextAccessor _http;

    public AuthService(UserManager<ApplicationUser> userManager, 
        IEmailService emailService, IConfiguration config, 
        ILocalizationService loc, IHttpContextAccessor http)
    {
        _userManager = userManager;
        _emailService = emailService;
        _config = config;
        _loc = loc;
        _http = http;
    }

    public async Task<AuthResponse> RegisterAsync(RegisterRequest request)
    {
        var user = new ApplicationUser
        {
            UserName = request.Email,
            Email = request.Email
        };

        var result = await _userManager.CreateAsync(user, request.Password);

        if (!result.Succeeded)
            throw new Exception("Registration failed");

        await _userManager.AddToRoleAsync(user, "User");

        var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

        var confirmationLink = $"http://localhost:5173/confirm-email?userId={user.Id}&token={token}";

        //var subject = _loc.Get("EmailSubject", lang);
        //var body = string.Format(_loc.Get("EmailBody", lang), confirmationLink);

        await _emailService.SendAsync(
            user.Email,
            "Confirm your email",
            $"Click the link to confirm your email: {confirmationLink}"
        );

        return new AuthResponse
        {
            Email = user.Email,
            Token = null
        };
    }

    public async Task<AuthResponse> LoginAsync(LoginRequest request)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);
        var lang = _http.HttpContext?.Request.Headers["Accept-Language"].ToString() ?? "en";

        if (user == null)
            throw new Exception(_loc.Get("UserNotFound", lang));

        //!!!
        if (!await _userManager.CheckPasswordAsync(user, request.Password))
            throw new Exception("Invalid password");

        if (!user.EmailConfirmed)
            throw new Exception(_loc.Get("EmailNotConfirmed", lang));

        var token = GenerateToken(user);

        return new AuthResponse
        {
            Email = user.Email,
            Token = await token
        };
    }

    private async Task<string> GenerateToken(ApplicationUser user)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.Email),
            new Claim(ClaimTypes.NameIdentifier, user.Id)
        };

        var roles = await _userManager.GetRolesAsync(user);

        claims.AddRange(roles.Select(role =>
            new Claim(ClaimTypes.Role, role)));

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));

        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _config["Jwt:Issuer"],
            audience: _config["Jwt:Audience"],
            claims: claims,
            expires: DateTime.Now.AddMinutes(double.Parse(_config["Jwt:DurationInMinutes"])),
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public async Task<AuthResponse> LoginWithGoogleAsync(string idToken)
    {
        var payload = await GoogleJsonWebSignature.ValidateAsync(idToken);

        var email = payload.Email;

        var user = await _userManager.FindByEmailAsync(email);

        if (user == null)
        {
            user = new ApplicationUser
            {
                UserName = email,
                Email = email,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(user);

            if (!result.Succeeded)
                throw new Exception("Google registration failed");

            await _userManager.AddToRoleAsync(user, "User");
        }

        var token = await GenerateToken(user);

        return new AuthResponse
        {
            Email = user.Email,
            Token = token
        };
    }

    public async Task ResendConfirmationEmailAsync(string email)
    {
        var user = await _userManager.FindByEmailAsync(email);

        if (user == null)
            throw new Exception("User not found");

        if (user.EmailConfirmed)
            throw new Exception("Email already confirmed");

        var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

        var encodedToken = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));

        var confirmationLink = $"http://localhost:5173/confirm-email?userId={user.Id}&token={encodedToken}";

        await _emailService.SendAsync(
            user.Email,
            "Confirm your email",
            $"Click the link to confirm your email: {confirmationLink}"
        );
    }
}