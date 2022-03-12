using Stylelabs.M.Sdk.WebClient;
using Stylelabs.M.Sdk.WebClient.Authentication;
using System;

namespace ContentHub.Client
{
    public static class ContentHubConnector
    {
        public static IWebMClient Client()
        {
            Uri endpoint = new Uri("https://hztlin03.stylelabsdemo.com");
            OAuthPasswordGrant oauth = new OAuthPasswordGrant
            {
                ClientId = "client_id_chirag",
                ClientSecret = "client_secret_chirag",
                UserName = "chiragwebclientuser",
                Password = "234qwe@#$QWE"
            };
            return MClientFactory.CreateMClient(endpoint, oauth);
        }
    }
}
