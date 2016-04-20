using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IChequeVendingMachine.Tests
{
    class Change
    {
        public int fivePenceCount = 10 * (int)Money.fivePence;

        public int twentyPenceCount = 2 * (int)Money.twentyPence;

        public int onePoundCount = 2 * (int)Money.onePound;
        
        public int onePenceCount = 7 * (int)Money.penny;
        
        public int twoPenceCount = 1 * (int)Money.twoPence;
    }
}
