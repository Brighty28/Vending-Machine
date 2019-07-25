using System.Collections.Generic;

namespace VendingMachine.Tests
{
    public class VendingMachine
    {
        private readonly CashManager cashManager;
        private readonly ProductManager productManager;
        private string message;
        private IExternalConnection headOfficeConnection;

        public VendingMachine(CashManager cashManager, ProductManager productManager)
        {
            this.cashManager = cashManager;
            this.productManager = productManager;
            message = "Ready";
        }

        public void Insert(params Coin[] insertedCoins)
        {
            cashManager.AddCoins(insertedCoins);
        }

        public void SetProduct(string location, Product product, int quantity = 1)
        {
            productManager.AddProductToStock(location, product, quantity);
        }

        public void SetHeadOfficeConnection(IExternalConnection externalConnection)
        {
            headOfficeConnection = externalConnection;
            productManager.SetHeadOfficeConnection(headOfficeConnection);
        }

        public void SetChange(params Coin[] coins)
        {
            cashManager.AddToFloat(coins);
        }

        public ICollection<Coin> Reject()
        {
            return cashManager.GetChange();
        }

        public Product Select(string location)
        {
            if (productManager.HasProductAt(location))
            {
                var productPrice = productManager.GetPriceAt(location);
                if (cashManager.CanAffordRequiredPrice(productPrice))
                {
                    cashManager.DeductFromBalanceForPrice(productPrice);
                    message = "Dispensing product from " + location;
                    return productManager.GetProductAt(location);
                }
            }
            message = "No product available at " + location;
            return null;
        }

        public string GetMessage()
        {
            return message;
        }

        public StockReport GetStockReport()
        {
            return productManager.GetStockReport();
        }

        public MoneyReport GetMoneyReport()
        {
            return cashManager.GetMoneyReport();
        }
    }
}