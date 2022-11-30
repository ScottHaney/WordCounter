using System;
using System.Collections.Generic;
using System.Text;

namespace WordCounting.Counting
{
    public class DefaultWordCountMethod : IWordCountMethod
    {
        public int UpdateCount(int currentCount)
        {
            return currentCount + 1;
        }
    }
}
