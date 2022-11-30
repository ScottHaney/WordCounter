using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WordCounting.CharacterIdentification
{
    /// <summary>
    /// Default implementation of <see cref="ICharacterIdentifier"/>. This is what you usually want to use.
    /// </summary>
    public class CharacterIdentifier : ICharacterIdentifier
    {
        public bool IsWordCharacterThatCanStartAWord(char c)
        {
            return char.IsLetter(c);
        }

        public bool IsWordCharacterThatCantStartAWord(char c)
        {
            return !char.IsWhiteSpace(c) && !_sentenceStructurePunctuationMarks.Contains(c);
        }

        private static readonly char[] _sentenceStructurePunctuationMarks = new char[]
        {
            '.', '?', '!', ',', ';', ':', '"'
        };
    }
}
