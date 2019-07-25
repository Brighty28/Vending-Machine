using System;
using System.Collections.Generic;
using System.Linq;

namespace VendingMachine.Tests
{
    public class MoneyReport
    {
        private readonly Dictionary<Coin, int> coins;

        public MoneyReport()
        {
            coins = Enum.GetValues(typeof(Coin)).Cast<Coin>().ToDictionary(coin => coin, initialCount => 0);
        }

        public void Add(Coin coin)
        {
            coins[coin] += 1;
        }

        public void Remove(Coin coin)
        {
            coins[coin] -= 1;
        }

        public int NumberOf(Coin coin)
        {
            return coins[coin];
        }
    }
}