using System.Reflection;
using System.Text.Json;
using Terminal.Gui;
using UsernameGenerator.Core;

public class AppWindow : Window
{
    private UsernameGeneratorService _service;
    private int _usernameLength = 9;
    private int _firstWordSyllableCount = 1;
    private int _secondWordSyllableCount = 1;

    public AppWindow()
    {
        _service = new UsernameGeneratorService(null);
        Title = "Username Generator (Ctrl + Q to quit)";
        ColorScheme = Colors.TopLevel;
        Width = Dim.Fill();
        Height = Dim.Fill();

        var lengthLabel = new Label("Username Length: ")
        {
            X = 1,
            Y = 1,
        };
        var lengthComboBox = new ComboBox(new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17 })
        {
            SelectedItem = 8,
            X = 1,
            Y = 2,
            Width = Dim.Percent(50),
        };
        lengthComboBox.SelectedItemChanged += args => { _usernameLength = args.Item + 1; };

        var firstSyllableCountLabel = new Label("First Word Syllable Count: ")
        {
            X = 1,
            Y = 4,
        };
        var firstSyllableCountComboBox = new ComboBox(new List<int> { 1, 2 })
        {
            SelectedItem = 0,
            X = 1,
            Y = 5,
            Width = Dim.Percent(50),
        };
        firstSyllableCountComboBox.SelectedItemChanged += args => { _firstWordSyllableCount = args.Item + 1; };

        var secondSyllableCountLabel = new Label("Second Word Syllable Count: ")
        {
            X = 1,
            Y = 7,
        };
        var secondSyllableCountComboBox = new ComboBox(new List<int> { 1, 2 })
        {
            SelectedItem = 0,
            X = 1,
            Y = 8,
            Width = Dim.Percent(50),
        };
        secondSyllableCountComboBox.SelectedItemChanged += args => { _secondWordSyllableCount = args.Item + 1; };

        var usernameDisplayLabel = new Label("          ")
        {
            X = Pos.Center(),
            Y = Pos.Center(),
            TextAlignment = TextAlignment.Centered,
        };

        var newUsernameButton = new Button("Generate New Username", true)
        {
            X = Pos.Center(),
            Y = 11,
        };
        newUsernameButton.Clicked += () =>
        {
            usernameDisplayLabel.Text = _service.GetNewCombination(_usernameLength, _firstWordSyllableCount,
                _secondWordSyllableCount);
        };

        var inputFrame = new FrameView("Filters")
        {
            X = 1,
            Y = 1,
            Width = Dim.Percent(40),
            Height = Dim.Fill()
        };
        inputFrame.Add(
            lengthLabel,
            lengthComboBox,
            firstSyllableCountLabel,
            firstSyllableCountComboBox,
            secondSyllableCountLabel,
            secondSyllableCountComboBox,
            newUsernameButton
        );

        var resultsFrame = new FrameView("Result")
        {
            X = Pos.Right(inputFrame) + 1,
            Y = 1,
            Width = Dim.Fill() - 1,
            Height = 7,
        };
        resultsFrame.Add(
            usernameDisplayLabel
        );

        Add(inputFrame, resultsFrame);
    }
}