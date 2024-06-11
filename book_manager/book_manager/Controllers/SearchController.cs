using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using book_manager.Business;
using book_manager;
using book_manager.Models;

[Route("api/[controller]")]
[ApiController]
public class SearchController : ControllerBase
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
    [HttpPost("validate")]
    public IActionResult ValidateToken()
    {
        try
        {
            // Verifica se o token de autoriza��o est� presente no cabe�alho da solicita��o
            if (!HttpContext.Request.Headers.ContainsKey("Authorization"))
            {
                return BadRequest(new { message = "Favor inserir o token" });
            }

            var token = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

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

            // Verifica se o token � v�lido
            if (!IsTokenValid(token))
            {
                return BadRequest(new { message = "Token expirado!" });
            }

            // Verifica se o usu�rio tem permiss�o para acessar a rota
            var roles = principal.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value).ToList();
            if (!roles.Contains("Manager") && !roles.Contains("ManagerPolicy"))
            {
                return Unauthorized(new { message = "Token sem permiss�o" });
            }

            return Ok(new { message = "Token v�lido" });
        }
        catch (Exception ex)
        {
           

            return StatusCode(500, new { message = $"Ocorreu um erro: {ex.Message}" });
        }
    }

    [HttpGet("books")]
    public ActionResult GetBooks()
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

            // Buscar todos os livros
            var books = _business.GetBooks();

            return Content(JsonConvert.SerializeObject(books), "application/json");
        }
        catch (Exception ex)
        {
            return StatusCode(500, JsonConvert.SerializeObject($"Ocorreu um erro: {ex.Message}"));
        }
    }


    [HttpPost("{titulo}/{genero}/{Emprestimo}")]
    public ActionResult Post(string titulo, string genero, bool Emprestimo)
    {
        try
        {
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

            if (!HttpContext.User.IsInRole("Manager") && !HttpContext.User.IsInRole("ManagerPolicy"))
            {
                return Unauthorized(new { message = "Token sem permiss�o" });
            }

            var livro = new Livro
            {
                Titulo = titulo,
                Genero = genero,
                Emprestimo = Emprestimo
            };

            _business.AddBook(livro);
            return Ok(new { message = "Livro adicionado com sucesso!" });
        }
        catch (Exception ex)
        {
            return StatusCode(500, JsonConvert.SerializeObject($"Ocorreu um erro: {ex.Message}"));
        }
    }
    [HttpPut("{id}/{titulo}/{genero}/{Emprestimo}")]
    public ActionResult Put(int id, string titulo, string genero, bool Emprestimo)
    {
        try
        {
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

            if (!HttpContext.User.IsInRole("Manager") && !HttpContext.User.IsInRole("ManagerPolicy"))
            {
                return Unauthorized(new { message = "Token sem permiss�o" });
            }

            var livro = new Livro
            {
                Id = id,
                Titulo = titulo,
                Genero = genero,
                Emprestimo = Emprestimo
            };

            _business.UpdateBook(id, livro);
            return Ok(new { message = "Livro atualizado com sucesso!" });
        }
        catch (Exception ex)
        {
            return StatusCode(500, JsonConvert.SerializeObject($"Ocorreu um erro: {ex.Message}"));
        }
    }
    [HttpDelete("{id}")]
    public ActionResult Delete(int id)
    {
        try
        {
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

            if (!HttpContext.User.IsInRole("Manager") && !HttpContext.User.IsInRole("ManagerPolicy"))
            {
                return Unauthorized(new { message = "Token sem permiss�o" });
            }

            _business.DeleteBook(id);
            return Ok(new { message = "Livro removido com sucesso!" });
        }
        catch (Exception ex)
        {
            return StatusCode(500, JsonConvert.SerializeObject($"Ocorreu um erro: {ex.Message}"));
        }
    }
    private static bool IsTokenValid(string token)
    {
        var expirationToken = GetExpirationDate(token);
        if (expirationToken > DateTime.UtcNow)
        {
            // O token ainda n�o expirou
            return true;
        }
        else
        {
            return false;// O token expirou ou n�o tem data de expira��o definido///
        }
    }
    public static DateTime? GetExpirationDate(string token)
    {
        var jwtHandler = new JwtSecurityTokenHandler();

        if (jwtHandler.CanReadToken(token))
        {
            try
            {
                var jwtToken = jwtHandler.ReadToken(token) as JwtSecurityToken;
                if (jwtToken != null)
                {
                    var expirationDate = jwtToken.ValidTo;
                    return expirationDate;
                }
            }
            catch (Exception ex)
            {
                return null;
            }

        }
        return null;
    }

}



