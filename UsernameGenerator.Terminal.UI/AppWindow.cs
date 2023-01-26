using System.Reflection;
using System.Text.Json;
using Terminal.Gui;
using UsernameGenerator.Core;

public class AppWindow : Window
{
    private UsernameGeneratorService _service;
    private byte _usernameLength = 9;
    private byte _firstWordSyllableCount = 1;
    private byte _secondWordSyllableCount = 1;

    public AppWindow()
    {
        _service = InitializeService();
        Title = "Username Generator (Ctrl + Q to quit)";
        ColorScheme = Colors.TopLevel;

        var usernameDisplayLabel = new Label("")
        {
            X = Pos.Center(),
            Y = Pos.Center(),
            TextAlignment = TextAlignment.Centered,
            AutoSize = true,
        };
        var newUsernameButton = new Button("Generate New Username", true)
        {
            X = Pos.Center(),
            Y = Pos.Center() + 1,
        };
        newUsernameButton.Clicked += () =>
        {
            usernameDisplayLabel.Text = _service.GetNewCombination(_usernameLength, _firstWordSyllableCount,
                _secondWordSyllableCount);
        };

        var lengthLabel = new Label("Username Length: ")
        {
            X = 1,
            Y = 1,
        };
        var lengthComboBox = new ComboBox(new List<byte> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17 })
        {
            SelectedItem = 8,
            X = 1,
            Y = 2,
            Width = Dim.Percent(25),
        };
        lengthComboBox.SelectedItemChanged += args => { _usernameLength = (byte)args.Value; };
        
        var firstSyllableCountLabel = new Label("First Word Syllable Count: ")
        {
            X = 1,
            Y = 3,
        };
        var firstSyllableCountComboBox = new ComboBox(new List<byte> { 1, 2 })
        {
            SelectedItem = 1,
            X = 1,
            Y = 4,
            Width = Dim.Percent(25),
        };
        firstSyllableCountComboBox.SelectedItemChanged += args => { _firstWordSyllableCount = (byte)args.Value; };

        var secondSyllableCountLabel = new Label("Second Word Syllable Count: ")
        {
            X = 1,
            Y = 5,
        };
        var secondSyllableCountComboBox = new ComboBox(new List<byte> { 1, 2 })
        {
            SelectedItem = 1,
            X = 1,
            Y = 6,
            Width = Dim.Percent(25),
        };
        secondSyllableCountComboBox.SelectedItemChanged += args => { _secondWordSyllableCount = (byte)args.Value; };

        Add(
            usernameDisplayLabel,
            newUsernameButton,
            lengthLabel,
            lengthComboBox,
            firstSyllableCountLabel,
            firstSyllableCountComboBox,
            secondSyllableCountLabel,
            secondSyllableCountComboBox
        );
    }

    private static UsernameGeneratorService InitializeService()
    {
        // The word list is stored in the compiled directory
        var assemblyPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

        var words = JsonSerializer.Deserialize<Word[]>(
            File.ReadAllText($"{assemblyPath}/Data/words-and-syllables.json")
        );

        return new UsernameGeneratorService(words);
    }
}