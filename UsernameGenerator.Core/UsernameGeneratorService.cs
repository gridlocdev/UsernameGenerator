using System.Diagnostics;
using System.Reflection;
using System.Text.Json;

namespace UsernameGenerator.Core;

public class UsernameGeneratorService
{
    private Word[] _words;
    private int ShortestWordLength { get; }
    private int LongestWordLength { get; }
    private const int searchLockoutTimeSeconds = 3;

    public UsernameGeneratorService(
        Word[]? words
    )
    {
        _words = words ?? GetWordListFromBinaryPath();

        // Mostly all words in the English language are under 255 letters, 
        ShortestWordLength = _words.MinBy(w => w.Name.Length)!.Name.Length;
        LongestWordLength = _words.MaxBy(w => w.Name.Length)!.Name.Length;
    }

    private static Word[] GetWordListFromBinaryPath()
    {
        // When the project is built, the JSON data file is loaded into the compiled directory as content
        var assemblyPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        var words = JsonSerializer.Deserialize<Word[]>(
            File.ReadAllText($"{assemblyPath}/Data/words-and-syllables.json")
        );
        if (words is null)
            throw new Exception("Word list either empty or not found at file location.");

        return words;
    }

    /// <summary>
    /// A method that randomly generates a username from the specified parameters. Parameters supplied with `null` are randomized.
    /// </summary>
    /// <param name="usernameLength">The number of total characters in the username</param>
    /// <param name="firstWordSyllableCount">The number of syllables in the first word</param>
    /// <param name="secondWordSyllableCount">The number of syllables in the second word</param>
    /// <returns>A username in the format "WordWord"</returns>
    /// <exception cref="UsernameNotPossibleException"></exception>
    /// <exception cref="TimeoutException"></exception>
    public string GetNewCombination(
        int? usernameLength,
        int? firstWordSyllableCount,
        int? secondWordSyllableCount
    )
    {
        firstWordSyllableCount ??= new Random().Next(1, 2);
        secondWordSyllableCount ??= new Random().Next(1, 2);
        // There are no words (that I know of) with 3 letters or less that also contain more than 2 syllables,
        // this avoids an infinite recursion based on the existing data
        if ((firstWordSyllableCount > 1 || secondWordSyllableCount > 1) && usernameLength is null)
            usernameLength = new Random().Next(4, 12);
        else
            usernameLength ??= new Random().Next(2, 12);

        if (usernameLength <= 3 && (firstWordSyllableCount > 1 || secondWordSyllableCount > 1))
            throw new UsernameNotPossibleException(
                "Too many syllables for username length, please either increase the username length or decrease the syllable count");

        if (usernameLength < ShortestWordLength * 2)
            throw new UsernameNotPossibleException("The \"UsernameLength\" property is too small, please enter a value 2 or higher.");

        if (usernameLength > LongestWordLength * 2)
            throw new UsernameNotPossibleException(
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
            if (stopwatch.Elapsed <= TimeSpan.FromSeconds(searchLockoutTimeSeconds)) continue;
            stopwatch.Stop();
            throw new TimeoutException(
                $"Search time took longer than {searchLockoutTimeSeconds} seconds, please consider adjusting your search criteria.");
        } while (
            (result.Length == usernameLength &&
             firstWord.SyllableCount == firstWordSyllableCount &&
             secondWord.SyllableCount == secondWordSyllableCount)
            == false
        );

        stopwatch.Stop();
        return result;
    }


    /// <summary>
    /// This exception occurs when criteria has been met that prevents the username from being generated due to a lack of possible results
    /// </summary>
    private class UsernameNotPossibleException : Exception
    {
        public UsernameNotPossibleException()
        {
        }

        public UsernameNotPossibleException(string message)
            : base(message)
        {
        }

        public UsernameNotPossibleException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}