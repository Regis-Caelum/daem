using Android.App;
using Android.Runtime;
using Firebase;

namespace YabieFrontend;

[Application]
public class MainApplication : MauiApplication
{
    public MainApplication(IntPtr handle, JniHandleOwnership ownership)
        : base(handle, ownership)
    {
        FirebaseApp.InitializeApp(this);
    }

    protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();
}