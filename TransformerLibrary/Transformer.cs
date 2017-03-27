using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;

namespace TransformerLibrary
{
    /// <summary>
    /// A class to transform text by replacing any given word with a different given word. Words are defined as any sequence of alphanumeric characters. Only whole words are considered.
    /// </summary>
    public class Transformer
    {
        Dictionary<string, string> _translationDictionary = new Dictionary<string, string>();

        /// <summary>
        /// Add a <paramref name="key"/> and its associated <paramref name="value"/> to the Dictionary.
        /// </summary>
        /// <param name="key">The word to look for.</param>
        /// <param name="value">The word to repalce the <paramref name="key"/> with.</param>
        /// <exception cref="ArgumentOutOfRangeException">The <paramref name="key"/>> or the <paramref name="value"/> contains non-alphanumeric characters.</exception>
        public void Add(string key, string value)
        {
            if (!IsAlphanumeric(key))
                throw new ArgumentOutOfRangeException(nameof(key), key, "Key must be alphanumeric.");
            if (!IsAlphanumeric(value))
                throw new ArgumentOutOfRangeException(nameof(value), value, "Value must be alphanumeric.");

            _translationDictionary.Add(key, value);
        }

        /// <summary>
        /// Read from the <paramref name="reader"/> and write to the <paramref name="writer"/> subsituting the value words for the key words.
        /// </summary>
        /// <param name="reader">The stream to read from.</param>
        /// <param name="writer">The stream to write to.</param>
        /// <exception cref="ArgumentNullException">The <paramref name="reader"/> or <paramref name="writer"/> is null.</exception>
        public void Transform(TextReader reader, TextWriter writer)
        {
            if (reader == null)
                throw new ArgumentNullException(nameof(reader));
            if (writer == null)
                throw new ArgumentNullException(nameof(writer));

            int c;
            while ((c = reader.Peek()) != -1)
            {
                if (IsAlphanumeric(((char)c).ToString()))
                    ProcessWord(reader, writer);
                else
                    ProcessNonWord(reader, writer);
            }
        }

        /// <summary>
        /// Read and write non-word characters until the next word character is found.
        /// </summary>
        /// <param name="reader">The stream to read from.</param>
        /// <param name="writer">The stream to write to.</param>
        /// <exception cref="ArgumentNullException">The <paramref name="reader"/> or <paramref name="writer"/> is null.</exception>
        private void ProcessNonWord(TextReader reader, TextWriter writer)
        {
        /// <exception cref="ArgumentNullException">The <paramref name="reader"/> or <paramref name="writer"/> is null.</exception>
            if (reader == null)
                throw new ArgumentNullException(nameof(reader));
            if (writer == null)
                throw new ArgumentNullException(nameof(writer));
            Debug.Assert(_translationDictionary != null);

            int c;
            while ((c = reader.Peek()) != -1 && !IsAlphanumeric(((char)c).ToString()))
            {
                writer.Write((char)reader.Read());
            }
        }

        /// <summary>
        /// Read and write word characters until the next non-word character is found.
        /// </summary>
        /// <param name="reader">The stream to read from.</param>
        /// <param name="writer">The stream to write to.</param>
        /// <exception cref="ArgumentNullException">The <paramref name="reader"/> or <paramref name="writer"/> is null.</exception>
        private void ProcessWord(TextReader reader, TextWriter writer)
        {
            if (reader == null)
                throw new ArgumentNullException(nameof(reader));
            if (writer == null)
                throw new ArgumentNullException(nameof(writer));
            Debug.Assert(_translationDictionary != null);

            int c;
            string word = "";
            while ((c = reader.Peek()) != -1 && IsAlphanumeric(((char)c).ToString()))
            {
                word += ((char)reader.Read()).ToString();
            }

            writer.Write(_translationDictionary.ContainsKey(word) ? _translationDictionary[word] : word);
        }

        /// <summary>
        /// Determine if the given string is strictly alphanumeric.
        /// </summary>
        /// <param name="s">The string to check.</param>
        /// <returns>true if the string contains only alphanumeric characters; otherwise false.</returns>
        private static bool IsAlphanumeric(string s) => s != null && Regex.IsMatch(s, @"^[a-zA-Z0-9]+$");
    }
}
