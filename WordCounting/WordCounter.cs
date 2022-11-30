using System;
using System.Collections.Generic;
using System.Linq;
using WordCounting.CharacterIdentification;
using WordCounting.Counting;

namespace WordCounting
{
    /// <summary>
    /// Counts the number of times each word occurs in English text
    /// </summary>
    public class WordCounter : IWordCounter
    {
        private readonly WordParser _wordParser;
        private readonly IWordCountMethod _wordCountMethod;
        private readonly bool _mergeCounts;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="characterIdentifier"></param>
        /// <param name="wordCountMethod"></param>
        /// <param name="mergeCounts">If true will calcluate separate counts for each text passed in and then merge them together into the total of each. This can be useful when using <see cref="IsPresentWordCountMethod"/> for instance to tell how many of the text arguments used a specific word at least once.</param>
        public WordCounter(ICharacterIdentifier characterIdentifier = null,
            IWordCountMethod wordCountMethod = null,
            bool mergeCounts = false)
        {
            _wordParser = new WordParser(characterIdentifier ?? new CharacterIdentifier());
            _wordCountMethod = wordCountMethod ?? new DefaultWordCountMethod();
            _mergeCounts = mergeCounts;
        }

        public Dictionary<string, int> Count(params string[] texts)
        {
            if (texts == null)
                return new Dictionary<string, int>();

            Dictionary<string, int> results = null;
            foreach (var text in texts)
            {
                if (_mergeCounts)
                {
                    var currentResults = CountInternal(text, null);
                    if (results == null)
                        results = currentResults;
                    else
                    {
                        foreach (var pair in currentResults)
                        {
                            if (results.TryGetValue(pair.Key, out int value))
                                results[pair.Key] = value + pair.Value;
                            else
                                results[pair.Key] = pair.Value;
                        }
                    }
                }
                else
                    results = CountInternal(text, results);
            }

            return results;
        }

        private Dictionary<string, int> CountInternal(string text, Dictionary<string, int> results)
        {
            results = results ?? new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
            foreach (var word in _wordParser.Parse(text))
                AddWord(word, results);

            return results;
        }

        private void AddWord(string word, Dictionary<string, int> results)
        {
            if (results.TryGetValue(word, out var match))
                results[word] = _wordCountMethod.UpdateCount(match);
            else
                results[word] = _wordCountMethod.UpdateCount(0);
        }
    }
}
