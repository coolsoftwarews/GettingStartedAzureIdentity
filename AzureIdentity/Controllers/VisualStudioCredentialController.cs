using Azure.Identity;
using AzureIdentity.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Graph;

namespace AzureIdentity.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VisualStudioCredentialController : ControllerBase
    {
        private IMyGraphService _myGraphService;

        public VisualStudioCredentialController(IMyGraphService myGraphService)
        {
            this._myGraphService = myGraphService;
        }
    
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var credential = new VisualStudioCredential();

            var graph = this._myGraphService.GraphClient(credential);

            return Ok(await graph.Users.Request().GetAsync());
        }

    }
}
