using UsernameGenerator.Core;

var service = new UsernameGeneratorService(
    firstWordSyllableMinCount: 1,
    firstWordSyllableMaxCount: 1,
    secondWordSyllableMinCount: 1,
    secondWordSyllableMaxCount: 1,
    maxUsernameLength: 9
    );

Console.WriteLine(
    "Directions: Press \"Enter\" to advance to the next item, or any other key to stop.\n"
);
var shouldContinue = true;
do
{
    string username = UsernameGeneratorService.GetNewCombination();
    Console.WriteLine(username);
    var key = Console.ReadKey(true).Key;
    if (key != ConsoleKey.Enter)
        shouldContinue = false;
} while (shouldContinue == true);
