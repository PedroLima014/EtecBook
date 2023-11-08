using System.Net.Mail;
using EtecBookAPI.Data;
using EtecBookAPI.DataTransferObjects;
using EtecBookAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EtecBookAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AccountController : ControllerBase
{
    private readonly AppDbContext _context;

    public AccountController(AppDbContext context)
    {
        _context = context;
    }

    [HttpPost("authenticate")]
    public async Task<IActionResult> Authenticate([FromBody] LoginDto login) 
    {
        if (login == null)
            return BadRequest();

        if (!ModelState.IsValid)
            return BadRequest();

        AppUser user = new();
        if(IsValidEmail(login.Email))
        {
            user = await _context.Users.FirstOrDefaultAsync(
                u => u.Email == login.Email && u.Password == login.Password
            );
        }
        else // login por username
        {
            user = await _context.Users.FirstOrDefaultAsync(
                u => u.UserName == login.Email && u.Password == login.Password
            );
        }
        if(user == null)
            return NotFound(new { Message = "Usuário e/ou Senha Inválidos!!" });
    
        return Ok(new { Message = "Login com sucesso" });
    }

    [HttpPost("register")]
    public async Task<IActionResult> RegisterUser([FromBody] RegisterDto register)
    {
        if (register == null)
            return BadRequest();
        
        if (!ModelState.IsValid)
            return BadRequest();

        if (await _context.Users.AnyAsync(u => u.Email.Equals(register.Email)))
            return BadRequest(new { Message = "Email já cadastrado!" });

        AppUser user = new()
        {
            Id = Guid.NewGuid(),
            Name = register.Name,
            Email = register.Email,
            Password = register.Password,
            Role = "Usuário",
            UserName = register.Email
        };

        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();

        return Ok(new { Message = "Usário registrado com sucesso" });
    }

    private static bool IsValidEmail(string email)
    {
        try
        {
            MailAddress mail =new (email);
            return true;
        }
        catch
        {
            return false;
        }
    }
    
}
