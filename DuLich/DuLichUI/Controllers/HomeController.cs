using APIIntegration.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ViewModel.Request.NguoiDung;

namespace DuLichUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly IAuthAPI _authAPI;
        private readonly IUserAPI _userAPI;
        private readonly ITourAPI _tourAPI;
        private readonly IConfiguration _configuration;

        public HomeController(IAuthAPI authAPI, IConfiguration configuration, IUserAPI userAPI, ITourAPI tourAPI)
        {
            _authAPI = authAPI;
            _configuration = configuration;
            _userAPI = userAPI;
            _tourAPI = tourAPI;
        }

        public async Task<IActionResult> Index()
        {
            var tours = await _tourAPI.GetAll();
            ViewData["tours"] = tours;
            return View();
        }

        private async Task AssignCookies(string jwt)
        {
            var userPrincipal = ValidateJWT(jwt);
            var authProperties = new AuthenticationProperties()
            {
                ExpiresUtc = DateTimeOffset.UtcNow.AddDays(10),
            };
            await HttpContext.SignInAsync(
                         "UserAuth",
                         userPrincipal,
                         authProperties
                         );
            HttpContext.Response.Cookies.Append("X-Access-Token-User", jwt, new CookieOptions() { HttpOnly = true, SameSite = SameSiteMode.Lax });
        }

        private ClaimsPrincipal ValidateJWT(string jwt)
        {
            IdentityModelEventSource.ShowPII = true;

            SecurityToken validatedToken;
            TokenValidationParameters validationParameters = new TokenValidationParameters();

            validationParameters.ValidateLifetime = true;

            validationParameters.ValidAudience = _configuration["Jwt:Audience"];
            validationParameters.ValidIssuer = _configuration["Jwt:Issuer"];
            validationParameters.IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));

            ClaimsPrincipal principal = new JwtSecurityTokenHandler().ValidateToken(jwt, validationParameters, out validatedToken);

            return principal;
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromForm] DangKyRequest request)
        {
            try
            {
                var res = await _authAPI.DangKy(request);
                if (res != null)
                    return BadRequest("Email đã tồn tại");
                return Ok("success");
            }
            catch (Exception ex)
            {
                return BadRequest("fail");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromForm] DangNhapRequest request)
        {
            try
            {
                var res = await _authAPI.DangNhap(request);
                if (res.Token != null)
                {
                    await AssignCookies(res.Token);
                    var userPrincipal = ValidateJWT(res.Token);
                    string id = userPrincipal.FindFirst(ClaimTypes.NameIdentifier).Value.ToString();
                    var nguoiDung = await _userAPI.GetById(int.Parse(id), "Bearer " + res.Token);

                    HttpContext.Session.SetString("BearerToken", res.Token);
                    HttpContext.Session.SetString("UserId", id);
                    var obj = JsonConvert.SerializeObject(nguoiDung);
                    HttpContext.Session.SetString("User", obj);
                }
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest("Tài khoản hoặc mật khẩu không chính xác");
            }
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            HttpContext.Session.Remove("BearerToken");
            HttpContext.Session.Remove("User");
            await HttpContext.SignOutAsync("UserAuth");
            HttpContext.Response.Cookies.Delete("X-Access-Token-User");
            return Redirect("/home");
        }
    }
}