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

        public void UpdateChangeCount(int changeToUpdate, int amount)
        {

            switch (changeToUpdate)
            {
                case 1:
                    pennyCount = amount --;
                break;
                case 2:
                    twoPenceCount = amount --;
                break;
                case 5:
                    fivePenceCount = amount --;
                break;
                case 20:
                    twentyPenceCount = amount --;
                break;
                case 100:
                    onePoundCount = amount --;
                break;
            }

        }
    }
}
