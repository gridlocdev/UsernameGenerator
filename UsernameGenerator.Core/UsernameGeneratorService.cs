using System.Diagnostics;
using System.Text.Json;

namespace UsernameGenerator.Core;

public class UsernameGeneratorService
{
    private byte FirstWordSyllableMinCount { get; set; } = 1;
    private byte FirstWordSyllableMaxCount { get; set; } = 1;
    private byte SecondWordSyllableMinCount { get; set; } = 1;
    private byte SecondWordSyllableMaxCount { get; set;  } = 1;
    public byte MaxUsernameLength { get; set; } = 5;
    private string[]? _firstWordList;
    private string[]? _secondWordList;

    public UsernameGeneratorService(
       Word[]? words
    )
    {
        _firstWordList = words.Where(x =>
            x.SyllableCount >= FirstWordSyllableMinCount && x.SyllableCount <= FirstWordSyllableMaxCount).Select(x => x.Name).ToArray();
        _secondWordList = words.Where(x =>
            x.SyllableCount >= SecondWordSyllableMinCount && x.SyllableCount <= SecondWordSyllableMaxCount).Select(x => x.Name).ToArray();
    }
    
    public async Task<string> GetNewCombinationAsync()
    {
        if (MaxUsernameLength < 2)
            throw new Exception("The \"MaxUsernameLength\" property is too small, please enter a value 2 or higher.");
        string result;
        byte attempts = 0;
        do
        {
            Debug.Assert(_firstWordList != null, nameof(_firstWordList) + " != null");
            var firstWordIndex = new Random().Next(0, _firstWordList.Length - 1);
            Debug.Assert(_secondWordList != null, nameof(_secondWordList) + " != null");
            var secondWordIndex = new Random().Next(0, _secondWordList.Length - 1);
            result =
                $"{_firstWordList[firstWordIndex]}{_secondWordList[secondWordIndex]}";
            
            attempts++;
            if (attempts >= 50000)
                break;
        } while (result.Length > MaxUsernameLength);

        return result;
    }
}