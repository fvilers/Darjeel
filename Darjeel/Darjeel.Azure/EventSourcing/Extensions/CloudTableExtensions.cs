using Microsoft.WindowsAzure.Storage.Table;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Darjeel.Azure.EventSourcing.Extensions
{
    internal static class CloudTableExtensions
    {
        public static Task<IList<T>> ExecuteQueryAsync<T>(this CloudTable table, TableQuery<T> query)
            where T : ITableEntity, new()
        {
            return table.ExecuteQueryAsync(query, CancellationToken.None);
        }

        public static async Task<IList<T>> ExecuteQueryAsync<T>(this CloudTable table, TableQuery<T> query, CancellationToken cancellationToken)
            where T : ITableEntity, new()
        {
            var items = new List<T>();
            TableContinuationToken token = null;

            do
            {

                var segment = await table.ExecuteQuerySegmentedAsync(query, token, cancellationToken);
                token = segment.ContinuationToken;
                items.AddRange(segment);

            } while (token != null);

            return items;
        }
    }
}
