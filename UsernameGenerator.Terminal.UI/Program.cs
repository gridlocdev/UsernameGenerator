using Terminal.Gui;

Application.Init();

try
{
    Application.Run<AppWindow>();
}
finally
{
    Application.Shutdown();
}