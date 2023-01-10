using System.Reflection;
using System.Text.Json;
using UsernameGenerator.Core;


var usernameService = new UsernameGeneratorService(
    JsonSerializer.Deserialize<Word[]>(
        File.ReadAllText("./Data/words-and-syllables.json")
    )
);

Console.WriteLine(
    "Directions: Press \"Enter\" to advance to the next item, or any other key to stop.\n"
);
var shouldContinue = true;
do
{
    var username = await usernameService.GetNewCombinationAsync();
    Console.WriteLine(username);
    var key = Console.ReadKey(true).Key;
    if (key != ConsoleKey.Enter)
        shouldContinue = false;
} while (shouldContinue);