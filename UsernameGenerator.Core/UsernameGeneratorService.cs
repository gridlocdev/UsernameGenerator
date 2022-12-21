namespace UsernameGenerator.Core;

public class UsernameGeneratorService
{
    private readonly byte _firstWordSyllableMinCount;
    private readonly byte _firstWordSyllableMaxCount;
    private readonly byte _secondWordSyllableMinCount;
    private readonly byte _secondWordSyllableMaxCount;
    private static byte _maxUsernameLength;
    private static string[] _firstWordList;
    private static string[] _secondWordList;
    private static string[] _wordList;
    public UsernameGeneratorService(
        byte firstWordSyllableMinCount,
        byte firstWordSyllableMaxCount,
        byte secondWordSyllableMinCount,
        byte secondWordSyllableMaxCount,
        byte maxUsernameLength
    )
    {
        _firstWordSyllableMinCount = firstWordSyllableMinCount;
        _firstWordSyllableMaxCount = firstWordSyllableMaxCount;
        _secondWordSyllableMinCount = secondWordSyllableMinCount;
        _secondWordSyllableMaxCount = secondWordSyllableMaxCount;
        _maxUsernameLength = maxUsernameLength;
        _wordList = File.ReadAllLines("./Data/words-and-syllables.csv");

        _firstWordList = LimitWordListBySyllableCount(_wordList, _firstWordSyllableMinCount, _firstWordSyllableMaxCount);
        _secondWordList = LimitWordListBySyllableCount(_wordList, _secondWordSyllableMinCount, _secondWordSyllableMaxCount);
    }
    
    public string GetNewCombination()
    {
        var result = "";
        do
        {
            var firstWordIndex = new Random().Next(0, _firstWordList.Length - 1);
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
            .ToArray<string>();
    }

}