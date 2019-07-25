using System.Collections.Generic;

namespace VendingMachine.Tests
{
    public class StockReport
    {
        private readonly Dictionary<string, int> stockDispensed = new Dictionary<string, int>();

        public int QuantityDispensedFrom(string location)
        {
            stockDispensed.TryGetValue(location, out var quantityDispensed);

            return quantityDispensed;
        }

        public void AddDispensedAt(string location)
        {
            if (stockDispensed.ContainsKey(location))
            {
                stockDispensed[location] += 1;
            }
            else
            {
                stockDispensed[location] = 1;
            }
        }
    }
}