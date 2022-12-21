using System.Text;

namespace UsernameGenerator.Core;

public class SyllableWriter
{
    public static async Task WriteToCsv()
    {
        Console.WriteLine("Writing words.txt file to words-and-syllables.txt...");
        string[] words = await File.ReadAllLinesAsync("./Data/words.txt");

        await Task.WhenAll();

        // ~~~ Write all syllable counts to CSV ~~~
        var csv = new StringBuilder();

        for (var i = 0; i < words.Length; i++)
        {
            csv.AppendLine(
                $"{words[i].ToTitleCase()},{words[i].GetSyllableCount()}"
            );
        }
        await File.WriteAllTextAsync("./Data/words-and-syllables.csv", csv.ToString());

        Console.WriteLine("Finished writing syllable csv file successfully!");
    }
}