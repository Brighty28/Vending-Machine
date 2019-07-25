using System;
using System.Collections.Generic;
using System.Linq;

namespace VendingMachine.Tests
{
    public class CashManager
    {
        private int balanceInPence;
        private readonly List<Coin> cashFloat = new List<Coin>();
        private readonly ReportManager reportManager;

        public CashManager(ReportManager reportManager)
        {
            this.reportManager = reportManager;
        }

        public void AddCoins(IEnumerable<Coin> insertedCoins)
        {
            var coinsBeingInserted = insertedCoins.ToArray();
            balanceInPence += coinsBeingInserted.Cast<int>().Sum();
            AddToFloat(coinsBeingInserted);
        }

        public ICollection<Coin> GetChange()
        {
            var returnedChange = new List<Coin>();
            var orderedFloat = cashFloat.OrderByDescending(x => x).ToList();
            while (balanceInPence > 0)
            {
                foreach (var coin in orderedFloat)
                {
                    var coinValue = (int)coin;
                    if (balanceInPence >= coinValue)
                    {
                        balanceInPence -= coinValue;
                        returnedChange.Add(coin);
                        reportManager.CoinRemoved(coin);
                    }
                }
            }
            return returnedChange;
        }

        public bool CanAffordRequiredPrice(decimal price)
        {
            return balanceInPence >= Convert.ToInt32(price * 100);
        }

        public void DeductFromBalanceForPrice(decimal price)
        {
            balanceInPence -= Convert.ToInt32(price * 100);
        }

        public void AddToFloat(params Coin[] coins)
        {
            cashFloat.AddRange(coins);
            Array.ForEach(coins, reportManager.CoinAdded);
        }

        public MoneyReport GetMoneyReport()
        {
            return reportManager.GetMoneyReport();
        }
    }
}