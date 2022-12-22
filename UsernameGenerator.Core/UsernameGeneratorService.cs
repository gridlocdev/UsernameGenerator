using System.Diagnostics;
using System.Text.Json;

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
        var words = JsonSerializer.Deserialize<Word[]>(
            File.ReadAllText("./Data/words-and-syllables.json")
            );

        _firstWordList = words.Where(x =>
            x.SyllableCount >= firstWordSyllableMinCount && x.SyllableCount <= firstWordSyllableMaxCount).Select(x => x.Name).ToArray();
        _secondWordList = words.Where(x =>
            x.SyllableCount >= secondWordSyllableMinCount && x.SyllableCount <= secondWordSyllableMaxCount).Select(x => x.Name).ToArray();
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
}