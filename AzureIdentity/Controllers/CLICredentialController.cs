using Azure.Identity;
using AzureIdentity.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AzureIdentity.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CLICredentialController : ControllerBase
    {
        private IMyGraphService _myGraphService;

        public CLICredentialController(IMyGraphService myGraphService)
        {
            this._myGraphService = myGraphService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            //make sure you download the Azure CLI: https://docs.microsoft.com/en-us/cli/azure/install-azure-cli
            //open command or PMC and type in az login
            var credential = new AzureCliCredential();

            var graph = this._myGraphService.GraphClient(credential);

            return Ok(await graph.Me.Request().GetAsync());
        }
    }


}
