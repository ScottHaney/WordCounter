using System;
using System.Collections.Generic;
using System.Text;

namespace WordCounting
{
    public interface IWordCounter
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="texts">The result will be the combined word counts from all of these strings.</param>
        /// <returns></returns>
        Dictionary<string, int> Count(string[] text);
    }
}
