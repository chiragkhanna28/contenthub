using ContentHub.Client;
using System;
using System.Collections.Generic;
using System.Text;

namespace ContentHub.Operations
{
    internal static class DeleteEntity
    {
        public static async void DeleteEntityById(long entityId)
        {
            try
            {
                await ContentHubConnector.Client().Entities.DeleteAsync(entityId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
