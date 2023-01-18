using System.Diagnostics;

namespace UsernameGenerator.Core;

public class UsernameGeneratorService
{
    private readonly Word[] _words;
    public byte ShortestWordLength { get; }
    public byte LongestWordLength { get; }

    public UsernameGeneratorService(
        Word[] words
    )
    {
        _words = words;

        // Mostly all words in the English language are under 255 letters, 
        ShortestWordLength = (byte)_words.MinBy(w => w.Name.Length)!.Name.Length;
        LongestWordLength = (byte)_words.MaxBy(w => w.Name.Length)!.Name.Length;
    }

    public string GetNewCombination(byte usernameLength = 9, byte firstWordSyllableCount = 1,
        byte secondWordSyllableCount = 1)
    {
        if (usernameLength < 2)
            throw new Exception("The \"UsernameLength\" property is too small, please enter a value 2 or higher.");

        if (usernameLength > LongestWordLength * 2)
            throw new Exception(
                "The \"UsernameLength\" property is too large for this data set. please enter a smaller word length.");

        // To increase performance, filter the list of words down to ones that actually fit within the supplied username length
        var filteredWords = _words.Where(
            w => w.Name.Length < usernameLength
                 // Filter out words that have more syllables than both of the two possible syllable counts
                 && w.SyllableCount <= ((firstWordSyllableCount > secondWordSyllableCount)
                     ? firstWordSyllableCount
                     : secondWordSyllableCount)
        ).ToArray();

        var stopwatch = Stopwatch.StartNew();
        string result;
        Word firstWord;
        Word secondWord;
        do
        {
            firstWord = filteredWords[new Random().Next(0, filteredWords.Length - 1)];
            secondWord = filteredWords[new Random().Next(0, filteredWords.Length - 1)];
            result =
                $"{firstWord.Name}{secondWord.Name}";

            // Break out after a few seconds if a word is not found to avoid infinite looping
            if (stopwatch.Elapsed <= TimeSpan.FromSeconds(3)) continue;
            stopwatch.Stop();
            throw new TimeoutException(
                "Search time took longer than 3 seconds, please consider adjusting your search criteria.");
        } while (
            (result.Length == usernameLength &&
             firstWord.SyllableCount == firstWordSyllableCount &&
             secondWord.SyllableCount == secondWordSyllableCount)
            == false
        );

        stopwatch.Stop();
        return result;
    }
}