using PlanillaUnicaWeb.Models;
using Microsoft.Graph;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace GestionAdministracionWeb.Helper
{
    public class GraphHelper
    {
        public static async Task<CachedUser> GetUserDetailsAsync(string accessToken)
        {
            var graphClient = new GraphServiceClient(
                new DelegateAuthenticationProvider(
                    (requestMessage) => {
                        requestMessage.Headers.Authorization =
                           new AuthenticationHeaderValue("Bearer", accessToken);
                        return Task.CompletedTask;
                    }));

            var user = await graphClient.Me.Request()
                .Select(u => new {
                    u.DisplayName,
                    u.Mail,
                    u.UserPrincipalName
                })
                .GetAsync();

            return new CachedUser
            {
                Avatar = string.Empty,
                DisplayName = user.DisplayName,
                Email = string.IsNullOrEmpty(user.Mail) ?
                    user.UserPrincipalName : user.Mail
            };
        }
    }
}