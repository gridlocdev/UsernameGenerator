using System.Diagnostics;
using System.Reflection;
using System.Text.Json;

namespace UsernameGenerator.Core;

public class UsernameGeneratorService
{
    private Word[] _words;
    private int ShortestWordLength { get; }
    private int LongestWordLength { get; }
    const int searchLockoutTimeSeconds = 3;

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

    public string GetNewCombination(int usernameLength = 9, int firstWordSyllableCount = 1,
        int secondWordSyllableCount = 1)
    {
        if (usernameLength < ShortestWordLength * 2)
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
}