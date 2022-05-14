using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App.Job;
using Android.OS;
using Android.Widget;
using AndroidX.Core.App;
using global::Android.App;
using global::Android.Content;
using global::Android.Net;
using MauiApp1.Platforms.Android.Services;

namespace MauiApp1.Platforms.Android.JobServices
{ 
    [Service(Exported = true, Permission =  "android.permission.BIND_JOB_SERVICE") ]
    public class InternetJobService : JobService
    {
        //https://docs.microsoft.com/en-us/xamarin/android/platform/android-job-scheduler
        //https://stackoverflow.com/questions/50137936/can-i-send-notification-using-job-scheduler
        //https://medium.com/@prakharsrivastava_219/schedule-your-task-for-internet-and-relax-b98e5fdb77fa
        //https://stackoverflow.com/questions/39310407/android-jobscheduler-using-setminimumlatency-with-setperiodic --usefull
        //https://stackoverflow.com/questions/33740266/run-code-once-after-each-boot-on-android for tomorrow
        public override bool OnStartJob(JobParameters @params)
        {
            //CreateNotificationChannel();
            Toast.MakeText(this, "Executed", ToastLength.Long).Show();
            JobFinished(@params, true);
            return true;
        }

        public override bool OnStopJob(JobParameters @params)
        {
            return true;
        }

        PendingIntent BuildIntentToShowMainActivity()
        {
            var notificationIntent = new Intent(this, typeof(MainActivity));
            var pendingIntent = PendingIntent.GetActivity(this, 0, notificationIntent, PendingIntentFlags.UpdateCurrent);
            return pendingIntent;
        }


        private void CreateNotificationChannel()
        {
            String NOTIFICATION_CHANNEL_ID = "com.Your.project.id";
            NotificationChannel chan = new NotificationChannel(NOTIFICATION_CHANNEL_ID, "Your Channel Name", NotificationImportance.High);

            NotificationManager manager = (NotificationManager)GetSystemService(Context.NotificationService);

            manager.CreateNotificationChannel(chan);

            NotificationCompat.Builder notificationBuilder = new NotificationCompat.Builder(this, NOTIFICATION_CHANNEL_ID);

            var notification = notificationBuilder
                  .SetContentTitle("test")
                  .SetContentText("test")
                  .SetSmallIcon(Resource.Drawable.notification_icon_background)
                  .SetContentIntent(BuildIntentToShowMainActivity())
                  //.SetOngoing(true) 
                  //.AddAction(BuildStopServiceAction())
                  .Build();

            manager.Notify(11000, notification);
        }
}

    public static class JobSchedulerHelpers
    {
        public static JobInfo.Builder CreateJobBuilderUsingJobId<T>(this Context context, int jobId) where T : JobService
        {
            var javaClass = Java.Lang.Class.FromType(typeof(T));
            var componentName = new ComponentName(context, javaClass);
            return new JobInfo.Builder(jobId, componentName);
        }
    }
}
