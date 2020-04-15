using BlowFishCS;
using DatingApp.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace DatingApp.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EncryptionsController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            BlowFish blowFish = new BlowFish("@dm1n1z7r@t0r!!!");
            EncryptResult encryptResult = new EncryptResult();
            encryptResult.serverName = blowFish.Encrypt_CBC("localhost");
            encryptResult.dbName = blowFish.Encrypt_CBC("DatingApp");
            encryptResult.userName = blowFish.Encrypt_CBC("sa");
            encryptResult.password = blowFish.Encrypt_CBC("password1");

            return Ok(encryptResult);
        }
    }
}