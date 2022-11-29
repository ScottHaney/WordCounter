using System;
using System.Collections.Generic;
using System.Linq;

namespace WordCounter
{
    public class WordCounter
    {
        private readonly ICharacterIdentifier _characterIdentifier;

        public WordCounter(ICharacterIdentifier characterIdentifier)
        {
            _characterIdentifier = characterIdentifier;
        }

        public Dictionary<string, int> Count(string text)
        {
            text = text ?? string.Empty;

            int? startIndex = null;
            var results = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);

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

    public interface ICharacterIdentifier
    {
        bool IsWordCharacterThatCanStartAWord(char c);
        bool IsWordCharacterThatCantStartAWord(char c);
    }

    public class CharacterIdentifier : ICharacterIdentifier
    {
        public bool IsWordCharacterThatCanStartAWord(char c)
        {
            return char.IsLetter(c);
        }

        public bool IsWordCharacterThatCantStartAWord(char c)
        {
            return c == '\'' || c == '-';
        }
    }
}
