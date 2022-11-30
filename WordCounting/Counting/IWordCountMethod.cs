using System;
using System.Collections.Generic;
using System.Text;

namespace WordCounting.Counting
{
    public interface IWordCountMethod
    {
        int UpdateCount(int currentCount);
    }
}
