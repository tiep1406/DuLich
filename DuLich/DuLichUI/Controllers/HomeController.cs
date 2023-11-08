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
using System.Text.Encodings.Web;
using ViewModel.Request.NguoiDung;

namespace DuLichUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly IAuthAPI _authAPI;
        private readonly IUserAPI _userAPI;
        private readonly ITourAPI _tourAPI;
        private readonly IDestinationAPI _destinationAPI;
        private readonly IConfiguration _configuration;

        public HomeController(IAuthAPI authAPI, IConfiguration configuration, IUserAPI userAPI, ITourAPI tourAPI, IDestinationAPI destinationAPI)
        {
            _authAPI = authAPI;
            _configuration = configuration;
            _userAPI = userAPI;
            _tourAPI = tourAPI;
            _destinationAPI = destinationAPI;
        }

        public async Task<IActionResult> Index()
        {
            ViewData["tours"] = await _tourAPI.GetAll();
            ViewData["destinations"] = await _destinationAPI.GetAll();
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
                var url = "/";
                if (res.Token != null)
                {
                    var userPrincipal = ValidateJWT(res.Token);
                    string id = userPrincipal.FindFirst(ClaimTypes.NameIdentifier).Value.ToString();
                    var nguoiDung = await _userAPI.GetById(int.Parse(id), "Bearer " + res.Token);
                    if (nguoiDung.TrangThai == 0)
                        throw new Exception("Tài khoản đã bị khóa");
                    await AssignCookies(res.Token);
                    HttpContext.Session.SetString("BearerToken", res.Token);
                    HttpContext.Session.SetString("UserId", id);
                    var obj = JsonConvert.SerializeObject(nguoiDung);
                    HttpContext.Session.SetString("User", obj);
                    HttpContext.Session.SetString("Quyen", nguoiDung.PhanQuyen.ToString());
                    HttpContext.Session.SetString("Avatar", nguoiDung.AnhDaiDien);
                    HttpContext.Session.SetString("Name", nguoiDung.HoTen);
                    if (nguoiDung.PhanQuyen == 0) url = "/admin";
                }
                return Ok(new { data = res, url });
            }
            catch (Exception ex)
            {
                return BadRequest("Tài khoản đã bị khóa");
            }
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            HttpContext.Session.Remove("BearerToken");
            HttpContext.Session.Remove("UserId");
            HttpContext.Session.Remove("User");
            HttpContext.Session.Remove("Quyen");
            HttpContext.Session.Remove("Avatar");
            HttpContext.Session.Remove("Name");
            await HttpContext.SignOutAsync("UserAuth");
            HttpContext.Response.Cookies.Delete("X-Access-Token-User");
            return Redirect("/home");
        }
    }
}