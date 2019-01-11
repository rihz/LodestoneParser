using System;
using System.Collections.Generic;
using System.Text;

namespace LodestoneParser.Exceptions
{
    public class CharacterNotLoadedException : Exception
    {
        public CharacterNotLoadedException()
            : base("No character is loaded. Please use the LoadCharacter function before using this method.")
        { }

        public CharacterNotLoadedException(string message)
            : base(message)
        { }

        public CharacterNotLoadedException(string message, Exception inner)
            : base(message, inner)
        { }
    }
}
