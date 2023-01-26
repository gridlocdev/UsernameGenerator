using System.Reflection;
using System.Text.Json;
using UsernameGenerator.Core;

// The word list is stored in the compiled directory
var assemblyPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

var words = JsonSerializer.Deserialize<Word[]>(
    File.ReadAllText($"{assemblyPath}/Data/words-and-syllables.json")
);

if (words is null)
    throw new Exception("Word list either empty or not found at file location.");

var usernameService = new UsernameGeneratorService(words);

Console.WriteLine(
    "Directions: Press \"Enter\" to advance to the next item, or any other key to stop.\n"
);
var shouldContinue = true;
do
{
    var usernameLength = new Random().Next(2, 12);
    var firstWordSyllableCount = usernameLength >= 6 ? new Random().Next(1, 2) : 1;
    var secondWordSyllableCount = usernameLength >= 6 ? new Random().Next(1, 2) : 1;
    
    var username = usernameService.GetNewCombination(
        usernameLength,
        firstWordSyllableCount,
        secondWordSyllableCount
    );
    Console.WriteLine(username);
    var key = Console.ReadKey(true).Key;
    if (key != ConsoleKey.Enter)
        shouldContinue = false;
} while (shouldContinue);