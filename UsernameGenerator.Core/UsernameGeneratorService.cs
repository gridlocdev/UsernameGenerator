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
            throw new Exception("The \"UsernameLength\" property is too large for this data set. please enter a smaller word length.");
        
        // To increase performance, filter the list of words down to ones that actually fit within the supplied username length
        var filteredWords = _words.Where(
            w => w.Name.Length < usernameLength 
                // Filter out words that have more syllables than both of the two possible syllable counts
                && w.SyllableCount <= ((firstWordSyllableCount > secondWordSyllableCount) ? firstWordSyllableCount : secondWordSyllableCount)
        ).ToArray();

        string result;
        Word firstWord;
        Word secondWord;
        var attempts = 0;
        do
        {
            firstWord = filteredWords[new Random().Next(0, filteredWords.Length - 1)];
            secondWord = filteredWords[new Random().Next(0, filteredWords.Length - 1)];
            result =
                $"{firstWord.Name}{secondWord.Name}";

            // Prevent an infinite loop by breaking out after a very large (and likely unreasonable to reach) number of retry attempts on the filter criteria
            attempts++;
            if (attempts >= 100000000)
                break;
        } while (
            (result.Length == usernameLength &&
             firstWord.SyllableCount == firstWordSyllableCount &&
             secondWord.SyllableCount == secondWordSyllableCount)
            == false
        );

        return result;
    }
}