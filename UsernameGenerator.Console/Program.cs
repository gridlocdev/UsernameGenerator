// To re-generate the word-syllable output from the .txt file, uncomment and run the following line:

await SyllableWriter.WriteToCSV();

var wordFileLines = await File.ReadAllLinesAsync("./data/words-and-syllables.csv");
byte maxUsernameLength = 9;

await Task.WhenAll();

string[] firstWordList = LimitWordListBySyllableCount(
    wordFileLines,
    minSyllables: 1,
    maxSyllables: 1
);
string[] secondWordList = LimitWordListBySyllableCount(
    wordFileLines,
    minSyllables: 1,
    maxSyllables: 1
);

Console.WriteLine(
    "Directions: Press \"Enter\" to advance to the next item, or any other key to stop.\n"
);
bool continuePrompt = true;
do
{
    PrintRandomCombination(firstWordList, secondWordList, maxUsernameLength);
    var key = Console.ReadKey(true).Key;
    if (key != ConsoleKey.Enter)
        continuePrompt = false;
} while (continuePrompt == true);

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
