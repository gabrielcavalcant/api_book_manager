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
            // Verificar se o cabe�alho Authorization est� presente
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

            // Configura os par�metros de valida��o do token
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

            // Verificar se o usu�rio tem as permiss�es necess�rias
            if (!HttpContext.User.IsInRole("Manager") && !HttpContext.User.IsInRole("ManagerPolicy"))
            {
                return Unauthorized(new { message = "Token sem permiss�o" });
            }

            // Filtrar livros por g�nero e t�tulo
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
        // Implementar a valida��o do token conforme necess�rio
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
            // Verificar se o cabe�alho Authorization est� presente
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

            // Configura os par�metros de valida��o do token
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

            // Verificar se o usu�rio tem as permiss�es necess�rias
            if (!HttpContext.User.IsInRole("Manager") && !HttpContext.User.IsInRole("ManagerPolicy"))
            {
                return Unauthorized(new { message = "Token sem permiss�o" });
            }

            // Filtrar livros por g�nero e t�tulo
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
        // Implementar a valida��o do token conforme necess�rio
        return true;
    }
}
