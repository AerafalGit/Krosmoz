// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Servers.GameServer.Services.Characters.Creation.NameGeneration;

/// <summary>
/// Represents a service for generating character names.
/// Implements the <see cref="ICharacterNameGenerationService"/> interface.
/// </summary>
public sealed class CharacterNameGenerationService : ICharacterNameGenerationService
{
    /// <summary>
    /// The maximum length of a syllable.
    /// </summary>
    private const int SyllableMaxLength = 3;

    /// <summary>
    /// The probability (in percentage) that the first part of the name starts with a vowel.
    /// </summary>
    private const int DefaultFirstPartIsVowelProbability = 20;

    /// <summary>
    /// The probability (in percentage) of adding a syllable in the middle part of the name.
    /// </summary>
    private const int DefaultAddSyllableInMiddlePartProbability = 45;

    /// <summary>
    /// The probability (in percentage) of using a terminal letter at the end of the name.
    /// </summary>
    private const int DefaultUseTerminalLetterProbability = 30;

    /// <summary>
    /// The probability (in percentage) of generating composed names (e.g., names with a hyphen).
    /// </summary>
    private const int DefaultComposedNamesProbability = 8;

    private readonly Dictionary<char, char[]> _bigrams;
    private readonly char[] _consonants;
    private readonly char[] _vowels;

    /// <summary>
    /// Initializes a new instance of the <see cref="CharacterNameGenerationService"/> class.
    /// Sets up the bigrams, consonants, and vowels used for name generation.
    /// </summary>
    public CharacterNameGenerationService()
    {
        _bigrams = [];

        AddBigram('c', ['_', 'h', 'l', 'r'], [8, 3, 2, 2]);
        AddBigram('g', ['_', 'l', 'n', 'r'], [10, 2, 1, 2]);
        AddBigram('l', ['_', 'l'], [8, 1]);
        AddBigram('m', ['_', 'm'], [8, 1]);
        AddBigram('n', ['_', 'n'], [6, 1]);
        AddBigram('p', ['_', 'h', 'l', 'p', 'r'], [8, 2, 1, 3, 1]);
        AddBigram('q', ['_', 'u'], [0, 1]);
        AddBigram('s', ['_', 'h', 'k', 'l', 'n', 'p', 'q', 'r', 's', 't'], [15, 1, 1, 1, 2, 5, 1, 2, 10, 5]);
        AddBigram('t', ['_', 'h', 'r', 't'], [8, 3, 5, 1]);
        AddBigram('a', ['t'], [1]);
        AddBigram('e', ['t', 'd'], [2, 1]);
        AddBigram('i', [], []);
        AddBigram('o', ['t'], [1]);
        AddBigram('u', ['s', 't'], [2, 1]);
        AddBigram('y', [], []);

        _vowels = GenerateProbabilisticList(['a', 'e', 'i', 'o', 'u', 'y'], [9, 15, 8, 6, 6, 1]);
        _consonants = GenerateProbabilisticList(['b', 'c', 'd', 'f', 'g', 'h', 'j', 'k', 'l', 'm', 'n', 'p', 'q', 'r', 's', 't', 'v', 'w', 'x', 'z'], [2, 2, 3, 2, 2, 2, 1, 1, 5, 3, 6, 2, 1, 6, 6, 6, 2, 1, 1, 1]);
    }

    /// <summary>
    /// Generates a new character name.
    /// </summary>
    /// <returns>A randomly generated character name as a string.</returns>
    public string GenerateName()
    {
        Span<char> buffer = stackalloc char[16];

        var length = GenerateName(buffer[..]);

        if (!CheckRandom(DefaultComposedNamesProbability))
            return new string(buffer[..length]);

        if (length < buffer.Length)
            buffer[length++] = '-';

        if (length < buffer.Length)
            length += GenerateName(buffer[length..]);

        return new string(buffer[..length]);
    }

    /// <summary>
    /// Generates a name and writes it into the provided buffer.
    /// </summary>
    /// <param name="buffer">The buffer to write the generated name into.</param>
    /// <returns>The length of the generated name.</returns>
    private int GenerateName(Span<char> buffer)
    {
        var length = 0;

        length += GenerateFirstPart(buffer[length..]);
        length += GenerateMiddlePart(buffer[length..]);
        length += GenerateLastPart(buffer[length..]);

        return length;
    }

    /// <summary>
    /// Generates the first part of a name.
    /// </summary>
    /// <param name="buffer">The buffer to write the first part into.</param>
    /// <returns>The length of the first part.</returns>
    private int GenerateFirstPart(Span<char> buffer)
    {
        if (CheckRandom(DefaultFirstPartIsVowelProbability))
        {
            buffer[0] = char.ToUpperInvariant(GetRandomVowel());
            return 1;
        }

        Span<char> syllableBuffer = stackalloc char[SyllableMaxLength];

        var syllableLength = GenerateSyllable(syllableBuffer);

        syllableBuffer[..syllableLength].CopyTo(buffer);

        var length = syllableLength;

        if (length > 1 && buffer[0] == buffer[1])
        {
            for (var i = 0; i < length - 1; i++)
                buffer[i] = buffer[i + 1];
            length--;
        }

        buffer[0] = char.ToUpperInvariant(buffer[0]);
        return length;
    }

