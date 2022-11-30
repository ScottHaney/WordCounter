using System;
using System.Collections.Generic;
using System.Text;

namespace WordCounter
{
    public interface IWordCounter
    {
        Dictionary<string, int> Count(string[] text);
    }
}
