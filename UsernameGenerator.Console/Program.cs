using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using UsernameGenerator.Core;
using Spectre.Console;
using Spectre.Console.Cli;

var app = new CommandApp<GenerateUsernamesCommand>();
return app.Run(args);

internal sealed class GenerateUsernamesCommand : Command<GenerateUsernamesCommand.Settings>
{
    public sealed class Settings : CommandSettings
    {
        [Description("The number of total characters in the username")]
        [CommandOption("--length")]
        [DefaultValue(null)]
        public int? UsernameLength { get; init; }

        [Description("The number of syllables in the first word")]
        [CommandOption("--word1syllables")]
        [DefaultValue(null)]
        public int? FirstWordSyllableCount { get; init; }

        [Description("The number of syllables in the second word")]
        [CommandOption("--word2syllables")]
        [DefaultValue(null)]
        public int? SecondWordSyllableCount { get; init; }
    }

    public override int Execute([NotNull] CommandContext context, [NotNull] Settings settings)
    {
        var usernameService = new UsernameGeneratorService(null);

        Console.WriteLine(
            "Directions: Press \"Enter\" to advance to the next item, or any other key to stop.\n"
        );
        var shouldContinue = true;
        do
        {
            var username = usernameService.GetNewCombination(
                settings.UsernameLength,
                settings.FirstWordSyllableCount,
                settings.SecondWordSyllableCount
            );
            Console.WriteLine(username);
            var key = Console.ReadKey(true).Key;
            if (key != ConsoleKey.Enter)
                shouldContinue = false;
        } while (shouldContinue);

        return 0;
    }
}