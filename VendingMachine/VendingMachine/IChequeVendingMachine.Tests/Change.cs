using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IChequeVendingMachine.Tests
{
    public class Change
    {
        public int fivePenceCount { get; set; }

        public int twentyPenceCount { get; set; }

        public int onePoundCount { get; set; }

        public int pennyCount { get; set; }

        public int twoPenceCount { get; set; }

        public Change()
        {
            fivePenceCount = 10 * (int)Money.fivePence;

            twentyPenceCount = 2 * (int)Money.twentyPence;

            onePoundCount = 2 * (int)Money.onePound;

            pennyCount = 7 * (int)Money.penny;

            twoPenceCount = 1 * (int)Money.twoPence;
        }

        public int UpdateChangeCount(int changeToUpdate, int amount)
        {
            int bob = 0;
            int fred = amount * 100;

            switch (changeToUpdate)
            {
                case 1:
                    bob += pennyCount = changeToUpdate - fred;
                break;
                case 2:
                    bob += twoPenceCount = changeToUpdate - fred;
                break;
                case 5:
                    bob += fivePenceCount = changeToUpdate - fred;
                break;
                case 20:
                    bob += twentyPenceCount = changeToUpdate - fred;
                break;
                case 100:
                    onePoundCount = - fred;
                    bob += changeToUpdate;
                break;
            }

            return bob;
        }
    }
}
