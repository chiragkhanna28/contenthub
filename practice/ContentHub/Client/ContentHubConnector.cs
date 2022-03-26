using Sitecore.CH.Project.WebClientSDK.Examples;
using Stylelabs.M.Sdk.WebClient;
using Stylelabs.M.Sdk.WebClient.Authentication;
using System;

namespace ContentHub.Client
{
    public static class ContentHubConnector
    {
        public static IWebMClient Client()
        {
            OAuthPasswordGrant oauth = new OAuthPasswordGrant
            {
                ClientId = AppSettings.ClientId,
                ClientSecret = AppSettings.ClientSecret,
                UserName = AppSettings.Username,
                Password = AppSettings.Password
            };
            return MClientFactory.CreateMClient(AppSettings.Host, oauth);
        }
    }
}
