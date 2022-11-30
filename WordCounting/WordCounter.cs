using System;
using System.Collections.Generic;
using System.Linq;
using WordCounting.CharacterIdentification;

namespace WordCounting
{
    /// <summary>
    /// Counts the number of times each word occurs in English text
    /// </summary>
    public class WordCounter : IWordCounter
    {
        private readonly ICharacterIdentifier _characterIdentifier;

        public WordCounter(ICharacterIdentifier characterIdentifier = null)
        {
            _characterIdentifier = characterIdentifier ?? new CharacterIdentifier();
        }

        public Dictionary<string, int> Count(params string[] texts)
        {
            if (texts == null)
                return new Dictionary<string, int>();

            Dictionary<string, int> results = null;
            foreach (var text in texts)
                results = Count(text, results);

            return results;
        }

        private Dictionary<string, int> Count(string text, Dictionary<string, int> currentResults)
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
                results[word] = match + 1;
            else
                results[word] = 1;
        }
    }
}
