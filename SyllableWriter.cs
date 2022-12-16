using System.Text;

public class SyllableWriter
{
    public static async Task Run()
    {
        string[] adjectives = await File.ReadAllLinesAsync("./data/english-adjectives.txt");
        string[] nouns = await File.ReadAllLinesAsync("./data/english-nouns.txt");

        await Task.WhenAll();

        // ~~~ Write all syllable counts to CSV ~~~
        var csv = new StringBuilder();

        for (int i = 0; i < adjectives.Length; i++)
        {
            csv.AppendLine(
                String.Format(
                    "{0},{1}",
                    adjectives[i].ToTitleCase(),
                    adjectives[i].GetSyllableCount()
                )
            );
        }
        File.WriteAllText("./data/adjectives-and-syllable-count.csv", csv.ToString());

        csv.Clear();

        for (int i = 0; i < nouns.Length; i++)
        {
            csv.AppendLine(
                String.Format("{0},{1}", nouns[i].ToTitleCase(), nouns[i].GetSyllableCount())
            );
        }
        File.WriteAllText("./data/nouns-and-syllable-count.csv", csv.ToString());
    }
}
