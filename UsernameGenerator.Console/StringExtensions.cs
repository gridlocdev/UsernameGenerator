using System.Globalization;

public static class StringExtensions
{
    public static string ToTitleCase(this string word) =>
        new CultureInfo("en-us", false).TextInfo.ToTitleCase(word);

    public static int GetSyllableCount(this string word)
    {
        word = word.ToLower().Trim();
        bool lastWasVowel = false;
        var vowels = new[] { 'a', 'e', 'i', 'o', 'u', 'y' };
        int count = 0;

        foreach (var c in word)
        {
            if (vowels.Contains(c))
            {
                if (!lastWasVowel)
                    count++;
                lastWasVowel = true;
            }
            else
                lastWasVowel = false;
        }

        if (
            (word.EndsWith("e") || word.EndsWith("es") || word.EndsWith("ed"))
            && !word.EndsWith("le")
        )
            count--;

        // Words cannot have zero or fewer syllables, so in the event the algorithm removes all syllables it should default to 1
        if (count <= 0)
            count = 1;

        return count;
    }
}
