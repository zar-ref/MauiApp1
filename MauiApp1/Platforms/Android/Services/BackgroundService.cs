using Android.Net;
using global::Android.App;
using global::Android.Content;
using global::Android.OS;
using global::AndroidX.Core.App;
using MauiApp1.Platforms.Android.BroadcastReceivers;
using MauiApp1.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App.Job;
using MauiApp1.Platforms.Android.JobServices;

namespace MauiApp1.Platforms.Android.Services
{
    //tutorial: https://stackoverflow.com/questions/57036530/bad-notification-for-startforeground-in-android-app
    [Service]
    public class BackgroundService : Service, IBackgroundService
    {

        public override void OnCreate()
        {
            base.OnCreate();
        }

        public override StartCommandResult OnStartCommand(Intent intent, StartCommandFlags flags, int startId)
        {
            if (intent.Action.Equals("START"))
            {
                String NOTIFICATION_CHANNEL_ID = "com.Your.project.id";
                NotificationChannel chan = new NotificationChannel(NOTIFICATION_CHANNEL_ID, "Your Channel Name", NotificationImportance.High);

                NotificationManager manager = (NotificationManager)GetSystemService(Context.NotificationService);

                manager.CreateNotificationChannel(chan);

                NotificationCompat.Builder notificationBuilder = new NotificationCompat.Builder(this, NOTIFICATION_CHANNEL_ID);

                BuildStartServiceAction();
                var notification = notificationBuilder
                      .SetContentTitle("test")
                      .SetContentText("test")
                      .SetSmallIcon(Resource.Drawable.notification_icon_background)
                      .SetContentIntent(BuildIntentToShowMainActivity())
                      .SetOngoing(true)
                      .AddAction(BuildStartServiceAction())
                      //.AddAction(BuildStopServiceAction())
                      .Build();

                StartForeground(10999, notification);
            }


            return StartCommandResult.Sticky;
        }
        PendingIntent BuildIntentToShowMainActivity()
        {
            var notificationIntent = new Intent(this, typeof(MainActivity));
            var pendingIntent = PendingIntent.GetActivity(this, 0, notificationIntent, PendingIntentFlags.UpdateCurrent);
            return pendingIntent;
        }


        public override IBinder OnBind(Intent intent)
        {
            return null;
        }

        public void StartBackgroundService()
        {
            MessagingCenter.Send<object, string>(this, "FromService", "hello");
        }

        NotificationCompat.Action BuildStartServiceAction()
        {
            var stopServiceIntent = new Intent(this, GetType());
            stopServiceIntent.SetAction("START");
            var stopServicePendingIntent = PendingIntent.GetService(this, 0, stopServiceIntent, 0);

            var builder = new NotificationCompat.Action.Builder(null, "test", stopServicePendingIntent);
            StartBackgroundService();


            return builder.Build();

        }

    }

    public static class randomc2
    {
        private static bool ReceiversRegistered = false;
        public static void MyOnCreate(Activity activity)
        {
            //InternetBroadcastReceiver receiver = new InternetBroadcastReceiver();
            //activity.RegisterReceiver(receiver, new IntentFilter(ConnectivityManager.ConnectivityAction));
            //Intent startServiceIntent = new Intent(activity, typeof(BackgroundService));
            //startServiceIntent.SetAction("START");
            //activity.StartService(startServiceIntent);


            //https://github.com/xamarin/monodroid-samples/blob/main/android5.0/JobScheduler/JobScheduler/MainActivity.cs
            //https://medium.com/@prakharsrivastava_219/schedule-your-task-for-internet-and-relax-b98e5fdb77fa
            RegisterServices(activity);
            var js = (JobScheduler)activity.GetSystemService(Context.JobSchedulerService);
            js.CancelAll();
            ComponentName jobService = new ComponentName(activity, Java.Lang.Class.FromType(typeof(InternetJobService)).Name);

            var jobInfoBuilder = new JobInfo.Builder(1, jobService).SetRequiredNetworkType(NetworkType.Any);
            var jobInfo = jobInfoBuilder.SetPersisted(true).SetMinimumLatency(50).Build(); 
            var result = js.Schedule(jobInfo);
            if(result == JobScheduler.ResultSuccess)
            { 
            }
             
        }

        private static void RegisterServices(Context contextIn)
        {
            if (ReceiversRegistered)
                return;
            Context context = contextIn.ApplicationContext;
            var receiver = new InternetBroadcastReceiver();
            context.RegisterReceiver(receiver , new IntentFilter(Intent.ActionBootCompleted));
            ReceiversRegistered = true;
        }

    }


}
