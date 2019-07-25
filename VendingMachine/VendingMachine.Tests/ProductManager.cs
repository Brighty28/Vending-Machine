using System;
using System.Collections.Generic;

namespace VendingMachine.Tests
{
    public class ProductManager
    {
        private readonly Dictionary<string, StockedProduct> stock = new Dictionary<string, StockedProduct>();
        private readonly ReportManager reportManager;
        private IExternalConnection headOfficeConnection;

        public ProductManager(ReportManager reportManager)
        {
            this.reportManager = reportManager;
        }

        public void AddProductToStock(string location, Product product, int quantity)
        {
            StockedProduct stockedProduct;
            if (stock.TryGetValue(location, out stockedProduct))
            {
                if (stockedProduct.Product == product)
                {
                    stockedProduct.AddProductQuantityToStock(quantity);
                }
            }
            else
            {
                stock.Add(location, new StockedProduct(product, quantity));
            }
        }

        public Product GetProductAt(string location)
        {
            StockedProduct stockedProduct;
            if (stock.TryGetValue(location, out stockedProduct))
            {
                stockedProduct.RemoveProductQuantityFromStock(1);
                reportManager.ProductDispensedFrom(location);
                if (stockedProduct.Quantity < 3 && headOfficeConnection != null)
                {
                    headOfficeConnection.StockNotificationFor(stockedProduct.Product.ProductId);
                }
                return stockedProduct.Product;
            }

            return null;
        }

        public bool HasProductAt(string location)
        {
            StockedProduct stockedProduct;
            stock.TryGetValue(location, out stockedProduct);

            return stockedProduct != null && stockedProduct.Quantity > 0;
        }

        public decimal GetPriceAt(string location)
        {
            return stock[location].Product.Price;
        }

        public StockReport GetStockReport()
        {
            return reportManager.GetStockReport();
        }

        public void SetHeadOfficeConnection(IExternalConnection externalConnection)
        {
            headOfficeConnection = externalConnection;
        }

        private class StockedProduct
        {
            public StockedProduct(Product product, int quantity)
            {
                Product = product;
                Quantity = quantity;
            }

            public Product Product { get; private set; }
            public int Quantity { get; private set; }

            public void AddProductQuantityToStock(int quantity)
            {
                Quantity += quantity;
            }

            public void RemoveProductQuantityFromStock(int quantity)
            {
                if (quantity > Quantity)
                {
                    throw new ArgumentException(string.Format("Unable to remove {0} items from stock of {1}", quantity, Quantity));
                }

                Quantity -= quantity;
            }
        }
    }
}