    /// <summary>
    /// Generates the middle part of a name.
    /// </summary>
    /// <param name="buffer">The buffer to write the middle part into.</param>
    /// <returns>The length of the middle part.</returns>
    private int GenerateMiddlePart(Span<char> buffer)
    {
        if (buffer.Length is 0)
            return 0;

        var length = 0;

        Span<char> syllableBuffer = stackalloc char[SyllableMaxLength];

        while (length < buffer.Length && CheckRandom(DefaultAddSyllableInMiddlePartProbability) && length < 3)
        {
            var syllableLength = GenerateSyllable(syllableBuffer);

            if (length + syllableLength > buffer.Length)
                break;

            syllableBuffer[..syllableLength].CopyTo(buffer[length..]);

            length += syllableLength;

            syllableBuffer.Clear();
        }

        return length;
    }

    /// <summary>
    /// Generates the last part of a name.
    /// </summary>
    /// <param name="buffer">The buffer to write the last part into.</param>
    /// <returns>The length of the last part.</returns>
    private int GenerateLastPart(Span<char> buffer)
    {
        if (buffer.Length < 3)
            return 0;

        Span<char> syllableBuffer = stackalloc char[SyllableMaxLength];

        var syllableLength = GenerateSyllable(syllableBuffer);

        if (syllableLength > buffer.Length)
            syllableLength = buffer.Length;

        syllableBuffer[..syllableLength].CopyTo(buffer);

        var length = syllableLength;

        if (length <= 0 || Random.Shared.Next(100) >= DefaultUseTerminalLetterProbability)
            return length;

        var lastChar = buffer[length - 1];

        if (!_bigrams.TryGetValue(lastChar, out var bigram) || bigram.Length <= 0)
            return length;

        var terminalChar = bigram[Random.Shared.Next(bigram.Length)];

        if (length < buffer.Length) buffer[length++] = terminalChar;

        return length;
    }

    /// <summary>
    /// Generates a syllable and writes it into the provided buffer.
    /// </summary>
    /// <param name="buffer">The buffer to write the syllable into.</param>
    /// <returns>The length of the generated syllable.</returns>
    private int GenerateSyllable(Span<char> buffer)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(buffer.Length, 3, nameof(buffer.Length));

        var length = 0;

        var consonant = GetRandomConsonant();

        buffer[length++] = consonant;

        if (_bigrams.TryGetValue(consonant, out var bigram) && bigram.Length > 0)
            buffer[length++] = bigram[Random.Shared.Next(bigram.Length)];

        buffer[length++] = GetRandomVowel();

        for (var i = 0; i < length; i++)
        {
            if (buffer[i] is not '_')
                continue;

            for (var j = i; j < length - 1; j++)
                buffer[j] = buffer[j + 1];

            length--;
            i--;
        }

        return length;
    }

    /// <summary>
    /// Adds a bigram to the dictionary with its associated probabilities.
    /// </summary>
    /// <param name="c">The character to associate the bigram with.</param>
    /// <param name="possibilities">The possible following characters.</param>
    /// <param name="probabilities">The probabilities of each following character.</param>
    private void AddBigram(char c, char[] possibilities, short[] probabilities)
    {
        _bigrams[c] = GenerateProbabilisticList(possibilities, probabilities);
    }

    /// <summary>
    /// Gets a random consonant from the probabilistic list.
    /// </summary>
    /// <returns>A randomly selected consonant.</returns>
    private char GetRandomConsonant()
    {
        return _consonants[Random.Shared.Next(_consonants.Length)];
    }

    /// <summary>
    /// Gets a random vowel from the probabilistic list.
    /// </summary>
    /// <returns>A randomly selected vowel.</returns>
    private char GetRandomVowel()
    {
        return _vowels[Random.Shared.Next(_vowels.Length)];
    }

    /// <summary>
    /// Checks if a random event occurs based on the given probability.
    /// </summary>
    /// <param name="probability">The probability (in percentage) of the event occurring.</param>
    /// <returns><c>true</c> if the event occurs; otherwise, <c>false</c>.</returns>
    private static bool CheckRandom(short probability)
    {
        return Random.Shared.Next(100) < probability;
    }

    /// <summary>
    /// Generates a probabilistic list of characters based on the given possibilities and probabilities.
    /// </summary>
    /// <param name="possibilities">The possible characters.</param>
    /// <param name="probabilities">The probabilities of each character.</param>
    /// <returns>An array of characters distributed according to the probabilities.</returns>
    private static char[] GenerateProbabilisticList(char[] possibilities, short[] probabilities)
    {
        var nb = probabilities.Aggregate(0, static (current, prob) => current + prob);

        var indexes = new List<int>();

        for (var j = 0; j < nb; j++)
            indexes.Add(j);

        var bigram = new char[nb];

        for (var k = 0; k < possibilities.Length; ++k)
        for (var l = 0; l < probabilities[k]; ++l)
        {
            var m = Random.Shared.Next(indexes.Count);
            var n = indexes[m];
            bigram[n] = possibilities[k];
            indexes.RemoveAt(m);
        }

        return bigram;
    }
}
