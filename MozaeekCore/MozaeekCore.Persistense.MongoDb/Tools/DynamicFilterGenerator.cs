using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using MongoDB.Bson;
using MongoDB.Driver;
using MozaeekCore.QueryModel;

namespace MozaeekCore.Persistense.MongoDb.Tools
{
    public static class DynamicFilterGenerator
    {
        public static FilterDefinition<TDocument> GenerateFilter<TDocument>(this FilterDefinitionBuilder<TDocument> builder, List<SearchParameter> searchParameters)
        {
            IList<FilterDefinition<TDocument>> filters = new List<FilterDefinition<TDocument>>();
            if (searchParameters.Any())
            {
                foreach (var parameter in searchParameters)
                {
                    switch (parameter.SearchType)
                    {
                        case SearchType.ContainText:
                            filters.Add(builder.Regex(parameter.Filed, new BsonRegularExpression($".*{parameter.Value}.*")));
                            break;
                        case SearchType.NotEqualValue:
                            filters.Add(builder.Ne(parameter.Filed, parameter.Value));
                            break;
                        case SearchType.EqualValue:
                            filters.Add(builder.Eq(parameter.Filed, parameter.Value));
                            break;
                        case SearchType.SearchIdInArray:
                            filters.Add(builder.Eq(parameter.Filed, parameter.Value));
                            break;
                            //filters.Add(builder.ElemMatch(parameter.Filed, el => Regex.IsMatch("Id", parameter.Value)));
                            //            filterbuildRequestTarget.ElemMatch(doc => doc.Points, el => el.Id == point.Id);
                            //builder.AnyEq("parent.children.name", new BsonRegularExpression(".*ob.*"))
                            //var filter = Builders<People>.Filter.ElemMatch(x => x.Parent.Children, x => Regex.IsMatch(x.Name, "regex"));
                            //var res = await collection.Find(filter).ToListAsync();
                    }
                }
            }
            else
            {
                filters.Add(builder.Where(_ => true));
            }
            var filterConcat = builder.And(filters);
            return filterConcat;
        }

    }
}