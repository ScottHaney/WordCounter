using System;
using System.Collections.Generic;
using System.Text;

namespace WordCounting.CharacterIdentification
{
    /// <summary>
    /// Allows for customization of which characters are considered to be part of a word.
    /// </summary>
    public interface ICharacterIdentifier
    {
        bool IsWordCharacterThatCanStartAWord(char c);
        bool IsWordCharacterThatCantStartAWord(char c);
    }
}
