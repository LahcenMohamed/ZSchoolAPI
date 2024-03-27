using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ZSchoolAPI.Models;
using ZSchoolAPI.Models.DTOs;

namespace ZSchoolAPI.Account
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController(UserManager<ApplactionUser> _userManager,RoleManager<IdentityRole> _roleManager,IConfiguration _configuration) : ControllerBase
    {
        private readonly UserManager<ApplactionUser> userManager = _userManager;
        private readonly IConfiguration configuration = _configuration;
        private readonly RoleManager<IdentityRole> roleManager = _roleManager;

        [Authorize(Roles = "Admin")]
        [HttpPost("/Resgister")]
        public async Task<IActionResult> Register([FromForm] RegisterUserDTO registerUserDto)
        {
            if (ModelState.IsValid)
            {
                
                ApplactionUser applactionUser = new()
                {
                    UserName = registerUserDto.UserName,
                    UserType = registerUserDto.UserType,
                    UserTypeId = registerUserDto.UserTypeId
                };
                IdentityResult result = await userManager.CreateAsync(applactionUser, registerUserDto.Password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(applactionUser, registerUserDto.UserType);
                    return Ok("Account Add Success");
                }
                return BadRequest(result.Errors.FirstOrDefault());
            }
            return BadRequest(ModelState);
        }

        [HttpPost("/Login")]
        public async Task<IActionResult> Login([FromForm] LoginDTO loginDTO)
        {
            if (ModelState.IsValid)
            {
                var zahard = await userManager.FindByNameAsync("Zahard");
                if (zahard is null)
                {
                    ApplactionUser Admin = new ApplactionUser
                    {
                        UserName = "Zahard",
                        Email = "zahard@gmail.com",
                        PhoneNumber = "06666",
                        UserType = "Admin",
                        UserTypeId = "0"
                    };

                    var result = await userManager.CreateAsync(Admin, "0558892473");
                    if (result.Succeeded)
                    {
                        await roleManager.CreateAsync(new IdentityRole("Admin"));
                        await roleManager.CreateAsync(new IdentityRole("Teacher"));
                        await roleManager.CreateAsync(new IdentityRole("Student"));
                        await userManager.AddToRoleAsync(Admin, "Admin");
                    }
                }
                var applicationUser = await userManager.FindByNameAsync(loginDTO.UserName);
                if (applicationUser != null)
                {
                    bool found = await userManager.CheckPasswordAsync(applicationUser, loginDTO.Password);
                    if (found)
                    {
                        var claims = new List<Claim>
                            {
                              new Claim(ClaimTypes.Name, applicationUser.UserName),
                              new Claim(ClaimTypes.NameIdentifier, applicationUser.Id),
                              new Claim("UserType", applicationUser.UserType),
                              new Claim("UserTypeId", applicationUser.UserTypeId),
                              new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                            };

                        var roles = await userManager.GetRolesAsync(applicationUser);
                        claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

                        var securityKey = new SymmetricSecurityKey(GenerateSymmetricKey(configuration["JWT:Secret"]));
                        var signincred = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

                        var mytoken = new JwtSecurityToken(
                                issuer: configuration["JWT:ValidIssuer"],
                                audience: configuration["JWT:ValidAudiance"],
                                claims: claims,
                                expires: DateTime.Now.AddMonths(3),
                                signingCredentials: signincred
                            );



                        return Ok(new
                        {
                            token = new JwtSecurityTokenHandler().WriteToken(mytoken),
                            expiration = mytoken.ValidTo,
                            type = applicationUser.UserType
                        });

                    }
                }
                return Unauthorized();
            }
            return BadRequest(ModelState);
        }
        static byte[] GenerateSymmetricKey(string secret)
        {
            // Convert the string secret to bytes using UTF-8 encoding
            byte[] keyBytes = Encoding.UTF8.GetBytes(secret);

            // Ensure the key is at least 256 bits (32 bytes) long
            if (keyBytes.Length < 32)
            {
                // If the key is too short, pad it with zeros or handle it as appropriate for your security requirements
                Array.Resize(ref keyBytes, 32);
            }

            return keyBytes;
        }
    }
}
