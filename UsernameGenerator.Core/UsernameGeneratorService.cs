namespace UsernameGenerator.Core;

public class UsernameGeneratorService
{
    private static string[] firstWordList;
    private static string[] secondWordList;

    static void PrintRandomCombination(
        string[] firstWordList,
        string[] secondWordList,
        int maxUsernameLength
    )
    {
        GenerateNewUsername:
        string username = String.Format(
            "{0}{1}",
            firstWordList[new Random().Next(0, firstWordList.Count() - 1)],
            secondWordList[new Random().Next(0, secondWordList.Count() - 1)]
        );
        if (username.Length > maxUsernameLength)
            goto GenerateNewUsername;
        Console.WriteLine(username);
    }

    public static string GetNewCombination(int maxUsernameLength)
    {
        return String.Format(
            "{0}{1}",
            firstWordList[new Random().Next(0, firstWordList.Count() - 1)],
            secondWordList[new Random().Next(0, secondWordList.Count() - 1)]
        );    
    }
    
    static string[] LimitWordListBySyllableCount(
        string[] commaDelimitedWordAndSyllableArray,
        int minSyllables,
        int maxSyllables
    )
    {
        return commaDelimitedWordAndSyllableArray
            .Where(
                x =>
                    int.Parse(x.Split(",")[1]) >= minSyllables
                    && int.Parse(x.Split(",")[1]) <= maxSyllables
            )
            .Select(x => x.Split(",")[0].ToString())
            .ToArray<string>();
    }

}