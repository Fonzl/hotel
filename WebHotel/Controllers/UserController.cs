using DTO.AuthDto;
using DTO.UserDto;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Repository.JwtRepository;
using Service.UserService;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using WebHotel;


namespace BookAppAPI.Controllers;

[ApiController]
[Route("users")]
public class UserController(IUserService userService, IJwtRepository jwtRepository, UserManager<IdentityUser> userManager) : Controller
{
    private readonly UserManager<IdentityUser> _userManager = userManager;    


    [HttpGet]
    public async Task<JsonResult> GetUsers()
    {
        var users = await userService.GetUsers();
        return Json(users);
    }
    [Route ("{username}")]
    [HttpGet]
    public async Task<JsonResult?> GetUser(string username)
    {
        var user = await userService.GetUser(username);
        return Json(user);
    }
    [Route("create")]
    [HttpPost]
    public async Task<JsonResult> CreateUser(CreateUserDto dto)
    {
        var result = await userService.InsertUser(dto);

        return Json(result);
    }

    [Route("CreateToken")]
    [HttpPost]
    public async Task<ActionResult<AuthSignInResponse>> CreatToken(AuthSignInDto authSignInDto)
    {
       
        if (!ModelState.IsValid)
        {
            return BadRequest("Bad credentials");
        }
        var user = await _userManager.FindByNameAsync(authSignInDto.UserName);
        if (user == null)
        {
            return BadRequest("Bad credentials");
        }
        var isPasswordValid = await _userManager.CheckPasswordAsync(user, authSignInDto.Password);

        if (!isPasswordValid)
        {
            return BadRequest("Bad credentials");
        }
        var token = jwtRepository.CreateToken(user);
        return Ok(token);
    }
    //[HttpPost("/token")]
    //public IActionResult Token(string username, string password)
    //{
    //    var identity = GetIdentity(username, password);
    //    if (identity == null)
    //    {
    //        return BadRequest(new { errorText = "Invalid username or password." });
    //    }

    //    var now = DateTime.UtcNow;
    //    // ������� JWT-�����
    //    var jwt = new JwtSecurityToken(
    //            issuer: AuthOptions.ISSUER,
    //            audience: AuthOptions.AUDIENCE,
    //            notBefore: now,
    //            claims: identity.Claims,
    //            expires: now.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
    //            signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
    //    var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

    //    var response = new
    //    {
    //        access_token = encodedJwt,
    //        username = identity.Name
    //    };

    //    return Json(response);
    //}

    //private ClaimsIdentity GetIdentity(string username, string password)
    //{
    //    var persons = userService.GetUsers;
    //    var person = persons.ToString(x => x.Name == username && x.password == password);
    //    if (person != null)
    //    {
    //        var claims = new List<Claim>
    //            {
    //                new Claim(ClaimsIdentity.DefaultNameClaimType, person.Login),
    //                new Claim(ClaimsIdentity.DefaultRoleClaimType, person.Role)
    //            };
    //        ClaimsIdentity claimsIdentity =
    //        new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
    //            ClaimsIdentity.DefaultRoleClaimType);
    //        return claimsIdentity;
    //    }

    //    // ���� ������������ �� �������
    //    return null;
    //}
}