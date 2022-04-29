using Android.App;
using Android.Content;
using Android.OS;
using AndroidX.Core.App;
using MauiApp1.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp1.Platforms.Android.Services
{
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
                var notification =notificationBuilder
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
        public static void MyOnCreate(Activity activity, Bundle bundle)
        {
            Intent startServiceIntent = new Intent(activity, typeof(BackgroundService));
            startServiceIntent.SetAction("START");
            activity.StartService(startServiceIntent);
        }
    }


}
