using Azure.Identity;
using AzureIdentity.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Graph;
using System.Net.Http.Headers;

namespace AzureIdentity.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnvironmentCredentialController : ControllerBase
    {
        private IMyGraphService _myGraphService;

        public EnvironmentCredentialController(IMyGraphService myGraphService)
        {
            this._myGraphService = myGraphService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            //AZURE_CLIENT_ID=?
            //AZURE_TENANT_ID=?
            //AZURE_CLIENT_SECRET=?

            //add the above mentioned variables to your Visual Studio Environment variables list. Separate them with commas

            var credential = new EnvironmentCredential();

            // the environment credentials will be used with the app registration Service Principal.
            // The ME endpoint requires a delegated authentication provider
            var graph = this._myGraphService.GraphClient(credential);

            return Ok(await graph.Me.Request().GetAsync());
        }

    }
}
