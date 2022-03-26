using ContentHub.Client;
using Stylelabs.M.Sdk.Contracts.Base;
using System.Threading.Tasks;

namespace ContentHub.Operations
{
    internal static class UpdateEntity
    {
        internal static async Task UpdateAssetAsync(IEntity assetEntity)
        {
            if (assetEntity != null)
            {
                //property update example
                assetEntity.SetPropertyValue("Title", "Update from the SDK");

                //relation update example
                var assetType = await ContentHubConnector.Client().Entities
                    .GetAsync("M.AssetType.BrandingAsset").ConfigureAwait(false);
                var assetTypeRelation = assetEntity.GetRelation<IChildToOneParentRelation>("AssetTypeToAsset");
                assetTypeRelation.Parent = assetType.Id.Value;

                await ContentHubConnector.Client().Entities.SaveAsync(assetEntity).ConfigureAwait(false);
            }
        }

        internal static async Task TrashAssetAsync(IEntity assetEntity)
        {
            if (assetEntity != null)
            {
                var finalLifeCycleDeleted = ReadEntity.GetEntityIdByIdentifier("M.Final.LifeCycle.Status.Deleted");
                var finalLifeCycleRelation = assetEntity.
                    GetRelation<IChildToOneParentRelation>("FinalLifeCycleStatusToAsset");
                finalLifeCycleRelation.Parent = finalLifeCycleDeleted.Value;

                await ContentHubConnector.Client().Entities.SaveAsync(assetEntity).ConfigureAwait(false);
            }
        }
    }
}
