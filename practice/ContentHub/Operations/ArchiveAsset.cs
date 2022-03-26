using ContentHub.Client;
using System;
using System.Collections.Generic;
using System.Text;

namespace ContentHub.Operations
{
    internal static class ArchiveAsset
    {
        public static async void ArchiveAssetById(long entityId)
        {
            try
            {
                await ContentHubConnector.Client().Assets.FinalLifeCycleManager.ArchiveAsync(entityId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
