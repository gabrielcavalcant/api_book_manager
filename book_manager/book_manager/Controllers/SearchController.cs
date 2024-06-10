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
    [HttpPost("validate")]
    public IActionResult ValidateToken()
    {
        try
        {
            // Verifica se o token de autorização está presente no cabeçalho da solicitação
            if (!HttpContext.Request.Headers.ContainsKey("Authorization"))
            {
                return BadRequest(new { message = "Favor inserir o token" });
            }

            var token = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

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

            // Verifica se o token é válido
            if (!IsTokenValid(token))
            {
                return BadRequest(new { message = "Token expirado!" });
            }

            // Verifica se o usuário tem permissão para acessar a rota
            var roles = principal.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value).ToList();
            if (!roles.Contains("Manager") && !roles.Contains("ManagerPolicy"))
            {
                return Unauthorized(new { message = "Token sem permissão" });
            }

            return Ok(new { message = "Token válido" });
        }
        catch (Exception ex)
        {
            // Logar o erro (dependendo do framework de logging que você está usando)
            // Logger.LogError(ex, "An error occurred while validating the token.");

            return StatusCode(500, new { message = $"Ocorreu um erro: {ex.Message}" });
        }
    }


    private bool IsTokenValid(string token)
    {
        // Implementar a validação do token conforme necessário
        return true;
    }
    [HttpPost("{titulo}/{genero}/{emprestado}")]
    public ActionResult Post(string titulo, string genero, bool emprestado)
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
                return Unauthorized(new { message = "Token sem permissão" });
            }

            var livro = new Livro
            {
                Titulo = titulo,
                Genero = genero,
                Emprestado = emprestado
            };

            _business.AddBook(livro);
            return Ok(new { message = "Livro adicionado com sucesso!" });
        }
        catch (Exception ex)
        {
            return StatusCode(500, JsonConvert.SerializeObject($"Ocorreu um erro: {ex.Message}"));
        }
    }
    [HttpPut("{id}/{titulo}/{genero}/{emprestado}")]
    public ActionResult Put(int id, string titulo, string genero, bool emprestado)
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
                return Unauthorized(new { message = "Token sem permissão" });
            }

            var livro = new Livro
            {
                Id = id,
                Titulo = titulo,
                Genero = genero,
                Emprestado = emprestado
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
                return Unauthorized(new { message = "Token sem permissão" });
            }

            _business.DeleteBook(id);
            return Ok(new { message = "Livro removido com sucesso!" });
        }
        catch (Exception ex)
        {
            return StatusCode(500, JsonConvert.SerializeObject($"Ocorreu um erro: {ex.Message}"));
        }
    }


}

