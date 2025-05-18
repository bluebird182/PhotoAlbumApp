using Microsoft.AspNetCore.Mvc;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using PhotoAlbumApp.Logic;

public class AccountController : Controller
{
    private readonly IAuthService _authService;

    public AccountController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpGet]
    public IActionResult Login() => View();

    [HttpPost]
    public async Task<IActionResult> Login(string username, string password)
    {
        var user = await _authService.ValidateUserAsync(username, password);
        if (user == null)
        {
            ModelState.AddModelError("", "Invalid credentials");
            return View();
        }

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.Username),
            new Claim("UserId", user.Id.ToString())
        };

        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        var principal = new ClaimsPrincipal(identity);

        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

        return RedirectToAction("Index", "Photo"); // redirect to wherever
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Login", "Account"); // or another neutral page

    }

    [HttpGet]
    public IActionResult Register() => View();

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Register(string username, string password)
    {
        if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
        {
            ModelState.AddModelError("", "Felhasználónév és jelszó megadása kötelező.");
            Console.WriteLine("Felhasználónév és jelszó megadása kötelező.");
            return View();
        }

        var success = await _authService.RegisterUserAsync(username, password);
        if (!success)
        {
            ModelState.AddModelError("", "A felhasználónév már létezik.");
            Console.WriteLine("A felhasználónév már létezik.");
            return View();
        }

        Console.WriteLine("Siker!");
        return RedirectToAction("Login", "Account");
    }

}

