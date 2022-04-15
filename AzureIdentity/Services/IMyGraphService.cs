using Azure.Core;
using Azure.Identity;
using Microsoft.Graph;
using System.Net.Http.Headers;

namespace AzureIdentity.Services
{
    public interface IMyGraphService
    {
        GraphServiceClient GraphClient(TokenCredential tokenCredential);
    }

    public class MyGraphService : IMyGraphService
    {
        public GraphServiceClient GraphClient(TokenCredential tokenCredential)
        {
            if(typeof(EnvironmentCredential).Equals(tokenCredential.GetType()))
                return  this.UseDelegateAuthenticationProvider(tokenCredential as EnvironmentCredential);

            return new GraphServiceClient(tokenCredential, scopes: new[] { "https://graph.microsoft.com/.default" });
        }

        private GraphServiceClient UseDelegateAuthenticationProvider(EnvironmentCredential environmentCredential)
        {
            var token = environmentCredential.GetToken(
                   new Azure.Core.TokenRequestContext(
                       new[] { "https://graph.microsoft.com/.default" }));

            var accessToken = token.Token;
            return new GraphServiceClient(
                new DelegateAuthenticationProvider((requestMessage) =>
                {
                    requestMessage
                    .Headers
                    .Authorization = new AuthenticationHeaderValue("bearer", accessToken);

                    return Task.CompletedTask;
                }));
        }
    }

}
