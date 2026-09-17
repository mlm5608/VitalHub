using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using WebAPI.Domains;
using WebAPI.Interfaces;
using WebAPI.Repositories;
using WebAPI.Utils.BlobStorage;
using WebAPI.ViewModels;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private IUsuarioRepository usuarioRepository { get; set; }

        public UsuarioController()
        {
            usuarioRepository = new UsuarioRepository();
        }

        [HttpPut("AlterarSenha")]
        public IActionResult UpdatePassword(string email, AlterarSenhaViewModel senha)
        {
            try
            {
                usuarioRepository.AlterarSenha(email, senha.SenhaNova!);

                return Ok("Senha alterada com sucesso !");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("BuscarPorId")]
        public IActionResult GetById(Guid id)
        {
            try
            {
                return Ok(usuarioRepository.BuscarPorId(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("AlterarFotoPerfil")]
        public async Task<IActionResult> UploadProfileImage(Guid id,[FromForm] UsuarioViewModel user)
        {
            try
            {
               Usuario usuarioBuscado = usuarioRepository.BuscarPorId(id);
                if (usuarioBuscado == null)
                {
                    return NotFound();
                }

                var connectionString = "DefaultEndpointsProtocol=https;AccountName=blobvitalhubg6guiluiz;AccountKey=YNCYMRd8g+WRNx5j+JxzndjqbxrwYscq9xJhpPAd11g0E0Xt3QGT5Gab6QISIsuv01u8RUKvT0Hd+ASt0kefow==;EndpointSuffix=core.windows.net";

                var containerName = "containervitalhubguiluiz";

                string fotoUrl = await AzureBlobStorageHelper.UploadImageBLobAsync(user.Arquivo!, connectionString, containerName);
                //fim do upload da imagem

                usuarioRepository.AtualizarFoto(id, fotoUrl);

                return Ok();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
