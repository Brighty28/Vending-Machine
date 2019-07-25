namespace VendingMachine.Tests
{
    public class ReportManager
    {
        private readonly StockReport stockReport = new StockReport();
        private readonly MoneyReport moneyReport = new MoneyReport();

        public void ProductDispensedFrom(string location)
        {
            stockReport.AddDispensedAt(location);
        }

        public void CoinAdded(Coin coin)
        {
            moneyReport.Add(coin);
        }

        public void CoinRemoved(Coin coin)
        {
            moneyReport.Remove(coin);
        }

        public StockReport GetStockReport()
        {
            return stockReport;
        }

        public MoneyReport GetMoneyReport()
        {
            return moneyReport;
        }
    }
}