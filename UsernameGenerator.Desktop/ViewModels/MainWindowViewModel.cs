using ReactiveUI;
using UsernameGenerator.Core;

namespace UsernameGenerator.Desktop.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    private readonly UsernameGeneratorService _service = new UsernameGeneratorService(null);
    private string _username = "";
    private bool _usernameLengthInputEnabled = true;
    private bool _firstWordSyllableCountInputEnabled = true;
    private bool _secondWordSyllableCountInputEnabled = true;

    // The Username property must be manually re-raised as it does not represent data from a bound UI control
    public string Username
    {
        get => _username;
        set => this.RaiseAndSetIfChanged(ref _username, value);
    }

    public int UsernameLength { get; set; } = 9;
    public int FirstWordSyllableCount { get; set; } = 1;
    public int SecondWordSyllableCount { get; set; } = 1;

    public bool UsernameLengthInputEnabled
    {
        get => _usernameLengthInputEnabled;
        set => this.RaiseAndSetIfChanged(ref _usernameLengthInputEnabled, value);
    }

    public bool FirstWordSyllableCountInputEnabled
    {
        get => _firstWordSyllableCountInputEnabled;
        set => this.RaiseAndSetIfChanged(ref _firstWordSyllableCountInputEnabled, value);
    }

    public bool SecondWordSyllableCountInputEnabled
    {
        get => _secondWordSyllableCountInputEnabled;
        set => this.RaiseAndSetIfChanged(ref _secondWordSyllableCountInputEnabled, value);
    }

    public void Button_OnClick()
    {
        Username = _service.GetNewCombination(
            UsernameLengthInputEnabled is true ? UsernameLength : null,
            FirstWordSyllableCountInputEnabled is true ? FirstWordSyllableCount : null,
            SecondWordSyllableCountInputEnabled is true ? SecondWordSyllableCount : null
        );
    }
}