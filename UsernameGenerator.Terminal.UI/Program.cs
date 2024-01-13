using Terminal.Gui;
using UsernameGenerator.Terminal.UI;

Application.Init();

try
{
    Application.Run<AppWindow>();
}
finally
{
    Application.Shutdown();
}