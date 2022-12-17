using System.Text;

public class SyllableWriter
{
    public static async Task WriteToCSV()
    {
        string[] adjectives = await File.ReadAllLinesAsync("./data/english-adjectives.txt");
        string[] words = await File.ReadAllLinesAsync("./data/words.txt");

        await Task.WhenAll();

        // ~~~ Write all syllable counts to CSV ~~~
        var csv = new StringBuilder();

        for (int i = 0; i < words.Length; i++)
        {
            csv.AppendLine(
                String.Format("{0},{1}", words[i].ToTitleCase(), words[i].GetSyllableCount())
            );
        }
        File.WriteAllText("./data/words-and-syllables.csv", csv.ToString());

        for (int i = 0; i < nouns.Length; i++)
        {
            csv.AppendLine(
                String.Format("{0},{1}", nouns[i].ToTitleCase(), nouns[i].GetSyllableCount())
            );
        }
        File.WriteAllText("./data/nouns-and-syllable-count.csv", csv.ToString());
    }
}
