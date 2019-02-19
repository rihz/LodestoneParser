using System;
using System.Collections.Generic;
using System.Text;

namespace LodestoneParser.Exceptions
{
    /// <summary>
    /// Exception thrown when a character hasn't been loaded yet.
    /// </summary>
    public class CharacterNotLoadedException : Exception
    {
        /// <summary>
        /// Create a new instance of the Exception.
        /// </summary>
        public CharacterNotLoadedException()
            : base("No character is loaded. Please use the LoadCharacter function before using this method.")
        { }

        /// <summary>
        /// Create a new instance of the Exception.
        /// </summary>
        /// <param name="message">The message for the Exception.</param>
        public CharacterNotLoadedException(string message)
            : base(message)
        { }

        /// <summary>
        /// Create a new instance of the Exception.
        /// </summary>
        /// <param name="message">The message for the Exception.</param>
        /// <param name="inner">The inner Exception.</param>
        public CharacterNotLoadedException(string message, Exception inner)
            : base(message, inner)
        { }
    }
}
