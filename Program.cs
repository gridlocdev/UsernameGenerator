// await SyllableWriter.Run();

var adjectiveFileLines = await File.ReadAllLinesAsync("./data/adjectives-and-syllable-count.csv");
var nounFileLines = await File.ReadAllLinesAsync("./data/adjectives-and-syllable-count.csv");

await Task.WhenAll();

string[] adjectives = LimitWordListBySyllableCount(
    adjectiveFileLines,
    minSyllables: 1,
    maxSyllables: 1
);
string[] nouns = LimitWordListBySyllableCount(nounFileLines, minSyllables: 1, maxSyllables: 1);

// Start the CLI
Console.WriteLine(
    "Directions: Press \"Enter\" to advance to the next item, or any other key to stop.\n"
);
bool continuePrompt = true;

do
{
    PrintRandomCombination(adjectives, nouns);
    var key = Console.ReadKey().Key;
    if (key != ConsoleKey.Enter)
        continuePrompt = false;
} while (continuePrompt == true);

static void PrintRandomCombination(string[] adjectives, string[] nouns)
{
    string username = String.Format(
        "{0}{1}",
        adjectives[new Random().Next(0, adjectives.Count() - 1)],
        nouns[new Random().Next(0, nouns.Count() - 1)]
    );

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
