using Avalonia.Interactivity;
using ReactiveUI;
using UsernameGenerator.Core;

namespace UsernameGenerator.Desktop.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    public string Username { get; set; } = "";
    public int UsernameLength { get; set; } = 9;
    public int FirstWordSyllableCount { get; set; } = 1;
    public int SecondWordSyllableCount { get; set; } = 1;

    public void Button_OnClick()
    {
        var service = new UsernameGeneratorService(null); 
        Username += "A";
    }
}