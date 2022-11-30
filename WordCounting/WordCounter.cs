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
        private readonly ICharacterIdentifier _characterIdentifier;
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
            _characterIdentifier = characterIdentifier ?? new CharacterIdentifier();
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

        private Dictionary<string, int> CountInternal(string text, Dictionary<string, int> currentResults)
        {
            text = text ?? string.Empty;

            int? startIndex = null;
            var results = currentResults ?? new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);

            for (int i = 0; i < text.Length; i++)
            {
                var currentChar = text[i];
                if (_characterIdentifier.IsWordCharacterThatCanStartAWord(currentChar))
                {
                    if (startIndex == null)
                        startIndex = i;
                }
                else if (_characterIdentifier.IsWordCharacterThatCantStartAWord(currentChar))
                { }
                else if (startIndex != null)
                {
                    AddWord(text, startIndex.Value, i - 1, results);
                    startIndex = null;
                }
            }

            //Make sure to the last word at the end of the text in case there is no ending punctuation
            if (startIndex != null)
                AddWord(text, startIndex.Value, text.Length - 1, results);

            return results;
        }

        private void AddWord(string text, int startIndex, int endIndex, Dictionary<string, int> results)
        {
            var word = text.Substring(startIndex, endIndex - startIndex + 1);
            if (results.TryGetValue(word, out var match))
                results[word] = _wordCountMethod.UpdateCount(match);
            else
                results[word] = _wordCountMethod.UpdateCount(0);
        }
    }
}
