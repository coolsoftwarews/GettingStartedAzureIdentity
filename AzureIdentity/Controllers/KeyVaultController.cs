using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AzureIdentity.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KeyVaultController : ControllerBase
    {

        [HttpGet]
        public async Task<IActionResult> Get()
        {
           var credential = new VisualStudioCredential();

           var secretClient = new SecretClient(new Uri("https://fns-kv-demo.vault.azure.net/"), credential);

           var result = await secretClient.GetSecretAsync("demo-secret");
            
           return Ok(result.Value.Value);
        }
    }
}
