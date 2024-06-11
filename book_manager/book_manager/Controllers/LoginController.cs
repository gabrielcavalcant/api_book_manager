using book_manager.Models;
using book_manager.Repository;
using book_manager.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Globalization;
using System.IO;
using System.Text;
using System.Threading.Tasks;



namespace book_manager.Controllers
{
    [ApiController]
    [Route("v1")]
    public class LoginController : ControllerBase
    {
        [HttpPost("login")]
        public ActionResult<dynamic> Authenticate([FromBody] LoginModel loginModel)
        {
            try
            {
                var user = UserRepository.Get(loginModel.Username, loginModel.Password);

                if (user == null)
                {
                    return NotFound(new { message = "Usuário ou senha inválidos" });
                }

                var (token, expiration) = TokenService.GenerateToken(user);

                // Obter o fuso horário de São Paulo
                var saoPauloTimeZone = TimeZoneInfo.FindSystemTimeZoneById("America/Sao_Paulo");

                // Converter a data de expiração para o fuso horário de São Paulo
                var localExpiration = TimeZoneInfo.ConvertTimeFromUtc(expiration, saoPauloTimeZone);

                // Formatar a data de expiração para exibição
                var formattedExpiration = localExpiration.ToString("dd-MM-yyyy HH:mm:ss");

                // Gerar o refresh token
                var refreshToken = TokenService.GenerateRefreshToken();

                // Salvar o refresh token
                TokenService.SaveRefreshToken(user.Username, refreshToken);

                // Limpar a senha do usuário antes de retornar os dados
                user.Password = "";

                return new
                {
                    user = user,
                    token = token,
                    refreshToken = refreshToken,
                    expiration = formattedExpiration
                };
            }
            catch (Exception ex)
            {
                return StatusCode(500, JsonConvert.SerializeObject($"Erro ao autenticar: {ex.Message}"));
            }
        }




        [HttpPost("refresh")]
        public IActionResult Refresh(string token, string refreshToken)
        {
            try
            {
                if (string.IsNullOrEmpty(token) || string.IsNullOrEmpty(refreshToken))
                {
                    return BadRequest(new { message = "Token ou token de atualização inválidos" });
                }

                var principal = TokenService.GetPrincipalFromExpiredToken(token);
                if (principal == null)
                {
                    return Unauthorized(new { message = "Token de acesso inválido" });
                }

                var username = principal.Identity.Name;
                var savedRefreshToken = TokenService.GetRefreshToken(username);
                if (savedRefreshToken != refreshToken)
                {
                    return Unauthorized(new { message = "Token de atualização inválido" });
                }

                // Aqui você pode verificar as permissões do usuário, se necessário

                var newJwtToken = TokenService.GenerateToken(principal.Claims);
                var newRefreshToken = TokenService.GenerateRefreshToken();
                TokenService.DeleteRefreshToken(username, refreshToken);
                TokenService.SaveRefreshToken(username, newRefreshToken);
                var expiration = newJwtToken.Expiration; ; // Supondo que ValidTo contém a data de expiração do token

                // Converter a data de expiração para o fuso horário de São Paulo, se necessário
                var saoPauloTimeZone = TimeZoneInfo.FindSystemTimeZoneById("America/Sao_Paulo");
                var localExpiration = TimeZoneInfo.ConvertTimeFromUtc(expiration, saoPauloTimeZone);

                // Formatar a data de expiração para exibição
                var formattedExpiration = localExpiration.ToString("dd-MM-yyyy HH:mm:ss");


                return Ok(new
                {
                    token = newJwtToken,
                    refreshToken = newRefreshToken,
                    expiration = formattedExpiration
                });
            }
            catch (SecurityTokenException)
            {
                return Unauthorized(new { message = "Token de acesso inválido" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, JsonConvert.SerializeObject($"Erro ao atualizar token: {ex.Message}"));
            }
        }

    }
        
}
