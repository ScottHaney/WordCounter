using System;
using System.Collections.Generic;
using System.Text;

namespace WordCounting.Counting
{
    /// <summary>
    /// The count will be a zero or one to indicate if the word is present or not. If the word occurs multiple times it will still be counted as a one.
    /// </summary>
    public class IsPresentWordCountMethod : IWordCountMethod
    {
        public int UpdateCount(int currentCount)
        {
            return 1;
        }
    }
}
