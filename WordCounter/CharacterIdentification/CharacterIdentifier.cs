using System;
using System.Collections.Generic;
using System.Text;

namespace WordCounter.CharacterIdentification
{
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
