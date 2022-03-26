using ContentHub.Client;
using System.Linq;
using Stylelabs.M.Base.Querying;
using Stylelabs.M.Base.Querying.Linq;
using Stylelabs.M.Framework.Essentials.LoadConfigurations;
using Stylelabs.M.Sdk.Contracts.Base;
using Stylelabs.M.Sdk.Contracts.Querying;
using System.Threading.Tasks;
using System.Collections.Generic;
using Stylelabs.M.Framework.Essentials.LoadOptions;
using System;

namespace ContentHub.Operations
{
    public static class ReadEntity
    {
        public static async Task<IEntity> GetEntityById(long entityId)
        {
            var entityLoadConfiguration = new EntityLoadConfiguration(
                CultureLoadOption.None,
                new PropertyLoadOption("Title", "FileName"),
                new RelationLoadOption("AssetTypeToAsset")
            );
            var query = Query.CreateQuery(entities => from e in entities
                                                      where e.Id == entityId
                                                      select e);

            return await ContentHubConnector.Client().Querying.SingleAsync(query, EntityLoadConfiguration.Full);
        }

        public static async Task<IEntityQueryResult> GetEntitiesByDefinition(string definitionName)
        {
            var query = Query.CreateQuery(entities => from e in entities
                                                      where e.DefinitionName == definitionName &&
                                                      e.Parent("ContentRepositoryToAsset") == 734
                                                      select e);
            query.Take = 150;

            return await ContentHubConnector.Client().Querying.QueryAsync(query, EntityLoadConfiguration.Default);
        }

        public static async Task<IEntityQueryResult> GetEntitiesByTitle(string title)
        {
            var query = Query.CreateQuery(entities => from e in entities
                                                      where e.DefinitionName == "M.Asset"
                                                      && e.Property("Title") == title
                                                      select e);

            return await ContentHubConnector.Client().Querying.QueryAsync(query, EntityLoadConfiguration.Default);
        }

        public static async Task<IEntityQueryResult> GetAssetsByAssetType(long assetTypeId)
        {
            var query = Query.CreateQuery(entities => from e in entities
                                                      where e.DefinitionName == "M.Asset" &&
                                                      e.Parent("AssetTypeToAsset") == assetTypeId
                                                      select e);

            return await ContentHubConnector.Client().Querying.QueryAsync(query, EntityLoadConfiguration.Default);
        }
        public static async Task<List<IEntity>> GetEntitiesByDefinitionByIterator(string definitionName)
        {
            List<IEntity> entities = new List<IEntity>();
            var query = Query.CreateQuery(entities => from e in entities
                                                      where e.DefinitionName == definitionName &&
                                                      e.Parent("ContentRepositoryToAsset") == 734 //Standard (DAM)
                                                      select e);
            var iterator = ContentHubConnector.Client().Querying.CreateEntityIterator(query, EntityLoadConfiguration.Default);
            while (await iterator.MoveNextAsync().ConfigureAwait(false))
            {
                entities.AddRange(iterator.Current.Items);
            }
            return entities;
        }
        public static async Task<List<IEntity>> GetEntitiesByDefinitionByScroller(string definitionName)
        {
            List<IEntity> entities = new List<IEntity>();
            var query = Query.CreateQuery(entities => from e in entities
                                                      where e.DefinitionName == definitionName &&
                                                      e.Parent("ContentRepositoryToAsset") == 734
                                                      select e);

            var scroller = ContentHubConnector.Client().Querying.CreateEntityScroller(query, TimeSpan.FromSeconds(30),
                EntityLoadConfiguration.DefaultCultureFull);

            scroller.Reset();

            while (await scroller.MoveNextAsync().ConfigureAwait(false))
            {
                entities.AddRange(scroller.Current.Items);
            }
            return entities;
        }
        public static long? GetEntityIdByIdentifier(string identifier)
        {
            var query = Query.CreateQuery(entities => from e in entities
                                                      where e.Identifier == identifier
                                                      select e);

            return ContentHubConnector.Client().Querying.SingleIdAsync(query).Result;
        }
    }
}
