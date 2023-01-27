using ReactiveUI;
using UsernameGenerator.Core;

namespace UsernameGenerator.Desktop.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    private readonly UsernameGeneratorService _service = new UsernameGeneratorService(null);
    private string _username = "";

    // The Username property must be manually re-raised as it does not represent data from a bound UI control
    public string Username
    {
        get => _username;
        set => this.RaiseAndSetIfChanged(ref _username, value);
    }

    public int UsernameLength { get; set; } = 9;
    public int FirstWordSyllableCount { get; set; } = 1;
    public int SecondWordSyllableCount { get; set; } = 1;

    public void Button_OnClick()
    {
        Username = _service.GetNewCombination(UsernameLength, FirstWordSyllableCount, SecondWordSyllableCount);
    }
}