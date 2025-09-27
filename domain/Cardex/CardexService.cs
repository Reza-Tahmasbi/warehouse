using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace domain.Cardex
{
    public class CardexService
    {
        /// <summary>
        /// Creates a new cardex entry and calculates the running balance
        /// </summary>
        public static Cardex CreateCardexEntry(
            int productId,
            DateTime transactionDate,
            string reference,
            string description,
            CardexType type,
            int quantityIn,
            int quantityOut,
            decimal unitCost,
            string location,
            string batchNumber = null,
            DateTime? expiryDate = null,
            string serialNumbers = null,
            string notes = null,
            string createdBy = null,
            int? transferId = null,
            int? transferItemId = null)
        {
            var totalCost = (quantityIn + quantityOut) * unitCost;
            
            return new Cardex
            {
                ProductId = productId,
                TransactionDate = transactionDate,
                Reference = reference,
                Description = description,
                Type = type,
                QuantityIn = quantityIn,
                QuantityOut = quantityOut,
                UnitCost = unitCost,
                TotalCost = totalCost,
                Location = location,
                BatchNumber = batchNumber,
                ExpiryDate = expiryDate,
                SerialNumbers = serialNumbers,
                Notes = notes,
                CreatedBy = createdBy,
                TransferId = transferId,
                TransferItemId = transferItemId
            };
        }

        /// <summary>
        /// Calculates the running balance for a product
        /// </summary>
        public static int CalculateRunningBalance(List<Cardex> cardexEntries, int productId)
        {
            return cardexEntries
                .Where(c => c.ProductId == productId)
                .OrderBy(c => c.TransactionDate)
                .ThenBy(c => c.Id)
                .Sum(c => c.QuantityIn - c.QuantityOut);
        }

        /// <summary>
        /// Updates all cardex entries with correct running balances
        /// </summary>
        public static void UpdateRunningBalances(List<Cardex> cardexEntries)
        {
            var sortedEntries = cardexEntries
                .OrderBy(c => c.TransactionDate)
                .ThenBy(c => c.Id)
                .ToList();

            var productBalances = new Dictionary<int, int>();

            foreach (var entry in sortedEntries)
            {
                if (!productBalances.ContainsKey(entry.ProductId))
                {
                    productBalances[entry.ProductId] = 0;
                }

                productBalances[entry.ProductId] += entry.QuantityIn - entry.QuantityOut;
                entry.Balance = productBalances[entry.ProductId];
            }
        }

        /// <summary>
        /// Gets cardex history for a specific product
        /// </summary>
        public static List<Cardex> GetProductHistory(List<Cardex> allCardexEntries, int productId, DateTime? fromDate = null, DateTime? toDate = null)
        {
            var query = allCardexEntries.Where(c => c.ProductId == productId);

            if (fromDate.HasValue)
                query = query.Where(c => c.TransactionDate >= fromDate.Value);

            if (toDate.HasValue)
                query = query.Where(c => c.TransactionDate <= toDate.Value);

            return query
                .OrderBy(c => c.TransactionDate)
                .ThenBy(c => c.Id)
                .ToList();
        }

        /// <summary>
        /// Gets current stock level for a product
        /// </summary>
        public static int GetCurrentStock(List<Cardex> cardexEntries, int productId)
        {
            return cardexEntries
                .Where(c => c.ProductId == productId)
                .Sum(c => c.QuantityIn - c.QuantityOut);
        }
    }
}
