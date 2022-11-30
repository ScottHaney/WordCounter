using System;
using System.Collections.Generic;
using System.Text;
using WordCounting.CharacterIdentification;

namespace WordCounting
{
    public class WordParser
    {
        private readonly ICharacterIdentifier _characterIdentifier;

        public WordParser(ICharacterIdentifier characterIdentifier)
        {
            _characterIdentifier = characterIdentifier;
        }

        public IEnumerable<string> Parse(string text)
        {
            text = text ?? string.Empty;

            int? startIndex = null;
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
                    yield return GetCurrentWord(text, startIndex.Value, i - 1);
                    startIndex = null;
                }
            }

            //Make sure to the last word at the end of the text in case there is no ending punctuation
            if (startIndex != null)
                yield return GetCurrentWord(text, startIndex.Value, text.Length - 1);
        }

        private string GetCurrentWord(string entireText, int startIndex, int endIndex)
            => entireText.Substring(startIndex, endIndex - startIndex + 1);
    }
}
