using System.Diagnostics;

namespace UsernameGenerator.Core;

public class UsernameGeneratorService
{
    private static byte _maxUsernameLength;
    private static string[]? _firstWordList;
    private static string[]? _secondWordList;

    public UsernameGeneratorService(
        byte firstWordSyllableMinCount,
        byte firstWordSyllableMaxCount,
        byte secondWordSyllableMinCount,
        byte secondWordSyllableMaxCount,
        byte maxUsernameLength
    )
    {
        _maxUsernameLength = maxUsernameLength;
        var wordList = File.ReadAllLines("./Data/words-and-syllables.csv");

        _firstWordList = LimitWordListBySyllableCount(wordList, firstWordSyllableMinCount, firstWordSyllableMaxCount);
        _secondWordList = LimitWordListBySyllableCount(wordList, secondWordSyllableMinCount, secondWordSyllableMaxCount);
    }
    
    public string GetNewCombination()
    {
        string result;
        do
        {
            Debug.Assert(_firstWordList != null, nameof(_firstWordList) + " != null");
            var firstWordIndex = new Random().Next(0, _firstWordList.Length - 1);
            Debug.Assert(_secondWordList != null, nameof(_secondWordList) + " != null");
            var secondWordIndex = new Random().Next(0, _secondWordList.Length - 1);
            result =
                $"{_firstWordList[firstWordIndex]}{_secondWordList[secondWordIndex]}";
        } while (result.Length < _maxUsernameLength);

        return result;
    }

    private static string[] LimitWordListBySyllableCount(
        IEnumerable<string> commaDelimitedWordAndSyllableArray,
        byte minSyllableCount,
        byte maxSyllableCount
    )
    {
        return commaDelimitedWordAndSyllableArray
            .Where(
                x =>
                    int.Parse(x.Split(",")[1]) >= minSyllableCount
                    && int.Parse(x.Split(",")[1]) <= maxSyllableCount
            )
            .Select(x => x.Split(",")[0].ToString())
            .ToArray();
    }

}