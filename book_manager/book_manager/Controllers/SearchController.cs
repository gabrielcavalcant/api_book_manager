using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using booke_manager.Business;

[Route("api/[controller]")]
[ApiController]
public class LivrosController : ControllerBase
{
    private readonly Business _business;

    public SearchController(Business business)
    {
        _business = business;
    }

    [HttpGet("{genero}/{titulo}")]
    public ActionResult Get(string genero, string titulo)
    {
        try
        {
            // Verificar se o cabeçalho Authorization está presente
            if (!HttpContext.Request.Headers.ContainsKey("Authorization"))
            {
                return BadRequest(new { message = "Favor inserir o token" });
            }

            var token = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            if (!IsTokenValid(token))
            {
                return BadRequest(new { message = "Token expirado!" });
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(Settings.Secret);

            // Configura os parâmetros de validação do token
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key)
            };

            SecurityToken validatedToken;
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out validatedToken);
            var userId = principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            // Verificar se o usuário tem as permissões necessárias
            if (!HttpContext.User.IsInRole("Manager") && !HttpContext.User.IsInRole("ManagerPolicy"))
            {
                return Unauthorized(new { message = "Token sem permissão" });
            }

            // Filtrar livros por gênero e título
            var livros = _business.GetLivrosByGeneroAndTitulo(genero, titulo);

            return Content(JsonConvert.SerializeObject(livros), "application/json");
        }
        catch (Exception ex)
        {
            return StatusCode(500, JsonConvert.SerializeObject($"Ocorreu um erro: {ex.Message}"));
        }
    }

    private bool IsTokenValid(string token)
    {
        // Implementar a validação do token conforme necessário
        return true;
    }
}
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using booke_manager.Business;

[Route("api/[controller]")]
[ApiController]
public class LivrosController : ControllerBase
{
    private readonly Business _business;

    public SearchController(Business business)
    {
        _business = business;
    }

    [HttpGet("{genero}/{titulo}")]
    public ActionResult Get(string genero, string titulo)
    {
        try
        {
            // Verificar se o cabeçalho Authorization está presente
            if (!HttpContext.Request.Headers.ContainsKey("Authorization"))
            {
                return BadRequest(new { message = "Favor inserir o token" });
            }

            var token = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            if (!IsTokenValid(token))
            {
                return BadRequest(new { message = "Token expirado!" });
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(Settings.Secret);

            // Configura os parâmetros de validação do token
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key)
            };

            SecurityToken validatedToken;
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out validatedToken);
            var userId = principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            // Verificar se o usuário tem as permissões necessárias
            if (!HttpContext.User.IsInRole("Manager") && !HttpContext.User.IsInRole("ManagerPolicy"))
            {
                return Unauthorized(new { message = "Token sem permissão" });
            }

            // Filtrar livros por gênero e título
            var livros = _business.GetLivrosByGeneroAndTitulo(genero, titulo);

            return Content(JsonConvert.SerializeObject(livros), "application/json");
        }
        catch (Exception ex)
        {
            return StatusCode(500, JsonConvert.SerializeObject($"Ocorreu um erro: {ex.Message}"));
        }
    }

    private bool IsTokenValid(string token)
    {
        // Implementar a validação do token conforme necessário
        return true;
    }
}
