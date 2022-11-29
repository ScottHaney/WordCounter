using System;
using System.Collections.Generic;
using System.Text;

namespace WordCounter.CharacterIdentification
{
    public interface ICharacterIdentifier
    {
        bool IsWordCharacterThatCanStartAWord(char c);
        bool IsWordCharacterThatCantStartAWord(char c);
    }
}
