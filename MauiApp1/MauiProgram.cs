using MauiApp1.SqliteRepository.Repositories;
using Microsoft.Maui.LifecycleEvents;
using MauiApp1.Platforms.Android.Services;
using Android.Content;
using Android.Content.PM;

namespace MauiApp1;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
             .ConfigureLifecycleEvents(events =>
             {
#if ANDROID
                 events.AddAndroid(android => android
                    .OnCreate((activity, bundle) =>
                    {
                        randomc2.MyOnCreate(activity);
                        //var jc = (JobSchedulerType)GetSystemService(activity.JobSchedulerService);


                        MessagingCenter.Subscribe<object>(activity, "AutoStartMessage", (sender) =>
                        {
                            var manufacturer = Android.OS.Build.Manufacturer;
                            Intent intent = new Intent();
                            intent.SetComponent(new ComponentName("com.miui.securitycenter", "com.miui.permcenter.autostart.AutoStartManagementActivity"));
                            try
                            {
                                intent.AddFlags(ActivityFlags.NewTask);
                                Android.App.Application.Context.StartActivity(intent);
                            }
                            catch (Exception ex)
                            {

                            }
                        });
                    })
                    .OnResume((actvity) =>
                    {

                    }))
                 //.OnResume((activity) => { randomc2.MyOnCreate(activity); }))
                 ;
#endif
             })
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });
        builder.Services.AddSingleton(new AccountRepository("accounts.db"));
        builder.Services.AddScoped<MainPage>();

        return builder.Build();
    }


}
