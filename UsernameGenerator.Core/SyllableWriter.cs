using System.Text;
using System.Text.Json;

namespace UsernameGenerator.Core;

public class SyllableWriter
{
    public static void WriteToJson()
    {
        Console.WriteLine("Writing words.txt file to words-and-syllables.json...");
        Word[] words = File.ReadAllLines("./Data/words.txt").Select(x => new Word{ Name = x, SyllableCount = x.GetSyllableCount()}).ToArray();

        var jsonString = JsonSerializer.Serialize(words);
        
        File.WriteAllText("./Data/words-and-syllables.json", jsonString);

        Console.WriteLine("Finished writing syllable json file successfully!");
    }
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