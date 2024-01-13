using Terminal.Gui;
using UsernameGenerator.Core;

namespace UsernameGenerator.Terminal.UI;

public sealed class AppWindow : Window
{
    private int _usernameLength = 9;
    private int _firstWordSyllableCount = 1;
    private int _secondWordSyllableCount = 1;
    private bool _usernameLengthInputEnabled;
    private bool _firstWordSyllableCountInputEnabled;
    private bool _secondWordSyllableCountInputEnabled;

    public AppWindow()
    {
        var service = new UsernameGeneratorService(null);
        Title = "Username Generator (Ctrl + Q to quit)";
        ColorScheme = Colors.ColorSchemes["TopLevel"];
        Width = Dim.Fill();
        Height = Dim.Fill();

        // Set positions of controls
        Label usernameLengthLabel = new("Username Length: ")
        {
            X = 3,
            Y = 1,
        };
        CheckBox usernameLengthInputEnabledCheckbox = new()
        {
            X = usernameLengthLabel.X - 2,
            Y = usernameLengthLabel.Y,
            Checked = _usernameLengthInputEnabled,
        };
        ComboBox usernameLengthComboBox =
            new(new List<int> { 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17 })
            {
                X = usernameLengthLabel.X,
                Y = usernameLengthLabel.Y + 1,
                Width = Dim.Percent(50),
                SelectedItem = 8,
                Enabled = _usernameLengthInputEnabled,
            };

        Label firstWordSyllableCountLabel = new("First Word Syllable Count: ")
        {
            X = 3,
            Y = 4,
        };
        CheckBox firstWordSyllableCountInputEnabledCheckbox = new()
        {
            X = firstWordSyllableCountLabel.X - 2,
            Y = firstWordSyllableCountLabel.Y,
            Checked = _firstWordSyllableCountInputEnabled,
        };
        ComboBox firstWordSyllableCountComboBox = new(new List<int> { 1, 2 })
        {
            X = firstWordSyllableCountLabel.X,
            Y = firstWordSyllableCountLabel.Y + 1,
            Width = Dim.Percent(50),
            SelectedItem = 0,
            Enabled = _firstWordSyllableCountInputEnabled,
        };

        Label secondWordSyllableCountLabel = new("Second Word Syllable Count: ")
        {
            X = 3,
            Y = 7,
        };
        CheckBox secondWordSyllableCountInputEnabledCheckbox = new()
        {
            X = secondWordSyllableCountLabel.X - 2,
            Y = secondWordSyllableCountLabel.Y,
            Checked = _secondWordSyllableCountInputEnabled,
        };
        ComboBox secondWordSyllableCountComboBox = new(new List<int> { 1, 2 })
        {
            X = secondWordSyllableCountLabel.X,
            Y = secondWordSyllableCountLabel.Y + 1,
            Width = Dim.Percent(50),
            SelectedItem = 0,
            Enabled = _secondWordSyllableCountInputEnabled,
        };

        Label usernameDisplayLabel = new("          ")
        {
            X = Pos.Center(),
            Y = Pos.Center(),
            TextAlignment = TextAlignment.Centered,
        };

        Button newUsernameButton = new("Generate New Username", true)
        {
            X = Pos.Center(),
            Y = 11,
        };

        // Initialize frames
        FrameView inputFrame = new("Filters")
        {
            X = 1,
            Y = 1,
            Width = Dim.Percent(40),
            Height = Dim.Fill()
        };
        inputFrame.Add(
            usernameLengthLabel,
            usernameLengthComboBox,
            firstWordSyllableCountLabel,
            firstWordSyllableCountComboBox,
            secondWordSyllableCountLabel,
            secondWordSyllableCountComboBox,
            newUsernameButton,
            usernameLengthInputEnabledCheckbox,
            firstWordSyllableCountInputEnabledCheckbox,
            secondWordSyllableCountInputEnabledCheckbox
        );

        FrameView resultsFrame = new("Result")
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

        // Set event handlers
        usernameLengthComboBox.SelectedItemChanged += (s, e) => { _usernameLength = Convert.ToInt32(e.Value); };
        usernameLengthInputEnabledCheckbox.Toggled += (s, e) =>
        {
            if (e.NewValue == null) return;

            _usernameLengthInputEnabled = e.NewValue.Value;
            usernameLengthComboBox.Enabled = _usernameLengthInputEnabled;
        };

        firstWordSyllableCountComboBox.SelectedItemChanged += (s, e) => { _firstWordSyllableCount = Convert.ToInt32(e.Value); };
        firstWordSyllableCountInputEnabledCheckbox.Toggled += (s, e) =>
        {
            if (e.NewValue == null) return;

            _firstWordSyllableCountInputEnabled = e.NewValue.Value;
            firstWordSyllableCountComboBox.Enabled = _firstWordSyllableCountInputEnabled;
        };

        secondWordSyllableCountComboBox.SelectedItemChanged += (s, e) => { _secondWordSyllableCount = Convert.ToInt32(e.Value); };
        secondWordSyllableCountInputEnabledCheckbox.Toggled += (s, e) =>
        {
            if (e.NewValue == null) return;

            _secondWordSyllableCountInputEnabled = e.NewValue.Value;
            secondWordSyllableCountComboBox.Enabled = _secondWordSyllableCountInputEnabled;
        };

        newUsernameButton.Clicked += (s, e) =>
        {
            usernameDisplayLabel.Text = service.GetNewCombination(
                _usernameLengthInputEnabled is true ? _usernameLength : null,
                _firstWordSyllableCountInputEnabled is true ? _firstWordSyllableCount : null,
                _secondWordSyllableCountInputEnabled is true ? _secondWordSyllableCount : null
            );
        };
    }
}