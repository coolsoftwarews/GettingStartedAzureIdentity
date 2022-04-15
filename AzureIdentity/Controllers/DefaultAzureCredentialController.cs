using Azure.Identity;
using AzureIdentity.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AzureIdentity.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DefaultAzureCredentialController : ControllerBase
    {
        private IMyGraphService _myGraphService;

        public DefaultAzureCredentialController(IMyGraphService myGraphService)
        {
            this._myGraphService = myGraphService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var credential = new DefaultAzureCredential();

            var graph = this._myGraphService.GraphClient(credential);

            //this will fail because we have Environment variables setup for the EnvironmentCredential implementation.
            //whe have not specified that the DefaultAzureCredential should use the Delegated Authenticated Provider class
            
            //return Ok(await graph.Me.Request().GetAsync())

            //hence, we use the users endpoint, as we have specified an application permission for this.
            return Ok(await graph.Users.Request().GetAsync());
        }
    }
}
