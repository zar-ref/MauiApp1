using Foundation;
using SQLitePCL;

namespace MauiApp1;

[Register("AppDelegate")]
public class AppDelegate : MauiUIApplicationDelegate
{
    //https://www.youtube.com/watch?v=vvq0etotS8M&ab_channel=CodingFeats
    //protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();
    protected override MauiApp CreateMauiApp()
    {
        raw.SetProvider(new SQLite3Provider_sqlite3());
        MauiProgram.CreateMauiApp();

    }
}
